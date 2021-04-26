using SimplyEmployeeTracker.DataAccess;
using SimplyEmployeeTracker.ViewModels;
using SimplyEmployeeTracker.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SimplyEmployeeTracker
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
