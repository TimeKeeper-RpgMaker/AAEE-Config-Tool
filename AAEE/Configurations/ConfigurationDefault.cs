using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using AAEE.Data;
using AAEE.Utility;

namespace AAEE.Configurations
{
    /// <summary>Represents the Default Configuration.</summary>
    [XmlType("ConfigurationDefault")]
    public class ConfigurationDefault : IResetable
    {
        #region Defaults

        #region Equipment

        // Default dual hold
        private bool defaultDualHold = false;

        // Default dual hold name
        private string defaultDualHoldNameWeapon = "Main Hand";
        private string defaultDualHoldNameShield = "Off Hand";

        // Default dual hold multiplier
        private decimal defaultDualHoldMulWeapon = 1.0m;
        private decimal defaultDualHoldMulShield = 0.5m;

        // Default shield bypass
        private bool defaultShieldBypass = false;
        private decimal defaultShieldBypassMul = 0.1m;

        // Default weapon Bypass
        private bool defaultWeaponBypass = false;
        private decimal defaultWeaponBypassMul = 0.1m;

        // Default reduce hand
        private int defaultReduceHand = 0;
        private decimal defaultReduceHandMul = 0.1m;

        #endregion

        #region Parameter

        // Default maximum HP
        private int defaultMaxHPInitial = 1;
        private int defaultMaxHPFinal = 1;

        // Default maximum SP
        private int defaultMaxSPInitial = 0;
        private int defaultMaxSPFinal = 0;

        // Default strengh
        private int defaultStrInitial = 1;
        private int defaultStrFinal = 1;

        // Default dexterity
        private int defaultDexInitial = 1;
        private int defaultDexFinal = 1;

        // Default agility
        private int defaultAgiInitial = 1;
        private int defaultAgiFinal = 1;

        // Default intelligence
        private int defaultIntInitial = 1;
        private int defaultIntFinal = 1;

        #endregion

        #region Parameter attack rate

        // Default strengh attack rate
        private decimal defaultStrRate = 0.05m;

        // Default dexterity attack rate
        private decimal defaultDexRate = 0.05m;

        // Default agility attack rate
        private decimal defaultAgiRate = 0.05m;

        // Default intelligence attack rate
        private decimal defaultIntRate = 0.05m;

        // Default physical defense rate
        private decimal defaultPDefRate = 0.5m;

        // Default magical defense rate
        private decimal defaultMDefRate = 0.5m;

        // Default guard rate
        private decimal defaultGuardRate = 0.5m;

        // Default evasion rate
        private decimal defaultEvaRate = 0.08m;

        #endregion

        #region Defense against Attack Critical

        // Default defense against attack critical rate
        private decimal defaultDefCritRate = 1.0m;

        // Default defense against attack critical damage
        private decimal defaultDefCritDamage = 1.0m;

        // Default defense against attack special critical rate
        private decimal defaultDefSpCritRate = 1.0m;

        // Default defense against attack special critical damage
        private decimal defaultDefSpCritDamage = 1.0m;

        #endregion

        #region Defense against Skill Critical

        // Default defense against skill critical rate
        private decimal defaultDefSkillCritRate = 1.0m;

        // Default defense against skill critical damage
        private decimal defaultDefSkillCritDamage = 1.0m;

        // Default defense against skill special critical rate
        private decimal defaultDefSkillSpCritRate = 1.0m;

        // Default defense against skill special critical damage
        private decimal defaultDefSkillSpCritDamage = 1.0m;

        #endregion

        #region Attack

        // Default attack
        private int defaultAtkInitial = 0;
        private int defaultAtkFinal = 0;

        // Default hit rate
        private decimal defaultHitInitial = 0.0m;
        private decimal defaultHitFinal = 0.0m;

        // Default attack animation
        private int defaultAnimCaster = 0;
        private int defaultAnimTarget = 0;

        // Default parameter attack force
        private decimal defaultStrForce = 1.0m;
        private decimal defaultDexForce = 0.0m;
        private decimal defaultAgiForce = 0.0m;
        private decimal defaultIntForce = 0.0m;

        // Default defense attack force
        private decimal defaultPDefForce = 1.0m;
        private decimal defaultMDefForce = 0.0m;

        #endregion

        #region Critical

        // Default critical rate
        private decimal defaultCritRate = 0.04m;

        // Default critical damage
        private decimal defaultCritDamage = 2.0m;

        // Default critical guard rate reduction
        private decimal defaultCritDefGuard = 1.0m;

        // Default critical evasion rate reduction
        private decimal defaultCritDefEva = 1.0m;

        #endregion

        #region Special Critical

        // Default special critical rate
        private decimal defaultSpCritRate = 0.05m;

        // Default special critical damage
        private decimal defaultSpCritDamage = 3.0m;

        // Default special critical guard rate reduction
        private decimal defaultSpCritDefGuard = 0.75m;

        // Default special critical evasion rate reduction
        private decimal defaultSpCritDefEva = 0.5m;

        #endregion

        #region Defense

        // Default physical defense
        private int defaultPDefInitial = 0;
        private int defaultPDefFinal = 0;

        // Default magical defense
        private int defaultMDefInitial = 0;
        private int defaultMDefFinal = 0;

        #endregion

        #endregion

        #region Fields

        #region Equipment

        // Equipment List
        private List<EquipType> equipType = new List<EquipType>();
        private List<EquipType> equipList = new List<EquipType>();

        // Dual hold
        private bool dualHold;

        // Dual hold name
        private string dualHoldNameWeapon;
        private string dualHoldNameShield;

        // Dual hold multiplier
        private decimal dualHoldMulWeapon;
        private decimal dualHoldMulShield;

        // Shield bypass
        private bool shieldBypass;
        private decimal shieldBypassMul;

        // Weapon bypass
        private bool weaponBypass;
        private decimal weaponBypassMul;

        // Reduce hand
        private int reduceHand;
        private decimal reduceHandMul;

        #endregion

        #region Parameter

        // Maximum HP
        private int maxHPInitial;
        private int maxHPFinal;

        // Maximum SP
        private int maxSPInitial;
        private int maxSPFinal;

        // Strengh
        private int strInitial;
        private int strFinal;

        // Dexterity
        private int dexInitial;
        private int dexFinal;

        // Agility
        private int agiInitial;
        private int agiFinal;

        // Intelligence
        private int intInitial;
        private int intFinal;

        #endregion

        #region Parameter Rate

        // Strengh attack rate
        private decimal strRate;

        // Dexterity attack rate
        private decimal dexRate;

        // Agility attack rate
        private decimal agiRate;

        // Intelligence attack rate
        private decimal intRate;

        // Physical defense attack rate
        private decimal pdefRate;

        // Magical defense attack rate
        private decimal mdefRate;

        // Guard rate
        private decimal guardRate;

        // Evasion rate
        private decimal evaRate;

        #endregion

        #region Defense against Attack Critical

        // Default defense against attack critical rate
        private decimal defCritRate;

        // Default defense against attack critical damage
        private decimal defCritDamage;

        // Default defense against attack special critical rate
        private decimal defSpCritRate;

        // Default defense against attack special critical damage
        private decimal defSpCritDamage;

        #endregion

        #region Defense against Skill Critical

        // Default defense against skill critical rate
        private decimal defSkillCritRate;

        // Default defense against skill critical damage
        private decimal defSkillCritDamage;

        // Default defense against skill special critical rate
        private decimal defSkillSpCritRate;

        // Default defense against skill special critical damage
        private decimal defSkillSpCritDamage;

        #endregion

        #region Attack

        // Default attack
        private int atkInitial;
        private int atkFinal;

        // Default hit rate
        private decimal hitInitial;
        private decimal hitFinal;

        // Default attack animation
        private int animCaster;
        private int animTarget;

        // Default parameter attack force
        private decimal strForce;
        private decimal dexForce;
        private decimal agiForce;
        private decimal intForce;

        // Default defense attack force
        private decimal pdefForce;
        private decimal mdefForce;

        #endregion

        #region Critical

        // Default critical rate
        private decimal critRate;

        // Default critical damage
        private decimal critDamage;

        // Default critical guard rate reduction
        private decimal critDefGuard;

        // Default critical evasion rate reduction
        private decimal critDefEva;

        #endregion

        #region Special Critical

        // Special critical rate
        private decimal spCritRate;

        // Special critical damage
        private decimal spCritDamage;

        // Default special critical guard rate reduction
        private decimal spCritDefGuard;

        // Default special critical evasion rate reduction
        private decimal spCritDefEva;

        #endregion

        #region Defense

        // Physical defense
        private int pdefInitial;
        private int pdefFinal;

        // Magical defense
        private int mdefInitial;
        private int mdefFinal;

        #endregion

        #endregion

        #region Properties

        #region Equipment

        /// <summary>Gets or sets the default equipment name list.</summary>
        public List<EquipType> EquipType
        {
            get { return equipType; }
            set { equipType = new List<EquipType>(value); }
        }

        /// <summary>Gets or sets the default equipment ID list.</summary>
        public List<EquipType> EquipList
        {
            get { return equipList; }
            set { equipList = new List<EquipType>(value); }
        }

        /// <summary>Gets or sets the default dual hold checkBox.</summary>
        public bool DualHold
        {
            get { return dualHold; }
            set { dualHold = value; }
        }

        /// <summary>Gets or sets the default dual hold weapon slot name.</summary>
        public string DualHoldNameWeapon
        {
            get { return dualHoldNameWeapon; }
            set { dualHoldNameWeapon = value; }
        }

        /// <summary>Gets or sets the default dual hold shield slot name.</summary>
        public string DualHoldNameShield
        {
            get { return dualHoldNameShield; }
            set { dualHoldNameShield = value; }
        }

        /// <summary>Gets or sets the default dual hold weapon slot multiplier.</summary>
        public decimal DualHoldMulWeapon
        {
            get { return dualHoldMulWeapon; }
            set { dualHoldMulWeapon = value; }
        }

        /// <summary>Gets or sets the default dual hold shield slot multiplier.</summary>
        public decimal DualHoldMulShield
        {
            get { return dualHoldMulShield; }
            set { dualHoldMulShield = value; }
        }

        /// <summary>Gets or sets the default custom shield bypass checkBox.</summary>
        public bool ShieldBypass
        {
            get { return shieldBypass; }
            set { shieldBypass = value; }
        }

        /// <summary>Gets or sets the default shield bypass multiplier.</summary>
        public decimal ShieldBypassMul
        {
            get { return shieldBypassMul; }
            set { shieldBypassMul = value; }
        }

        /// <summary>Gets or sets the default weapon bypass checkBox.</summary>
        public bool WeaponBypass
        {
            get { return weaponBypass; }
            set { weaponBypass = value; }
        }

        /// <summary>Gets or sets the default weapon bypass multiplier.</summary>
        public decimal WeaponBypassMul
        {
            get { return weaponBypassMul; }
            set { weaponBypassMul = value; }
        }

        /// <summary>Gets or sets the default reduce hand.</summary>
        public int ReduceHand
        {
            get { return reduceHand; }
            set { reduceHand = value; }
        }

        /// <summary>Gets or sets the default reduce hand multiplier.</summary>
        public decimal ReduceHandMul
        {
            get { return reduceHandMul; }
            set { reduceHandMul = value; }
        }

        #endregion

        #region Parameter

        /// <summary>Gets or sets the default maximum HP Initial.</summary>
        public int MaxHPInitial
        {
            get { return maxHPInitial; }
            set { maxHPInitial = value; }
        }

        /// <summary>Gets or sets the default maximum HP Final.</summary>
        public int MaxHPFinal
        {
            get { return maxHPFinal; }
            set { maxHPFinal = value; }
        }

        /// <summary>Gets or sets the default maximum SP Initial.</summary>
        public int MaxSPInitial
        {
            get { return maxSPInitial; }
            set { maxSPInitial = value; }
        }

        /// <summary>Gets or sets the default maximum SP Final.</summary>
        public int MaxSPFinal
        {
            get { return maxSPFinal; }
            set { maxSPFinal = value; }
        }

        /// <summary>Gets or sets the default strengh Initial.</summary>
        public int StrInitial
        {
            get { return strInitial; }
            set { strInitial = value; }
        }

        /// <summary>Gets or sets the default strengh Final.</summary>
        public int StrFinal
        {
            get { return strFinal; }
            set { strFinal = value; }
        }

        /// <summary>Gets or sets the default dexterity Initial.</summary>
        public int DexInitial
        {
            get { return dexInitial; }
            set { dexInitial = value; }
        }

        /// <summary>Gets or sets the default dexterity Final.</summary>
        public int DexFinal
        {
            get { return dexFinal; }
            set { dexFinal = value; }
        }

        /// <summary>Gets or sets the default agility Initial.</summary>
        public int AgiInitial
        {
            get { return agiInitial; }
            set { agiInitial = value; }
        }

        /// <summary>Gets or sets the default agility Final.</summary>
        public int AgiFinal
        {
            get { return agiFinal; }
            set { agiFinal = value; }
        }

        /// <summary>Gets or sets the default intelligence Initial.</summary>
        public int IntInitial
        {
            get { return intInitial; }
            set { intInitial = value; }
        }

        /// <summary>Gets or sets the default intelligence Final.</summary>
        public int IntFinal
        {
            get { return intFinal; }
            set { intFinal = value; }
        }

        #endregion

        #region Parameter Rate

        /// <summary>Gets or sets the default strengh attack rate.</summary>
        public decimal StrRate
        {
            get { return strRate; }
            set { strRate = value; }
        }

        /// <summary>Gets or sets the default dexterity attack rate.</summary>
        public decimal DexRate
        {
            get { return dexRate; }
            set { dexRate = value; }
        }

        /// <summary>Gets or sets the default agility attack rate.</summary>
        public decimal AgiRate
        {
            get { return agiRate; }
            set { agiRate = value; }
        }

        /// <summary>Gets or sets the default intelligence attack rate.</summary>
        public decimal IntRate
        {
            get { return intRate; }
            set { intRate = value; }
        }

        /// <summary>Gets or sets the default unarmored physical defense attack rate.</summary>
        public decimal PDefRate
        {
            get { return pdefRate; }
            set { pdefRate = value; }
        }

        /// <summary>Gets or sets the default unarmored magical defense attack rate.</summary>
        public decimal MDefRate
        {
            get { return mdefRate; }
            set { mdefRate = value; }
        }

        /// <summary>Gets or sets the default guard rate.</summary>
        public decimal GuardRate
        {
            get { return guardRate; }
            set { guardRate = value; }
        }

        /// <summary>Gets or sets the default evasion rate.</summary>
        public decimal EvaRate
        {
            get { return evaRate; }
            set { evaRate = value; }
        }

        #endregion

        #region Defense against Attack Critical

        /// <summary>Gets or sets the default defense agaisnt attack critical rate.</summary>
        public decimal DefCritRate
        {
            get { return defCritRate; }
            set { defCritRate = value; }
        }

        /// <summary>Gets or sets the default defense agaisnt attack critical damage.</summary>
        public decimal DefCritDamage
        {
            get { return defCritDamage; }
            set { defCritDamage = value; }
        }

        /// <summary>Gets or sets the default defense agaisnt attack special critical rate.</summary>
        public decimal DefSpCritRate
        {
            get { return defSpCritRate; }
            set { defSpCritRate = value; }
        }

        /// <summary>Gets or sets the default defense agaisnt attack special critical damage.</summary>
        public decimal DefSpCritDamage
        {
            get { return defSpCritDamage; }
            set { defSpCritDamage = value; }
        }

        #endregion

        #region Defense against Skill Critical

        /// <summary>Gets or sets the default defense agaisnt skill critical rate.</summary>
        public decimal DefSkillCritRate
        {
            get { return defSkillCritRate; }
            set { defSkillCritRate = value; }
        }

        /// <summary>Gets or sets the default defense agaisnt skill critical damage.</summary>
        public decimal DefSkillCritDamage
        {
            get { return defSkillCritDamage; }
            set { defSkillCritDamage = value; }
        }

        /// <summary>Gets or sets the default defense agaisnt skill special critical rate.</summary>
        public decimal DefSkillSpCritRate
        {
            get { return defSkillSpCritRate; }
            set { defSkillSpCritRate = value; }
        }

        /// <summary>Gets or sets the default defense agaisnt skill special critical damage.</summary>
        public decimal DefSkillSpCritDamage
        {
            get { return defSkillSpCritDamage; }
            set { defSkillSpCritDamage = value; }
        }

        #endregion

        #region Attack

        /// <summary>Gets or sets the default attack Initial.</summary>
        public int AtkInitial
        {
            get { return atkInitial; }
            set { atkInitial = value; }
        }

        /// <summary>Gets or sets the default attack Final.</summary>
        public int AtkFinal
        {
            get { return atkFinal; }
            set { atkFinal = value; }
        }

        /// <summary>Gets or sets the default hit rate Initial.</summary>
        public decimal HitInitial
        {
            get { return hitInitial; }
            set { hitInitial = value; }
        }

        /// <summary>Gets or sets the default hit rate Final.</summary>
        public decimal HitFinal
        {
            get { return hitFinal; }
            set { hitFinal = value; }
        }

        /// <summary>Gets or sets the default caster attack animation.</summary>
        public int AnimCaster
        {
            get { return animCaster; }
            set { animCaster = value; }
        }

        /// <summary>Gets or sets the default target attack animation.</summary>
        public int AnimTarget
        {
            get { return animTarget; }
            set { animTarget = value; }
        }

        /// <summary>Gets or sets the default strengh attack force.</summary>
        public decimal StrForce
        {
            get { return strForce; }
            set { strForce = value; }
        }

        /// <summary>Gets or sets the default dexterity attack force.</summary>
        public decimal DexForce
        {
            get { return dexForce; }
            set { dexForce = value; }
        }

        /// <summary>Gets or sets the default agility attack force.</summary>
        public decimal AgiForce
        {
            get { return agiForce; }
            set { agiForce = value; }
        }

        /// <summary>Gets or sets the default intelligence attack force.</summary>
        public decimal IntForce
        {
            get { return intForce; }
            set { intForce = value; }
        }

        /// <summary>Gets or sets the default physical defense attack force.</summary>
        public decimal PDefForce
        {
            get { return pdefForce; }
            set { pdefForce = value; }
        }

        /// <summary>Gets or sets the default magical defense attack force.</summary>
        public decimal MDefForce
        {
            get { return mdefForce; }
            set { mdefForce = value; }
        }

        #endregion

        #region Critical

        /// <summary>Gets or sets the default critical rate.</summary>
        public decimal CritRate
        {
            get { return critRate; }
            set { critRate = value; }
        }

        /// <summary>Gets or sets the default critical damage.</summary>
        public decimal CritDamage
        {
            get { return critDamage; }
            set { critDamage = value; }
        }

        /// <summary>Gets or sets the default critical guard rate reduction.</summary>
        public decimal CritDefGuard
        {
            get { return critDefGuard; }
            set { critDefGuard = value; }
        }

        /// <summary>Gets or sets the default critical evasion rate reduction.</summary>
        public decimal CritDefEva
        {
            get { return critDefEva; }
            set { critDefEva = value; }
        }

        #endregion

        #region Special critical

        /// <summary>Gets or sets the default critical rate.</summary>
        public decimal SpCritRate
        {
            get { return spCritRate; }
            set { spCritRate = value; }
        }

        /// <summary>Gets or sets the default special critical damage.</summary>
        public decimal SpCritDamage
        {
            get { return spCritDamage; }
            set { spCritDamage = value; }
        }

        /// <summary>Gets or sets the default special critical guard rate reduction.</summary>
        public decimal SpCritDefGuard
        {
            get { return spCritDefGuard; }
            set { spCritDefGuard = value; }
        }

        /// <summary>Gets or sets the default special critical evasion rate reduction.</summary>
        public decimal SpCritDefEva
        {
            get { return spCritDefEva; }
            set { spCritDefEva = value; }
        }

        #endregion

        #region Defense

        /// <summary>Gets or sets the default physical defense Initial.</summary>
        public int PDefInitial
        {
            get { return pdefInitial; }
            set { pdefInitial = value; }
        }

        /// <summary>Gets or sets the default physical defense Final.</summary>
        public int PDefFinal
        {
            get { return pdefFinal; }
            set { pdefFinal = value; }
        }

        /// <summary>Gets or sets the default magical defense Initial.</summary>
        public int MDefInitial
        {
            get { return mdefInitial; }
            set { mdefInitial = value; }
        }

        /// <summary>Gets or sets the default magical defense Final.</summary>
        public int MDefFinal
        {
            get { return mdefFinal; }
            set { mdefFinal = value; }
        }

        #endregion

        #endregion

        #region Setup

        public ConfigurationDefault()
        {
            Reset();
        }

        /// <summary>Resets the configuration to default.</summary>
        public void Reset()
        {
            #region Equipment

            // Equipment list
            this.equipType.Clear();
            this.equipList.Clear();

            // Dual hold
            this.dualHold = defaultDualHold;

            // Dual hold name
            this.dualHoldNameWeapon = defaultDualHoldNameWeapon;
            this.dualHoldNameShield = defaultDualHoldNameShield;

            // Dual hold multiplier
            this.dualHoldMulWeapon = defaultDualHoldMulWeapon;
            this.dualHoldMulShield = defaultDualHoldMulShield;

            // Shield bypass
            this.shieldBypass = defaultShieldBypass;
            this.shieldBypassMul = defaultShieldBypassMul;

            // Weapon bypass
            this.weaponBypass = defaultWeaponBypass;
            this.weaponBypassMul = defaultWeaponBypassMul;

            // Reduce hand
            this.reduceHand = defaultReduceHand;
            this.reduceHandMul = defaultReduceHandMul;

            #endregion

            #region Parameter

            // Maximum HP
            this.maxHPInitial = defaultMaxHPInitial;
            this.maxHPFinal = defaultMaxHPFinal;

            // Maximum SP
            this.maxSPInitial = defaultMaxSPInitial;
            this.maxSPFinal = defaultMaxSPFinal;

            // Strengh
            this.strInitial = defaultStrInitial;
            this.strFinal = defaultStrFinal;

            // Dexterity
            this.dexInitial = defaultDexInitial;
            this.dexFinal = defaultDexFinal;

            // Agility
            this.agiInitial = defaultAgiInitial;
            this.agiFinal = defaultAgiFinal;

            // Intelligence
            this.intInitial = defaultIntInitial;
            this.intFinal = defaultIntFinal;

            #endregion

            #region Parameter Rate

            // Strengh attack rate
            this.strRate = defaultStrRate;

            // Dexterity attack rate
            this.dexRate = defaultDexRate;

            // Agility attack rate
            this.agiRate = defaultAgiRate;

            // Intelligence attack rate
            this.intRate = defaultIntRate;

            // Physical defense attack rate
            this.pdefRate = defaultPDefRate;

            // Magical defense attack rate
            this.mdefRate = defaultMDefRate;

            // Guard rate
            this.guardRate = defaultGuardRate;

            // Evasion rate
            this.evaRate = defaultEvaRate;

            #endregion

            #region Defense against Attack Critical

            // Defense against attack critical rate
            this.defCritRate = defaultDefCritRate;

            // Defense against attack critical damage
            this.defCritDamage = defaultDefCritDamage;

            // Defense against attack special critical rate
            this.defSpCritRate = defaultDefSpCritRate;

            // Defense against attack special critical damage
            this.defSpCritDamage = defaultDefSpCritDamage;

            #endregion

            #region Defense against Skill Critical

            // Defense against skill critical rate
            this.defSkillCritRate = defaultDefSkillCritRate;

            // Defense against skill critical damage
            this.defSkillCritDamage = defaultDefSkillCritDamage;

            // Defense against skill special critical rate
            this.defSkillSpCritRate = defaultDefSkillSpCritRate;

            // Defense against skill special critical damage
            this.defSkillSpCritDamage = defaultDefSkillSpCritDamage;

            #endregion

            #region Attack

            // Attack
            this.atkInitial = defaultAtkInitial;
            this.atkFinal = defaultAtkFinal;

            // Hit Rate
            this.hitInitial = defaultHitInitial;
            this.hitFinal = defaultHitFinal;

            // Attack animation
            this.animCaster = defaultAnimCaster;
            this.animTarget = defaultAnimTarget;

            // Parameter attack force
            this.strForce = defaultStrForce;
            this.dexForce = defaultDexForce;
            this.agiForce = defaultAgiForce;
            this.intForce = defaultIntForce;

            // Defense attack force
            this.pdefForce = defaultPDefForce;
            this.mdefForce = defaultMDefForce;

            #endregion

            #region Critical

            // Critical rate
            this.critRate = defaultCritRate;

            // Critical damage
            this.critDamage = defaultCritDamage;

            // Critical guard reduction
            this.critDefGuard = defaultCritDefGuard;

            // Critical evasion reduction
            this.critDefEva = defaultCritDefEva;

            #endregion

            #region Special Critical

            // Special critical rate
            this.spCritRate = defaultSpCritRate;

            // Special critical damage
            this.spCritDamage = defaultSpCritDamage;

            // Special critical guard reduction
            this.spCritDefGuard = defaultSpCritDefGuard;

            // Special critical evasion reduction
            this.spCritDefEva = defaultSpCritDefEva;

            #endregion

            #region Defense

            // Physical defense
            this.pdefInitial = defaultPDefInitial;
            this.pdefFinal = defaultPDefFinal;

            // Magical defense
            this.mdefInitial = defaultMDefInitial;
            this.mdefFinal = defaultMDefFinal;

            #endregion
        }

        public void SetupList()
        {
            int i = 0;
            if (AAEEData.EquipTypeName.Length > 0)
            {
                foreach (string equip in AAEEData.EquipTypeName)
                {
                    equipType.Add(new EquipType(i, equip));
                    i++;
                }
            }
            if (AAEEData.EquipListID.Length > 0)
            {
                foreach (int equip in AAEEData.EquipListID)
                {
                    equipList.Add(new EquipType(equip, AAEEData.EquipTypeName[equip]));
                }
            }
        }

        #endregion

        #region Reset

        #region Equipment

        // Reset equipment id list
        public void ResetEquipListID(int removeIndex)
        {
            int removed = 0;
            int sizeList = equipList.Count - 1;
            if (removeIndex >= 5)
            {
                equipType.RemoveAt(removeIndex);
                if (equipType.Count >= 5)
                {
                    foreach (EquipType equip in equipType)
                    {
                        if (equip.ID > removeIndex)
                        {
                            equip.ID = equip.ID - 1;
                        }
                    }
                }
                if (true)
                {
                    for (int i = 0; i <= sizeList; i++)
                    {
                        if (equipList[i - removed].ID == removeIndex)
                        {
                            equipList.RemoveAt(i - removed);
                            removed++;
                        }
                        else if (equipList[i - removed].ID > removeIndex)
                        {
                            equipList[i - removed].ID = equipList[i - removed].ID - 1;
                        }
                    }
                }
            }
        }

        #endregion
        
        #region Defense against Attack Critical

        // Reset defense against attack special critical rate
        public void ResetDefSpCritRate()
        {
            this.defSpCritRate = defaultDefSpCritRate;
        }

        // Reset defense against attack special critical damage
        public void ResetDefSpCritDamage()
        {
            this.defSpCritDamage = defaultDefSpCritDamage;
        }

        #endregion
        
        #region Defense against Skill Critical

        // Reset defense against skill critical rate
        public void ResetDefSkillCritRate()
        {
            this.defSkillCritRate = defaultDefSkillCritRate;
        }

        // Reset defense against skill critical damage
        public void ResetDefSkillCritDamage()
        {
            this.defSkillCritDamage = defaultDefSkillCritDamage;
        }

        // Reset defense against skill special critical rate
        public void ResetDefSkillSpCritRate()
        {
            this.defSkillSpCritRate = defaultDefSkillSpCritRate;
        }

        // Reset defense against skill special critical damage
        public void ResetDefSkillSpCritDamage()
        {
            this.defSkillSpCritDamage = defaultDefSkillSpCritDamage;
        }

        #endregion

        #region Attack

        // Reset attack
        public void ResetAttack()
        {
            this.atkFinal = defaultAtkFinal;
        }

        // Reset hit rate
        public void ResetHit()
        {
            this.hitFinal = defaultHitFinal;
        }

        // Reset unarmed attack animation
        public void ResetAnim()
        {
            this.animCaster = defaultAnimCaster;
            this.animTarget = defaultAnimTarget;
        }

        #endregion

        #region Special Critical

        // Special critical rate
        public void ResetSpCritRate()
        {
            this.spCritRate = defaultSpCritRate;
        }

        // Special critical damage
        public void ResetSpCritDamage()
        {
            this.spCritDamage = defaultSpCritDamage;
        }

        // Special critical guard reduction
        public void ResetSpCritDefGuard()
        {
            this.spCritDefGuard = defaultSpCritDefGuard;
        }

        // Special critical evasion reduction
        public void ResetSpCritDefEva()
        {
            this.spCritDefEva = defaultSpCritDefEva;
        }

        #endregion

        #region Special Critical

        // Special physical defense
        public void ResetPDef()
        {
            this.pdefFinal = defaultPDefFinal;
        }

        // Special critical damage
        public void ResetMDef()
        {
            this.mdefFinal = defaultMDefFinal;
        }

        #endregion

        #endregion

        #region Behavior

        /// <summary>Compares two configurations.</summary>
        /// <param name="other">The other configuration.</param>
        /// <returns>True or false.</returns>
        public bool Equals(ConfigurationDefault other)
        {
            #region Equipment

            // Equipment list
            if (!this.EquipType.Equals(other.EquipType)) return false;
            if (!this.EquipList.Equals(other.EquipList)) return false;

            // Dual hold
            if (this.DualHold != other.DualHold) return false;

            // Dual hold name
            if (this.DualHoldNameWeapon != other.DualHoldNameWeapon) return false;
            if (this.DualHoldNameShield != other.DualHoldNameShield) return false;

            // Dual hold multiplier
            if (this.DualHoldMulWeapon != other.DualHoldMulWeapon) return false;
            if (this.DualHoldMulShield != other.DualHoldMulShield) return false;

            // Shield bypass
            if (this.ShieldBypass != other.ShieldBypass) return false;
            if (this.ShieldBypassMul != other.ShieldBypassMul) return false;

            // Weapon bypass
            if (this.WeaponBypass != other.WeaponBypass) return false;
            if (this.WeaponBypassMul != other.WeaponBypassMul) return false;

            // Reduce hand
            if (this.ReduceHand != other.ReduceHand) return false;
            if (this.ReduceHandMul != other.ReduceHandMul) return false;

            #endregion

            #region Parameter

            // Maximum HP
            if (this.MaxHPInitial != other.MaxHPInitial) return false;
            if (this.MaxHPFinal != other.MaxHPFinal) return false;

            // Maximum SP
            if (this.MaxSPInitial != other.MaxSPInitial) return false;
            if (this.MaxSPFinal != other.MaxSPFinal) return false;

            // Strengh
            if (this.StrInitial != other.StrInitial) return false;
            if (this.StrFinal != other.StrFinal) return false;

            // Dexterity
            if (this.DexInitial != other.DexInitial) return false;
            if (this.DexFinal != other.DexFinal) return false;

            // Agility
            if (this.AgiInitial != other.AgiInitial) return false;
            if (this.AgiFinal != other.AgiFinal) return false;

            // Intelligence
            if (this.IntInitial != other.IntInitial) return false;
            if (this.IntFinal != other.IntFinal) return false;

            // Guard rate
            if (this.GuardRate != other.GuardRate) return false;

            // Evasion rate
            if (this.EvaRate != other.EvaRate) return false;

            #endregion

            #region Parameter rate

            // Strengh attack rate
            if (this.StrRate != other.StrRate) return false;

            // Dexterity attack rate
            if (this.DexRate != other.DexRate) return false;

            // Agility attackrate
            if (this.AgiRate != other.AgiRate) return false;

            // Intelligence attack rate
            if (this.IntRate != other.IntRate) return false;

            // Physical defense attack rate
            if (this.PDefRate != other.PDefRate) return false;

            // Magical defense attack rate
            if (this.MDefRate != other.MDefRate) return false;

            #endregion

            #region Defense against Attack Critical

            // Defense against attack critical rate
            if (this.DefCritRate != other.DefCritRate) return false;

            // Defense against attack critical damage
            if (this.DefCritDamage != other.DefCritDamage) return false;

            // Defense against attack special critical rate
            if (this.DefSpCritRate != other.DefSpCritRate) return false;

            // Defense against attack special critical damage
            if (this.DefSpCritDamage != other.DefSpCritDamage) return false;

            #endregion

            #region Defense against Skill Critical

            // Defense against skill critical rate
            if (this.DefSkillCritRate != other.DefSkillCritRate) return false;

            // Defense against skill critical damage
            if (this.DefSkillCritDamage != other.DefSkillCritDamage) return false;

            // Defense against skill special critical rate
            if (this.DefSkillSpCritRate != other.DefSkillSpCritRate) return false;

            // Defense against skill special critical Damage
            if (this.DefSkillSpCritDamage != other.DefSkillSpCritDamage) return false;

            #endregion

            #region Attack

            // Attack
            if (this.AtkInitial != other.AtkInitial) return false;
            if (this.AtkFinal != other.AtkFinal) return false;

            // Hit rate
            if (this.HitInitial != other.HitInitial) return false;
            if (this.HitFinal != other.HitFinal) return false;

            // Parameter attack force
            if (this.StrForce != other.StrForce) return false;
            if (this.DexForce != other.DexForce) return false;
            if (this.AgiForce != other.AgiForce) return false;
            if (this.IntForce != other.IntForce) return false;

            // Defense attack force
            if (this.PDefForce != other.PDefForce) return false;
            if (this.MDefForce != other.MDefForce) return false;

            #endregion

            #region Critical

            // Critical Rate
            if (this.CritRate != other.CritRate) return false;

            // Critical Damage
            if (this.CritDamage != other.CritDamage) return false;

            // Critical Guard Reduction
            if (this.CritDefGuard != other.CritDefGuard) return false;

            // Critical Evasion Reduction
            if (this.CritDefEva != other.CritDefEva) return false;

            #endregion

            #region Special Critical

            // Special critical rate
            if (this.SpCritRate != other.SpCritRate) return false;

            // Special critical damage
            if (this.SpCritDamage != other.SpCritDamage) return false;

            // Special critical guard reduction
            if (this.SpCritDefGuard != other.SpCritDefGuard) return false;

            // Special critical evasion reduction
            if (this.SpCritDefEva != other.SpCritDefEva) return false;

            #endregion

            #region Defense

            // Physical defense
            if (this.PDefInitial != other.PDefInitial) return false;
            if (this.PDefFinal != other.PDefFinal) return false;

            // Magical defense
            if (this.MDefInitial != other.MDefInitial) return false;
            if (this.MDefFinal != other.MDefFinal) return false;

            #endregion

            return true;
        }

        #endregion

        #region Clone

        /// <summary>Copy configurations</summary>
        /// <param name="other">Other configuration to copy</param>
        public virtual void CloneFrom(ConfigurationDefault other)
        {
            #region Equipment

            // Equipment list
            this.EquipType.Clear();
            if (other.EquipType.Count > 0)
            {
                foreach (EquipType equip in other.EquipType)
                {
                    this.EquipType.Add(new EquipType(equip.ID, equip.Name));
                }
            }
            this.EquipList.Clear();
            if (other.EquipList.Count > 0)
            {
                foreach (EquipType equip in other.EquipList)
                {
                    this.EquipList.Add(equip);
                }
            }

            // Dual hold
            this.DualHold = other.DualHold;

            // Dual hold name
            this.DualHoldNameWeapon = other.DualHoldNameWeapon;
            this.DualHoldNameShield = other.DualHoldNameShield;

            // Dual hold multiplier
            this.DualHoldMulWeapon = other.DualHoldMulWeapon;
            this.DualHoldMulShield = other.DualHoldMulShield;

            // Shield bypass
            this.ShieldBypass = other.ShieldBypass;
            this.ShieldBypassMul = other.ShieldBypassMul;

            // Weapon bypass
            this.WeaponBypass = other.WeaponBypass;
            this.WeaponBypassMul = other.WeaponBypassMul;

            // Reduce hand
            this.ReduceHand = other.ReduceHand;
            this.ReduceHandMul = other.ReduceHandMul;

            #endregion

            #region Parameter

            // Maximum HP
            this.MaxHPInitial = other.MaxHPInitial;
            this.MaxHPFinal = other.MaxHPFinal;

            // Maximum SP
            this.MaxSPInitial = other.MaxSPInitial;
            this.MaxSPFinal = other.MaxSPFinal;

            // Strengh
            this.StrInitial = other.StrInitial;
            this.StrFinal = other.StrFinal;

            // Dexterity
            this.DexInitial = other.DexInitial;
            this.DexFinal = other.DexFinal;

            // Agility
            this.AgiInitial = other.AgiInitial;
            this.AgiFinal = other.AgiFinal;

            // Intelligence
            this.IntInitial = other.IntInitial;
            this.IntFinal = other.IntFinal;

            // Guard rate
            this.GuardRate = other.GuardRate;

            // Evasion rate
            this.EvaRate = other.EvaRate;

            #endregion

            #region Parameter rate

            // Strengh attack rate
            this.StrRate = other.StrRate;

            // Dexterity attack rate
            this.DexRate = other.DexRate;

            // Agility attackrate
            this.AgiRate = other.AgiRate;

            // Intelligence attack rate
            this.IntRate = other.IntRate;

            // Physical defense attack rate
            this.PDefRate = other.PDefRate;

            // Magical defense attack rate
            this.MDefRate = other.MDefRate;

            #endregion

            #region Defense against Attack Critical

            // Defense against attack critical rate
            this.DefCritRate = other.DefCritRate;

            // Defense against attack critical damage
            this.DefCritDamage = other.DefCritDamage;

            // Defense against attack special critical rate
            this.DefSpCritRate = other.DefSpCritRate;

            // Defense against attack special critical damage
            this.DefSpCritDamage = other.DefSpCritDamage;

            #endregion

            #region Defense against Skill Critical

            // Defense against skill critical rate
            this.DefSkillCritRate = other.DefSkillCritRate;

            // Defense against skill critical damage
            this.DefSkillCritDamage = other.DefSkillCritDamage;

            // Defense against skill special critical rate
            this.DefSkillSpCritRate = other.DefSkillSpCritRate;

            // Defense against skill special critical Damage
            this.DefSkillSpCritDamage = other.DefSkillSpCritDamage;

            #endregion

            #region Attack

            // Attack
            this.AtkInitial = other.AtkInitial;
            this.AtkFinal = other.AtkFinal;

            // Hit rate
            this.HitInitial = other.HitInitial;
            this.HitFinal = other.HitFinal;

            // Attack animation
            this.AnimCaster = other.AnimCaster;
            this.AnimTarget = other.AnimTarget;

            // Parameter attack force
            this.StrForce = other.StrForce;
            this.DexForce = other.DexForce;
            this.AgiForce = other.AgiForce;
            this.IntForce = other.IntForce;

            // Defense attack force
            this.PDefForce = other.PDefForce;
            this.MDefForce = other.MDefForce;

            #endregion

            #region Critical

            // Critical Rate
            this.CritRate = other.CritRate;

            // Critical Damage
            this.CritDamage = other.CritDamage;

            // Critical Guard Reduction
            this.CritDefGuard = other.CritDefGuard;

            // Critical Evasion Reduction
            this.CritDefEva = other.CritDefEva;

            #endregion

            #region Special Critical

            // Special critical rate
            this.SpCritRate = other.SpCritRate;

            // Special critical damage
            this.SpCritDamage = other.SpCritDamage;

            // Special critical guard reduction
            this.SpCritDefGuard = other.SpCritDefGuard;

            // Special critical evasion reduction
            this.SpCritDefEva = other.SpCritDefEva;

            #endregion

            #region Defense

            // Physical defense
            this.PDefInitial = other.PDefInitial;
            this.PDefFinal = other.PDefFinal;

            // Magical defense
            this.MDefInitial = other.MDefInitial;
            this.MDefFinal = other.MDefFinal;

            #endregion
        }

        #endregion
    }
}