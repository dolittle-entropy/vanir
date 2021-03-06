// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

import React from 'react';
import { useHistory } from 'react-router-dom';
import { withViewModel } from '../mvvm';
import { RoutingViewModel } from './RoutingViewModel';

export interface RoutingProps {
    children?: React.ReactNode[] | React.ReactNode;
}

export const Routing = withViewModel<RoutingViewModel, RoutingProps>(RoutingViewModel, ({ viewModel, props }) => {
    const history = useHistory();

    viewModel.navigated = (path: string) => {
        history.push(path);
    };

    return (
        <>
            {props.children}
        </>
    );
});
