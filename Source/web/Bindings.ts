// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

import { ApolloClient } from 'apollo-client';
import { HttpLink } from 'apollo-link-http';
import { InMemoryCache } from 'apollo-cache-inmemory';
import { container } from 'tsyringe';
import { constructor } from '@dolittle/vanir-dependency-inversion';
import { DataSource } from './DataSource';
import { Configuration } from './Configuration';
import { INavigator, Navigator } from './routing';
import { Bindings as MessagingBindings } from './messaging/Bindings';

export class Bindings {
    static initialize(configuration: Configuration) {
        const cache = new InMemoryCache();
        const link = new HttpLink({
            uri: `${configuration.prefix}/graphql`
        });

        const client = new ApolloClient({
            cache,
            link,
            name: `${configuration.name} Client`,
            version: configuration.versionInfo.version,
            queryDeduplication: false,
            defaultOptions: {
                watchQuery: {
                    fetchPolicy: 'cache-and-network'
                }
            }
        });

        container.registerInstance(DataSource as constructor<DataSource>, client);

        container.registerSingleton(INavigator as constructor<INavigator>, Navigator);

        container.registerInstance(History, window.history);

        MessagingBindings.initialize();
    }
}