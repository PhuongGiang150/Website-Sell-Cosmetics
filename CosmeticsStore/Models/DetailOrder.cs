namespace CosmeticsStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DetailOrder")]
    public partial class DetailOrder
    {
        public int DetailOrderID { get; set; }

        public int? OrderID { get; set; }

        public int? ProductID { get; set; }

        [StringLength(255)]
        [DisplayName("Tên sản phẩm")]
        public string ProductName { get; set; }

        [DisplayName("Số lượng")]
        public int? Quantity { get; set; }

        [DisplayName("Giá")]
        public decimal? Price { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
