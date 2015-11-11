using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebshopAdmin;

namespace webshopAdmin.Dao
{
    public class Database
    {
        private static lewebshopEntities DB = null;
        public static lewebshopEntities db
        {
            get
            {
                if (DB == null)
                {
                    DB = new lewebshopEntities();
                }
                return DB;
            }

        }

    }
}
