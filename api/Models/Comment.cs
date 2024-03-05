using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content  { get; set; } =string.Empty;
        public DateTime Createdon { get; set; } = DateTime.Now;

        public int? StockID { get; set; }
        public Stock? Stock { get; set; }
    }
}