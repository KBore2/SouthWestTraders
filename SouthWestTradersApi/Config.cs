using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace SouthWestTradersApi
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope
                {
                    Name = "roles",
                    DisplayName = "roles",
                    UserClaims =
                    {
                        JwtClaimTypes.Email,
                        JwtClaimTypes.Name,
                        JwtClaimTypes.FamilyName,
                        JwtClaimTypes.PhoneNumber,
                        JwtClaimTypes.Profile,
                        JwtClaimTypes.Role

                    }
                },
                new ApiScope
                {
                    Name = "read",
                    DisplayName = "read",
                    UserClaims =
                    {
                        JwtClaimTypes.Email,
                        JwtClaimTypes.Name,
                        JwtClaimTypes.FamilyName
                    }
                }
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource
                {
                    Enabled = true,
                    UserClaims =
                    {
                        "unique_name",
                        JwtClaimTypes.Email,
                        JwtClaimTypes.BirthDate,
                        JwtClaimTypes.PhoneNumber,
                        JwtClaimTypes.Role,
                        JwtClaimTypes.GivenName,
                        JwtClaimTypes.FamilyName,
                        JwtClaimTypes.PreferredUserName

                    }

                },
                new ApiResource("read","Read identity"),
                new ApiResource("roles","Read roles")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                //implicit client
                new Client
                {
                    ClientId = "southwest.traders",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = new List<string>
                    {
                        "https://localhost:5001/swagger/oauth2-redirect.html"
                    },
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowAccessTokensViaBrowser = true,
                    AllowOfflineAccess = true,
                    AccessTokenLifetime = 3600,
                    AllowedScopes =
                    {
                        "roles",
                        "read",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                    }
                },

                //resource owner grant client
                new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowOfflineAccess = true,
                    AccessTokenType = AccessTokenType.Jwt,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AccessTokenLifetime = 3600,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes =
                    {
                        "read",
                        "roles",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,

                    }

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
                    Username = "alice",
                    Password = "password",
                    Claims = new List<Claim>
                    {
                      new Claim(type: "role", value: "Admin"),
                      new Claim(type: "email", value: "alice@swt.co.za"),
                      new Claim(type: "name", value: "Alice"),
                      new Claim(type: "family_name", value: "Jones")
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "bob",
                    Password = "password",
                    Claims = new List<Claim>
                    {
                      new Claim(type: "role", value: "Consumer"),
                      new Claim(type: "email", value: "bob@swt.co.za"),
                      new Claim(type: "name", value: "Bob"),
                      new Claim(type: "family_name", value: "Smith")
                    }
                }
            };
        }
    }
}
