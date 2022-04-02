using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Snap.Core.DependencyInjection;
using System.Windows.Input;

namespace Injury.Calculator.Plugin
{
    [ViewModel(InjectAs.Transient)]
    public class CalculatorViewModel : ObservableObject
    {
        public CalculatorViewModel()
        {
            CountDamageCommand = new RelayCommand(CountDamage);
            Data = new();
        }
        private DatasModel? _data;
        public DatasModel Data
        {
            get => _data!;
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

        public ICommand CountDamageCommand { get; set; }
        private void CountDamage()
        {
            double xDM = 0, yDM = 0;
            double D_RR;
            double EM = 1 + ((278 * Data.Elemental_Mystery) / (Data.Elemental_Mystery + 1400)) / 100;//精通转换独立乘区系数
            double DMG = 1 + (Data.CRIT_DMG / 100);//爆伤乘区
            double DEF = (Data.Role_Level + 100) / (Data.Monster_Level + 100 + (Data.Monster_Level + 100) * (1 - (Data.Reduce_Defense / 100)));//减防公式
            double D_M = Data.Multiple / 100;//倍率百分比转化
            double D_EA = 1 + (Data.Element_Addition / 100);//伤害加成增伤公式
            double ER = Data.Charge_Efficiency * 0.25;
            if (Data.Reduce_Resistance / 100 > 0.1)//怪物减抗是否为负判断
            {
                D_RR = 1 - (0.1 - (Data.Reduce_Resistance / 100) / 2);
            }
            else
            {
                D_RR = 1 - (0.1 - (Data.Reduce_Resistance / 100));
            }

            if (TypeIndex == 0 && EffectIndex == 0)//无增幅+无套装
            {
                yDM = (Data.Atks * DMG * D_EA * D_M * DEF * D_RR);
                xDM = (Data.Atks * D_EA * D_M * DEF * D_RR);

            }
            else if (TypeIndex == 0 && EffectIndex == 1)//无增幅+魔女4
            {
                yDM = (Data.Atks * DMG * (D_EA + 0.075) * D_M * DEF * D_RR);
                xDM = (Data.Atks * (D_EA + 0.075) * D_M * DEF * D_RR);

            }
            else if (TypeIndex == 0 && EffectIndex == 2)//无增幅+绝缘4
            {
                if (ER > 0.75)
                {
                    ER = 0.75;
                }

                yDM = (Data.Atks * DMG * (D_EA + ER) * D_M * DEF * D_RR);
                xDM = (Data.Atks * (D_EA + ER) * D_M * DEF * D_RR);
            }
            else if (TypeIndex == 1 && EffectIndex == 0)//1.5倍+无套装
            {
                yDM = (Data.Atks * DMG * (D_EA + 0.075) * D_M * DEF * D_RR * EM * 1.5);
                xDM = (Data.Atks * (D_EA + 0.075) * D_M * DEF * D_RR * EM * 1.5);
            }
            else if (TypeIndex == 1 && EffectIndex == 1)//1.5倍+魔女4
            {
                yDM = (Data.Atks * DMG * (D_EA + 0.075) * D_M * DEF * D_RR * (EM + 0.15) * 1.5);
                xDM = (Data.Atks * (D_EA + 0.075) * D_M * DEF * D_RR * (EM + 0.15) * 1.5);
            }
            else if (TypeIndex == 1 && EffectIndex == 2)//1.5倍+绝缘4
            {
                if (ER > 0.75)
                {
                    ER = 0.75;
                }

                yDM = (Data.Atks * DMG * (D_EA + ER) * D_M * DEF * D_RR * EM * 1.5);
                xDM = (Data.Atks * (D_EA + ER) * D_M * DEF * D_RR * EM * 1.5);
            }
            else if (TypeIndex == 2 && EffectIndex == 0)//2倍+无套装
            {
                yDM = (Data.Atks * DMG * (D_EA + 0.075) * D_M * DEF * D_RR * EM * 2);
                xDM = (Data.Atks * (D_EA + 0.075) * D_M * DEF * D_RR * EM * 2);
            }
            else if (TypeIndex == 2 && EffectIndex == 1)//2倍+魔女4
            {
                yDM = (Data.Atks * DMG * (D_EA + 0.075) * D_M * DEF * D_RR * (EM + 0.15) * 2);
                xDM = (Data.Atks * (D_EA + 0.075) * D_M * DEF * D_RR * (EM + 0.15) * 2);
            }
            else if (TypeIndex == 2 && EffectIndex == 2)//2倍+绝缘4
            {
                if (ER > 0.75)
                {
                    ER = 0.75;
                }

                yDM = (Data.Atks * DMG * (D_EA + ER) * D_M * DEF * D_RR * EM * 2);
                xDM = (Data.Atks * (D_EA + ER) * D_M * DEF * D_RR * EM * 2);
            }
            if (yDM > 9999999)
            {
                yDM = 9999999;
            }
            else if (yDM < 1)
            {
                yDM = 1;
            }

            if (xDM > 9999999)
            {
                xDM = 9999999;
            }
            else if (xDM < 1)
            {
                xDM = 1;
            }

            XDM = (int)xDM;
            YDM = (int)yDM;
        }
    }
}
