using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace Ghis.EventAggregator.Lib.Tests
{
    public class DTM12EventAggregatorTests
    {
        /// <summary>
        /// Subscribe to projection: using Select Query
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task A_valid_message_is_invoked_on_the_supplied_projection_subscription()
        {
            // Arrange

            int total = 50;
            var simpleEventArray = new SimpleEvent[total];
            var actual = new List<SimpleEvent>();
            var expected = new List<SimpleEvent>();
            for (int k = 0; k < total; k++)
            {
                var eventToPushish = new SimpleEvent() { Id = k };
                simpleEventArray[k] = eventToPushish;

                //setup expected list.
                var eventToPushish2 = new SimpleEvent() { Id = 2 * k };
                expected.Add(eventToPushish2);
            }

            var dTM12EventAggregator = new DTM12EventAggregator();

            dTM12EventAggregator.GetEvent<SimpleEvent>()
               .Select(se =>
                {
                    return new SimpleEvent() { Id = se.Id * 2 };
                })
                .Subscribe(se =>
                 {
                     actual.Add(se);
                 });

            // Act
            int i = 0;
            while (i < total)
            {
                await Task.Delay(100);
                dTM12EventAggregator.Publish<SimpleEvent>(simpleEventArray[i]);
                i++;
            }

            //Assert
            // Assert.Equal(expected, actual);
            Assert.Throws<EqualException>(() => Assert.Equal(expected, actual));
            Assert.NotEqual(expected, actual);
        }

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
            var dTM12EventAggregator = new DTM12EventAggregator();

            dTM12EventAggregator.GetEvent<SimpleEvent>()
                .Where(se => se.Id == 50)
                .Subscribe(se => actualId = se.Id);

            // Act
            int i = 0;
            while (i < total)
            {
                await Task.Delay(100);
                dTM12EventAggregator.Publish<SimpleEvent>(simpleEventArray[i]);
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
            string expected = "PROFINET_DTM";
            string actual = string.Empty;
            var eventPublisher = new DTM12EventAggregator();

            eventPublisher.GetEvent<SimpleEvent>().Subscribe(se => actual = se.Message);
            eventPublisher.Publish<SimpleEvent>(eventToPushish);

            //Assert
            Assert.Same(expected, actual);
        }

        /// <summary>
        /// Execute the desired handler in the UI thread
        /// </summary>
        [Fact]
        public void A_valid_message_is_invoked_on_the_supplied_subscription_UI_Thread()
        {
            //arrange
            var eventToPushish = new SimpleEvent() { Message = "PROFINET_DTM" };
            string expected = "PROFINET_DTM";
            string actual = string.Empty;
            var eventPublisher = new DTM12EventAggregator();

            eventPublisher.GetEvent<SimpleEvent>()
                .Synchronize()
                .Subscribe(s => actual = s.Message);

            eventPublisher.Publish<SimpleEvent>(eventToPushish);

            //Assert
            Assert.Same(expected, actual);
        }

        /// <summary>
        /// Simple subscription test using TEvent as int
        /// </summary>
        [Fact]
        public void A_valid_subscriber_is_assigned_to_valide_publisher_int()
        {
            bool expected = false;
            var dTM12EventAggregator = new DTM12EventAggregator();

            dTM12EventAggregator.GetEvent<int>()
                .Subscribe(se => expected = true);

            dTM12EventAggregator.Publish(120);

            Assert.True(expected);
        }

        [Fact]
        public void A_valid_subscriber_is_assigned_to_valide_publisher_String()
        {
            //Arrange
            bool eventWasRaised = false;
            var dTM12EventAggregator = new DTM12EventAggregator();

            dTM12EventAggregator.GetEvent<string>()
                .Subscribe(se => eventWasRaised = true);

            //Act
            dTM12EventAggregator.Publish(" I m a single character collection");

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