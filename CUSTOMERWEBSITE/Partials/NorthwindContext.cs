using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace CUSTOMERWEBSITE.Models
{
    // partial class：這裡是你自己的擴充，不會被 Scaffold 蓋掉
    public partial class NorthwindContext : DbContext
    {
        // 1. 設定連線
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // 注意：你需要 using Microsoft.Extensions.Configuration;
                var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                // !!! 請確認這個名稱跟 appsettings.json 裡一致 !!!
                var connStr = config.GetConnectionString("Northwind");
                optionsBuilder.UseSqlServer(connStr);
            }
        }

        // 2. 你可以加自訂方法
        public void SayHello()
        {
            Console.WriteLine("Hello, this is the partial NorthwindContext!");
        }

        // 3. 你也可以加自訂 partial void，做 Model 微調
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            // 這裡可加 Model 的細部設定，不會被 Scaffold 蓋掉
            // 例如：
            // modelBuilder.Entity<Customer>().Property(c => c.Email).IsUnicode(false);
        }
    }
}


//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Options;

//namespace CUSTOMERWEBSITE.Models
//{
//    public partial class NorthwindContext
//    {
//        // 這裡你可以加你自己要的功能，例如自訂方法或 partial void
//        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
//        {
//            // 這裡寫你自己的 model 設定
//            // 例如：
//            // modelBuilder.Entity<Customer>().Property(c => c.YourProp).IsUnicode(false);
//            if(!OptionsBuilder.Isconfigured)
//            { 
//                IConfiguration config=new
//            }

//        // 也可以加自訂功能
//        public void SayHello()
//        {
//            Console.WriteLine("Hello, this is the partial NorthwindContext!");
//        }
//    }
//}
