using FluentValidation;
using RobinBradley2021.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobinBradley2021.DataValidation
{
    class EmployeeValidation : AbstractValidator<EmployeeModel>
    {
        public EmployeeValidation()
        {
            RuleFor(e => e.FirstName).NotEmpty()
                .Length(3, 50);

        }
    }
}
