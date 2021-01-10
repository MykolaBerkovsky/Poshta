using Poshta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Data.Entity;

namespace Poshta.Providers
{
    public class UserRoleProvider : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            string[] roles = new string[] { };
            using (ModelDB db = new ModelDB())
            {
                // Получаем пользователя
                USER user = db.USER.Include(u => u.ROLES).FirstOrDefault(u => u.contact_number.ToString() == username);
                if (user != null && user.ROLES != null)
                {
                    // получаем роль
                    roles = new string[] { user.ROLES.role };
                }
                return roles;
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            using (ModelDB db = new ModelDB())
            {
                // Получаем пользователя
                USER user = db.USER.Include(u => u.ROLES).FirstOrDefault(u => u.contact_number.ToString() == username);

                if (user != null && user.ROLES != null && user.ROLES.role == roleName)
                    return true;
                else
                    return false;
            }
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}