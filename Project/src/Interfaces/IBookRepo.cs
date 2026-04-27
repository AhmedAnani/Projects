using System;
using System.Collections.Generic;
using System.Text;
using testing.src.Models;

public interface IBookRepo
{
    void AddBook(Book book);
    void RemoveBook(int id);
    void DeleteBook(int id);
    
    void UpdateBook(int id, Book updatedBook);

    List<Book> GetAllBooks();


}
