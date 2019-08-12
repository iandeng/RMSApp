using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RMSApp.Entities.Models
{
    public class Training
    {
        public int Id { get; set; }
        [Required]
        public string TrainingName { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}
