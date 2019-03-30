using System;
using System.Collections.Concurrent;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Ghis.EventAggregator.Lib
{
    public class DTM12EventAggregator : IDTM12EventAggregator
    {
        /// <summary>
        /// Provides and stores a specific event type by given an event type
        /// </summary>
        private readonly ConcurrentDictionary<Type, object> concurrentDictionary = new ConcurrentDictionary<Type, object>();

        /// <summary>
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <returns></returns>
        public IObservable<TEvent> GetEvent<TEvent>()
        {
            var subject = concurrentDictionary.GetOrAdd(typeof(TEvent), t => new Subject<TEvent>());
            var subjectEvent = (ISubject<TEvent>)subject;
            return subjectEvent.AsObservable();
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="payloadEvent"></param>
        public void Publish<TEvent>(TEvent payloadEvent)
        {
            object subject;
            if (concurrentDictionary.TryGetValue(typeof(TEvent), out subject))
            {
                ((ISubject<TEvent>)subject).OnNext(payloadEvent);
            }
        }
    }
}