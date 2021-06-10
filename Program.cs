using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace PracticalTest
{
    class Program
    {
        private static Organization DeserializeXmlToOrganization(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Organization));
            FileStream fs = new FileStream(filename, FileMode.Open);
            using (var reader = XmlReader.Create(fs))
            {
                var organization = (Organization)serializer.Deserialize(reader);
                return organization;
            }
        }

        private static void SerializeOrganizationToJson(Organization o, string dest)
        {
            OrganizationWrapper wrapper = new OrganizationWrapper(o);
            var json = JsonConvert.SerializeObject(wrapper, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(dest + "/organization.json", json);
        }

        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Please pass the location of organization.xml and the JSON output path.");
                return;
            }

            var sourcePath = args[0]; //"../../../resources/organization.xml"
            var destPath = args[1];

            // Deserialize the organization file into an object
            Organization o = DeserializeXmlToOrganization(sourcePath);

            // Print the current Employee details of the organization
            o.PrintAllEmployeeDetails();

            // Swap employees of the Platform and Maintenance teams
            // Alternatively, this could have been done by swapping the unit name instead of the employees.
            // If there are additional attributes/details per Unit then the below would not work correctly, 
            // so this method won't be used:
            /*
                o.GetFirstUnit("Platform Team", true).Name = "tempName";
                o.GetFirstUnit("Maintenance Team", true).Name = "Platform Team";
                o.GetFirstUnit("tempName", true).Name = "Maintenance Team";
            */

            // Instead we will swap the employees:
            Unit platform = o.GetFirstUnit("Platform Team", true);
            Unit maintenance = o.GetFirstUnit("Maintenance Team", true);

            HashSet<Employee> temp = platform.Employees;
            platform.Employees = maintenance.Employees;
            maintenance.Employees = temp;

            // Output the new structure into a JSON file
            SerializeOrganizationToJson(o, destPath);

        }
    }
}
