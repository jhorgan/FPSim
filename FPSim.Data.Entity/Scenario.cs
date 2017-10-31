using System;

namespace FPSim.Data.Entity
{
    public class Scenario
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public int StatusId { get; set; }
        public int ResultStatusId { get; set; }
        public string Description { get; set; }
        public DateTime? WarmUpPeriod { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Replications { get; set; }
        public int? RandomSkip { get; set; }
        public bool IsArchived { get; set; }
        public string ExperimentReference { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        // Relationships
        public Project Project { get; set; }
    }
}
