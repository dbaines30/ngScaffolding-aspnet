using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;

namespace ngScaffolding_oauth
{
    public static class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            var resource = new ApiResource("ngscaffolding", "ngscaffolding.Api");
            resource.UserClaims = new List<string>{"role"};
        
            return new List<ApiResource>
            {
                resource
            //    new ApiResource {
            //    Name = "ngscaffolding",
            //    DisplayName = "ngScaffolding.Api",
            //    UserClaims = new List<string> {"role"},
            //    ApiSecrets = new List<Secret> {new Secret("secret".Sha256())}
            //}

            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource
                {
                    
                    Name = "website",
                    UserClaims = new List<string> { "website" }
                },
                new IdentityResource
                {
                    Name = "role",
                    UserClaims = new List<string> { "role" }
                }

            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "ngscaffolding",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AlwaysSendClientClaims = true,
                    RefreshTokenUsage = TokenUsage.ReUse,
                    UpdateAccessTokenClaimsOnRefresh = true,

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "role",
                        "ngscaffolding"
                    },
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "siteadmin",
                    Password = "password",
                    Claims = new List<Claim>
                    {
                        new Claim("name", "Site Admin"),
                        new Claim(JwtClaimTypes.Role, "user"),
                        new Claim(JwtClaimTypes.Role, "admin")
                    }
                }
            };
        }
    }
}
