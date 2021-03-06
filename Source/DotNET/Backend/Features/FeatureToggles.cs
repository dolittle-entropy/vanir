// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Vanir.Backend.Features
{
    /// <summary>
    /// Represents an implementation of <see cref="IFeatureToggles"/>
    /// </summary>
    public class FeatureToggles : IFeatureToggles
    {
        Features _features;

        /// <summary>
        /// Initializes a new instance of <see cref="FeatureToggles"/>.
        /// </summary>
        /// <param name="provider"><see cref="IFeaturesProvider"/> for providing features.</param>
        public FeatureToggles(IFeaturesProvider provider)
        {
            provider.Features.Subscribe(_ => _features = _);
        }

        /// <inheritdoc/>
        public bool IsOn(string feature)
        {
            if (!_features.ContainsKey(feature)) return false;
            return _features[feature].IsOn;
        }
    }
}
