using System.Security.Claims;
using JWTBearer.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace EnsitechLibrary.Models
{
    public class Jwt
    {
        public string Key { get; set; } = "";
        public string Issuer { get; set; } = "";
        public string Audience { get; set; } = "";
        public string Subject { get; set; } = "";

        public static dynamic ValidateToken(ClaimsIdentity identity)
        {
            try {
                if (identity.Claims.Count() == 0)
                {
                    return new
                    {
                        success = false,
                        message = "Verify token sent is valid",
                        result = ""
                    };
                }
                var id = identity.Claims.FirstOrDefault(x => x.Type == "Id").Value;

                User user = User.GetUsers().FirstOrDefault(x => x.Id == id);
                return new                 {
                    success = true,
                    message = "Success",
                    result = $"{user}"
                };
            }
            catch(Exception ex) {
                return new
                {
                    success = false,
                    message = $"catch {ex.Message}",
                    result = ""
                };
            }
        }
    }

}
