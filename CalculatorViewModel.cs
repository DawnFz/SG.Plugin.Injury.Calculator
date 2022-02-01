using Microsoft.Toolkit.Mvvm.ComponentModel;
using Snap.Core.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Injury.Calculator.Plugin
{
    [ViewModel(InjectAs.Transient)]
    internal class CalculatorViewModel : ObservableObject
    {
        private IEnumerable<object> icons;

        public IEnumerable<object> Icons { get => icons; set => SetProperty(ref icons, value); }

        public CalculatorViewModel()
        {
            List<object>? list = new();
            ICollection<FontFamily>? families = Fonts.GetFontFamilies(@"C:\Windows\Fonts\segmdl2.ttf");
            foreach (FontFamily family in families)
            {
                ICollection<Typeface>? typefaces = family.GetTypefaces();
                foreach (Typeface typeface in typefaces)
                {
                    typeface.TryGetGlyphTypeface(out GlyphTypeface glyph);
                    IDictionary<int, ushort> characterMap = glyph.CharacterToGlyphMap;

                    foreach (KeyValuePair<int, ushort> kvp in characterMap)
                    {
                        list.Add(new { Glyph = (char)kvp.Key, Data = kvp.Key });
                    }
                }
            }
            icons = list;
            CountDamageCommand = new DelegateCommand { ExecuteAction = new Action<object>(CountDamage) };
            data = new();
        }
        private DatasModel _data;
        public DatasModel data
        {
            get => _data;
            set => SetProperty(ref _data, value);
        }

        private int _EffectIndex = 0;
        public int EffectIndex
        {
            get => _EffectIndex;
            set => SetProperty(ref _EffectIndex, value);
        }

        private int _TypeIndex = 0;
        public int TypeIndex
        {
            get => _TypeIndex;
            set => SetProperty(ref _TypeIndex, value);
        }

        private int _YDM;
        public int YDM { get => _YDM; set => SetProperty(ref _YDM, value); }
        private int _XDM;
        public int XDM { get => _XDM; set => SetProperty(ref _XDM, value); }
        private int _ZDM;
        public int ZDM { get => _ZDM; set => SetProperty(ref _ZDM, value); }

        public DelegateCommand CountDamageCommand { get; set; }
        private void CountDamage(object parameter)
        {
            double D_RR;
            double EM = 1 + ((278 * data.Elemental_Mystery) / (data.Elemental_Mystery + 1400)) / 100;//精通转换独立乘区系数
            double DMG = 1 + (data.CRIT_DMG / 100);//爆伤乘区
            double DEF = (data.Role_Level + 100) / (data.Monster_Level + 100 + (data.Monster_Level + 100) * (1 - (data.Reduce_Defense / 100)));//减防公式
            double D_M = data.Multiple / 100;//倍率百分比转化
            double D_EA = 1 + (data.Element_Addition / 100);//伤害加成增伤公式
            double ER = data.Charge_Efficiency * 0.25;
            if (data.Reduce_Resistance / 100 > 0.1)//怪物减抗是否为负判断
            {
                D_RR = 1 - (0.1 - (data.Reduce_Resistance / 100) / 2);
            }
            else
            {
                D_RR = 1 - (0.1 - (data.Reduce_Resistance / 100));
            }

            if (TypeIndex == 0 && EffectIndex == 0)//无增幅+无套装
            {
                YDM = (int)(data.Atks * DMG * D_EA * D_M * DEF * D_RR);
                XDM = (int)(data.Atks * D_EA * D_M * DEF * D_RR);
                
            }
            else if (TypeIndex == 0 && EffectIndex == 1)//无增幅+魔女4
            {
                YDM = (int)(data.Atks * DMG * (D_EA + 0.075) * D_M * DEF * D_RR);
                XDM = (int)(data.Atks * (D_EA + 0.075) * D_M * DEF * D_RR);

            }
            else if (TypeIndex == 0 && EffectIndex == 2)//无增幅+绝缘4
            {
                if (ER > 0.75)
                    ER = 0.75;
                YDM = (int)(data.Atks * DMG * (D_EA+ER) * D_M * DEF * D_RR);
                XDM = (int)(data.Atks * (D_EA+ER) * D_M * DEF * D_RR);
            }
            else if (TypeIndex == 1 && EffectIndex == 0)//1.5倍+无套装
            {
                YDM = (int)(data.Atks * DMG * (D_EA + 0.075) * D_M * DEF * D_RR * EM * 1.5);
                XDM = (int)(data.Atks * (D_EA + 0.075) * D_M * DEF * D_RR * EM * 1.5);
            }
            else if (TypeIndex == 1 && EffectIndex == 1)//1.5倍+魔女4
            {
                YDM = (int)(data.Atks * DMG * (D_EA + 0.075) * D_M * DEF * D_RR * (EM + 0.15) * 1.5);
                XDM = (int)(data.Atks * (D_EA + 0.075) * D_M * DEF * D_RR * (EM + 0.15) * 1.5);
            }
            else if (TypeIndex == 1 && EffectIndex == 2)//1.5倍+绝缘4
            {
                if (ER > 0.75)
                    ER = 0.75;
                YDM = (int)(data.Atks * DMG * (D_EA + ER) * D_M * DEF * D_RR*EM*1.5);
                XDM = (int)(data.Atks * (D_EA + ER) * D_M * DEF * D_RR*EM*1.5);
            }
            else if (TypeIndex == 2 && EffectIndex == 0)//2倍+无套装
            {
                YDM = (int)(data.Atks * DMG * (D_EA + 0.075) * D_M * DEF * D_RR * EM * 2);
                XDM = (int)(data.Atks * (D_EA + 0.075) * D_M * DEF * D_RR * EM * 2);
            }
            else if (TypeIndex == 2 && EffectIndex == 1)//2倍+魔女4
            {
                YDM = (int)(data.Atks * DMG * (D_EA + 0.075) * D_M * DEF * D_RR * (EM + 0.15) *2);
                XDM = (int)(data.Atks * (D_EA + 0.075) * D_M * DEF * D_RR * (EM + 0.15) * 2);
            }
            else if (TypeIndex == 2 && EffectIndex == 2)//2倍+绝缘4
            {
                if (ER > 0.75)
                    ER = 0.75;
                YDM = (int)(data.Atks * DMG * (D_EA + ER) * D_M * DEF * D_RR * EM * 2);
                XDM = (int)(data.Atks * (D_EA + ER) * D_M * DEF * D_RR * EM * 2);
            }
        }
    }
}
