using Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace SMSOnline
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider; // lấy các service trong startup(ConfigureServices)

                try
                { // dùng service locator để lấy ra nhưng thằng đã được register trong startup(ConfigureServices)
                    var dbInitializer = services.GetService<DbInitializer>();
                    dbInitializer.Seed().Wait(); // chạy phương thức seed chứa các dữ liệu mẫu reder ra cùng vs database
                }
                catch (Exception ex)
                {// dùng logger để xuất ra error
                    var logger = services.GetService<ILogger<Program>>();
                    logger.LogError(ex, "lỗi chạy seed khi khởi tạo database");
                }
            }
            host.Run();
            //CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}