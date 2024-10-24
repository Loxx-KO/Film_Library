using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace API.Models.OutputModels.Movies
{
    public class MovieOutputModel
    {
        /// <summary>
        /// Movie identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Movie title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Movie director
        /// </summary>
        public string DirectorName { get; set; }

        /// <summary>
        /// Movie year
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Movie price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Movie categories
        /// </summary>
        public List<string> CategoriesNames { get; set; }
    }
}