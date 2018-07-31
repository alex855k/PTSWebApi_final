using System;
using System.Collections.Generic;

namespace PTSWebApi.Models
{
    public partial class PtsResultEntry
    {
        public int ResultEntryId { get; set; }
        public double ResultValue { get; set; }
        public int ResultFormatId { get; set; }
        public int DosageAmountId { get; set; }
        public int TrialGroupId { get; set; }

        public PtsDosageAmount DosageAmount { get; set; }
        public PtsResultFormat ResultFormat { get; set; }
        public PtsTrialGroup TrialGroup { get; set; }
    }
}
