using System.IO;
using Styx;
using Styx.Helpers;
using System.ComponentModel;
using DefaultValue = Styx.Helpers.DefaultValueAttribute;
using Styx.Common;
using Styx.Common.Helpers;

namespace KingWoW
{
    public class DiscPriestSettings : Settings
    {
        public static DiscPriestSettings Instance = new DiscPriestSettings();


        public string path_name = Path.Combine(Styx.Common.Utilities.AssemblyDirectory, string.Format(@"KingWOWCurrentSettings/DiscPriest/DiscPriest-Settings-{0}.xml", StyxWoW.Me.Name));

        public DiscPriestSettings()
            : base(Path.Combine(Styx.Common.Utilities.AssemblyDirectory, string.Format(@"KingWOWCurrentSettings/DiscPriest/DiscPriest-Settings-{0}.xml", StyxWoW.Me.Name)))
        {
        }

        [Setting]
        [DefaultValue(2)]
        [Category("设置多目标DOT")]
        [DisplayName("多目标痛 最少敌人数量")]
        [Description("Enable Multidot of ShadowWord: Pain rotation if Enemy in range withouut this dot >= this value")]
        public int Multidot_SW_Pain_EnemyNumberMin { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置目标")]
        [DisplayName("自动攻击目标")]
        [Description("Enable/Disable autofacing current target")]
        public bool AutofaceTarget { get; set; }

        [Setting]
        [DefaultValue(3)]
        [Category("设置多目标DOT")]
        [DisplayName("多目标痛 最大敌人数量")]
        [Description("Disable Multidot rotation if dotted Enemy with ShadowWors: Pain> this value")]
        public int Multidot_SW_Pain_EnemyNumberMax { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置多目标DOT")]
        [DisplayName("启用多目标输出")]
        [Description("Enable/Disable use of Multidot rotation")]
        public bool MultidotEnabled { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置多目标DOT")]
        [DisplayName("不使用DOT对被控目标")]
        [Description("Avoid Crowd Controlled in Multidot Rotation")]
        public bool MultidotAvoidCC { get; set; }


        [Setting]
        [DefaultValue(28)]
        [Category("设置风筝")]
        [DisplayName("风筝距离(码)")]
        [Description("Distance to pull from")]
        public float PullDistance { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置风筝")]
        [DisplayName("启用风筝")]
        [Description("fastPull: avoid prebuff")]
        public bool fastPull { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置自我治疗")]
        [DisplayName("启用优先自我治疗")]
        [Description("Enable/Disable Self Healing Priority: HP% is set below")]
        public bool SelfHealingPriorityEnabled { get; set; }

        [Setting]
        [DefaultValue(50)]
        [Category("设置自我治疗")]
        [DisplayName("优先自我治疗 生命值%")]
        [Description("if enabled Self Healing Priority: healing you before other in party until below this HP%")]
        public float SelfHealingPriorityHP { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置爆发技能")]
        [DisplayName("启用天使长")]
        [Description("Cast Archangel at 5 stacks of evangelism")]
        public bool UseArchangel { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置爆发技能")]
        [DisplayName("启用心灵专注")]
        [Description("Use Inner Focus every CD")]
        public bool UseInnerFocus { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置戒律治疗循环")]
        [DisplayName("启用戒律赎罪治疗")]
        [Description("if NOT AutoSwitchAtonementNormal setting enabled then Manual switch between Atonement and Normal healing ")]
        public bool UseDisciplineAtonementHealingRotation { get; set; }
        

        [Setting]
        [DefaultValue(Rotation.PVE)]
        [Category("设置戒律治疗循环")]
        [DisplayName("循环类型PVE/PVP/任务")]
        [Description("use PVE or PVP or QUESTING rotation tipe")]
        public Rotation RotationType { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置戒律治疗循环")]
        [DisplayName("自动切换赎罪治疗/正常治疗")]
        [Description("Automatic switch between Atonement and Normal healing (use Normal healing when there is nothing to attack)")]
        public bool AutoSwitchAtonementNormal { get; set; }

        public enum Rotation
        {
            PVE,
            PVP,
            QUESTING
        }

        public enum InnerType
        {
            FIRE,
            WILL,
            MANUAL
        }

        [Setting]
        [DefaultValue(InnerType.FIRE)]
        [Category("设置BUFF")]
        [DisplayName("使用心灵之火(FIRE)/心灵意志(WILL)")]
        [Description("chose inner to use")]
        public InnerType InnerToUse { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置BUFF")]
        [DisplayName("自动真言术:韧")]
        [Description("Cast Automaticaly PW:Fortitude on party")]
        public bool AutoPWFortitude { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置BUFF")]
        [DisplayName("启用愈合祷言")]
        [Description("Enable/Disable Preyer of Mending")]
        public bool Use_PoM { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置BUFF")]
        [DisplayName("自动心灵意志(移动中)")]
        [Description("chose to use inner will if moving")]
        public bool InnerWillOnMoving { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置BUFF")]
        [DisplayName("使用加速技能(移动中)")]
        [Description("chose to use AngelicFaether/PW:Shield if talented and if moving")]
        public bool BurstSpeedMoving { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置BUFF")]
        [DisplayName("使用渐隐术")]
        [Description("chose to use fade on aggro")]
        public bool UseFade { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置驱散魔法")]
        [DisplayName("使用纯净术 ")]
        [Description("Use Purify for cure disease and dispell magic")]
        public bool UsePurify { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置驱散魔法")]
        [DisplayName("使用群体驱散 ")]
        [Description("Use UseMassDispell for cure disease and dispell magic on many players")]
        public bool UseMassDispell { get; set; }

        [Setting]
        [DefaultValue(3)]
        [Category("设置驱散魔法")]
        [DisplayName("群体驱散目标数量")]
        [Description("Set number of dispellable players for Mass Dispell")]
        public int MassDispellCount { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置杂项")]
        [DisplayName("启用复活术")]
        [Description("enable use of Resurrection out of combat")]
        public bool UseResurrection { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置杂项")]
        [DisplayName("进入战斗自动下马")]
        [Description("Auto start combat rotation when in combat and mounted")]
        public bool AutoDismountOnCombat { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置杂项")]
        [DisplayName("自动启用显示治疗信息")]
        [Description("Show a chat Message when healing autoswitch from atonement to rormal and viceversa")]
        public bool ShowMessageSwitching { get; set; }

        [Setting]
        [DefaultValue(50)]
        [Category("设置杂项")]
        [DisplayName("治疗队友/团队NPC生命值%")]
        [Description("Set Threshold for heal targetted NPC over party member: CC will heal the targetted NPC if noone party/raid member in healing range below this value")]
        public int HealNPC_threshold { get; set; }
        

        [Setting]
        [DefaultValue(true)]
        [Category("设置杂项")]
        [DisplayName("脱战后自动治疗")]
        [Description("enable-disable use of OutOfCombat healing")]
        public bool OOCHealing { get; set; }
        

        [Setting]
        [DefaultValue(false)]
        [Category("设置BUFF")]
        [DisplayName("启用防恐惧结界")]
        [Description("enable use of Fear Ward on CD")]
        public bool UseFearWard { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置减伤技能")]
        [DisplayName("自动使用痛苦压制")]
        [Description("enable use of Pain Suppression")]
        public bool UsePainSuppression { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置对坦治疗")]
        [DisplayName("只对坦使用痛苦压制")]
        [Description("enable use of Pain Suppression ONLY on TANK or on you when in solo")]
        public bool UsePainSuppressionOnlyOnTank { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置对副坦治疗")]
        [DisplayName("对副坦使用痛苦压制")]
        [Description("enable use of Pain Suppression on OFF_TANK or on you when in solo")]
        public bool UsePainSuppressionOnOffTank { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置对副坦治疗")]
        [DisplayName("启用副坦治疗")]
        [Description("enable/disable OffTank Healing priority (same features of main tank except keep grace) but main tank will be always first")]
        public bool OffTankHealing { get; set; }
        

        [Setting]
        [DefaultValue(50)]
        [Category("设置减伤")]
        [DisplayName("痛苦压制生命值%")]
        [Description("Cast Pain Suppression at target % hp")]
        public int PainSuppressionPercent { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置减伤")]
        [DisplayName("当灵魂护壳开启时使用治疗祷言")]
        [Description("Use Spirit Shell everytime CC going to use Prayer Of Healing")]
        public bool UseSS_on_POM { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置对坦治疗")]
        [DisplayName("Priorize Tank Healing")]
        [Description("Priorize Tank healing over all except an SoS heal")]
        public bool PriorizeTankHealing { get; set; }
        
        
        [Setting]
        [DefaultValue(75)]
        [Category("设置法力值恢复")]
        [DisplayName("使用暗影魔/摧心魔  法力值%")]
        [Description("Cast ShadowFiend/Mindbender at % mana")]
        public int ShadowFiendPercent { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置法力值管理")]
        [DisplayName("尝试预存法力值")]
        [Description("Try save mana: interrupt no needed heal")]
        public bool TrySaveMana { get; set; }

        [Setting]
        [DefaultValue(100)]
        [Category("设置对坦治疗")]
        [DisplayName("真言术:盾 给坦 生命值%")]
        [Description("Cast PW: Shield on Tank at % hp set to 100 for always up shield on tank")]
        public int TankShieldPercent { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置对坦治疗")]
        [DisplayName("给坦使用恢复术")]
        [Description("Mantain Renew on main Tank")]
        public bool RenewOnMainTank { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置对副坦治疗")]
        [DisplayName("给副坦使用恢复术")]
        [Description("Mantain Renew on OffTank")]
        public bool RenewOnOffTank { get; set; }

        [Setting]
        [DefaultValue(55)]
        [Category("设置真言术:盾")]
        [DisplayName("使用真言术:盾 生命值%")]
        [Description("Cast PW: Shield on Player not Tank at % hp")]
        public int PWShieldPercent { get; set; }


        //NORMAL (NOT ATONEMENT) HEALING TUNING
        [Setting]
        [DefaultValue(80)]
        [Category("设置正常治疗(停用赎罪模式)")]
        [DisplayName("苦修 生命值%")]
        [Description("Cast Penance at target % hp")]
        public int NoAtonementPenancePercent { get; set; }

        [Setting]
        [DefaultValue(60)]
        [Category("设置正常治疗(停用赎罪模式)")]
        [DisplayName("联结治疗 生命值%")]
        [Description("Cast Binding Heal if target and me hp % lower than this value")]
        public int NoAtonementBindingHealPercent { get; set; }

        [Setting]
        [DefaultValue(90)]
        [Category("设置正常治疗(停用赎罪模式)")]
        [DisplayName("治疗术 生命值%")]
        [Description("Cast Heal at target % hp")]
        public int NoAtonementHealPercent { get; set; }

        [Setting]
        [DefaultValue(50)]
        [Category("设置正常治疗(停用赎罪模式)")]
        [DisplayName("强效治疗术 生命值%")]
        [Description("Cast Greater Heal at target % hp")]
        public int NoAtonementGHPercent { get; set; }

        [Setting]
        [DefaultValue(30)]
        [Category("设置正常治疗(停用赎罪模式)")]
        [DisplayName("快速治疗 生命值%")]
        [Description("Cast Flash Heal at target % hp")]
        public int NoAtonementFlashHealPercent { get; set; }

        [Setting]
        [DefaultValue(80)]
        [Category("设置正常治疗(停用赎罪模式)")]
        [DisplayName("光明涌动 生命值%")]
        [Description("Cast Surge of Ligth if player hp < %. SoL will cast on lower hp player same if buff ending")]
        public int NoAtonementSoLPercent { get; set; }

        [Setting]
        [DefaultValue(40)]
        [Category("设置正常治疗(停用赎罪模式)")]
        [DisplayName("绝望祷言 生命值%")]
        [Description("Cast Desperate Prayer if we lower than % hp")]
        public int NoAtonementDesperatePrayerPercent { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置正常治疗(停用赎罪模式)")]
        [DisplayName("使用恢复(移动中)")]
        [Description("Cast Renew at RenewPercent if moving")]
        public bool NoAtonementUseRenewOnMoving { get; set; }

        [Setting]
        [DefaultValue(80)]
        [Category("设置正常治疗(停用赎罪模式)")]
        [DisplayName("使用恢复 生命值%")]
        [Description("Cast Renew at hp%")]
        public int NoAtonementRenewPercent { get; set; }

        /*[Setting]
        [DefaultValue(true)]
        [Category("设置真言术:盾")]
        [DisplayName("精神护壳自动每队治疗祷言")]
        [Description("AutoTrigger Spirit Shell: when buff detected autospam prayer of healing")]
        public bool AutoTriggerSS { get; set; }*/

        //ATONEMENT HEALING TUNING
        [Setting]
        [DefaultValue(50)]
        [Category("设置赎罪治疗")]
        [DisplayName("苦修 生命值%")]
        [Description("Cast Penance at target % hp")]
        public int AtonementPenancePercent { get; set; }

        [Setting]
        [DefaultValue(60)]
        [Category("设置赎罪治疗")]
        [DisplayName("联结治疗 生命值%")]
        [Description("Cast Binding Heal if target and me hp % lower than this value")]
        public int AtonementBindingHealPercent { get; set; }

        [Setting]
        [DefaultValue(0)]
        [Category("设置赎罪治疗")]
        [DisplayName("治疗术 生命值%")]
        [Description("Cast Heal at target % hp")]
        public int AtonementHealPercent { get; set; }

        [Setting]
        [DefaultValue(0)]
        [Category("设置赎罪治疗")]
        [DisplayName("强效治疗术 生命值%")]
        [Description("Cast Greater Heal at target % hp")]
        public int AtonementGHPercent { get; set; }

        [Setting]
        [DefaultValue(30)]
        [Category("设置赎罪治疗")]
        [DisplayName("快速治疗 生命值%")]
        [Description("Cast Flash Heal at target % hp")]
        public int AtonementFlashHealPercent { get; set; }

        [Setting]
        [DefaultValue(80)]
        [Category("设置赎罪治疗")]
        [DisplayName("光明涌动 %生命值")]
        [Description("Cast Surge of Ligth if player hp < %. SoL will cast on lower hp player same if buff ending")]
        public int AtonementSoLPercent { get; set; }

        [Setting]
        [DefaultValue(40)]
        [Category("设置赎罪治疗")]
        [DisplayName("绝望祷言 生命值%")]
        [Description("Cast Desperate Prayer if we lower than % hp")]
        public int AtonementDesperatePrayerPercent { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置赎罪治疗")]
        [DisplayName("使用恢复术(移动中)")]
        [Description("Cast Renew at RenewPercent if moving")]
        public bool AtonementUseRenewOnMoving { get; set; }

        [Setting]
        [DefaultValue(75)]
        [Category("设置赎罪治疗")]
        [DisplayName("恢复术 生命值%")]
        [Description("Cast Renew at hp%")]
        public int AtonementRenewPercent { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置减伤")]
        [DisplayName("自动使用真言术:障")]
        [Description("enable automatic use of PW: Barrier")]
        public bool AutoUsePWBarrier { get; set; }

        [Setting]
        [DefaultValue(60)]
        [Category("设置减伤")]
        [DisplayName("真言术:障 生命值%")]
        [Description("Cast PW: Barrier if Player below %h >= #PWBarrierNumber")]
        public int PWBarrierPercent { get; set; }

        [Setting]
        [DefaultValue(6)]
        [Category("设置减伤")]
        [DisplayName("真言术:障 人员数量")]
        [Description("Cast PW: Barrier if Player below PWBarrierPercent >= this number")]
        public int PWBarrierNumber { get; set; }

        [Setting]
        [DefaultValue(70)]
        [Category("设置BUFF")]
        [DisplayName("能量灌注 生命值%")]
        [Description("Cast Power Infusion to empower healing if number of player lower than %hp >= PowerInfusionNumber")]
        public int PowerInfusionPercent { get; set; }

        [Setting]
        [DefaultValue(3)]
        [Category("设置BUFF")]
        [DisplayName("能量灌注 人员数量")]
        [Description("Cast Power Infusion to empower healing if number of player lower than PowerInfusionPercent hp% >= this value")]
        public int PowerInfusionNumber { get; set; }

        [Setting]
        [DefaultValue(60)]
        [Category("设置BUFF")]
        [DisplayName("能量灌注 法力值%")]
        [Description("Cast Power Infusion to lower mana cost spell if our mana is lower than this value")]
        public int PowerInfusionManaPercent { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置对坦治疗")]
        [DisplayName("保持坦的恩赐")]
        [Description("Try to keep and mantain 3 stacks of grace on Tank")]
        public bool keepGraceOnTank { get; set; }

        [Setting]
        [DefaultValue(4500)]
        [Category("设置对坦治疗")]
        [DisplayName("刷新坦的恩赐时间")]
        [Description("Try to keep and mantain 3 stacks of grace on Tank: if grace stacks >=2 and Time grace to expire <= this value (milliseconds) CC will cast a heal just to refresh buff")]
        public int keepGraceOnTankTime { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置施法中断")]
        [DisplayName("启用施法中断(非精神护壳状态")]
        [Description("Interrupt any other spell cast (except SpiritShell) if anyone lower than SoS heal % hp")]
        public bool SoSEnabled { get; set; }

        [Setting]
        [DefaultValue(20)]
        [Category("设置施法中断")]
        [DisplayName("SoS治疗当队友生命值低于%")]
        [Description("Cast SoS heal if anyone in party lower than % hp")]
        public int SoSPercent { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置群体治疗")]
        [DisplayName("禁用群体治疗")]
        [Description("Enable/disable AOE healing")]
        public bool DisableAOE_HealingRotation { get; set; }

        [Setting]
        [DefaultValue(70)]
        [Category("设置群体治疗")]
        [DisplayName("队友 生命值%")]
        [Description("Cast Prayer Of Healing if player lower than % hp >= PrayerOfHealingNumber")]
        public int PrayerOfHealingPercent { get; set; }

        [Setting]
        [DefaultValue(2)]
        [Category("设置群体治疗")]
        [DisplayName("群体治疗 人员数量")]
        [Description("Cast Prayer Of Healing if player lower than PrayerOfHealingPercent >= this value")]
        public int PrayerOfHealingNumber { get; set; }

        [Setting]
        [DefaultValue(85)]
        [Category("设置群体治疗")]
        [DisplayName("爆流/光晕/神圣新星 生命值%")]
        [Description("Cast Cascade/Halo/DivineStar if player lower than % hp >= CascadeHaloDivineStarNumber")]      
        public int CascadeHaloDivinestarPercent { get; set; }

        [Setting]
        [DefaultValue(2)]
        [Category("设置群体治疗")]
        [DisplayName("爆流/光晕/神圣新星 人员数量")]
        [Description("Cast Cascade/halo/DivineStar if player lower than CascadeHaloDivinestarPercent >= this value")]
        public int CascadeHaloDivineStarNumber { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置群体治疗")]
        [DisplayName("使用爆流/光晕/神圣新星 攻击")]
        [Description("Cast Divine Star in offensive mode if you are facing CascadeHaloDivineStarNumber enemy")]
        public bool DivineStarOffensive { get; set; }

        [Setting]
        [DefaultValue(25)]
        [Category("设置法力值恢复")]
        [DisplayName("希望圣歌 法力值%")]
        [Description("Cast Hymn Of Hope at % mana")]
        public int HymnOfHopePercent { get; set; }

        [Setting]
        [DefaultValue(20)]
        [Category("设置食物")]
        [DisplayName("使用饮料 法力值%")]
        [Description("Drink at % mana")]
        public int ManaPercent { get; set; }

        [Setting]
        [DefaultValue(0)]
        [Category("设置食物")]
        [DisplayName("使用食物 生命值%")]
        [Description("Eat at % hp")]
        public int HealthPercent { get; set; }

        /*[Setting]
        [DefaultValue(true)]
        [Category("设置赎罪")]
        [DisplayName("启用赎罪")]
        [Description("Chose to heal dpsing or not")]
        public bool UseAtonement { get; set; }*/

        [Setting]
        [DefaultValue(100)]
        [Category("设置赎罪")]
        [DisplayName("赎罪 生命值%")]
        [Description("Start Atonement healing only if someone on group has hp% < this value")]
        public int AtonementHp { get; set; }

        [Setting]
        [DefaultValue(10)]
        [Category("设置赎罪")]
        [DisplayName("使用赎罪最低法力值")]
        [Description("Cast Smite,Penance and Holy Fire to heal if mana bigger than this value. If you dont want atonement set this value to 100")]
        public int AtonementManaThreshold { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置赎罪")]
        [DisplayName("使用苦修输出")]
        [Description("use penance in dps rotation end in atonement rotation")]
        public bool UsePenanceInDPS { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置虚空转移")]
        [DisplayName("启用虚空转移")]
        [Description("enable/Disable Void Shift ")]
        public bool UseVoidShift { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置对坦治疗")]
        [DisplayName("对主坦使用虚空转移")]
        [Description("enable/Disable cast Void Shift only on tank")]
        public bool UseVoidShiftOnlyOnTank { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置对副坦治疗")]
        [DisplayName("对副坦使用虚空转移")]
        [Description("enable/Disable cast Void Shift on offtank too")]
        public bool UseVoidShiftOnOffTank { get; set; }

        [Setting]
        [DefaultValue(100)]
        [Category("设置对副坦治疗")]
        [DisplayName("对副坦使用真言术:盾 生命值%")]
        [Description("Cast PW: Shield on OffTank at % hp set to 100 for always up shield on tank")]
        public int OffTankShieldPercent { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置战斗控制")]
        [DisplayName("启用心灵尖啸")]
        [Description("Use Psychic scream if enemy in range")]
        public bool UsePsychicScream { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置战斗控制")]
        [DisplayName("不攻击被控目标")]
        [Description("Do not Attack CrowdControlled enemy NB: may slow performance!")]
        public bool AvoidCC { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置战斗控制")]
        [DisplayName("启用虚空触须")]
        [Description("Use Void Tendrils if enemy in range")]
        public bool UseVoidTendrils { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置战斗控制")]
        [DisplayName("启用灵能魔")]
        [Description("Use Psyfiend if enemy in range")]
        public bool UsePsyfiend { get; set; }

        [Setting]
        [DefaultValue(20)]
        [Category("设置虚空转移")]
        [DisplayName("虚空转移的目标 生命值%")]
        [Description("cast Void Shift on target below of this HP%")]
        public int VoidShiftTarget { get; set; }

        [Setting]
        [DefaultValue(50)]
        [Category("设置虚空转移")]
        [DisplayName("虚空转移自己低于 生命值%")]
        [Description("cast Void Shift on target only if my HP greather than this value")]
        public int VoidShiftMe { get; set; }
        
       
        [Setting]
        [DefaultValue(true)]
        [Category("设置PVP")]
        [DisplayName("启用驱散魔法")]
        [Description("Use UseDispellMagic on current target: NB no logic implemented, so CC will dispell all dispellable aura")]
        public bool UseDispellMagic { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置PVP")]
        [DisplayName("启用暗言术:灭")]
        [Description("Use SW: Death in PVP rotation")]
        public bool UseSWD { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置PVP")]
        [DisplayName("启用暗言术:痛")]
        [Description("Use SW: Pain in PVP rotation")]
        public bool UseSWP { get; set; }

        [Setting, DefaultValue(KingwowKeys.X)]
        [Category("设置热键")]
        [DisplayName("暂停")]
        [Description("enable/disable Routine")]
        public KingwowKeys PauseKey { get; set; }

        [Setting, DefaultValue(ModifierKeys.Alt)]
        [Category("设置热键")]
        [DisplayName("ModKey")]
        [Description("Mod key for HotKey")]
        public ModifierKeys ModKey { get; set; }

        [Setting, DefaultValue(KingwowKeys.M)]
        [Category("设置热键")]
        [DisplayName("启用/禁用戒律赎罪模式")]
        [Description("enable/disable AtonementHealingRotation")]
        public KingwowKeys UseDisciplineAtonementHealingRotationKey { get; set; }

    }
}