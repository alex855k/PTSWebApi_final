using System;
using System.Collections.Generic;

namespace PTSWebApi.Models
{
    public partial class PtsDosageType
    {
        public PtsDosageType()
        {
            PtsDosageAmount = new HashSet<PtsDosageAmount>();
            PtsTrialTypeDosageType = new HashSet<PtsTrialTypeDosageType>();
        }

        public int DosageTypeId { get; set; }
        public string DosageName { get; set; }
        public int? UnitTypeId { get; set; }

        public ICollection<PtsDosageAmount> PtsDosageAmount { get; set; }
        public ICollection<PtsTrialTypeDosageType> PtsTrialTypeDosageType { get; set; }
    }
}
