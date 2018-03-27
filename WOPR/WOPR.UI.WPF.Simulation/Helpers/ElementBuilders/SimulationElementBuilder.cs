using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOPR.Domain.DataObjects.SimulationElements;
using WOPR.Domain.DataObjects.SimulationElements.Assets;
using WOPR.Domain.DataObjects.SimulationElements.Assets.Platforms;
using WOPR.Domain.DataObjects.SimulationElements.Assets.Platforms.Offensive;
using WOPR.UI.WPF.Common.Messages;
using WOPR.UI.WPF.Simulation.Helpers.ElementBuilders;
using WOPR.UI.WPF.Simulation.Models.Platforms;
using WOPR.UI.WPF.Simulation.Models.Platforms.Offensive;

namespace WOPR.UI.WPF.Simulation.Models
{
    public class SimulationElementBuilder : ISimulationElementBuilder
    {
        private readonly IMessageBus m_MessageBus;

        private readonly IPlatformModelFactory m_OffensivePlatformFactory;

        // For the Actor, we will have:
        // 1 Controller (NMCC)
        // n Commanders (Installation)
        // m Platforms  (Silo, Bomber, Submarine)
        // p Weapons

        // We will also have geographical assets
        // x military assets    (Force)
        // y civilian assets    (Value)

        // The controller and the commanders will have a link to a fixed geographical asset
        // Silos will also be linked to a fixed geographical asset
        // Bombers, Submarines etc are real world geographical entities but are not fixed.

        // So:
        /*  ACTOR
         *  -------------
         *  ControllerDO Controller
         *      List<CommanderDO> Commanders
         *          List<Platform> Platforms
         *              List<Asset> Weapons
         *  
         *  
         *  List<Asset> Locations (excluding moveable entities)
         */

        public SimulationElementBuilder(IMessageBus messageBus, IPlatformModelFactory offensivePlatformFactory)
        {
            this.m_MessageBus = messageBus;
            this.m_OffensivePlatformFactory = offensivePlatformFactory;
        }

        #region Builder Methods

        public IControllerModel BuildController(ControllerDO controller)
        {
            if (controller == null)
            {
                throw new ArgumentNullException("The controller cannot be null");
            }
            ControllerModel controllerModel = new ControllerModel(controller, m_MessageBus);
            return controllerModel;
        }

        public IEnumerable<CommanderModel> BuildCommanders(IEnumerable<CommanderDO> commanders)
        {
            if (commanders == null)
            {
                throw new ArgumentNullException("The commanders collection cannot be null");
            }
            List<CommanderModel> commanderModels = new List<CommanderModel>();
            foreach (var commander in commanders)
            {
                IPlatformModel platform = GetPlatform(commander.Platform);
                CommanderModel model = new CommanderModel(commander, platform, m_MessageBus);
                commanderModels.Add(model);
            }
            return commanderModels;
        }

        public IEnumerable<IPlatformModel> BuildOffensivePlatforms(IEnumerable<OffensivePlatformBaseDO> offensiveAssets)
        {
            if (offensiveAssets == null)
            {
                throw new ArgumentNullException("Offensive assets collection cannot be null");
            }
            List<IPlatformModel> platforms = new List<IPlatformModel>();
            foreach (var platform in offensiveAssets)
            {
                var model = m_OffensivePlatformFactory.GetPlatformModel(platform, m_MessageBus);
                platforms.Add(model);
            }
            return platforms;
        }

        private IPlatformModel GetPlatform(PlatformBaseDO platform)
        {
            var model = m_OffensivePlatformFactory.GetPlatformModel(platform, m_MessageBus);
            return model;
        }

        public void BuildAllElements(ActorDO Actor)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public interface ISimulationElementBuilder
    {
        IControllerModel BuildController(ControllerDO controller);

        IEnumerable<CommanderModel> BuildCommanders(IEnumerable<CommanderDO> commanders);

        IEnumerable<IPlatformModel> BuildOffensivePlatforms(IEnumerable<OffensivePlatformBaseDO> offensiveAssets);

        void BuildAllElements(ActorDO Actor);
    } 
}
