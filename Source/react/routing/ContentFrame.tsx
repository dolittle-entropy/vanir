// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

import { constructor } from '@dolittle/vanir-dependency-inversion';
import { IMessenger } from '@dolittle/vanir-web';
import React from 'react';
import { container } from 'tsyringe';

export type ContentFrameProps = {
    src: string;
    load?: Function;
    loaded?: Function;
};

export const ContentFrame = (props: ContentFrameProps) => {
    const iframeRef = React.createRef<HTMLIFrameElement>();
    const iframe = React.createElement('iframe', {
        ref: iframeRef,
        src: props.src,
        async: true,
        style: {
            backgroundColor: 'transparent'
        },
        frameBorder: 1,
        allowFullScreen: true,
        onLoadStart: () => props.load?.(),
        onLoad: () => {
            if (iframeRef && iframeRef.current && iframeRef.current.contentDocument) {
                container.resolve(IMessenger as constructor<IMessenger>).setCurrentContentDocument(iframeRef.current.contentDocument);
            }
            props.loaded?.();
        }
    });

    return iframe;
};
