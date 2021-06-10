using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace PracticalTest
{
    [XmlRoot("Employee")]
    public class Employee
    {
        [XmlText]
        public string EmployeeName { get; set; }

        [XmlAttribute("Title")]
        public string Title { get; set; }

        public Employee() { }

        public Employee(string name, string title)
        {
            EmployeeName = name;
            Title = title;
        }

        public string GetEmployeeDetails()
        {
            return String.Format("Name: {0}, Title: {1}", EmployeeName, Title);
        }

        public string GetEmployeeDetails(string unitName)
        {
            return String.Format("{0}, Unit: {1}", GetEmployeeDetails(), unitName);
        }
    }
}
