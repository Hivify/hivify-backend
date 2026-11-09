using Association.Application;
using Association.Infrastructure;
using BuildingBlocks.Infrastructure;
using Complaints.Application;
using Complaints.Infrastructure;
using Department.Application;
using Department.Infrastructure;
using DocumentsMgmt.Application;
using Feeds.Application;
using Feeds.Infrastructure;
using Finbuckle.MultiTenant.AspNetCore.Extensions;
using Hivify.Api.Integrations;
using Hivify.Api.MultiTenancy;
using Hivify.Api.Security;
using Hivify.Api.Security.Tenant.TenantAuthorization;
using Houses.Application;
using Houses.Infrastructure;
using Identity.Application;
using Identity.Infrastructure;
using Identity.Infrastructure.DotNETIdentity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnBoarding.Application;
using OnBoarding.Infrastructure;
using Tiering.Application;
using Tiering.Infrastructure;
using Tiering.Infrastructure.Persistence.Seeders;




var builder = WebApplication.CreateBuilder(args);

#region API

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();





#endregion


#region Database

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "Connection string 'DefaultConnection' not found.");


#endregion


#region Authentication & Authorization


builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
.AddIdentityCookies();





// Configure the Identity application cookie

builder.Services
    .AddOptions<CookieAuthenticationOptions>(
        IdentityConstants.ApplicationScheme)
    .Configure(options =>
    {
        // No Cookie.Domain: the cookie is scoped to the current host.
        options.Cookie.Path = "/";
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    });





// Register the custom authorization handler for the AssociationRoleRequirement
builder.Services.AddScoped<IAuthorizationHandler, TenantRoleAuthorizationHandler>();

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("AssociationAdmin", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.AddRequirements(new TenantRoleRequirement("AssociationAdmin"));
    });
#endregion




#region Building Blocks and Application Services


// Add buildingBlocks 
builder.Services.AddBuildingBlocks(builder.Configuration, builder.Environment.IsDevelopment());

// Multi-Tenancy Integration 
builder.Services.AddHivifyMultiTenancy();

// Services Integrations
builder.Services.AddServicesIntegrations();

// Security Services
builder.Services.AddServicesSecurity();



// User Management
builder.Services.AddUserMgmtServices();
builder.Services.AddUserIdentityInfrastructure(connectionString);


// Documents
builder.Services.AddDocumentServices();

//Complaints
builder.Services.AddComplaintsApplication();
builder.Services.AddComplaintsInfrastructure(connectionString);

// Houses
builder.Services.AddHouseServices();
builder.Services.AddHousesInfrastructure(connectionString);

// Feed
builder.Services.AddFeedServices();
builder.Services.AddFeedInfrastructure(connectionString);
// Department
builder.Services.AddDepartmentServices();
builder.Services.AddDepartmentInfrastructure(connectionString);
// Association
builder.Services.AddAssociationServices();
builder.Services.AddAssociationInfrastructure(connectionString);

// Onboarding
builder.Services.AddOnBoardingServices();
builder.Services.AddOnboardingInfrastructure(connectionString);

// Tiering 
builder.Services.AddTierInfrastructure(connectionString);
builder.Services.AddTieringServices();




#endregion


#region AI Services

builder.Services.AddHivifyAIServices();

#endregion

#region Cors

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.SetIsOriginAllowed(origin => new Uri(origin).Host.EndsWith(".localhost"))
           .AllowAnyMethod()
           .AllowAnyHeader()
           .AllowCredentials();
    });
});

#endregion
var app = builder.Build();

app.UseCors("AllowFrontend");


using (var scope = app.Services.CreateScope())
{
    await IdentitySeeder.SeedAsync(scope.ServiceProvider);
    await TierSeeder.SeedAsync(scope.ServiceProvider);

}

#region HTTP Pipeline

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}
// add the MultiTenant middleware
app.UseMultiTenant();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();



#endregion


app.Run();
