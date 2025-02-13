﻿namespace IWantApp.EndPoints.Security {
    public class TokenPost {

        public static string Template => "/tokens";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;

        [AllowAnonymous]
        public static IResult Action(LoginRequest loginRequest, IConfiguration configuration, UserManager<IdentityUser> userManager) {
           
            var user = userManager.FindByEmailAsync(loginRequest.Email).Result;
            if (user == null)
                Results.BadRequest();

            if (!userManager.CheckPasswordAsync(user, loginRequest.Password).Result)
                Results.BadRequest();

            var claims = userManager.GetClaimsAsync(user).Result;
            var subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Email, loginRequest.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                });
            subject.AddClaims(claims);
            var key = Encoding.ASCII.GetBytes(configuration["JwtBearerTokenSettings:SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = subject,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = configuration["JwtBearerTokenSettings:Audience"],
                Issuer = configuration["JwtBearerTokenSettings:Issuer"],
                Expires = DateTime.UtcNow.AddSeconds(3000)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Results.Ok(new {
                token = tokenHandler.WriteToken(token),
            });
        }

    }
}
