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
    class DemonologyWarlockCombatClass : KingWoWAbstractBaseClass
    {

        private static string Name = "KingWoW DemonologyWarlock'";

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
        private DateTime nextTimeRuneOfPowerAllowed;
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

        private const string FROSTFIRE_BOLT = "Frostfire Bolt";
        private const string FIRE_BLAST = "Fire Blast";
        private const string ROCKET_JUMP = "Rocket Jump";
        private const string COUNTERSPELL = "Counterspell";
        private const string SUMMON_WATER_ELEMENTAL = "Summon Water Elemental";
        private const string FROSTBOLT = "Frostbolt";
        private const string POLYMORPH = "Polymorph";
        private const string ARCANE_EXPLOSION = "Arcane Explosion";
        private const string ICE_LANCE = "Ice Lance";
        private const string FINGER_OF_FROST = "Fingers of Frost";
        private const string CONE_OF_COLD = "Cone of Cold";
        private const string REMOVE_CURSE = "Remove Curse";
        private const string SLOW_FALL = "Slow Fall";
        private const string MOLTEN_ARMOR = "Molten Armor";
        private const string CONJURE_REFRESHMENT = "Conjure Refreshment";
        private const string FLAMESTRIKE = "Flamestrike";
        private const string CREATE_HEALTHSTONE = "Create Healthstone";
        private const string MIRROR_IMAGE = "Mirror Image";
        private const string BLIZZARD = "Blizzard";
        private const string Hand_of_Guldan = "Hand of Gul'dan";
        private const string INVISIBILITY = "Invisibility";
        private const string DARK_INTENT = "Dark Intent";
        private const string DARK_SOUL = "Dark Soul: Knowledge";
        private const string FROZEN_ORB = "Frozen Orb";
        private const string DEEP_FREEZE = "Deep Freeze";
        private const string CONJURE_REFRESHMENT_TABLE = "Conjure Refreshment Table";
        private const string BRAIN_FREEZE = "Brain Freeze";
        private const string MAGE_ARMOR = "Mage Armor";
        private const string TIME_WARP = "Time Warp";
        private const string ALTER_TIME = "Alter Time";

        private const string BRILLIANT_MANA_GEM = "Brilliant Mana Gem";
        private const string MANA_GEM = "Brilliant Mana Gem";

        private const string FREEZE = "Freeze";
        //END OF SPELLS AND AURAS ==============================

        //TALENTS
        private const string ArchimondesDarkness = "ArchimondesDarkness";
        private const string TOUCH_OF_CHAOS = "Touch of Chaos";
        private const string LIFE_TAP = "Life Tap";
        private const string BURNING_RUSH = "Burning Rush";
        private const string BLAZING_SPEED = "Blazing Speed";
        private const string RING_OF_FROST = "Ring of Frost";
        private const string FROSTJAW = "Frostjaw";
        private const string GREATER_INVISIBILITY = "Greater Invisibility";

        private const string COLD_SNAP = "Cold Snap";
        private const string NETHER_TEMPEST = "Nether Tempest";
        
        private const string INVOCATION_BUFF = "Invoker's Energy";
        private const string RUNE_OF_POWER = "Rune of Power";

        private const string METAMORPHOSIS = "Metamorphosis";
        private const string CORRUPTION = "Corruption";
        private const string PYROBLAST_PROC = "Pyroblast!";
        private const string INFERNO_BLAST = "Inferno Blast";
        private const string SHADOW_BOLT = "Shadow Bolt";
        private const string SHADOWFURY = "Shadowfury";
        
         

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
            HotkeysManager.Register("Routine Pause", (Keys)DemonologyWarlockSettings.Instance.PauseKey, DemonologyWarlockSettings.Instance.ModKey, hk =>
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
            HotkeysManager.Register("Multidot", (Keys)DemonologyWarlockSettings.Instance.MultidotKey, DemonologyWarlockSettings.Instance.ModKey, hk =>
            {
                DemonologyWarlockSettings.Instance.MultidotEnabled = !DemonologyWarlockSettings.Instance.MultidotEnabled;
                if (DemonologyWarlockSettings.Instance.MultidotEnabled)
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
            HotkeysManager.Register("AvoidAOE", (Keys)DemonologyWarlockSettings.Instance.AvoidAOEKey, DemonologyWarlockSettings.Instance.ModKey, hk =>
            {
                DemonologyWarlockSettings.Instance.AvoidAOE = !DemonologyWarlockSettings.Instance.AvoidAOE;
                if (DemonologyWarlockSettings.Instance.AvoidAOE)
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

        public DemonologyWarlockCombatClass()
        {
            utils = new KingWoWUtility();
            movement = new Movement();
            extra = new ExtraUtils();
            tank = null;
            lastTank = null; ;
            SoloBotType = false;
            BaseBot = "unknown";
            nextTimeRuneOfPowerAllowed = DateTime.Now;
            talents = new TalentManager();

        }

        public override bool Combat
        {
            get
            {
                if ((Me.Mounted && !DemonologyWarlockSettings.Instance.AutoDismountOnCombat) || IsCRPaused || !StyxWoW.IsInGame || !StyxWoW.IsInWorld || Me.Silenced/*|| utils.IsGlobalCooldown(true)*/ || utils.isAuraActive(DRINK) || utils.isAuraActive(FOOD) || Me.IsChanneling || utils.MeIsCastingWithLag())
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
                if ((Me.Mounted && !DemonologyWarlockSettings.Instance.AutoDismountOnCombat) || IsCRPaused || !StyxWoW.IsInGame || !StyxWoW.IsInWorld || Me.Silenced/*|| utils.IsGlobalCooldown(true)*/ || utils.isAuraActive(DRINK) || utils.isAuraActive(FOOD) || Me.IsChanneling || utils.MeIsCastingWithLag() || Me.Mounted)
                    return false;

                if (!Me.Combat && DemonologyWarlockSettings.Instance.UseEvocation)
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
                    if (!target.InLineOfSpellSight || target.Distance > DemonologyWarlockSettings.Instance.PullDistance)
                    {
                        movement.KingHealMove(target, DemonologyWarlockSettings.Instance.PullDistance);
                    }
                    if (utils.CanCast(SHADOW_BOLT, target)/*&& !Me.IsMoving*/)
                    {
                        if (!Me.IsMoving && !Me.IsFacing(target))
                        {
                            utils.LogActivity(FACING, target.Name);
                            Me.SetFacing(target);
                        }

                        utils.LogActivity(SHADOW_BOLT, target.Name);
                        return utils.Cast(SHADOW_BOLT, target);
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
                Logging.Write("Powered by Attilio76");
                BotEvents.OnBotStartRequested += new BotEvents.OnBotStartStopRequestedDelegate(BotEvents_OnBotStart);
                Lua.Events.AttachEvent("GROUP_ROSTER_UPDATE", UpdateGroupChangeEvent);
                InitializeHotkey();
                RegisterHotkeys();
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
                if (Me.ManaPercent <= DemonologyWarlockSettings.Instance.ManaPercent &&
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
                if (Me.HealthPercent <= DemonologyWarlockSettings.Instance.HealthPercent &&
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

        public void SetNextTimeRuneOfPower()
        {
            //3 seconds wait to avoid popping 2 rune of frost cause has high priority
            nextTimeRuneOfPowerAllowed = DateTime.Now + new TimeSpan(0, 0, 0, 0, 3000);
        }
        
        public static bool HaveHealthStone { get { return StyxWoW.Me.BagItems.Any(i => i.Entry == 5512); } }

        private bool Buff()
        {
            if (utils.Mounted() || utils.MeIsCastingWithLag() /*ExtraUtilsSettings.Instance.PauseRotation || */)
                return false;
            //Mana Gem
            if (!Me.Combat && !HaveHealthStone && Me.Level >= 10 && utils.CanCast(CREATE_HEALTHSTONE) && !Me.IsMoving)
            {
                utils.LogActivity(CREATE_HEALTHSTONE);
                return utils.Cast(CREATE_HEALTHSTONE);
            }

            //Dark Intent
            if (DemonologyWarlockSettings.Instance.AutoBuffBrillance && !utils.isAuraActive(DARK_INTENT) && utils.CanCast(DARK_INTENT))
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

        private bool ProcWork()
        {
            //cast  Frost Bomb on cooldown.
//actions+=/hand_of_guldan,if=!in_flight&dot.shadowflame.remains<travel_time+action.shadow_bolt.cast_time&talent.demonbolt.enabled&((set_bonus.tier17_4pc=0&((charges=1&recharge_time<4)|charges=2))|(charges=3|(charges=2&recharge_time<13.8-travel_time*2))|dot.shadowflame.remains>travel_time)

            if (utils.CanCast(Hand_of_Guldan))
            {
                WoWUnit target = null;
                if (DemonologyWarlockSettings.Instance.TargetTypeSelected == DemonologyWarlockSettings.TargetType.MANUAL)
                    target = Me.CurrentTarget;
                else if (DemonologyWarlockSettings.Instance.TargetTypeSelected == DemonologyWarlockSettings.TargetType.AUTO)
                {
                    target = utils.getTargetToAttack(40, tank);
                }
                else if (DemonologyWarlockSettings.Instance.TargetTypeSelected == DemonologyWarlockSettings.TargetType.SEMIAUTO)
                {
                    target = Me.CurrentTarget;
                    if (target == null || target.IsDead || !target.InLineOfSpellSight || target.Distance - target.CombatReach - 1 > 40)
                        target = utils.getTargetToAttack(40, tank);
                }
                
                if (target != null && !target.IsDead && target.InLineOfSpellSight && target.Distance - target.CombatReach - 1 <= 40)
                {
                    utils.LogActivity(Hand_of_Guldan, target.Name);
                    return utils.Cast(Hand_of_Guldan, target);
                }
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

            if (DemonologyWarlockSettings.Instance.UseShadowfury && Me.Combat && (utils.AllAttaccableEnemyMobsInRange(12).Count() >= 1) && utils.CanCast(SHADOWFURY))
            {
                ShadowfuryCandidateTarget = utils.EnemyInRangeWithMobsAround(35, 10, DemonologyWarlockSettings.Instance.ShadowfuryAOECount);
                if (ShadowfuryCandidateTarget != null)
                {
                    utils.LogActivity(SHADOWFURY, ShadowfuryCandidateTarget.Name);
                    utils.Cast(SHADOWFURY);
                    return SpellManager.ClickRemoteLocation(ShadowfuryCandidateTarget.Location);
                }
            }

            if (DemonologyWarlockSettings.Instance.UseBlink && Me.Combat && Me.IsMoving && (utils.AllAttaccableEnemyMobsInRange(12).Count() >= 1) && utils.CanCast(ROCKET_JUMP))
            {
                utils.LogActivity(ROCKET_JUMP);
                return utils.Cast(ROCKET_JUMP);
            }
            return false;
        }
        
        private bool CombatRotation()
        {
            extra.UseHealthstone();
            extra.UseRacials();
            extra.UseTrinket1();//虚无碎片
            extra.WaterSpirit();
            extra.LifeSpirit();
            Defensivececk();
            UseCD();
            ProcWork();
            //Multidot();

            WoWUnit target = null;
            if (DemonologyWarlockSettings.Instance.TargetTypeSelected == DemonologyWarlockSettings.TargetType.MANUAL)
                target = Me.CurrentTarget;
            else if (DemonologyWarlockSettings.Instance.TargetTypeSelected == DemonologyWarlockSettings.TargetType.AUTO)
            {
                target = utils.getTargetToAttack(40, tank);
            }
            else if (DemonologyWarlockSettings.Instance.TargetTypeSelected == DemonologyWarlockSettings.TargetType.SEMIAUTO)
            {
                target = Me.CurrentTarget;
                if (target == null || target.IsDead || !target.InLineOfSpellSight || target.Distance - target.CombatReach -1  > 40)
                    target = utils.getTargetToAttack(40, tank);
            }
            
            if (target != null && !target.IsFriendly && target.Attackable && !target.IsDead && target.InLineOfSpellSight && target.Distance - target.CombatReach -1  <= 40)
            {
                if (DemonologyWarlockSettings.Instance.TargetTypeSelected == DemonologyWarlockSettings.TargetType.AUTO)
                    target.Target();
                if ((DemonologyWarlockSettings.Instance.AutofaceTarget || SoloBotType) && !Me.IsMoving)
                {
                    Me.SetFacing(target);
                }
                
                if (utils.CanCast("Grimoire: Infernal") && utils.isAuraActive(DARK_SOUL) && utils.AllAttaccableEnemyMobsInRangeFromTarget(target, 10).Count() >= 9)
                {
                    utils.LogActivity("Grimoire: Infernal");
                    return utils.Cast("Grimoire: Infernal");
                }
                else if (utils.CanCast("Grimoire: Doomguard") && utils.isAuraActive(DARK_SOUL))
                {
                    utils.LogActivity("Grimoire: Doomguard");
                    return utils.Cast("Grimoire: Doomguard");
                }

                //apply dot
                if (utils.MyAuraTimeLeft(CORRUPTION, target) < 3500 && !utils.isAuraActive(METAMORPHOSIS))
                {
                    utils.LogActivity("CORRUPTION", target.Name);
                    return utils.Cast(CORRUPTION, target);
                }
                //if (!Me.IsMoving && nextTimeVampiricTouchAllowed <= DateTime.Now && utils.MyAuraTimeLeft(VAMPIRIC_TOUCH, target) < 4500
                //    && !(talents.IsSelected(9) && utils.MyAuraTimeLeft(DEVOURING_PLAGUE, target) > 0) && !(Me.IsChanneling && Me.ChanneledCastingSpellId == MIND_FLY_INSANITY))
                //{
                //    utils.LogActivity("VAMPIRIC_TOUCH", target.Name);
                //    SetNextTimeVampiricTouch();
                //    return utils.Cast(VAMPIRIC_TOUCH, target);
                //}

                //apply  Nether Tempest and always refresh it right before the last tick;
                //if (utils.CanCast(NETHER_TEMPEST, target) && (utils.MyAuraTimeLeft(NETHER_TEMPEST, target) < 1500) && !(target.IsPlayer && DemonologyWarlockSettings.Instance.AvoidDOTPlayers))
                //{
                //    utils.LogActivity(NETHER_TEMPEST, target.Name);
                //    return utils.Cast(NETHER_TEMPEST, target);
                //}

                //apply  Living Bomb and refresh it right before or right after the last tick (the expiring Living Bomb will explode in both cases);
                //if (utils.CanCast(LIVING_BOMB, target) && (utils.MyAuraTimeLeft(LIVING_BOMB, target) < 1500) && !(target.IsPlayer && DemonologyWarlockSettings.Instance.AvoidDOTPlayers))
                //{
                //    utils.LogActivity(LIVING_BOMB, target.Name);
                //    return utils.Cast(LIVING_BOMB, target);
                //}

                //+++++++++++++++++++++++++AOE rotation start+++++++++++++++++++++++++++++++//
                if (DemonologyWarlockSettings.Instance.UseFlameStrike && utils.CanCast(FLAMESTRIKE) && !FrostMageSettings.Instance.AvoidAOE && target.Distance <= 40 && utils.AllAttaccableEnemyMobsInRangeFromTarget(target, 10).Count() >= DemonologyWarlockSettings.Instance.AOECount)
                {
                    utils.LogActivity(FLAMESTRIKE, target.Name);
                    utils.Cast(FLAMESTRIKE);
                    return SpellManager.ClickRemoteLocation(target.Location);
                }

                //if (DemonologyWarlockSettings.Instance.UseDragonBreath && utils.CanCast(DRAGON_BREATH) && !FrostMageSettings.Instance.AvoidAOE && utils.AllAttaccableEnemyMobsInRange(15).Count() >= DemonologyWarlockSettings.Instance.AOECount)
                //{
                //    utils.LogActivity(DRAGON_BREATH);
                //    return utils.Cast(DRAGON_BREATH);
                //}
                //
                //if (DemonologyWarlockSettings.Instance.UseArcaneExplosion && utils.CanCast(ARCANE_EXPLOSION) && !FrostMageSettings.Instance.AvoidAOE && utils.AllAttaccableEnemyMobsInRange(10).Count() >= DemonologyWarlockSettings.Instance.AOECount)
                //{
                //    utils.LogActivity(ARCANE_EXPLOSION);
                //    return utils.Cast(ARCANE_EXPLOSION);
                //}
                //
                //if (DemonologyWarlockSettings.Instance.UseRingOfFrost && utils.CanCast(RING_OF_FROST) && target.Distance2DSqr <= 30 * 30)
                //{
                //    utils.LogActivity(RING_OF_FROST, target.Name);
                //    utils.Cast(RING_OF_FROST);
                //    return SpellManager.ClickRemoteLocation(target.Location);
                //}

                Multidot(); 

                //Cast  SHADOW_BOLT as a filler spell.
                if (!Me.IsMoving && utils.CanCast(SHADOW_BOLT, target))
                {
                    utils.LogActivity(SHADOW_BOLT, target.Name);
                    return utils.Cast(SHADOW_BOLT, target);
                }

                //+++++++++++++++++++++++DPS moving   START+++++++++++++++++++++++++++
                //BURNING_RUSH
                if (Me.IsMoving && utils.CanCast(BURNING_RUSH) && !utils.isAuraActive(BURNING_RUSH))
                {
                    utils.LogActivity(BURNING_RUSH);
                    utils.Cast(BURNING_RUSH);
                }
                //TOUCH_OF_CHAOS
                if (Me.IsMoving && SpellManager.HasSpell(TOUCH_OF_CHAOS) && !utils.isAuraActive(METAMORPHOSIS))
                {
                    utils.LogActivity(TOUCH_OF_CHAOS, target.Name);
                    return utils.Cast(TOUCH_OF_CHAOS, target);
                }
            }
            else if (ExtraUtilsSettings.Instance.movementEnabled && Me.CurrentTarget != null && !Me.CurrentTarget.IsDead && (!Me.CurrentTarget.InLineOfSpellSight || Me.CurrentTarget.Distance - Me.CurrentTarget.CombatReach - 1 > DemonologyWarlockSettings.Instance.PullDistance))
            {
                movement.KingHealMove(Me.CurrentTarget, DemonologyWarlockSettings.Instance.PullDistance);
            }
            
            return false;
        }

        //Ice veyn
        //Mirror image  Summon Doomguard  Grimoire: Doomguard Grimoire: Infernal 
        //Alter Time
        private bool UseCD()
        {
            if (Me.Combat && Me.GotTarget)
            {

                if (extra.IsTargetBoss())
                {
                    if (utils.CanCast(DARK_SOUL) && DemonologyWarlockSettings.Instance.CDUseDarkSoul == DemonologyWarlockSettings.CDUseType.BOSS)
                    {
                        utils.LogActivity(DARK_SOUL);
                        return utils.Cast(DARK_SOUL);
                    }
                    else if (utils.CanCast(DARK_SOUL) && DemonologyWarlockSettings.Instance.CDUseDarkSoul == DemonologyWarlockSettings.CDUseType.CONDITION)
                    {
                        if (utils.GetAuraStack(Me, DARK_SOUL, true) == 2)
                        {
                            utils.LogActivity(DARK_SOUL);
                            return utils.Cast(DARK_SOUL);
                        }
                        else if (!HasTalent(WarlockTalents.ArchimondesDarkness))
                        {
                            utils.LogActivity(DARK_SOUL);
                            return utils.Cast(DARK_SOUL);
                        }
                        else if (Me.CurrentTarget.HealthPercent < DemonologyWarlockSettings.Instance.Phase1KillBossHP && CurrentDemonicFury>400)
                        {
                            utils.LogActivity(DARK_SOUL);
                            return utils.Cast(DARK_SOUL);
                        }
                        else if (Me.CurrentTarget.HealthPercent < DemonologyWarlockSettings.Instance.Phase2KillBossHP)
                        {
                            utils.LogActivity(DARK_SOUL);
                            return utils.Cast(DARK_SOUL);
                        }
                        else if (Me.GetAuraByName(PYROBLAST_PROC).TimeLeft.TotalMilliseconds >= 2000 && CurrentDemonicFury>=400)
                        {
                            utils.LogActivity(DARK_SOUL);
                            return utils.Cast(DARK_SOUL);
                        }
                    }
                    
                }
            }
            return false;
        }

        private bool Multidot()
        {
            if (DemonologyWarlockSettings.Instance.MultidotEnabled)
            {
                int enemyNumber = utils.AllAttaccableEnemyMobsInRangeTargettingMyParty(40f, DemonologyWarlockSettings.Instance.MultidotAvoidCC).Count();
                if (enemyNumber >= DemonologyWarlockSettings.Instance.MultidotEnemyNumberMin)
                {
                    WoWUnit TargetForMultidot = null;
                    //apply  Nether Tempest and always refresh it right before the last tick;
                    if (utils.CanCast(NETHER_TEMPEST) && utils.AllEnemyMobsHasMyAura(NETHER_TEMPEST).Count() < DemonologyWarlockSettings.Instance.MultidotEnemyNumberMax)
                    {
                        TargetForMultidot = utils.NextApplyAuraTarget(NETHER_TEMPEST, 40, 1000, DemonologyWarlockSettings.Instance.MultidotAvoidCC, DemonologyWarlockSettings.Instance.AvoidDOTPlayers);
                        if (TargetForMultidot != null)
                        {
                            utils.LogActivity("   MULTIDOT   " + NETHER_TEMPEST, TargetForMultidot.Name);
                            return utils.Cast(NETHER_TEMPEST, TargetForMultidot);
                        }
                    }

                    //apply  Living Bomb and refresh it right before or right after the last tick (the expiring Living Bomb will explode in both cases);
                    //if (utils.CanCast(LIVING_BOMB) && utils.AllEnemyMobsHasMyAura(LIVING_BOMB).Count() < 3)
                    //{
                    //    TargetForMultidot = utils.NextApplyAuraTarget(LIVING_BOMB, 40, 1000, DemonologyWarlockSettings.Instance.MultidotAvoidCC, DemonologyWarlockSettings.Instance.AvoidDOTPlayers);
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
        
        #region Talents
        public static bool HasTalent(WarlockTalents tal)
        {
            return talents.IsSelected((int)tal);
        }
        
        public enum WarlockTalents
        {
        
            DarkRegeneration = 1,
            SoulLeech,
            HarvestLife,
            SearingFlames = HarvestLife,
        
            HowlOfTerror,
            MortalCoil,
            Shadowfury,
        
            SoulLink,
            SacrificialPact,
            DarkBargain,
        
            BloodHorror,
            BurningRush,
            UnboundWill,
        
            GrimoireOfSupremacy,
            GrimoireOfService,
            GrimoireOfSacrifice,
            GrimoireOfSynergy = GrimoireOfSacrifice,
        
            ArchimondesDarkness,
            KiljaedensCunning,
            MannorothsFury,
        
            SoulburnHaunt,
            Demonbolt = SoulburnHaunt,
            CharredRemains = SoulburnHaunt,
            Cataclysm,
            DemonicServitude
        }
        #endregion
        
        private static uint CurrentDemonicFury { get { return Me.GetCurrentPower(WoWPowerType.DemonicFury); } }
    }
}
