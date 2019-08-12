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
    {
        [Fact]
        public async Task SuccessBookingTest()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("http://localhost:50541/api/Training/BookTraining"
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
