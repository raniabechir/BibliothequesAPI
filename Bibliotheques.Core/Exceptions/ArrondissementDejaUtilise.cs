namespace Bibliotheques.Core.Exceptions
{
    public class ArrondissementDejaUtiliseException : ValidationException
    {
        public ArrondissementDejaUtiliseException()
            : base("Arrondisement ", "Une bibliothèque existe déjà dans cet arrondissement.")
        {
        }
    }
}
