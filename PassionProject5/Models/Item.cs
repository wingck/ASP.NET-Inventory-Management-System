using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PassionProject5.Models
{
    public class Item
    {
        [Key]
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public int ItemNum { get; set; }
    }
    public class ItemDto
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public int ItemNum { get; set; }
    }
}