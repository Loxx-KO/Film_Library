using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.OutputModels.Categories
{
    public class CategoryOutputModel
    {
        /// <summary>
        /// Category identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Category name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Movies names
        /// </summary>
        public List<string> MoviesNames { get; set; }
    }
}