using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailService.Models
{
    public class EmailModel
    {
        public string emailId { get; set; }
        public string visitorName { get; set; }
        public string aadharNumber { get; set; }
        public string employeeId { get; set; }
        public int visitorType { get; set; }
        public string phoneNumber { get; set; }
    }
}