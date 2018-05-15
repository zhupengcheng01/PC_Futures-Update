
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Controls;

namespace Utilities
{
    public class UserControlHelper
    {
        /// <summary>
        /// 接受数据的反射到类的方法
        /// </summary>
        /// <param name="classname"></param>
        /// <param name="Mothed"></param>
        /// <param name="param"></param>
        public static void CreateAaccpMothed(string classname,string Mothed,object param)
        {
            Assembly assembly = Assembly.Load("Utilities");
            Type type = assembly.GetType("Utilities.ViewModel." + classname);
            object instance = assembly.CreateInstance("Utilities.ViewModel." + classname);
            Object[] params_obj = new Object[1];
            params_obj[0] = param;
            object value = type.GetMethod(Mothed).Invoke(instance, params_obj);
        }

        public static bool IsNum(string num)
        {

            double m = 0;
            if (!double.TryParse(num, out m))
            {

                return false;
            }
            if (Convert.ToDouble(num) <= 0)
            {

                return false;
            }
            return true;
        }
    }
}
