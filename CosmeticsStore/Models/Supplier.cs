namespace CosmeticsStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Supplier
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Supplier()
        {
            Products = new HashSet<Product>();
        }

        public int SupplierID { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Vui lòng nhập trường này!")]
        [DisplayName("Nhà Cung Cấp")]
        public string Name { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "Vui lòng nhập trường này!")]
        [DisplayName("Địa chỉ")]
        public string Address { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "Vui lòng nhập trường này!")]
        public string Email { get; set; }

        [StringLength(12)]
        [Required(ErrorMessage = "Vui lòng nhập trường này!")]
        public string Phone { get; set; }

        [DisplayName("Trạng thái")]
        public bool? Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
