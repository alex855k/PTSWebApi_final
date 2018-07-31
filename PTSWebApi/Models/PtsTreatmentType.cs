using System;
using System.Collections.Generic;

namespace PTSWebApi.Models
{
    public partial class PtsTreatmentType
    {
        public PtsTreatmentType()
        {
            PtsTreatment = new HashSet<PtsTreatment>();
        }

        public int TreatmentTypeId { get; set; }
        public string TreatmentTypeName { get; set; }

        public ICollection<PtsTreatment> PtsTreatment { get; set; }
    }
}
