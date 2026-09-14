namespace Bibliotheques.Core.Exceptions
{
    public abstract class ValidationException : Exception
    {
        public string Champ { get; }

        protected ValidationException(string champ, string message) : base(message)
        {
            Champ = champ;
        }
    }
}
