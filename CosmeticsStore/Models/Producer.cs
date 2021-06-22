namespace CosmeticsStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Producer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Producer()
        {
            Products = new HashSet<Product>();
        }

        public int ProducerID { get; set; }

        [StringLength(100)]
        [DisplayName("Tên Hãng")]
        [Required(ErrorMessage = "Vui lòng nhập trường này!")]
        public string Name { get; set; }

        [StringLength(250)]
        [DisplayName("Bí danh")]
        public string MetaTitle { get; set; }

        [DisplayName("Hiển thị")]
        public int? DisplayOder { get; set; }

        [StringLength(255)]
        [DisplayName("Thông tin")]
        [Required(ErrorMessage = "Vui lòng nhập trường này!")]
        public string Info { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập trường này!")]
        public string Logo { get; set; }

        [DisplayName("Trạng thái")]
        public bool? Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
