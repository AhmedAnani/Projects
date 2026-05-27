using Project.src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.src.Controller
{
    public class UserConsolePrinter
    {
        // ── User Info ─────────────────────────────────────────
        public static void UserInfo(User user)
        {

            ConsolePrinter.Divider();
            ConsolePrinter.ItemInfo("ID", user.Id.ToString());
            ConsolePrinter.ItemInfo("Name", user.Name);
            ConsolePrinter.ItemInfo("Email", user.Email);
            ConsolePrinter.ItemInfo("Role", user.Role.ToString());
        }

        // ── Users Info ─────────────────────────────────────────
        public static void Users(IEnumerable<User> users)
        {
            ConsolePrinter.Header("Users of The Library");

            if (!users.Any())
            {
                ConsolePrinter.Empty("No users found.");
                return;
            }

            foreach (var user in users)
                UserInfo(user);
        }
    }
}
