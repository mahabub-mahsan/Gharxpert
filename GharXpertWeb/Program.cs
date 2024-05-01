using GharXpertWeb;
using GharXpertWeb.Service;
using GharXpertWeb.Service.IService;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddHttpClient<IWorkService, WorkService>();
builder.Services.AddScoped<IWorkService, WorkService>();
builder.Services.AddHttpClient<IWorkTypeService, WorkTypeService>();
builder.Services.AddScoped<IWorkTypeService, WorkTypeService>();
builder.Services.AddHttpClient<IServiceChargesService, ServiceChargesSrvice>();
builder.Services.AddScoped<IServiceChargesService, ServiceChargesSrvice>();
builder.Services.AddHttpClient<IConstructionTypeService, ConstructionTypeService>();
builder.Services.AddScoped<IConstructionTypeService, ConstructionTypeService>();
builder.Services.AddHttpClient<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddHttpClient<IContactOurExpertService, ContactOurExpertService>();
builder.Services.AddScoped<IContactOurExpertService, ContactOurExpertService>();
builder.Services.AddHttpClient<IQuotationMasterService, QuotationMasterService>();
builder.Services.AddScoped<IQuotationMasterService, QuotationMasterService>();






builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpClient<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.HttpOnly = true;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                    options.LoginPath = "/Auth/Login";
                    options.AccessDeniedPath = "/Auth/AccessDenied";
                    options.SlidingExpiration = true;
                });

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(100);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
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
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
