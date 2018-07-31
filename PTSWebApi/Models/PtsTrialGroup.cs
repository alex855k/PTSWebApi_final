using System;
using System.Collections.Generic;

namespace PTSWebApi.Models
{
    public partial class PtsTrialGroup
    {
        public PtsTrialGroup()
        {
            PtsResultEntry = new HashSet<PtsResultEntry>();
            PtsTreatment = new HashSet<PtsTreatment>();
            PtsTrialObservation = new HashSet<PtsTrialObservation>();
        }

        public int TrialGroupId { get; set; }
        public int? TrialGroupNr { get; set; }
        public int? PlantId { get; set; }
        public int? FieldBlockId { get; set; }

        public PtsFieldBlock FieldBlock { get; set; }
        public PtsPlant Plant { get; set; }
        public ICollection<PtsResultEntry> PtsResultEntry { get; set; }
        public ICollection<PtsTreatment> PtsTreatment { get; set; }
        public ICollection<PtsTrialObservation> PtsTrialObservation { get; set; }
    }
}
