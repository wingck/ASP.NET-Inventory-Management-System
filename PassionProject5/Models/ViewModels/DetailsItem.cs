using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProject5.Models.ViewModels
{
    public class DetailsItem
    {
        public ItemDto SelectedItem { get; set; }
        public IEnumerable<PurchaseDto> RelatedPurchases { get; set; }
    }
}