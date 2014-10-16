using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AAEE.Utility
{
    /// <summary>Holds the application version.</summary>
    public class Version
    {
        #region Fields

        private int[] versions = new int[] { 2, 0, 0, 2, 0, 0, 2, 0, 0 };
        private bool betaFlag = true;

        #endregion

        #region Properties

        /// <summary>Gets and sets if this configuration tool in beta.</summary>
        public bool BetaFlag
        {
            get { return betaFlag; }
            set { betaFlag = value; }
        }

        #region Script

        /// <summary>Gets the script main version.</summary>
        public int MainScript
        {
            get { return versions[0]; }
        }
        /// <summary>Gets the script minor version.</summary>
        public int MinorScript
        {
            get { return versions[1]; }
        }
        /// <summary>Gets the script revision version.</summary>
        public int RevisionScript
        {
            get { return versions[2]; }
        }
        /// <summary>Gets the script version.</summary>
        public string Script
        {
            get { return (versions[0].ToString() + "." + versions[1].ToString() + "." + versions[2].ToString()); }
        }

        #endregion

        #region Configuration

        /// <summary>Gets this configuration main version.</summary>
        public int MainCfg
        {
            get { return versions[3]; }
        }
        /// <summary>Gets this configuration minor version.</summary>
        public int MinorCfg
        {
            get { return versions[4]; }
        }
        /// <summary>Gets this configuration revision version.</summary>
        public int RevisionCfg
        {
            get { return versions[5]; }
        }
        /// <summary>Gets the configuration version.</summary>
        public string Cfg
        {
            get { return (versions[3].ToString() + "." + versions[4].ToString() + "." + versions[5].ToString()); }
        }
        
        #endregion

        #region Application

        /// <summary>Gets this configuration main version.</summary>
        public int MainApp
        {
            get { return versions[6]; }
        }
        /// <summary>Gets this configuration minor version.</summary>
        public int MinorApp
        {
            get { return versions[7]; }
        }
        /// <summary>Gets this configuration revision version.</summary>
        public int RevisionApp
        {
            get { return versions[8]; }
        }
        /// <summary>Gets the application version.</summary>
        public string App
        {
            get { return (versions[6].ToString() + "." + versions[7].ToString() + "." + versions[8].ToString()); }
        }

        #endregion

        #endregion
    }
}

