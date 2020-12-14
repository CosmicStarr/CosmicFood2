using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class MenuItems
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        [Range(00.01, int.MaxValue, ErrorMessage = "Price should be greater than one dollar!")]
        public double Price { get; set; }

        [Display(Name = "Category Type")]
        public int CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }

        [Display(Name = "FoodType")]
        public int FoodTypeID { get; set; }

        [ForeignKey("FoodTypeID")]
        public virtual FoodType FoodType { get; set; }
    }
}

