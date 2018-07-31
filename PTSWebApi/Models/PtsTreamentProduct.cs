using System;
using System.Collections.Generic;

namespace PTSWebApi.Models
{
    public partial class PtsTreamentProduct
    {
        public int TreamentProductId { get; set; }
        public double ProductDose { get; set; }
        public int TreatmentId { get; set; }
        public int ProductId { get; set; }

        public PtsProduct Product { get; set; }
        public PtsTreatment Treatment { get; set; }
    }
}
