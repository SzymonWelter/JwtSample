using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using JwtSample.DataContext;
using JwtSample.TokenProviders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace JwtSample
{
    public partial class Startup
    {
        private readonly SymmetricSecurityKey _signingKey;

        private readonly TokenValidationParameters _tokenValidationParameters;

        private readonly TokenProviderOptions _tokenProviderOptions;

        private void ConfigureAuth(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options => { options.TokenValidationParameters = _tokenValidationParameters; })
                .AddCookie(options =>
                {
                    options.Cookie.Name = Configuration.GetSection("TokenAuthentication:CookieName").Value;
                    options.TicketDataFormat = new CustomJwtDataFormat(
                        SecurityAlgorithms.HmacSha256,
                        _tokenValidationParameters);
                });
        }

        private Task<ClaimsIdentity> GetIdentity(string username, string password, UsersContext usersContext)
        {
            var userExist = usersContext.Users.Any(x => x.Username == username && x.Password == password);
            if(!userExist)
                return Task.FromResult<ClaimsIdentity>(null);
            return Task.FromResult(new ClaimsIdentity(new GenericIdentity(username, "Token"), new Claim[] { }));                        
        }

    }
}
