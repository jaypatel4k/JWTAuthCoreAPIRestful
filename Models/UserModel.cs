namespace JWTAuthCoreAPIRestful.Models
{
    public class UserModel : BaseEntity
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public string? EmailAddress { get; set; }
        public DateTime DateOfJoing { get; set; }
    }

}
