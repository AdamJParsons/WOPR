using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOPR.Domain.DataObjects.SimulationElements;

namespace WOPR.UI.WPF.Simulation.Models
{
    public class RoleDomainFactory : IRoleDomainFactory
    {
        private readonly IUnityContainer m_Unity;

        public RoleDomainFactory(IUnityContainer unity)
        {
            this.m_Unity = unity;
        }

        public IRoleDomain GetRoleDomain(ActorDO role)
        {
            IRoleDomain domain = m_Unity.Resolve<IRoleDomain>();
            domain.SetData(role);
            return domain;
        }
    }

    public interface IRoleDomainFactory
    {
        IRoleDomain GetRoleDomain(ActorDO role);
    }
}
