namespace Hubla.Sales.Application.Shared.Models
{
    public class OperationResult
    {
        public bool HasRowsAffected { get; }

        public bool HasUnexpectedError { get; }

        public int RowsAffected { get; }

        private OperationResult(bool hasRowsAffected, bool hasUnexpectedError, int affectedRowsCount)
        {
            HasRowsAffected = hasRowsAffected;
            HasUnexpectedError = hasUnexpectedError;
            RowsAffected = affectedRowsCount;
        }

        public static OperationResult Success(int affectedRows) => new(affectedRows > 0, false, affectedRows);

        public static OperationResult Error => new(false, true, 0);
    }
}