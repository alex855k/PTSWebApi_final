using System;
using System.Collections.Generic;

namespace PTSWebApi.Models
{
    public partial class PtsProductCategory
    {
        public PtsProductCategory()
        {
            PtsProduct = new HashSet<PtsProduct>();
        }

        public int ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }

        public ICollection<PtsProduct> PtsProduct { get; set; }
    }
}
