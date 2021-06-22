namespace CosmeticsStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class News
    {
        [Key]
        public int NewID { get; set; }

        [StringLength(250)]
        [DisplayName("Tiêu đề")]
        [Required(ErrorMessage = "Vui lòng nhập trường này!")]
        public string Name { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [DisplayName("Hiển thị")]
        public int? DisplayOrder { get; set; }

        [DisplayName("Ngày cập nhật")]
        [DataType(DataType.Date)]
        public DateTime? UpdateDate { get; set; }

        [DisplayName("Tin tức")]
        [Required(ErrorMessage = "Vui lòng nhập trường này!")]
        public string Content { get; set; }

        [DisplayName("Ảnh minh họa")]
        [Required(ErrorMessage = "Vui lòng nhập trường này!")]
        [StringLength(250)]
        public string Image { get; set; }

        [DisplayName("Mô tả")]
        [Required(ErrorMessage = "Vui lòng nhập trường này!")]
        [StringLength(1000)]
        public string Description { get; set; }

        [DisplayName("Trạng thái")]
        public bool? Status { get; set; }
    }
}
