using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.OutputModels.Orders
{
    public class OrderOutputModel
    {
        /// <summary>
        /// Order identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Order name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Order total price
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// Order movies names
        /// </summary>
        public List<string> MoviesNames { get; set; }
    }
}