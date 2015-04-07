using System.IO;
using Styx;
using Styx.Helpers;
using System.ComponentModel;
using DefaultValue = Styx.Helpers.DefaultValueAttribute;
using Styx.Common;
using Styx.Common.Helpers;

namespace KingWoW
{
    class ExtraUtilsSettings : Settings
    {
        public static ExtraUtilsSettings Instance = new ExtraUtilsSettings();

        public string path_name = Path.Combine(Styx.Common.Utilities.AssemblyDirectory, string.Format(@"KingWOWCurrentSettings/ExtraBuffUtilsSettings/ExtraBuffUtilsSettings-{0}.xml", StyxWoW.Me.Name));

        public ExtraUtilsSettings()
            : base(Path.Combine(Styx.Common.Utilities.AssemblyDirectory, string.Format(@"KingWOWCurrentSettings/ExtraBuffUtilsSettings/ExtraBuffUtilsSettings-{0}.xml", StyxWoW.Me.Name)))
        {
        }

        public enum FlaskType
        {
            Intellect,
            Spirit,
            Stamina,
            Strenght,
            Agility
        }

        [Setting]
        [DefaultValue(false)]
        [Category("炼金术")]
        [DisplayName("使用炼金术专用合剂")]
        [Description("Enable/disable use of alchemy flask")]
        public bool UseAlchemyFlask { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("移动设置")]
        [DisplayName("自动移动")]
        [Description("Enable/disable CC movement")]
        public bool movementEnabled { get; set; }
        

        [Setting]
        [DefaultValue(FlaskType.Intellect)]
        [Category("炼金术")]
        [DisplayName("合剂类型Int智力/Spi精神/Sta耐力/Str力量/Agi敏捷")]
        [Description("Intellect(智力)/Spirit(精神)/Stamina(耐力)/Strenght(力量)/Agility(敏捷)")]
        public FlaskType FlaskTypeToUse { get; set; }

        [Setting]
        [DefaultValue(40)]
        [Category("治疗石(SS糖)")]
        [DisplayName("治疗石(SS糖)使用血量%")]
        [Description("Use healthstone if lower than HP%")]
        public float HealthsStoneHP { get; set; }

        public enum TrinketUseType
        {
            OnBoss,
            OnCrowdControlled,
            Manual,
            Always,
            At_HP,
            At_MANA
        }

        public enum GenericUseType
        {
            OnBoss,
            Manual,
            Always
        }

        [Setting]
        [DefaultValue(TrinketUseType.Manual)]
        [Category("饰品1")]
        [DisplayName("启用饰品1对像")]
        [Description("Use trinket_1 on condition")]
        public TrinketUseType UseTrinket_1_OnCondition { get; set; }

        [Setting]
        [DefaultValue(50)]
        [Category("饰品1")]
        [DisplayName("血量%")]
        [Description("Use trinket_1 on condition At_HP if HP lower than this value")]
        public float Trinket_1_HP { get; set; }

        [Setting]
        [DefaultValue(50)]
        [Category("重新行动")]
        [DisplayName("重新行动生命血量%")]
        [Description("Use Life spirit if lower than HP%")]
        public float LifeSpiritHP { get; set; }

        [Setting]
        [DefaultValue(30)]
        [Category("重新行动")]
        [DisplayName("重新行动魔法值%")]
        [Description("Use Water spirit if lower than MANA%")]
        public float WaterSpiritMANA { get; set; }

        [Setting]
        [DefaultValue(50)]
        [Category("饰品1")]
        [DisplayName("法力值%")]
        [Description("Use trinket_1 on condition At_MANA if MANA lower than this value")]
        public float Trinket_1_MANA { get; set; }

        [Setting]
        [DefaultValue(TrinketUseType.Manual)]
        [Category("饰品2")]
        [DisplayName("启用饰品2对像")]
        [Description("Use trinket_2 on condition")]
        public TrinketUseType UseTrinket_2_OnCondition { get; set; }

        [Setting]
        [DefaultValue(50)]
        [Category("饰品2")]
        [DisplayName("血量%")]
        [Description("Use trinket_2 on condition At_HP if HP lower than this value")]
        public float Trinket_2_HP { get; set; }

        [Setting]
        [DefaultValue(50)]
        [Category("饰品2")]
        [DisplayName("法力值%")]
        [Description("Use trinket_2 on condition At_MANA if MANA lower than this value")]
        public float Trinket_2_MANA { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("提示音")]
        [DisplayName("启用/禁用 ")]
        [Description("Enable/Disable CC sounds")]
        public bool SoundsEnabled { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("种族技能")]
        [DisplayName("启用/禁用 ")]
        [Description("Enable/Disable Racial Ability")]
        public bool UseRacials { get; set; }

        [Setting]
        [DefaultValue(GenericUseType.Always)]
        [Category("专业技能")]
        [DisplayName("使用工程手套")]
        [Description("Use Engi. Gloves OnCondition")]
        public GenericUseType UseGloves_OnCondition { get; set; }

        [Setting]
        [DefaultValue(GenericUseType.Always)]
        [Category("专业技能")]
        [DisplayName("使用生命之血")]
        [Description("Use Lifeblood OnCondition")]
        public GenericUseType UseLifeblood_OnCondition { get; set; }

        [Setting]
        [DefaultValue(70)]
        [Category("种族技能")]
        [DisplayName("纳鲁的祝福 血量%")]
        [Description("Cast Gift of the Naaru if your HP% lower than this value")]
        public int GigtOfTheNaaruHP { get; set; }

        [Setting]
        [DefaultValue(70)]
        [Category("种族技能")]
        [DisplayName("奥术洪流 魔法值%")]
        [Description("Cast Arcane Torrent if your MANA% lower than this value")]
        public int ArcaneTorrentMana { get; set; }


        [Setting]
        [DefaultValue(true)]
        [Category("BOSS逻辑")]
        [DisplayName("启用BOSS逻辑攻击")]
        [Description("Enable boss logic: interrupt cast on thock or oondast etc..")]
        public bool UseBossLogic { get; set; }
 
    }
}
