// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

import { IMessenger, MicroserviceConfiguration, NavigatedTo } from '@dolittle/vanir-web';
import { injectable } from 'tsyringe';

@injectable()
export class RoutingViewModel {
    currentPath: string;

    navigated!: (path: string) => void;

    constructor(readonly microserviceConfiguration: MicroserviceConfiguration, private readonly _messenger: IMessenger) {
        this.currentPath = microserviceConfiguration.prefix;
        _messenger.subscribeTo(NavigatedTo, _ => {
            let path = _.path;
            if (path.startsWith('/')) {
                path = path.substr(1);
            }

            const actualPath = `/_/${path}`;
            this.navigated?.(actualPath);
        });
    }

    activate() {

    }
}
