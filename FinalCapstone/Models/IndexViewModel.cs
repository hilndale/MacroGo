using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCapstone.Models
{
    public class IndexViewModel
    {
        public int MinCarbs { get; set; }
        public int MaxCarbs { get; set; }
        public int MinProtein { get; set; }
        public int MaxProtein { get; set; }
        public int MinFat { get; set; }
        public int MaxFat { get; set; }
        public string RestaurantChosen { get; set; }
        public string OrderbySelect { get; set; }
        //public string OrderbySelect2 { get; set; }
        //public string OrderbySelect3 { get; set; }
        public int FilterCountSelect { get; set; }
        public int SearchRadius { get; set; }
        

        public IList<SelectListItem> RestaurantSelect { get; set; }

        public IList<SelectListItem> OrderBy = new List<SelectListItem>()
        {
           new SelectListItem() {Text = "Protein"},
           new SelectListItem() {Text = "Fat"},
           new SelectListItem() {Text = "Carbs"}
        };

        //public IList<SelectListItem> OrderbyLevel2 = new List<SelectListItem>()
        //{
        //   new SelectListItem() {Text = "Protein"},
        //   new SelectListItem() {Text = "Fat"},
        //   new SelectListItem() {Text = "Carbs"}
        //};

        //public IList<SelectListItem> OrderbyLevel3 = new List<SelectListItem>()
        //{
        //   new SelectListItem() {Text = "Protein"},
        //   new SelectListItem() {Text = "Fat"},
        //   new SelectListItem() {Text = "Carbs"}
        //};

        public IList<SelectListItem> FilterCount = new List<SelectListItem>()
        {
           new SelectListItem() {Text = "10"},
           new SelectListItem() {Text = "20"},
           new SelectListItem() {Text = "30"},
           new SelectListItem() {Text = "50"},
           new SelectListItem() {Text = "100"},
           new SelectListItem() {Text = "All"}
        };

        public IList<SelectListItem> SearchRadiusSelection = new List<SelectListItem>()
        {
           new SelectListItem() {Text = "1"},
           new SelectListItem() {Text = "2"},
           new SelectListItem() {Text = "3"},
           new SelectListItem() {Text = "5"},
           new SelectListItem() {Text = "10"},
           new SelectListItem() {Text = "No Limit"}
        };

        public IList<Item> Results { get; set; }

    }
}
