using Project.src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.src.Controller
{
    public class CategoryConsolePrinter
    {
        // ── Category Info ─────────────────────────────────────────
        public static void CategoryInfo(Category category)
        {
            ConsolePrinter.Divider();
            ConsolePrinter.ItemInfo("ID", category.Id.ToString());
            ConsolePrinter.ItemInfo("Name", category.Name);
        }

        // ── Categories Info ─────────────────────────────────────────
        public static void Categories(IEnumerable<Category> categories)
        {
            ConsolePrinter.Header("Categories");

            if (!categories.Any())
            {
                ConsolePrinter.Empty("No categories found.");
                return;
            }

            foreach (var category in categories)
                CategoryInfo(category);
        }
    }
}
