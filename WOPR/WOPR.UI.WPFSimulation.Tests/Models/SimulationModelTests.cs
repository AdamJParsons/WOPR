using Microsoft.Practices.Unity;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WOPR.Domain.DataObjects.SimulationElements;
using WOPR.Domain.DataObjects.SimulationElements.Assets;
using WOPR.UI.WPF.Simulation.Models;

namespace WOPR.UI.WPFSimulation.Tests.Models
{
    [TestFixture]
    public class SimulationModelTests
    {
        ISimulationModel model;

        [SetUp]
        public void Setup()
        {
            SimulationDO simulation = new SimulationDO();
            Mock<IControllerModel> mockControllerModel = new Mock<IControllerModel>();
            Mock<IRoleDomainFactory> mockRoleDomainFactory = new Mock<IRoleDomainFactory>();
            Mock<IRoleDomain> mockLRoleDomain = new Mock<IRoleDomain>();
            mockRoleDomainFactory.Setup<IRoleDomain>(x => x.GetRoleDomain(It.IsAny<ActorDO>())).Returns(mockLRoleDomain.Object);
            model = new SimulationModel(simulation, mockRoleDomainFactory.Object);
        }

        [Test]
        public void SimulationModel_UT001_Terminate_WhenCalled_SetsStatusToTerminated()
        {
            model.Terminate();

            Assert.AreEqual(SimulationStatus.Terminated, model.Status);
        }

        [Test]
        public void SimulationModel_UT002_Execute_WhenCalledWithValidStrategy_SetsStatusToStarted()
        {
            StrategyDO strategy = new StrategyDO();
            model.Execute(strategy);

            Assert.AreEqual(SimulationStatus.Started, model.Status);
        }

        [Test]
        public void SimulationModel_UT003_Execute_WhenCalledWithNullStrategy_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(new TestDelegate(() => model.Execute(null)));
        }

        [Test]
        public void SimulationModel_UT004_Execute_WhenCalledWithValidStrategy_CreatesPrincipalController()
        {
            StrategyDO strategy = new StrategyDO();

            model.Execute(strategy);

            Assert.IsNotNull(model.PrincipalRole);
        }

        [Test]
        public void SimulationModel_UT005_Execute_WhenCalledWithValidStrategy_CreatesAdversaryController()
        {
            StrategyDO strategy = new StrategyDO();

            model.Execute(strategy);

            Assert.IsNotNull(model.AdversaryRole);
        }

        [Test]
        public void SimulationModel_UT006_Execute_WhenCalledWithValidStrategy_SetsSimulationStartTime()
        {
            StrategyDO strategy = new StrategyDO();

            model.Execute(strategy);

            Assert.IsNotNull(model.SimulationStartTime);
        }

        [Test]
        public void SimulationModel_UT007_Elapsed_GetsTheSimulationDuration()
        {
            StrategyDO strategy = new StrategyDO();

            model.Execute(strategy);
            DateTime startTime = model.SimulationStartTime.Value;

            Thread.Sleep(TimeSpan.FromSeconds(1));
            TimeSpan? elapsed = model.Elapsed;

            Assert.AreEqual(startTime.AddSeconds(1), startTime.AddSeconds(elapsed.Value.Seconds));
        }

        [Test]
        public void SimulationModel_UT008_Elapsed_WhenSimulationStartTimeIsNull_ReturnsNull()
        {
            TimeSpan? elapsed = model.Elapsed;

            Assert.IsNull(elapsed);
        }

        [Test]
        public void SimulationModel_UT009_Terminate_WhenCalled_SimulationStartTimeIsSetToNull()
        {
            StrategyDO strategy = new StrategyDO();
            model.Execute(strategy);

            model.Terminate();

            Assert.IsNull(model.SimulationStartTime);
        }

        [Test]
        public void SimulationModel_UT010_Terminate_WhenCalled_ElapsedIsSetToNull()
        {
            StrategyDO strategy = new StrategyDO();
            model.Execute(strategy);

            model.Terminate();

            Assert.IsNull(model.Elapsed);
        }

        [Test]
        public void SimulationModel_UT011_Terminate_WhenCalled_PrincipalControllerIsSetToNull()
        {
            StrategyDO strategy = new StrategyDO();
            model.Execute(strategy);

            model.Terminate();

            Assert.IsNull(model.PrincipalRole);
        }

        [Test]
        public void SimulationModel_UT012_Terminate_WhenCalled_AdversaryControllerIsSetToNull()
        {
            StrategyDO strategy = new StrategyDO();
            model.Execute(strategy);

            model.Terminate();

            Assert.IsNull(model.AdversaryRole);
        }

        [Test]
        public void SimulationModel_UT013_Execute_WhenCalledWithAValidStrategyWithARoleWithOneCommander_CreatesOneCommanderController()
        {

            StrategyDO strategy = new StrategyDO();
            ActorDO role = new ActorDO();
            role.Commanders = new List<CommanderDO>();
            CommanderDO commander = new CommanderDO();
            role.Commanders.Add(commander);
            strategy.Role = role;
            model.Execute(strategy);

            //CommanderModel commanderModel = model.Commanders.Single(x => x.Entity == commander);

            //Assert.IsNotNull(model.Commanders);
            //Assert.IsNotEmpty(model.Commanders);
            //Assert.Contains(commanderModel);
        }
    }

    public class MockUnityContainer : IUnityContainer
    {

        public IUnityContainer AddExtension(UnityContainerExtension extension)
        {
            throw new NotImplementedException();
        }

        public object BuildUp(Type t, object existing, string name, params ResolverOverride[] resolverOverrides)
        {
            throw new NotImplementedException();
        }

        public object Configure(Type configurationInterface)
        {
            throw new NotImplementedException();
        }

        public IUnityContainer CreateChildContainer()
        {
            throw new NotImplementedException();
        }

        public IUnityContainer Parent
        {
            get { throw new NotImplementedException(); }
        }

        public IUnityContainer RegisterInstance(Type t, string name, object instance, LifetimeManager lifetime)
        {
            throw new NotImplementedException();
        }

        public IUnityContainer RegisterType(Type from, Type to, string name, LifetimeManager lifetimeManager, params InjectionMember[] injectionMembers)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ContainerRegistration> Registrations
        {
            get { throw new NotImplementedException(); }
        }

        public IUnityContainer RemoveAllExtensions()
        {
            throw new NotImplementedException();
        }

        public object Resolve(Type t, string name, params ResolverOverride[] resolverOverrides)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> ResolveAll(Type t, params ResolverOverride[] resolverOverrides)
        {
            throw new NotImplementedException();
        }

        public void Teardown(object o)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
