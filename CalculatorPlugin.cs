using DGP.Genshin;
using DGP.Genshin.Core.Plugins;
using DGP.Genshin.Service.Abstratcion;
using System;

[assembly: SnapGenshinPlugin]

namespace Injury.Calculator.Plugin
{
    /// <summary>
    /// 插件实例实现
    /// </summary>
    [ImportPage(typeof(CalculatorPage), "伤害计算器", "\uE94C")]
    public class CalculatorPlugin : IPlugin
    {
        public string Name => "伤害计算器";
        public string Description => "本插件用于根据角色属性面板粗略计算大致伤害，有些拥有独立乘区的角色可能计算结果会有较大偏差，实际伤害请以游戏内为准，本版本为先行版，可能存在一些问题，如发现或有建议请联系开发者";
        public string Author => "DawnFz（ねねだん）";
        public Version Version => new("1.0.2.1");
        public bool IsEnabled
        {
            get;
            set;
        }
    }
}