using Microsoft.EntityFrameworkCore;
using StudentManagmentSystem.WebApp.Configurations;
using StudentManagmentSystem.WebApp.Controllers;
using StudentManagmentSystem.WebApp.Models;
using StudentManagmentSystem.WebApp.Repository;
using StudentManagmentSystem.WebApp.Repository.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<StudentManagmentContext>(options => options.UseSqlServer());
builder.Services.AddScoped<IApplicationUserService,ApplicationUserService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped(typeof(IBaseRepository<>),typeof(BaseRepository<>));
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();
app.UseMiddleware <AuthMiddleware>();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
