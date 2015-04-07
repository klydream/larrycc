using System.IO;
using Styx;
using Styx.Helpers;
using System.ComponentModel;
using DefaultValue = Styx.Helpers.DefaultValueAttribute;
using Styx.Common;
using Styx.Common.Helpers;
using System.Windows.Forms;

namespace KingWoW
{
    public class FrostMageSettings : Settings
    {
        public static FrostMageSettings Instance = new FrostMageSettings();

        public string path_name = Path.Combine(Styx.Common.Utilities.AssemblyDirectory, string.Format(@"KingWOWCurrentSettings/FrostMage/FrostMage-Settings-{0}.xml", StyxWoW.Me.Name));

        public FrostMageSettings()
            : base(Path.Combine(Styx.Common.Utilities.AssemblyDirectory, string.Format(@"KingWOWCurrentSettings/FrostMage/FrostMage-Settings-{0}.xml", StyxWoW.Me.Name)))
        {
        }

        public enum TargetType
        {
            MANUAL,
            SEMIAUTO,
            AUTO
        }

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
        [DefaultValue(false)]
        [Category("设置驱散诅咒")]
        [DisplayName("启用驱散诅咒")]
        [Description("Autocast remove curse on friends")]
        public bool UseDecurse { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置打断")]
        [DisplayName("自动打断")]
        [Description("Cast counterspell if target casting and interrumpible")]
        public bool AutoInterrupt { get; set; }

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
        [DisplayName("多目标DOT最少人数")]
        [Description("Enable Multidot rotation if Enemy in range >= this value")]
        public int MultidotEnemyNumberMin { get; set; }

        [Setting]
        [DefaultValue(5)]
        [Category("设置多目标DOT")]
        [DisplayName("多目标DOT最大人数")]
        [Description("Disable Multidot rotation if dotted Enemy > this value")]
        public int MultidotEnemyNumberMax { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置多目标DOT")]
        [DisplayName("启用多目标DOT输出循环")]
        [Description("Enable/Disable use of Multidot rotation")]
        public bool MultidotEnabled { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置多目标DOT")]
        [DisplayName("不攻击被控目标")]
        [Description("Avoid Crowd Controlled in Multidot Rotation")]
        public bool MultidotAvoidCC { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置多目标DOT")]
        [DisplayName("对玩家使用")]
        [Description("Avoid apply dot on player")]
        public bool AvoidDOTPlayers { get; set; }

        [Setting]
        [DefaultValue(28)]
        [Category("设置风筝")]
        [DisplayName("风筝距离(码)")]
        [Description("Distance to pull from")]
        public float PullDistance { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置宠物")]
        [DisplayName("启用宠物")]
        [Description("enable/disable Pet")]
        public bool UsePet { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置宠物")]
        [DisplayName("启用宠物冰冻术")]
        [Description("enable/disable Use of Pet Freeze spell")]
        public bool UsePetFreeze { get; set; }
        

        public enum ArmorType
        {
            FROST,
            MOLTEN,
            MAGE
        }

        [Setting]
        [DefaultValue(ArmorType.FROST)]
        [Category("设置BUFF")]
        [DisplayName("启用护甲术 FROST(霜)/MOLTEN(熔岩)/MAGE(法师)")]
        [Description("chose armor to use")]
        public ArmorType ArmorToUse { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置BUFF")]
        [DisplayName("启用寒冰屏障")]
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
        [DefaultValue(0)]
        [Category("设置食物")]
        [DisplayName("食物 生命值%")]
        [Description("Eat at % hp")]
        public int HealthPercent { get; set; }

        [Setting]
        [DefaultValue(5)]
        [Category("设置AOE")]
        [DisplayName("烈焰风暴攻击范围")]
        [Description("use Flamestrike AOE spells if mobs number >= this value")]
        public int FlamestrikeAOECount { get; set; }

        [Setting]
        [DefaultValue(5)]
        [Category("设置AOE")]
        [DisplayName("魔爆术攻击范围")]
        [Description("use ArcaneExplosion AOE spells if mobs number >= this value")]
        public int ExplosionAOECount { get; set; }

        [Setting]
        [DefaultValue(8)]
        [Category("设置AOE")]
        [DisplayName("暴风雪攻击范围")]
        [Description("use Blizzard AOE spells if mobs number >= this value")]
        public int BlizzardAOECount { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置AOE")]
        [DisplayName("禁用AOE")]
        [Description("Try to avoid AOE spells ")]
        public bool AvoidAOE { get; set; }     

        public enum CDUseType
        {
            COOLDOWN,
            BOSS,
            MANUAL
        }

        [Setting]
        [DefaultValue(CDUseType.COOLDOWN)]
        [Category("设置爆发CD")]
        [DisplayName("启用冰冷血脉")]
        [Description("Chose when use Icy Veins CD")]
        public CDUseType CDUseIcyVeins { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置控制")]
        [DisplayName("自动寒冰护体")]
        [Description("Cast TemporalShield if talented on Cooldown")]
        public bool AutoTemporalShield { get; set; }

        [Setting]
        [DefaultValue(CDUseType.COOLDOWN)]
        [Category("设置爆发CD")]
        [DisplayName("启用镜像")]
        [Description("Chose when use your Mirror Image CD")]
        public CDUseType CDUseMirrorImage { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置爆发CD")]
        [DisplayName("自动寒冰宝珠")]
        [Description("Chose if AutoCast FrozerOrb (you can disable it questing")]
        public bool AutoFrozerOrb { get; set; }
        

        [Setting]
        [DefaultValue(CDUseType.COOLDOWN)]
        [Category("设置爆发CD")]
        [DisplayName("启用操控时间")]
        [Description("Chose when use your AlterTime CD")]
        public CDUseType CDUseAlterTime { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置控制")]
        [DisplayName("启用深度冻结")]
        [Description("Chose to enable/disable Deep Freeze")]
        public bool UseDeepFreeze { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置控制")]
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
        [DisplayName("启用冰锥术")]
        [Description("Chose to enable/disable cone of cold when facing mob")]
        public bool UseConeOfCold { get; set; }

        [Setting]
        [DefaultValue(3)]
        [Category("设置杂项")]
        [DisplayName("冰锥术敌人数")]
        [Description("cast cone of cold when facing a value of mobs equals or greater than this value")]
        public int ConeOfColdCount { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置杂项")]
        [DisplayName("启用冰霜新星")]
        [Description("Chose to enable/disable automatic FrostNova")]
        public bool UseFrostNova { get; set; }
        

        [Setting]
        [DefaultValue(15)]
        [Category("设置杂项")]
        [DisplayName("寒冰屏障 生命值")]
        [Description("Use Ice Block if hp lower than this value")]
        public int IceBlockHP { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置杂项")]
        [DisplayName("启用寒冰屏障")]
        [Description("Chose to enable/disable automatic IceBlock")]
        public bool IceBlockUse { get; set; }
        

        [Setting]
        [DefaultValue(true)]
        [Category("设置杂项")]
        [DisplayName("启用能量符文")]
        [Description("Chose to enable/disable if talented RuneOfPower")]
        public bool UseRuneOfPower { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置杂项")]
        [DisplayName("自动祈愿")]
        [Description("Chose to enable/disable if talented automatic invocation buff")]
        public bool EvocationBuffAuto { get; set; }
        

        [Setting]
        [DefaultValue(true)]
        [Category("设置杂项")]
        [DisplayName("启用咒术护盾")]
        [Description("Chose to enable/disable if talented Incanter Ward on CD")]
        public bool UseIncenterWardOnCD { get; set; }

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

        [Setting]
        [DefaultValue(true)]
        [Category("设置AOE")]
        [DisplayName("启用烈焰风暴")]
        [Description("enable/disable use of UseFlameStrike")]
        public bool UseFlameStrike { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置AOE")]
        [DisplayName("启用魔爆术")]
        [Description("enable/disable use of ArcaneExplosion")]
        public bool UseArcaneExplosion { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置AOE")]
        [DisplayName("启用暴风雪")]
        [Description("enable/disable use of Blizzard")]
        public bool UseBlizzard { get; set; }

        [Setting, DefaultValue(KingwowKeys.X)]
        [Category("设置热键")]
        [DisplayName("暂停")]
        [Description("enable/disable Routine")]
        public KingwowKeys PauseKey { get; set; }

        [Setting, DefaultValue(ModifierKeys.Alt)]
        [Category("设置热键")]
        [DisplayName("组合键")]
        [Description("Mod key for hotkey")]
        public ModifierKeys ModKey { get; set; }

        [Setting, DefaultValue(KingwowKeys.M)]
        [Category("设置热键")]
        [DisplayName("多目标DOT 模式切换")]
        [Description("enable/disable Multidot")]
        public KingwowKeys MultidotKey { get; set; }

        [Setting, DefaultValue(KingwowKeys.A)]
        [Category("设置热键")]
        [DisplayName("AOE模式切换")]
        [Description("enable/disable AvoidAOE function")]
        public KingwowKeys AvoidAOEKey { get; set; }
        
    }
}
