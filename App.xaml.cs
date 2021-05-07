using RobinBradley2021.DataAccess;
using RobinBradley2021.ViewModels;
using RobinBradley2021.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RobinBradley2021
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            System.Threading.Thread.Sleep(3000);
            Window window = new MainWindow();
            window.Show();       
        }
    }
}
