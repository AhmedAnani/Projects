
using Microsoft.EntityFrameworkCore;
using Project.src.App;
using Project.src.Controller;
using Project.src.Data;
using Project.src.Enums;
using Project.src.Models;
using Project.src.Repositories;
using Project.src.Services;
using System;
using System.ComponentModel.DataAnnotations;
namespace Project
{
    class Program
    {
        public static void Main(string[] args)
        {
            var app = new LibraryApp();
            app.Run();


        }
    }
}