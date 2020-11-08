using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagementProj.Models
{
    public static class UserManager
    {
        static ApplicationDbContext db = new ApplicationDbContext();
        static UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        static RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

        public static List<string> GetAllRoles()
        {
            return db.Roles.Select(a => a.Name).ToList();
        }
        public static List<ApplicationUser> GetAllUserNames()
        {
            return db.Users.ToList();
        }
        public static bool AddNewRole(string roleName)  // return ture means success, false means role name already exist
        {
            if(!roleManager.RoleExists(roleName))
            {
                roleManager.Create(new IdentityRole { Name = roleName });
                return true;                 
            }
            else
            {
                return false;
            }
        }
        public static IdentityResult RemoveRole(string roleName)
        {
            return roleManager.Delete(new IdentityRole { Name = roleName });
        }
        public static bool CheckUserInRole(string userId, string roleName)
        {
            return userManager.IsInRole(userId, roleName);
        }
        public static IdentityResult AddUserToRole(string userId, string roleName)
        {
            return userManager.AddToRole(userId, roleName);
        }
        public static List<string> GetAllRolesForUser(string userId)
        {
            return userManager.GetRoles(userId).ToList();
        }

    }
}