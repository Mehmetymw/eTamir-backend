// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace eTamir.IdentityServer
{

    public static class Config
    {
        public const string CatalogPermission = "catalog_fullpermission";
        public const string AddressPermission = "address_fullpermission";
        public const string PhotoStockPermission = "photo_stock_fullpermission";
        public const string FavPermission = "fav_fullpermission";
        public const string AppointmetnPermission = "appointment_fullpermission";
        public const string GatewayPermission = "gateway_fullpermission";
        public const string IdentityResourceRole = "roles";
        public static IEnumerable<ApiResource> ApiResources =>
                   new ApiResource[]{
                        new ApiResource("resource_catalog"){
                            Scopes = {
                                CatalogPermission
                            }
                        },
                        new ApiResource("resource_address"){
                            Scopes = {
                                AddressPermission
                            }
                        },

                        new ApiResource("resource_photo_stock"){
                            Scopes = {
                                PhotoStockPermission
                            }
                        },
                        new ApiResource("resource_fav"){
                            Scopes = {
                                FavPermission
                            }
                        },
                        new ApiResource("resource_appointment"){
                            Scopes = {
                                AppointmetnPermission
                            }
                        },
                        new ApiResource("resource_gateway"){
                            Scopes = {
                                GatewayPermission
                            }
                        },
                        new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
                   };

        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                        new IdentityResources.Email(),
                        new IdentityResources.OpenId(),
                        new IdentityResources.Profile(),
                        new IdentityResource(){
                            Name = IdentityResourceRole,
                            DisplayName = "Roles",
                            Description = "User roles",
                            UserClaims = {
                                "role"
                            }
                        }
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName, ""),
                new ApiScope(CatalogPermission, "Catalog API için full erişim"),
                new ApiScope(PhotoStockPermission, "PhotoStock API için full erişim"),
                new ApiScope(FavPermission, "Fav API için full erişim"),
                new ApiScope(AppointmetnPermission, "Appointment API için full erişim"),
                new ApiScope(GatewayPermission, "Gateway API için full erişim"),
                new ApiScope(AddressPermission, "Address API için full erişim")
            };


        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientName = "React",
                    ClientId = "React",
                    ClientSecrets = {
                        new Secret("secret".Sha256()),
                    },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = {
                        GatewayPermission,
                        CatalogPermission,
                        PhotoStockPermission,
                        AddressPermission,
                        IdentityServerConstants.LocalApi.ScopeName,
                    }
                },
                new Client
                {
                    ClientName = "ReactForUser",
                    ClientId = "ReactForUser",
                    AllowOfflineAccess = true,
                    ClientSecrets = {
                        new Secret("secret".Sha256()),
                    },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = {
                        IdentityServerConstants.LocalApi.ScopeName,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        CatalogPermission,
                        PhotoStockPermission,
                        IdentityResourceRole,
                        FavPermission,
                        AppointmetnPermission,
                        GatewayPermission,
                        AddressPermission
                    },
                    AccessTokenLifetime = 1 * 60 * 60,
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime =  1 * 60 * 60* 24*60,
                    RefreshTokenUsage = TokenUsage.ReUse
                },
            };
    }
}