using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOPR.Domain.DataObjects.SimulationElements.MissionElements;
using WOPR.UI.WPF.Simulation.Models.MissionElements;

namespace WOPR.UI.WPFSimulation.Tests.Models.MissionElements
{
    [TestFixture]
    public class TargetingPackageModelTests
    {
        [Test]
        public void TargetingPackageModel_UT001_WhenPackageContainsNoElements_InvalidOperationExceptionIsThrown()
        {
            TargetingPackageDO package = new TargetingPackageDO();
            
            ITargetingPackageModel packageModel = null;
            Assert.Throws<InvalidOperationException>(new TestDelegate(() => packageModel = new TargetingPackageModel(package)));
        }

        [Test]
        public void TargetingPackageModel_UT002_WhenPackageContainsFiveElements_ElementsCollectionContainsFiveElements()
        {
            TargetingPackageDO package = new TargetingPackageDO();

            package.Elements = new List<TargetingPackageElementDO>();
            for (int i = 0; i < 5; i++)
            {
                TargetingPackageElementDO element = new TargetingPackageElementDO();
                package.Elements.Add(element);
            }

            ITargetingPackageModel packageModel = new TargetingPackageModel(package);

            Assert.IsNotNull(packageModel.Elements);
            Assert.IsTrue(packageModel.Elements.Count == 5);
        }
    }
}
