
using Project.src.Data;
using Project.src.Enums;
using Project.src.Models;
using Project.src.Repositories;
using System;
using System.ComponentModel.DataAnnotations;
namespace Project
{
    class Program
    {
        public static void Main(string[] args)
        {
            var Context = new AppDbContext();

            SeedData.Initialize(Context);
            var context = new AppDbContext();
            var repo = new LibraryItemRepository(context);
            var categoryRepo = new CategoryRepository(context);
            var category = new Category("Programming");
            categoryRepo.Add(category);

            // ── CREATE ──────────────────────────────────────────
            var book = new Book("Clean Code", category.Id, "Robert Martin", "Best practices");
            repo.Add(book);
            Console.WriteLine("Book Added ");
            Console.WriteLine($"Book Id = {book.Id}");

            // ── GET BY ID ─────────────────────────────────────────
            var item = repo.GetById(book.Id);
            Console.WriteLine(item?.DisplayInfo());

            

            // ── GET ALL ───────────────────────────────────────────
            var all = repo.GetAll();
            foreach (var i in all)
                Console.WriteLine(i.DisplayInfo());

            // ── UPDATE ────────────────────────────────────────────
            repo.Update(1, item => item.Rename("Clean Code 2nd Edition"));

            // ── DELETE ────────────────────────────────────────────
            repo.Delete(1);


        }
    }
}