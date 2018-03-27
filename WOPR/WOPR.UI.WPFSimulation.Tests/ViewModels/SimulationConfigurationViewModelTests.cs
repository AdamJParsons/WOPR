using Moq;
using NUnit.Framework;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOPR.Domain.DataObjects.SimulationElements;
using WOPR.Infrastructure.Services.Data;
using WOPR.UI.WPF.Common.Messages.Simulation;
using WOPR.UI.WPF.Simulation.ViewModels;

namespace WOPR.UI.WPFSimulation.Tests.ViewModels
{
    [TestFixture]
    public class SimulationConfigurationViewModelTests
    {
        private ISimulationConfigurationViewModel m_Model;

        private Mock<IEventAggregator> eventAggregator;

        private Mock<SimulationStartEvent> mockSimulationStartEvent;

        [SetUp]
        public void Setup()
        {
            Mock<IStrategyDataService> dataServiceMock = new Moq.Mock<IStrategyDataService>();
            Mock<IEventAggregator> mockAggregator = new Mock<IEventAggregator>();
            Mock<SimulationStartEvent> mockStartEvent = new Mock<SimulationStartEvent>();
            mockAggregator.Setup<SimulationStartEvent>(x => x.GetEvent<SimulationStartEvent>()).Returns(mockStartEvent.Object);
            this.mockSimulationStartEvent = mockStartEvent;
            eventAggregator = mockAggregator;

            // Create two actors
            List<ActorDO> allActors = new List<ActorDO>();
            for (int i = 1; i <= 2; i++)
            {
                ActorDO actor = new ActorDO { Id = i, Name = string.Format("Actor {0}", i.ToString()) };
                allActors.Add(actor);
            }

            List<StrategyDO> allStrategies = new List<StrategyDO>();
            int ctr = 0;
            foreach (var actor in allActors)
            {
                var otherActors = allActors.Where(x => x != actor);
                foreach (var target in otherActors)
                {
                    ctr++;
                    StrategyDO strategy = new StrategyDO 
                    { 
                        Id = ctr, 
                        Role = actor, 
                        TargetRole = target,
                        Name = string.Format("Strategy for {0} targeting {1}", actor.Name, target.Name) 
                    };

                    allStrategies.Add(strategy);
                }
            }

            dataServiceMock.Setup<IEnumerable<ActorDO>>(x => x.GetRoles()).Returns(allActors);
            dataServiceMock.Setup<IEnumerable<StrategyDO>>(x => x.GetStrategies(1, 2)).Returns(allStrategies.Where(x => x.Role.Id == 1 && x.TargetRole.Id == 2));
            ISimulationConfigurationViewModel model = new SimulationConfigurationViewModel(dataServiceMock.Object, mockAggregator.Object);
            this.m_Model = model;
        }

        [Test]
        public void SimulationConfigurationViewModel_UT001_UserCanChoosePrincipalRole()
        {
            Assert.NotNull(m_Model.Roles);
            Assert.IsNotEmpty(m_Model.Roles);
        }

        [Test]
        public void SimulationConfigurationViewModel_UT002_WhenPrincipalRoleSelected_UserCanChooseAdversaryRole()
        {
            m_Model.PrincipalRole = m_Model.Roles.First();

            Assert.IsNotNull(m_Model.AdversaryRoles);
            Assert.IsNotEmpty(m_Model.AdversaryRoles);
        }

        [Test]
        public void SimulationConfigurationViewModel_UT003_WhenPrincipalRoleSelected_UserCanNotChooseTheirOwnRoleAsAdversaryRole()
        {
            var actorA = m_Model.Roles.Single(x => x.Name == "Actor 1");
            m_Model.PrincipalRole = actorA;

            var adversaryActorA = m_Model.AdversaryRoles.Cast<ActorDO>().SingleOrDefault(x => x == actorA);

            Assert.IsNull(adversaryActorA);
        }

        [Test]
        public void SimulationConfigurationViewModel_UT004_WhenPrincipalRoleAndAdversaryRoleSelected_UserCanChooseStrategy()
        {
            var actorA = m_Model.Roles.Single(x => x.Name == "Actor 1");
            var actorB = m_Model.Roles.Single(x => x.Name == "Actor 2");
            m_Model.PrincipalRole = actorA;
            m_Model.AdversaryRole = actorB;

            Assert.IsNotNull(m_Model.Strategies);
            Assert.IsNotEmpty(m_Model.Strategies);
        }

        [Test]
        public void SimulationConfigurationViewModel_UT005_WhenPrincipalRoleAndAdversaryRoleSelected_StrategiesAvailableTargetOnlyTheSelectedAdversaryRole()
        {
            var actorA = m_Model.Roles.Single(x => x.Name == "Actor 1");
            var actorB = m_Model.Roles.Single(x => x.Name == "Actor 2");
            m_Model.PrincipalRole = actorA;
            m_Model.AdversaryRole = actorB;

            foreach (var strategy in m_Model.Strategies)
            {
                Assert.AreEqual(actorB, strategy.TargetRole);
            }
        }

        [Test]
        public void SimulationConfigurationViewModel_UT006_Execute_WhenNullStrategy_ThrowsNullReferenceException()
        {
            var actorA = m_Model.Roles.Single(x => x.Name == "Actor 1");
            var actorB = m_Model.Roles.Single(x => x.Name == "Actor 2");
            m_Model.PrincipalRole = actorA;
            m_Model.AdversaryRole = actorB;

            Assert.Throws<NullReferenceException>(new TestDelegate(() => m_Model.Execute()));
        }

        [Test]
        public void SimulationConfigurationViewModel_UT007_Execute_WhenStrategySet_RaisesStrategyStartMessage()
        {
            var actorA = m_Model.Roles.Single(x => x.Name == "Actor 1");
            var actorB = m_Model.Roles.Single(x => x.Name == "Actor 2");
            m_Model.PrincipalRole = actorA;
            m_Model.AdversaryRole = actorB;

            m_Model.SelectedStrategy = m_Model.Strategies.First();

            m_Model.Execute();
            this.mockSimulationStartEvent.Verify(x => x.Publish(m_Model.SelectedStrategy));
        }
    }
}
