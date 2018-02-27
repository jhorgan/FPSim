using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FPSim.Data.Repository.Test.Integration
{
    [TestClass]
    public class TestScenarioRepository
    {
        private AppDbContext _context;

        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            _context = TestConnectionUtils.CreateDbContext();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _context.Dispose();
        }

        [TestMethod]
        public void WhenValidProjectThenScenarioCountShouldBeGreaterThanZero()
        {
            // Arrange
            int scenarioCount;

            var userId = TestInitializeUtils.CreateTestUser();
            var applicationId = TestInitializeUtils.CreateTestApplication();
            var projectId = TestInitializeUtils.CreateTestProject(applicationId, userId);
            TestInitializeUtils.CreateTestScenario(projectId);
            TestInitializeUtils.CreateTestScenario(projectId);

            // Act
            using (var unitOfWork = new UnitOfWork(_context))
            {
                var scenarios = unitOfWork.Scenarios.GetScenariosForProject(projectId);
                scenarioCount = scenarios.ToList().Count;
            }

            // Assert
            Assert.AreEqual(2, scenarioCount);
        }
    }
}
