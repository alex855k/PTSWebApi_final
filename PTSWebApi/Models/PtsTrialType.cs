using System;
using System.Collections.Generic;

namespace PTSWebApi.Models
{
    public partial class PtsTrialType
    {
        public PtsTrialType()
        {
            PtsTrialBlock = new HashSet<PtsTrialBlock>();
            PtsTrialTypeDosageType = new HashSet<PtsTrialTypeDosageType>();
        }

        public int TrialTypeId { get; set; }
        public string TrialTypeName { get; set; }
        public string TrialTypeDescription { get; set; }

        public ICollection<PtsTrialBlock> PtsTrialBlock { get; set; }
        public ICollection<PtsTrialTypeDosageType> PtsTrialTypeDosageType { get; set; }
    }
}
