using System.Collections.Generic;
using System.Linq;
using FPSim.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace FPSim.Data.Repository
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<Project> GetProjectsForUser(int userId)
        {
            // Avoid returning the project image as it is not always required.
            // Use the GetProjectImage() to get the image.
            return Context.Set<Project>()
                .Where(project => project.UserId == userId)
                .Select(project => new Project()
                {
                    Id = project.Id,
                    ApplicationId = project.ApplicationId,
                    UserId = project.UserId,
                    Name = project.Name,
                    Description = project.Description,
                    IsArchived = project.IsArchived,
                    DateCreated = project.DateCreated,
                    DateModified = project.DateModified
                });
        }

        public byte[] GetProjectImage(int projectId)
        {
            var project = Context.Set<Project>()
                .Where(p => p.Id == projectId)
                .Select(p => new Project()
                {
                    Image = p.Image
                })
                .FirstOrDefault();

            return project?.Image;
        }


        public IEnumerable<Project> GetProjectsAndReleatedScenariosForUser(int userId)
        {
            return Context.Set<Project>()
                .Where(project => project.UserId == userId && !project.IsArchived)
                .Include(project => project.Scenarios);
        }
    }
}
