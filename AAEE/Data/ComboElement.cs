using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AAEE.Data
{
    public class ComboElement
    {
        #region Fields

        private int id = 0;
        private string name = "";

        #endregion

        #region Init

        public ComboElement(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        #endregion

        #region Properties

        /// <summary>Gets or sets the actor ID.</summary>
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>Gets or sets the actor name.</summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        #endregion

        #region Overrides

        public override string ToString()
        {
            return name;
        }

        #endregion
    }
}
