using System;
using System.Collections.Generic;

namespace PTSWebApi.Models
{
    public partial class PtsPlant
    {
        public PtsPlant()
        {
            PtsTrialGroup = new HashSet<PtsTrialGroup>();
        }

        public int PlantId { get; set; }
        public string PlantName { get; set; }
        public int? PlantTypeId { get; set; }

        public PtsPlantType PlantType { get; set; }
        public ICollection<PtsTrialGroup> PtsTrialGroup { get; set; }
    }
}
