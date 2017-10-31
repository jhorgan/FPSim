using System;
using System.Collections.Generic;

namespace FPSim.Data.Entity
{
    public class Project
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public string Description { get; set; }
        public bool IsArchived { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        // Relationships
        public User User { get; set; }
        public Application Application { get; set; }
        public List<Scenario> Scenarios { get; set; }
    }
}
