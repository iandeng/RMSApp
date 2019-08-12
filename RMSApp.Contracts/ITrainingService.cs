using RMSApp.Entities.POCO;
using System;
using System.Collections.Generic;
using System.Text;

namespace RMSApp.Contracts
{
    public interface ITrainingService
    {
        BookingResult CreateTrainingBooking(string name, DateTime startDate, DateTime endDate);
    }
}
