using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Entities.Models;

namespace API.Models.InputModels.Orders
{
    public class OrderInputModel
    {
        /// <summary>
        /// Order name
        /// </summary>
        [Required, MaxLength(60)]
        public string Name { get; set; }

        ///// <summary>
        ///// Order total price
        ///// </summary>
        //[Range(0, (double)decimal.MaxValue)]
        //public decimal TotalPrice { get; set; }

        /// <summary>
        /// Order movies identifiers
        /// </summary>
        [Required]
        public List<int> MovieIds { get; set; }
    }
}