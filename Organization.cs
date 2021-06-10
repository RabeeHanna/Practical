using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace PracticalTest
{
    /// <summary>
    /// Wrapper class used for making JSON object have the Organization attribute.
    /// </summary>
    public class OrganizationWrapper
    {
        public Organization Organization { get; set; }

        public OrganizationWrapper(Organization org)
        {
            Organization = org;
        }
    }

    [XmlRoot("Organization")]
    public class Organization : Container
    {
        public Organization() { }

        public Organization(string name)
        {
            Name = name;
            Units = new HashSet<Unit>();
            Employees = new HashSet<Employee>();
        }      

        /// <summary>
        /// Collect employee details from the entire organization.
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllEmployeeDetails()
        {
            var result = new List<string>();
            foreach (var e in Employees)
            {
                result.Add(e.GetEmployeeDetails());
            }
            foreach (var u in Units)
            {
                result.AddRange(u.GetEmployeeDetails());
            }
            return result;
        }

        public void PrintAllEmployeeDetails()
        {
            foreach (var detail in GetAllEmployeeDetails())
            {
                Console.WriteLine(detail);
            }
        }

    }
}
