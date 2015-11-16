
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
        public static UserProfile findUser(String pass)
        {
            UserProfile temp = (from l in DB.UserProfiles where l.LoginUser.pass == pass select l).First();
            return temp;
        }
        public static void updateUser(LoginUser u, String newname, String newpass)
        {
            u.name = newname;
            u.pass = newpass;
            DB.SaveChanges();
        }
    }
}