using Microsoft.Practices.Unity;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WOPR.Infrastructure.Utilities.Logging;
using WOPR.UI.WPF.Common.Helpers;
using WOPR.UI.WPF.Simulation;
using WOPR.UI.WPF.Theatre;

namespace WOPR.Infrastructure
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override System.Windows.DependencyObject CreateShell()
        {
            return this.Container.TryResolve<Shell>();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
            ModuleCatalog.AddModule(new Prism.Modularity.ModuleInfo("TheatreModule", typeof(TheatreModule).AssemblyQualifiedName));
            ModuleCatalog.AddModule(new Prism.Modularity.ModuleInfo("SimulationModule", typeof(SimulationModule).AssemblyQualifiedName));
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            App.Current.MainWindow = (Window)this.Shell;
            App.Current.MainWindow.Show();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            if (!Container.IsRegistered<IWOPRLogger>())
            {
                Container.RegisterType<IWOPRLogger, WOPRLogger>(new PerResolveLifetimeManager());
            }

            if (!Container.IsRegistered<IMappingHelper>())
            {
                Container.RegisterType<IMappingHelper, MappingHelper>(new PerResolveLifetimeManager());
            }
        }
    }
}
