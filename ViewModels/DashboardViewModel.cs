using GalaSoft.MvvmLight;
using RobinBradley2021.Models;
using RobinBradley2021.Other;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RobinBradley2021.DataAccess.GetData;
namespace RobinBradley2021.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        public ObservableCollection<EquipmentAssignmentRecordModel> RecentEquipmentAssignments { get; set; }
        public ObservableCollection<EquipmentAssignmentRecordModel> DueEquipmentAssignments { get; set; }
        public ObservableCollection<EquipmentAssignmentRecordModel> OverdueEquipmentAssignments { get; set; }
        public ObservableCollection<VehicleAssignmentRecordModel> RecentVehicleAssignments { get; set; }
        public ObservableCollection<VehicleAssignmentRecordModel> DueVehicleAssignments { get; set; } 
        public ObservableCollection<VehicleAssignmentRecordModel> OverdueVehicleAssignments { get; set; }
        public ObservableCollection<CertificationModel> DueEmployeeCertifications { get; set; }
        public ObservableCollection<CertificationModel> OverdueEmployeeCertifications { get; set; }
        public ObservableCollection<CICModel> DueCIC { get; set; }
        public ObservableCollection<CICModel> OverdueCIC { get; set; }

        public RelayCommand<object> LoadInitialDataCommand { get; set; }
        public async void LoadInitialData(object e)
        {
            var equipmentAssignments = await EquipmentAssignmentQueryAsync();
            RecentEquipmentAssignments = new ObservableCollection<EquipmentAssignmentRecordModel>(equipmentAssignments.Where(x => x.DateOut >= DateTime.Now.AddDays(-2) && x.DateOut <=DateTime.Today ));
            DueEquipmentAssignments = new ObservableCollection<EquipmentAssignmentRecordModel>(equipmentAssignments.Where(x => x.DueDate == DateTime.Now && x.IsResolved == false));
            OverdueEquipmentAssignments = new ObservableCollection<EquipmentAssignmentRecordModel>(equipmentAssignments.Where(x => x.IsResolved == false && x.DueDate < DateTime.Today));
            var vehicleAssignments = await VehicleAssignmentQueryAsync();
            RecentVehicleAssignments = new ObservableCollection<VehicleAssignmentRecordModel>(vehicleAssignments.Where(x => x.DateOut >= DateTime.Now.AddDays(-2) && x.DateOut <= DateTime.Today));
            DueVehicleAssignments = new ObservableCollection<VehicleAssignmentRecordModel>(vehicleAssignments.Where(x => x.IsResolved == false && x.DueDate == DateTime.Today));
            OverdueVehicleAssignments = new ObservableCollection<VehicleAssignmentRecordModel>(vehicleAssignments.Where(x => x.IsResolved == false && x.DueDate < DateTime.Today));
        }

        public DashboardViewModel()
        {
              
        }
    }
}
