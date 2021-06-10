using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace PracticalTest
{
    /// <summary>
    /// Contains Xml elements that can have a Name, Units, and Employees.
    /// </summary>
    public abstract class Container
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlArray("Units")]
        [XmlArrayItem("Unit", IsNullable = false)]
        public HashSet<Unit> Units { get; set; }

        [XmlElement("Employee", IsNullable = false)]
        public HashSet<Employee> Employees { get; set; }

       
        /// <summary>
        /// Returns the first Unit within this Container object given the unitName. Optional: recursively check for the unit.
        /// </summary>
        /// <param name="unitName"></param>
        /// <param name="recurse"></param>
        /// <returns></returns>
        public Unit GetFirstUnit(string unitName, bool recurse = false)
        {
            foreach (var u in Units)
            {
                if (u.Name.Equals(unitName))
                {
                    return u;
                }
            }

            if (recurse)
            {
                foreach (var u in Units)
                {
                    var result = u.GetFirstUnit(unitName, true);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }

            return null;
        }
    }
}
