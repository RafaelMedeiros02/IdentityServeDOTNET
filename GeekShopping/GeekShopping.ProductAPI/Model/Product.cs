using GeekShopping.ProductAPI.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.ProductAPI.Model
{
    [Table("product")]
    public class Product : BaseEntity
    {

        //name

        [Column("name")]
        [Required]
        [StringLength(150)]
        public string Name  { get; set; }

        //price

        [Column("price")]
        [Required]
        [Range(1,10000)]
        public decimal Price { get; set; }

        //description
        [Column("description")]
        [StringLength(500)]
        public string Description { get; set; }

        //category name 
        [Column("category_name")]
        [StringLength(50)]
        public string CategoryName { get; set; }

        //image url 
        [Column("image_url")]
        [StringLength(300)]
        public string ImageUrl { get; set; }

    }
}
