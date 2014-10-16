using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using AAEE.Data;
using AAEE.Utility;

namespace AAEE.DataPacks
{
    /// <summary>This class serves as datapack for skill configuration.</summary>
    [XmlType("DataPackSkill")]
    public class DataPackSkill : IResetable
    {
        #region Defaults

        // Default name
        private string defaultName = "New Family";

        #region Attack

        // Default attack
        private bool defaultCustomAtk = false;
        private int defaultAtkInitial = 0;

        // Default hit rate
        private bool defaultCustomHit = false;
        private decimal defaultHitInitial = 0.0m;

        // Default parameter attack force
        private bool defaultCustomParamForce = false;
        private decimal defaultStrForce = 1.0m;
        private decimal defaultDexForce = 0.0m;
        private decimal defaultAgiForce = 0.0m;
        private decimal defaultIntForce = 0.0m;

        // Default defense attack force
        private bool defaultCustomDefenseForce = false;
        private decimal defaultPDefForce = 1.0m;
        private decimal defaultMDefForce = 0.0m;

        #endregion

        #region Critical

        // Default critical rate
        private bool defaultCustomCritRate = false;
        private decimal defaultCritRate = 0.04m;

        // Default critical damage
        private bool defaultCustomCritDamage = false;
        private decimal defaultCritDamage = 2.0m;

        // Default critical guard rate reduction
        private bool defaultCustomCritDefGuard = false;
        private decimal defaultCritDefGuard = 0.0m;

        // Default critical evasion rate reduction
        private bool defaultCustomCritDefEva = false;
        private decimal defaultCritDefEva = 0.0m;

        #endregion

        #region Special Critical

        // Default special critical rate
        private bool defaultCustomSpCritRate = false;
        private decimal defaultSpCritRate = 0.05m;

        // Default special critical damage
        private bool defaultCustomSpCritDamage = false;
        private decimal defaultSpCritDamage = 3.0m;

        // Default special critical guard rate reduction
        private bool defaultCustomSpCritDefGuard = false;
        private decimal defaultSpCritDefGuard = 0.75m;

        // Default special critical evasion rate reduction
        private bool defaultCustomSpCritDefEva = false;
        private decimal defaultSpCritDefEva = 0.5m;

        #endregion

        #endregion

        #region Fields

        // Id and name
        private int id;
        private string name;

        #region In Family

        private List<Skill> skillFamily = new List<Skill>();

        #endregion

        #region Attack

        // Attack
        private bool customAtk;
        private int atkInitial;

        // Hit rate
        private bool customHit;
        private decimal hitInitial;

        // Parameter attack force
        private bool customParamForce;
        private decimal strForce;
        private decimal dexForce;
        private decimal agiForce;
        private decimal intForce;

        // Defense attack force
        private bool customDefenseForce;
        private decimal pdefForce;
        private decimal mdefForce;

        #endregion

        #region Critical

        // Critical rate
        private bool customCritRate;
        private decimal critRate;

        // Critical damage
        private bool customCritDamage;
        private decimal critDamage;

        // Critical guard rate reduction
        private bool customCritDefGuard;
        private decimal critDefGuard;

        // Critical evasion rate reduction
        private bool customCritDefEva;
        private decimal critDefEva;

        #endregion

        #region Special Critical

        // Special critical rate
        private bool customSpCritRate;
        private decimal spCritRate;

        // Special critical damage
        private bool customSpCritDamage;
        private decimal spCritDamage;

        // Special critical guard rate reduction
        private bool customSpCritDefGuard;
        private decimal spCritDefGuard;

        // Special critical evasion rate reduction
        private bool customSpCritDefEva;
        private decimal spCritDefEva;

        #endregion

        #endregion

        #region Properties

        /// <summary>Gets or sets the skill ID.</summary>
        [XmlAttribute()]
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>Gets or sets the skill name.</summary>
        [XmlAttribute()]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        #region In Family

        /// <summary>Gets or sets the list of skill in the family.</summary>
        public List<Skill> SkillFamily
        {
            get { return skillFamily; }
            set { skillFamily = new List<Skill>(value); }
        }

        #endregion

        #region Attack

        /// <summary>Gets or sets the enemy custom unarmed attack checkBox.</summary>
        public bool CustomAtk
        {
            get { return customAtk; }
            set { customAtk = value; }
        }
        /// <summary>Gets or sets the enemy unarmed attack Initial.</summary>
        public int AtkInitial
        {
            get { return atkInitial; }
            set { atkInitial = value; }
        }
        /// <summary>Gets or sets the enemy custom unarmed hit rate checkBox.</summary>
        public bool CustomHit
        {
            get { return customHit; }
            set { customHit = value; }
        }
        /// <summary>Gets or sets the enemy unarmed hit rate Initial.</summary>
        public decimal HitInitial
        {
            get { return hitInitial; }
            set { hitInitial = value; }
        }
        /// <summary>Gets or sets the enemy custom parameter attack force checkBox.</summary>
        public bool CustomParamForce
        {
            get { return customParamForce; }
            set { customParamForce = value; }
        }
        /// <summary>Gets or sets the enemy strengh attack force.</summary>
        public decimal StrForce
        {
            get { return strForce; }
            set { strForce = value; }
        }
        /// <summary>Gets or sets the enemy dexterity attack force.</summary>
        public decimal DexForce
        {
            get { return dexForce; }
            set { dexForce = value; }
        }
        /// <summary>Gets or sets the enemy agility attack force.</summary>
        public decimal AgiForce
        {
            get { return agiForce; }
            set { agiForce = value; }
        }
        /// <summary>Gets or sets the enemy intelligence attack force.</summary>
        public decimal IntForce
        {
            get { return intForce; }
            set { intForce = value; }
        }
        /// <summary>Gets or sets the enemy custom unarmored defense attack force checkBox.</summary>
        public bool CustomDefenseForce
        {
            get { return customDefenseForce; }
            set { customDefenseForce = value; }
        }
        /// <summary>Gets or sets the enemy unarmored physical defense attack force.</summary>
        public decimal PDefForce
        {
            get { return pdefForce; }
            set { pdefForce = value; }
        }
        /// <summary>Gets or sets the enemy unarmored magical defense attack force.</summary>
        public decimal MDefForce
        {
            get { return mdefForce; }
            set { mdefForce = value; }
        }

        #endregion

        #region Critical

        /// <summary>Gets or sets the enemy custom unarmed critical rate checkBox.</summary>
        public bool CustomCritRate
        {
            get { return customCritRate; }
            set { customCritRate = value; }
        }
        /// <summary>Gets or sets the enemy unarmed critical rate.</summary>
        public decimal CritRate
        {
            get { return critRate; }
            set { critRate = value; }
        }
        /// <summary>Gets or sets the enemy custom unarmed critical damage checkBox.</summary>
        public bool CustomCritDamage
        {
            get { return customCritDamage; }
            set { customCritDamage = value; }
        }
        /// <summary>Gets or sets the enemy unarmed critical damage.</summary>
        public decimal CritDamage
        {
            get { return critDamage; }
            set { critDamage = value; }
        }
        /// <summary>Gets or sets the enemy custom unarmed critical guard rate reduction checkBox.</summary>
        public bool CustomCritDefGuard
        {
            get { return customCritDefGuard; }
            set { customCritDefGuard = value; }
        }
        /// <summary>Gets or sets the enemy unarmed critical guard rate reduction.</summary>
        public decimal CritDefGuard
        {
            get { return critDefGuard; }
            set { critDefGuard = value; }
        }
        /// <summary>Gets or sets the enemy custom unarmed critical evasion rate reduction checkBox.</summary>
        public bool CustomCritDefEva
        {
            get { return customCritDefEva; }
            set { customCritDefEva = value; }
        }
        /// <summary>Gets or sets the enemy unarmed critical evasion rate reduction.</summary>
        public decimal CritDefEva
        {
            get { return critDefEva; }
            set { critDefEva = value; }
        }

        #endregion

        #region Special Critical

        /// <summary>Gets or sets the enemy custom unarmed critical rate checkBox.</summary>
        public bool CustomSpCritRate
        {
            get { return customSpCritRate; }
            set { customSpCritRate = value; }
        }
        /// <summary>Gets or sets the enemy unarmed critical rate.</summary>
        public decimal SpCritRate
        {
            get { return spCritRate; }
            set { spCritRate = value; }
        }
        /// <summary>Gets or sets the enemy custom unarmed special critical damage checkBox.</summary>
        public bool CustomSpCritDamage
        {
            get { return customSpCritDamage; }
            set { customSpCritDamage = value; }
        }
        /// <summary>Gets or sets the enemy unarmed special critical damage.</summary>
        public decimal SpCritDamage
        {
            get { return spCritDamage; }
            set { spCritDamage = value; }
        }
        /// <summary>Gets or sets the enemy custom unarmed special critical guard rate reduction checkBox.</summary>
        public bool CustomSpCritDefGuard
        {
            get { return customSpCritDefGuard; }
            set { customSpCritDefGuard = value; }
        }
        /// <summary>Gets or sets the enemy unarmed special critical guard rate reduction.</summary>
        public decimal SpCritDefGuard
        {
            get { return spCritDefGuard; }
            set { spCritDefGuard = value; }
        }
        /// <summary>Gets or sets the enemy custom unarmed special critical evasion rate reduction checkBox.</summary>
        public bool CustomSpCritDefEva
        {
            get { return customSpCritDefEva; }
            set { customSpCritDefEva = value; }
        }
        /// <summary>Gets or sets the enemy unarmed special critical evasion rate reduction.</summary>
        public decimal SpCritDefEva
        {
            get { return spCritDefEva; }
            set { spCritDefEva = value; }
        }

        #endregion

        #endregion

        #region Behavior

        public DataPackSkill()
        {
            this.id = 0;
            this.name = "";
            this.Reset();
        }

        public DataPackSkill(int id)
        {
            this.id = id;
            this.name = defaultName;
            this.Reset();
        }

        public DataPackSkill(int inpuntID, string inputName)
        {
            this.id = inpuntID;
            this.name = inputName;
            this.Reset();
        }

        /// <summary>Resets the configuration to default.</summary>
        public void Reset()
        {
            #region In Family

            this.skillFamily.Clear();

            #endregion

            #region Attack

            // Attack
            this.customAtk = defaultCustomAtk;
            this.atkInitial = defaultAtkInitial;

            // Hit Rate
            this.customHit = defaultCustomHit;
            this.hitInitial = defaultHitInitial;

            // Parameter attack force
            this.customParamForce = defaultCustomParamForce;
            this.strForce = defaultStrForce;
            this.dexForce = defaultDexForce;
            this.agiForce = defaultAgiForce;
            this.intForce = defaultIntForce;

            // Defense attack force
            this.customDefenseForce = defaultCustomDefenseForce;
            this.pdefForce = defaultPDefForce;
            this.mdefForce = defaultMDefForce;

            #endregion

            #region Critical

            // Critical rate
            this.customCritRate = defaultCustomCritRate;
            this.critRate = defaultCritRate;

            // Critical damage
            this.customCritDamage = defaultCustomCritDamage;
            this.critDamage = defaultCritDamage;

            // Critical guard reduction
            this.customCritDefGuard = defaultCustomCritDefGuard;
            this.critDefGuard = defaultCritDefGuard;

            // Critical evasion reduction
            this.customCritDefEva = defaultCustomCritDefEva;
            this.critDefEva = defaultCritDefEva;

            #endregion

            #region Special Critical

            // Special critical rate
            this.customSpCritRate = defaultCustomSpCritRate;
            this.spCritRate = defaultSpCritRate;

            // Special critical damage
            this.customSpCritDamage = defaultCustomSpCritDamage;
            this.spCritDamage = defaultSpCritDamage;

            // Special critical guard reduction
            this.customSpCritDefGuard = defaultCustomSpCritDefGuard;
            this.spCritDefGuard = defaultSpCritDefGuard;

            // Special critical evasion reduction
            this.customSpCritDefEva = defaultCustomSpCritDefEva;
            this.spCritDefEva = defaultSpCritDefEva;

            #endregion
        }

        /// <summary>Compares configurations</summary>
        /// <param name="other">Other configuration of comparison</param>
        /// <returns>True or false.</returns>
        public bool Equals(DataPackSkill other)
        {
            if (!this.Equals(other)) return false;

            // ID and name
            if (this.ID != other.ID) return false;
            if (this.Name != other.Name) return false;

            #region In family

            if (!this.SkillFamily.Equals(other.SkillFamily)) return false;

            #endregion

            #region Attack

            // Attack
            if (this.CustomAtk != other.CustomAtk) return false;
            if (this.AtkInitial != other.AtkInitial) return false;

            // Hit rate
            if (this.CustomHit != other.CustomHit) return false;
            if (this.HitInitial != other.HitInitial) return false;

            // Parameter attack force
            if (this.CustomParamForce != other.CustomParamForce) return false;
            if (this.StrForce != other.StrForce) return false;
            if (this.DexForce != other.DexForce) return false;
            if (this.AgiForce != other.AgiForce) return false;
            if (this.IntForce != other.IntForce) return false;

            // Defense attack force
            if (this.CustomDefenseForce != other.CustomDefenseForce) return false;
            if (this.PDefForce != other.PDefForce) return false;
            if (this.MDefForce != other.MDefForce) return false;

            #endregion

            #region Critical

            // Critical Rate
            if (this.CustomCritRate != other.CustomCritRate) return false;
            if (this.CritRate != other.CritRate) return false;

            // Critical Damage
            if (this.CustomCritDamage != other.CustomCritDamage) return false;
            if (this.CritDamage != other.CritDamage) return false;

            // Critical Guard Reduction
            if (this.CustomCritDefGuard != other.CustomCritDefGuard) return false;
            if (this.CritDefGuard != other.CritDefGuard) return false;

            // Critical Evasion Reduction
            if (this.CustomCritDefEva != other.CustomCritDefEva) return false;
            if (this.CritDefEva != other.CritDefEva) return false;

            #endregion

            #region Special Critical

            // Special critical rate
            if (this.CustomSpCritRate != other.CustomCritRate) return false;
            if (this.SpCritRate != other.SpCritRate) return false;

            // Special critical damage
            if (this.CustomSpCritDamage != other.CustomSpCritDamage) return false;
            if (this.SpCritDamage != other.SpCritDamage) return false;

            // Special critical guard reduction
            if (this.CustomSpCritDefGuard != other.CustomSpCritDefGuard) return false;
            if (this.SpCritDefGuard != other.SpCritDefGuard) return false;

            // Special critical evasion reduction
            if (this.CustomSpCritDefEva != other.CustomSpCritDefEva) return false;
            if (this.SpCritDefEva != other.SpCritDefEva) return false;

            #endregion

            return true;
        }

        #endregion

        #region Clone

        /// <summary>Copy configurations</summary>
        /// <param name="other">Other configuration to copy</param>
        public virtual void CloneFrom(DataPackSkill other)
        {
            // ID and Name
            this.ID = other.ID;
            this.Name = other.Name;
            
            #region In family

            this.SkillFamily.Clear();
            if (other.SkillFamily.Count > 0)
            {
                foreach (Skill skill in other.SkillFamily)
                {
                    this.SkillFamily.Add(new Skill(skill.ID, skill.Name));
                }
            }

            #endregion

            #region Attack

            // Attack
            this.CustomAtk = other.CustomAtk;
            this.AtkInitial = other.AtkInitial;

            // Hit Rate
            this.CustomHit = other.CustomHit;
            this.HitInitial = other.HitInitial;

            // Parameter force
            this.CustomParamForce = other.CustomParamForce;
            this.StrForce = other.StrForce;
            this.DexForce = other.DexForce;
            this.AgiForce = other.AgiForce;
            this.IntForce = other.IntForce;

            // Defense force
            this.CustomDefenseForce = other.CustomDefenseForce;
            this.PDefForce = other.PDefForce;
            this.MDefForce = other.MDefForce;

            #endregion

            #region Critical

            // Critical rate
            this.CustomCritRate = other.CustomCritRate;
            this.CritRate = other.CritRate;

            // Critical damage
            this.CustomCritDamage = other.CustomCritDamage;
            this.CritDamage = other.CritDamage;

            // Critical guard reduction
            this.CustomCritDefGuard = other.CustomCritDefGuard;
            this.CritDefGuard = other.CritDefGuard;

            // Critical evasion reduction
            this.CustomCritDefEva = other.CustomCritDefEva;
            this.CritDefEva = other.CritDefEva;

            #endregion

            #region Special Critical

            // Special critical rate
            this.CustomSpCritRate = other.CustomSpCritRate;
            this.SpCritRate = other.SpCritRate;

            // Special critical damage
            this.CustomSpCritDamage = other.CustomSpCritDamage;
            this.SpCritDamage = other.SpCritDamage;

            // Special critical guard reduction
            this.CustomSpCritDefGuard = other.CustomSpCritDefGuard;
            this.SpCritDefGuard = other.SpCritDefGuard;

            // Special critical evasion reduction
            this.CustomSpCritDefEva = other.CustomSpCritDefEva;
            this.SpCritDefEva = other.SpCritDefEva;

            #endregion
        }

        /// <summary>Copy configurations from clipboard</summary>
        /// <param name="other">Other configuration to copy</param>
        public virtual void PasteFrom(DataPackSkill other)
        {
            #region Attack

            // Attack
            this.CustomAtk = other.CustomAtk;
            this.AtkInitial = other.AtkInitial;

            // Hit Rate
            this.CustomHit = other.CustomHit;
            this.HitInitial = other.HitInitial;

            // Parameter force
            this.CustomParamForce = other.CustomParamForce;
            this.StrForce = other.StrForce;
            this.DexForce = other.DexForce;
            this.AgiForce = other.AgiForce;
            this.IntForce = other.IntForce;

            // Defense force
            this.CustomDefenseForce = other.CustomDefenseForce;
            this.PDefForce = other.PDefForce;
            this.MDefForce = other.MDefForce;

            #endregion

            #region Critical

            // Critical rate
            this.CustomCritRate = other.CustomCritRate;
            this.CritRate = other.CritRate;

            // Critical damage
            this.CustomCritDamage = other.CustomCritDamage;
            this.CritDamage = other.CritDamage;

            // Critical guard reduction
            this.CustomCritDefGuard = other.CustomCritDefGuard;
            this.CritDefGuard = other.CritDefGuard;

            // Critical evasion reduction
            this.CustomCritDefEva = other.CustomCritDefEva;
            this.CritDefEva = other.CritDefEva;

            #endregion

            #region Special Critical

            // Special critical rate
            this.CustomSpCritRate = other.CustomSpCritRate;
            this.SpCritRate = other.SpCritRate;

            // Special critical damage
            this.CustomSpCritDamage = other.CustomSpCritDamage;
            this.SpCritDamage = other.SpCritDamage;

            // Special critical guard reduction
            this.CustomSpCritDefGuard = other.CustomSpCritDefGuard;
            this.SpCritDefGuard = other.SpCritDefGuard;

            // Special critical evasion reduction
            this.CustomSpCritDefEva = other.CustomSpCritDefEva;
            this.SpCritDefEva = other.SpCritDefEva;

            #endregion
        }

        #endregion

        #region Overrides

        public override string ToString()
        {
            return this.id.ToString("000") + ": " + this.name.ToString();
        }

        #endregion
    }
}
