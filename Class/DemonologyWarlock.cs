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
        private const string HAND_OF_GULDAN = "Hand of Gul'dan";
        private const string INVISIBILITY = "Invisibility";
        private const string DARK_INTENT = "Dark Intent";
        private const string DARK_SOUL = "Dark Soul: Knowledge";
        private const string FROZEN_ORB = "Frozen Orb";
        private const string CONJURE_REFRESHMENT_TABLE = "Conjure Refreshment Table";
        private const string MAGE_ARMOR = "Mage Armor";
        private const string TIME_WARP = "Time Warp";
        private const string ALTER_TIME = "Alter Time";

        private const string CHAOS_WAVE = "Chaos Wave";

        //END OF SPELLS AND AURAS ==============================

        //TALENTS
        private const string ARCHIMONDESDARKNESS = "ArchimondesDarkness";
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
        private const string SHADOWFLAME = "Hand of Gul'dan";
        private const string MOLTEN_CORE = "Molten Core";
        private const string SOUL_FIRE = "Soul Fire";
        private const string DOOM = "Doom";
        private const string CORRUPTION = "Corruption";
        private const string INFERNO_BLAST = "Inferno Blast";
        private const string SHADOW_BOLT = "Shadow Bolt";
        private const string SHADOW_FURY = "Shadow Fury";
        private const string DARK_FLIGHT = "Dark Flight";
        private const string GRIMOIRE_OF_SACRIFICE = "GrimoireOfSacrifice";
        private const string IMMOLATION_AURA = "Immolation Aura";
        private const string FELSTORM = "Felstorm";
        private const string WRATHSTORM = "Wrathstorm";
        private const string IMP_SWARM = "Imp Swarm";
        private const string HELLFIRE = "Hellfire";
        private const string MORTAL_CLEAVE = "Mortal Cleave";
        private const string MARK_OF_BLEEDING_HOLLOW = "Mark of Bleeding Hollow";
        private const string ARCHMAGES_GREATER_INCANDESCENCE = "Archmage's Greater Incandescence";
        private const string HOWLING_SOUL = "Howling Soul";
        private const string VOID_SHARDS = "Void Shards";
        
        private       int    MyGCD = 1500;
        private DateTime     nextTimeUseNoGcd;
        private DateTime     startTime_hand_of_guldan;
        private DateTime     StartCombat;
        private       int    molten_core_execute_time;
        private const int    hand_of_guldan_travel_time = 1500;
        private       long   time_to_die=9999;
        private       int    time_elapse=0;
        private       bool   start_combat=false;

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
            talents = new TalentManager();
            nextTimeUseNoGcd = DateTime.Now;
            MyGCD = (int)(1500 * Me.SpellHasteModifier);

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
                if ((tank.Combat || Me.Combat) && !start_combat)
                {
                    utils.LogActivity("start combat");
                    SetnextTimeUseNoGcd();
                    StartCombat = DateTime.Now;
                    startTime_hand_of_guldan = DateTime.Now;
                    start_combat = true;
                }
                else if(!tank.Combat && !Me.Combat)
                {
                	  utils.LogActivity("start combat stop");
                	  start_combat = false;
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
                    if (utils.CanCast(HAND_OF_GULDAN, target))
                    {
                        if (!Me.IsMoving && !Me.IsFacing(target))
                        {
                            utils.LogActivity(FACING, target.Name);
                            Me.SetFacing(target);
                        }
                        utils.LogActivity("start combate with HAND OF GULDAN", target.Name);
                        return utils.Cast(HAND_OF_GULDAN, target);
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
                RegisterHotkeys();
                utils.FillParties();
                molten_core_execute_time = (int)utils.GetSpellCastTime(SOUL_FIRE).TotalMilliseconds/2;
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
        
        public static bool HaveHealthStone { get { return StyxWoW.Me.BagItems.Any(i => i.Entry == 5512); } }
        
        private bool Buff()
        {
            
            if (utils.Mounted() || utils.MeIsCastingWithLag() /*ExtraUtilsSettings.Instance.PauseRotation || */)
                return false;
                
            GetBestPet();
            //HEALTH STONE
            if (HasTalent(WarlockTalents.GrimoireOfSacrifice) && utils.CanCast(GRIMOIRE_OF_SACRIFICE) && !utils.isAuraActive(GRIMOIRE_OF_SACRIFICE))
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

            if (DemonologyWarlockSettings.Instance.UseShadowfury && Me.Combat && (utils.AllAttaccableEnemyMobsInRange(12).Count() >= 1) && utils.CanCast(SHADOW_FURY))
            {
                ShadowfuryCandidateTarget = utils.EnemyInRangeWithMobsAround(35, 10, DemonologyWarlockSettings.Instance.ShadowfuryAOECount);
                if (ShadowfuryCandidateTarget != null)
                {
                    utils.LogActivity(SHADOW_FURY, ShadowfuryCandidateTarget.Name);
                    utils.Cast(SHADOW_FURY);
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
            
            //foreach (var a in Me.GetAllAuras())
            //{
            //    utils.LogActivity(a.Name+"1321123123123s");
            //}
            Multidot();

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
                
                UseCD(target);
                //foreach (var a in target.GetAllAuras())
                //{
                //    utils.LogActivity(a.Name+"1321123123123s");
                //}
                
                //if (utils.CanCast(SOUL_FIRE, target) && utils.isAuraActive(METAMORPHOSIS))
                //    utils.LogActivity("bbbbbbbbbbbbbbbbbbbbbbbb");
                //else
                //    utils.LogActivity("aaaaaaaaaaaaaaaaaaaaaaaa");   
                //if (utils.isAuraActive(METAMORPHOSIS))
                //    utils.LogActivity("ccccccccccccccccccccccccccccc");
                //else
                //    utils.LogActivity("ddddddddddddddddddddddddddddddd");  
                if (nextTimeUseNoGcd>DateTime.Now)
                    return true;
                if (utils.CanCast(SOUL_FIRE, target)&& time_elapse<20)
                {  
                	  time_elapse=(DateTime.Now-StartCombat).Seconds;
                	  utils.LogActivity("time_elapse==========="+time_elapse);
                }
                //utils.LogActivity("aaaaaaaaaaaaaaaaaaaaaaaa"+(int)utils.MyAuraTimeLeft("Shadowflame", target));
                //actions+=/hand_of_guldan,if=!in_flight&dot.shadowflame.remains<travel_time+action.shadow_bolt.cast_time&(((set_bonus.tier17_4pc=0&((charges=1&recharge_time()<4)|charges=2))|(charges=3|(charges=2&recharge_time()<13.8-travel_time*2))&((cooldown.cataclysm.remains>dot.shadowflame.duration)|!talent.cataclysm.enabled))|dot.shadowflame.remains>travel_time)
                if (utils.CanCast(HAND_OF_GULDAN) && !hand_of_guldan_in_flight && (int)utils.MyAuraTimeLeft(SHADOWFLAME, target)<hand_of_guldan_travel_time+utils.GetSpellCastTime(SHADOW_BOLT).TotalMilliseconds && 
                (((!set_bonus.tier17_4pc && ((utils.GetCharges(HAND_OF_GULDAN)==1 && recharge_time()<4) || utils.GetCharges(HAND_OF_GULDAN)==2)) 
                   || (utils.GetCharges(HAND_OF_GULDAN)==3 || (utils.GetCharges(HAND_OF_GULDAN)==2 && recharge_time()<13800-hand_of_guldan_travel_time*2))) || (int)utils.MyAuraTimeLeft(SHADOWFLAME, target)>hand_of_guldan_travel_time))
                {
                    utils.LogActivity(HAND_OF_GULDAN, target.Name);
                    startTime_hand_of_guldan = DateTime.Now;
                    return utils.Cast(HAND_OF_GULDAN, target);
                }
                //actions+=/hand_of_guldan,if=!in_flight&dot.shadowflame.remains<travel_time+action.shadow_bolt.cast_time&talent.demonbolt.enabled&((set_bonus.tier17_4pc=0&((charges=1&recharge_time()<4)|charges=2))|(charges=3|(charges=2&recharge_time()<13.8-travel_time*2))|dot.shadowflame.remains>travel_time)
                //actions+=/hand_of_guldan,if=!in_flight&dot.shadowflame.remains<3.7&time<5&buff.demonbolt.remains<gcd*2&(charges>=2|set_bonus.tier17_4pc=0)&action.dark_soul.charges>=1
                //actions+=/service_pet,if=talent.grimoire_of_service.enabled&(target.time_to_die>120|target.time_to_die<=25|(buff.dark_soul.remains&target.health.pct<20))
                if (utils.CanCast("Grimoire: Doomguard") && HasTalent(WarlockTalents.GrimoireOfService) && (time_to_die>120 || time_to_die<=25 || (utils.isAuraActive(DARK_SOUL) && Me.CurrentTarget.HealthPercent<20)))
                {
                    utils.LogActivity("Grimoire: Doomguard", target.Name);
                    return utils.Cast("Grimoire: Doomguard", target);
                }
                //actions+=/summon_doomguard,if=!talent.demonic_servitude.enabled&active_enemies<9
                //actions+=/summon_infernal,if=!talent.demonic_servitude.enabled&active_enemies>=9
                //actions+=/kiljaedens_cunning,if=!cooldown.cataclysm.remains&buff.metamorphosis.up
                //actions+=/cataclysm,if=buff.metamorphosis.up   
                //actions+=/immolation_aura,if=demonic_fury>450&active_enemies>=3&buff.immolation_aura.down  
                if (utils.CanCast(SOUL_FIRE, target) && utils.isAuraActive(METAMORPHOSIS) && demonic_fury>450 && active_enemies_surround()>=3 && !utils.isAuraActive(IMMOLATION_AURA))
                {
                    utils.LogActivity(IMMOLATION_AURA, target.Name);
                    return utils.Cast(IMMOLATION_AURA, target);
                }
                
                //actions+=/doom,if=buff.metamorphosis.up&target.time_to_die>=30*spell_haste&remains<=(duration*0.3)&(remains<cooldown.cataclysm.remains|!talent.cataclysm.enabled)&trinket.stacking_proc.multistrike.react<10
                if (utils.CanCast(SOUL_FIRE, target) && utils.isAuraActive(METAMORPHOSIS) && time_to_die>=27 && utils.MyAuraTimeLeft(DOOM, target) < 3500 )
                {
                    utils.LogActivity(DOOM, target.Name);
                    return utils.Cast(DOOM, target);
                }
                
                //actions+=/corruption,cycle_targets=1,if=target.time_to_die>=6&remains<=(0.3*duration)&buff.metamorphosis.down
                if (utils.CanCast(CORRUPTION, target) && !utils.isAuraActive(METAMORPHOSIS) && time_to_die>=6 && utils.MyAuraTimeLeft(CORRUPTION, target) < 3500 )
                {
                    utils.LogActivity(CORRUPTION, target.Name);
                    return utils.Cast(CORRUPTION, target);
                }
                
                //actions+=/cancel_metamorphosis,if=buff.metamorphosis.up&((demonic_fury<650&!glyph.dark_soul.enabled)|demonic_fury<450)&buff.dark_soul.down&(trinket.stacking_proc.multistrike.down&trinket.proc.any.down|demonic_fury<(800-cooldown.dark_soul.remains*(10%spell_haste)))&target.time_to_die>20
                if(utils.CanCast(SOUL_FIRE) && utils.isAuraActive(METAMORPHOSIS) && ((demonic_fury<650 && !HasGlyph(DARK_SOUL)) || demonic_fury<450) && !utils.isAuraActive(DARK_SOUL) && time_to_die>20)
                {
                    if(!utils.isAuraActive(MARK_OF_BLEEDING_HOLLOW) && !utils.isAuraActive(ARCHMAGES_GREATER_INCANDESCENCE) && !utils.isAuraActive(HOWLING_SOUL))
                    {    
                        Me.GetAuraByName(METAMORPHOSIS).TryCancelAura();
                        utils.LogActivity("Cancel Metamorphosis for next dark soul with no other buff"+demonic_fury);
                        return true;
                    }
                    else if(utils.GetSpellCooldown(DARK_SOUL).Seconds <= 20 && utils.GetCharges(DARK_SOUL)==0 && demonic_fury<300)
                    {    
                        Me.GetAuraByName(METAMORPHOSIS).TryCancelAura();
                        utils.LogActivity("Cancel Metamorphosis for next dark soul with other buff"+demonic_fury);
                        return true;
                    }
                }
                //actions+=/cancel_metamorphosis,if=buff.metamorphosis.up&action.hand_of_guldan.charges>0&dot.shadowflame.remains<action.hand_of_guldan.travel_time+action.shadow_bolt.cast_time&((demonic_fury<100&buff.dark_soul.remains>10)|time<15)&!glyph.dark_soul.enabled
                if(utils.CanCast(SOUL_FIRE) && utils.isAuraActive(METAMORPHOSIS)	&& utils.GetCharges(CHAOS_WAVE)>0 && !HasGlyph(DARK_SOUL)
                	&& (int)utils.MyAuraTimeLeft(SHADOWFLAME, target)<hand_of_guldan_travel_time+utils.GetSpellCastTime(SHADOW_BOLT).TotalMilliseconds
                	&& ((demonic_fury<100 && utils.MyAuraTimeLeft(DARK_SOUL, Me)>10000) || time_elapse<15))
                {
                    utils.LogActivity("Cancel Metamorphosis for start boost"+demonic_fury);
                    Me.GetAuraByName(METAMORPHOSIS).TryCancelAura();
                    return true;
                }
                //actions+=/cancel_metamorphosis,if=buff.metamorphosis.up&action.hand_of_guldan.charges=3&(!buff.dark_soul.remains>gcd|action.metamorphosis.cooldown<gcd)
                if(utils.CanCast(SOUL_FIRE) && utils.isAuraActive(METAMORPHOSIS) && utils.GetCharges(CHAOS_WAVE)==3 && (utils.MyAuraTimeLeft(DARK_SOUL, Me)<MyGCD || (int)utils.GetSpellCooldown(METAMORPHOSIS).TotalMilliseconds<MyGCD))
                {
                    utils.LogActivity("Cancel Metamorphosis before METAMORPHOSIS"+demonic_fury);
                    Me.GetAuraByName(METAMORPHOSIS).TryCancelAura();
                    return true;
                }
                //actions+=/chaos_wave,if=buff.metamorphosis.up&(buff.dark_soul.up&active_enemies>=2|(charges=3|set_bonus.tier17_4pc=0&charges=2))
                if (utils.CanCast(SOUL_FIRE, target) && utils.isAuraActive(METAMORPHOSIS) && demonic_fury>=80  && utils.isAuraActive(DARK_SOUL) && (active_enemies_aoe(target)>=2 || (utils.GetCharges(CHAOS_WAVE)==3 || (!set_bonus.tier17_4pc && utils.GetCharges(CHAOS_WAVE)==2))))
                {
                    utils.LogActivity(CHAOS_WAVE, target.Name);
                    return utils.Cast(CHAOS_WAVE, target);
                }
                //actions+=/soul_fire,if=buff.metamorphosis.up&buff.molten_core.react&(buff.dark_soul.remains>execute_time|target.health.pct<=25)&(((buff.molten_core.stack*execute_time>=trinket.stacking_proc.multistrike.remains-1|demonic_fury<=ceil((trinket.stacking_proc.multistrike.remains-buff.molten_core.stack*execute_time)*40)+80*buff.molten_core.stack)|target.health.pct<=25)&trinket.stacking_proc.multistrike.remains>=execute_time|trinket.stacking_proc.multistrike.down|!trinket.has_stacking_proc.multistrike)
                if (utils.CanCast(SOUL_FIRE, target) && demonic_fury>=80 && utils.isAuraActive(METAMORPHOSIS) && utils.isAuraActive(MOLTEN_CORE) && (utils.MyAuraTimeLeft(DARK_SOUL, Me)>molten_core_execute_time || Me.CurrentTarget.HealthPercent<25))
                {
                    utils.LogActivity(SOUL_FIRE, target.Name);
                    return utils.Cast(SOUL_FIRE, target);
                }
                //actions+=/touch_of_chaos,cycle_targets=1,if=buff.metamorphosis.up&dot.corruption.remains<17.4&demonic_fury>750
                if (utils.CanCast(SOUL_FIRE, target) && utils.isAuraActive(METAMORPHOSIS) && utils.MyAuraTimeLeft(CORRUPTION, target)<17400 && demonic_fury>750)
                {
                    utils.LogActivity(TOUCH_OF_CHAOS, target.Name);
                    return utils.Cast(TOUCH_OF_CHAOS, target);
                }
                //actions+=/touch_of_chaos,if=buff.metamorphosis.up
                if (utils.CanCast(SOUL_FIRE, target) && demonic_fury>=40 && utils.isAuraActive(METAMORPHOSIS))
                {
                    utils.LogActivity(TOUCH_OF_CHAOS+target.Name+utils.MyAuraTimeLeft(ARCHMAGES_GREATER_INCANDESCENCE, Me)+utils.MyAuraTimeLeft(HOWLING_SOUL, Me)+utils.MyAuraTimeLeft(MARK_OF_BLEEDING_HOLLOW, Me));
                    return utils.Cast(TOUCH_OF_CHAOS, target);
                }
                //actions+=/metamorphosis,if=buff.dark_soul.remains>gcd&(time>6|debuff.shadowflame.stack=2)&(demonic_fury>300|!glyph.dark_soul.enabled)&(demonic_fury>=80&buff.molten_core.stack>=1|demonic_fury>=40)
                if (utils.CanCast(METAMORPHOSIS) && !utils.isAuraActive(METAMORPHOSIS)
                && utils.MyAuraTimeLeft(DARK_SOUL, Me)>MyGCD
                && (time_elapse>6 || utils.GetAuraStack(target, SHADOWFLAME, false)==2)
                && (demonic_fury>300 || !HasGlyph(DARK_SOUL))
                && ((demonic_fury>=80 && utils.GetAuraStack(Me, MOLTEN_CORE, true)>=1) || (demonic_fury>=40 && demonic_fury<80)) )
                {
                    if(utils.Cast(METAMORPHOSIS))
                    {
                    	  utils.LogActivity(METAMORPHOSIS+demonic_fury+"with dark soul");
                        SetnextTimeUseNoGcd();
                        return true;
                    }
                    else
                    {
                        utils.LogActivity("5555555555555555555555555");
                        return false;
                    }
                    
                }
                //actions+=/metamorphosis,if=(trinket.stacking_proc.multistrike.react|trinket.proc.any.react)&((demonic_fury>450&action.dark_soul.recharge_time()>=10&glyph.dark_soul.enabled)|(demonic_fury>650&cooldown.dark_soul.remains>=10))
                if (utils.CanCast(METAMORPHOSIS) && !utils.isAuraActive(METAMORPHOSIS) && (utils.isAuraActive(MARK_OF_BLEEDING_HOLLOW) || utils.isAuraActive(ARCHMAGES_GREATER_INCANDESCENCE) || utils.isAuraActive(HOWLING_SOUL)) && ((demonic_fury>=450 && HasGlyph(DARK_SOUL)) || ( ((int)utils.GetSpellCooldown(DARK_SOUL).TotalMilliseconds>10000) && utils.GetCharges(DARK_SOUL)==0 && demonic_fury>=650)))
                {
                    if(utils.Cast(METAMORPHOSIS))
                    {
                    	  utils.LogActivity(METAMORPHOSIS+demonic_fury+"with other buff and have enough dark soul cooldown");
                        SetnextTimeUseNoGcd();
                        return true;
                    }
                    else
                    {
                        utils.LogActivity("666666666666666666666666");
                        return false;
                    }
                }
                //actions+=/metamorphosis,if=!cooldown.cataclysm.remains&talent.cataclysm.enabled
                //actions+=/metamorphosis,if=!dot.doom.ticking&target.time_to_die>=30%(1%spell_haste)&demonic_fury>300
                if (utils.CanCast(METAMORPHOSIS) && demonic_fury>300 && !utils.isAuraActive(DOOM, target) && Me.CurrentTarget.HealthPercent > 9)
                {
                    if(utils.Cast(METAMORPHOSIS))
                    {
                    	  utils.LogActivity(METAMORPHOSIS+demonic_fury+"for doom");
                        SetnextTimeUseNoGcd();
                        return true;
                    }
                    else
                    {
                        utils.LogActivity("1111111111111111111111111");
                        return false;
                    }
                }
                //actions+=/metamorphosis,if=(demonic_fury>750&(action.hand_of_guldan.charges=0|(!dot.shadowflame.ticking&!action.hand_of_guldan.in_flight_to_target)))|floor(demonic_fury%80)*action.soul_fire.execute_time>=target.time_to_die
                if (utils.CanCast(METAMORPHOSIS) && !utils.isAuraActive(METAMORPHOSIS) && ((demonic_fury>750 && (utils.GetCharges(HAND_OF_GULDAN)==0 || (!utils.isAuraActive(SHADOWFLAME, target) && hand_of_guldan_in_flight))) || (demonic_fury>240 && time_to_die < 10)))
                {
                    if(utils.Cast(METAMORPHOSIS))
                    {
                    	  utils.LogActivity(METAMORPHOSIS+demonic_fury+"for hand_of_guldan");
                        SetnextTimeUseNoGcd();
                        return true;
                    }
                    else
                    {
                        utils.LogActivity("22222222222222222222222");
                        return false;
                    }
                }
                //actions+=/metamorphosis,if=demonic_fury>=950
                if (utils.CanCast(METAMORPHOSIS) && demonic_fury>=950 && !utils.isAuraActive(METAMORPHOSIS))
                {
                    if(utils.Cast(METAMORPHOSIS))
                    {
                    	  utils.LogActivity(METAMORPHOSIS+demonic_fury+"with overload demonic_fury");
                        SetnextTimeUseNoGcd();
                        return true;
                    }
                    else
                    {
                        utils.LogActivity("33333333333333333333333333333");
                        return false;
                    }
                }
                
                //actions+=/cancel_metamorphosis
                if(utils.CanCast(SOUL_FIRE) && utils.isAuraActive(METAMORPHOSIS))
                {
                    utils.LogActivity("Cancel Metamorphosis when no demonic_fury"+demonic_fury);
                    Me.GetAuraByName(METAMORPHOSIS).TryCancelAura();
                    //SetnextTimeUseNoGcd();
                    return true;
                }
                
                //actions+=/imp_swarm
                if (utils.CanCast(IMP_SWARM))
                {
                    utils.LogActivity(IMP_SWARM, target.Name);
                    return utils.Cast(IMP_SWARM, target);
                }
                //actions+=/hellfire,interrupt=1,if=active_enemies>=5
                if (utils.CanCast(HELLFIRE) && active_enemies_surround()>=5)
                {
                    utils.LogActivity(HELLFIRE, target.Name);
                    return utils.Cast(HELLFIRE, target);
                }
                
                //actions+=/soul_fire,if=buff.molten_core.react&(buff.molten_core.stack>=7|target.health.pct<=25|(buff.dark_soul.remains&cooldown.metamorphosis.remains>buff.dark_soul.remains)|trinket.proc.any.remains>execute_time|trinket.stacking_proc.multistrike.remains>molten_core_execute_time)
                //                                             &(buff.dark_soul.remains<action.shadow_bolt.cast_time|buff.dark_soul.remains>execute_time)
                if (utils.CanCast(SOUL_FIRE) && (utils.GetAuraStack(target, MOLTEN_CORE, true)>=7 || Me.CurrentTarget.HealthPercent<25 || (utils.isAuraActive(DARK_SOUL) && utils.GetSpellCooldown(METAMORPHOSIS).TotalMilliseconds>(int)utils.MyAuraTimeLeft(DARK_SOUL, Me)) || (int)utils.MyAuraTimeLeft(ARCHMAGES_GREATER_INCANDESCENCE, Me)>molten_core_execute_time
                                                                                                                                                                                                                                                                         || (int)utils.MyAuraTimeLeft(HOWLING_SOUL, Me)>molten_core_execute_time
                                                                                                                                                                                                                                                                         || (int)utils.MyAuraTimeLeft(MARK_OF_BLEEDING_HOLLOW, Me)>molten_core_execute_time)
                   && utils.isAuraActive(MOLTEN_CORE) && ((int)utils.MyAuraTimeLeft(DARK_SOUL, Me)<utils.GetSpellCastTime(SHADOW_BOLT).TotalMilliseconds || (int)utils.MyAuraTimeLeft(DARK_SOUL, Me)>molten_core_execute_time))
                {
                    utils.LogActivity(SOUL_FIRE, target.Name);
                    return utils.Cast(SOUL_FIRE, target);
                }
                //actions+=/soul_fire,if=buff.molten_core.react&target.time_to_die<(time+target.time_to_die)*0.25+cooldown.dark_soul.remains
                if (utils.CanCast(SOUL_FIRE) && utils.isAuraActive(MOLTEN_CORE) && time_to_die<20 )
                {
                    utils.LogActivity(SOUL_FIRE, target.Name);
                    return utils.Cast(SOUL_FIRE, target);
                }
                //actions+=/life_tap,if=mana.pct<40&buff.dark_soul.down
                if (utils.CanCast(LIFE_TAP) && Me.ManaPercent<40 && !utils.isAuraActive(DARK_SOUL) )
                {
                    utils.LogActivity(LIFE_TAP);
                    return utils.Cast(LIFE_TAP);
                }
                //actions+=/hellfire,interrupt=1,if=active_enemies>=4
                //actions+=/shadow_bolt
                if (utils.CanCast(SHADOW_BOLT))
                {
                    utils.LogActivity(SHADOW_BOLT, target.Name);
                    return utils.Cast(SHADOW_BOLT, target);
                }
                //actions+=/hellfire,moving=1,interrupt=1
                //actions+=/life_tap
                
                //apply dot
                
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
                if (DemonologyWarlockSettings.Instance.UseFlameStrike && utils.CanCast(FLAMESTRIKE) && !DemonologyWarlockSettings.Instance.AvoidAOE && target.Distance <= 40 && utils.AllAttaccableEnemyMobsInRangeFromTarget(target, 10).Count() >= DemonologyWarlockSettings.Instance.AOECount)
                {
                    utils.LogActivity(FLAMESTRIKE, target.Name);
                    utils.Cast(FLAMESTRIKE);
                    return SpellManager.ClickRemoteLocation(target.Location);
                }

                //if (DemonologyWarlockSettings.Instance.UseDragonBreath && utils.CanCast(DRAGON_BREATH) && !DemonologyWarlockSettings.Instance.AvoidAOE && utils.AllAttaccableEnemyMobsInRange(15).Count() >= DemonologyWarlockSettings.Instance.AOECount)
                //{
                //    utils.LogActivity(DRAGON_BREATH);
                //    return utils.Cast(DRAGON_BREATH);
                //}
                //
                //if (DemonologyWarlockSettings.Instance.UseArcaneExplosion && utils.CanCast(ARCANE_EXPLOSION) && !DemonologyWarlockSettings.Instance.AvoidAOE && utils.AllAttaccableEnemyMobsInRange(10).Count() >= DemonologyWarlockSettings.Instance.AOECount)
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
                if (Me.IsMoving && SpellManager.HasSpell(TOUCH_OF_CHAOS) && utils.isAuraActive(METAMORPHOSIS))
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
        private bool UseCD(WoWUnit target)
        {
            if (Me.Combat && Me.GotTarget)
            {

                if (utils.CanCast(DARK_SOUL) && DemonologyWarlockSettings.Instance.CDUseDarkSoul == DemonologyWarlockSettings.CDUseType.BOSS)
                {
                    utils.LogActivity(DARK_SOUL);
                    return utils.Cast(DARK_SOUL);
                }
                else if (utils.CanCast(DARK_SOUL) && DemonologyWarlockSettings.Instance.CDUseDarkSoul == DemonologyWarlockSettings.CDUseType.CONDITION && !utils.isAuraActive(DARK_SOUL))
                {
                    //(charges=2&(time>6|(debuff.shadowflame.stack=1&action.hand_of_guldan.in_flight)))
                    if (utils.GetCharges(DARK_SOUL)==2 && (time_elapse>6 || (utils.GetAuraStack(target, SHADOWFLAME, true)==1 && hand_of_guldan_in_flight)))
                    {
                        utils.LogActivity(DARK_SOUL);
                        return utils.Cast(DARK_SOUL);
                    }
                    //!talent.archimondes_darkness.enabled
                    else if (!HasTalent(WarlockTalents.ArchimondesDarkness))
                    {
                        utils.LogActivity(DARK_SOUL);
                        return utils.Cast(DARK_SOUL);
                    }
                    //(target.time_to_die<=20&!glyph.dark_soul.enabled)
                    else if (time_to_die<=20 && !HasGlyph(DARK_SOUL))
                    {
                        utils.LogActivity(DARK_SOUL);
                        return utils.Cast(DARK_SOUL);
                    }
                    //target.time_to_die<=10|(target.time_to_die<=60&demonic_fury>400)
                    else if (time_to_die<=10 || (time_to_die<=60 && demonic_fury>400))
                    {
                        utils.LogActivity(DARK_SOUL);
                        return utils.Cast(DARK_SOUL);
                    }
                    //((trinket.proc.any.react|trinket.stacking_proc.any.react)&(demonic_fury>600|(glyph.dark_soul.enabled&demonic_fury>450))))
                    else if ((demonic_fury>600 || (HasGlyph(DARK_SOUL) && demonic_fury>450)) && ((int)utils.MyAuraTimeLeft(ARCHMAGES_GREATER_INCANDESCENCE, Me)>8
                                                                                                || (int)utils.MyAuraTimeLeft(HOWLING_SOUL, Me)>8
                                                                                                || (int)utils.MyAuraTimeLeft(VOID_SHARDS, Me)>16 
                                                                                                || (int)utils.MyAuraTimeLeft(MARK_OF_BLEEDING_HOLLOW, Me)>3))
                    {
                        utils.LogActivity(DARK_SOUL+"with other burst");
                        return utils.Cast(DARK_SOUL);
                    }
                }
                
                //actions+=/imp_swarm,if=!talent.demonbolt.enabled&(buff.dark_soul.up|(cooldown.dark_soul.remains>(120%(1%spell_haste)))|time_to_die<32)&time>3
                if (utils.CanCast(IMP_SWARM))
                {
                    utils.LogActivity(IMP_SWARM, target.Name);
                    return utils.Cast(IMP_SWARM, target);
                }
                //actions+=/felguard:felstorm
                if (utils.CanCast(FELSTORM))
                {
                    utils.LogActivity(FELSTORM, target.Name);
                    return utils.Cast(FELSTORM, target);
                }
                
                //actions+=/wrathguard:wrathstorm
                if (utils.CanCast(FELSTORM))
                {
                    utils.LogActivity(WRATHSTORM, target.Name);
                    return utils.Cast(WRATHSTORM, target);
                }
                
                //actions+=/wrathguard:mortal_cleave,if=pet.wrathguard.cooldown.wrathstorm.remains>5
                if (utils.CanCast(MORTAL_CLEAVE) && utils.GetSpellCooldown(WRATHSTORM).Seconds>5)
                {
                    utils.LogActivity(MORTAL_CLEAVE, target.Name);
                    return utils.Cast(MORTAL_CLEAVE, target);
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
                    if (utils.CanCast(CORRUPTION) && utils.AllEnemyMobsHasMyAura(CORRUPTION).Count() < DemonologyWarlockSettings.Instance.MultidotEnemyNumberMax)
                    {
                        TargetForMultidot = utils.NextApplyAuraTarget(CORRUPTION, 40, 1000, DemonologyWarlockSettings.Instance.MultidotAvoidCC, DemonologyWarlockSettings.Instance.AvoidDOTPlayers);
                        if (TargetForMultidot != null)
                        {
                            utils.LogActivity("   MULTIDOT   " + NETHER_TEMPEST, TargetForMultidot.Name);
                            return utils.Cast(CORRUPTION, TargetForMultidot);
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
        
        private bool HasGlyph(string tal)
        {
            return talents.HasGlyph(tal);
        }
        
        private bool HasTalent(WarlockTalents tal)
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
        
            ArchimondesDarkness,
            KiljaedensCunning,
            MannorothsFury,
        
            SoulburnHaunt,
            Demonbolt = SoulburnHaunt,
            CharredRemains = SoulburnHaunt,
            Cataclysm,
            DemonicServitude
        }
        
        private static uint demonic_fury { get { return Me.GetCurrentPower(WoWPowerType.DemonicFury); } }
        
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

            WarlockPet bestPet = (WarlockPet)DemonologyWarlockSettings.Instance.PetToSummon;
            
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
        
        public void SetnextTimeUseNoGcd()
        {
            //in periond of start boost, cancel Metamorphosis before 15s
            nextTimeUseNoGcd = DateTime.Now + new TimeSpan(0, 0, 0, 0, 800);
        }
        
        public bool hand_of_guldan_in_flight
        {
            //in periond of start boost, cancel Metamorphosis before 15s
            get { return (startTime_hand_of_guldan + new TimeSpan(0, 0, 0, 0, 1500) > DateTime.Now);}
        }
        
        public int recharge_time()
        {
            TimeSpan time_recharge = new TimeSpan(0, 0, 0, 0, 1500)-(DateTime.Now - startTime_hand_of_guldan);
            return time_recharge.Seconds;
        }
        
        
        public int active_enemies_aoe(WoWUnit target) 
        { 
        	return utils.AllAttaccableEnemyMobsInRangeFromTarget(target, 10).Count();
        }
        
        public int active_enemies_aoe_dot() 
        { 
        	return utils.AllAttaccableEnemyMobsInRangeFromTarget(Me, 40).Count();
        }
        
        public int active_enemies_surround() 
        { 
        	return utils.AllAttaccableEnemyMobsInRangeFromTarget(Me, 10).Count();
        }
        
        public static class set_bonus 
        {
            public static bool tier17_2pc { get { return (StyxWoW.Me.HasAura("Item - Warlock T17 Demonology 2P Bonus") ? true : false); } }
            public static bool tier17_4pc { get { return (StyxWoW.Me.HasAura("Item - Warlock T17 Demonology 4P Bonus") ? true : false); } }
        }
        
    }
}
