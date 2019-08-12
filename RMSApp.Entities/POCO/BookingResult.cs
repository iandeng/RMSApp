using System;
using System.Collections.Generic;
using System.Text;

namespace RMSApp.Entities.POCO
{
    public class BookingResult
    {
        public string Message { get; set; }
        public int TrainingDurationInDays { get; set; }
        public bool IsSuccess { get; set; }
    }
}
