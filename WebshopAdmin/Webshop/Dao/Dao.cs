
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
        public static LoginUser findUser(String pass)
        {
            var LoginUser = from l in DB.LoginUsers where l.pass == pass select l;
            return (LoginUser)LoginUser;
        }
        public static void updateUser(LoginUser u, String newname, String newpass)
        {
            u.name = newname;
            u.pass = newpass;
            DB.SaveChanges();
        }
    }
}