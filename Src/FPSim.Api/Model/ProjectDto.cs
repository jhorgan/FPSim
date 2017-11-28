using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPSim.Api.Model
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        // Base64 encoded image
        public string Image { get; set; }
        public int UserId { get; set; }
        public int ApplicationId { get; set; }
    }
}
