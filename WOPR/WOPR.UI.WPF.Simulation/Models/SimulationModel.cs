using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOPR.Domain.DataObjects.SimulationElements;
using WOPR.Domain.DataObjects.SimulationElements.Assets;
using WOPR.UI.WPF.Common.Messages;

namespace WOPR.UI.WPF.Simulation.Models
{
    public class SimulationModel : ModelBase<SimulationDO>, ISimulationModel
    {
        private readonly IRoleDomainFactory m_RoleDomainFactory;

        private SimulationStatus m_Status;

        private IRoleDomain m_PrincipalRole;

        private IRoleDomain m_AdversaryRole;

        private DateTime? m_SimulationStartTime;

        public SimulationStatus Status
        {
            get
            {
                return m_Status;
            }
            private set
            {
                m_Status = value;
                OnPropertyChanged("Status");
            }
        }

        public IRoleDomain PrincipalRole
        {
            get
            {
                return m_PrincipalRole;
            }
            private set
            {
                m_PrincipalRole = value;
                OnPropertyChanged("PrincipalRole");
            }
        }

        public IRoleDomain AdversaryRole
        {
            get
            {
                return m_AdversaryRole;
            }
            private set
            {
                m_AdversaryRole = value;
                OnPropertyChanged("AdversaryRole");
            }
        }

        public DateTime? SimulationStartTime
        {
            get
            {
                return m_SimulationStartTime;
            }
            private set
            {
                m_SimulationStartTime = value;
                OnPropertyChanged("SimulationStartTime");
            }
        }

        public TimeSpan? Elapsed
        {
            get
            {
                if (SimulationStartTime.HasValue)
                {
                    return DateTime.Now.Subtract(SimulationStartTime.Value);
                }
                return null;
            }
        }

        public SimulationModel(SimulationDO simulation, IRoleDomainFactory roleDomainFactory)
            : base(simulation)
        {
            this.m_RoleDomainFactory = roleDomainFactory;
        }

        public void Execute(StrategyDO strategy)
        {
            if (strategy == null)
            {
                throw new ArgumentNullException("Strategy cannot be null");
            }
            this.Status = SimulationStatus.Started;

            SetRoles(strategy);

            SimulationStartTime = DateTime.Now;
        }

        private void SetRoles(StrategyDO strategy)
        {
            // The role domains contain all the simulation elements for a specific role
            // i.e. Command and control structures, assets and infrastructure.
            IRoleDomain primaryRoleDomain = m_RoleDomainFactory.GetRoleDomain(strategy.Role);
            this.PrincipalRole = primaryRoleDomain;

            IRoleDomain adversaryRoleDomain = m_RoleDomainFactory.GetRoleDomain(strategy.TargetRole);
            this.AdversaryRole = adversaryRoleDomain;
        }

        public void Terminate()
        {
            this.Status = SimulationStatus.Terminated;
            this.SimulationStartTime = null;
            this.PrincipalRole = null;
            this.AdversaryRole = null;
        }
    }

    public enum SimulationStatus
    {
        NOTSET = 0,

        Started = 1,

        Finished = 2,

        Terminated = 3
    }

    public interface ISimulationModel : IModel<SimulationDO>
    {
        SimulationStatus Status { get; }

        IRoleDomain PrincipalRole { get; }

        IRoleDomain AdversaryRole { get; }

        DateTime? SimulationStartTime { get; }

        TimeSpan? Elapsed { get; }

        void Execute(StrategyDO strategy);

        void Terminate();
    }
}
