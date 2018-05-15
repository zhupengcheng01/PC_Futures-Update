using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Utilities
{
    public class KeyCodeValue
    {
        public static Dictionary<int, string> KeyValues = new Dictionary<int, string>();
        public static void AddKeys()
        {
            //KeyValues.Clear();
            //foreach (var e in Enum.GetValues(typeof(Key)))
            //{
            //    int key = Convert.ToInt32(e);

            //    string EnumName = e.ToString();
            //    KeyValues.Add(key, EnumName);
            //}

        }
        

    }
}
