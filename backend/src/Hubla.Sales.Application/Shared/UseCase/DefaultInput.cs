using System.Diagnostics.CodeAnalysis;

namespace Hubla.Sales.Application.Shared.UseCase
{
    [ExcludeFromCodeCoverage]
    public sealed class DefaultInput : IInput
    {
        private DefaultInput()
        {
        }

        public static DefaultInput Default => new();
    }
}