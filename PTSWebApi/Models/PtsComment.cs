using System;
using System.Collections.Generic;

namespace PTSWebApi.Models
{
    public partial class PtsComment
    {
        public PtsComment()
        {
            PtsTreatmentComment = new HashSet<PtsTreatmentComment>();
            PtsTrialObservation = new HashSet<PtsTrialObservation>();
        }

        public int CommentId { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime? ChangedDate { get; set; }
        public string CommentText { get; set; }
        public int? UserId { get; set; }

        public PtsUser User { get; set; }
        public ICollection<PtsTreatmentComment> PtsTreatmentComment { get; set; }
        public ICollection<PtsTrialObservation> PtsTrialObservation { get; set; }
    }
}
