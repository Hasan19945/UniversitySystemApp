using UniversitySystemApp.Models;

namespace UniversitySystemApp.Interfaces
{
    public interface IUserAuthentication
    {
        User? Login(string username, string password);
        bool UsernameExists(string username);
    }
}