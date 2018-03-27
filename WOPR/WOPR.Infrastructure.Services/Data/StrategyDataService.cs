using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOPR.Domain.DataObjects.SimulationElements;

namespace WOPR.Infrastructure.Services.Data
{
    public class StrategyDataService : IStrategyDataService
    {
        public IEnumerable<ActorDO> GetRoles()
        {
            throw new NotImplementedException();
        }


        public IEnumerable<StrategyDO> GetStrategies(long prinicpalId, long targetId)
        {
            throw new NotImplementedException();
        }
    }

    public interface IStrategyDataService
    {
        IEnumerable<ActorDO> GetRoles();

        IEnumerable<StrategyDO> GetStrategies(long prinicpalId, long targetId);
    }
}
