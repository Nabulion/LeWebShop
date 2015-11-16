
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webshop.Dao
{
    public class Dao
    {
        private static lewebshopEntities DB;

        public static lewebshopEntities getDB()
        {
            if (DB == null)
            {
                DB = new lewebshopEntities();
            }
            return DB;
        }
        public static UserProfile findUser(String pass, String name)
        {
            UserProfile temp = (from l in DB.UserProfiles where l.LoginUser.pass == pass && l.LoginUser.name == name select l).First();
            return temp;
        }
        public static void updateUser(UserProfile u, String newname, String newpass, String newEmail, String newAdress, String zipCode, bool newsletter)
        {

            u.LoginUser.name = newname;
            u.LoginUser.pass = newpass;
            u.Email = newEmail;
            u.adress = newAdress;
            u.zipcode = zipCode;
            u.newsletter = newsletter;
            DB.SaveChanges();
        }
    }
}