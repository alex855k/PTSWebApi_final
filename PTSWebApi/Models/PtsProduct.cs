using System;
using System.Collections.Generic;

namespace PTSWebApi.Models
{
    public partial class PtsProduct
    {
        public PtsProduct()
        {
            PtsTreamentProduct = new HashSet<PtsTreamentProduct>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductOwner { get; set; }
        public int? ProductCategoryId { get; set; }

        public PtsProductCategory ProductCategory { get; set; }
        public ICollection<PtsTreamentProduct> PtsTreamentProduct { get; set; }
    }
}
