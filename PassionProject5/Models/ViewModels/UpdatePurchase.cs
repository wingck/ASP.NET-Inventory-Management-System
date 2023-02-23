using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProject5.Models.ViewModels
{
    public class UpdatePurchase
    {
        public PurchaseDto SelectedPurchase { get; set; }
        public IEnumerable<ItemDto> ItemOptions { get; set; }
    }
}