using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace WikiApplication
{
    [Serializable]
    class Information : IComparable<Information>, IComparer<Information>
    {
        // variables
        #region
        private string Name;
        private string Category;
        private string Structure;
        private string Definition;
        #endregion

        // Setters and Getters
        #region
        public string name
        {
            get { return Name; }
            set { Name = value; }
        }

        public string category
        {
            get { return Category; }
            set { Category = value; }
        }

        public string structure
        {
            get { return Structure; }
            set { Structure = value; }
        }

        public string definition
        {
            get { return Definition; }
            set { Definition = value; }
        }
        public bool isLinear { get; set; }
        public int rdoSelectedIndex { get; set; }
        public string rdoSelectedType { get; set; }
        #endregion

        // Constructor 
        #region
        public Information(string name, string category, string structure, string definition)
        {
            this.Name = name;
            this.Category = category;
            this.Structure = structure;
            this.Definition = definition;
        }

        public Information()
        {
        }
        #endregion

        // tostring override
        #region
        public override string ToString()
        {
            return Name + " ---> " + Category;
        }
        #endregion

        // Icompare sort by name
        #region
        public int CompareTo(Information other)
        {
            return Name.CompareTo(other.Name);
        }

        public int Compare(Information x, Information y)
        {
            return x.Name.ToLower().CompareTo(y.Name.ToLower());
        }
        #endregion

    }
}