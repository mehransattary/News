using Microsoft.EntityFrameworkCore;
using Data.Context;
using Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllersWithViews();

//*******SqlServer*******************//
var myConnection = builder.Configuration.GetConnectionString("connection");
builder.Services.AddDbContext<AppDbContext>(c => c.UseSqlServer(myConnection));

//*******Services*******************//
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddScoped<IServiceImage, ServiceImage>();
builder.Services.AddScoped<ISliderService, SliderService>();
builder.Services.AddScoped<INewsService, NewService>();
builder.Services.AddScoped<IAccountService, AccountService>();

//*******Identity*******************//
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(x =>
{
    x.LoginPath = "/Account/Login";
    x.LogoutPath = "/Account/Logout";
    x.ExpireTimeSpan = TimeSpan.FromDays(300);
});
builder.Services.AddAuthorization(options =>
  {
      options.AddPolicy("Admin",
          authBuilder =>
          {
              authBuilder.RequireRole("Admin");
          });
      options.AddPolicy("User",
             authBuilder =>
             {
                 authBuilder.RequireRole("User");
             });
  });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();



app.MapControllerRoute(
    name: "MyAreaProducts",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
