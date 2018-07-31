using System;
using System.Collections.Generic;

namespace PTSWebApi.Models
{
    public partial class PtsTreatmentImage
    {
        public int TreatmentImageId { get; set; }
        public string ImagePath { get; set; }
        public string Caption { get; set; }
        public int? TreatmentId { get; set; }

        public PtsTreatment Treatment { get; set; }
    }
}
