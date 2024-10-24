using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;

namespace API.Models.InputModels.Movies
{
    public class MovieInputModel
    {
        /// <summary>
        /// Movie title
        /// </summary>
        [Required, MaxLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// Movie year
        /// </summary>
        [Required, IntegerValidator]
        public int Year { get; set; }

        /// <summary>
        /// Director identifier
        /// </summary>
        [Required, IntegerValidator]
        public int DirectorId { get; set; }

        /// <summary>
        /// Movie price
        /// </summary>
        [Required, Range(0, (double)decimal.MaxValue)]
        public decimal Price { get; set; }

        /// <summary>
        /// Movie image path
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Categories identifiers
        /// </summary>
        [Required]
        public List<int> CategoryIds { get; set; }
    }
}