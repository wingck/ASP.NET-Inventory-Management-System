using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace PassionProject5.Models
{
    public class Purchase
    {
        [Key]
        public int PurchaseID { get; set; }
        public string PurchaseNum { get; set; }

        [ForeignKey("Item")]
        public int ItemID { get; set; }
        public virtual Item Item { get; set; }
    }
    public class PurchaseDto{
        public int PurchaseID { get; set; }
        public string PurchaseNum { get; set; }
        public string ItemName { get; set; }
    }
}