using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using RMSApp.Entities.POCO;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RMSApp.Tests
{
    public class IntegrationTests
        : IClassFixture<WebApplicationFactory<RMSApp.Startup>>
    {
        private readonly WebApplicationFactory<RMSApp.Startup> _factory;

        public IntegrationTests(WebApplicationFactory<RMSApp.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task MandatoryInputsTest()
        {
            using (var client = _factory.CreateClient())
            {
                var response = await client.PostAsync("/api/Training/BookTraining"
                        , new StringContent(
                        JsonConvert.SerializeObject(new TrainingInfo()
                        {
                            TrainingName = "",
                            StartDate = DateTime.MinValue,
                            EndDate = DateTime.MinValue
                        }),
                    Encoding.UTF8,
                    "application/json"));

                response.EnsureSuccessStatusCode();

                // Deserialize and examine results.
                var stringResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<BookingResult>(stringResponse);
                var message = "All fields are mandatory, please fill in all fields before continuing.";
                Assert.True(!result.IsSuccess && result.Message == message, $"No inputs - error message should be {message}");
            }
        }

        [Fact]
        public async Task InvalidStartDateTest()
        {
            using (var client = _factory.CreateClient())
            {
                var response = await client.PostAsync("/api/Training/BookTraining"
                        , new StringContent(
                        JsonConvert.SerializeObject(new TrainingInfo()
                        {
                            TrainingName = "Training Day",
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(-2)
                        }),
                    Encoding.UTF8,
                    "application/json"));

                response.EnsureSuccessStatusCode();

                // Deserialize and examine results.
                var stringResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<BookingResult>(stringResponse);
                var message = "The training start date must be earlier than the end date, please try again.";
                Assert.True(!result.IsSuccess && result.Message == message, $"Invalid start date and end date - error message should be {message}");
            }
        }

        [Fact]
        public async Task SuccessBookingTest()
        {
            using (var client = _factory.CreateClient())
            {
                var response = await client.PostAsync("/api/Training/BookTraining"
                        , new StringContent(
                        JsonConvert.SerializeObject(new TrainingInfo()
                        {
                            TrainingName = "Training Day",
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(3)
                        }),
                    Encoding.UTF8,
                    "application/json"));

                response.EnsureSuccessStatusCode();

                // Deserialize and examine results.
                var stringResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<BookingResult>(stringResponse);
                var message = $"The training has been booked successfully. Training duration: {result.TrainingDurationInDays} days.";
                Assert.True(result.IsSuccess && result.Message == message, $"Successful booking - success message should be {message}");
            }
        }
    }
}
