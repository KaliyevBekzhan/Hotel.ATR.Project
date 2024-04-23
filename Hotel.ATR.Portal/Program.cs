using Hotel.ATR.Portal;
using Hotel.ATR.Portal.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Security;
using Serilog;
using Serilog.Events;
using System.Configuration;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<HotelAtrContext>(
    options => options.UseSqlServer(connectionString)
    );

builder.Services.Configure<APIEndpoint>(
    
    builder.Configuration.GetSection("APIEndpoint"));

//builder.Host.ConfigureLogging(logging =>
//{
//    logging.ClearProviders();
//    logging
//    //.AddDebug()
//    //.AddConsole()
//    //.AddEventLog()
//    .AddSeq();
//});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = "/Home/login";
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMvc().AddMvcLocalization(LanguageViewLocationExpanderFormat.Suffix);
builder.Services.Configure<RequestLocalizationOptions>(options => 
{
    var cultures = new[]
    {
        new CultureInfo("ru-Ru"),
        new CultureInfo("kk-Kz"),
        new CultureInfo("en-Us")
    };

    options.DefaultRequestCulture = new RequestCulture(culture:"ru-Ru", uiCulture: "ru-Ru");
    options.SupportedCultures = cultures;
    options.SupportedUICultures = cultures;

});

builder.Services.AddLocalization(option => option.ResourcesPath = "Resources");

//builder.Services.AddTransient<IRepository, RepositorySQL>();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Seq("http://localhost:5341/")
    .WriteTo.File("Logs/logs.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Services.AddSingleton<Serilog.ILogger>(Log.Logger);

static void HandleMapOpen(IApplicationBuilder app){
    app.Run(async context =>
    {
        await context.Response.WriteAsync("Hello");
    });
}

builder.Services.Configure<CookieTempDataProviderOptions>(options =>
{
    options.Cookie.IsEssential = true;
    //options.Cookie.Domain = "localhost:62882";
    options.Cookie.Expiration = TimeSpan.FromSeconds(160);
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(5);
    options.Cookie.Name = ".HotelATR.Session";
});

builder.Services.AddCors(cors =>
{
    cors.AddPolicy("Policy_1", builder => builder.WithOrigins("http://localhost:5288").WithMethods("GET"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseCors("Policy_1");

app.UseSession();

app.UseRouting();

//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync("Before invoke app.Use\n");

//    await next();

//    await context.Response.WriteAsync("After invoke app.Use\n");
//});

app.UseContentMiddleware();

app.Map("/m2", HandleMapOpen);

app.Map("/m1", appMap =>
{
    appMap.Run(async context =>
    {
        await context.Response.WriteAsync("Hello"); 
    });
});

var localOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();

app.UseRequestLocalization(localOptions.Value);



app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
