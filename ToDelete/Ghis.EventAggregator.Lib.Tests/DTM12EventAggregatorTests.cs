using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Ghis.EventAggregator.Lib.Tests
{
    public class DTM12EventAggregatorTests
    {
        /// <summary>
        /// Selective subscription by using where clausel of linq.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task A_valid_message_is_invoked_on_the_supplied_selective_subscription()
        {
            // Arrange
            int actualId = 0;
            int expectedId = 50;
            int total = 500;
            var simpleEventArray = new SimpleEvent[total];
            for (int k = 0; k < total; k++)
            {
                var eventToPushish = new SimpleEvent() { Id = k, Message = "PROFINET_DTM Nr." + k };
                simpleEventArray[k] = eventToPushish;
            }
            string message = string.Empty;
            var eventPublisher = new DTM12EventAggregator();

            eventPublisher.GetEvent<SimpleEvent>()
                .Where(se => se.Id == 50)
                .Subscribe(se => actualId = se.Id);

            // Act
            int i = 0;
            while (i < total)
            {
                await Task.Delay(100);
                eventPublisher.Publish<SimpleEvent>(simpleEventArray[i]);
                i++;
            }

            //Assert
            Assert.Equal(actualId, expectedId);
        }

        [Fact]
        public void A_valid_message_is_invoked_on_the_supplied_subscription()
        {
            //arrange
            var eventToPushish = new SimpleEvent() { Message = "PROFINET_DTM" };
            string message = string.Empty;
            var eventPublisher = new DTM12EventAggregator();

            eventPublisher.GetEvent<SimpleEvent>().Subscribe(se => message = se.Message);
            eventPublisher.Publish<SimpleEvent>(eventToPushish);

            //Assert
            Assert.Same(message, eventToPushish.Message);
        }

        /// <summary>
        /// Simple subscription test using TEvent as int
        /// </summary>
        [Fact]
        public void A_valid_subscriber_is_assigned_to_valide_publisher_int()
        {
            bool eventWasRaised = false;
            var eventPublisher = new DTM12EventAggregator();

            eventPublisher.GetEvent<int>()
                .Subscribe(se => eventWasRaised = true);

            eventPublisher.Publish(12);

            Assert.True(eventWasRaised);
        }

        [Fact]
        public void A_valid_subscriber_is_assigned_to_valide_publisher_String()
        {
            //Arrange
            bool eventWasRaised = false;
            var eventPublisher = new DTM12EventAggregator();

            eventPublisher.GetEvent<string>()
                .Subscribe(se => eventWasRaised = true);

            //Act
            eventPublisher.Publish(" I m a single character collection");

            //Assert
            Assert.True(eventWasRaised);
        }

        private class SimpleEvent
        {
            public int Id { get; set; }
            public string Message { get; set; }
        }
    }
}