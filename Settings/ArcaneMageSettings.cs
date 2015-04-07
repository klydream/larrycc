using System.IO;
using Styx;
using Styx.Helpers;
using System.ComponentModel;
using DefaultValue = Styx.Helpers.DefaultValueAttribute;
using Styx.Common;
using Styx.Common.Helpers;

namespace KingWoW
{
    public class ArcaneMageSettings : Settings
    {
        public static ArcaneMageSettings Instance = new ArcaneMageSettings();

        public string path_name = Path.Combine(Styx.Common.Utilities.AssemblyDirectory, string.Format(@"KingWOWCurrentSettings/ArcaneMage/ArcaneMage-Settings-{0}.xml", StyxWoW.Me.Name));

        public ArcaneMageSettings()
            : base(Path.Combine(Styx.Common.Utilities.AssemblyDirectory, string.Format(@"KingWOWCurrentSettings/ArcaneMage/ArcaneMage-Settings-{0}.xml", StyxWoW.Me.Name)))
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
        [DisplayName("启用战斗中唤醒")]
        [Description("enable/disable use of evocation in combat if not rune and not evocation talent selected")]
        public bool UseEvocationInCombat { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置驱散诅咒")]
        [DisplayName("启用驱散诅咒")]
        [Description("Autocast remove curse on friends")]
        public bool UseDecurse { get; set; }

        [Setting]
        [DefaultValue(true)]
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
        [DisplayName("DOT不攻击玩家")]
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
        [DisplayName("启用多目标DOT输出循环")]
        [Description("Enable/Disable use of Multidot rotation")]
        public bool MultidotEnabled { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置低法力值输出循环")]
        [DisplayName("启用低法力值循环")]
        [Description("Enable/Disable Conservative Mana Rotation")]
        public bool UseConservativeRotation { get; set; }

        [Setting]
        [DefaultValue(100)]
        [Category("设置低法力值输出循环")]
        [DisplayName("低法力值上限%")]
        [Description("use Conservative Mana Rotation till mana equals or greater than this value")]
        public int UpperBoundConservativeMana { get; set; }

        [Setting]
        [DefaultValue(4)]
        [Category("设置低法力值输出循环")]
        [DisplayName("奥术充能低法力值")]
        [Description("Cast Missiles on conservative mana rotation if arcane charges equals or greater than this value")]
        public int ConservativeManaMissilesOnlyAtChargeMaiorThan { get; set; }

        [Setting]
        [DefaultValue(4)]
        [Category("设置低法力值输出循环")]
        [DisplayName("奥术弹幕(奥术充能触发)")]
        [Description("Cast Barrage on conservative mana rotation if arcane charges equals or greater than this value")]
        public int ConservativeCastBarrageAtCharge { get; set; }
        
        

        [Setting]
        [DefaultValue(false)]
        [Category("设置多目标DOT")]
        [DisplayName("不攻击被控目标")]
        [Description("Avoid Crowd Controlled in Multidot Rotation")]
        public bool MultidotAvoidCC { get; set; }

        [Setting]
        [DefaultValue(40)]
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

        [Setting]
        [DefaultValue(ArmorType.MAGE)]
        [Category("设置BUFF")]
        [DisplayName("启用护甲术 FROST(霜)/MOLTEN(熔岩)/MAGE(法师)")]
        [Description("chose armor to use")]
        public ArmorType ArmorToUse { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置杂项")]
        [DisplayName("自动战斗下马")]
        [Description("Auto start combat rotation when in combat and mounted")]
        public bool AutoDismountOnCombat { get; set; }
        
        [Setting]
        [DefaultValue(false)]
        [Category("设置杂项")]
        [DisplayName("自动唤醒(祈愿BUFF)")]
        [Description("Chose to enable/disable if talented automatic invocation buff")]
        public bool EvocationBuffAuto { get; set; }
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
        [Description("chose if automatically cast Ice ward on Tank")]
        public bool IceWardOnTank { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置BUFF")]
        [DisplayName("启用寒冰护体")]
        [Description("chose if automatically cast Ice barrier on cooldown")]
        public bool UseIcebarrier { get; set; }   

        [Setting]
        [DefaultValue(50)]
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
        [DefaultValue(4)]
        [Category("设置AOE")]
        [DisplayName("AOE敌人数量")]
        [Description("use AOE spells if mobs number >= this value")]
        public int AOECount { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置AOE")]
        [DisplayName("尝试撤消AOE")]
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
        [DisplayName("启用魔爆术")]
        [Description("enable/disable use of ArcaneExplosion")]
        public bool UseArcaneExplosion { get; set; }

        public enum CDUseType
        {
            COOLDOWN,
            BOSS,
            MANUAL
        }

        [Setting]
        [DefaultValue(CDUseType.COOLDOWN)]
        [Category("设置爆发CD")]
        [DisplayName("启用奥术强化")]
        [Description("Chose when use ArcanePower CD")]
        public CDUseType CDUseArcanePower { get; set; }

        [Setting]
        [DefaultValue(CDUseType.COOLDOWN)]
        [Category("设置爆发CD")]
        [DisplayName("启用镜像")]
        [Description("Chose when use your Mirror Image CD")]
        public CDUseType CDUseMirrorImage { get; set; }

        [Setting]
        [DefaultValue(CDUseType.COOLDOWN)]
        [Category("设置爆发CD")]
        [DisplayName("启用操控时间")]
        [Description("Chose when use your AlterTime CD")]
        public CDUseType CDUseAlterTime { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置控制技能")]
        [DisplayName("自动时光护盾")]
        [Description("Cast TemporalShield if talented on Cooldown")]
        public bool AutoTemporalShield { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置控制技能")]
        [DisplayName("启用深度冻结")]
        [Description("Chose to enable/disable Deep Freeze")]
        public bool UseDeepFreeze { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置控制技能")]
        [DisplayName("启用冰霜之环")]
        [Description("Chose to enable/disable use of ring of frost")]
        public bool UseRingOfFrost { get; set; }

        [Setting]
        [DefaultValue(false)]
        [Category("设置杂项")]
        [DisplayName("启用闪现")]
        [Description("Chose to enable/disable automatic Blink")]
        public bool UseBlink { get; set; }

        [Setting]
        [DefaultValue(false)]
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
        [DefaultValue(false)]
        [Category("设置杂项")]
        [DisplayName("启用咒术护盾")]
        [Description("Chose to enable/disable if talented Incanter Ward on CD")]
        public bool UseIncenterWardOnCD { get; set; }

        [Setting]
        [DefaultValue(15)]
        [Category("设置杂项")]
        [DisplayName("寒冰屏障 生命值%")]
        [Description("Use Ice Block if hp lower than this value")]
        public int IceBlockHP { get; set; }

        [Setting]
        [DefaultValue(true)]
        [Category("设置杂项")]
        [DisplayName("启用寒冰屏障")]
        [Description("Chose to enable/disable automatic IceBlock")]
        public bool IceBlockUse { get; set; }

        [Setting]
        [DefaultValue(4)]
        [Category("设置循环")]
        [DisplayName("施放奥术飞弹最大奥术充能层数")]
        [Description("cast arcane missiles only if charges greater than this value: if you want cast missile everitime UP just put thus value to 0")]
        public int MissilesOnlyAtChargeMaiorThan { get; set; }  
     
        [Setting]
        [DefaultValue(true)]
        [Category("设置循环")]
        [DisplayName("奥术飞弹(两种特效触发)")]
        [Description("cast arcane missiles if two procs ignoring charges setting")]
        public bool AlwaysMissilesAtTwoProcs { get; set; } 
               

        [Setting]
        [DefaultValue(80)]
        [Category("设置法力值")]
        [DisplayName("使用法力宝石 法力值%")]
        [Description("Use Mana Gem if your mana % lower tan this value")]
        public int UseManaGemPercent { get; set; }

        [Setting]
        [DefaultValue(40)]
        [Category("设置法力值")]
        [DisplayName("唤醒 法力值%")]
        [Description("Use Evocation if talented and your mana % lower tan this value")]
        public int EvocationToRecMana { get; set; }
        
        [Setting]
        [DefaultValue(true)]
        [Category("设置法力值")]
        [DisplayName("停止施放奥术冲击(奥术充能满层")]
        [Description("Stop cast arcane blast if try casting at max charge")]
        public bool avoid_arcane_blast_at_max_charge { get; set; }
        

        [Setting]
        [DefaultValue(4)]
        [Category("设置法力值")]
        [DisplayName("使用奥术弹幕(奥术充能层数)")]
        [Description("Cast Barrage at chosen charges")]
        public int CastBarrageAtCharge { get; set; }

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
        [DisplayName("PauseKey")]
        [Description("enable/disable Routine")]
        public KingwowKeys PauseKey { get; set; }

        [Setting, DefaultValue(ModifierKeys.Alt)]
        [Category("设置热键")]
        [DisplayName("组合键")]
        [Description("Mod key for hotkey")]
        public ModifierKeys ModKey { get; set; }

        [Setting, DefaultValue(KingwowKeys.M)]
        [Category("设置热键")]
        [DisplayName("多目标DOT切换")]
        [Description("enable/disable Multidot")]
        public KingwowKeys MultidotKey { get; set; }

        [Setting, DefaultValue(KingwowKeys.A)]
        [Category("设置热键")]
        [DisplayName("暂停AOE")]
        [Description("enable/disable AvoidAOE function")]
        public KingwowKeys AvoidAOEKey { get; set; }

    }
}
