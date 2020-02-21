// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdSrvInMem
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
               
            };


        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {

                new ApiResource("NotaFiscal", "NotaFiscal")
            };


        public static IEnumerable<Client> Clients =>
            new Client[]
            {

                 new Client
                {
                    ClientId = "mvc.implicit",
                    ClientName = "Minha Aplicação Web MVC", 
                    AllowedGrantTypes = GrantTypes.Implicit, // Token Implicit, provavelmente server to server mudar aqui
                    AllowAccessTokensViaBrowser = true,   
                    RedirectUris = { "http://localhost:27003/signin-oidc" },
                    AllowedScopes = { "NotaFiscal", "openid", "profile"},
            },
 
            };
    }
}