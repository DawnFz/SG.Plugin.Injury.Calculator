using Snap.Core.DependencyInjection;
using SystemPage = System.Windows.Controls.Page;

namespace Injury.Calculator.Plugin
{
    [View(InjectAs.Transient)]
    public partial class CalculatorPage : SystemPage
    {
        public CalculatorPage(CalculatorViewModel vm)
        {
            DataContext = vm;
            InitializeComponent();
        }
    }
}
