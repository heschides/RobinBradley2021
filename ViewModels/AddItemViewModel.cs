using Microsoft.Win32;
using SimplyEmployeeTracker.DataAccess;
using SimplyEmployeeTracker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyEmployeeTracker.ViewModels
{
    class AddItemViewModel
    {
        public ObservableCollection<EquipmentClassModel> EquipmentClasses { get; set; }
        private ObservableCollection<string> _selectedFiles;
        public ObservableCollection<string> SelectedFiles
        {
            get { return _selectedFiles; }
            set { _selectedFiles = value; }
        }


        public RelayCommand<object>GetEquipmentClassesCommand { get; set; }
        public async void GetEquipmentClasses(object e)
        {
            var _equipmentClasses = await GetData.EquipmentClassQueryAsync();
            var IDs = new List<int>();
            foreach (EquipmentClassModel _class in EquipmentClasses) { IDs.Add(_class.Id); }

            foreach (EquipmentClassModel _class in _equipmentClasses)
            {
                if (IDs.Contains(_class.Id))
                { }
                else { EquipmentClasses.Add(_class); }
            }
        }

        public RelayCommand<object>OpenFileDialogCommand { get; set; }
        public void OpenFileDialogWindow(object e)
        {
            List<string> _selectedFilesList;
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == true)
            {
                _selectedFilesList = openFile.SafeFileNames.ToList();
                var _selectedFiles = new ObservableCollection<string>();
                foreach (string s in _selectedFilesList)
                {
                    _selectedFiles.Add(s);
                }
            }
        }
    
        public AddItemViewModel()
        {
            SelectedFiles = new ObservableCollection<string>();
            EquipmentClasses = new ObservableCollection<EquipmentClassModel>();
            GetEquipmentClassesCommand = new RelayCommand<object>(GetEquipmentClasses);
            OpenFileDialogCommand = new RelayCommand<object>(OpenFileDialogWindow);
        }

    }
}
