
namespace PetProject.Utilities.Exceptions
{
    public class PetProjectException : Exception
    {
        public PetProjectException(string message, Exception inner) : base(message, inner) { }
        public PetProjectException(string message) : base(message) { }
    }
}
