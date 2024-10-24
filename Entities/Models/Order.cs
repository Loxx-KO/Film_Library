using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal TotalPrice { get; set; }

        public ICollection<int> MovieIds { get; set; }
        public virtual ICollection<Movie> Movies { get; set; }
    }
}
