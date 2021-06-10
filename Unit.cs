using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace PracticalTest
{
    [XmlRoot("Unit")]
    public class Unit : Container
    {
        public Unit() { }

        public Unit(string name)
        {
            Name = name;
            Units = new HashSet<Unit>();
            Employees = new HashSet<Employee>();
        }

        public List<string> GetEmployeeDetails()
        {
            var details = new List<String>();
            foreach (var e in Employees)
            {
                details.Add(e.GetEmployeeDetails(Name));
            }
            foreach (var u in Units)
            {
                details.AddRange(u.GetEmployeeDetails());
            }
            return details;
        }
    }
}
