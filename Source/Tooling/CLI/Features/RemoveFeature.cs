// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.CommandLine.Invocation;
using System.Linq;
using System.Threading.Tasks;
using Dolittle.Vanir.Backend.Features;
using Dolittle.Vanir.CLI.Contexts;

namespace Dolittle.Vanir.CLI.Features
{
    public class RemoveFeature : AlterFeaturesCommandHandler
    {
        public const string NameArgument = "name";

        public RemoveFeature(
            ContextOf<ApplicationContext> getApplicationContext,
            ContextOf<MicroserviceContext> getMicroserviceContext,
            ContextOf<FeaturesContext> getFeaturesContext) : base(
                getApplicationContext,
                getMicroserviceContext,
                getFeaturesContext
            )
        {
        }

        protected override Task<int> Invoke(InvocationContext context, IDictionary<string, Feature> features)
        {
            var name = context.ParseResult.ValueForArgument<string>(NameArgument);
            var feature = features.Values.SingleOrDefault(_ => _.Name == name);
            if (feature == default)
            {
                context.Console.Error.Write($"Feature '{name}' does not exist");
                return Task.FromResult(-1);
            }

            features.Remove(name);

            return Task.FromResult(0);
        }
    }
}
