using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AAEE.Utility
{

    /// <summary>Serves as data holder for the data connecting the AAEE script, RMXP and this application.</summary>
    public static class AAEEData
    {
        #region Fields

        #region RMXP

        private static Version version = new Version();
        private static string[] fileNames = new string[] { "Actors", "Classes", "Skills", "Items", "Weapons", "Armors", "Enemies", "States", "Animations" };

        #endregion

        #region Default

        // Equipment
        private static string[] equipTypeName = new string[] { "Weapon", "Shield", "Helmet", "Body Armor", "Accessory" };
        private static int[] equipListID = new int[] { 0, 1, 2, 3, 4 };

        #endregion

        #endregion

        #region Properties

        #region RMXP

        /// <summary>Gets the version.</summary>
        public static Version Version
        {
            get { return version; }
        }
        /// <summary>Gets the RMXP Data folder file names that are used.</summary>
        public static string[] FileNames
        {
            get { return fileNames; }
        }

        #endregion

        #region Default

        /// <summary>Gets the default equip type name.</summary>
        public static string[] EquipTypeName
        {
            get { return equipTypeName; }
        }
        /// <summary>Gets the default equip list ID.</summary>
        public static int[] EquipListID
        {
            get { return equipListID; }
        }

        #endregion

        #endregion
    }
}
