using Project.src.Enums;
using Project.src.Interfaces.IService;
using Project.src.Models;
using System.Linq;


namespace Project.src.Services
{
    public class AuthorizationService : IAuthorizationService
    {

        //This method used to check if the user has the required role to perform a specific action
        private static bool HasRole(User? user, params UserRole[] allowedRoles)
        {
            return user != null && allowedRoles.Contains(user.Role);
        }

        //This method used to manage users (Admin only can manage users)
        public bool CanManageUsers(User? user) => HasRole(user, UserRole.Admin);


        //This method used to give permission to view reports in the system (Admin only can view reports)
        public bool CanViewReports(User? user) => HasRole(user, UserRole.Admin);
     

        //This method used to give permission to Add Items in the system (Employee and Admin can add items)
        public bool CanAddItems(User? user) => HasRole(user,UserRole.Admin, UserRole.Employee);
       

        //This method used to give permission to Update Items in the system (Employee and Admin can update items)
        public bool CanUpdateItems(User? user) => HasRole(user, UserRole.Admin, UserRole.Employee);


        //This method used to give permission to delete Items in the system (Employee and Admin can delete items)
        public bool CanDeleteItems(User? user) => HasRole(user, UserRole.Admin, UserRole.Employee);


        //This method used to give permission to view Items in the system (Employee, Admin, and User can view items)
        public bool CanViewItems(User? user) => HasRole(user, UserRole.Admin, UserRole.Employee, UserRole.User);


        //This method used to give permission to borrow Items in the system (Employee and User can borrow items)
        public bool CanBorrow(User? user) => HasRole(user, UserRole.Employee, UserRole.User);


        //This method used to give permission to purchase Items in the system (Employee and User can purchase items)
        public bool CanBuy(User? user) => HasRole(user, UserRole.Employee, UserRole.User);

    }
}
