using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOPR.UI.WPF.Theatre.ViewModels;
using WOPR.UI.WPF.Theatre.Views;

namespace WOPR.UI.WPF.Theatre
{
    public class TheatreModule : IModule
    {
        private readonly IRegionManager m_RegionManager;

        private readonly IUnityContainer m_Unity;

        public TheatreModule(IRegionManager regionManager, IUnityContainer unity)
        {
            this.m_RegionManager = regionManager;
            this.m_Unity = unity;
        }

        public void Initialize()
        {
            if (!m_Unity.IsRegistered<IWorldViewModel>())
            {
                m_Unity.RegisterType<IWorldViewModel, WorldViewModel>();
            }

            m_RegionManager.RegisterViewWithRegion("MainContentRegion", typeof(WorldView));
        }
    }
}
