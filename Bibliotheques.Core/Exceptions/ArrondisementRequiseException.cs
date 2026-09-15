

namespace Bibliotheques.Core.Exceptions
{
    public class ArrondisementRequiseException : ValidationException
    {
        public ArrondisementRequiseException()
            : base("Adresse", "L'adresse est requise et ne peut pas être vide.") { }
    }
}
