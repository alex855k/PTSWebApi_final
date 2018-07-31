using System;
using System.Collections.Generic;

namespace PTSWebApi.Models
{
    public partial class PtsFieldBlock
    {
        public PtsFieldBlock()
        {
            PtsTrialBlock = new HashSet<PtsTrialBlock>();
            PtsTrialGroup = new HashSet<PtsTrialGroup>();
        }

        public int FieldBlockId { get; set; }
        public string BlockChar { get; set; }
        public short YearCreated { get; set; }
        public string BlockDescription { get; set; }

        public ICollection<PtsTrialBlock> PtsTrialBlock { get; set; }
        public ICollection<PtsTrialGroup> PtsTrialGroup { get; set; }
    }
}
