using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Warehouse.Core.Constants;
using Warehouse.Infrastructure.Data;
using Warehouse.Infrastructure.Data.Identity;
using Warehouse.ModelBinders;



var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews()
    .AddMvcOptions(options =>
    {
        options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
        options.ModelBinderProviders.Insert(1, new DateTimeModelBinderProvider(FormatingConstant.NormalDateFormat));
        options.ModelBinderProviders.Insert(2, new DoubleModelBinderProvider());
    })
    .AddMvcLocalization(LanguageViewLocationExpanderFormat.Suffix);

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential 
    // cookies is needed for a given request.
    options.CheckConsentNeeded = context => true;

    options.MinimumSameSitePolicy = SameSiteMode.None;
});

//builder.Services.AddStackExchangeRedisCache(options =>
//{
//    options.Configuration = builder.Configuration.GetConnectionString("Redis");
//});

builder.Services.AddLocalization(options => options.ResourcesPath = "Resourses");
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedLanguages = new CultureInfo[]
    {
        new CultureInfo("bg"),
        new CultureInfo("en")
    };

    options.DefaultRequestCulture = new RequestCulture("bg");
    options.SupportedCultures = supportedLanguages;
    options.SupportedUICultures = supportedLanguages;
});

builder.Services.AddAuthentication()
    .AddFacebook(options =>
    {
        //That is the user-secrets key, that searches the value in file on my computer
        //If I want to open the file and see the values, I shoud right click on my project
        //-> Manage User Secrets
        //It is used so that information would be not visible for the public eye
        //In development enviroment it will work, but when we are in production we have to store it
        //somewhere else
        options.AppId = builder.Configuration.GetValue<string>("Facebook:AppId");
        options.AppSecret = builder.Configuration.GetValue<string>("Facebook:AppSecret");
        //TO DO:
        //for the button to work go to meta for developers/ verification and make it buisness


    });
builder.Services.AddApplicationServices();
builder.Services.AddSwaggerGen(options => 
{
    options.IncludeXmlComments(@"C:\Users\Lenovo\source\repos\Warehouse\WarehouseAPI\bin\Debug\net6.0");
});

var app = builder.Build();  

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();

app.UseRouting();
app.UseRequestLocalization();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "Area",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
