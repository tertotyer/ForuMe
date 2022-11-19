using Duende.IdentityServer;
using Duende.IdentityServer.Models;


namespace ForuMe.Services.Identity
{
    public static class SD
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>{
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes
        {
            get
            {
                return new List<ApiScope> {
                new ApiScope("ForuMe", "ForuMe server"),
                new ApiScope("read", "Read data"),
                new ApiScope("write", "Write data"),
                new ApiScope("delete", "Delete  data")
            };
            }
        }

        public static IEnumerable<Client> Clients =>
            new List<Client> {
                new Client()
                {
                    ClientId = "client", 
                    ClientSecrets = { new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "read", "write", "profile" },
                },
                new Client()
                {
                    ClientId = "ForuMe",
                    ClientSecrets = { new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://localhost:44346/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:44346/signout-callback-oidc" },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "ForuMe"
                    }
                }
            };
    }
}
