using System.ComponentModel.DataAnnotations;

namespace PetProject.WebAPI.Models.Requestes
{
    public class SignInRequestModel
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
