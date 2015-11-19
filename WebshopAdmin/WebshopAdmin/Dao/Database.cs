using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebshopAdmin;

namespace WebshopAdmin.Dao
{
    public class Database
    {
        private static lewebshopEntities1 DB = null;
        public static lewebshopEntities1 db
        {
            get
            {
                if (DB == null)
                {
                    DB = new lewebshopEntities1();
                }
                return DB;
            }

        }

    }
}
