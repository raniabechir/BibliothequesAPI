namespace Bibliotheques.Core.Exceptions
{
    public class CapaciteInvalideException : ValidationException
    {
        public CapaciteInvalideException(int capacite)
        : base("Capacite", $"La capacité de {capacite} est hors limites. Elle doit être comprise entre 1 et 5000.") { }
    }
}
