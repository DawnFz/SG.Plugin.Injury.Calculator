namespace Injury.Calculator.Plugin
{
    public class DatasModel
    {
        public double Atks { get; set; }//攻击力
        public double CRIT_Rate { get; set; }//暴击率
        public double CRIT_DMG { get; set; }//暴击伤害
        public double Elemental_Mystery { get; set; }//元素精通
        public double Element_Addition { get; set; }//元素伤害、元素增伤
        public double Reduce_Resistance { get; set; }//减抗
        public double Reduce_Defense { get; set; }//减防
        public double Role_Level { get; set; }//角色等级
        public double Monster_Level { get; set; }//怪物等级
        public double Multiple { get; set; }//倍率
        public double Charge_Efficiency { get; set; }//元素充能效率
    }
}
