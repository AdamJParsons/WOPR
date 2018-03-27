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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WOPR.UI.WPF.Theatre.ViewModels;

namespace WOPR.UI.WPF.Theatre.Views
{
    /// <summary>
    /// Interaction logic for WorldView.xaml
    /// </summary>
    public partial class WorldView : UserControl
    {
        private IWorldViewModel m_ViewModel;

        [Dependency]
        public IWorldViewModel ViewModel
        {
            get
            {
                return m_ViewModel;
            }
            set
            {
                m_ViewModel = value;
                this.DataContext = value;
                if (value != null)
                {
                    value.World = this.PART_World;
                }
            }
        }

        public WorldView()
        {
            InitializeComponent();
        }
    }
}
