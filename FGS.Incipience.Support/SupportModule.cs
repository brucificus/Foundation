using Autofac;

using DiffPlex;
using DiffPlex.DiffBuilder;

using FGS.Incipience.Support.Abstractions;

using Microsoft.AspNetCore.Mvc.Filters;

using Support.Validation;

namespace FGS.Incipience.Support
{
    public class SupportModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ValidateModelStateFilter>().As<IActionFilter>();
            builder.RegisterType<Clock>().As<IClock>();
            builder.RegisterType<SideBySideDiffBuilder>().As<ISideBySideDiffBuilder>();
            builder.RegisterType<Differ>().As<IDiffer>();
        }
    }
}
