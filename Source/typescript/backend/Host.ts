// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

import 'reflect-metadata';

import path from 'path';
import dotenv from 'dotenv';

import * as Dolittle from './dolittle';
import * as Features from './features';
import * as Express from './web';
import * as DependencyInversion from '@dolittle/vanir-dependency-inversion';

export { logger } from './logging';

import '@dolittle/projections';
import { Configuration } from './Configuration';
import { BackendArguments } from './BackendArguments';
import { container } from 'tsyringe';

import { MongoDb } from './mongodb';
import { Resources } from './resources';
import { HostContext } from './HostContext';

export class Host {
    static async start(startArguments: BackendArguments) {
        HostContext.run(async () => {
            const envPath = path.resolve(process.cwd(), '.env');
            dotenv.config({ path: envPath });
            const configuration = Configuration.create();

            DependencyInversion.initialize();

            container.registerInstance(Configuration, configuration);

            MongoDb.initialize();
            await Resources.initialize();
            await Dolittle.initialize(configuration, startArguments);
            Features.initialize();
            await Express.initialize(configuration, startArguments);
        });
    }
}
