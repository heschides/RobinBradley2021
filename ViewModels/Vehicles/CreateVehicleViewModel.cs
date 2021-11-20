using RobinBradley2021.DataAccess;
using RobinBradley2021.Models;
using RobinBradley2021.Models.Vehicles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RobinBradley2021.Models.Enums;

namespace RobinBradley2021.ViewModels.Vehicles
{
    public class CreateVehicleViewModel
    {
        //PROPERTIES
        public int Id { get; set; }
        public string FleetNumber { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Year { get; set; }
        public ObservableCollection<VehicleClassModel> VehicleClassOptions { get; set; }
        public VehicleClassModel SelectedClass { get; set; }
        public string VIN { get; set; }
        public string GasCardNumber { get; set; }
        public string TransponderNumber { get; set; }
        public Month RegistrationMonth { get; set; }
        public Month InspectionMonth { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Dealership { get; set; }
        public string RegistrationNumber { get; set; }
        public decimal? Price { get; set; }
        public int? WarrantyMonths { get; set; }
        public ObservableCollection<string> MonthList { get; set; }

        //COMMANDS
        public RelayCommand<object> LoadInitialDataCommand { get; set; }
        public RelayCommand<object> CreateVehicleCommand { get; set; }

        //METHODS
        public void CreateNewVehicle(object e)
        {
            var newVehicle = new VehicleModel();
            newVehicle.Id = Id;
            newVehicle.FleetNumber = FleetNumber;
            newVehicle.Make = Manufacturer;
            newVehicle.Model = Model;
            newVehicle.Color = Color;
            newVehicle.Year = Year;
            newVehicle.Class = SelectedClass;
            newVehicle.VIN = VIN;
            newVehicle.RegistrationNumber = RegistrationNumber;
            newVehicle.GasCardNumber = GasCardNumber;
            newVehicle.TransponderNumber = TransponderNumber;
            newVehicle.RegistrationMonth = RegistrationMonth;
            newVehicle.InspectionMonth = InspectionMonth;
            newVehicle.PurchaseDate = PurchaseDate;
            newVehicle.Price = Price;
            newVehicle.WarrantyMonths = WarrantyMonths;
            VehicleRepository.CreateVehicle(newVehicle);

            Id = int.MinValue;
            FleetNumber = string.Empty;
            Manufacturer = string.Empty;
            Model = string.Empty;
            Color = string.Empty;
            Year = null;
            SelectedClass = null;
            VIN = string.Empty;
            GasCardNumber = string.Empty;
            TransponderNumber = string.Empty;
            RegistrationMonth = Month.January;
            InspectionMonth = Month.January;
            PurchaseDate = DateTime.MinValue;
            Price = null;
            WarrantyMonths = null;
        }

        public async void LoadInitialData(object e)
        {

            var vehicleClassOptions = await DataAccess.VehicleRepository.VehicleClassQueryAsync();
            foreach (VehicleClassModel _model in vehicleClassOptions)
            {
                VehicleClassOptions.Add(_model);
            }
            foreach (var _month in Enum.GetNames<Month>())
            {
                MonthList.Add(_month);
            }
        }

        public CreateVehicleViewModel()
        {
            VehicleClassOptions = new ObservableCollection<VehicleClassModel>();
            MonthList = new ObservableCollection<string>();
            CreateVehicleCommand = new RelayCommand<object>(CreateNewVehicle);
            LoadInitialDataCommand = new RelayCommand<object>(LoadInitialData);

        }
    }
}
