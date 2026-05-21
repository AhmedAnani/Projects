
using Project.src.Data;
using Project.src.Enums;
using Project.src.Models;
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


        }
    }
}