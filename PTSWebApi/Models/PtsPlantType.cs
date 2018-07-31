using System;
using System.Collections.Generic;

namespace PTSWebApi.Models
{
    public partial class PtsPlantType
    {
        public PtsPlantType()
        {
            PtsPlant = new HashSet<PtsPlant>();
        }

        public int PlantTypeId { get; set; }
        public string PlantTypeName { get; set; }

        public ICollection<PtsPlant> PtsPlant { get; set; }
    }
}
