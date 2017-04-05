using FisherInsuranceApi.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FisherInsuranceApi.Security
{
    public class JwtProvider
    {
        private readonly RequestDelegate _next;
        private TimeSpan TokenExpiration;
        private SigningCredentials SigningCredentials;
        private FisherContext db;
        private UserManager<ApplicationUser> UserManager;
        private SignInManager<ApplicationUser> SignInManager;

        
        private static readonly string PrivateKey = "private_key_1234567890"; //never do this IRL; use something like https://vaultproject.io 
        public static readonly SymmetricSecurityKey SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(PrivateKey));

        public static readonly string Issuer = "FisherInsurance";
        public static string TokenEndPoint = "api/connect/token";

        public JwtProvider(RequestDelegate next, FisherContext db, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _next = next;

           
            this.db = db;
            UserManager = userManager;
            SignInManager = signInManager;

            //Configure JWT Token settings
            TokenExpiration = TimeSpan.FromMinutes(10);
            SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

        }


        public JwtProvider(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            if(!httpContext.Request.Path.Equals(TokenEndPoint, StringComparison.Ordinal))
            {
                return _next(httpContext);
            }

            if (httpContext.Request.Method.Equals("POST") && httpContext.Request.HasFormContentType)
            {
                return CreateToken(httpContext);
            }
            else
            {
                httpContext.Response.StatusCode = 400;
                return httpContext.Response.WriteAsync("Bad Request.");
            }
        }

        private async Task CreateToken(HttpContext httpContext)
        {
            try
            {
                //get form POST data
                string username = httpContext.Request.Form["username"];
                string password = httpContext.Request.Form["password"];

                //check username
                var user = await UserManager.FindByNameAsync(username);

                //if we don't find with username, try email
                if (user == null && username.Contains("@"))
                {
                    user = await UserManager.FindByEmailAsync(username);
                }

                var success = user != null && await UserManager.CheckPasswordAsync(user, password);
                if (success)
                {
                    DateTime now = DateTime.UtcNow;

                    //create the claims about the user for the token
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Iss, Issuer),
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now)
                                                                    .ToUnixTimeSeconds()
                                                                    .ToString(), ClaimValueTypes.Integer64),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email), 
                        new Claim(JwtRegisteredClaimNames.GivenName, user.UserName)
                    };

                    //create the actual token
                    var token = new JwtSecurityToken(
                        claims: claims,
                        notBefore: now,
                        expires: now.Add(TokenExpiration),
                        signingCredentials: SigningCredentials);

                    var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

                    //create the response. This is an anonymous type; no need to create a special class to hold two props.
                    var jwt = new
                    {
                        access_token = encodedToken,
                        expiration = (int)TokenExpiration.TotalSeconds
                    };

                    httpContext.Response.ContentType = "application/json"; // this should look familiar
                    await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(jwt));

                    return;
                }

            }
            catch (Exception)
            {
                throw;
            }

            //if we make it this far, we could not authenticate the user
            httpContext.Response.StatusCode = 400;
            await httpContext.Response.WriteAsync("Invalid username or password.");

        }
    }

    public static class JwtProviderExtensions
    {
        public static IApplicationBuilder UseJwtProvider(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtProvider>();
        }
    }
}