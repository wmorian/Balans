using System;
using Xunit;

namespace Ghis.EventAggregator.Lib.Tests
{
    public class DTM12EventAggregatorTests
    {
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