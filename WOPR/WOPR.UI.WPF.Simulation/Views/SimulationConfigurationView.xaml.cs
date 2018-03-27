using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WOPR.UI.WPF.Simulation.ViewModels;

namespace WOPR.UI.WPF.Simulation.Views
{
    /// <summary>
    /// Interaction logic for SimulationConfigurationView.xaml
    /// </summary>
    public partial class SimulationConfigurationView : Window
    {
        private ISimulationConfigurationViewModel m_ViewModel;

        [Dependency]
        public ISimulationConfigurationViewModel ViewModel
        {
            get
            {
                return m_ViewModel;
            }
            set
            {
                m_ViewModel = value;
                DataContext = value;
            }
        }

        public SimulationConfigurationView()
        {
            InitializeComponent();
        }
    }
}
