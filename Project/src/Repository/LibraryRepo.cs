using Project.src.Models;
using System;
using System.Collections.Generic;
using System.Text;
using testing.src.Interfaces;
using testing.src.Models;

namespace Project.src.Repository
{
    internal class LibraryRepo : ILibraryRepo
    {
        private List<LibraryItem> _items = new List<LibraryItem>();
        public void AddItem(LibraryItem item)
        {
            _items.Add(item);
        }



        public void DeleteItem(int id)
        {
            var item = _items.FirstOrDefault(b => b.Id == id);
            if (item == null)
            {
                Console.WriteLine("Book not found.");
                return;
            }
            _items.Remove(item);
        }

        public List<LibraryItem> GetAllItems()
        {
            return _items;
        }



        public void UpdateItem(int id, LibraryItem updateditem)
        {
            var item = _items.Find(b => b.Id == id);
            if (item == null)
            {
                Console.WriteLine("Book not found.");
                return;
            }
            _items.Remove(item);
            _items.Add(updateditem);
        }
       
    }
}
