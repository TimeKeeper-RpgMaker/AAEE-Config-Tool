using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using AAEE.Data;
using AAEE.Utility;

namespace AAEE.Configurations
{
    /// <summary>Represents the General Configuration.</summary>
    [XmlType("ConfigurationGeneral")]
    public class ConfigurationGeneral : IResetable
    {
        #region Defaults

        #region Stat Limit Bypass

        private bool defaultSetLimitBypass = true;
        private int defaultSetHPSPMaxLimit = 999999;
        private int defaultSetStatMaxLimit = 999;

        #endregion

        #region Attack Skill Rate Type

        private int defaultSkillParamRateType = 0;
        private int defaultSkillDefenseRateType = 0;

        #endregion

        #region Actor class behavior

        private int defaultOrderEquipmentList = 0;
        private int defaultOrderEquipmentMultiplier = 0;
        private int defaultOrderEquipmentFlags = 0;
        private int defaultOrderHandReduce = 0;
        private int defaultOrderUnarmedAttackForce = 0;

        #endregion

        #region Cursed

        // Cursed color
        private byte defaultCursedColorRed = 255;
        private byte defaultCursedColorGreen = 0;
        private byte defaultCursedColorBlue = 255;
        private byte defaultCursedColorAlpha = 255;

        // Cursed equipment Setting
        private bool defaultSetShowCursed = true;
        private bool defaultSetBlockCursed = true;

        #endregion

        #endregion

        #region Fields

        #region Stat Limit Bypass

        private bool setLimitBypass;
        private int setHPSPMaxLimit;
        private int setStatMaxLimit;

        #endregion

        #region Attack Skill Rate Type

        private int skillParamRateType;
        private int skillDefenseRateType;

        #endregion

        #region Actor class behavior

        private int orderEquipmentList;
        private int orderEquipmentMultiplier;
        private int orderEquipmentFlags;
        private int orderHandReduce;
        private int orderUnarmedAttackForce;

        #endregion

        #region Cursed

        // Cursed color
        private byte cursedColorRed;
        private byte cursedColorGreen;
        private byte cursedColorBlue;
        private byte cursedColorAlpha;

        // Cursed equipment Setting
        private bool setShowCursed;
        private bool setBlockCursed;

        #endregion

        #endregion

        #region Properties

        #region Stat Limit Bypass

        /// <summary>Gets or sets the general stat limit bypass setting.</summary>
        public bool SetLimitBypass
        {
            get { return setLimitBypass; }
            set { setLimitBypass = value; }
        }
        /// <summary>Gets or sets the general HP and SP maximum limit setting.</summary>
        public int SetHPSPMaxLimit
        {
            get { return setHPSPMaxLimit; }
            set { setHPSPMaxLimit = value; }
        }
        /// <summary>Gets or sets the general stat maximum limit setting.</summary>
        public int SetStatMaxLimit
        {
            get { return setStatMaxLimit; }
            set { setStatMaxLimit = value; }
        }

        #endregion

        #region Attack Skill Rate Type

        /// <summary>Gets or sets the general skill parameter rate type setting.</summary>
        public int SkillParamRateType
        {
            get { return skillParamRateType; }
            set { skillParamRateType = value; }
        }
        /// <summary>Gets or sets the general skill defense rate type setting.</summary>
        public int SkillDefenseRateType
        {
            get { return skillDefenseRateType; }
            set { skillDefenseRateType = value; }
        }

        #endregion

        #region Actor class behavior

        /// <summary>Gets or sets the behavior of the actor and class equipment list.</summary>
        public int OrderEquipmentList
        {
            get { return orderEquipmentList; }
            set { orderEquipmentList = value; }
        }
        /// <summary>Gets or sets the behavior of the equipment multiplier.</summary>
        public int OrderEquipmentMultiplier
        {
            get { return orderEquipmentMultiplier; }
            set { orderEquipmentMultiplier = value; }
        }
        /// <summary>Gets or sets the behavior of the equipment flags.</summary>
        public int OrderEquipmentFlags
        {
            get { return orderEquipmentFlags; }
            set { orderEquipmentFlags = value; }
        }
        /// <summary>Gets or sets the multi-hand reduction.</summary>
        public int OrderHandReduce
        {
            get { return orderHandReduce; }
            set { orderHandReduce = value; }
        }
        /// <summary>Gets or sets the unarmed attack parameter.</summary>
        public int OrderUnarmedAttackForce
        {
            get { return orderUnarmedAttackForce; }
            set { orderUnarmedAttackForce = value; }
        }

        #endregion

        #region Cursed

        /// <summary>Gets or sets the general cursed color red setting.</summary>
        public byte CursedColorRed
        {
            get { return cursedColorRed; }
            set { cursedColorRed = value; }
        }
        /// <summary>Gets or sets the general cursed color green setting.</summary>
        public byte CursedColorGreen
        {
            get { return cursedColorGreen; }
            set { cursedColorGreen = value; }
        }
        /// <summary>Gets or sets the general cursed color blue setting.</summary>
        public byte CursedColorBlue
        {
            get { return cursedColorBlue; }
            set { cursedColorBlue = value; }
        }
        /// <summary>Gets or sets the general cursed color alpha setting.</summary>
        public byte CursedColorAlpha
        {
            get { return cursedColorAlpha; }
            set { cursedColorAlpha = value; }
        }
        /// <summary>Gets or sets the general show cursed equipment setting.</summary>
        public bool SetShowCursed
        {
            get { return setShowCursed; }
            set { setShowCursed = value; }
        }
        /// <summary>Gets or sets the general block cursed equipment from been equiped setting.</summary>
        public bool SetBlockCursed
        {
            get { return setBlockCursed; }
            set { setBlockCursed = value; }
        }

        #endregion

        #endregion

        #region Setup

        public ConfigurationGeneral()
        {
            Reset();
        }

        /// <summary>Resets the configuration to default.</summary>
        public void Reset()
        {
            #region Stat Limit Bypass

            this.setLimitBypass = defaultSetLimitBypass;
            ResetStatMaximum();

            #endregion

            #region Attack Skill Rate Type

            this.skillParamRateType = defaultSkillParamRateType;
            this.skillDefenseRateType = defaultSkillDefenseRateType;

            #endregion
            
            #region Actor Class behavior

            this.orderEquipmentList = defaultOrderEquipmentList;
            this.orderEquipmentMultiplier = defaultOrderEquipmentMultiplier;
            this.orderEquipmentFlags = defaultOrderEquipmentFlags;
            this.orderHandReduce = defaultOrderHandReduce;
            this.orderUnarmedAttackForce = defaultOrderUnarmedAttackForce;

            #endregion

            #region Cursed

            // Cursed color
            this.cursedColorRed = defaultCursedColorRed;
            this.cursedColorGreen = defaultCursedColorGreen;
            this.cursedColorBlue = defaultCursedColorBlue;
            this.cursedColorAlpha = defaultCursedColorAlpha;

            // Cursed equipment Setting
            this.setShowCursed = defaultSetShowCursed;
            this.setBlockCursed = defaultSetBlockCursed;

            #endregion
        }

        public void ResetStatMaximum()
        {
            this.setHPSPMaxLimit = defaultSetHPSPMaxLimit;
            this.setStatMaxLimit = defaultSetStatMaxLimit;
        }

        #endregion

        #region Behavior

        /// <summary>Compares two configurations.</summary>
        /// <param name="other">The other configuration.</param>
        /// <returns>True or false.</returns>
        public bool Equals(ConfigurationGeneral other)
        {
            #region Stat Limit Bypass

            if (this.SetLimitBypass != other.SetLimitBypass) return false;
            if (this.SetHPSPMaxLimit != other.SetHPSPMaxLimit) return false;
            if (this.SetStatMaxLimit != other.SetStatMaxLimit) return false;

            #endregion

            #region Attack Skill Rate Type

            if (this.SkillParamRateType != other.SkillParamRateType) return false;
            if (this.SkillDefenseRateType != other.SkillDefenseRateType) return false;

            #endregion

            #region Actor Class behavior

            if (this.OrderEquipmentList != other.OrderEquipmentList) return false;
            if (this.OrderEquipmentMultiplier != other.OrderEquipmentMultiplier) return false;
            if (this.OrderEquipmentFlags != other.OrderEquipmentFlags) return false;
            if (this.OrderHandReduce != other.OrderHandReduce) return false;
            if (this.OrderUnarmedAttackForce != other.OrderUnarmedAttackForce) return false;

            #endregion

            #region Cursed

            // Cursed color
            if (this.CursedColorRed != other.CursedColorRed) return false;
            if (this.CursedColorGreen != other.CursedColorGreen) return false;
            if (this.CursedColorBlue != other.CursedColorBlue) return false;
            if (this.CursedColorAlpha != other.CursedColorAlpha) return false;

            // Cursed equipment Setting
            if (this.SetShowCursed != other.SetShowCursed) return false;
            if (this.SetBlockCursed != other.SetBlockCursed) return false;

            #endregion

            return true;
        }

        #endregion

        #region Clone

        /// <summary>Copy configurations</summary>
        /// <param name="other">Other configuration to copy</param>
        public virtual void CloneFrom(ConfigurationGeneral other)
        {
            #region Stat Limit Bypass

            this.SetLimitBypass = other.SetLimitBypass;
            this.SetHPSPMaxLimit = other.SetHPSPMaxLimit;
            this.SetStatMaxLimit = other.SetStatMaxLimit;

            #endregion

            #region Attack Skill Rate Type

            this.SkillParamRateType = other.SkillParamRateType;
            this.SkillDefenseRateType = other.SkillDefenseRateType;

            #endregion

            #region Actor Class behavior

            this.OrderEquipmentList = other.OrderEquipmentList;
            this.OrderEquipmentMultiplier = other.OrderEquipmentMultiplier;
            this.OrderEquipmentFlags = other.OrderEquipmentFlags;
            this.OrderHandReduce = other.OrderHandReduce;
            this.OrderUnarmedAttackForce = other.OrderUnarmedAttackForce;

            #endregion

            #region Cursed

            // Cursed color
            this.CursedColorRed = other.CursedColorRed;
            this.CursedColorGreen = other.CursedColorGreen;
            this.CursedColorBlue = other.CursedColorBlue;
            this.CursedColorAlpha = other.CursedColorAlpha;

            // Cursed equipment Setting
            this.SetShowCursed = other.SetShowCursed;
            this.SetBlockCursed = other.SetBlockCursed;

            #endregion
        }

        #endregion
    }
}