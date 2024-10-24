using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.OutputModels.Directors
{
    public class DirectorOutputModel
    {
        /// <summary>
        /// Director identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Director name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Director surname
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Movies names
        /// </summary>
        public List<string> MoviesNames { get; set; }
    }
}