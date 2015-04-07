using System.IO;
using Styx;
using Styx.Helpers;
using System.ComponentModel;
using DefaultValue = Styx.Helpers.DefaultValueAttribute;
using Styx.Common;
using Styx.Common.Helpers;

namespace KingWoW
{
    public class RetriPaladinSettings : Settings
    {
        public static RetriPaladinSettings Instance = new RetriPaladinSettings();

        public string path_name = Path.Combine(Styx.Common.Utilities.AssemblyDirectory, string.Format(@"KingWOWCurrentSettings/RetriPaladin/RetriPaladin-Settings-{0}.xml", StyxWoW.Me.Name));

        public RetriPaladinSettings()
            : base(Path.Combine(Styx.Common.Utilities.AssemblyDirectory, string.Format(@"KingWOWCurrentSettings/RetriPaladin/RetriPaladin-Settings-{0}.xml", StyxWoW.Me.Name)))
        {
        }

        public enum CDUseType
        {
            COOLDOWN,
            BOSS,
            MANUAL
        }

        [Setting]
        [DefaultValue(CDUseType.COOLDOWN)]
        [Category("设置技能CD")]
        [DisplayName("复仇之怒")]
        [Description("Chose when use Avenging Wrath CD")]
        public CDUseType CDUseAvengingWrath { get; set; }

        [Setting]
        [DefaultValue(CDUseType.COOLDOWN)]
        [Category("设置技能CD")]
        [DisplayName("远古列王守卫")]
        [Description("Chose when use Guardian of Ancient Kings CD")]
        public CDUseType CDUseGuardian_of_Ancient_Kings { get; set; }

        [Setting]
        [DefaultValue(CDUseType.COOLDOWN)]
        [Category("设置技能CD")]
        [DisplayName("神圣复仇者")]
        [Description("Chose when use Holy Avenger CD")]
        public CDUseType CDUseHoly_Avenger { get; set; }
       

        public enum BlessingType
        {
            MANUAL,
            KING,
            MIGTH
        }

        public enum SealType
        {
            MANUAL,
            AUTO,
            INSIGHT,
            TRUTH,
            RIGHTEOUSNESS
        }

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
        [DefaultValue(false)]
        [Category("设置移动")]
        [DisplayName("超脱")]
        [Description("Enable/Disable UseEmancipate expecially in PVP")]
        public bool UseEmancipate { get; set; }

        

        [Setting]
        [DefaultValue(BlessingType.MANUAL)]
        [Category("设置BUFF")]
        [DisplayName("祝福类型 KING王者/MIGTH力量(精通)")]
        [Description("Blessing to use")]
        public BlessingType BlessingToUse { get; set; }

        [Setting]
        [DefaultValue(10)]
        [Category("设置BUFF")]
        [DisplayName("圣结护盾(当检测攻击力BUFF时)")]
        [Description("If Sacred shield buff is up and CC detect a specified % increment of Attack Power it recasts Sacred Shield for powerful absorb")]
        public double AttackPowerIncrement { get; set; }

        [Setting]
        [DefaultValue(SealType.AUTO)]
        [Category("设置BUFF")]
        [DisplayName("圣印(INSIGHT洞察/TRUTH真理/RIGHTEOUSNESS正义)")]
        [Description("SealType to use: AUTO will use seal of insight and will cast seal of righteousness on AOE")]
        public SealType SealToUse { get; set; }

        [Setting]
        [DefaultValue(30)]
        [Category("设置风筝")]
        [DisplayName("风筝距离")]
        [Description("Distance to pull from")]
        public float PullDistance { get; set; }

        [Setting]
        [DefaultValue(70)]
        [Category("设置自动治疗")]
        [DisplayName("神圣意志 永恒之火/荣耀圣令 生命值")]
        [Description("Cast Autoheal spells at HP% if DivinePurpose proc")]
        public int DivinePurposeProcAutoHealHP_EF_WoG { get; set; }

        [Setting]
        [DefaultValue(40)]
        [Category("设置自动治疗")]
        [DisplayName("HolyPower 永恒之火/荣耀圣令 生命值")]
        [Description("Cast Autoheal spells at HP% if DivinePurpose proc")]
        public int HolyPowerAutoHealHP_EF_WoG { get; set; }

        [Setting]
        [DefaultValue(70)]
        [Category("设置自动治疗")]
        [DisplayName("无私治愈 生命值")]
        [Description("Cast Autoheal spells at HP% if selfless healer proc")]
        public int ProcAutoHealHPSelfless { get; set; }

        [Setting]
        [DefaultValue(70)]
        [Category("设置自动治疗")]
        [DisplayName("使用第六层天赋 生命值")]
        [Description("Cast Autoheal spells at HP% if can cast any Tier6 talent")]
        public int AutoHealTier6 { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置自动治疗")]
        [DisplayName("启用自动治疗")]
        [Description("enable Autoheal")]
        public bool AutoHeal { get; set; }

        [Setting]
        [DefaultValue(90)]
        [Category("设置应急治疗")]
        [DisplayName("圣佑术 生命值%")]
        [Description("Cast Divine Protection at HP%")]
        public int DivineProtection { get; set; }

        [Setting]
        [DefaultValue(80)]
        [Category("设置应急治疗")]
        [DisplayName("纯净之手 生命值%")]
        [Description("Cast HandOfPurity at HP%")]
        public int HandOfPurity { get; set; }


        [Setting]
        [DefaultValue(10)]
        [Category("设置应急治疗")]
        [DisplayName("圣盾术 生命值%")]
        [Description("Cast Divine Shield at HP%")]
        public int DShp { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置打断")]
        [DisplayName("自动打断")]
        [Description("Cast rebuke/fist_of_justice if target casting")]
        public bool AutoInterrupt { get; set; }


        [Setting]
        [DefaultValue(false)]
        [Category("设置队伍应急治疗")]
        [DisplayName("圣疗术 对队员")]
        [Description("Use Lay on hand for other party player")]
        public bool UseLoHOnPartyMember { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置队伍应急治疗")]
        [DisplayName("保护之手 对队员")]
        [Description("Use Hand of Protection for other party player")]
        public bool UseHoPOnPartyMember { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置队伍应急治疗")]
        [DisplayName("圣佑术 对队员")]
        [Description("Use Divine Shield for other party player")]
        public bool UseDSOnPartyMember { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置队伍应急治疗")]
        [DisplayName("使用能量技能治疗(永恒之火/荣耀圣令) 对队员")]
        [Description("Use Word of Glory/Eternal Flame/etc.. for other party player")]
        public bool UseHealingPartyMember { get; set; }

        [Setting]
        [DefaultValue(10)]
        [Category("设置应急治疗")]
        [DisplayName("保护之手 生命值%")]
        [Description("Cast Hand of Protection at HP%)")]
        public int HoPHp { get; set; }

        [Setting]
        [DefaultValue(15)]
        [Category("设置应急治疗")]
        [DisplayName("圣疗术 生命值%")]
        [Description("Cast Lay on Hands at HP%")]
        public int LoHhp { get; set; }

        [Setting]
        [DefaultValue(7)]
        [Category("设置AOE")]
        [DisplayName("正义圣印 敌数")]
        [Description("If Autoseal enabled: cast  Seal of Righteousness if enemy around 8 will be greater or equal than this value")]
        public int AOE_Seal_Of_Righteousness { get; set; }

        [Setting]
        [DefaultValue(3)]
        [Category("设置AOE")]
        [DisplayName("正义之锤 敌数")]
        [Description("cast  Hammer of the Righteous if enemy around 8 will be greater or equal than this value")]
        public int AOE_Hammer_Of_The_Righteous { get; set; }

        [Setting]
        [DefaultValue(3)]
        [Category("设置AOE")]
        [DisplayName("神圣风暴 敌数")]
        [Description("cast  Divine Storm if enemy around 8 will be greater or equal than this value")]
        public int AOE_Divine_Storm { get; set; }

        [Setting]
        [DefaultValue(20)]
        [Category("设置进食")]
        [DisplayName("饮料 法力值%")]
        [Description("Drink at % mana")]
        public int ManaPercent { get; set; }

        [Setting]
        [DefaultValue(50)]
        [Category("设置进食")]
        [DisplayName("食物 生命值%")]
        [Description("Eat at % hp")]
        public int HealthPercent { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置杂项")]
        [DisplayName("使用复活术")]
        [Description("enable use of Redemption out of combat")]
        public bool UseRedemption { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置杂项")]
        [DisplayName("启用脱战后治疗")]
        [Description("enable use of healing spell to full you until HP < 80")]
        public bool OutOfCombatHeal { get; set; }



    }
}