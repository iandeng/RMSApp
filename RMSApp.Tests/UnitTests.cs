using RMSApp.Service;
using System;
using Xunit;

namespace RMSApp.Tests
{
    public class UnitTests
    {
        private readonly TrainingService _trainingService;

        public UnitTests()
        {
            _trainingService = new TrainingService();
        }

        [Fact]
        public void MandatoryInputsTest()
        {
            var message = "All fields are mandatory, please fill in all fields before continuing.";
            var result =_trainingService.CreateTrainingBooking("", DateTime.MinValue, DateTime.MinValue);
            Assert.True(!result.IsSuccess && result.Message == message, $"No inputs - error message should be {message}");

            result = _trainingService.CreateTrainingBooking("Training Day", DateTime.MinValue, DateTime.Now);
            Assert.True(!result.IsSuccess && result.Message == message, $"One missing input - error message should be {message}");
        }

        [Fact]
        public void InvalidStartDateTest()
        {
            var message = "The training start date must be earlier than the end date, please try again.";
            var result = _trainingService.CreateTrainingBooking("Training Day", DateTime.Now, DateTime.Now.AddDays(-2));
            Assert.True(!result.IsSuccess && result.Message == message, $"Invalid start date and end date - error message should be {message}");
        }

        [Fact]
        public void SuccessBookingTest()
        {
            var result = _trainingService.CreateTrainingBooking("Training Day", DateTime.Now, DateTime.Now.AddDays(3));
            var message = $"The training has been booked successfully. Training duration: {result.TrainingDurationInDays} days.";
            Assert.True(result.IsSuccess && result.Message == message, $"Successful booking - success message should be {message}");
        }
    }
}
