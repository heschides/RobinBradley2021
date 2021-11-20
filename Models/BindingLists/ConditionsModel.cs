using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobinBradley2021.Models.Administration
{
   public class ConditionsModel
    {
        private ObservableCollection<string> _selection;

        public ObservableCollection<string> Selection
        {
            get { return new ObservableCollection<string>() { "New", "Excellent", "Good", "Fair", "Poor", "Unacceptable" }; }
            set { _selection = value; }
        }
        }

    }


