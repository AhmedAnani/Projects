using Project.src.Interfaces;
using testing.src.Models;


namespace Project.src.Repository
{
    public class LibraryRepo : ILibraryRepo
    {
        private List<LibraryItem> _items = new List<LibraryItem>();
        public void AddItem(LibraryItem item)//Add a new item to the library
        {
            if(_items.Any(b => b.Id == item.Id))//Check if an item with the same ID already exists
            {
                throw new Exception("ID already exists.");

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
            var existingItem = _items.FirstOrDefault(b => b.Id == id);

            if (existingItem == null)
            {
                Console.WriteLine("Item not found.");
                return;
            }

            try
            {
                // Basic properties
                existingItem.Title = updateditem.Title;
                existingItem.IsAvailable = updateditem.IsAvailable;

                // Handle specific types
                if (existingItem is Book existingBook && updateditem is Book newBook)
                {
                    existingBook.Author = newBook.Author;
                    existingBook.Description = newBook.Description;
                    existingBook.Category = newBook.Category;
                }
                else if (existingItem is EBook existingEBook && updateditem is EBook newEBook)
                {
                    existingEBook.Author = newEBook.Author;
                    existingEBook.Description = newEBook.Description;
                    existingEBook.Category = newEBook.Category;
                    existingEBook.FileSize = newEBook.FileSize;
                }

                Console.WriteLine($"{existingItem.Title} updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update failed: {ex.Message}");
            }
        }
       
    }
}
