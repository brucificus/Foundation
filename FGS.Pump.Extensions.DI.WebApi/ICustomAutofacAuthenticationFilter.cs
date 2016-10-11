﻿using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace FGS.Pump.Extensions.DI.WebApi
{
    /// <summary>
    /// An authentication filter that will be created for each controller request.
    /// </summary>
    /// <remarks>Taken and modified from: https://github.com/autofac/Autofac.WebApi/blob/e3e83d6506c137e6cacbc5fdb57634d7a388a5e9/src/Autofac.Integration.WebApi/IAutofacAuthenticationFilter.cs </remarks>
    public interface ICustomAutofacAuthenticationFilter
    {
        /// <summary>
        /// Called when a request requires authentication.
        /// </summary>
        /// <param name="context">The context for the authentication.</param>
        /// <param name="cancellationToken">A cancellation token for signaling task ending.</param>
        Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken);

        /// <summary>
        /// Called when an authentication challenge is required.
        /// </summary>
        /// <param name="context">The context for the authentication challenge.</param>
        /// <param name="cancellationToken">A cancellation token for signaling task ending.</param>
        Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken);
    }
}