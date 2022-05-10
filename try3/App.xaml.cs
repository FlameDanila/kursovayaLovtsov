using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace try3
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static KinoteatrEntities1 bd = new KinoteatrEntities1(); 
        public static Product izmenenie = new Product();
        public static Category categoryChange = new Category();
    }
}
