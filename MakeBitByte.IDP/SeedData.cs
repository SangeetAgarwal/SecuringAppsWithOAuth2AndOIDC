﻿using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Serilog;

namespace MakeBitByte.IDP
{
    public static class SeedData
    {
        public static void EnsureSeedData(WebApplication app)
        {
            using var scope = app.Services
                .GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider
                .GetService<ConfigurationDbContext>();
            // EnsureSeedData(context);
        }

        private static void EnsureSeedData(ConfigurationDbContext context)
        {
            foreach (var client in Config.Clients)
            {
                // If a client not found in dB insert it
                if (!context.Clients.Any(r => r.ClientId == client.ClientId))
                {
                    context.Clients.Add(client.ToEntity());
                    context.SaveChanges();
                }
                else
                {
                    Log.Debug($"Client with Client id of {client.ClientId} already populated");
                }
            }

            if (!context.IdentityResources.Any())
            {
                Log.Debug("IdentityResources being populated");
                foreach (var resource in Config.IdentityResources.ToList())
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
            else
            {
                Log.Debug("IdentityResources already populated");
            }

            if (!context.ApiScopes.Any())
            {
                Log.Debug("ApiScopes being populated");
                foreach (var resource in Config.ApiScopes.ToList())
                {
                    context.ApiScopes.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
            else
            {
                Log.Debug("ApiScopes already populated");
            }

            if (!context.ApiResources.Any())
            {
                Log.Debug("ApiResources being populated");
                foreach (var resource in Config.ApiResources.ToList())
                {
                    context.ApiResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
            else
            {
                Log.Debug("ApiResources already populated");
            }
        }
    }
}