using System;
using System.Collections.Generic;

namespace PTSWebApi.Models
{
    public partial class PtsUnitType
    {
        public PtsUnitType()
        {
            PtsDosageAmount = new HashSet<PtsDosageAmount>();
            PtsResultFormat = new HashSet<PtsResultFormat>();
        }

        public int UnitTypeId { get; set; }
        public string UnitTypeName { get; set; }

        public ICollection<PtsDosageAmount> PtsDosageAmount { get; set; }
        public ICollection<PtsResultFormat> PtsResultFormat { get; set; }
    }
}
