using System.IO;
using Styx;
using Styx.Helpers;
using System.ComponentModel;
using DefaultValue = Styx.Helpers.DefaultValueAttribute;
using Styx.Common;
using Styx.Common.Helpers;

namespace KingWoW
{
    public class HolyPriestSettings : Settings
    {
        public static HolyPriestSettings Instance = new HolyPriestSettings();

        public string path_name = Path.Combine(Styx.Common.Utilities.AssemblyDirectory, string.Format(@"KingWOWCurrentSettings/HolyPriest/HolyPriest-Settings-{0}.xml", StyxWoW.Me.Name));

        public HolyPriestSettings()
            : base(Path.Combine(Styx.Common.Utilities.AssemblyDirectory, string.Format(@"KingWOWCurrentSettings/HolyPriest/HolyPriest-Settings-{0}.xml", StyxWoW.Me.Name)))
        {
        }

        [Setting]
        [DefaultValue(28)]
        [Category("设置风筝")]
        [DisplayName("风筝距离")]
        [Description("Distance to pull from")]
        public float PullDistance { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置目标")]
        [DisplayName("自动攻击目标")]
        [Description("Enable/Disable autofacing current target")]
        public bool AutofaceTarget { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置自我治疗")]
        [DisplayName("启用优先自我治疗")]
        [Description("Enable/Disable Self Healing Priority: 生命值% is set below")]
        public bool SelfHealingPriorityEnabled { get; set; }

        [Setting]
        [DefaultValue(40)]
        [Category("设置自我治疗")]
        [DisplayName("优先自我治疗 生命值%")]
        [Description("if enabled Self Healing Priority: healing you before other in party until below this 生命值%")]
        public float SelfHealingPriorityHP { get; set; }

        [Setting]
        [DefaultValue(0)]
        [Category("设置治疗循环")]
        [DisplayName("循环类型0正常模式/1优先坦和单目标/2优先群体/3PVP模式")]
        [Description("Use 0 for NORMAL ROTATION, Use 1 For First Tank and SINGLE TARGET ROTATION, Use 2 for AOE PRIORITY ROTATION, use 3 for PVP ROTATION")]
        public int RotationType { get; set; }

        public enum InnerType
        {
            FIRE,
            WILL
        }

        [Setting]
        [DefaultValue(InnerType.FIRE)]
        [Category("设置BUFF")]
        [DisplayName("启用心灵之火(FIRE)/心灵意志(WILL)")]
        [Description("chose inner to use")]
        public InnerType InnerToUse { get; set; }

        public enum ChakraType
        {
            CHASTISE,
            SANCTUARY,
            SERENITY
        }

        [Setting]
        [DefaultValue(ChakraType.SANCTUARY)]
        [Category("设置BUFF")]
        [DisplayName("启用脉轮:CHASTISE(罚)/SANCTUARY(佑)/SERENITY(静)")]
        [Description("chose Chakra to use")]
        public ChakraType ChakraToUse { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置BUFF")]
        [DisplayName("自动真言术:韧")]
        [Description("在队伍中自动使用真言术:韧 ")]
        public bool AutoPWFortitude { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置BUFF")]
        [DisplayName("自动切换心灵意志(移动中)")]
        [Description("chose to use inner will if moving")]
        public bool InnerWillOnMoving { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置BUFF")]
        [DisplayName("自动使用加速技能(移动中)")]
        [Description("chose to use Angelic Faether if talented and if moving")]
        public bool AngelicFeatherOnMoving { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置BUFF")]
        [DisplayName("自动光明之泉(无雕纹)")]
        [Description("AutoCastLightwell in your position in combat")]
        public bool AutoCastLightwell { get; set; }
        

        [Setting]
        [DefaultValue(true)]
        [Category("设置驱散")]
        [DisplayName("启用纯净术 ")]
        [Description("Use Purify for cure disease and dispell magic")]
        public bool UsePurify { get; set; }

        [Setting]
        [DefaultValue(3)]
        [Category("设置驱散")]
        [DisplayName("群体驱散人数")]
        [Description("Set number of dispellable players for Mass Dispell")]
        public int MassDispellCount { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置杂项")]
        [DisplayName("战斗自动下马")]
        [Description("Auto start combat rotation when in combat and mounted")]
        public bool AutoDismountOnCombat { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置杂项")]
        [DisplayName("启用复活术")]
        [Description("enable use of Resurrection out of combat")]
        public bool UseResurrection { get; set; }

        [Setting]
        [DefaultValue(50)]
        [Category("设置杂项")]
        [DisplayName("治疗NPC目标 生命值%")]
        [Description("Set Threshold for heal targetted NPC over party member: CC will heal the targetted NPC if noone party/raid member in healing range below this value")]
        public int HealNPC_threshold { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置杂项")]
        [DisplayName("启用脱战治疗")]
        [Description("enable-disable use of OutOfCombat healing")]
        public bool OOCHealing { get; set; }

        [Setting]
        [DefaultValue(15)]
        [Category("设置杂项")]
        [DisplayName("守护之魂 生命值%")]
        [Description("当目标生命值低于% 使用守护之魂")]
        public int GuardianSpiritPercent { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置BUFF")]
        [DisplayName("启用防恐惧结界")]
        [Description("enable use of Fear Ward on CD")]
        public bool UseFearWard { get; set; }

        [Setting]
        [DefaultValue(85)]
        [Category("设置治疗")]
        [DisplayName("治疗术 生命值%")]
        [Description("Cast Heal at target % hp")]
        public int HealPercent { get; set; }

        [Setting]
        [DefaultValue(65)]
        [Category("设置治疗")]
        [DisplayName("联结治疗 生命值%")]
        [Description("Cast Binding Heal if target and me hp % lower than this value")]
        public int BindingHealPercent { get; set; }

        [Setting]
        [DefaultValue(75)]
        [Category("设置法力值管理")]
        [DisplayName("暗影魔/摧心魔 法力值%")]
        [Description("Cast ShadowFiend/Mindbender at % 法力值")]
        public int ShadowFiendPercent { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置法力值管理")]
        [DisplayName("启用真言术:慰")]
        [Description("Enable use of PW: Solace if enabled to gain 法力值 and heal")]
        public bool UsePW_Solace { get; set; }
        

        [Setting]
        [DefaultValue(true)]
        [Category("设置法力值管理")]
        [DisplayName("尝试保存法力")]
        [Description("Try save mana: interrupt no needed heal")]
        public bool TrySaveMana { get; set; }

        [Setting]
        [DefaultValue(50)]
        [Category("设置治疗")]
        [DisplayName("强效治疗术 生命值%")]
        [Description("Cast 强效治疗术 at target % hp")]
        public int GHPercent { get; set; }

        [Setting]
        [DefaultValue(90)]
        [Category("设置治疗")]
        [DisplayName("圣言术:静 生命值%")]
        [Description("Cast 圣言术: Serenity at target % hp")]
        public int HWSerenityPercent { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置群体治疗")]
        [DisplayName("启用群体治疗模式")]
        [Description("Enable/disable AOE healing")]
        public bool DisableAOE_HealingRotation { get; set; }

        [Setting]
        [DefaultValue(90)]
        [Category("设置群体治疗")]
        [DisplayName("圣言术: 佑 生命值%")]
        [Description("Cast 圣言术: 佑 if Player below %h >= HW佑Number")]
        public int HWSanctuaryPercent { get; set; }

        [Setting]
        [DefaultValue(2)]
        [Category("设置群体治疗")]
        [DisplayName("圣言术: 佑 人数")]
        [Description("Cast 圣言术: 佑 if Player below HW佑Percent >= this number")]
        public int HWSanctuaryNumber { get; set; }

        [Setting]
        [DefaultValue(80)]
        [Category("设置群体治疗")]
        [DisplayName("愈合祷言(神圣洞察激活) 生命值%")]
        [Description("Cast PreyerOfMending on proc of Divine Insight if Player below %h >= PrayerOfMendingNumber")]
        public int PrayerOfMendingPercent { get; set; }

        [Setting]
        [DefaultValue(2)]
        [Category("设置群体治疗")]
        [DisplayName("愈合祷言(神圣洞察激活) 人数")]
        [Description("Cast PreyerOfMending if Player below PrayerOfMendingPercent >= this number")]
        public int PrayerOfMendingNumber { get; set; }

        [Setting]
        [DefaultValue(90)]
        [Category("设置群体治疗")]
        [DisplayName("治疗之环 生命值%")]
        [Description("Cast CircleOfHealing if Player below 生命值% >= CircleOfHealingNumber")]
        public int CircleOfHealingPercent { get; set; }

        [Setting]
        [DefaultValue(2)]
        [Category("设置群体治疗")]
        [DisplayName("治疗之环 人数")]
        [Description("Cast CircleOfHealing if Player below CircleOfHealingPercent >= this number")]
        public int CircleOfHealingNumber { get; set; }

        [Setting]
        [DefaultValue(60)]
        [Category("设置群体治疗")]
        [DisplayName("神圣赞美诗 生命值%")]
        [Description("Cast 神圣赞美诗 if Player below %h >= DHNumber")]
        public int DHPercent { get; set; }

        [Setting]
        [DefaultValue(3)]
        [Category("设置群体治疗")]
        [DisplayName("神圣赞美诗 人数")]
        [Description("Cast 神圣赞美诗 if Player below DHPercent >= this number")]
        public int DHNumber { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置群体治疗")]
        [DisplayName("圣言术: 佑(对主坦)")]
        [Description("Cast 圣言术: 佑 if Player below HWSerenityPercent NEAR TANK >= HW佑Number")]
        public bool HWSanctuaryOnTank { get; set; }

        [Setting]
        [DefaultValue(30)]
        [Category("设置治疗")]
        [DisplayName("快速治疗 生命值%")]
        [Description("Cast 快速治疗 at target % hp")]
        public int FlashHealPercent { get; set; }

        [Setting]
        [DefaultValue(85)]
        [Category("设置治疗")]
        [DisplayName("当光明涌动触发时 生命值%")]
        [Description("Cast Surge of Ligth if player hp < %. SoL will cast on lower hp player same if buff ending")]
        public int SoLPercent { get; set; }

        [Setting]
        [DefaultValue(70)]
        [Category("设置BUFF")]
        [DisplayName("能量灌注 生命值%")]
        [Description("Cast 能量灌注 to empower healing if number of player lower than %hp >= PowerInfusionNumber")]
        public int PowerInfusionPercent { get; set; }

        [Setting]
        [DefaultValue(3)]
        [Category("设置BUFF")]
        [DisplayName("能量灌注 人数")]
        [Description("Cast 能量灌注 to empower healing if number of player lower than PowerInfusionPercent 生命值% >= this value")]
        public int PowerInfusionNumber { get; set; }

        [Setting]
        [DefaultValue(60)]
        [Category("设置BUFF")]
        [DisplayName("能量灌注 法力值%")]
        [Description("Cast 能量灌注 to lower 法力值 cost spell if our 法力值 is lower than this value")]
        public int PowerInfusionManaPercent { get; set; }

        [Setting]
        [DefaultValue(40)]
        [Category("设置治疗")]
        [DisplayName("绝望祷言 生命值%")]
        [Description("Cast 绝望祷言 if we lower than % hp")]
        public int DesperatePrayerPercent { get; set; }

        [Setting]
        [DefaultValue(80)]
        [Category("设置群体治疗")]
        [DisplayName("治疗祷言 生命值%")]
        [Description("Cast Prayer Of Healing if player lower than % hp >= PrayerOfHealingNumber")]
        public int PrayerOfHealingPercent { get; set; }

        [Setting]
        [DefaultValue(3)]
        [Category("设置群体治疗")]
        [DisplayName("治疗祷言 人数")]
        [Description("Cast Prayer Of Healing if player lower than PrayerOfHealingPercent >= this value")]
        public int PrayerOfHealingNumber { get; set; }

        [Setting]
        [DefaultValue(85)]
        [Category("设置群体治疗")]
        [DisplayName("爆流/光晕/神圣之星 生命值%")]
        [Description("Cast Cascade/Halo if player lower than % hp >= CascadeHaloDivineStarNumber")]
        public int CascadeHaloPercent { get; set; }

        [Setting]
        [DefaultValue(2)]
        [Category("设置群体治疗")]
        [DisplayName("爆流/光晕/神圣之星 人数")]
        [Description("Cast Cascade/Halo if player lower than CascadeHaloDivinestarPercent >= this value")]
        public int CascadeHaloNumber { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置治疗")]
        [DisplayName("总是给当前坦恢复术")]
        [Description("try keep Renew always active on tank")]
        public bool RenewAlwaysActiveOnTank { get; set; }
        

        [Setting]
        [DefaultValue(true)]
        [Category("设置治疗")]
        [DisplayName("使用恢复术(移动中)")]
        [Description("Cast Renew at RenewPercent if moving")]
        public bool UseRenewOnMoving { get; set; }

        [Setting]
        [DefaultValue(80)]
        [Category("设置治疗")]
        [DisplayName("恢复术 生命值%")]
        [Description("Cast Renew at 生命值%")]
        public int RenewPercent { get; set; }

        [Setting]
        [DefaultValue(25)]
        [Category("设置法力值管理")]
        [DisplayName("希望圣歌 法力值%")]
        [Description("Cast Hymn Of Hope at % 法力值")]
        public int HymnOfHopePercent { get; set; }

        [Setting]
        [DefaultValue(20)]
        [Category("设置食物")]
        [DisplayName("饮料 法力值%")]
        [Description("使用饮料 % 法力值")]
        public int ManaPercent { get; set; }

        [Setting]
        [DefaultValue(0)]
        [Category("设置食物")]
        [DisplayName("食物 生命值%")]
        [Description("Eat at % hp")]
        public int HealthPercent { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置虚空转移")]
        [DisplayName("启用虚空转移")]
        [Description("enable/Disable Void Shift ")]
        public bool UseVoidShift { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置虚空转移")]
        [DisplayName("只对坦职业使用虚空转移")]
        [Description("enable/Disable cast Void Shift only on tank")]
        public bool UseVoidShiftOnlyOnTank { get; set; }

        [Setting]
        [DefaultValue(20)]
        [Category("设置虚空转移")]
        [DisplayName("虚空转移目标 生命值%")]
        [Description("cast Void Shift on target below of this 生命值%")]
        public int VoidShiftTarget { get; set; }

        [Setting]
        [DefaultValue(50)]
        [Category("设置虚空转移")]
        [DisplayName("对自己使用虚空转移")]
        [Description("cast Void Shift on target only if my HP greather than this value")]
        public int VoidShiftMe { get; set; }

        [Setting, DefaultValue(KingwowKeys.X)]
        [Category("设置热键")]
        [DisplayName("暂停键")]
        [Description("enable/disable Routine")]
        public KingwowKeys PauseKey { get; set; }

        [Setting, DefaultValue(ModifierKeys.Alt)]
        [Category("设置热键")]
        [DisplayName("组合键")]
        [Description("Mod key for hotkey")]
        public ModifierKeys ModKey { get; set; }

        
    }
}