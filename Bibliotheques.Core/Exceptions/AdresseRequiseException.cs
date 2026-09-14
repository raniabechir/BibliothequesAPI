

namespace Bibliotheques.Core.Exceptions
{
    public class AdresseRequiseException : ValidationException
    {
        public AdresseRequiseException()
            : base("Adresse", "L'adresse est requise et ne peut pas être vide.") { }
    }
}
