using BuildingBlocks.Infrastructure;
using Complaints.Application;
using Complaints.Infrastructure;
using Department.Application;
using Department.Infrastructure;
using DocumentsMgmt.Application;
using Feeds.Application;
using Feeds.Infrastructure;
using Houses.Application;
using Houses.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserMgmt.Application;
using UserMgmt.Infrastructure;
using UserMgmt.Infrastructure.Persistence;

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

builder.Services.AddAuthorization();

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

#endregion


#region AI Services

builder.Services.AddHivifyAIServices();

#endregion


var app = builder.Build();

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


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

#endregion


app.Run();
