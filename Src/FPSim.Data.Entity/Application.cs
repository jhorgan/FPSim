using System;
using System.Collections.Generic;
using System.Text;

namespace FPSim.Data.Entity
{
    public class Application
    {
        public int Id { get; set; }
        public string Name { get; set; }        
        public byte[] Image { get; set; }
        public byte[] ModelFile { get; set; }
        public string ModelFilename { get; set; }
        public string Notes { get; set; }
        public bool IsArchived { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        // Relationships
        public List<Project> Projects { get; set; }
    }
}
