
using MelidyaEntity.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelidyaEntity.EntityFramework
{
    public class RepositoryBase
    {
        protected static DataContext dataContext;
        private static object _lock = new object();

        protected RepositoryBase()
        {
            CreateContext();
        }

        private static void CreateContext()
        {
            if (dataContext == null)
            {
                lock (_lock)
                {
                    if (dataContext == null)
                    {
                        dataContext = new  DataContext();
                    }
                }
            }
        }
    }
}
