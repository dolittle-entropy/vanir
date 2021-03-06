// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

import { container } from 'tsyringe';
import { constructor } from '@dolittle/vanir-dependency-inversion';
import { createLogger, format, transports } from 'winston';
import { ILogger } from './ILogger';

const loggerOptions = {
    level: 'info',
    format: format.colorize(),
    transports: [
        new transports.Console({
            format: format.simple()
        })
    ]
};

export const logger: ILogger = createLogger(loggerOptions);
container.registerInstance(ILogger as constructor<ILogger>, logger);
