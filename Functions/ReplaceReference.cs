using RobinBradley2021.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobinBradley2021.Functions
{
    public class ReferenceAction
    {
        public static void ReplaceReferences(ObservableCollection<EmployeeStatusModel> collection, EmployeeModel selectedEmployee)
        {
            if (collection.Count > 0)
            {

                for (int i = collection.Count - 1; i >= 0; i--)
                {
                    if (selectedEmployee.Status.Name == collection[i].Name && !ReferenceEquals(selectedEmployee.Status.Name, collection[i].Name))
                    {
                        collection.Add(selectedEmployee.Status);
                        collection.Remove(collection[i]);
                    }
                }
            }
        }         
        public static void ReplaceReferences(ObservableCollection<JobTitleModel> collection, EmployeeModel selectedEmployee)
        {
            if (collection.Count > 0)
            {

                for (int i = collection.Count - 1; i >= 0; i--)
                {
                    if (selectedEmployee.JobTitle.Name == collection[i].Name && !ReferenceEquals(selectedEmployee.JobTitle.Name, collection[i].Name))
                    {
                        collection.Add(selectedEmployee.JobTitle);
                        collection.Remove(collection[i]);
                    }
                }
            }
        }
        public static void ReplaceReferences(ObservableCollection<DepartmentModel> collection, EmployeeModel selectedEmployee)
        {
            if (collection.Count > 0)
            {

                for (int i = collection.Count - 1; i >= 0; i--)
                {
                    if (selectedEmployee.Department.Name == collection[i].Name && !ReferenceEquals(selectedEmployee.Department.Name, collection[i].Name))
                    {
                        collection.Add(selectedEmployee.Department);
                        collection.Remove(collection[i]);
                    }
                }
            }
        }
    }
}
