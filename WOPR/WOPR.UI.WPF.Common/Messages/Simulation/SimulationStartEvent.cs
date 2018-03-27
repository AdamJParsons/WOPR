using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOPR.UI.WPF.Common.Messages.Simulation
{
    public class SimulationStartEvent : PubSubEvent<WOPR.Domain.DataObjects.SimulationElements.StrategyDO>
    {
    }
}
