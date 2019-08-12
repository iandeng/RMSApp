using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RMSApp.Entities.POCO;
using RMSApp.Service;

namespace RMSApp.Controllers
{
    [Route("api/[controller]")]
    public class TrainingController : Controller
    {
        [HttpPost("[action]")]
        public BookingResult BookTraining([FromBody] TrainingInfo info)
        {
            TrainingService trainingService = new TrainingService();
            return trainingService.CreateTrainingBooking(info.TrainingName, info.StartDate, info.EndDate);
        }
    }
}
