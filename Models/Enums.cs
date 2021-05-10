using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobinBradley2021.Models
{
    public class Enums
    {
        public enum PhoneType {[Display(Name = "Home")] Home, [Display(Name = "Personal Cell")] PersonalCell, [Display(Name = "Office")] Office, [Display(Name = "Business Cell")] BusinessCell, [Display(Name = "Fax")] Fax, [Display(Name = "Other")] Other }
        public enum VehicleClass {[Display(Name = "Car")] Car, [Display(Name = "Pickup")] Pickup, [Display(Name = "Van")] Van, [Display(Name = "Box Truck")] BoxTruck, [Display(Name = "Trailer")] Trailer, [Display(Name = "Other")] Other }
        public enum GenericCondition {[Display(Name = "Excellent")] Excellent, [Display(Name = "Good")] Good, [Display(Name = "Fair")] Fair, [Display(Name = "Poor")] Poor, [Display(Name = "Unacceptable")] Unacceptable }
        public enum EmployeeUniformSize {[Display(Name = "Small")] Small, [Display(Name = "Medium")] Medium, [Display(Name = "Large")] Large, [Display(Name = "Extra Large")] ExtraLarge, [Display(Name = "2X Large")] XXLarge, [Display(Name = "3XLarge")] XXXLarge, [Display(Name = "4X Large")] XXXXLarge,  [Display(Name = "Small Tall")] SmallTall, [Display(Name = "MediumTall")] MediumTall, [Display(Name = "Large Tall")] LargeTall, [Display(Name = "Extra Large Tall")] XLTall, [Display(Name = "2XLarge Tall")] XXLargeTall, [Display(Name = "3XLarge Tall")] XXXLTall, [Display(Name = "3XLarge Tall")] XXXLargeTall, [Display(Name = "4XLarge Tall")] XXXXLargeTall }
        public enum EmailType {[Display(Name = "Personal")] Personal, [Display(Name = "Work")] Work, [Display(Name = "Other")] Other }
        
    }
}
