using GalaSoft.MvvmLight;
using RobinBradley2021.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobinBradley2021.ViewModels.Equipment
{
    class AddCICRecordWindowViewModel : ViewModelBase

    {
        public ObservableCollection<CICClassModel> CICClasses { get; set; }
        public ObservableCollection<CertificationClassModel> CertificationClasses { get; set; }
        public string CertificationEquipmentId { get; set; }
        public string InspectionEquipmentId { get; set; }
        public string CICEquipmentId { get; set; }
        public string SelectedCertificationClass { get; set; }
        public string SelectedCalibrationClass { get; set; }
        


    }
}
