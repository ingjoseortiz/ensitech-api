namespace JWTBearer.Models
{
    public class User
    {
        public string Id { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;

        public static List<User> GetUsers()
        {

            var users = new List<User>() {
                new User{
                    Id = "1",
                    UserName = "Jose",
                    Password = "6532r435a4t654t",
                    Rol = "client",
                },
                new User{
                    Id = "2",
                    UserName = "Luis",
                    Password = "56a4d46a4ds4",
                    Rol = "client",
                },
                new User{
                    Id = "3",
                    UserName = "Gabriel",
                    Password = "5ea4d46a4ds!",
                    Rol = "administrator",
                }
            };
            return users;
        }
    }
}
