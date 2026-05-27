using Project.src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.src.Controller
{
    public class LibraryItemConsolePrinter
    {
        // ── LibraryItem Info ─────────────────────────────────────────
        public static void LibraryItemInfo(LibraryItem item)
        {
            ConsolePrinter.Divider();
            ConsolePrinter.ItemInfo("ID", item.Id.ToString());
            ConsolePrinter.ItemInfo("Type", item.ItemType.ToString());
            ConsolePrinter.ItemInfo("Title", item.Title);
            ConsolePrinter.ItemInfo("Category", item.Category?.Name ?? $"Category ID: {item.CategoryId}");
            ConsolePrinter.ItemInfo("Status", item.Status.ToString());

            if (item is BookItem bookItem)
            {
                ConsolePrinter.ItemInfo("Author", bookItem.Author);
                ConsolePrinter.ItemInfo("Description", bookItem.Description);
            }

            if (item is EBook ebook)
                ConsolePrinter.ItemInfo("File Size", ebook.FileSize);
        }

        // ── LibraryItems Info ─────────────────────────────────────────
        public static void LibraryItems(IEnumerable<LibraryItem> items)
        {
            ConsolePrinter.Header("Library Items");

            if (!items.Any())
            {
                ConsolePrinter.Empty("No library items found.");
                return;
            }

            foreach (var item in items)
                LibraryItemInfo(item);
        }
    
    }
}
