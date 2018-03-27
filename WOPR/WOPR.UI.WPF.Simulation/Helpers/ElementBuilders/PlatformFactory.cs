using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOPR.Domain.DataObjects.SimulationElements.Assets;
using WOPR.Domain.DataObjects.SimulationElements.Assets.Platforms;
using WOPR.Domain.DataObjects.SimulationElements.Assets.Platforms.Offensive;
using WOPR.UI.WPF.Common.Messages;
using WOPR.UI.WPF.Simulation.Models.Platforms;
using WOPR.UI.WPF.Simulation.Models.Platforms.Offensive;

namespace WOPR.UI.WPF.Simulation.Helpers.ElementBuilders
{
    public class PlatformModelFactory : IPlatformModelFactory
    {
        public IPlatformModel GetPlatformModel(PlatformBaseDO platform, IMessageBus messageBus)
        {
            if (platform == null)
            {
                throw new ArgumentNullException("The platform cannot be null");
            }
            if (messageBus == null)
            {
                throw new ArgumentNullException("The message bus cannot be null");
            }

            if (platform is SiloDO)
            {
                return new SiloModel(platform as SiloDO);
            }
            else if (platform is BallisticMissileSubmarineDO)
            {
                return new BallisticMissileSubmarineModel(platform as BallisticMissileSubmarineDO);
            }
            else if (platform is StrategicBomberDO)
            {
                return new StrategicBomberModel(platform as StrategicBomberDO);
            }
            throw new NotImplementedException();
        }
    }

    public interface IPlatformModelFactory
    {
        IPlatformModel GetPlatformModel(PlatformBaseDO platform, IMessageBus messageBus);
    }
}
