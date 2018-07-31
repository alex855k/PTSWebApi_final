using System;
using System.Collections.Generic;

namespace PTSWebApi.Models
{
    public partial class PtsTreatmentComment
    {
        public int TreatmentCommentId { get; set; }
        public int TreatmentId { get; set; }
        public int CommentId { get; set; }

        public PtsComment Comment { get; set; }
        public PtsTreatment Treatment { get; set; }
    }
}
