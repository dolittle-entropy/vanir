// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;
using Dolittle.SDK.Events;
using HotChocolate;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Dolittle.Vanir.Backend.Dolittle
{
    public class EventStreamSubscription
    {
        public class EventForStream
        {
            public string Content { get; set; }
        }

        static ITopicEventSender _sender;
        static ITopicEventSender Sender => _sender ??= Container.ServiceProvider.GetService<ITopicEventSender>();

        public static async Task EventHandler(object @event, EventContext _)
        {
            await Sender.SendAsync("newEvent", new EventForStream
            {
                Content = JsonConvert.SerializeObject(@event)
            }).ConfigureAwait(false);
        }

        [Subscribe(MessageType = typeof(EventForStream))]
        [Topic("newEvent")]
#pragma warning disable CA1707
        public Task<EventForStream> system_eventStream([EventMessage] EventForStream @event) => Task.FromResult(@event);
#pragma warning restore
    }
}
