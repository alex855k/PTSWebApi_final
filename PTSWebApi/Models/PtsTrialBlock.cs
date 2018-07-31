using System;
using System.Collections.Generic;

namespace PTSWebApi.Models
{
    public partial class PtsTrialBlock
    {
        public PtsTrialBlock()
        {
            PtsResultFormat = new HashSet<PtsResultFormat>();
        }

        public int TrialBlockId { get; set; }
        public string TrialBlockDescription { get; set; }
        public short? TrialEnd { get; set; }
        public int? TrialTypeId { get; set; }
        public int? FieldBlockId { get; set; }

        public PtsFieldBlock FieldBlock { get; set; }
        public PtsTrialType TrialType { get; set; }
        public ICollection<PtsResultFormat> PtsResultFormat { get; set; }
    }
}
