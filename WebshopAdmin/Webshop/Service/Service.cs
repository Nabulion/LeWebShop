using System;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace Webshop.Service
{
    public static class Service
    {
        private static lewebshopEntities4 DB = Dao.Dao.getDB();

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
            createShoppingCart(temp);
            DB.UserProfiles.Add(temp);
            DB.SaveChanges();
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
        public static UserProfile validateLogin(String password, String name)
        {
            String tempPass = HashPass(password);
            UserProfile user = Dao.Dao.findUser(tempPass, name);

            if (user == null)
            {
                throw new Exception("Password not correct");
            }
            return user;
        }
        public static void deleteUserLogin(String pass, String name)
        {

            UserProfile temp = Dao.Dao.findUser(pass, name);
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
        public static void updateUserLogin(UserProfile temp, String newname, String newpass, String newEmail, String newAdress, String zipCode, bool newsletter)
        {
            Dao.Dao.updateUser(temp, newname, newpass, newEmail, newAdress, zipCode, newsletter);
        }
        public static UserProfile findUser(int id)
        {
            return DB.UserProfiles.Find(id);
        }
        public static Visa createVisa(UserProfile temp, String Visa)
        {
            return Dao.Dao.visaInfo(temp, Visa);
        }
        public static List<Product> getProductCategory(String category)
        {
            return Dao.Dao.getProduct(category);

        }
        public static Product findProduct(int id)
        {
            return Dao.Dao.findProduct(id);
        }
        public static ShoppingCart createShoppingCart(UserProfile u)
        {
            return Dao.Dao.createShoppingCart(u);
        }
        public static void findShoppingCart(UserProfile u)
        {
            
        }
        public static void addToCart(UserProfile u, Product p, int count)
        {
            Dao.Dao.AddToCart(u,p,count);

        }
            
        
    }
}