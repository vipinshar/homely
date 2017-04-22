using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homely.Common.Model
{
    public class HomePageViewModel
    {
        public HomePageViewModel()
        {
            this.featuredListingViewModel = new List<FeaturedListingViewModel>();
            this.hotPropertyViewModel = new List<HotPropertyViewModel>();
            this.propertyCountViewModel = new PropertyCountViewModel();
            this.reviewViewModel = new List<ReviewViewModel>();
            
        }
        public PropertyCountViewModel propertyCountViewModel { get; set; }

        public List<FeaturedListingViewModel> featuredListingViewModel { get; set; }

        public List<ReviewViewModel> reviewViewModel { get; set; }

        public List<HotPropertyViewModel> hotPropertyViewModel { get; set; }

    }

    public class PropertyCountViewModel
    {
        public int FreshListing { get; set; }

        public int LatestListing { get; set; }

        public int DelhiPropertyCount { get; set; }

        public int GZBPropertyCount { get; set; }

        public int GGNPropertyCount { get; set; }

        public int NoidaPropertyCount { get; set; }

        public int MumbaiPropertyCount { get; set; }

    }

    public class FeaturedListingViewModel
    {
        public string Locality { get; set; }
        public string SocietyName { get; set; }
        public int Rent { get; set; }
        public string Area { get; set; }
        public string Bathroom { get; set; }
        public string Bedroom { get; set; }
        public string PropertyCode { get; set; }
        public string Url { get; set; }
        public int TransactionType { get; set; }

        public string imageUrl { get; set; }
        public int TotalRows { get; set; }
    }

    public class ReviewViewModel
    {
        public string Review { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }

    }
    public class HotPropertyViewModel
    {
        public string imageUrl { get; set; }
        public string Locality { get; set; }
        public string SocietyName { get; set; }
        public int SubPropertyType { get; set; }
        public int Rent { get; set; }
        public string Area { get; set; }
        public string Bathroom { get; set; }
        public string Bedroom { get; set; }
        public string Url { get; set; }
        public string PropertyCode { get; set; }
    }
}