using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using RobinBradley2021.Models;
using RobinBradley2021.Models.Employees;
using RobinBradley2021.Models.Equipment;
using RobinBradley2021.Views.Equipment;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace RobinBradley2021.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {

        //PROPERTIES

        public ObservableCollection<EquipmentAssignmentRecordModel> EquipmentAssignments { get; set; }
        public ObservableCollection<VehicleAssignmentRecordModel> VehicleAssignments { get; set; }
        public ObservableCollection<CertificationRecordModel> Certifications { get; set; }
        public ObservableCollection<CICRecordModel> CIC { get; set; }

        private EquipmentAssignmentRecordModel selectedAssignmentRecent;
        public EquipmentAssignmentRecordModel SelectedAssignmentRecent
        {
            get => selectedAssignmentRecent;
            set
            {
                if (recentAssignmentLineItems != null)
                {
                    if (Set(ref selectedAssignmentRecent, value))
                    {
                        RaisePropertyChanged(nameof(RecentAssignmentLineItems));
                    }
                }
            }
        }
        private EquipmentAssignmentRecordModel selectedAssignmentDue;
        public EquipmentAssignmentRecordModel SelectedAssignmentDue
        {
            get => selectedAssignmentDue;
            set
            {
                if (recentAssignmentLineItems != null)
                {
                    if (Set(ref selectedAssignmentDue, value))
                    {
                        RaisePropertyChanged(nameof(DueAssignmentLineItems));
                    }
                }
            }
        }
        private EquipmentAssignmentRecordModel selectedAssignmentOverdue;
        public EquipmentAssignmentRecordModel SelectedAssignmentOverdue
        {
            get => selectedAssignmentOverdue;
            set
            {
                if (dueAssignmentLineItems != null)
                {
                    if (Set(ref selectedAssignmentOverdue, value))
                    {
                        RaisePropertyChanged(nameof(OverdueAssignmentLineItems));
                    }
                }

            }
        }

        private ObservableCollection<EquipmentAssignmentLineItemModel> recentAssignmentLineItems;
        public ObservableCollection<EquipmentAssignmentLineItemModel> RecentAssignmentLineItems
        {
            get
            {
                if (SelectedAssignmentRecent != null) { recentAssignmentLineItems = SelectedAssignmentRecent.LineItems; }
                return recentAssignmentLineItems;
            }
            set
            {
                recentAssignmentLineItems = value;
            }
        }
        private ObservableCollection<EquipmentAssignmentLineItemModel> dueAssignmentLineItems;
        public ObservableCollection<EquipmentAssignmentLineItemModel> DueAssignmentLineItems
        {
            get
            {
                if (SelectedAssignmentDue != null) { dueAssignmentLineItems = SelectedAssignmentDue.LineItems; }
                return dueAssignmentLineItems;
            }
            set
            {
                dueAssignmentLineItems = value;
            }
        }
        private ObservableCollection<EquipmentAssignmentLineItemModel> overdueAssignmentLineItems;
        public ObservableCollection<EquipmentAssignmentLineItemModel> OverdueAssignmentLineItems
        {
            get
            {
                if (SelectedAssignmentOverdue != null) { overdueAssignmentLineItems = SelectedAssignmentOverdue.LineItems; }
                return overdueAssignmentLineItems;
            }
            set { overdueAssignmentLineItems = value; }
        }

        public DateTime now = DateTime.Now;
        public EmployeeTabViewModel EmployeeTabVM = new EmployeeTabViewModel();


        //COLLECTIONVIEWS

        public ICollectionView EquipmentAssignmentsCollectionOverdue
        {
            get
            {
                var view = new CollectionViewSource { Source = EquipmentAssignments }.View;
                view.Filter = item => item is EquipmentAssignmentRecordModel model && model.DueDate < now && model.IsResolved == false;
                return view;
            }
        }
        public ICollectionView EquipmentAssignmentCollectionDue
        {
            get
            {
                if (EquipmentAssignments == null)
                    return null;
                else
                {
                    foreach (EmployeeModel _employee in EmployeeTabVM.Employees)
                    {
                        foreach (EquipmentAssignmentRecordModel _record in _employee.EquipmentAssignments)
                        {
                            if (!EquipmentAssignments.Contains(_record))
                            {
                                EquipmentAssignments.Add(_record);
                            }
                        }
                    }
                }
                var view = new CollectionViewSource { Source = EquipmentAssignments }.View;
                view.Filter = item => item is EquipmentAssignmentRecordModel model && model.IsResolved == false && model.IsStandardIssue == false;
                return view;
            }
        }
        public ICollectionView EquipmentAssignmentsCollectionRecent
        {
            get
            {
                if (EquipmentAssignments == null)
                    return null;
                else
                {
                    foreach (EmployeeModel _employee in EmployeeTabVM.Employees)
                    {
                        foreach (EquipmentAssignmentRecordModel _record in _employee.EquipmentAssignments)
                        {
                            if (!EquipmentAssignments.Contains(_record))
                            {
                                EquipmentAssignments.Add(_record);

                            }
                        }
                    }
                }
                var view = new CollectionViewSource { Source = EquipmentAssignments }.View;
                view.Filter = item => item is EquipmentAssignmentRecordModel model && model.DueDate < now && model.IsResolved == false;
                return view;
            }
        }

        public ICollectionView VehicleAssignmentsCollectionRecent
        {
            get
            {
                var view = new CollectionViewSource { Source = VehicleAssignments }.View;
                view.Filter = item => item is VehicleAssignmentRecordModel model;

                return view;
            }
        }
        public ICollectionView VehicleAssignmentsCollectionDue
        {
            get
            {
                if (VehicleAssignments == null)
                    return null;
                else
                {
                    foreach (EmployeeModel _employee in EmployeeTabVM.Employees)
                    {
                        foreach (VehicleAssignmentRecordModel _record in _employee.VehicleAssignments)
                        {
                            if (!VehicleAssignments.Contains(_record))
                            {
                                VehicleAssignments.Add(_record);
                            }
                        }
                    }
                }
                var view = new CollectionViewSource { Source = VehicleAssignments }.View;
                view.Filter = item => item is VehicleAssignmentRecordModel model;
                return view;
            }
        }
        public ICollectionView VehicleAssignmentsCollectionOverdue
        {
            get
            {
                if (VehicleAssignments == null)
                    return null;
                else
                {
                    foreach (EmployeeModel _employee in EmployeeTabVM.Employees)
                    {
                        foreach (VehicleAssignmentRecordModel _record in _employee.VehicleAssignments)
                        {
                            if (!VehicleAssignments.Contains(_record))
                            {
                                VehicleAssignments.Add(_record);

                            }
                        }
                    }
                }
                var view = new CollectionViewSource { Source = VehicleAssignments }.View;
                view.Filter = item => item is VehicleAssignmentRecordModel model;
                return view;
            }
        }

        public ICollectionView CertificationsCollectionRecent
        {
            get
            {
                var view = new CollectionViewSource { Source = Certifications }.View;
                view.Filter = item => item is CertificationRecordModel model && model.DateEnd < DateTime.Now.AddDays(-5);
                return view;
            }
        }
        public ICollectionView CertificationsCollectionDue
        {
            get
            {
                var view = new CollectionViewSource { Source = Certifications }.View;
                view.Filter = item => item is CertificationRecordModel model && model.DateEnd < DateTime.Now.AddDays(30) && model.DateEnd > DateTime.Now;
                return view;
            }
        }
        public ICollectionView CertficiationsCollectionOverdue
        {
            get
            {
                var view = new CollectionViewSource { Source = Certifications }.View;
                view.Filter = item => item is CertificationRecordModel model && model.DateEnd > DateTime.Now;
                return view;
            }
        }

        public ICollectionView CICCollectionRecent
        {
            get
            {
                var view = new CollectionViewSource { Source = CIC }.View;
                view.Filter = item => item is CICRecordModel model && model.DateStart < DateTime.Now.AddDays(-5);
                return view;
            }
        }
        public ICollectionView CCICCollectionDue
        {
            get
            {
                var view = new CollectionViewSource { Source = CIC }.View;
                view.Filter = item => item is CICRecordModel model && model.DateDue < DateTime.Now.AddDays(30) && model.DateDue > DateTime.Now;
                return view;
            }
        }
        public ICollectionView CICCollectionOverdue
        {
            get
            {
                var view = new CollectionViewSource { Source = CIC }.View;
                view.Filter = item => item is CICRecordModel model && model.DateDue > DateTime.Now;
                return view;
            }
        }

        //METHODS
        private void Row_DoubleClick(object e)
        {
            var window = new AssignmentLineItemsWindow();
            window.Show();
        }

        // COMMANDS
        public RelayCommand<object> OpenLineItemWindow { get; set; }

        //CONSTRUCTOR
        public DashboardViewModel()
        {
            EquipmentAssignments = new ObservableCollection<EquipmentAssignmentRecordModel>();
            VehicleAssignments = new ObservableCollection<VehicleAssignmentRecordModel>();
            Certifications = new ObservableCollection<CertificationRecordModel>();
            CIC = new ObservableCollection<CICRecordModel>();
            OpenLineItemWindow = new RelayCommand<Object>(Row_DoubleClick);
            RecentAssignmentLineItems = new ObservableCollection<EquipmentAssignmentLineItemModel>();
        }
    }
}