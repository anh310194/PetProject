
namespace PetProject.Core.Exceptions
{
    public class PetProjectException: Exception
    {
        public string Message;
        public PetProjectException(string message)
        {
            this.Message = message;
        }
    }
}
