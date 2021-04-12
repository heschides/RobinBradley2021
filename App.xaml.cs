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
        protected async override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var employeeViewModel = await EmployeeViewModel.CreateEmployeeViewModelAsync();
            var createNewEmployeeRecordViewModel = await CreateNewEmployeeRecordViewModel.CreateNewEmployeeRecordViewModelAsync();
            Window window = new Dashboard();
            window.Show();
        }
    }
}
