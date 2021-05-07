using RobinBradley2021.DataAccess;
using RobinBradley2021.Models;
using RobinBradley2021.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobinBradley2021.ViewModels
{
    public class EquipmentTabViewModel
    {
        public ObservableCollection<EquipmentModel> Equipment { get; set; }

        public RelayCommand<object> OpenAddItemWindowCommand { get; private set; }
        public static void OpenAddItemWindow(object e)
        {
            var window = new AddItem();
            window.Show();
        }
        public RelayCommand<object> OpenAssignItemWindowCommand { get; set; }
        public static void OpenAssignItemWindow(object e)
        {
            var window = new AssignItemWindow();
            window.Show();
        }
        public RelayCommand<object> OpenReturnItemWindowCommand { get; set; }
        public static void OpenReturnItemWindow(object e)
        {
            var window = new ReturnItemWindow();
            window.Show();
        }
        public RelayCommand<object> OpenAddCICRecordWindowCommand { get; set; }
        public static void OpenAddCICRecordWindow(object e)
        {
            var window = new AddCICRecordWindow();
            window.Show();
        }
        public RelayCommand<object> OpenRepairItemWindowCommand { get; set; }
        public static void OpenRepairItemWindow(object e)
        {
            var window = new RepairItemWindow();
            window.Show();
        }
        public RelayCommand<object> OpenAssignmentsWindowCommand { get; set; }
        public static void OpenAssignmentsWindow()
        {
            var window = new AssignItemWindow();
            window.Show();
        }
        public RelayCommand<object>RefreshEquipmentCommand { get; private set; }
        public async void RefreshEquipment(object e)
        {

           var equipment = await GetData.EquipmentQueryAsync();
            var Ids = new List<int>();
            foreach (EquipmentModel _equipment in Equipment) { Ids.Add(_equipment.Id); }

            foreach (EquipmentModel _equipment in equipment)
            {
                if (Ids.Contains(_equipment.Id))
                { }
                else { Equipment.Add(_equipment); }
            }
        }

        public EquipmentTabViewModel()
        {
            Equipment = new ObservableCollection<EquipmentModel>();
            OpenAddItemWindowCommand = new RelayCommand<object>(OpenAddItemWindow);
            OpenAssignItemWindowCommand = new RelayCommand<object>(OpenAssignItemWindow);
            OpenReturnItemWindowCommand = new RelayCommand<object>(OpenReturnItemWindow);
            OpenAddCICRecordWindowCommand = new RelayCommand<object>(OpenAddCICRecordWindow);
            OpenRepairItemWindowCommand = new RelayCommand<object>(OpenRepairItemWindow);
            OpenAssignItemWindowCommand = new RelayCommand<object>(OpenAssignItemWindow);
            RefreshEquipmentCommand = new RelayCommand<object>(RefreshEquipment);
        }

    }
}
