// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Reflection;

namespace Dolittle.Vanir.Backend.GraphQL
{
    /// <summary>
    /// Extension methods for working with types of <see cref="GraphController"/>.
    /// </summary>
    public static class GraphControllerTypeExtensions
    {
        /// <summary>
        /// Get root path for a <see cref="GraphController"/>
        /// </summary>
        /// <param name="type">Type of <see cref="GraphController"/> to get for.</param>
        /// <returns>The root path - empty string if none specified.</returns>
        public static string GetRootPath(this Type type)
        {
            var attribute = type.GetCustomAttribute<GraphRootAttribute>();
            return attribute?.Path ?? string.Empty;
        }
    }
}
