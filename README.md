#  Library Management System

A console-based Library Management System built in C# that demonstrates object-oriented programming principles, design patterns, and clean architecture. The system allows users to borrow books, buy items (books, eBooks, magazines), and provides administrative capabilities for managing library inventory.

##  Features

### For Users (Regular Users)
- **Browse Library Items** - View all available items in the library
- **Search Items** - Find items by their unique ID
- **Borrow Books** - Borrow physical books with a 14-day limit (max 3 books)
- **Buy Items** - Purchase books, eBooks, or magazines
- **Return Books** - Return borrowed books with late fee calculation

### For Admins
- **User Management** - Add, update, and delete user accounts

### For Employees
- **Add New Items** - Add new books, eBooks, or magazines to the library
- **Update Items** - Modify existing item information
- **Delete Items** - Remove items from the library
- **Can Borrow&Buy Items** - Act Like Users

### For Users
- **View Items**
- **Borrow Items**
- **Buy Items**
  
### System Rules
- Maximum borrow limit: **3 books per user**
- Borrowing period: **14 days**
- Late fee: **10 currency units per day**
- Only books are borrowable (eBooks and magazines are buy-only)
- Items must be available to be borrowed or purchased

### Architecture
- Controller Layer → Handles user actions (LibraryManager)
- Service Layer → Business logic (Borrowing, Buying, Auth)
- Repository Layer → Data storage and management
- Model Layer → Core entities (User, Book, etc.)

### GG
