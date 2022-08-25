namespace PetProject.WebAPI.Models.Responses
{
    public class TokenModel
    {
        public string? Token { get; set; }
        public string? Type { get; set; }
        public double ExpiredTime { get; set; }
    }
}
