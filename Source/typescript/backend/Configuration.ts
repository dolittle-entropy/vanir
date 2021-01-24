// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

import nconf from 'nconf';
import { DatabaseConfiguration } from './DatabaseConfiguration';
import defaults from './DefaultConfiguration';
import { DolittleConfiguration } from './DolittleConfiguration';


export class Configuration {
    routeSegment: string = '';
    isRooted: boolean = false;
    publicPath: string = '';
    port: number = 80;
    microserviceId: string = '';

    database: DatabaseConfiguration;
    eventstore: DatabaseConfiguration;

    dolittle: DolittleConfiguration;

    environment: string = 'development';

    constructor() {
        this.database = new DatabaseConfiguration();
        this.eventstore = new DatabaseConfiguration();
        this.dolittle = new DolittleConfiguration();
    }

    get isDevelopment() {
        return this.environment === 'development';
    }

    get isProduction() {
        return this.environment === 'production';
    }

    static create(): Configuration {
        nconf
            .argv({ parseValues: true })
            .env({ separator: '__', parseValues: true, lowerCase: true })
            .file({ file: 'vanir.json' })
            .defaults(defaults);

        const instance = new Configuration();
        const source = nconf.get();

        for (const property in instance) {
            if (source[property]) {
                instance[property] = source[property];
            }
        }

        instance.environment = nconf.get('node:env') || 'development';

        return instance;
    }
}
