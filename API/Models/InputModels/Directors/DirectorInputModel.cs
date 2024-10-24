using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API.Models.InputModels.Directors
{
    public class DirectorInputModel
    {
        /// <summary>
        /// Director name
        /// </summary>
        [Required, MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Director surname
        /// </summary>
        [Required, MaxLength(50)]
        public string Surname { get; set; }
    }
}