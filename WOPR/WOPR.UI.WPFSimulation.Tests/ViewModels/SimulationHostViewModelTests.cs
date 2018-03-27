using Moq;
using NUnit.Framework;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOPR.Domain.DataObjects.SimulationElements;
using WOPR.UI.WPF.Common.Messages.Simulation;
using WOPR.UI.WPF.Simulation.Models;
using WOPR.UI.WPF.Simulation.ViewModels;

namespace WOPR.UI.WPFSimulation.Tests.ViewModels
{
    [TestFixture]
    public class SimulationHostViewModelTests
    {
        private ISimulationHostViewModel m_Model;

        private Mock<IEventAggregator> eventAggregator;

        private Mock<SimulationStartEvent> mockSimulationStartEvent;

        [SetUp]
        public void Setup()
        {
            Mock<IEventAggregator> mockAggregator = new Mock<IEventAggregator>();
            Mock<SimulationStartEvent> mockStartEvent = new Mock<SimulationStartEvent>();
            mockAggregator.Setup<SimulationStartEvent>(x => x.GetEvent<SimulationStartEvent>()).Returns(mockStartEvent.Object);
            IRoleDomainFactory mockRoleDomainFactory = Mock.Of<IRoleDomainFactory>();
            this.mockSimulationStartEvent = mockStartEvent;
            eventAggregator = mockAggregator;

            ISimulationHostViewModel model = new SimulationHostViewModel(eventAggregator.Object, mockRoleDomainFactory);
            m_Model = model;
        }

        [Test]
        public void SimulationHostViewModel_UT001_StartSimulation_WhenPassedValidStrategy_NewSimulationModelIsCreated()
        {
            StrategyDO strategy = new StrategyDO();
            m_Model.StartSimulation(strategy);

            Assert.IsNotNull(m_Model.Simulation);
        }

        [Test]
        public void SimulationHostViewModel_UT002_StartSimulation_WhenPassedNullStrategy_ArgumentNullExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(new TestDelegate(() => m_Model.StartSimulation(null)));
        }

        [Test]
        public void SimulationHostViewModel_UT003_StartSimulation_WhenPassedValidStrategy_ExistingSimulationIsTerminated()
        {
            StrategyDO originalStrategy = new StrategyDO();
            // Start a simulation
            m_Model.StartSimulation(originalStrategy);
            ISimulationModel originalSimulation = m_Model.Simulation;

            // Start a new simulation
            StrategyDO newStrategy = new StrategyDO();
            m_Model.StartSimulation(newStrategy);

            Assert.AreEqual(SimulationStatus.Terminated, originalSimulation.Status);
        }

        [Test]
        public void SimulationHostViewModel_UT004_StartSimulation_WhenPassedValidStrategy_NewSimulationIsSet()
        {
            StrategyDO originalStrategy = new StrategyDO();
            // Start a simulation
            m_Model.StartSimulation(originalStrategy);
            ISimulationModel originalSimulation = m_Model.Simulation;

            // Start a new simulation
            StrategyDO newStrategy = new StrategyDO();
            m_Model.StartSimulation(newStrategy);
            ISimulationModel newSimulation = m_Model.Simulation;

            Assert.AreEqual(m_Model.Simulation, newSimulation);
        }

        [Test]
        public void SimulationHostViewModel_UT005_StopSimulation_TerminatesTheSimulation()
        {
            StrategyDO originalStrategy = new StrategyDO();
            // Start a simulation
            m_Model.StartSimulation(originalStrategy);

            m_Model.StopSimulation();

            Assert.AreEqual(SimulationStatus.Terminated, m_Model.Simulation.Status);
        }

        [Test]
        public void SimulationHostViewModel_UT005_StopSimulation_ThrowsInvalidOperationException_IfSimulationNotStarted()
        {
            StrategyDO originalStrategy = new StrategyDO();

            Assert.Throws<InvalidOperationException>(new TestDelegate(() => m_Model.StopSimulation()));
        }
    }
}
