using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Styx.WoWInternals.DBC;
using Styx.WoWInternals.WoWObjects;
using Styx.WoWInternals;
using Styx.Common;
using Styx.CommonBot.Routines;
using Styx.Helpers;
using Styx.Pathing;
using Styx;
using System.Diagnostics;
using System.Threading;
using System.IO;
using Styx.CommonBot;
using System.Windows.Forms;
using Styx.CommonBot.Inventory;

namespace KingWoW
{
    class DestructionWarlockCombatClass : KingWoWAbstractBaseClass
    {

        private static string Name = "KingWoW DestructionWarlock'";

        #region CONSTANT AND VARIABLES

        //START OF CONSTANTS ==============================
        private bool IsCRPaused = false;
        private const bool LOGGING = true;
        private const bool DEBUG = false;
        private const bool TRACE = false;
        private KingWoWUtility utils = null;
        private Movement movement = null;
        private ExtraUtils extra = null;

        private WoWUnit tank = null;
        private WoWUnit lastTank = null;
        private bool SoloBotType = false;
        private string BaseBot = "unknown";
        private TalentManager talents = null;
        
        private WoWUnit ShadowfuryCandidateTarget = null;
        private WoWUnit blizzardCandidateTarget = null;

        private const string DEBUG_LABEL = "DEBUG";
        private const string TRACE_LABEL = "TRACE";
        private const string TANK_CHANGE = "TANK CHANGED";
        private const string FACING = "FACING";

        //START OF SPELLS AND AURAS ==============================
        private const string DRINK = "Drink";
        private const string FOOD = "Food";

        //END OF SPELLS AND AURAS ==============================

        //TALENTS
        private const string LIFE_TAP = "Life Tap";
        private const string CHARRED_REMAINS = "Charred Remains";
        private const string CHAOS_BOLT = "Chaos Bolt";
        private const string CREATE_HEALTHSTONE = "Create Healthstone";
        private const string HAVOC = "Havoc";
        private const string BACKDRAFT = "Backdraft";
        private const string SHADOWBURN = "Shadowburn";
        private const string FIRE_AND_BRIMSTONE = "Fire and Brimstone";
        private const string IMMOLATE = "Immolate";
        private const string CONFLAGRATE = "Conflagrate";
        private const string RAIN_OF_FIRE = "Rain of Fire";
        private const string INCINERATE = "Incinerate";
        
        private const string DARK_INTENT = "Dark Intent";
        private const string DARK_SOUL = "Dark Soul: Instability";
        private const string ARCHIMONDESDARKNESS = "ArchimondesDarkness";

        private const string SHADOW_FURY = "SHADOW_FURY";
        private const string DARK_FLIGHT = "DARK_FLIGHT";
        private const string GRIMOIRE_OF_SACRIFICE = "Grimoire of Sacrifice";
        
        private const string CHAOTIC_INFUSION = "Chaotic Infusion";
        private const string MARK_OF_BLEEDING_HOLLOW = "Mark of Bleeding Hollow";
        private const string ARCHMAGES_GREATER_INCANDESCENCE = "Archmage's Greater Incandescence";
        private const string HOWLING_SOUL = "Howling Soul";
        private const string VOID_SHARDS = "Void Shards";
        private       int    MyGCD=1500;
        private DateTime     nextTimeCancelMetamorphosis;
        private DateTime     StartCombat;
        private       long   time_to_die=9999;
        //END TALENTS
        //END OF CONSTANTS ==============================

        #endregion

        #region Hotkeys
        private void InitializeHotkey()
        {
            HotkeysManager.Initialize(StyxWoW.Memory.Process.MainWindowHandle);
        }
        private void RegisterHotkeys()
        {
            HotkeysManager.Register("Routine Pause", (Keys)DestructionWarlockSettings.Instance.PauseKey, DestructionWarlockSettings.Instance.ModKey, hk =>
            {
                IsCRPaused = !IsCRPaused;
                if (IsCRPaused)
                {
                    Lua.DoString(@"print('Routine \124cFFE61515 Paused!')");
                    Logging.Write("Routine Paused!");
                }
                else
                {
                    Lua.DoString(@"print('Routine \124cFF15E61C Resumed!')");
                    Logging.Write("Routine Resumed!");
                }
            });
            HotkeysManager.Register("Multidot", (Keys)DestructionWarlockSettings.Instance.MultidotKey, DestructionWarlockSettings.Instance.ModKey, hk =>
            {
                DestructionWarlockSettings.Instance.MultidotEnabled = !DestructionWarlockSettings.Instance.MultidotEnabled;
                if (DestructionWarlockSettings.Instance.MultidotEnabled)
                {
                    Lua.DoString(@"print('Multidot \124cFF15E61C Enabled!')");
                    Logging.Write("Multidot Enabled!");
                }

                else
                {
                    Lua.DoString(@"print('Multidot \124cFFE61515 Disabled!')");
                    Logging.Write("Multidot Disabled!");
                }

            });
            HotkeysManager.Register("AvoidAOE", (Keys)DestructionWarlockSettings.Instance.AvoidAOEKey, DestructionWarlockSettings.Instance.ModKey, hk =>
            {
                DestructionWarlockSettings.Instance.AvoidAOE = !DestructionWarlockSettings.Instance.AvoidAOE;
                if (DestructionWarlockSettings.Instance.AvoidAOE)
                {
                    Lua.DoString(@"print('AvoidAOE \124cFF15E61C Enabled!')");
                    Logging.Write("AvoidAOE Enabled!");
                }

                else
                {
                    Lua.DoString(@"print('AvoidAOE \124cFFE61515 Disabled!')");
                    Logging.Write("AvoidAOE Disabled!");
                }

            });
        }
        private void RemoveHotkeys()
        {
            HotkeysManager.Unregister("Routine Pause");
            HotkeysManager.Unregister("Multidot");
        }

        private void ReRegisterHotkeys()
        {
            RemoveHotkeys();
            RegisterHotkeys();
        }
        #endregion

        public int GetTargetIgniteStrength()
        {
            string command = "return select(15,UnitDebuff(\"target\", \"Ignite\", nil, \"PLAYER\"))";
            int baseDmg = Lua.GetReturnVal<int>(command, 0);
            return baseDmg;       
        }

        public DestructionWarlockCombatClass()
        {
            utils = new KingWoWUtility();
            movement = new Movement();
            extra = new ExtraUtils();
            tank = null;
            lastTank = null; ;
            SoloBotType = false;
            BaseBot = "unknown";
            talents = new TalentManager();
            nextTimeCancelMetamorphosis = DateTime.Now;
            MyGCD = (int)(1500 * Me.SpellHasteModifier);

        }

        public override bool Combat
        {
            get
            {
                if ((Me.Mounted && !DestructionWarlockSettings.Instance.AutoDismountOnCombat) || IsCRPaused || !StyxWoW.IsInGame || !StyxWoW.IsInWorld || Me.Silenced/*|| utils.IsGlobalCooldown(true)*/ || utils.isAuraActive(DRINK) || utils.isAuraActive(FOOD) || Me.IsChanneling || utils.MeIsCastingWithLag())
                    return false;
                //UPDATE TANK
                //tank = utils.GetTank();
                tank = utils.SimpleGetTank(40f);
                if (tank == null || !tank.IsValid || !tank.IsAlive) tank = Me;
                if (tank != null && (lastTank == null || lastTank.Guid != tank.Guid))
                {
                    lastTank = tank;
                    utils.LogActivity(TANK_CHANGE, tank.Class.ToString());
                }
                return CombatRotation();
            }
        }

        public override bool Pulse
        {
            get
            {
                if (Me.IsDead) return MyDeath();
                if ((Me.Mounted && !DestructionWarlockSettings.Instance.AutoDismountOnCombat) || IsCRPaused || !StyxWoW.IsInGame || !StyxWoW.IsInWorld || Me.Silenced/*|| utils.IsGlobalCooldown(true)*/ || utils.isAuraActive(DRINK) || utils.isAuraActive(FOOD) || Me.IsChanneling || utils.MeIsCastingWithLag() || Me.Mounted)
                    return false;
                if (!Me.Combat && DestructionWarlockSettings.Instance.UseEvocation)
                    LifeTap();
                //UPDATE TANK
                //tank = utils.GetTank();
                tank = utils.SimpleGetTank(40f);
                if (tank == null || !tank.IsValid || !tank.IsAlive) tank = Me;

                if (tank != null && (lastTank == null || lastTank.Guid != tank.Guid))
                {
                    lastTank = tank;
                    utils.LogActivity(TANK_CHANGE, tank.Class.ToString());
                }
                if (tank != null && tank.Combat && !Me.Combat)
                    return CombatRotation();
                return false;
            }
        }

        private bool MyDeath()
        {
            if (SoloBotType && Me.IsDead && !Me.IsGhost)
            {

                int Delay = 3000;
                Thread.Sleep(Delay);
                Lua.DoString(string.Format("RunMacroText(\"{0}\")", "/script RepopMe()"));
            }
            return false;
        }

        public override bool Pull
        {
            get
            {
                WoWUnit target = Me.CurrentTarget;
                if (target != null && target.IsDead)
                    Me.ClearTarget();
                else if (target != null && !target.IsFriendly && target.Attackable && !target.IsDead)
                {
                    if (!target.InLineOfSpellSight || target.Distance > DestructionWarlockSettings.Instance.PullDistance)
                    {
                        movement.KingHealMove(target, DestructionWarlockSettings.Instance.PullDistance);
                    }
                    if (utils.CanCast(CONFLAGRATE, target))
                    {
                        if (!Me.IsMoving && !Me.IsFacing(target))
                        {
                            utils.LogActivity(FACING, target.Name);
                            Me.SetFacing(target);
                        }
                        utils.LogActivity("start combate with CONFLAGRATE", target.Name);
                        SetCancelMetamorphosis();
                        StartCombat = DateTime.Now;
                        return utils.Cast(CONFLAGRATE, target);
                    }
                }
                return false;
            }
        }

        public override bool Initialize
        {
            get
            {
                if (ExtraUtilsSettings.Instance.SoundsEnabled)
                {
                    try
                    {
                        SoundManager.LoadSoundFilePath(@"\Routines\King-wow\Sounds\Welcome.wav");
                        SoundManager.SoundPlay();
                    }
                    catch { }
                }
                Logging.Write("Ciao " + Me.Class.ToString());
                Logging.Write("Welcome to " + Name + " custom class");
                Logging.Write("Tanks All HonorBuddy Forum developers for code inspiration!");
                BotEvents.OnBotStartRequested += new BotEvents.OnBotStartStopRequestedDelegate(BotEvents_OnBotStart);
                Lua.Events.AttachEvent("GROUP_ROSTER_UPDATE", UpdateGroupChangeEvent);
                InitializeHotkey();
                //RegisterHotkeys();
                utils.FillParties();
                return true; ;
            }
        }

        private void UpdateGroupChangeEvent(object sender, LuaEventArgs args)
        {
            Logging.Write("Update Groups composition");
            utils.FillParties();
        }

        void BotEvents_OnBotStart(EventArgs args)
        {
            ReRegisterHotkeys();
            talents.Update();
            BotUpdate();
        }

        public override bool NeedCombatBuffs { get { return Buff(); } }

        public override bool NeedPreCombatBuffs { get { return Buff(); } }

        public override bool NeedPullBuffs { get { return Buff(); } }

        public override bool NeedRest
        {
            get
            {
                if (Me.IsDead) return false;
                if ((utils.isAuraActive(DRINK) || utils.isAuraActive(FOOD)) && (Me.ManaPercent < 100 || Me.HealthPercent < 100))
                    return true;
                if (Me.ManaPercent <= DestructionWarlockSettings.Instance.ManaPercent &&
                !utils.isAuraActive(DRINK) && !Me.Combat && !Me.IsMoving && !utils.MeIsCastingWithLag())
                {
                    WoWItem mydrink = Consumable.GetBestDrink(false);
                    if (mydrink != null)
                    {
                        utils.LogActivity("Drinking/Eating");
                        Styx.CommonBot.Rest.DrinkImmediate();
                        return true;
                    }
                }
                if (Me.HealthPercent <= DestructionWarlockSettings.Instance.HealthPercent &&
                !utils.isAuraActive(FOOD) && !Me.Combat && !Me.IsMoving && !utils.MeIsCastingWithLag())
                {
                    WoWItem myfood = Consumable.GetBestFood(false);
                    if (myfood != null)
                    {
                        utils.LogActivity("Eating");
                        Styx.CommonBot.Rest.DrinkImmediate();
                        return true;
                    }
                }
                return false;
            }
        }
        
        public static bool HaveHealthStone { get { return StyxWoW.Me.BagItems.Any(i => i.Entry == 5512); } }
        
        private bool Buff()
        {
            
            if (utils.Mounted() || utils.MeIsCastingWithLag() /*ExtraUtilsSettings.Instance.PauseRotation || */)
                return false;
                
            GetBestPet();
            //GRIMOIRE_OF_SACRIFICE
            if (HasTalent(WarlockTalents.GrimoireOfSacrifice) && utils.CanCast(GRIMOIRE_OF_SACRIFICE) &&  !utils.isAuraActive(GRIMOIRE_OF_SACRIFICE))
            {
                utils.LogActivity(GRIMOIRE_OF_SACRIFICE);
               return utils.Cast(GRIMOIRE_OF_SACRIFICE);
            }
            //HEALTH STONE
            if (!Me.Combat && !HaveHealthStone && Me.Level >= 10 && utils.CanCast(CREATE_HEALTHSTONE) && !Me.IsMoving)
            {
                utils.LogActivity(CREATE_HEALTHSTONE);
                return utils.Cast(CREATE_HEALTHSTONE);
            }
            //Dark Intent
            if (DestructionWarlockSettings.Instance.AutoBuffBrillance && !utils.isAuraActive(DARK_INTENT) && utils.CanCast(DARK_INTENT))
            {
                utils.LogActivity(DARK_INTENT);
                return utils.Cast(DARK_INTENT);
            }

            return false;
        }

        private bool LifeTap()
        {
            if (Me.ManaPercent < 30 && Me.HealthPercent > 60)
            {
                utils.LogActivity(LIFE_TAP);
                return utils.Cast(LIFE_TAP);
            }
            return false;
        }
        
        private bool BotUpdate()
        {
            if (BaseBot.Equals(BotManager.Current.Name))
                return false;
            if (utils.IsBotBaseInUse("LazyRaider") || utils.IsBotBaseInUse("Tyrael"))
            {
                Logging.Write("Detected LazyRaider/tyrael:");
                Logging.Write("Disable all movements");
                SoloBotType = false;
                BaseBot = BotManager.Current.Name;
                return true;
            }

            if (utils.IsBotBaseInUse("Raid Bot"))
            {
                Logging.Write("Detected RaidBot:");
                Logging.Write("Disable all movements");
                SoloBotType = false;
                BaseBot = BotManager.Current.Name;
                return true;
            }


            if (utils.IsBotBaseInUse("BGBuddy"))
            {
                Logging.Write("Detected BGBuddy Bot:");
                Logging.Write("Enable PVP Rotation");
                SoloBotType = false;
                BaseBot = BotManager.Current.Name;
                return true;
            }

            Logging.Write("Base bot detected: " + BotManager.Current.Name);
            SoloBotType = true;
            BaseBot = BotManager.Current.Name;
            return true;
        }

        private bool Defensivececk()
        {

            if (DestructionWarlockSettings.Instance.UseShadowfury && Me.Combat && (utils.AllAttaccableEnemyMobsInRange(12).Count() >= 1) && utils.CanCast(SHADOW_FURY))
            {
                ShadowfuryCandidateTarget = utils.EnemyInRangeWithMobsAround(35, 10, DestructionWarlockSettings.Instance.ShadowfuryAOECount);
                if (ShadowfuryCandidateTarget != null)
                {
                    utils.LogActivity(SHADOW_FURY, ShadowfuryCandidateTarget.Name);
                    utils.Cast(SHADOW_FURY);
                    return SpellManager.ClickRemoteLocation(ShadowfuryCandidateTarget.Location);
                }
            }
            
            return false;
        }
        
        private bool CombatRotation()
        {
            extra.UseHealthstone();
            extra.UseRacials();
            //actions=use_item,name=shards_of_nothing
            extra.UseTrinket1();//虚无碎片
            extra.UseTrinket2();
            extra.WaterSpirit();
            extra.LifeSpirit();
            Defensivececk();
            
            //foreach (var a in Me.GetAllAuras())
            //{
            //    utils.LogActivity(a.Name+"1321123123123s");
            //}
            //Multidot();

            WoWUnit target = null;
            if (DestructionWarlockSettings.Instance.TargetTypeSelected == DestructionWarlockSettings.TargetType.MANUAL)
                target = Me.CurrentTarget;
            else if (DestructionWarlockSettings.Instance.TargetTypeSelected == DestructionWarlockSettings.TargetType.AUTO)
            {
                target = utils.getTargetToAttack(40, tank);
            }
            else if (DestructionWarlockSettings.Instance.TargetTypeSelected == DestructionWarlockSettings.TargetType.SEMIAUTO)
            {
                target = Me.CurrentTarget;
                if (target == null || target.IsDead || !target.InLineOfSpellSight || target.Distance - target.CombatReach -1  > 40)
                    target = utils.getTargetToAttack(40, tank);
            }
            
            if (target != null && !target.IsFriendly && target.Attackable && !target.IsDead && target.InLineOfSpellSight && target.Distance - target.CombatReach -1  <= 40)
            {
                if (DestructionWarlockSettings.Instance.TargetTypeSelected == DestructionWarlockSettings.TargetType.AUTO)
                    target.Target();
                if ((DestructionWarlockSettings.Instance.AutofaceTarget || SoloBotType) && !Me.IsMoving)
                {
                    Me.SetFacing(target);
                }
                if (HasTalent(WarlockTalents.ArchimondesDarkness))
                {
                   Logging.Write("ArchimondesDarkness");
                }
                //actions+=/dark_soul,if=!talent.archimondes_darkness.enabled|(talent.archimondes_darkness.enabled&(charges=2|trinket.proc.any.react|trinket.stacking_any.intellect.react>6|target.time_to_die<40))
                if (utils.CanCast(DARK_SOUL) && !utils.isAuraActive(DARK_SOUL) &&( HasTalent(WarlockTalents.ArchimondesDarkness) || (HasTalent(WarlockTalents.ArchimondesDarkness) && (utils.GetCharges(DARK_SOUL)==2 
                                                                                                                                                     || (int)utils.MyAuraTimeLeft(ARCHMAGES_GREATER_INCANDESCENCE, Me)>6
                                                                                                                                                     || (int)utils.MyAuraTimeLeft(HOWLING_SOUL, Me)>6
                                                                                                                                                     || (int)utils.MyAuraTimeLeft(VOID_SHARDS, Me)>6 
                                                                                                                                                     || utils.isAuraActive(MARK_OF_BLEEDING_HOLLOW)
                                                                                                                                                     || time_to_die<40))))
                {
                    utils.LogActivity(DARK_SOUL);
                    return utils.Cast(DARK_SOUL);
                }
                //actions+=/summon_doomguard,if=!talent.demonic_servitude.enabled&active_enemies(target)<9
                //actions+=/summon_infernal,if=!talent.demonic_servitude.enabled&active_enemies(target)>=9
                if (active_enemies(target) >= 6 || (active_enemies(target) >= 4 && HasTalent(WarlockTalents.CharredRemains)) && !DestructionWarlockSettings.Instance.AvoidAOE)
                {
                    utils.LogActivity("Start AOE");
                    return aoe(target);
                }
                else
                {
                    utils.LogActivity("Start Single");
                    return single(target);
                }
                
            }
            return false;
        }
        
        private bool aoe(WoWUnit target)
        {
            //actions.aoe+=/havoc,target=2,if=(!talent.charred_remains.enabled|buff.fire_and_brimstone.down)
            if (utils.CanCast(HAVOC) && (HasTalent(WarlockTalents.CharredRemains) || !utils.isAuraActive(FIRE_AND_BRIMSTONE)))
            {
                utils.LogActivity(HAVOC, Me.FocusedUnit.Name);
                return utils.Cast(HAVOC, Me.FocusedUnit);
            }
            //actions.aoe+=/shadowburn,if=!talent.charred_remains.enabled&buff.havoc.remains
            if (utils.CanCast(SHADOWBURN) && (HasTalent(WarlockTalents.CharredRemains) && utils.isAuraActive(HAVOC)))
            {
                utils.LogActivity(SHADOWBURN);
                return utils.Cast(SHADOWBURN);
            }
            
            //actions.aoe+=/chaos_bolt,if=!talent.charred_remains.enabled&buff.havoc.remains>cast_time&buff.havoc.stack>=3
            if (utils.CanCast(CHAOS_BOLT) && (HasTalent(WarlockTalents.CharredRemains) && (int)utils.MyAuraTimeLeft(HAVOC, Me)>utils.GetSpellCastTime(CHAOS_BOLT).Milliseconds && utils.GetAuraStack(Me, HAVOC, true)>=3))
            {
                utils.LogActivity(CHAOS_BOLT, target.Name);
                return utils.Cast(CHAOS_BOLT, target);
            }
            
            //actions.aoe+=/fire_and_brimstone,if=buff.fire_and_brimstone.down
            if (utils.CanCast(FIRE_AND_BRIMSTONE))
            {
                utils.LogActivity(FIRE_AND_BRIMSTONE);
                return utils.Cast(FIRE_AND_BRIMSTONE);
            }
            
            //actions.aoe+=/immolate,if=buff.fire_and_brimstone.up&!dot.immolate.ticking
            if (utils.CanCast(IMMOLATE) && utils.isAuraActive(FIRE_AND_BRIMSTONE) && !utils.isAuraActive(IMMOLATE, target))
            {
                utils.LogActivity(IMMOLATE, target.Name);
                return utils.Cast(IMMOLATE, target);
            }
            
            //actions.aoe+=/conflagrate,if=buff.fire_and_brimstone.up&charges=2
            if (utils.CanCast(CONFLAGRATE) && utils.isAuraActive(FIRE_AND_BRIMSTONE) && utils.GetCharges(CONFLAGRATE)==2)
            {
                utils.LogActivity(CONFLAGRATE, target.Name);
                return utils.Cast(CONFLAGRATE, target);
            }
            
            //actions.aoe+=/immolate,if=buff.fire_and_brimstone.up&dot.immolate.remains<=(dot.immolate.duration*0.3)
            if (utils.CanCast(IMMOLATE) && utils.isAuraActive(FIRE_AND_BRIMSTONE) && utils.MyAuraTimeLeft(IMMOLATE, target) < 4500)
            {
                utils.LogActivity(IMMOLATE, target.Name);
                return utils.Cast(IMMOLATE);
            }
            
            //actions.aoe+=/chaos_bolt,if=talent.charred_remains.enabled&buff.fire_and_brimstone.up
            if (utils.CanCast(CHAOS_BOLT) && utils.isAuraActive(FIRE_AND_BRIMSTONE) && HasTalent(WarlockTalents.CharredRemains) && burning_ember>=2.5)
            {
                utils.LogActivity(CHAOS_BOLT, target.Name);
                return utils.Cast(CHAOS_BOLT, target);
            }
            
            //actions.aoe+=/incinerate
            if (utils.CanCast(INCINERATE))
            {
                utils.LogActivity(INCINERATE, target.Name);
                return utils.Cast(INCINERATE, target);
            }
            return false;
        }
        
        private bool single(WoWUnit target)
        {
            //actions.single_target=havoc,target=2
            //if (utils.CanCast(HAVOC) && active_enemies(target)>=2)
            if (utils.CanCast(HAVOC) && target != null)
            {
                utils.LogActivity(HAVOC, Me.FocusedUnit.Name);
                return utils.Cast(HAVOC, Me.FocusedUnit);
            }

            //actions.single_target+=/shadowburn,if=talent.charred_remains.enabled&target.time_to_die<10
            if (utils.CanCast(SHADOWBURN) && HasTalent(WarlockTalents.CharredRemains) && time_to_die<10)
            {
                utils.LogActivity(SHADOWBURN,target.Name);
                return utils.Cast(SHADOWBURN,target);
            }
            
            //actions.single_target+=/fire_and_brimstone,if=buff.fire_and_brimstone.down&dot.immolate.remains<=action.immolate.cast_time&active_enemies(target)>4
            if (utils.CanCast(FIRE_AND_BRIMSTONE) && !utils.isAuraActive(FIRE_AND_BRIMSTONE) && active_enemies(target)>4)
            {
                utils.LogActivity(FIRE_AND_BRIMSTONE);
                return utils.Cast(FIRE_AND_BRIMSTONE);
            }
            
            //actions.single_target+=/immolate,cycle_targets=1,if=remains<=cast_time
            if (utils.CanCast(IMMOLATE) && (int)utils.MyAuraTimeLeft(IMMOLATE, target)<=utils.GetSpellCastTime(IMMOLATE).Milliseconds)
            {
                utils.LogActivity(IMMOLATE, target.Name);
                return utils.Cast(IMMOLATE, target);
            }
            
            if (utils.CanCast(IMMOLATE) && (int)utils.MyAuraTimeLeft(IMMOLATE, Me.FocusedUnit)<=utils.GetSpellCastTime(IMMOLATE).Milliseconds)
            {
                utils.LogActivity(IMMOLATE, Me.FocusedUnit.Name);
                return utils.Cast(IMMOLATE,Me.FocusedUnit);
            }
            
            //actions.single_target+=/cancel_buff,name=fire_and_brimstone,if=buff.fire_and_brimstone.up&dot.immolate.remains>(dot.immolate.duration*0.3)
            if (utils.CanCast(IMMOLATE) && utils.isAuraActive(FIRE_AND_BRIMSTONE) && (int)utils.MyAuraTimeLeft(IMMOLATE, target)>4500)
            {
                utils.LogActivity("Cancel FIRE_AND_BRIMSTONE");
                Me.GetAuraByName(FIRE_AND_BRIMSTONE).TryCancelAura();
                return true;
            }
            
            //actions.single_target+=/shadowburn,if=buff.havoc.remains
            if (utils.CanCast(SHADOWBURN) && utils.isAuraActive(HAVOC))
            {
                utils.LogActivity(SHADOWBURN, target.Name);
                return utils.Cast(SHADOWBURN, target);
            }
            
            //actions.single_target+=/chaos_bolt,if=buff.havoc.remains>cast_time&buff.havoc.stack>=3
            if (utils.CanCast(CHAOS_BOLT) && (int)utils.MyAuraTimeLeft(HAVOC, Me)>utils.GetSpellCastTime(CHAOS_BOLT).Milliseconds &&  utils.GetAuraStack(Me, HAVOC, true)>=3)
            {
                utils.LogActivity(CHAOS_BOLT, target.Name);
                return utils.Cast(CHAOS_BOLT, target);
            }
            
            //actions.single_target+=/conflagrate,if=charges=2
            if (utils.CanCast(CONFLAGRATE) && utils.GetCharges(CONFLAGRATE)==2)
            {
                utils.LogActivity(CONFLAGRATE, target.Name);
                return utils.Cast(CONFLAGRATE, target);
            }
            
            //actions.single_target+=/chaos_bolt,if=talent.charred_remains.enabled&active_enemies(target)>1&target.health.pct>20
            if (utils.CanCast(CHAOS_BOLT) && HasTalent(WarlockTalents.CharredRemains) && active_enemies(target)>1 && Me.CurrentTarget.HealthPercent>20 && burning_ember>=1)
            {
                utils.LogActivity(CHAOS_BOLT, target.Name+active_enemies(target).ToString());
                return utils.Cast(CHAOS_BOLT, target);
            }
            
            //actions.single_target+=/chaos_bolt,if=talent.charred_remains.enabled&buff.backdraft.stack<3&burning_ember>=2.5
            if (utils.CanCast(CHAOS_BOLT) && HasTalent(WarlockTalents.CharredRemains) && utils.GetAuraStack(Me, BACKDRAFT, true)<3 && burning_ember>=2.5)
            {
                utils.LogActivity(CHAOS_BOLT, target.Name);
                return utils.Cast(CHAOS_BOLT, target);
            }

            //actions.single_target+=/chaos_bolt,if=buff.backdraft.stack<3&(burning_ember>=3.5|buff.dark_soul.up|target.time_to_die<20)
            if (utils.CanCast(CHAOS_BOLT) && utils.GetAuraStack(Me, BACKDRAFT, true)<3 && (burning_ember>=3.5 || utils.isAuraActive(DARK_SOUL) || time_to_die<20))
            {
                utils.LogActivity(CHAOS_BOLT, target.Name);
                return utils.Cast(CHAOS_BOLT, target);
            }
            //actions.single_target+=/chaos_bolt,if=buff.backdraft.stack<3&set_bonus.tier17_2pc=1&burning_ember>=2.5
            if (utils.CanCast(CHAOS_BOLT) && utils.GetAuraStack(Me, BACKDRAFT, true)<3 && utils.isAuraActive(CHAOTIC_INFUSION) && burning_ember>=2.5)
            {
                utils.LogActivity(CHAOS_BOLT, target.Name);
                return utils.Cast(CHAOS_BOLT, target);
            }
            //actions.single_target+=/chaos_bolt,if=buff.backdraft.stack<3&buff.archmages_greater_incandescence_int.react&buff.archmages_greater_incandescence_int.remains>cast_time
            if (utils.CanCast(CHAOS_BOLT) && utils.GetAuraStack(Me, BACKDRAFT, true)<3 && utils.isAuraActive(ARCHMAGES_GREATER_INCANDESCENCE) && (int)utils.MyAuraTimeLeft(ARCHMAGES_GREATER_INCANDESCENCE, Me)>utils.GetSpellCastTime(CHAOS_BOLT).Milliseconds)
            {
                utils.LogActivity(CHAOS_BOLT, target.Name);
                return utils.Cast(CHAOS_BOLT, target);
            }

            //actions.single_target+=/chaos_bolt,if=buff.backdraft.stack<3&trinket.proc.intellect.react&trinket.proc.intellect.remains>cast_time
            //actions.single_target+=/chaos_bolt,if=buff.backdraft.stack<3&trinket.proc.crit.react&trinket.proc.crit.remains>cast_time
            //actions.single_target+=/chaos_bolt,if=buff.backdraft.stack<3&trinket.stacking_proc.multistrike.react>=8&trinket.stacking_proc.multistrike.remains>=cast_time
            //actions.single_target+=/chaos_bolt,if=buff.backdraft.stack<3&trinket.proc.multistrike.react&trinket.proc.multistrike.remains>cast_time
            //actions.single_target+=/chaos_bolt,if=buff.backdraft.stack<3&trinket.proc.versatility.react&trinket.proc.versatility.remains>cast_time
            //actions.single_target+=/chaos_bolt,if=buff.backdraft.stack<3&trinket.proc.mastery.react&trinket.proc.mastery.remains>cast_time
            if (utils.CanCast(CHAOS_BOLT) && utils.GetAuraStack(Me, BACKDRAFT, true)<3 && utils.isAuraActive(HOWLING_SOUL) && (int)utils.MyAuraTimeLeft(HOWLING_SOUL, Me)>utils.GetSpellCastTime(CHAOS_BOLT).Milliseconds)
            {
                utils.LogActivity(CHAOS_BOLT, target.Name);
                return utils.Cast(CHAOS_BOLT, target);
            }
            if (utils.CanCast(CHAOS_BOLT) && utils.GetAuraStack(Me, BACKDRAFT, true)<3 && utils.isAuraActive(MARK_OF_BLEEDING_HOLLOW) && (int)utils.MyAuraTimeLeft(MARK_OF_BLEEDING_HOLLOW, Me)>utils.GetSpellCastTime(CHAOS_BOLT).Milliseconds)
            {
                utils.LogActivity(CHAOS_BOLT, target.Name);
                return utils.Cast(CHAOS_BOLT, target);
            }
            //actions.single_target+=/fire_and_brimstone,if=buff.fire_and_brimstone.down&dot.immolate.remains<=(dot.immolate.duration*0.3)&active_enemies(target)>4
            if (utils.CanCast(FIRE_AND_BRIMSTONE) && !utils.isAuraActive(FIRE_AND_BRIMSTONE) && (int)utils.MyAuraTimeLeft(IMMOLATE, target)<=4500 && active_enemies(target)>4)
            {
                utils.LogActivity(FIRE_AND_BRIMSTONE);
                return utils.Cast(FIRE_AND_BRIMSTONE);
            }
            //actions.single_target+=/immolate,cycle_targets=1,if=remains<=(duration*0.3)
            if (utils.CanCast(IMMOLATE) && (int)utils.MyAuraTimeLeft(IMMOLATE, target)<=4500)
            {
                utils.LogActivity(IMMOLATE, target.Name);
                return utils.Cast(IMMOLATE, target);
            }
            //actions.single_target+=/conflagrate
            if (utils.CanCast(CONFLAGRATE))
            {
                utils.LogActivity(CONFLAGRATE, target.Name);
                return utils.Cast(CONFLAGRATE, target);
            }
            //actions.single_target+=/incinerate
            if (utils.CanCast(INCINERATE))
            {
                utils.LogActivity(INCINERATE, target.Name);
                return utils.Cast(INCINERATE, target);
            }
            return false;
        }


        private bool Multidot()
        {
            if (DestructionWarlockSettings.Instance.MultidotEnabled)
            {
                int enemyNumber = utils.AllAttaccableEnemyMobsInRangeTargettingMyParty(40f, DestructionWarlockSettings.Instance.MultidotAvoidCC).Count();
                if (enemyNumber >= DestructionWarlockSettings.Instance.MultidotEnemyNumberMin)
                {
                    WoWUnit TargetForMultidot = null;
                    //apply  Nether Tempest and always refresh it right before the last tick;
                    //if (utils.CanCast(CORRUPTION) && utils.AllEnemyMobsHasMyAura(CORRUPTION).Count() < DestructionWarlockSettings.Instance.MultidotEnemyNumberMax)
                    //{
                    //    TargetForMultidot = utils.NextApplyAuraTarget(CORRUPTION, 40, 1000, DestructionWarlockSettings.Instance.MultidotAvoidCC, DestructionWarlockSettings.Instance.AvoidDOTPlayers);
                    //    if (TargetForMultidot != null)
                    //    {
                    //        utils.LogActivity("   MULTIDOT   " + NETHER_TEMPEST, TargetForMultidot.Name);
                    //        return utils.Cast(CORRUPTION, TargetForMultidot);
                    //    }
                    //}

                    //apply  Living Bomb and refresh it right before or right after the last tick (the expiring Living Bomb will explode in both cases);
                    //if (utils.CanCast(LIVING_BOMB) && utils.AllEnemyMobsHasMyAura(LIVING_BOMB).Count() < 3)
                    //{
                    //    TargetForMultidot = utils.NextApplyAuraTarget(LIVING_BOMB, 40, 1000, DestructionWarlockSettings.Instance.MultidotAvoidCC, DestructionWarlockSettings.Instance.AvoidDOTPlayers);
                    //    if (TargetForMultidot != null)
                    //    {
                    //        utils.LogActivity("   MULTIDOT   " + LIVING_BOMB, TargetForMultidot.Name);
                    //        return utils.Cast(LIVING_BOMB, TargetForMultidot);
                    //    }
                    //}

                }
            }
            return false;
        }
        
        private bool HasTalent(WarlockTalents tal)
        {
            //Logging.Write("Has Talent"+(int)tal);
            return talents.IsSelected((int)tal);
            //return true;
        }
        
        public enum WarlockTalents
        {
        
            DarkRegeneration = 1,
            SoulLeech,
            HarvestLife,
            SearingFlames = HarvestLife,
        
            HowlOfTerror,
            MortalCoil,
            SHADOW_FURY,
        
            SoulLink,
            SacrificialPact,
            DarkBargain,
        
            BloodHorror,
            BurningRush,
            UnboundWill,
        
            GrimoireOfSupremacy,
            GrimoireOfService,
            GrimoireOfSacrifice,
            GRIMOIRE_OF_SYNERGY = GrimoireOfSacrifice,
        
            SoulburnHaunt,
            Demonbolt = SoulburnHaunt,
            ArchimondesDarkness = SoulburnHaunt,
            KiljaedensCunning,
            MannorothsFury,
        
            CharredRemains,
            Cataclysm,
            DemonicServitude
        }
        
        private static uint CurrentDemonicFury { get { return Me.GetCurrentPower(WoWPowerType.DemonicFury); } }
        
        #region Pet Support

        /// <summary>
        /// determines the best WarlockPet value to use.  Attempts to use 
        /// user setting first, but if choice not available yet will choose Imp 
        /// for instances and Voidwalker for everything else.  
        /// </summary>
        /// <returns>WarlockPet to use</returns>
        public bool GetBestPet()
        {
            if (Me.GotAlivePet || utils.isAuraActive(GRIMOIRE_OF_SACRIFICE))
                return false;

            WarlockPet bestPet = (WarlockPet)DestructionWarlockSettings.Instance.PetToSummon;
            
            string spellName = "Summon " + bestPet.ToString();
            
            if (utils.CanCast(spellName) && !Me.IsMoving)
            {
                utils.LogActivity(spellName);
                return utils.Cast(spellName);
            }
            else if (utils.CanCast("Summon Voidwalker") && !Me.IsMoving)
            {
                utils.LogActivity("Summon Voidwalker");
                return utils.Cast("Summon Voidwalker");
            }
            return false;
            
        }

        public enum WarlockPet
        {
            None        = 0,    
            Imp         = 23,       // Pet.CreatureFamily.Id
            Voidwalker  = 16,
            Succubus    = 17,
            Felhunter   = 15,
            Felguard    = 29,
            Doomguard   = 19,
            Infernal	  = 108,
            Other       = 99999     // a quest or other pet forced upon us for some reason
        }
       
        #endregion
        
        public void SetCancelMetamorphosis()
        {
            //in periond of start boost, cancel Metamorphosis before 15s
            nextTimeCancelMetamorphosis = DateTime.Now + new TimeSpan(0, 0, 0, 0, 15000);
        }
        
        public double burning_ember { get { return Me.GetPowerInfo(WoWPowerType.BurningEmbers).Current / 10; } }
        public int active_enemies(WoWUnit target) 
        { 
        	return utils.AllAttaccableEnemyMobsInRangeFromTarget(target, 10).Count();
        }
       
    }
}
