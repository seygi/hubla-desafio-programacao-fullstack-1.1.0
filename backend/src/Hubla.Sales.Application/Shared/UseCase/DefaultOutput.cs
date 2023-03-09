using System.Diagnostics.CodeAnalysis;

namespace Hubla.Sales.Application.Shared.UseCase
{
    [ExcludeFromCodeCoverage]
    public sealed class DefaultOutput : IOutput
    {
        private DefaultOutput()
        {
        }

        public static DefaultOutput Default => new();
    }
}