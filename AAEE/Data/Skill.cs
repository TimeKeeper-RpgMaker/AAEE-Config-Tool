using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AAEE.Data
{
    [XmlType("Skill")]
    public class Skill
    {
        #region Fields

        private int id = 0;
        private string name = "";

        #endregion

        #region Init

        public Skill()
        {
            this.id = 0;
            this.name = "";
        }

        public Skill(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        #endregion

        #region Properties

        /// <summary>Gets or sets the actor ID.</summary>
        [XmlAttribute()]
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>Gets or sets the actor name.</summary>
        [XmlAttribute()]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        #endregion

        #region Overrides

        public override string ToString()
        {
            return (id == 0 ? "None" : string.Format("{0:000}: {1}", id, name));
        }

        #endregion
    }
}