using System.IO;
using Styx;
using Styx.Helpers;
using System.ComponentModel;
using DefaultValue = Styx.Helpers.DefaultValueAttribute;
using Styx.Common;
using Styx.Common.Helpers;

namespace KingWoW
{
    public class DemonologyWarlockSettings : Settings
    {
        public static DemonologyWarlockSettings Instance = new DemonologyWarlockSettings();

        public string path_name = Path.Combine(Styx.Common.Utilities.AssemblyDirectory, string.Format(@"KingWOWCurrentSettings/FireMage/FireMage-Settings-{0}.xml", StyxWoW.Me.Name));

        public DemonologyWarlockSettings()
            : base(Path.Combine(Styx.Common.Utilities.AssemblyDirectory, string.Format(@"KingWOWCurrentSettings/FireMage/FireMage-Settings-{0}.xml", StyxWoW.Me.Name)))
        {
        }

        [Setting]
        [DefaultValue(28)]
        [Category("设置风筝")]
        [DisplayName("风筝距离")]
        [Description("Distance to pull from")]
        public float PullDistance { get; set; }

        public enum ArmorType
        {
            FROST,
            MOLTEN,
            MAGE
        }

        public enum TargetType
        {
            MANUAL,
            SEMIAUTO,
            AUTO
        }

        [Setting]
        [DefaultValue(false)]
        [Category("设置打断")]
        [DisplayName("自动打断")]
        [Description("Cast counterspell if target casting and interrumpible")]
        public bool AutoInterrupt { get; set; }

        [Setting]
        [DefaultValue(85)]
        [Category("设置法力值")]
        [DisplayName("法力宝石 法力值%")]
        [Description("Use Mana Gem if your mana % lower tan this value")]
        public int UseManaGemPercent { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置目标")]
        [DisplayName("自动攻击目标")]
        [Description("Enable/Disable autofacing current target")]
        public bool AutofaceTarget { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置驱散诅咒")]
        [DisplayName("启用驱散诅咒")]
        [Description("Autocast remove curse on friend")]
        public bool UseDecurse { get; set; }

        [Setting]
        [DefaultValue(TargetType.AUTO)]
        [Category("设置目标")]
        [DisplayName("目标选择类型")]
        [Description("AUTO/MANUAL enable/disable autotargeting SEMIAUTO: same logic of AUTO but no switch selected target")]
        public TargetType TargetTypeSelected { get; set; }

        [Setting]
        [DefaultValue(2)]
        [Category("设置多目标DOT")]
        [DisplayName("多目标DOT最少人数")]
        [Description("Enable Multidot rotation if Enemy in range >= this value")]
        public int MultidotEnemyNumberMin { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置多目标DOT")]
        [DisplayName("不对玩家使用DOT")]
        [Description("Avoid apply dot on player")]
        public bool AvoidDOTPlayers { get; set; }

        [Setting]
        [DefaultValue(5)]
        [Category("设置多目标DOT")]
        [DisplayName("多目标DOT最大人数")]
        [Description("Disable Multidot rotation if dotted Enemy > this value")]
        public int MultidotEnemyNumberMax { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置多目标DOT")]
        [DisplayName("启用多目标DOT输出")]
        [Description("Enable/Disable use of Multidot rotation")]
        public bool MultidotEnabled { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置多目标DOT")]
        [DisplayName("DOT不攻击被控目标")]
        [Description("Avoid Crowd Controlled in Multidot Rotation")]
        public bool MultidotAvoidCC { get; set; }

        [Setting]
        [DefaultValue(ArmorType.MOLTEN)]
        [Category("设置BUFF")]
        [DisplayName("启用护甲术 FROST(霜)/MOLTEN(熔岩)/MAGE(法师)")]
        [Description("chose armor to use")]
        public ArmorType ArmorToUse { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置BUFF")]
        [DisplayName("启用寒冰护体")]
        [Description("chose if automatically cast Ice barrier on cooldown")]
        public bool UseIcebarrier { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置BUFF")]
        [DisplayName("自动奥术光辉")]
        [Description("chose if buff arcane brillance automatic or manual")]
        public bool AutoBuffBrillance { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置BUFF")]
        [DisplayName("寒冰结界对坦使用")]
        [Description("chose if automaticallycast Ice ward on Tank")]
        public bool IceWardOnTank { get; set; }

        [Setting]
        [DefaultValue(20)]
        [Category("设置食物")]
        [DisplayName("饮料 法力值%")]
        [Description("Drink at % mana")]
        public int ManaPercent { get; set; }

        [Setting]
        [DefaultValue(50)]
        [Category("设置食物")]
        [DisplayName("食物 生命值%")]
        [Description("Eat at % hp")]
        public int HealthPercent { get; set; }

        [Setting]
        [DefaultValue(50)]
        [Category("设置唤醒")]
        [DisplayName("唤醒 生命值%")]
        [Description("Cast evocation if not rune and not evocation talent selected")]
        public int EvocationHP { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置唤醒")]
        [DisplayName("启用唤醒")]
        [Description("enable/disable use of evocation if not rune and not evocation talent selected")]
        public bool UseEvocation { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置唤醒")]
        [DisplayName("战斗中使用唤醒")]
        [Description("enable/disable use of evocation in combat if not rune and not evocation talent selected")]
        public bool UseEvocationInCombat { get; set; }

        [Setting]
        [DefaultValue(4)]
        [Category("设置AOE")]
        [DisplayName("启用AOE敌人数")]
        [Description("use AOE spells if mobs number >= this value")]
        public int AOECount { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置AOE")]
        [DisplayName("尝试暂停AOE")]
        [Description("Try to avoid AOE spells ")]
        public bool AvoidAOE { get; set; }  

        [Setting]
        [DefaultValue(true)]
        [Category("设置AOE")]
        [DisplayName("启用烈焰风暴")]
        [Description("enable/disable use of UseFlameStrike")]
        public bool UseFlameStrike { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置AOE")]
        [DisplayName("启用龙息术")]
        [Description("enable/disable use of DragonBreath")]
        public bool UseDragonBreath { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置AOE")]
        [DisplayName("启用魔爆术")]
        [Description("enable/disable use of ArcaneExplosion")]
        public bool UseArcaneExplosion { get; set; }
        
        

        public enum CDUseType
        {
            COOLDOWN,
            BOSS,
            CONDITION,
            MANUAL
        }

        public enum CDCombustionUseType
        {
            COOLDOWN,
            BOSS,
            CONDITION,
            MANUAL
        }

        [Setting]
        [DefaultValue(CDCombustionUseType.COOLDOWN)]
        [Category("设置燃烧")]
        [DisplayName("启用燃烧")]
        [Description("Chose when use Combustion: MANUAL, COOLDOWN will cast if ignite and pyroblast proc on target, CONDITION will cast if ignite power greater or equal than MinIgniteForCombustion")]
        public CDCombustionUseType CDUseCombustion { get; set; }


        [Setting]
        [DefaultValue(20000)]
        [Category("设置燃烧")]
        [DisplayName("使用燃烧(当点燃时间大于)")]
        [Description("if use Combustion: CONDITION will cast combustion if ignite power greater or equal than MinIgniteForCombustion")]
        public int MinIgniteForCombustion { get; set; }
        


        [Setting]
        [DefaultValue(CDUseType.COOLDOWN)]
        [Category("设置爆发CD")]
        [DisplayName("启用魔典")]
        [Description("Chose when use your Grimoire CD")]
        public CDUseType CDUseGrimoire { get; set; }

        [Setting]
        [DefaultValue(CDUseType.COOLDOWN)]
        [Category("设置爆发CD")]
        [DisplayName("启用操控时间")]
        [Description("Chose when use your AlterTime CD")]
        public CDUseType CDUseAlterTime { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置战斗智能")]
        [DisplayName("自动时光盾")]
        [Description("Cast TemporalShield if talented on Cooldown")]
        public bool AutoTemporalShield { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置战斗智能")]
        [DisplayName("启用深度冻结")]
        [Description("Chose to enable/disable Deep Freeze")]
        public bool UseDeepFreeze { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置战斗智能")]
        [DisplayName("启用冰霜之环")]
        [Description("Chose to enable/disable use of ring of frost")]
        public bool UseRingOfFrost { get; set; }
        
        [Setting]
        [DefaultValue(true)]
        [Category("设置杂项")]
        [DisplayName("自动战斗下马")]
        [Description("Auto start combat rotation when in combat and mounted")]
        public bool AutoDismountOnCombat { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置杂项")]
        [DisplayName("启用闪现")]
        [Description("Chose to enable/disable automatic Blink")]
        public bool UseBlink { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置杂项")]
        [DisplayName("启用冰霜新星")]
        [Description("Chose to enable/disable automatic FrostNova")]
        public bool UseFrostNova { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置杂项")]
        [DisplayName("启用能量符文")]
        [Description("Chose to enable/disable if talented RuneOfPower")]
        public bool UseRuneOfPower { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置杂项")]
        [DisplayName("唤醒BUFF(祈愿)")]
        [Description("Chose to enable/disable if talented automatic invocation buff")]
        public bool EvocationBuffAuto { get; set; }


        [Setting]
        [DefaultValue(true)]
        [Category("设置杂项")]
        [DisplayName("启用咒术护盾")]
        [Description("Chose to enable/disable if talented Incanter Ward on CD")]
        public bool UseIncenterWardOnCD { get; set; }

        [Setting]
        [DefaultValue(9)]
        [Category("设置杂项")]
        [DisplayName("一级斩杀生命百分比")]
        [Description("Start First Last Kill if boss hp lower than this value and should more than Second")]
        public int Phase1KillBossHP { get; set; }
        
        [Setting]
        [DefaultValue(3)]
        [Category("设置杂项")]
        [DisplayName("二级斩杀生命百分比")]
        [Description("Start Second Last Kill if boss hp lower than this value")]
        public int Phase2KillBossHP { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置杂项")]
        [DisplayName("启用寒冰屏障")]
        [Description("Chose to enable/disable automatic IceBlock")]
        public bool IceBlockUse { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置杂项")]
        [DisplayName("瞬发炎暴术")]
        [Description("Use Pyroblast only if HU buff is up")]
        public bool PyroOnlyWithHU { get; set; }


        [Setting]
        [DefaultValue(false)]
        [Category("设置PVP")]
        [DisplayName("启用法术吸取")]
        [Description("UseSpellSteal on focused target if exist or Current target")]
        public bool UseSpellSteal { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置PVP")]
        [DisplayName("启用变形术")]
        [Description("UsePolymorf on focused target")]
        public bool UsePolymorf { get; set; }

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

        [Setting, DefaultValue(KingwowKeys.M)]
        [Category("设置热键")]
        [DisplayName("暂停多目标DOT")]
        [Description("enable/disable Multidot")]
        public KingwowKeys MultidotKey { get; set; }

        [Setting, DefaultValue(KingwowKeys.A)]
        [Category("设置热键")]
        [DisplayName("暂停AOE")]
        [Description("enable/disable AvoidAOE function")]
        public KingwowKeys AvoidAOEKey { get; set; }

    }
}