using System.IO;
using Styx;
using Styx.Helpers;
using System.ComponentModel;
using DefaultValue = Styx.Helpers.DefaultValueAttribute;
using Styx.Common;
using Styx.Common.Helpers;

namespace KingWoW
{
    public class ShadowPriestSettings : Settings
    {
        public static ShadowPriestSettings Instance = new ShadowPriestSettings();


        public string path_name = Path.Combine(Styx.Common.Utilities.AssemblyDirectory, string.Format(@"KingWOWCurrentSettings/ShadowPriest/ShadowPriest-Settings-{0}.xml", StyxWoW.Me.Name));

        public ShadowPriestSettings()
            : base(Path.Combine(Styx.Common.Utilities.AssemblyDirectory, string.Format(@"KingWOWCurrentSettings/ShadowPriest/ShadowPriest-Settings-{0}.xml", StyxWoW.Me.Name)))
        {
        }

        [Setting]
        [DefaultValue(28)]
        [Category("设置风筝")]
        [DisplayName("风筝距离(码)")]
        [Description("Distance to pull from")]
        public float PullDistance { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置自我治疗")]
        [DisplayName("启用自我治疗")]
        [Description("Enable/Disable Self Healing Priority: HP% is set below")]
        public bool SelfHealingPriorityEnabled { get; set; }

        [Setting]
        [DefaultValue(40)]
        [Category("设置自我治疗")]
        [DisplayName("自我治疗优先的血量%")]
        [Description("if enabled Self Healing Priority: healing you until below this HP%")]
        public float SelfHealingPriorityHP { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置爆发技能")]
        [DisplayName("对焦点使用")]
        [Description("Use Inner Focus every CD")]
        public bool UseInnerFocus { get; set; }

        public enum InnerType
        {
            FIRE,
            WILL
        }

        [Setting]
        [DefaultValue(InnerType.FIRE)]
        [Category("设置BUFF")]
        [DisplayName("使用心灵之火或心灵意志")]
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
        [DisplayName("移动中切换心灵意志")]
        [Description("chose to use inner will if moving")]
        public bool InnerWillOnMoving { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置BUFF")]
        [DisplayName("移动时使用加速技能")]
        [Description("chose to use AngelicFaether/PW:Shield if talented and if moving")]
        public bool BurstSpeedMoving { get; set; }

        [Setting]
        [DefaultValue(5)]
        [Category("设置群体治疗")]
        [DisplayName("使用光晕最少人数")]
        [Description("Use Cascade/Halo if players below Cascade_Halo_HP >= this value")]
        public int Cascade_Halo_Number { get; set; }

        [Setting]
        [DefaultValue(65)]
        [Category("设置群体治疗")]
        [DisplayName("使用光晕的血量值")]
        [Description("Use Cascade/Halo if players with HPbelow this value >= Cascade_Halo_Number")]
        public int Cascade_Halo_HP { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置BUFF")]
        [DisplayName("使用渐隐术")]
        [Description("chose to use fade on aggro")]
        public bool UseFade { get; set; }
        

        [Setting]
        [DefaultValue(false)]
        [Category("设置驱散及被控")]
        [DisplayName("驱散所有被控效果 ")]
        [Description("try to dispell asap avoiding normal rotation priority")]
        public bool DispellASAP { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置驱散及被控")]
        [DisplayName("Dispell Only Majror")]
        [Description("dispell only player affecterd by major game note debuff: If you set this to false CC will dispell EVERYTHING")]
        public bool DispellOnlyMajor { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置驱散及被控")]
        [DisplayName("使用净化术 ")]
        [Description("Use Purify for cure disease and dispell magic")]
        public bool UsePurify { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置驱散及被控")]
        [DisplayName("使用群体驱散 ")]
        [Description("Use UseMassDispell for cure disease and dispell magic on many players")]
        public bool UseMassDispell { get; set; }

        [Setting]
        [DefaultValue(3)]
        [Category("设置驱散及被控")]
        [DisplayName("群体驱散最少人数")]
        [Description("Set number of dispellable players for Mass Dispell")]
        public int MassDispellCount { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置杂项")]
        [DisplayName("使用复活术")]
        [Description("enable use of Resurrection out of combat")]
        public bool UseResurrection { get; set; }
        
        [Setting]
        [DefaultValue(true)]
        [Category("设置杂项")]
        [DisplayName("使用脱离战斗恢复血量")]
        [Description("enable-disable use of OutOfCombat healing")]
        public bool OOCHealing { get; set; }
        

        [Setting]
        [DefaultValue(false)]
        [Category("设置BUFF")]
        [DisplayName("使用防护恐惧结界")]
        [Description("enable use of Fear Ward on CD")]
        public bool UseFearWard { get; set; }      

        [Setting]
        [DefaultValue(true)]
        [Category("设置坦")]
        [DisplayName("协助治疗主坦")]
        [Description("enable/disable Tank help Healing")]
        public bool TankHealing { get; set; }

        [Setting]
        [DefaultValue(15)]
        [Category("设置消散")]
        [DisplayName("设置消散使用血量百份比")]
        [Description("Use Dispersion at hp%")]
        public int DispersionHP { get; set; }

        [Setting]
        [DefaultValue(10)]
        [Category("设置消散")]
        [DisplayName("设置消散使用魔法量百份比")]
        [Description("Use Dispersion at mana%")]
        public int DispersionMana { get; set; }

        [Setting]
        [DefaultValue(65)]
        [Category("设置吸血鬼拥抱")]
        [DisplayName("吸血鬼拥抱使用血量%")]
        [Description("Use Vampiric Embrance if my HP or VampiricEmbranceNumber players below this value")]
        public int VampiricEmbranceHP { get; set; }

        [Setting]
        [DefaultValue(5)]
        [Category("设置吸血鬼拥抱")]
        [DisplayName("吸血鬼拥抱使用最少人数")]
        [Description("Use Vampiric Embrance players with HP <= VampiricEmbranceHP equal or greater this value")]
        public int VampiricEmbranceNumber { get; set; }
        
        [Setting]
        [DefaultValue(90)]
        [Category("设置魔法值恢复")]
        [DisplayName("暗影魔/摧心魔使用魔法值%")]
        [Description("Cast ShadowFiend/Mindbender at % mana")]
        public int ShadowFiendPercent { get; set; }

        [Setting]
        [DefaultValue(0)]
        [Category("设置坦")]
        [DisplayName("对坦使用盾 血量%")]
        [Description("Cast PW: Shield on Tank at % hp set to 100 for always up shield on tank")]
        public int TankShieldPercent { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置坦")]
        [DisplayName("对主坦使用恢复术")]
        [Description("Mantain Renew on main Tank")]
        public bool RenewOnTank { get; set; }

        [Setting]
        [DefaultValue(50)]
        [Category("设置真言术:盾")]
        [DisplayName("真言术:盾 血量%")]
        [Description("Cast PW: Shield on Player at % hp")]
        public int PWShieldPercent { get; set; }

        //HEALING TUNING
        [Setting]
        [DefaultValue(50)]
        [Category("设置治疗回复")]
        [DisplayName("强效治疗血量%")]
        [Description("Cast Greater Heal at target % hp")]
        public int GHPercent { get; set; }

        [Setting]
        [DefaultValue(30)]
        [Category("设置治疗回复")]
        [DisplayName("快速治疗血量%")]
        [Description("Cast Flash Heal at target % hp")]
        public int FlashHealPercent { get; set; }

        [Setting]
        [DefaultValue(40)]
        [Category("设置治疗回复")]
        [DisplayName("使用绝望祷言血量%")]
        [Description("Cast Desperate Prayer if we lower than % hp")]
        public int DesperatePrayerPercent { get; set; }

        [Setting]
        [DefaultValue(75)]
        [Category("设置治疗回复")]
        [DisplayName("使用恢复血量%")]
        [Description("Cast Renew at hp%")]
        public int RenewPercent { get; set; }

        public enum PowerInfusionUseType
        {
            COOLDOWN,
            BOSS,
            MANUAL
        }

        [Setting]
        [DefaultValue(PowerInfusionUseType.BOSS)]
        [Category("设置爆发技能")]
        [DisplayName("使用能量灌注")]
        [Description("Cast Power Infusion on BOSS on COOLDOWN or MANUAL type")]
        public PowerInfusionUseType PowerInfusionUse { get; set; }

        [Setting]
        [DefaultValue(2)]
        [Category("设置AOE")]
        [DisplayName("使用 爆流/神圣之星/光晕 敌人数量")]
        [Description("Cast Cascade/halo/DivineStar if player lower than CascadeHaloDivinestarPercent >= this value")]
        public int CascadeHaloDivineStarNumber { get; set; }

        [Setting]
        [DefaultValue(20)]
        [Category("设置食物")]
        [DisplayName("魔法值低于%")]
        [Description("Drink at % mana")]
        public int ManaPercent { get; set; }

        [Setting]
        [DefaultValue(0)]
        [Category("设置食物")]
        [DisplayName("血量低于%")]
        [Description("Eat at % hp")]
        public int HealthPercent { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置虚空转移")]
        [DisplayName("启用虚空转移")]
        [Description("enable/Disable Void Shift ")]
        public bool UseVoidShift { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置坦")]
        [DisplayName("对主坦使用虚空转移")]
        [Description("enable/Disable cast Void Shift only on tank")]
        public bool UseVoidShiftOnTank { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置坦")]
        [DisplayName("对副坦使用虚空转移")]
        [Description("enable/Disable cast Void Shift on offtank too")]
        public bool UseVoidShiftOnOffTank { get; set; }

        [Setting]
        [DefaultValue(0)]
        [Category("设置坦")]
        [DisplayName("对副坦使用盾 血量%")]
        [Description("Cast PW: Shield on OffTank at % hp set to 100 for always up shield on tank")]
        public int OffTankShieldPercent { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置战斗例程")]
        [DisplayName("使用心灵尖啸")]
        [Description("Use Psychic scream if enemy in range")]
        public bool UsePsychicScream { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置战斗例程")]
        [DisplayName("不攻击被控制的敌人")]
        [Description("Do not Attack CrowdControlled enemy NB: may slow performance!")]
        public bool AvoidCC { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置战斗例程")]
        [DisplayName("使用虚空触须")]
        [Description("Use Void Tendrils if enemy in range")]
        public bool UseVoidTendrils { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置战斗例程")]
        [DisplayName("使用灵能魔")]
        [Description("Use Psyfiend if enemy in range")]
        public bool UsePsyfiend { get; set; }

        [Setting]
        [DefaultValue(20)]
        [Category("设置虚空转移")]
        [DisplayName("虚空转移目标血量%")]
        [Description("cast Void Shift on target below of this HP%")]
        public int VoidShiftTarget { get; set; }

        [Setting]
        [DefaultValue(50)]
        [Category("设置虚空转移")]
        [DisplayName("虚空转移自已血量%")]
        [Description("cast Void Shift on target only if my HP greather than this value")]
        public int VoidShiftMe { get; set; }
        
       
        [Setting]
        [DefaultValue(true)]
        [Category("设置PVP")]
        [DisplayName("驱散目标BUFF")]
        [Description("Use UseDispellMagic on current target: NB no logic implemented, so CC will dispell all dispellable aura")]
        public bool UseDispellMagic { get; set; }

        public enum TargetType
        {
            MANUAL,
            SEMIAUTO,
            AUTO
        }

        [Setting]
        [DefaultValue(true)]
        [Category("设置目标")]
        [DisplayName("自动攻击目标")]
        [Description("Enable/Disable autofacing current target")]
        public bool AutofaceTarget { get; set; }

        [Setting]
        [DefaultValue(TargetType.AUTO)]
        [Category("设置目标")]
        [DisplayName("选择目标类型")]
        [Description("AUTO/MANUAL enable/disable autotargeting SEMIAUTO: same logic of AUTO but no switch selected target")]
        public TargetType TargetTypeSelected { get; set; }

        [Setting]
        [DefaultValue(2)]
        [Category("设置多目标DOT")]
        [DisplayName("痛的多目标最少数量")]
        [Description("Enable Multidot of ShadowWord: Pain rotation if Enemy in range withouut this dot >= this value")]
        public int Multidot_SW_Pain_EnemyNumberMin { get; set; }

        [Setting]
        [DefaultValue(3)]
        [Category("设置多目标DOT")]
        [DisplayName("痛的多目标最大数量")]
        [Description("Disable Multidot rotation if dotted Enemy with ShadowWors: Pain> this value")]
        public int Multidot_SW_Pain_EnemyNumberMax { get; set; }

        [Setting]
        [DefaultValue(2)]
        [Category("设置多目标DOT")]
        [DisplayName("吸血鬼之触的多目标最小数量")]
        [Description("Enable Multidot of VampiricTouch rotation if Enemy in range withouut this dot >= this value")]
        public int Multidot_VampiricTouch_EnemyNumberMin { get; set; }

        [Setting]
        [DefaultValue(3)]
        [Category("设置多目标DOT")]
        [DisplayName("吸血鬼之触的多目标最大数量")]
        [Description("Disable Multidot rotation if dotted Enemy with VampiricTouch > this value")]
        public int Multidot_VampiricTouch_EnemyNumberMax { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置多目标DOT")]
        [DisplayName("启用多目标DOT")]
        [Description("Enable/Disable use of Multidot rotation")]
        public bool MultidotEnabled { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置多目标DOT")]
        [DisplayName("避免被控目标使用DOT")]
        [Description("Avoid Crowd Controlled in Multidot Rotation")]
        public bool MultidotAvoidCC { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置打断施法")]
        [DisplayName("自动打断施法")]
        [Description("Cast counterspell if target casting and interrumpible")]
        public bool AutoInterrupt { get; set; }

        [Setting]
        [DefaultValue(5)]
        [Category("设置AOE")]
        [DisplayName("使用精神灼烧最少敌人数量")]
        [Description("Use Mind Sear if enemies around Tank or OffTank > this value")]
        public int MindSearEnemyNumberMin { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置AOE")]
        [DisplayName("使用精神灼烧")]
        [Description("Enable/Disable use of MindSear")]
        public bool UseMindSear { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置AOE")]
        [DisplayName("总是对敌人使用精神灼烧")]
        [Description("use of MindSear olso on current enemy target if condition verified")]
        public bool MindSearAlsoOnEnemies { get; set; }

        [Setting]
        [DefaultValue(1000)]
        [Category("设置杂项")]
        [DisplayName("精神鞭笞时间(豪秒)")]
        [Description("Set mindFlayDuration - 250 ms")]
        public int mindFlayDuration { get; set; }
        
        [Setting]
        [DefaultValue(2000)]
        [Category("设置杂项")]
        [DisplayName("精神灼烧时间")]
        [Description("Set mindSearDuration - 250 ms")]
        public int mindSearDuration { get; set; }    
        

    }
}