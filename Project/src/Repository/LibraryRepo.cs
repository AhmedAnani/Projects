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
        public void AddItem(LibraryItem item)//Add a new item to the library
        {
            if(_items.Any(b => b.Id == item.Id))//Check if an item with the same ID already exists
            {
                Console.WriteLine("ID already exists.");
                return;
            }
           
            _items.Add(item);
            Console.WriteLine($"{item.Title} added successfully.");
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
            Console.WriteLine($"{item.Title} deleted successfully.");
        }

        public List<LibraryItem> GetAllItems()//Return the list of all library items
        {

            return new List<LibraryItem>(_items);
        }



        public void UpdateItem(int id, LibraryItem updateditem)//Find the item by id and update it with the new information
        {
            int index = _items.FindIndex(b => b.Id == id);
            if (index == -1)
            {
                Console.WriteLine("Book not found.");
                return;
            }
            updateditem.Id = id; // Ensure the updated item has the same ID
            _items[index] = updateditem;
            Console.WriteLine($"{updateditem.Title} updated successfully.");
        }
       
    }
}
