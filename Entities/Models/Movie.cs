using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? DirectorId { get; set; }
        public virtual Director Director { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public ICollection<int> CategoryIds { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public string Image { get; set; }
    }
}
