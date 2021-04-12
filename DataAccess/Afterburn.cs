using SimplyEmployeeTracker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SimplyEmployeeTracker.DataAccess
{
    public static class Afterburn
    {
        public static void BurnEmployeeCollection(ObservableCollection<EmployeeModel> e)
        {
            foreach (EmployeeModel employee in e)
            {
                var groupedEmailList = new List<EmailModel>();
                var groupedPhoneList = new List<PhoneModel>();
                var groupedCitations = new List<CitationModel>();
                var groupedCertificationsList = new List<CertificationModel>();
                List<int> phoneIDs = new List<int>();
                List<int> emailIDs = new List<int>();
                List<int> citationIDs = new List<int>();
                List<int> certificationIDs = new List<int>();

                if (employee.Emails != null)
                {
                    foreach (EmailModel email in employee.Emails)
                    {
                        if (!emailIDs.Contains(email.ID))
                        {
                            emailIDs.Add(email.ID);
                            groupedEmailList.Add(email);
                        }
                    }
                }
                if (employee.Phones != null) {
                    foreach (PhoneModel phoneModel in employee.Phones)
                    {
                        if (!phoneIDs.Contains(phoneModel.ID))
                        {
                            phoneIDs.Add(phoneModel.ID);
                            groupedPhoneList.Add(phoneModel);
                        }
                    }
                }

                if (employee.Citations != null)
                {
                    foreach (CitationModel citationModel in employee.Citations)
                        if (!citationIDs.Contains(citationModel.ID))
                        {
                            citationIDs.Add(citationModel.ID);
                            groupedCitations.Add(citationModel);
                        }
                }
                if (employee.Certifications != null)
                {
                    foreach (CertificationModel certificationModel in employee.Certifications)
                        if (!certificationIDs.Contains(certificationModel.ID))
                        {
                            certificationIDs.Add(certificationModel.ID);
                            groupedCertificationsList.Add(certificationModel);
                        }
                }

                ObservableCollection<CertificationModel> c = new ObservableCollection<CertificationModel>(groupedCertificationsList);
                employee.Certifications = c;
                ObservableCollection<CitationModel> g = new ObservableCollection<CitationModel>(groupedCitations);
                employee.Citations = g;
                ObservableCollection<EmailModel> em = new ObservableCollection<EmailModel>(groupedEmailList);
                employee.Emails = em;
                ObservableCollection<PhoneModel> p = new ObservableCollection<PhoneModel>(groupedPhoneList);
                employee.Phones = p;
            }
        }

    }
}

