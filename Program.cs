using Expense_Tracker.Data;
using Microsoft.EntityFrameworkCore;
using Expense_Tracker.Services; 

var builder = WebApplication.CreateBuilder(args);

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
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
