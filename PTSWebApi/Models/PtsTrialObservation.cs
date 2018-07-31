using System;
using System.Collections.Generic;

namespace PTSWebApi.Models
{
    public partial class PtsTrialObservation
    {
        public int TrialObservationId { get; set; }
        public int? CommentId { get; set; }
        public int? TrialGroupId { get; set; }

        public PtsComment Comment { get; set; }
        public PtsTrialGroup TrialGroup { get; set; }
    }
}
