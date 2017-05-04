using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.Database;

namespace FinalProject.Database
{

    /// <summary>
    /// 操作数据库
    /// </summary>
    public class DatabaseAccessor
    {
        private static readonly Entities entities;

        /// <summary>
        /// 构造方法，新建一个Entity framework访问类，然后打开数据库链接
        /// </summary>
        static DatabaseAccessor()
        {
            entities = new Entities();
            entities.Database.Connection.Open();
        }
        /// <summary>
        /// 数据库访问类
        /// </summary>
        public static Entities Instance
        {
            get
            {
                return entities;
            }
        }
    }
}
