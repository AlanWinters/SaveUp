using DevExpress.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SaveUp.Library;
using Microsoft.EntityFrameworkCore;
using SaveUp.Library.Services;
using SaveUp.Views;
using SaveUp.ViewModels;

namespace SaveUp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            SQLitePCL.Batteries.Init();
            var builder = MauiApp.CreateBuilder();
            builder
                .UseDevExpress()
                .UseDevExpressControls()
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Configure Database Connection
            //var constr = "Server=localhost;Database=SaveUp_Database;Uid=root;Pwd=root;";
            
            var constr = "data source="+ Path.Combine(FileSystem.AppDataDirectory, "db.sqlite");
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite(constr);
                //options.UseMySql(constr, MySqlServerVersion.LatestSupportedServerVersion);
                options.EnableSensitiveDataLogging();
            }, ServiceLifetime.Singleton);

            builder.Services.AddScoped<ProductService>();
            builder.Services.AddScoped<ProductListViewModel>();
            builder.Services.AddScoped<AddProductViewModel>();
            builder.Services.AddScoped<AddProductPage>();
            builder.Services.AddScoped<ProductListPage>();
            builder.Services.AddScoped<ChartViewModel>();
            builder.Services.AddScoped<ChartPage>();



#if DEBUG
            builder.Logging.AddDebug();
#endif

            var app =  builder.Build();

            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var created = dbContext.Database.EnsureCreated();

            if(dbContext.Products.FirstOrDefault() is null)
            {
                dbContext.Products.Add(new Models.ProductModel
                {
                    ProductName = "Test",
                    Description = "desc",
                    Category = Library.Enums.Category.Essen,
                    Price = 1,
                });
                dbContext.SaveChanges();
            }



            return app;
        }
    }
}
