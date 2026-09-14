namespace Bibliotheques.Core.Exceptions
{
    public class PaginationInvalideException : ValidationException
    {
        public PaginationInvalideException(string message)
        : base("Pagination", message) { }
    }
}
