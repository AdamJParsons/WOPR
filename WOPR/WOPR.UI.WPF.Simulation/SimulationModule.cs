using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOPR.UI.WPF.Simulation.ViewModels;
using WOPR.UI.WPF.Simulation.Views;

namespace WOPR.UI.WPF.Simulation
{
    public class SimulationModule : IModule
    {
        private readonly IRegionManager m_RegionManager;

        private readonly IUnityContainer m_Unity;

        public SimulationModule(IRegionManager regionManager, IUnityContainer unity)
        {
            this.m_RegionManager = regionManager;
            this.m_Unity = unity;
        }

        public void Initialize()
        {
            if (!m_Unity.IsRegistered<ISimulationConfigurationViewModel>())
            {
                m_Unity.RegisterType<ISimulationConfigurationViewModel, SimulationConfigurationViewModel>();
            }

            m_RegionManager.RegisterViewWithRegion("FooterRegion", typeof(SimulationStatusView));
        }
    }
}
