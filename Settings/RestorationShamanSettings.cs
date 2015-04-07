using System.IO;
using Styx;
using Styx.Helpers;
using System.ComponentModel;
using DefaultValue = Styx.Helpers.DefaultValueAttribute;
using Styx.Common;
using Styx.Common.Helpers;

namespace KingWoW
{
    public class RestorationShamanSettings : Settings
    {
        public static RestorationShamanSettings Instance = new RestorationShamanSettings();

        public string path_name = Path.Combine(Styx.Common.Utilities.AssemblyDirectory, string.Format(@"KingWOWCurrentSettings/RestorationShaman/RestorationShaman-Settings-{0}.xml", StyxWoW.Me.Name));

        public RestorationShamanSettings()
            : base(Path.Combine(Styx.Common.Utilities.AssemblyDirectory, string.Format(@"KingWOWCurrentSettings/RestorationShaman/RestorationShaman-Settings-{0}.xml", StyxWoW.Me.Name)))
        {
        }

        public enum ImbueType
        {
            DEFAULT,
            MANUAL
        }

        [Setting]
        [DefaultValue(false)]
        [Category("设置应急治疗模式")]
        [DisplayName("启用应急治疗(需要先祖迅捷)")]
        [Description("enable SoS healing: It require ANCESTRAL SWIFTNESS otherwise will cast a simple HEALING SURGE")]
        public bool UseSoS { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置应急治疗模式")]
        [DisplayName("只对坦应急治疗")]
        [Description("enable SoS routine only on tank")]
        public bool SoS_healing_only_on_tank { get; set; }

        [Setting]
        [DefaultValue(30)]
        [Category("设置应急治疗模式")]
        [DisplayName("应急治疗生命值%")]
        [Description("Cast SoS heal only if sos healing target hp% below this value")]
        public int SoSHP { get; set; }

        [Setting]
        [DefaultValue(ImbueType.DEFAULT)]
        [Category("Settings IMBUE")]
        [DisplayName("ImbueType")]
        [Description("Imbue method")]
        public ImbueType imbueType { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置移动")]
        [DisplayName("自动攻击当前目标")]
        [Description("Enable/Disable autofacing current target")]
        public bool AutofaceTarget { get; set; }

        [Setting]
        [DefaultValue(28)]
        [Category("设置风筝")]
        [DisplayName("风筝距离")]
        [Description("Distance to pull from")]
        public float PullDistance { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置自我治疗")]
        [DisplayName("优先自我治疗")]
        [Description("Enable/Disable Self Healing Priority: HP% is set below")]
        public bool SelfHealingPriorityEnabled { get; set; }

        [Setting]
        [DefaultValue(50)]
        [Category("设置自我治疗")]
        [DisplayName("优先自我治疗生命值%")]
        [Description("if enabled Self Healing Priority: healing you before other in party until below this HP%")]
        public float SelfHealingPriorityHP { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置打断")]
        [DisplayName("自动打断")]
        [Description("Cast WindShear if target casting")]
        public bool AutoInterrupt { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置战斗控制")]
        [DisplayName("启用妖术(对焦点)")]
        [Description("Use Hex on focused target")]
        public bool UseHex { get; set; }
        

        /*[Setting]
        [DefaultValue(0)]
        [Category("设置治疗循环")]
        [DisplayName("循环类型(0标准,1坦和单目标,2群体治疗,3PVP")]
        [Description("Use 0 for NORMAL ROTATION, Use 1 For First Tank and SINGLE TARGET ROTATION, Use 2 for AOE PRIORITY ROTATION, use 3 for PVP ROTATION")]
        public int RotationType { get; set; }*/

        public enum CDUseType
        {
            COOLDOWN,
            AT_CONDITION,
            MANUAL
        }

        [Setting]
        [DefaultValue(CDUseType.AT_CONDITION)]
        [Category("设置升腾")]
        [DisplayName("升腾使用时机 ")]
        [Description("choose when cast Ascendance CD")]
        public CDUseType WhenUseAscendance { get; set; }

        [Setting]
        [DefaultValue(65)]
        [Category("设置升腾")]
        [DisplayName("升腾 生命值%")]
        [Description("cast Ascendance if AT_CONDITION choosen and AscendanceNumber players below this value HP% verified ")]
        public int AscendanceHP { get; set; }

        [Setting]
        [DefaultValue(3)]
        [Category("设置升腾")]
        [DisplayName("人员数量")]
        [Description("cast Ascendance if AT_CONDITION choosen and players below AscendanceHP number high or equal this number verified ")]
        public int AscendanceNumber { get; set; }



        [Setting]
        [DefaultValue(true)]
        [Category("设置驱散")]
        [DisplayName("启用驱散 ")]
        [Description("enable/diable dispell")]
        public bool UseDispell { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置杂项")]
        [DisplayName("启用先祖之魂")]
        [Description("enable use of Resurrection out of combat")]
        public bool UseResurrection { get; set; }

        [Setting]
        [DefaultValue(80)]
        [Category("设置自我项")]
        [DisplayName("萨满之怒 生命值%")]
        [Description("Cast ShamanistcRage if player HP% below this value ")]
        public int ShamanistcRageHP { get; set; }

        [Setting]
        [DefaultValue(50)]
        [Category("设置自我项")]
        [DisplayName("星界转移 生命值%")]
        [Description("Cast Astral Shift if HP% lower than this value")]
        public int AS_HP { get; set; }

        [Setting]
        [DefaultValue(50)]
        [Category("设置杂项")]
        [DisplayName("治疗目标NPC阈值")]
        [Description("Set Threshold for heal targetted NPC over party member: CC will heal the targetted NPC if noone party/raid member in healing range below this value")]
        public int HealNPC_threshold { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置杂项")]
        [DisplayName("自动灵魂行者的恩赐")]
        [Description("enable use Autocast Spiritwalker's Grace when moving on combat (it is activated when CC found at least a player whith less than healing wave percent HP")]
        public bool Autocast_Spirit_Walk { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置杂项")]
        [DisplayName("刷新潮汐奔涌")]
        [Description("Cast Riptide on lowest heal target if no tidalWaves buff active")]
        public bool MantainTidalWaves { get; set; }

        [Setting]
        [DefaultValue(85)]
        [Category("设置治疗法术")]
        [DisplayName("激流 生命值%")]
        [Description("Cast Riptide at target % hp")]
        public int RiptidePercent { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置治疗法术")]
        [DisplayName("使用激流保持坦的BUFF")]
        [Description("Try to mantain Riptide always UP on tank")]
        public bool RiptideAlwaysUPOnTank { get; set; }

        [Setting]
        [DefaultValue(90)]
        [Category("设置治疗法术")]
        [DisplayName("治疗波 生命值%")]
        [Description("Cast HealingWave at target % hp")]
        public int HealingWavePercent { get; set; }

        [Setting]
        [DefaultValue(75)]
        [Category("设置法力控制")]
        [DisplayName("法力潮汐图腾 法力值%")]
        [Description("Mana Tide Totem at % mana")]
        public int ManaTideTotemPercent { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置法力控制")]
        [DisplayName("保存法力值")]
        [Description("Try save mana: interrupt no needed heal")]
        public bool TrySaveMana { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置法力控制")]
        [DisplayName("闪电箭回复法力(魔流雕纹)")]
        [Description("Cast LIGHTNING_BOLT in no other heal needed to rec mana: used ONLY if \"Telluric Currents\" glyphed")]
        public bool LB_for_rec_mana { get; set; }
        

        [Setting]
        [DefaultValue(50)]
        [Category("设置治疗法术")]
        [DisplayName("强效治疗波 生命值%")]
        [Description("Cast Greater Healing Wave at target % hp")]
        public int GreaterHealingWavePercent { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置AOE治疗")]
        [DisplayName("禁用AOE治疗")]
        [Description("Enable/disable AOE healing")]
        public bool DisableAOE_HealingRotation { get; set; }
        

        [Setting]
        [DefaultValue(85)]
        [Category("设置AOE治疗")]
        [DisplayName("治疗之雨 生命值%")]
        [Description("Cast Healing Rain if Player below %h >= #HealingRainNumber")]
        public int HealingRainPercent { get; set; }

        [Setting]
        [DefaultValue(3)]
        [Category("设置AOE治疗")]
        [DisplayName("治疗之雨 人数")]
        [Description("Cast Healing Rain if Player below HealingRainPercent >= this number")]
        public int HealingRainNumber { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置AOE治疗")]
        [DisplayName("只在坦区域 治疗之雨")]
        [Description("Cast Healing Rain only on tank location if condition verified")]
        public bool HealingRainOnlyOnTankLocation { get; set; }

        [Setting]
        [DefaultValue(30)]
        [Category("设置治疗法术")]
        [DisplayName("治疗之涌 生命值%")]
        [Description("Cast Healing Surge at target % hp")]
        public int HealingSurgePercent { get; set; }

        [Setting]
        [DefaultValue(75)]
        [Category("设置AOE治疗")]
        [DisplayName("治疗链 生命值%")]
        [Description("Cast Chain Heal if player lower than % hp >= ChainHealNumber")]
        public int ChainHealPercent { get; set; }

        [Setting]
        [DefaultValue(3)]
        [Category("设置AOE治疗")]
        [DisplayName("治疗链 人数")]
        [Description("Cast Chain Heal if player lower than ChainHealPercent >= this value")]
        public int ChainHealNumber { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置AOE治疗")]
        [DisplayName("对坦区域 治疗链")]
        [Description("Cast Chain heal only from tank location if condition verified")]
        public bool ChainHealOnlyFromTank { get; set; }

        [Setting]
        [DefaultValue(50)]
        [Category("设置AOE治疗")]
        [DisplayName("灵魂链接 生命值%")]
        [Description("Cast Spirit Link if player lower than % hp >= SpiritLinkNumber")]
        public int SpiritLinkPercent { get; set; }

        [Setting]
        [DefaultValue(4)]
        [Category("设置AOE治疗")]
        [DisplayName("灵魂链接 人数")]
        [Description("Cast Spirit Link if player lower than SpiritLinkPercent >= this value")]
        public int SpiritLinkNumber { get; set; }

        [Setting]
        [DefaultValue(40)]
        [Category("设置进食")]
        [DisplayName("饮料 法力值%")]
        [Description("Drink at % mana")]
        public int ManaPercent { get; set; }

        [Setting]
        [DefaultValue(40)]
        [Category("设置进食")]
        [DisplayName("食物 生命值%")]
        [Description("Eat at % hp")]
        public int HealthPercent { get; set; }

        public enum healingTotemCastType
        {
            ALWAYS,
            AT_CONDITION,
            MANUAL
        }

        [Setting]
        [DefaultValue(healingTotemCastType.ALWAYS)]
        [Category("设置治疗之泉TT")]
        [DisplayName("治疗之泉施放时机")]
        [Description("Always will cast on ccondown, conditios will cast stream totem when heal percent and number verified")]
        public healingTotemCastType WhenCastStreamTotem { get; set; }

        [Setting]
        [DefaultValue(90)]
        [Category("设置治疗之泉TT")]
        [DisplayName("治疗之泉 生命值%")]
        [Description("Cast Stream Totem if player lower than % hp >= StreamTotemNumber")]
        public int StreamTotemPercent { get; set; }

        [Setting]
        [DefaultValue(3)]
        [Category("设置治疗之泉TT")]
        [DisplayName("治疗之泉 人数")]
        [Description("Cast Stream Totem if player lower than StreamTotemPercent >= this value")]
        public int StreamTotemNumber { get; set; }

        [Setting]
        [DefaultValue(healingTotemCastType.AT_CONDITION)]
        [Category("设置治疗之潮TT")]
        [DisplayName("治疗之潮施放时机")]
        [Description("Always will cast on ccondown, conditios will cast stream totem when heal percent and number verified")]
        public healingTotemCastType WhenCastHealingTideTotem { get; set; }

        [Setting]
        [DefaultValue(50)]
        [Category("设置治疗之潮TT")]
        [DisplayName("治疗之潮 生命值%")]
        [Description("Cast Stream Totem if player lower than % hp >= StreamTotemNumber")]
        public int HealingTideTotemPercent { get; set; }

        [Setting]
        [DefaultValue(4)]
        [Category("设置治疗之潮TT")]
        [DisplayName("治疗之潮 人数")]
        [Description("Cast HealingTideTotem if player lower than StreamTotemPercent >= this value")]
        public int HealingTideTotemNumber { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置杂项")]
        [DisplayName("启用脱战治疗")]
        [Description("enable-disable use of OutOfCombat healing")]
        public bool OOCHealing { get; set; }
    }
}