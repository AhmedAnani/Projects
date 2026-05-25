using Project.src.Enums;
using Project.src.Exceptions;
using Project.src.Interfaces;
using Project.src.Models;
using System;
using System.Collections.Generic;


namespace Project.src.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthorizationService _authorizationService;

        //Dependency injection of the user repository and authorization service to manage users and check permissions
        public UserService(IUserRepository userRepository, IAuthorizationService authorizationService)
        {
            //To ensure that the service cannot be instantiated without providing the necessary dependencies, we throw an exception if either of them is null.
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
        }

        //This service is responsible for Adding new user to the system
        public void AddUser(User? currentUser, string name, string email, UserRole role)
        {
            //Check if the current user is authorized to manage users
            if (!_authorizationService.CanManageUsers(currentUser))
                throw new UnauthorizedAccessException("You do not have permission to manage users.");

            //Create a new user object with the provided information
            var newUser = new User(name, email, role);

            try
            {
                //Check if the email is already in use
                if (_userRepository.EmailExists(newUser.Email))
                    throw new EmailExistsException("A user with that email already exists.");

                //Add the new user to the system
                _userRepository.Add(newUser);
            }
            catch (EmailExistsException)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new InvalidOperationException("An error occurred while adding the user.", ex);
            }

        }

        public void DeleteUser(User? currentUser, int userId)
        {
            //Check if the current user is authorized to delete users from the system
            if (!_authorizationService.CanManageUsers(currentUser))
                throw new UnauthorizedAccessException("You do not have permission to manage users.");

            //Check if the user id is not valid
            if (userId <= 0)
                throw new ArgumentException("User id must be greater than zero.", nameof(userId));

            //Prevent users from deleting their own account
            if (currentUser != null && currentUser.Id == userId)
                throw new InvalidOperationException("You cannot delete your own account.");

            //Check if the user exists before attempting to delete
            if (!_userRepository.CheckExists(userId))
                throw new KeyNotFoundException($"User with this id: {userId} not found.");

            try
            {
                //Delete the user from the system
                _userRepository.Delete(userId);
            }

            //To catch KeyNotFoundException from above code and rethrow it to be handled by the caller
            catch (KeyNotFoundException)
            {
                throw;
            }

            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while deleting the user.", ex);
            }

        }

        public IEnumerable<User> GetAllUsers(User? currentUser)
        {
            //Check if the current user is authorized to Get all users in the system
            if (!_authorizationService.CanManageUsers(currentUser))
                throw new UnauthorizedAccessException("You do not have permission to manage users.");

            try
            {
                //Fetch and return all users from the system
                return _userRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while fetching users.", ex);
            }
        }

        public User? GetUserByEmail(User? currentUser, string email)
        {
            //Check if the current user is authorized to manage users
            if (!_authorizationService.CanManageUsers(currentUser))
                throw new UnauthorizedAccessException("You do not have permission to manage users.");

            //Check if the email is valid or not
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));

            try
            {
                //Fetch and return the user with the specified email from the system
                var ReturnedUser = _userRepository.GetByEmail(email);
                //Check if the user with the specified email exists in the system
                if (ReturnedUser == null)
                    throw new KeyNotFoundException($"This User email: {email} not found");
                //Return the user with the specified email
                return ReturnedUser;
            }
            //To catch KeyNotFoundException from above code and rethrow it to be handled by the caller
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while fetching the user.", ex);
            }
        }

        public User? GetUserById(User? currentUser, int userId)
        {
            //Check if the current user is authorized to Get specific user from the system
            if (!_authorizationService.CanManageUsers(currentUser))
                throw new UnauthorizedAccessException("You do not have permission to manage users.");

            //Check if the user id is not valid
            if (userId <= 0)
                throw new ArgumentException("User id must be greater than zero.", nameof(userId));

            try
            {
                //Fetch and return the user with the specified id from the system
                return _userRepository.GetById(userId);
            }
            //To catch KeyNotFoundException from above code and rethrow it to be handled by the caller
            catch (KeyNotFoundException)
            {
                throw;
            }

            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while fetching the user.", ex);
            }

        }

        public void UpdateUser(User? currentUser, int userId, string name, string email, UserRole role)
        {
            //Check if the current user is authorized to Get specific user from the system
            if (!_authorizationService.CanManageUsers(currentUser))
                throw new UnauthorizedAccessException("You do not have permission to manage users.");

            //Check if the user id is not valid
            if (userId <= 0)
                throw new ArgumentException("User id must be greater than zero.", nameof(userId));


            //Get the existing user to check if the email is being updated and if the new email is already in use by another user

            //Create a new user object with the provided information to validate the input before updating the existing user
            var validatedUser = new User(name, email, role);
        
            try
            {
                //Check if the user exists before attempting to update
                if (!_userRepository.CheckExists(userId))
                    throw new KeyNotFoundException($"User with this id: {userId} not found.");

                var userWithSameEmail = _userRepository.GetByEmail(validatedUser.Email);


                if (userWithSameEmail != null && userWithSameEmail.Id != userId)
                    throw new EmailExistsException("A user with that email already exists.");


                //Update the user's information in the system
                _userRepository.Update(userId, u =>
                {
                    u.ChangeName(validatedUser.Name);
                    u.ChangeEmail(validatedUser.Email);
                    u.ChangeRole(validatedUser.Role);
                });
            }

            //To catch KeyNotFoundException from above code and rethrow it to be handled by the caller
            catch (KeyNotFoundException)
            {
                throw;
            }

            catch (EmailExistsException)
            {
                throw;
            }

            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while updating the user.", ex);
            }
        }
    }
}
