
using Infrastructure.Contexts;
using Infrastructure.Identity;
using Infrastructure.Misc;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<CmsDbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = true;
})
.AddEntityFrameworkStores<CmsDbContext>().AddDefaultTokenProviders();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));


var app = builder.Build();

await DbMigrate.MigrateDb(app.Services);

app.UseHsts();
app.UseExceptionHandler("/Error/500");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
