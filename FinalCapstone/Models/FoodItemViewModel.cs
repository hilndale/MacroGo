using Microsoft.AspNetCore.Mvc.Rendering;
using FinalCapstone.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FinalCapstone.Models
{
    public class FoodItemViewModel
    {
        public int FoodId { get; set; }
        [Required]
        public string FoodName { get; set; }
        public int RestaurantId { get; set; }
        [Required(ErrorMessage = "*")]
        [Range(0, 3000, ErrorMessage = "Must enter a quantity between 0 and 3000.")]
        public int Protein { get; set; }
        [Required(ErrorMessage = "*")]
        [Range(0, 3000, ErrorMessage = "Must enter a quantity between 0 and 3000.")]
        public int Fat { get; set; }
        [Required(ErrorMessage = "*")]
        [Range(0, 3000, ErrorMessage = "Must enter a quantity between 0 and 3000.")]
        public int Carbs { get; set; }
        [Required(ErrorMessage = "*")]
        [Range(0, 3000, ErrorMessage = "Must enter a quantity between 0 and 3000.")]
        public int Calories { get; set; }
        public string RestaurantChosen { get; set; }  
    
        public IList<SelectListItem> RestaurantSelect { get; set; }
       

    }

    
}
