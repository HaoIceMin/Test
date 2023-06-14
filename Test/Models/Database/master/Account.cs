using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Test.Models.Database.master
{
    public partial class Account
    {
        public int Id { get; set; }
        public string Account1 { get; set; }
        public string Password { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
