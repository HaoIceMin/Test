using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Models.Employees
{
    public class Employees_EditModel
    {
        [Required]
        public int EmployeeID { get; set; }
        [Required]
        [MaxLength(20)]
        [DisplayName("姓")]
        public string LastName { get; set; }
        [DisplayName("名")]
        [Required]
        [MaxLength(10)]
        public string FirstName { get; set; }
        [DisplayName("職稱")]
        [MaxLength(30)]
        public string Title { get; set; }
        [DisplayName("尊稱")]
        [MaxLength(25)]
        public string TitleOfCourtesy { get; set; }
        public List<SelectListItem> TitleOfCourtesyList { get; set; }
        [DisplayName("分機")]
        [MaxLength(4)]
        public string Extension { get; set; }
        [DisplayName("生日")]
        public DateTime BirthDate { get; set; }
        [DisplayName("雇用日")]
        public DateTime HireDate { get; set; }
        [DisplayName("地址")]
        [MaxLength(60)]
        public string Address { get; set; }
        [DisplayName("城市")]
        [MaxLength(15)]
        public string City { get; set; }
        [DisplayName("地區")]
        [MaxLength(15)]
        public string Region { get; set; }
        [DisplayName("郵遞區號")]
        [MaxLength(4)]
        public string PostalCode { get; set; }
        [DisplayName("國家")]
        [MaxLength(15)]
        public string Country { get; set; }
        [DisplayName("家電")]
        [MaxLength(24)]
        public string HomePhone { get; set; }
        [DisplayName("備註")]
        public string Notes { get; set; }
        [DisplayName("上司")]
        public string ReportsTo { get; set; }
        public List<SelectListItem> ReportsToList { get; set; }
    }
}
