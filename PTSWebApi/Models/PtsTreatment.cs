using System;
using System.Collections.Generic;

namespace PTSWebApi.Models
{
    public partial class PtsTreatment
    {
        public PtsTreatment()
        {
            PtsTreamentProduct = new HashSet<PtsTreamentProduct>();
            PtsTreatmentComment = new HashSet<PtsTreatmentComment>();
            PtsTreatmentImage = new HashSet<PtsTreatmentImage>();
        }

        public int TreatmentId { get; set; }
        public DateTime? TreatmentDate { get; set; }
        public string TreatmentStage { get; set; }
        public bool? IsTrialTreatment { get; set; }
        public int? TreatmentTypeId { get; set; }
        public int TrialGroupId { get; set; }

        public PtsTreatmentType TreatmentType { get; set; }
        public PtsTrialGroup TreatmentTypeNavigation { get; set; }
        public ICollection<PtsTreamentProduct> PtsTreamentProduct { get; set; }
        public ICollection<PtsTreatmentComment> PtsTreatmentComment { get; set; }
        public ICollection<PtsTreatmentImage> PtsTreatmentImage { get; set; }
    }
}
