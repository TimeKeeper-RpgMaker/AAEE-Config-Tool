using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AAEE.Data
{
    [XmlType("EquipType")]
    public class EquipType
    {
        #region Default

        private string defaultName = "New Equip Type";

        #endregion

        #region Fields

        private int id = 0;
        private string name = "";

        #endregion

        #region Init

        public EquipType()
        {
            this.id = 0;
            this.name = "";
        }

        public EquipType(int id)
        {
            this.id = id;
            this.name = defaultName;
        }

        public EquipType(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        #endregion

        #region Properties

        /// <summary>Gets or sets the equip type ID.</summary>
        [XmlAttribute()]
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>Gets or sets the equip type name.</summary>
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
            return string.Format("{0:000}: {1}", id, name);
        }

        #endregion
    }
}
