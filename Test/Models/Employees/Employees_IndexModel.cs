using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Models.Employees
{
    public class Employees_IndexModel
    {
        public int EmployeeID { get; set; }
        [DisplayName("姓")]
        public string LastName { get; set; }
        [DisplayName("名")]
        public string FirstName { get; set; }
        [DisplayName("職稱")]
        public string Title { get; set; }
        [DisplayName("尊稱")]
        public string TitleOfCourtesy { get; set; }
        [DisplayName("分機")]
        public string Extension { get; set; }
    }
}
