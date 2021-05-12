// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dolittle.Vanir.CLI.ProxyGenerator
{
    public interface IProxyType
    {
        string Name { get; }
        string Namespace { get; }
        string FilePathForImports { get; }
    }
}