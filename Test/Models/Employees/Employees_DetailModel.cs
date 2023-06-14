using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Models.Employees
{
    public class Employees_DetailModel
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
        [DisplayName("生日")]
        public string BirthDate { get; set; }
        [DisplayName("雇用日")]
        public string HireDate { get; set; }
        [DisplayName("地址")]
        public string Address { get; set; }
        [DisplayName("城市")]
        public string City { get; set; }
        [DisplayName("地區")]
        public string Region { get; set; }
        [DisplayName("郵遞區號")]
        public string PostalCode { get; set; }
        [DisplayName("國家")]
        public string Country { get; set; }
        [DisplayName("家電")]
        public string HomePhone { get; set; }
        [DisplayName("備註")]
        public string Notes { get; set; }
        [DisplayName("上司")]
        public string ReportsTo { get; set; }
        public int? ReportsToIndex { get; set; }
    }
}
