using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace dotNetCoreWebAPI.Models
{
    public class Trip
    {        
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        //For Avoiding Data Annotations Example
        public int AlternativeID { get; set; }
    }
}
