// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using FluentValidation;

namespace Dolittle.Vanir.Backend.GraphQL.Validation
{
    public interface IValidators
    {
        bool HasFor(Type type);
        IValidator GetFor(Type type);
    }
}