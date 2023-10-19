using Microsoft.EntityFrameworkCore;
using RetroVideo.Data.Abstract;
using RetroVideo.Data.Concrete;
using RetroVideo.Entities;
using RetroVideo.Services;
using RetroVideo.Services.Abstract;
using RetroVideo.Services.Concrete;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// MARK : Register DbContext
builder.Services.AddDbContextPool<RetroVideoContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddSession();

// MARK : Register Data Manager
builder.Services.AddTransient<IBaseDataManager<RetroVideoContext>, BaseDataManager>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// MARK : Transients
builder.Services.AddTransient(typeof(IBaseServiceManager<,>), typeof(BaseServiceManager<,>));

builder.Services.AddTransient<IFilmSM, FilmSM>();
builder.Services.AddTransient<IGenreSM, GenreSM>();
builder.Services.AddTransient<IKlantenSM, KlantenSM>();
builder.Services.AddTransient<IReservatySM, ReservatySM>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Genre}/{action=Index}/{id?}");

app.Run();