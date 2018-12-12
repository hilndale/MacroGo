using System.Collections.Generic;
using System.Linq;

namespace FinalCapstone.Models
{
    public class IndexModel
    {
        //bool methods to determine if the item meets each requirement 
        //if carbMin or carbMax is 0, the user has chosen not to specify a value and will not be part of the method
        //if neither is specified, carb requirement met will always return true
        //protein and fat follow same logic 

        public bool MeetsCarbRequirement(int carbs, IndexViewModel criteria)
        {
            if (criteria.MinCarbs != 0 && criteria.MaxCarbs != 0)
            {
                if (criteria.MinCarbs <= carbs && criteria.MaxCarbs >= carbs)
                {
                    return true;
                }
            }
            else if (criteria.MinCarbs != 0)
            {
                if (criteria.MinCarbs <= carbs)
                {
                    return true;
                }
            }
            else if (criteria.MaxCarbs != 0)
            {
                if (criteria.MaxCarbs >= carbs)
                {
                    return true;
                }
            }
            else if (criteria.MaxCarbs == 0 && criteria.MinCarbs == 0)
            {
                return true;
            }

            return false;
        }

        public bool MeetsProteinRequirement(int protein, IndexViewModel criteria)
        {
            if (criteria.MinProtein != 0 && criteria.MaxProtein != 0)
            {
                if (criteria.MinProtein <= protein && criteria.MaxProtein >= protein)
                {
                    return true;
                }
            }
            else if (criteria.MinProtein != 0)
            {
                if (criteria.MinProtein <= protein)
                {
                    return true;
                }
            }
            else if (criteria.MaxProtein != 0)
            {
                if (criteria.MaxProtein >= protein)
                {
                    return true;
                }
            }
            else if (criteria.MaxProtein == 0 && criteria.MinProtein == 0)
            {
                return true;
            }

            return false;
        }

        public bool MeetsFatRequirement(int fat, IndexViewModel criteria)
        {
            if (criteria.MinFat != 0 && criteria.MaxFat != 0)
            {
                if (criteria.MinFat <= fat && criteria.MaxFat >= fat)
                {
                    return true;
                }
            }
            else if (criteria.MinFat != 0)
            {
                if (criteria.MinFat <= fat)
                {
                    return true;
                }
            }
            else if (criteria.MaxFat != 0)
            {
                if (criteria.MaxFat >= fat)
                {
                    return true;
                }
            }
            else if (criteria.MaxFat == 0 && criteria.MinFat == 0)
            {
                return true;
            }

            return false;
        }

        //if restaurant is an empty string, all restaurants are valid options, so returns true
        public bool MeetsRestaurantRequirement(string restaurant, IndexViewModel criteria)
        {
            if (criteria.RestaurantChosen == restaurant)
            {
                return true;
            }
            else if (criteria.RestaurantChosen == "All Restaurants")
            {
                return true;
            }
            return false;
        }

        public IList<Item> Filter(IList<Item> list, IndexViewModel criteria)
        {
            list = new List<Item>();

            if (criteria.FilterCountSelect == 10)
            {
                return list.Take(10).ToList();
            }
            else if (criteria.FilterCountSelect == 20)
            {
                return list.Take(20).ToList();
            }
            else if (criteria.FilterCountSelect == 50)
            {
                return list.Take(50).ToList();
            }
            else if (criteria.FilterCountSelect == 100)
            {
                return list.Take(100).ToList();
            }
            else
            {
                return list;
            }
        }

        public IList<Item> Sort(IList<Item> list, IndexViewModel criteria)
        {
            list = new List<Item>();
            IEnumerable<Item> sortedEnum = new List<Item>();

            if (criteria.OrderbySelect == "Carbs")
            {
                sortedEnum = list.OrderBy(f => f.Carbs);
            }
            else if (criteria.OrderbySelect == "Fat")
            {
                sortedEnum = list.OrderBy(f => f.Fat);
            }
            else
            {
                sortedEnum = list.OrderBy(f => f.Protein);
            }

            return sortedEnum.ToList();
        }

        //final method to return complete list matching all criteria 
        //all criteria will be used in the final method
        public IList<Item> GetResult(IList<Item> allItems, IndexViewModel criteria)
        {
            IList<Item> ResultFoodItems = new List<Item>();

            foreach (Item item in allItems)
            {
                if (MeetsCarbRequirement(item.Carbs, criteria) && MeetsProteinRequirement(item.Protein, criteria) && MeetsFatRequirement(item.Fat, criteria) && MeetsRestaurantRequirement(item.RestaurantName, criteria))
                {
                    ResultFoodItems.Add(item);
                }
            }

            //separating into two methods does not allow the search function to be called ?? 

            //IList<Item> Test = ResultFoodItems;

            //IList<Item> sortedEnum = Sort(Test, criteria);

            //IList<Item> filtered = Filter(sortedEnum, criteria);


            IEnumerable<Item> sortedEnum = new List<Item>();

            if (criteria.OrderbySelect == "Carbs")
            {
                sortedEnum = ResultFoodItems.OrderByDescending(f => f.Carbs);
            }
            else if (criteria.OrderbySelect == "Fat")
            {
                sortedEnum = ResultFoodItems.OrderByDescending(f => f.Fat);
            }
            else
            {
                sortedEnum = ResultFoodItems.OrderByDescending(f => f.Protein);
            }


            if (criteria.FilterCountSelect == 10)
            {
                return sortedEnum.Take(10).ToList();
            }
            else if (criteria.FilterCountSelect == 20)
            {
                return sortedEnum.Take(20).ToList();
            }
            else if (criteria.FilterCountSelect == 50)
            {
                return sortedEnum.Take(50).ToList();
            }
            else if (criteria.FilterCountSelect == 100)
            {
                return sortedEnum.Take(100).ToList();
            }

            return sortedEnum.ToList();
        }
    }
}
