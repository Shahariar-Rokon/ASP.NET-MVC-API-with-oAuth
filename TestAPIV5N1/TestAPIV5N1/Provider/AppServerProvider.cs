using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using TestAPIV5N1.Repos;

namespace TestAPIV5N1.Provider
{
    public class AppServerProvider:OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
           using(UserRepo repo = new UserRepo())
            {
                var user = repo.ValidateUser(context.UserName, context.Password);
                if(user== null)
                {
                    context.SetError("i", "u/p");
                }
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name,user.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Email,user.Email));
                foreach(var item in user.Roles.Split(','))
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role,item.Trim()));
                }
                context.Validated(identity);
            } 
        }
    }
}