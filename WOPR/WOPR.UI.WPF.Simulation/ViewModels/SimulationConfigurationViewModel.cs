using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using WOPR.Domain.DataObjects.SimulationElements;
using WOPR.Infrastructure.Services.Data;
using WOPR.UI.WPF.Common.Messages.Simulation;
using WOPR.UI.WPF.Common.ViewModels;

namespace WOPR.UI.WPF.Simulation.ViewModels
{
    public class SimulationConfigurationViewModel : ViewModelBase, ISimulationConfigurationViewModel
    {
        private readonly IStrategyDataService m_DataService;

        private readonly IEventAggregator m_EventAggregator;

        private ObservableCollection<ActorDO> m_Roles;

        private ListCollectionView m_AdversaryRoles;

        private ActorDO m_PrincipalRole;

        private ActorDO m_AdversaryRole;

        private ObservableCollection<StrategyDO> m_Strategies;

        private StrategyDO m_SelectedStrategy;

        public ObservableCollection<ActorDO> Roles
        {
            get
            {
                return m_Roles;
            }
            private set
            {
                m_Roles = value;
                OnPropertyChanged("Roles");
            }
        }

        public ActorDO PrincipalRole
        {
            get
            {
                return m_PrincipalRole;
            }
            set
            {
                m_PrincipalRole = value;
                OnPropertyChanged("PrincipleRole");
                AdversaryRoles.Refresh();
                SetStrategies();
            }
        }

        public ListCollectionView AdversaryRoles
        {
            get
            {
                return m_AdversaryRoles;
            }
            private set
            {
                m_AdversaryRoles = value;
                OnPropertyChanged("AdversaryRoles");
            }
        }

        public ActorDO AdversaryRole
        {
            get
            {
                return m_AdversaryRole;
            }
            set
            {
                m_AdversaryRole = value;
                OnPropertyChanged("Adversary");
                SetStrategies();
            }
        }

        public ObservableCollection<StrategyDO> Strategies
        {
            get
            {
                return m_Strategies;
            }
            private set
            {
                m_Strategies = value;
                OnPropertyChanged("Strategies");
            }
        }

        public StrategyDO SelectedStrategy
        {
            get
            {
                return m_SelectedStrategy;
            }
            set
            {
                m_SelectedStrategy = value;
                OnPropertyChanged("SelectedStrategy");
            }
        }

        public ICommand StartSimulationCommand { get; private set; }

        public SimulationConfigurationViewModel(IStrategyDataService dataService, IEventAggregator eventAggregator)
        {
            this.m_DataService = dataService;
            this.m_EventAggregator = eventAggregator;
            Initialise();
        }

        private void Initialise()
        {
            var roles = m_DataService.GetRoles();
            ObservableCollection<ActorDO> observableRoles = new ObservableCollection<ActorDO>(roles);
            this.Roles = observableRoles;

            AdversaryRoles = new ListCollectionView(observableRoles);
            AdversaryRoles.Filter += (o) =>
            {
                if (o != null)
                {
                    return o != PrincipalRole;
                }
                return false;
            };

            StartSimulationCommand = new DelegateCommand(() => Execute());
        }

        private void SetStrategies()
        {
            if (PrincipalRole != null && AdversaryRole != null)
            {
                var strategies = m_DataService.GetStrategies(PrincipalRole.Id, AdversaryRole.Id);
                this.Strategies = new ObservableCollection<StrategyDO>(strategies);
            }
        }

        public void Execute()
        {
            if (SelectedStrategy == null)
            {
                throw new NullReferenceException("Selected strategy is null");
            }
            m_EventAggregator.GetEvent<SimulationStartEvent>().Publish(SelectedStrategy);
        }
    }

    public interface ISimulationConfigurationViewModel
    {
        ObservableCollection<ActorDO> Roles { get; }

        ActorDO PrincipalRole { get; set; }

        ListCollectionView AdversaryRoles { get; }

        ActorDO AdversaryRole { get; set; }

        ObservableCollection<StrategyDO> Strategies { get; }

        StrategyDO SelectedStrategy { get; set; }

        void Execute();
    }
}
