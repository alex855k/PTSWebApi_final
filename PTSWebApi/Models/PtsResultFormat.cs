using System;
using System.Collections.Generic;

namespace PTSWebApi.Models
{
    public partial class PtsResultFormat
    {
        public PtsResultFormat()
        {
            PtsResultEntry = new HashSet<PtsResultEntry>();
        }

        public int ResultFormatId { get; set; }
        public string ResultFormatTitle { get; set; }
        public string ResultFormatDescription { get; set; }
        public int? TrialBlockId { get; set; }
        public int? UnitTypeId { get; set; }

        public PtsTrialBlock TrialBlock { get; set; }
        public PtsUnitType UnitType { get; set; }
        public ICollection<PtsResultEntry> PtsResultEntry { get; set; }
    }
}
