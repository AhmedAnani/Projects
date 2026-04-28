using Project.src.Models;
using System;
using System.Collections.Generic;
using System.Text;
using testing.src.Models;

namespace Project.src.Interfaces
{
    internal interface ILibraryRepo
    {
        void AddItem(LibraryItem book);
        void DeleteItem(int id);

        void UpdateItem(int id, LibraryItem updatedBook);

        List<LibraryItem> GetAllItems();

    }
}