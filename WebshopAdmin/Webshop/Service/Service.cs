using System;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webshop.Service
{
    public static class Service
    {
        private static lewebshopEntities DB = Dao.Dao.getDB();

        public static UserProfile createUser(String name, String password, String email, String adress, String zipcode, bool newsletter)
        {
            LoginUser tempUser = new LoginUser();
            tempUser.pass = HashPass(password);
            tempUser.name = name;
            DB.LoginUsers.Add(tempUser);
            UserProfile temp = createUserProfile(email, adress, zipcode, newsletter, tempUser);
            try
            {
                DB.SaveChanges();
            }
            catch
            {

            }
            return temp;
        }
        private static UserProfile createUserProfile(String email, String adress, String zipcode, bool newsletter, LoginUser u)
        {
            UserProfile temp = new UserProfile();
            temp.Email = email;
            temp.adress = adress;
            temp.zipcode = zipcode;
            temp.newsletter = newsletter;
            temp.LoginUser = u;
            DB.UserProfiles.Add(temp);
            return temp;
        }
        private static String HashPass(String password)
        {
            SHA1 sha1 = SHA1.Create();

            var input = Encoding.ASCII.GetBytes(password);

            var hashData = sha1.ComputeHash(input);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hashData.Length; i++)
            {
                sb.Append(hashData[i].ToString("X2"));
            }

            return sb.ToString();
        }
        public static UserProfile validateLogin(String password)
        {
            String tempPass = HashPass(password);
            UserProfile user = Dao.Dao.findUser(tempPass);
           
            if (user == null)
            {
                throw new Exception("Password not correct");
            }
            return user;
        }
        public static void deleteUserLogin(String pass)
        {
            
            UserProfile temp = Dao.Dao.findUser(pass);
            DB.LoginUsers.Remove(temp.LoginUser);
            DB.UserProfiles.Remove(temp);
            try
            {
                DB.SaveChanges();
            }
            catch
            {
                DB.Entry(temp).Reload();
            }
            
        }
        public static void updateUserLogin(String pass, String newname, String newpass)
        {
            UserProfile temp = Dao.Dao.findUser(pass);
            Dao.Dao.updateUser(temp.LoginUser, newname, newpass);
        }
        public static UserProfile findUser(int id)
        {
            return DB.UserProfiles.Find(id);
        }

    }
}