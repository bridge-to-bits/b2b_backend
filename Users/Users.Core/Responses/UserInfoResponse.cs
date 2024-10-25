namespace Users.Core.Responses
{
    public class UserInfoResponse
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string City { get; set; }
        public string Avatar { get; set; }
        public double Rating { get; set; }
    }
}