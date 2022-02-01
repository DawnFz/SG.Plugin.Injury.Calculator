using DGP.Genshin;

namespace Injury.Calculator.Plugin
{
    public partial class CalculatorPage : System.Windows.Controls.Page
    {
        public CalculatorPage()
        {
            DataContext = App.AutoWired<CalculatorViewModel>();
            InitializeComponent();
        }
    }
}
