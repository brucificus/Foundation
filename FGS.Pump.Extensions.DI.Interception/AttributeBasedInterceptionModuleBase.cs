﻿using System;
using System.Linq;

using Castle.DynamicProxy;

namespace FGS.Pump.Extensions.DI.Interception
{
    public abstract class AttributeBasedInterceptionModuleBase<TAttribute> : CustomInterceptionModuleBase
        where TAttribute : Attribute
    {
        internal static readonly AttributeProxyGenerationHook<TAttribute> ProxyGenerationHook = new AttributeProxyGenerationHook<TAttribute>();

        protected override IProxyGenerationHook CreateProxyGenerationHook(Type originalImplementationType)
        {
            return ProxyGenerationHook;
        }

        protected override bool ShouldInterceptType(Type originalImplementationType)
        {
            return originalImplementationType.GetMethods().Any(m => ProxyGenerationHook.ShouldInterceptMethod(originalImplementationType, m));
        }
    }
}