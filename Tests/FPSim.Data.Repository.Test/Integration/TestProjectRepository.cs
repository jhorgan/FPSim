using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FPSim.Data.Repository.Test.Integration
{
    [TestClass]
    public class TestProjectUnitOfWork
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
        public void WhenValidUserThenProjectCountShouldBeGreaterThanZero()
        {
            // Arrange
            int projectCount;

            var userId = TestInitializeUtils.CreateTestUser();
            var applicationId = TestInitializeUtils.CreateTestApplication();
            TestInitializeUtils.CreateTestProject(applicationId, userId);
            TestInitializeUtils.CreateTestProject(applicationId, userId);

            // Act
            using (var unitOfWork = new UnitOfWork(_context))
            {
                var projects = unitOfWork.Projects.GetProjectsForUser(userId);
                projectCount = projects.ToList().Count;
            }

            // Assert
            Assert.AreEqual(2, projectCount);
        }

        [TestMethod]
        public void WhenInValidUserThenProjectCountShouldBeZero()
        {
            // Arrange
            int projectCount;

            var userId = TestInitializeUtils.CreateTestUser();
            var applicationId = TestInitializeUtils.CreateTestApplication();
            TestInitializeUtils.CreateTestProject(applicationId, userId);

            // Act
            using (var unitOfWork = new UnitOfWork(_context))
            {
                const int invalidUserId = -1;
                var projects = unitOfWork.Projects.GetProjectsForUser(invalidUserId);
                projectCount = projects.ToList().Count;
            }

            // Assert
            Assert.AreEqual(0, projectCount);
        }

        [TestMethod]
        public void WhenValidUserThenProjectsAndRelatedScenariosShouldBeGreaterThanZero()
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
                var projects = unitOfWork.Projects.GetProjectsAndReleatedScenariosForUser(userId);
                var project = projects.First();

                scenarioCount = project.Scenarios.Count;
            }

            // Assert
            Assert.AreEqual(2, scenarioCount);
        }

        [TestMethod]
        public void WhenProjectHasImageThenGetProjectImageShouldBeNotNull()
        {
            // Arrange            
            var userId = TestInitializeUtils.CreateTestUser();
            var applicationId = TestInitializeUtils.CreateTestApplication();
            var imageFilename = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Images",
                "TestProjectImage.png");
            var imageOctets = ReadImageAsOctectString(imageFilename);
            var projectId = TestInitializeUtils.CreateTestProject(applicationId, userId, imageOctets);

            // Act
            byte[] imageAsByteArray;
            using (var unitOfWork = new UnitOfWork(_context))
            {
                imageAsByteArray = unitOfWork.Projects.GetProjectImage(projectId);
            }

            // Assert
            Assert.IsNotNull(imageAsByteArray);
            Assert.IsTrue(imageAsByteArray.Length > 0);
        }

        private static string ReadImageAsOctectString(string imageFilename)
        {
            var buffer = File.ReadAllBytes(imageFilename);
            var hex = new StringBuilder(buffer.Length * 2);
            foreach (var b in buffer)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }
    }
}
