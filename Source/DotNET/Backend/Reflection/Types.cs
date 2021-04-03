// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;

namespace Dolittle.Vanir.Backend.Reflection
{
    /// <summary>
    /// Represents an implementation of <see cref="ITypes"/>
    /// </summary>
    public class Types : ITypes
    {
        private readonly IContractToImplementorsMap  _contractToImplementorsMap = new ContractToImplementorsMap();

        /// <summary>
        /// Initializes a new instance of <see cref="Types"/>
        /// </summary>
        public Types()
        {
            Populate();
            _contractToImplementorsMap.Feed(All);
        }

        /// <inheritdoc/>
        public IEnumerable<Type> All { get; private set; }

        /// <inheritdoc/>
        public Type FindSingle<T>()
        {
            var type = FindSingle(typeof(T));
            return type;
        }

        /// <inheritdoc/>
        public IEnumerable<Type> FindMultiple<T>()
        {
            var typesFound = FindMultiple(typeof(T));
            return typesFound;
        }

        /// <inheritdoc/>
        public Type FindSingle(Type type)
        {
            var typesFound = _contractToImplementorsMap.GetImplementorsFor(type);
            ThrowIfMultipleTypesFound(type, typesFound);
            return typesFound.SingleOrDefault();
        }

        /// <inheritdoc/>
        public IEnumerable<Type> FindMultiple(Type type)
        {
            var typesFound = _contractToImplementorsMap.GetImplementorsFor(type);
            return typesFound;
        }

        /// <inheritdoc/>
        public Type FindTypeByFullName(string fullName)
        {
            var typeFound = _contractToImplementorsMap.All.SingleOrDefault(t => t.FullName == fullName);
            ThrowIfTypeNotFound(fullName, typeFound);
            return typeFound;
        }

        void ThrowIfMultipleTypesFound(Type type, IEnumerable<Type> typesFound)
        {
            if (typesFound.Count() > 1)
                throw new MultipleTypesFound(type, typesFound);
        }

        void ThrowIfTypeNotFound(string fullName, Type typeFound)
        {
            if (typeFound == null) throw new UnableToResolveTypeByName(fullName);
        }

        void Populate()
        {
            var entryAssembly = Assembly.GetEntryAssembly();
            var dependencyModel = DependencyContext.Load(entryAssembly);
            var assemblies = dependencyModel.RuntimeLibraries
                                .Where(_ => _.Type.Equals("project", StringComparison.InvariantCultureIgnoreCase))
                                .Select(_ => Assembly.Load(_.Name))
                                .ToArray();

            var types = new List<Type>();
            foreach (var assembly in assemblies)
            {
                types.AddRange(assembly.DefinedTypes);
            }

            All = types;
        }
    }
}