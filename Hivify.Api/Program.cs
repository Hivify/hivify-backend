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
using Hivify.Api.Controllers.ControlPlane.Associations.Authorization.TenantRoleAssociationAuthorization;
using Hivify.Api.MultiTenancy;
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

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("TenantAdmin", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.AddRequirements(
            new AssociationRoleRequirement("TenantAdmin"));
    });
});

// User Management
builder.Services.AddUserMgmtServices();
builder.Services.AddUserMgmtInfrastructure(connectionString);


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



// current Tenant
builder.Services.AddHivifyMultiTenancy();
builder.Services.AddScoped<IAuthorizationHandler, AssociationRoleAuthorizationHandler>();

#endregion


#region Building Blocks
builder.Services.AddBuildingBlocks(builder.Configuration);




// Documents
builder.Services.AddDocumentServices();

//Complaints
builder.Services.AddComplaintServices();
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
