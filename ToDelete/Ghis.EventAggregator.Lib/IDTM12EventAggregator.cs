using System;

namespace Ghis.EventAggregator.Lib
{
    /// <summary>
    /// Defines an interface to get instances of an observable of event type and publishes a value of
    /// an specify event type.
    /// </summary>
    public interface IDTM12EventAggregator
    {
        /// <summary>
        /// Gets an instance of observable of event type.
        /// </summary>
        /// <typeparam name="TEvent">The desired event type</typeparam>
        /// <returns>An instance of an IObservable of event type</returns>
        IObservable<TEvent> GetEvent<TEvent>();

        /// <summary>
        /// Publishes an instance of an event value.
        /// </summary>
        /// <typeparam name="TEvent">An specific event type</typeparam>
        /// <param name="sampleEvent">A value of event</param>
        void Publish<TEvent>(TEvent sampleEvent);
    }
}