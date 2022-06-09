using System;

using R5T.Lombardy;

using R5T.D0083;
using R5T.D0105;
using R5T.T0062;
using R5T.T0063;


namespace R5T.S0039
{
    public static partial class IServiceActionExtensions
    {
        /// <summary>
        /// Adds the <see cref="O000_Main"/> operation as a <see cref="Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O000_Main> AddO000_MainAction(this IServiceAction _,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<IStringlyTypedPathOperator> stringlyTypedPathOperatorAction,
            IServiceAction<IVisualStudioProjectFileReferencesProvider> visualStudioProjectFileReferencesProviderAction)
        {
            var serviceAction = _.New<O000_Main>(services => services.AddO000_Main(
                notepadPlusPlusOperatorAction,
                stringlyTypedPathOperatorAction,
                visualStudioProjectFileReferencesProviderAction));

            return serviceAction;
        }
    }
}