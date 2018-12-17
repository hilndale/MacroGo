using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalCapstone.Models
{
    public class UserFavoritesViewModel
    {
        public IList<UserFavorites> Favorites { get; set; } 
    }
}
