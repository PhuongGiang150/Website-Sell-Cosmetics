namespace CosmeticsStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Review
    {
        public int ReviewID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập trường này!")]
        [DisplayName("Nội dung")]
        public string Content { get; set; }

        [DisplayName("Số điểm(/10)")]
        public int? Star { get; set; }

        public int? CusomerID { get; set; }

        public int? ProductID { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Product Product { get; set; }
    }
}
