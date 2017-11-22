using System;
using System.Linq;
using FPSim.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FPSim.Data.Repository.Test.Integration
{
    [TestClass]
    public class TestApplicationRepository
    {
        private AppDbContext _context;

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
        public void WhenQueryApplicationShouldReturnRelatedProjects()
        {
            // Arrange
            var userId = TestInitializeUtils.CreateTestUser();
            var applicationId = TestInitializeUtils.CreateTestApplication();
            TestInitializeUtils.CreateTestProject(applicationId, userId);
            TestInitializeUtils.CreateTestProject(applicationId, userId);
            int projectCount;

            // Act
            using (var unitOfWork = new UnitOfWork(_context))
            {
                var applications = unitOfWork.Applications.GetApplicationAndReleatedProjects(applicationId);
                projectCount = applications.Projects.ToList().Count;
            }

            // Assert
            Assert.AreEqual(2, projectCount);
        }

        [TestMethod]
        public void WhenAddApplicationShouldExist()
        {
            // Arrange
            var application = new Application()
            {
                Name = "Test Application",
                IsArchived = false,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            // Act
            using (var unitOfWork = new UnitOfWork(_context))
            {
                unitOfWork.Applications.Add(application);
                unitOfWork.Complete();
            }

            // Assert
            Assert.AreNotEqual(0, application.Id);
        }
    }
}
