using Microsoft.Practices.Unity;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOPR.Domain.DataObjects.SimulationElements;
using WOPR.UI.WPF.Common.Messages.Simulation;
using WOPR.UI.WPF.Common.ViewModels;
using WOPR.UI.WPF.Simulation.Models;

namespace WOPR.UI.WPF.Simulation.ViewModels
{
    public class SimulationHostViewModel : ViewModelBase, ISimulationHostViewModel
    {
        private readonly IEventAggregator m_EventAggregator;

        private readonly IRoleDomainFactory m_RoleDomainFactory;

        private SubscriptionToken m_SimulationStartToken;

        private ISimulationModel m_Simulation;

        public ISimulationModel Simulation
        {
            get
            {
                return m_Simulation;
            }
            private set
            {
                m_Simulation = value;
                OnPropertyChanged("Simulation");
            }
        }

        public SimulationHostViewModel(IEventAggregator eventAggregator, IRoleDomainFactory roleDomainFactory)
        {
            m_EventAggregator = eventAggregator;
            m_RoleDomainFactory = roleDomainFactory;
            Initialise();
        }

        private void Initialise()
        {
            var startEvent = m_EventAggregator.GetEvent<SimulationStartEvent>();
            if (!startEvent.Contains(m_SimulationStartToken))
            {
                startEvent.Subscribe((strategy) => OnSimulationStarted(strategy), ThreadOption.UIThread, false);
            }
        }

        private void OnSimulationStarted(StrategyDO strategy)
        {
            StartSimulation(strategy);
        }

        public void StartSimulation(StrategyDO strategy)
        {
            if (strategy == null)
            {
                throw new ArgumentNullException("Strategy must not be null");
            }

            if (Simulation != null)
            {
                TerminateExistingSimulation(Simulation);
            }

            SimulationDO simulation = new SimulationDO();
            SimulationModel model = new SimulationModel(simulation, m_RoleDomainFactory);
            this.Simulation = model;
            model.Execute(strategy);
        }

        public void StopSimulation()
        {
            if (Simulation == null || Simulation.Status != SimulationStatus.Started)
            {
                throw new InvalidOperationException("The simulation is not in a started state");
            }

            TerminateExistingSimulation(this.Simulation);
        }

        private void TerminateExistingSimulation(ISimulationModel simulation)
        {
            simulation.Terminate();
        }
    }

    public interface ISimulationHostViewModel
    {
        ISimulationModel Simulation { get; }

        void StartSimulation(StrategyDO strategy);

        void StopSimulation();
    }
}
