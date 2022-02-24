using DGP.Genshin.Core.Plugins;
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
        public string Name => "伤害计算器 - 1.0.3";
        public string Description => "本插件用于根据角色属性面板粗略计算大致伤害，有些拥有独立乘区的角色可能计算结果会有较大偏差，实际伤害请以游戏内为准，本版本为先行版，可能存在一些问题，如发现或有建议请联系开发者";
        public string Author => "DawnFz（ねねだん）";
        public Version Version => new("1.0.3.0");

        /// <summary>
        /// 可以使用 Snap Genshin 内置的设置服务储存
        /// 也可以自己实现储存逻辑
        /// </summary>
        public bool IsEnabled
        {
            get;
            set;
        }
    }
}