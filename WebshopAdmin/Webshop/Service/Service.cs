using System;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webshop.Service
{
    public class Service
    {
        lewebshopEntities DB = Dao.Dao.getDB();

        public LoginUser createUserLogin(String name, String password)
        {
            LoginUser tempUser = new LoginUser();
            tempUser.pass = HashPass(password);
            tempUser.name = name;
            DB.LoginUsers.Add(tempUser);
            DB.SaveChanges();
            return tempUser;
        }
        private String HashPass(String password)
        {
            SHA1 sha1 = SHA1.Create();

            byte[] hashData = sha1.ComputeHash(Encoding.Default.GetBytes(password));

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hashData.Length; i++)
            {
                sb.Append(hashData[i].ToString());
            }

            return sb.ToString();
        }
        public LoginUser validateLogin(String password)
        {
            String tempPass = HashPass(password);
            LoginUser user = Dao.Dao.findUser(tempPass);
            if (user == null)
            {
                throw new Exception("Password not correct");
            }
            return user;
        }
        public void deleteUserLogin(String pass)
        {
            LoginUser temp = Dao.Dao.findUser(pass);
            DB.LoginUsers.Remove(temp);
            DB.SaveChanges();
        }
        public void updateUserLogin(String pass, String newname, String newpass)
        {
            LoginUser temp = Dao.Dao.findUser(pass);
            Dao.Dao.updateUser(temp, newname, newpass);
        }

    }
}