using System;
using System.Collections.Generic;

namespace PTSWebApi.Models
{
    public partial class PtsTrialTypeDosageType
    {
        public int TrialTypeDosageTypeId { get; set; }
        public int TrialTypeId { get; set; }
        public int DosageTypeId { get; set; }

        public PtsDosageType DosageType { get; set; }
        public PtsTrialType TrialType { get; set; }
    }
}
