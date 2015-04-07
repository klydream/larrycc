using System.IO;
using Styx;
using Styx.Helpers;
using System.ComponentModel;
using DefaultValue = Styx.Helpers.DefaultValueAttribute;
using Styx.Common;
using Styx.Common.Helpers;

namespace KingWoW
{
    public class EnhancementShamanSettings : Settings
    {
        public static EnhancementShamanSettings Instance = new EnhancementShamanSettings();

        public string path_name = Path.Combine(Styx.Common.Utilities.AssemblyDirectory, string.Format(@"KingWOWCurrentSettings/EnhancementShaman/EnhancementShaman-Settings-{0}.xml", StyxWoW.Me.Name));

        public EnhancementShamanSettings()
            : base(Path.Combine(Styx.Common.Utilities.AssemblyDirectory, string.Format(@"KingWOWCurrentSettings/EnhancementShaman/EnhancementShaman-Settings-{0}.xml", StyxWoW.Me.Name)))
        {
        }


        [Setting]
        [DefaultValue(false)]
        [Category("设置驱散")]
        [DisplayName("启用驱散 ")]
        [Description("enable/diable dispell")]
        public bool UseDispell { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置杂项")]
        [DisplayName("启用先祖之魂")]
        [Description("enable use of Resurrection out of combat")]
        public bool UseResurrection { get; set; }

        [Setting]
        [DefaultValue(28)]
        [Category("设置风筝")]
        [DisplayName("风筝距离")]
        [Description("Distance to pull from")]
        public float PullDistance { get; set; }

        [Setting]
        [DefaultValue(20)]
        [Category("设置进食")]
        [DisplayName("饮料 法力值%")]
        [Description("Drink at % mana")]
        public int ManaPercent { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置打断")]
        [DisplayName("自动打断")]
        [Description("Cast WindShear if target casting")]
        public bool AutoInterrupt { get; set; }

        [Setting]
        [DefaultValue(0)]
        [Category("设置进食")]
        [DisplayName("食物 生命值%")]
        [Description("Eat at % hp")]
        public int HealthPercent { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置漩涡武器")]
        [DisplayName("启用闪电箭和闪电链")]
        [Description("Use CHAIN_LIGHTNING or LIGHTNING_BOLT as filler in rotation if reach sufficent maelstrom stacks configurable")]
        public bool Use_LB_or_CL_as_filler { get; set; }

        [Setting]
        [DefaultValue(3)]
        [Category("设置漩涡武器")]
        [DisplayName("漩涡武器释放层数")]
        [Description("Use CHAIN_LIGHTNING or LIGHTNING_BOLT as filler in rotation if reach this number of maelstrom stacks")]
        public int maelstrom_fillet_count { get; set; }
        

        public enum ImbueType
        {
            DEFAULT,
            CHOOSEN,
            MANUAL
        }

        [Setting]
        [DefaultValue(ImbueType.DEFAULT)]
        [Category("设置灌魔")]
        [DisplayName("灌魔类型")]
        [Description("Imbue method if CHOOSEN set MH and OH imbue type below")]
        public ImbueType imbueType { get; set; }

        public enum ImbueWeaponType
        {
            WINDFURY,
            FLAMETONGUE,
            FROSTBRAND,
            ROCKBITER,
            EARTHLIVING
        }

        [Setting]
        [DefaultValue(ImbueWeaponType.WINDFURY)]
        [Category("设置灌魔")]
        [DisplayName("主手灌魔(WINDFURY风怒/FLAMETONGUE火舌/FROSTBRAND冰封/ROCKBITER石化/EARTHLIVING生命)")]
        [Description("If Imbue method is CHOOSEN set MH imbue to this value")]
        public ImbueWeaponType MainHand_imbue { get; set; }

        [Setting]
        [DefaultValue(ImbueWeaponType.FLAMETONGUE)]
        [Category("设置灌魔")]
        [DisplayName("副手灌魔(WINDFURY风怒/FLAMETONGUE火舌/FROSTBRAND冰封/ROCKBITER石化/EARTHLIVING生命)")]
        [Description("If Imbue method is CHOOSEN set OH imbue to this value")]
        public ImbueWeaponType OffHand_imbue { get; set; }

        public enum CDUseType
        {
            COOLDOWN,
            BOSS,
            MANUAL
        }

        public enum AirTotemType
        {
            CAPACITOR,
            GROUNDIG,
            MANUAL
        }

        [Setting]
        [DefaultValue(AirTotemType.GROUNDIG)]
        [Category("设置TT")]
        [DisplayName("空气TT")]
        [Description("Chose Air Totem to pup active as possible CD")]
        public AirTotemType AirTotemActive { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置TT")]
        [DisplayName("启用TT召回")]
        [Description("Use Totemic Recall when OutOfCombat")]
        public bool Totemic_recall_OOC { get; set; }
        
        [Setting]
        [DefaultValue(true)]
        [Category("设置天赋")]
        [DisplayName("瞬发元素冲击")]
        [Description("Use Elemental Blast only on five maelstrom")]
        public bool EB_on_five { get; set; }

        [Setting]
        [DefaultValue(50)]
        [Category("设置天赋")]
        [DisplayName("星界转移 生命值%")]
        [Description("Cast Astral Shift if HP% lower than this value")]
        public int AS_HP { get; set; }

        [Setting]
        [DefaultValue(CDUseType.MANUAL)]
        [Category("设置技能CD")]
        [DisplayName("启用火元素")]
        [Description("Chose when use your Fire Elemental CD")]
        public CDUseType FireElementalCD { get; set; }

        [Setting]
        [DefaultValue(CDUseType.COOLDOWN)]
        [Category("设置技能CD")]
        [DisplayName("启用野性狼魂")]
        [Description("Chose when use your Feral Spirits CD")]
        public CDUseType FeralSpiritCD { get; set; }

        [Setting]
        [DefaultValue(CDUseType.BOSS)]
        [Category("设置技能CD")]
        [DisplayName("启用风暴之鞭TT")]
        [Description("Chose when use your Storm Lash Totem CD")]
        public CDUseType StormlLashTotemCD { get; set; }

        [Setting]
        [DefaultValue(CDUseType.COOLDOWN)]
        [Category("设置技能CD")]
        [DisplayName("启用元素掌握")]
        [Description("Chose when use your Elemental Mastery CD")]
        public CDUseType ElementalMasteryCD { get; set; }
        

        [Setting]
        [DefaultValue(CDUseType.MANUAL)]
        [Category("设置技能CD")]
        [DisplayName("启用升腾")]
        [Description("Chose when use your Ascendance CD")]
        public CDUseType AscendanceCD { get; set; }

        [Setting]
        [DefaultValue(2)]
        [Category("设置AOE")]
        [DisplayName("闪电链 敌数")]
        [Description("Use chain lightining when # mobs > this value")]
        public int ChainLightining_number { get; set; }

        [Setting]
        [DefaultValue(5)]
        [Category("设置AOE")]
        [DisplayName("熔岩TT 敌数")]
        [Description("Use Magma Totem when # mobs > this value")]
        public int MagmaTotem_number { get; set; }

        [Setting]
        [DefaultValue(5)]
        [Category("设置AOE")]
        [DisplayName("火焰新星 敌数")]
        [Description("Use Fire Nova when # mobs > this value")]
        public int FireNova_number { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置治疗")]
        [DisplayName("协助治疗模式")]
        [Description("Enable/Disable assist healing mode")]
        public bool AssistHealing { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置治疗")]
        [DisplayName("脱战时回复满血")]
        [Description("Full my HP when OutOfCombat and my mana above 70")]
        public bool FullMeOOC { get; set; }
        
        

        [Setting]
        [DefaultValue(true)]
        [Category("设置移动")]
        [DisplayName("自动攻击目标")]
        [Description("Enable/Disable autofacing current target")]
        public bool AutofaceTarget { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置移动")]
        [DisplayName("自动选择目标")]
        [Description("Enable/Disable AutoTarget enemy")]
        public bool AutoTarget { get; set; }
        
        

        [Setting]
        [DefaultValue(65)]
        [Category("设置治疗")]
        [DisplayName("治疗之潮/先祖指引 生命值%")]
        [Description("HealingTide/Ancestral Guidance PlayerHP condition")]
        public int HealingTide_AncestralGuidanceHP { get; set; }

        [Setting]
        [DefaultValue(4)]
        [Category("设置治疗")]
        [DisplayName("治疗之潮/先祖指引 人数")]
        [Description("HealingTide/Ancestral Guidance PlayersNumber condition")]
        public int HealingTide_AncestralGuidanceNumber { get; set; }

        [Setting]
        [DefaultValue(80)]
        [Category("设置治疗")]
        [DisplayName("治疗之雨 生命值%")]
        [Description("HealingRainHP PlayerHP condition")]
        public int HealingRainHP { get; set; }

        [Setting]
        [DefaultValue(5)]
        [Category("设置治疗")]
        [DisplayName("治疗之雨 人数")]
        [Description("HealingRainNumber PlayersNumber condition")]
        public int HealingRainNumber { get; set; }

        [Setting]
        [DefaultValue(80)]
        [Category("设置治疗")]
        [DisplayName("治疗链 生命值%")]
        [Description("ChainHealHP PlayerHP condition")]
        public int ChainHealHP { get; set; }

        [Setting]
        [DefaultValue(3)]
        [Category("设置治疗")]
        [DisplayName("治疗链 人数")]
        [Description("ChainHeal PlayersNumber condition")]
        public int ChainHealNumber { get; set; }

        [Setting]
        [DefaultValue(90)]
        [Category("设置治疗")]
        [DisplayName("治疗之泉 生命值%")]
        [Description("HealingStreamHP PlayerHP condition")]
        public int HealingStreamHP { get; set; }

        [Setting]
        [DefaultValue(3)]
        [Category("设置治疗")]
        [DisplayName("治疗之泉 人数")]
        [Description("HealingStreamNumber PlayersNumber condition")]
        public int HealingStreamNumber { get; set; }

        [Setting]
        [DefaultValue(80)]
        [Category("设置治疗")]
        [DisplayName("萨满之怒 生命值%")]
        [Description("Cast ShamanistcRage if player HP% below this value ")]
        public int ShamanistcRageHP { get; set; }

        

        [Setting]
        [DefaultValue(40)]
        [Category("设置治疗")]
        [DisplayName("治疗之涌 生命值%")]
        [Description("Cast HealingSurge if player hp <= this value and maelstrom >=3")]
        public int HealingSurgeHP { get; set; }

        [Setting]
        [DefaultValue(40)]
        [Category("设置治疗")]
        [DisplayName("先祖指引 自身生命值%")]
        [Description("Cast AncestralGuidance if player hp <= this value")]
        public int AncestralGuidanceMyHP { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置治疗")]
        [DisplayName("治疗之涌只对自已使用")]
        [Description("Cast HealingSurge Only on Shaman or other player too")]
        public bool HealingSurgeOnlyOnMe { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置控制")]
        [DisplayName("启用妖术")]
        [Description("Use Hex on focused target")]
        public bool UseHex { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置控制")]
        [DisplayName("只对焦点使用妖术 ")]
        [Description("Use Hex on focused target only if instant cast")]
        public bool UseInstantHex { get; set; }
        
    }
}