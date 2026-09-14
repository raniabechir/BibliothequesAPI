namespace Bibliotheques.Core.Exceptions
{
    public class NomInvalideException : ValidationException
    {
        public NomInvalideException(string nom)
        : base("Nom", $"Le nom '{nom}' est invalide. Il doit contenir entre 2 et 100 caractères.") { }
    }
}
