using DGP.Genshin.Core.Plugins;
using System;
using System.Windows.Input;

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
        public string Description => "用于根据角色属性面板粗略计算大致伤害，拥有独立乘区的角色可能计算结果会有较大偏差，实际伤害以游戏内为准，如发现或有建议请联系开发者";
        public string Author => "DawnFz（ねねだん）";
        public Version Version => new("1.0.5.0");

        /// <summary>
        /// 可以使用 Snap Genshin 内置的设置服务储存
        /// 也可以自己实现储存逻辑
        /// </summary>
        public bool IsEnabled
        {
            get;
        }
        #region IPlugin2
        public string? DetailLink
        {
            get => "https://github.com/DawnFz/SG.Plugin.Injury.Calculator";
        }

        public bool IsSettingSupported
        {
            get => false;
        }

        public ICommand SettingCommand
        {
            get => null!;
        }
        #endregion
    }
}