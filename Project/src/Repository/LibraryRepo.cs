using Project.src.Interfaces;
using Project.src.Models;
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
            Console.WriteLine($"{item.Title} Added Successfully.");
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
            Console.WriteLine($"{item.Title} Deleted Successfully.");
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
                if (existingItem is BookItem existingBook && updateditem is BookItem newBook)
                {
                    existingBook.Author = newBook.Author;
                    existingBook.Description = newBook.Description;
                    existingBook.Category = newBook.Category;
                    if(existingBook is EBook Ebook) {
                        Ebook.FileSize = Ebook.FileSize;
                    }
                }
                
                Console.WriteLine($"{existingItem.Title} Updated Successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update failed: {ex.Message}");
            }
        }
        public bool CheckItem(int id)
        {
            var DeleteItem = _items.Find(i => i.Id == id);
            if (DeleteItem == null) 
                return false;

            return true;
        }
       
    }
}
