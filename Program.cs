using Expense_Tracker.Data;
using Microsoft.EntityFrameworkCore;
using Expense_Tracker.Services;
using Syncfusion.Licensing;

var builder = WebApplication.CreateBuilder(args);

// Register Syncfusion license
/*SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NBaF1cWGhIfEx1RHxQdld5ZFRHallYTnNWUj0eQnxTdEFjXHxfcXZWQ2BeVkR+Vw==");
*/
// Add services to the container.   
builder.Services.AddControllersWithViews();


// Dependency Injection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnections")));
builder.Services.AddSingleton<BreadcrumbService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
