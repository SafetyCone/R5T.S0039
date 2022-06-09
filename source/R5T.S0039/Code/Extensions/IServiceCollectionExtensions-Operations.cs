using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.Lombardy;

using R5T.D0083;
using R5T.D0105;
using R5T.T0063;


namespace R5T.S0039
{
    public static partial class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the <see cref="O000_Main"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO000_Main(this IServiceCollection services,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<IStringlyTypedPathOperator> stringlyTypedPathOperatorAction,
            IServiceAction<IVisualStudioProjectFileReferencesProvider> visualStudioProjectFileReferencesProviderAction)
        {
            services
                .Run(notepadPlusPlusOperatorAction)
                .Run(stringlyTypedPathOperatorAction)
                .Run(visualStudioProjectFileReferencesProviderAction)
                .AddSingleton<O000_Main>();

            return services;
        }
    }
}