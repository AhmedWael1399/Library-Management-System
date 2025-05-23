using LibraryManagementSystem.DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.BLL.Interfaces;
using LibraryManagementSystem.BLL.Repositories;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using LibraryManagementSystem.BLL.Services;


namespace LibraryManagementSystem
{

    public class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

         
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<LibraryManagementSystemDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                )
            );

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            
            builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IBorrowingTransactionRepository, BorrowingTransactionRepository>();

            builder.Services.AddScoped<IAuthorService, AuthorService>();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IBorrowingTransactionService, BorrowingTransactionService>();



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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
