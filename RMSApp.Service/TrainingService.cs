using Microsoft.EntityFrameworkCore;
using RMSApp.Contracts;
using RMSApp.Entities.Models;
using RMSApp.Entities.POCO;
using RMSApp.Repositories;
using System;

namespace RMSApp.Service
{
    public class TrainingService : ITrainingService
    {
        TrainingContext _context;
        IRepository<Training> _repository;
        public TrainingService()
        {
            var options = new DbContextOptionsBuilder<TrainingContext>().UseSqlite("Data Source=training.db").Options;
            _context = new TrainingContext(options);
            _repository = new Repository<Training>(_context);
        }

        public BookingResult CreateTrainingBooking(string name, DateTime startDate, DateTime endDate)
        {
            var minDate = new DateTime(1970, 01, 01);
            if (string.IsNullOrEmpty(name) || startDate <= minDate || endDate <= minDate)
            {
                return new BookingResult()
                {
                    Message = "All fields are mandatory, please fill in all fields before continuing."
                };
            }

            if (startDate > endDate)
            {
                return new BookingResult()
                {
                    Message = "The training start date must be earlier than the end date, please try again."
                };
            }

            _repository.Add(new Training()
            {
                TrainingName = name,
                StartDate = startDate,
                EndDate = endDate
            });

            var trainingDuration = (endDate - startDate).Days + 1;
            return new BookingResult()
            {
                Message = $"The training has been booked successfully. Training duration: {trainingDuration} days.",
                TrainingDurationInDays = trainingDuration,
                IsSuccess = true
            };
        }
    }
}
