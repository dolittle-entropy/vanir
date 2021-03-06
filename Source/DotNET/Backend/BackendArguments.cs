// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.SDK;
using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using MongoDB.Driver;

namespace Microsoft.Extensions.DependencyInjection
{
    public class BackendArguments
    {
        public ILoggerFactory LoggerFactory { get; set; } = new NullLoggerFactory();
        public Action<IRequestExecutorBuilder> GraphQLExecutorBuilder { get; set; } = (_) => { };
        public Action<ClientBuilder> DolittleClientBuilderCallback { get; set; } = (_) => { };
        public Action<MongoClientSettings> MongoClientSettingsCallback { get; set; } = (_) => { };

        /// <summary>
        /// Setting indicating whether or not to automatically forward all public events over the Dolittle Event Horizon or not.
        /// </summary>
        /// <remarks>
        /// If you want to be more specific - you set this to false and do the filtering manually; configured through the <see cref="DolittleClientBuilderCallback"/>
        /// callback.
        /// </remarks>
        public bool PublishAllPublicEvents { get; set; } = true;

        /// <summary>
        /// Indicating whether or not to automatically expose events in GraphQL schema when running in development.
        /// </summary>
        /// <remarks>
        /// Defaults to true.
        /// </remarks>
        public bool ExposeEventsInGraphQLSchema { get; set; } = true;
    }
}
