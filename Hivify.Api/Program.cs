using Association.Application;
using Association.Infrastructure;
using BuildingBlocks.ApplicationPorts.CurrentTenantPort;
using BuildingBlocks.ApplicationPorts.CurrentUserPort;
using BuildingBlocks.Infrastructure;
using Complaints.Application;
using Complaints.Infrastructure;
using Department.Application;
using Department.Infrastructure;
using DocumentsMgmt.Application;
using Feeds.Application;
using Feeds.Infrastructure;
using Finbuckle.MultiTenant.Abstractions;
using Finbuckle.MultiTenant.AspNetCore.Extensions;
using Finbuckle.MultiTenant.Extensions;
using Hivify.Api.Controllers.ControlPlane.Associations.Authorization;
using Hivify.Api.CurrentUserProvider;
using Hivify.Api.MultiTenancy;
using Houses.Application;
using Houses.Infrastructure;
using Identity.Application;
using Identity.Infrastructure;
using Identity.Infrastructure.DotNETIdentity;
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
// add MultiTenant services
builder.Services
    .AddMultiTenant<TenantInfo>()
    .WithHostStrategy("__tenant__.localhost")
    .WithStore<AssociationTenantStore>(ServiceLifetime.Scoped);




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
            new TenantRoleRequirement("TenantAdmin"));
    });
});


// current User
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();
builder.Services.AddScoped<IApplicationAuthorization, ApplicationAuthorization>();


// current Tenant
builder.Services.AddScoped<ICurrentTenant, CurrentTenant>();
builder.Services.AddScoped<IAuthorizationHandler, TenantRoleAuthorizationHandler>();

#endregion


#region Building Blocks
builder.Services.AddBuildingBlocks(builder.Configuration);



// User Management
builder.Services.AddUserMgmtServices();
builder.Services.AddUserMgmtInfrastructure(connectionString);

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
        policy.WithOrigins("http://localhost:3000")
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
