using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobinBradley2021.Models.BindingLists
{
    public class FuelLevelModel
    {
        private ObservableCollection<string> _selection;

        public ObservableCollection<string> Selection
        {
            get { return new ObservableCollection<string> { "Full", "90%", "80%", "70%", "60%", "50%", "40%", "30%", "20%", "10%", "Empty" }; }
            set { _selection = value; }
        }

    }
}
