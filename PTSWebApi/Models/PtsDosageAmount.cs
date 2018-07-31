using System;
using System.Collections.Generic;

namespace PTSWebApi.Models
{
    public partial class PtsDosageAmount
    {
        public PtsDosageAmount()
        {
            PtsResultEntry = new HashSet<PtsResultEntry>();
        }

        public int DosageAmountId { get; set; }
        public double? ResultValue { get; set; }
        public int? UnitTypeId { get; set; }
        public int? DosageTypeId { get; set; }

        public PtsDosageType DosageType { get; set; }
        public PtsUnitType UnitType { get; set; }
        public ICollection<PtsResultEntry> PtsResultEntry { get; set; }
    }
}
