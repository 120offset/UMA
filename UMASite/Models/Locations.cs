using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UMASite.Models
{
    public class Locations
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Please enter a Tag Name")]
        [Display(Name = "Tag")]
        public string Tag { get; set; }
        [Required(ErrorMessage = "Please enter city latitude")]
        public double Latitude { get; set; }
        [Required(ErrorMessage = "Please enter city longitude ")]
        public double Longitude { get; set; }
        public string Description { get; set; }
    }
}