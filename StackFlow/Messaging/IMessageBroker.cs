using System;
using System.Collections.Generic;
using System.Text;

namespace StackFlow.Messaging
{
    public interface IMessageBroker : IDisposable
    {
        /// <summary>
        /// Post (publish) a message to the message broker, which will distribute it to subscribers
        /// </summary>
        /// <typeparam name="T">The Type parameter of the IMessage</typeparam>
        /// <param name="Message">The message itself</param>
        /// <returns><see cref="IMessage{T}"/> with a json string describing the result of the publish in the form:
        /// {"Status"="Success/Failure"; "NumberofSubscribers"="<see cref="int"/>"}</returns>
        IMessage<string> Publish<T>(IMessage<T> Message);
        /// <summary>
        /// Subscribes the caller to IMessage of type T. 
        /// </summary>
        /// <typeparam name="T">The type parameter of the IMessage to subscribe to</typeparam>
        /// <param name="Subscription">The delegate to invoke when a message is posted for the subscriber</param>
        /// <returns>true if succesfully subscribed, else false</returns>
        bool Subscribe<T>(Action<IMessage<T>> Subscription);
        /// <summary>
        /// Unsubscribes from the given type of message
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Subscription"></param>
        /// <returns>true if succesfully unsubscribed, else false</returns>
        bool UnSubscribe<T>(Action<IMessage<T>> Subscription);
    }
}
