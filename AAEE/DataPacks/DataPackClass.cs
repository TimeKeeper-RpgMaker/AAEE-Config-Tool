using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using AAEE.Data;
using AAEE.Utility;

namespace AAEE.DataPacks
{
    /// <summary>This class serves as datapack for class configuration.</summary>
    [XmlType("DataPackClass")]
    public class DataPackClass
    {
        #region Defaults

        // Default name
        private string defaultName = "New Family";
        
        #region Equipment

        // Default equipment
        private bool defaultCustomEquip = false;

        // Default dual hold
        private bool defaultDualHold = false;

        // Default dual hold name
        private bool defaultCustomDualHoldName = false;
        private string defaultDualHoldNameWeapon = "Main Hand";
        private string defaultDualHoldNameShield = "Off Hand";

        // Default dual hold multiplier
        private bool defaultCustomDualHoldMul = false;
        private decimal defaultDualHoldMulWeapon = 1.0m;
        private decimal defaultDualHoldMulShield = 0.5m;

        // Default shield bypass
        private bool defaultShieldBypass = false;
        private decimal defaultShieldBypassMul = 0.1m;

        // Default weapon Bypass
        private bool defaultWeaponBypass = false;
        private decimal defaultWeaponBypassMul = 0.1m;

        // Default reduce hand
        private bool defaultCustomReduceHand = false;
        private int defaultReduceHand = 0;
        private decimal defaultReduceHandMul = 0.1m;

        #endregion

        #region Parameter

        // Default maximum HP
        private bool defaultCustomMaxHP = false;
        private decimal defaultMaxHPMul = 1.0m;
        private int defaultMaxHPAdd = 0;

        // Default maximum SP
        private bool defaultCustomMaxSP = false;
        private decimal defaultMaxSPMul = 1.0m;
        private int defaultMaxSPAdd = 0;

        // Default strengh
        private bool defaultCustomStr = false;
        private decimal defaultStrMul = 1.0m;
        private int defaultStrAdd = 0;

        // Default dexterity
        private bool defaultCustomDex = false;
        private decimal defaultDexMul = 1.0m;
        private int defaultDexAdd = 0;

        // Default agility
        private bool defaultCustomAgi = false;
        private decimal defaultAgiMul = 1.0m;
        private int defaultAgiAdd = 0;

        // Default intelligence
        private bool defaultCustomInt = false;
        private decimal defaultIntMul = 1.0m;
        private int defaultIntAdd = 0;

        #endregion

        #region Parameter rate

        // Default strengh attack rate
        private bool defaultCustomStrRate = false;
        private decimal defaultStrRateMul = 1.0m;
        private int defaultStrRateAdd = 0;

        // Default dexterity attack rate
        private bool defaultCustomDexRate = false;
        private decimal defaultDexRateMul = 1.0m;
        private int defaultDexRateAdd = 0;

        // Default agility attack rate
        private bool defaultCustomAgiRate = false;
        private decimal defaultAgiRateMul = 1.0m;
        private int defaultAgiRateAdd = 0;

        // Default intelligence attack rate
        private bool defaultCustomIntRate = false;
        private decimal defaultIntRateMul = 1.0m;
        private int defaultIntRateAdd = 0;

        // Default physical defense attack rate
        private bool defaultCustomPDefRate = false;
        private decimal defaultPDefRateMul = 1.0m;
        private int defaultPDefRateAdd = 0;

        // Default magical defense attack rate
        private bool defaultCustomMDefRate = false;
        private decimal defaultMDefRateMul = 1.0m;
        private int defaultMDefRateAdd = 0;

        // Default guard rate
        private bool defaultCustomGuardRate = false;
        private decimal defaultGuardRateMul = 1.0m;
        private int defaultGuardRateAdd = 0;

        // Default evasion rate
        private bool defaultCustomEvaRate = false;
        private decimal defaultEvaRateMul = 1.0m;
        private int defaultEvaRateAdd = 0;

        #endregion

        #region Defense against attack critical

        // Default defense against attack critical rate
        private bool defaultCustomDefCritRate = false;
        private decimal defaultDefCritRateMul = 1.0m;
        private int defaultDefCritRateAdd = 0;

        // Default defense against attack critical damage
        private bool defaultCustomDefCritDamage = false;
        private decimal defaultDefCritDamageMul = 1.0m;
        private int defaultDefCritDamageAdd = 0;

        // Default defense against attack special critical rate
        private bool defaultCustomDefSpCritRate = false;
        private decimal defaultDefSpCritRateMul = 1.0m;
        private int defaultDefSpCritRateAdd = 0;

        // Default defense against attack special critical damage
        private bool defaultCustomDefSpCritDamage = false;
        private decimal defaultDefSpCritDamageMul = 1.0m;
        private int defaultDefSpCritDamageAdd = 0;

        #endregion

        #region Defense against skill critical

        // Default defense against skill critical rate
        private bool defaultCustomDefSkillCritRate = false;
        private decimal defaultDefSkillCritRateMul = 1.0m;
        private int defaultDefSkillCritRateAdd = 0;

        // Default defense against skill critical damage
        private bool defaultCustomDefSkillCritDamage = false;
        private decimal defaultDefSkillCritDamageMul = 1.0m;
        private int defaultDefSkillCritDamageAdd = 0;

        // Default defense against skill special critical rate
        private bool defaultCustomDefSkillSpCritRate = false;
        private decimal defaultDefSkillSpCritRateMul = 1.0m;
        private int defaultDefSkillSpCritRateAdd = 0;

        // Default defense against skill special critical damage
        private bool defaultCustomDefSkillSpCritDamage = false;
        private decimal defaultDefSkillSpCritDamageMul = 1.0m;
        private int defaultDefSkillSpCritDamageAdd = 0;

        #endregion

        #region Passive attack

        // Default passive attack
        private bool defaultCustomPassiveAtk = false;
        private decimal defaultAtkMul = 1.0m;
        private int defaultAtkAdd = 0;

        // Default passive hit rate
        private bool defaultCustomPassiveHit = false;
        private decimal defaultHitMul = 1.0m;
        private int defaultHitAdd = 0;

        #endregion

        #region Passive critical

        // Default passive critical rate
        private bool defaultCustomPassiveCritRate = false;
        private decimal defaultCritRateMul = 1.0m;
        private int defaultCritRateAdd = 0;

        // Default passive critical damage
        private bool defaultCustomPassiveCritDamage = false;
        private decimal defaultCritDamageMul = 1.0m;
        private int defaultCritDamageAdd = 0;

        // Default passive critical guard rate reduction
        private bool defaultCustomPassiveCritDefGuard = false;
        private decimal defaultCritDefGuardMul = 1.0m;
        private int defaultCritDefGuardAdd = 0;

        // Default passive critical evasion rate reduction
        private bool defaultCustomPassiveCritDefEva = false;
        private decimal defaultCritDefEvaMul = 1.0m;
        private int defaultCritDefEvaAdd = 0;

        #endregion

        #region Passive special critical

        // Default passive special critical rate
        private bool defaultCustomPassiveSpCritRate = false;
        private decimal defaultSpCritRateMul = 1.0m;
        private int defaultSpCritRateAdd = 0;

        // Default passive special critical damage
        private bool defaultCustomPassiveSpCritDamage = false;
        private decimal defaultSpCritDamageMul = 1.0m;
        private int defaultSpCritDamageAdd = 0;

        // Default passive special critical guard rate reduction
        private bool defaultCustomPassiveSpCritDefGuard = false;
        private decimal defaultSpCritDefGuardMul = 1.0m;
        private int defaultSpCritDefGuardAdd = 0;

        // Default passive special critical evasion rate reduction
        private bool defaultCustomPassiveSpCritDefEva = false;
        private decimal defaultSpCritDefEvaMul = 1.0m;
        private int defaultSpCritDefEvaAdd = 0;

        #endregion

        #region Passive defense

        // Default physical defense
        private bool defaultCustomPassivePDef = false;
        private decimal defaultPDefMul = 1.0m;
        private int defaultPDefAdd = 0;

        // Default magical defense
        private bool defaultCustomPassiveMDef = false;
        private decimal defaultMDefMul = 1.0m;
        private int defaultMDefAdd = 0;

        #endregion

        #region Unarmed Attack

        // Default attack
        private bool defaultCustomUnarmedAtk = false;
        private int defaultAtkInitial = 0;
        private int defaultAtkFinal = 0;

        // Default hit rate
        private bool defaultCustomUnarmedHit = false;
        private decimal defaultHitInitial = 0.0m;
        private decimal defaultHitFinal = 0.0m;

        // Default attack animation
        private bool defaultCustomUnarmedAnim = false;
        private int defaultAnimCaster = 0;
        private int defaultAnimTarget = 0;

        // Default parameter attack force
        private bool defaultCustomUnarmedParamForce = false;
        private decimal defaultStrForce = 1.0m;
        private decimal defaultDexForce = 0.0m;
        private decimal defaultAgiForce = 0.0m;
        private decimal defaultIntForce = 0.0m;

        // Default defense attack force
        private bool defaultCustomUnarmedDefenseForce = false;
        private decimal defaultPDefForce = 1.0m;
        private decimal defaultMDefForce = 0.0m;

        #endregion

        #region Unarmed Critical

        // Default critical rate
        private bool defaultCustomUnarmedCritRate = false;
        private decimal defaultCritRate = 0.04m;

        // Default critical damage
        private bool defaultCustomUnarmedCritDamage = false;
        private decimal defaultCritDamage = 2.0m;

        // Default critical guard rate reduction
        private bool defaultCustomUnarmedCritDefGuard = false;
        private decimal defaultCritDefGuard = 1.0m;

        // Default critical evasion rate reduction
        private bool defaultCustomUnarmedCritDefEva = false;
        private decimal defaultCritDefEva = 1.0m;

        #endregion

        #region Unarmed Special Critical

        // Default special critical rate
        private bool defaultCustomUnarmedSpCritRate = false;
        private decimal defaultSpCritRate = 0.05m;

        // Default special critical damage
        private bool defaultCustomUnarmedSpCritDamage = false;
        private decimal defaultSpCritDamage = 3.0m;

        // Default special critical guard rate reduction
        private bool defaultCustomUnarmedSpCritDefGuard = false;
        private decimal defaultSpCritDefGuard = 0.75m;

        // Default special critical evasion rate reduction
        private bool defaultCustomUnarmedSpCritDefEva = false;
        private decimal defaultSpCritDefEva = 0.5m;

        #endregion

        #region Unarmoured defense

        // Default physical defense
        private bool defaultCustomUnarmouredPDef = false;
        private int defaultPDefInitial = 0;
        private int defaultPDefFinal = 0;

        // Default magical defense
        private bool defaultCustomUnarmouredMDef = false;
        private int defaultMDefInitial = 0;
        private int defaultMDefFinal = 0;

        #endregion

        #endregion

        #region Fields

        // Id and name
        private int id;
        private string name;

        // In Family
        private List<Class> classFamily = new List<Class>();

        #region Equipment

        // Equipment List
        private bool customEquip;
        private List<EquipType> equipType = new List<EquipType>();
        private List<EquipType> equipList = new List<EquipType>();

        // Dual hold
        private bool dualHold;

        // Dual hold name
        private bool customDualHoldName;
        private string dualHoldNameWeapon;
        private string dualHoldNameShield;

        // Dual hold multiplier
        private bool customDualHoldMul;
        private decimal dualHoldMulWeapon;
        private decimal dualHoldMulShield;

        // Shield bypass
        private bool shieldBypass;
        private decimal shieldBypassMul;

        // Weapon bypass
        private bool weaponBypass;
        private decimal weaponBypassMul;

        // Reduce hand
        private bool customReduceHand;
        private int reduceHand;
        private decimal reduceHandMul;

        #endregion

        #region Parameter

        // Maximum HP
        private bool customMaxHP;
        private decimal maxHPMul;
        private int maxHPAdd;

        // Maximum SP
        private bool customMaxSP;
        private decimal maxSPMul;
        private int maxSPAdd;

        // Strengh
        private bool customStr;
        private decimal strMul;
        private int strAdd;

        // Dexterity
        private bool customDex;
        private decimal dexMul;
        private int dexAdd;

        // Agility
        private bool customAgi;
        private decimal agiMul;
        private int agiAdd;

        // Intelligence
        private bool customInt;
        private decimal intMul;
        private int intAdd;

        #endregion

        #region Parameter rate

        // Strengh attack rate
        private bool customStrRate;
        private decimal strRateMul;
        private int strRateAdd;

        // Dexterity attack rate
        private bool customDexRate;
        private decimal dexRateMul;
        private int dexRateAdd;

        // Agility attack rate
        private bool customAgiRate;
        private decimal agiRateMul;
        private int agiRateAdd;

        // Intelligence attack rate
        private bool customIntRate;
        private decimal intRateMul;
        private int intRateAdd;

        // Physical defense attack rate
        private bool customPDefRate;
        private decimal pdefRateMul;
        private int pdefRateAdd;

        // Magical defense attack rate
        private bool customMDefRate;
        private decimal mdefRateMul;
        private int mdefRateAdd;

        // Guard rate
        private bool customGuardRate;
        private decimal guardRateMul;
        private int guardRateAdd;

        // Evasion rate
        private bool customEvaRate;
        private decimal evaRateMul;
        private int evaRateAdd;

        #endregion

        #region Defense against attack critical

        // Defense against attack critical rate
        private bool customDefCritRate;
        private decimal defCritRateMul;
        private int defCritRateAdd;

        // Defense against attack critical damage
        private bool customDefCritDamage;
        private decimal defCritDamageMul;
        private int defCritDamageAdd;

        // Defense against attack special critical rate
        private bool customDefSpCritRate;
        private decimal defSpCritRateMul;
        private int defSpCritRateAdd;

        // Defense against attack special critical damage
        private bool customDefSpCritDamage;
        private decimal defSpCritDamageMul;
        private int defSpCritDamageAdd;

        #endregion

        #region Defense against skill critical

        // Default defense against skill critical rate
        private bool customDefSkillCritRate;
        private decimal defSkillCritRateMul;
        private int defSkillCritRateAdd;

        // Default defense against skill critical damage
        private bool customDefSkillCritDamage;
        private decimal defSkillCritDamageMul;
        private int defSkillCritDamageAdd;

        // Default defense against skill special critical rate
        private bool customDefSkillSpCritRate;
        private decimal defSkillSpCritRateMul;
        private int defSkillSpCritRateAdd;

        // Default defense against skill special critical damage
        private bool customDefSkillSpCritDamage;
        private decimal defSkillSpCritDamageMul;
        private int defSkillSpCritDamageAdd;

        #endregion

        #region Passive attack

        // Default attack
        private bool customPassiveAtk;
        private decimal atkMul;
        private int atkAdd;

        // Default hit rate
        private bool customPassiveHit;
        private decimal hitMul;
        private int hitAdd;

        #endregion

        #region Passive critical

        // Default critical rate
        private bool customPassiveCritRate;
        private decimal critRateMul;
        private int critRateAdd;

        // Default critical damage
        private bool customPassiveCritDamage;
        private decimal critDamageMul;
        private int critDamageAdd;

        // Default critical guard rate reduction
        private bool customPassiveCritDefGuard;
        private decimal critDefGuardMul;
        private int critDefGuardAdd;

        // Default critical evasion rate reduction
        private bool customPassiveCritDefEva;
        private decimal critDefEvaMul;
        private int critDefEvaAdd;

        #endregion

        #region Passive special critical

        // Default special critical rate
        private bool customPassiveSpCritRate;
        private decimal spCritRateMul;
        private int spCritRateAdd;

        // Default special critical damage
        private bool customPassiveSpCritDamage;
        private decimal spCritDamageMul;
        private int spCritDamageAdd;

        // Default special critical guard rate reduction
        private bool customPassiveSpCritDefGuard;
        private decimal spCritDefGuardMul;
        private int spCritDefGuardAdd;

        // Default special critical evasion rate reduction
        private bool customPassiveSpCritDefEva;
        private decimal spCritDefEvaMul;
        private int spCritDefEvaAdd;

        #endregion

        #region Passive defense

        // Default physical defense
        private bool customPassivePDef;
        private decimal pdefMul;
        private int pdefAdd;

        // Default magical defense
        private bool customPassiveMDef;
        private decimal mdefMul;
        private int mdefAdd;

        #endregion

        #region Unarmed Attack

        // Attack
        private bool customUnarmedAtk;
        private int atkInitial;
        private int atkFinal;

        // Hit rate
        private bool customUnarmedHit;
        private decimal hitInitial;
        private decimal hitFinal;

        // Attack animation
        private bool customUnarmedAnim;
        private int animCaster;
        private int animTarget;

        // Parameter attack force
        private bool customUnarmedParamForce;
        private decimal strForce;
        private decimal dexForce;
        private decimal agiForce;
        private decimal intForce;

        // Defense attack force
        private bool customUnarmedDefenseForce;
        private decimal pdefForce;
        private decimal mdefForce;

        #endregion

        #region Unarmed Critical

        // Critical rate
        private bool customUnarmedCritRate;
        private decimal critRate;

        // Critical damage
        private bool customUnarmedCritDamage;
        private decimal critDamage;

        // Critical guard rate reduction
        private bool customUnarmedCritDefGuard;
        private decimal critDefGuard;

        // Critical evasion rate reduction
        private bool customUnarmedCritDefEva;
        private decimal critDefEva;

        #endregion

        #region Unarmed Special Critical

        // Special critical rate
        private bool customUnarmedSpCritRate;
        private decimal spCritRate;

        // Special critical damage
        private bool customUnarmedSpCritDamage;
        private decimal spCritDamage;

        // Special critical guard rate reduction
        private bool customUnarmedSpCritDefGuard;
        private decimal spCritDefGuard;

        // Special critical evasion rate reduction
        private bool customUnarmedSpCritDefEva;
        private decimal spCritDefEva;

        #endregion

        #region Unarmoured Defense

        // Physical defense
        private bool customUnarmouredPDef;
        private int pdefInitial;
        private int pdefFinal;

        // Magical defense
        private bool customUnarmouredMDef;
        private int mdefInitial;
        private int mdefFinal;

        #endregion

        #endregion

        #region Properties

        /// <summary>Gets or sets the equipment ID.</summary>
        [XmlAttribute()]
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>Gets or sets the equipment name.</summary>
        [XmlAttribute()]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>Gets or sets the list of class in the family.</summary>
        public List<Class> ClassFamily
        {
            get { return classFamily; }
            set { classFamily = new List<Class>(value); }
        }

        #region Equipment

        /// <summary>Gets or sets the actor or class custom equip checkBox.</summary>
        public bool CustomEquip
        {
            get { return customEquip; }
            set { customEquip = value; }
        }

        /// <summary>Gets or sets the actor or class equipment name list.</summary>
        public List<EquipType> EquipType
        {
            get { return equipType; }
            set { equipType = new List<EquipType>(value); }
        }

        /// <summary>Gets or sets the actor or class equipment ID list.</summary>
        public List<EquipType> EquipList
        {
            get { return equipList; }
            set { equipList = new List<EquipType>(value); }
        }

        /// <summary>Gets or sets the actor or class dual hold checkBox.</summary>
        public bool DualHold
        {
            get { return dualHold; }
            set { dualHold = value; }
        }

        /// <summary>Gets or sets the actor or class custom dual hold name checkBox.</summary>
        public bool CustomDualHoldName
        {
            get { return customDualHoldName; }
            set { customDualHoldName = value; }
        }

        /// <summary>Gets or sets the actor or class dual hold weapon slot name.</summary>
        public string DualHoldNameWeapon
        {
            get { return dualHoldNameWeapon; }
            set { dualHoldNameWeapon = value; }
        }

        /// <summary>Gets or sets the actor or class dual hold shield slot name.</summary>
        public string DualHoldNameShield
        {
            get { return dualHoldNameShield; }
            set { dualHoldNameShield = value; }
        }

        /// <summary>Gets or sets the actor or class custom dual hold multiplier checkBox.</summary>
        public bool CustomDualHoldMul
        {
            get { return customDualHoldMul; }
            set { customDualHoldMul = value; }
        }

        /// <summary>Gets or sets the actor or class dual hold weapon slot multiplier.</summary>
        public decimal DualHoldMulWeapon
        {
            get { return dualHoldMulWeapon; }
            set { dualHoldMulWeapon = value; }
        }

        /// <summary>Gets or sets the actor or class dual hold shield slot multiplier.</summary>
        public decimal DualHoldMulShield
        {
            get { return dualHoldMulShield; }
            set { dualHoldMulShield = value; }
        }

        /// <summary>Gets or sets the actor or class custom shield bypass checkBox.</summary>
        public bool ShieldBypass
        {
            get { return shieldBypass; }
            set { shieldBypass = value; }
        }

        /// <summary>Gets or sets the actor or class shield bypass multiplier.</summary>
        public decimal ShieldBypassMul
        {
            get { return shieldBypassMul; }
            set { shieldBypassMul = value; }
        }

        /// <summary>Gets or sets the actor or class weapon bypass checkBox.</summary>
        public bool WeaponBypass
        {
            get { return weaponBypass; }
            set { weaponBypass = value; }
        }

        /// <summary>Gets or sets the actor or class weapon bypass multiplier.</summary>
        public decimal WeaponBypassMul
        {
            get { return weaponBypassMul; }
            set { weaponBypassMul = value; }
        }

        /// <summary>Gets or sets the actor or class custom reduce hand checkBox.</summary>
        public bool CustomReduceHand
        {
            get { return customReduceHand; }
            set { customReduceHand = value; }
        }

        /// <summary>Gets or sets the actor or class reduce hand.</summary>
        public int ReduceHand
        {
            get { return reduceHand; }
            set { reduceHand = value; }
        }

        /// <summary>Gets or sets the actor or class reduce hand multiplier.</summary>
        public decimal ReduceHandMul
        {
            get { return reduceHandMul; }
            set { reduceHandMul = value; }
        }

        #endregion

        #region Parameter

        /// <summary>Gets or sets the equipment custom maximum HP checkBox.</summary>
        public bool CustomMaxHP
        {
            get { return customMaxHP; }
            set { customMaxHP = value; }
        }
        /// <summary>Gets or sets the equipment maximum HP multiplier.</summary>
        public decimal MaxHPMul
        {
            get { return maxHPMul; }
            set { maxHPMul = value; }
        }
        /// <summary>Gets or sets the equipment maximum HP plus.</summary>
        public int MaxHPAdd
        {
            get { return maxHPAdd; }
            set { maxHPAdd = value; }
        }

        /// <summary>Gets or sets the equipment custom maximum SP checkBox.</summary>
        public bool CustomMaxSP
        {
            get { return customMaxSP; }
            set { customMaxSP = value; }
        }
        /// <summary>Gets or sets the equipment maximum SP multiplier.</summary>
        public decimal MaxSPMul
        {
            get { return maxSPMul; }
            set { maxSPMul = value; }
        }
        /// <summary>Gets or sets the equipment maximum SP plus.</summary>
        public int MaxSPAdd
        {
            get { return maxSPAdd; }
            set { maxSPAdd = value; }
        }

        /// <summary>Gets or sets the equipment custom strengh checkBox.</summary>
        public bool CustomStr
        {
            get { return customStr; }
            set { customStr = value; }
        }
        /// <summary>Gets or sets the equipment strengh multiplier.</summary>
        public decimal StrMul
        {
            get { return strMul; }
            set { strMul = value; }
        }
        /// <summary>Gets or sets the equipment strengh plus.</summary>
        public int StrAdd
        {
            get { return strAdd; }
            set { strAdd = value; }
        }

        /// <summary>Gets or sets the equipment custom dexterity checkBox.</summary>
        public bool CustomDex
        {
            get { return customDex; }
            set { customDex = value; }
        }
        /// <summary>Gets or sets the equipment dexterity multiplier.</summary>
        public decimal DexMul
        {
            get { return dexMul; }
            set { dexMul = value; }
        }
        /// <summary>Gets or sets the equipment dexterity plus.</summary>
        public int DexAdd
        {
            get { return dexAdd; }
            set { dexAdd = value; }
        }

        /// <summary>Gets or sets the equipment custom agility checkBox.</summary>
        public bool CustomAgi
        {
            get { return customAgi; }
            set { customAgi = value; }
        }
        /// <summary>Gets or sets the equipment agility multiplier.</summary>
        public decimal AgiMul
        {
            get { return agiMul; }
            set { agiMul = value; }
        }
        /// <summary>Gets or sets the equipment agility plus.</summary>
        public int AgiAdd
        {
            get { return agiAdd; }
            set { agiAdd = value; }
        }

        /// <summary>Gets or sets the equipment custom intelligence checkBox.</summary>
        public bool CustomInt
        {
            get { return customInt; }
            set { customInt = value; }
        }
        /// <summary>Gets or sets the equipment intelligence multiplier.</summary>
        public decimal IntMul
        {
            get { return intMul; }
            set { intMul = value; }
        }
        /// <summary>Gets or sets the equipment intelligence plus.</summary>
        public int IntAdd
        {
            get { return intAdd; }
            set { intAdd = value; }
        }

        #endregion

        #region Parameter rate

        /// <summary>Gets or sets the equipment custom strengh attack rate checkBox.</summary>
        public bool CustomStrRate
        {
            get { return customStrRate; }
            set { customStrRate = value; }
        }
        /// <summary>Gets or sets the equipment strengh attack rate multiplier.</summary>
        public decimal StrRateMul
        {
            get { return strRateMul; }
            set { strRateMul = value; }
        }
        /// <summary>Gets or sets the equipment strengh attack rate plus.</summary>
        public int StrRateAdd
        {
            get { return strRateAdd; }
            set { strRateAdd = value; }
        }

        /// <summary>Gets or sets the equipment custom dexterity attack rate checkBox.</summary>
        public bool CustomDexRate
        {
            get { return customDexRate; }
            set { customDexRate = value; }
        }
        /// <summary>Gets or sets the equipment dexterity attack rate multiplier.</summary>
        public decimal DexRateMul
        {
            get { return dexRateMul; }
            set { dexRateMul = value; }
        }
        /// <summary>Gets or sets the equipment dexterity attack rate plus.</summary>
        public int DexRateAdd
        {
            get { return dexRateAdd; }
            set { dexRateAdd = value; }
        }

        /// <summary>Gets or sets the equipment custom agility attack rate checkBox.</summary>
        public bool CustomAgiRate
        {
            get { return customAgiRate; }
            set { customAgiRate = value; }
        }
        /// <summary>Gets or sets the equipment agility attack rate multiplier.</summary>
        public decimal AgiRateMul
        {
            get { return agiRateMul; }
            set { agiRateMul = value; }
        }
        /// <summary>Gets or sets the equipment agility attack rate plus.</summary>
        public int AgiRateAdd
        {
            get { return agiRateAdd; }
            set { agiRateAdd = value; }
        }

        /// <summary>Gets or sets the equipment custom intelligence attack rate checkBox.</summary>
        public bool CustomIntRate
        {
            get { return customIntRate; }
            set { customIntRate = value; }
        }
        /// <summary>Gets or sets the equipment intelligence attack rate multiplier.</summary>
        public decimal IntRateMul
        {
            get { return intRateMul; }
            set { intRateMul = value; }
        }
        /// <summary>Gets or sets the equipment intelligence attack rate plus.</summary>
        public int IntRateAdd
        {
            get { return intRateAdd; }
            set { intRateAdd = value; }
        }

        /// <summary>Gets or sets the equipment custom unarmored physical defense attack rate checkBox.</summary>
        public bool CustomPDefRate
        {
            get { return customPDefRate; }
            set { customPDefRate = value; }
        }
        /// <summary>Gets or sets the equipment unarmored physical defense attack rate multiplier.</summary>
        public decimal PDefRateMul
        {
            get { return pdefRateMul; }
            set { pdefRateMul = value; }
        }
        /// <summary>Gets or sets the equipment unarmored physical defense attack rate plus.</summary>
        public int PDefRateAdd
        {
            get { return pdefRateAdd; }
            set { pdefRateAdd = value; }
        }

        /// <summary>Gets or sets the equipment custom unarmored magical defense attack rate checkBox.</summary>
        public bool CustomMDefRate
        {
            get { return customMDefRate; }
            set { customMDefRate = value; }
        }
        /// <summary>Gets or sets the equipment unarmored magical defense attack rate multiplier.</summary>
        public decimal MDefRateMul
        {
            get { return mdefRateMul; }
            set { mdefRateMul = value; }
        }
        /// <summary>Gets or sets the equipment unarmored magical defense attack rate plus.</summary>
        public int MDefRateAdd
        {
            get { return mdefRateAdd; }
            set { mdefRateAdd = value; }
        }

        /// <summary>Gets or sets the equipment custom guard rate checkBox.</summary>
        public bool CustomGuardRate
        {
            get { return customGuardRate; }
            set { customGuardRate = value; }
        }
        /// <summary>Gets or sets the equipment guard rate multiplier.</summary>
        public decimal GuardRateMul
        {
            get { return guardRateMul; }
            set { guardRateMul = value; }
        }
        /// <summary>Gets or sets the equipment guard rate plus.</summary>
        public int GuardRateAdd
        {
            get { return guardRateAdd; }
            set { guardRateAdd = value; }
        }

        /// <summary>Gets or sets the equipment custom evasion rate checkBox.</summary>
        public bool CustomEvaRate
        {
            get { return customEvaRate; }
            set { customEvaRate = value; }
        }
        /// <summary>Gets or sets the equipment evasion rate multiplier.</summary>
        public decimal EvaRateMul
        {
            get { return evaRateMul; }
            set { evaRateMul = value; }
        }
        /// <summary>Gets or sets the equipment evasion rate plus.</summary>
        public int EvaRateAdd
        {
            get { return evaRateAdd; }
            set { evaRateAdd = value; }
        }

        #endregion

        #region Defense against attack critical

        /// <summary>Gets or sets the equipment custom defense agaisnt attack critical rate checkBox.</summary>
        public bool CustomDefCritRate
        {
            get { return customDefCritRate; }
            set { customDefCritRate = value; }
        }
        /// <summary>Gets or sets the equipment defense agaisnt attack critical rate multiplier.</summary>
        public decimal DefCritRateMul
        {
            get { return defCritRateMul; }
            set { defCritRateMul = value; }
        }
        /// <summary>Gets or sets the equipment defense agaisnt attack critical rate plus.</summary>
        public int DefCritRateAdd
        {
            get { return defCritRateAdd; }
            set { defCritRateAdd = value; }
        }

        /// <summary>Gets or sets the equipment custom defense agaisnt attack critical damage checkBox.</summary>
        public bool CustomDefCritDamage
        {
            get { return customDefCritDamage; }
            set { customDefCritDamage = value; }
        }
        /// <summary>Gets or sets the equipment defense agaisnt attack critical damage multiplier.</summary>
        public decimal DefCritDamageMul
        {
            get { return defCritDamageMul; }
            set { defCritDamageMul = value; }
        }
        /// <summary>Gets or sets the equipment defense agaisnt attack critical damage plus.</summary>
        public int DefCritDamageAdd
        {
            get { return defCritDamageAdd; }
            set { defCritDamageAdd = value; }
        }

        /// <summary>Gets or sets the equipment custom defense agaisnt attack special critical rate checkBox.</summary>
        public bool CustomDefSpCritRate
        {
            get { return customDefSpCritRate; }
            set { customDefSpCritRate = value; }
        }
        /// <summary>Gets or sets the equipment defense agaisnt attack special critical rate multiplier.</summary>
        public decimal DefSpCritRateMul
        {
            get { return defSpCritRateMul; }
            set { defSpCritRateMul = value; }
        }
        /// <summary>Gets or sets the equipment defense agaisnt attack special critical rate plus.</summary>
        public int DefSpCritRateAdd
        {
            get { return defSpCritRateAdd; }
            set { defSpCritRateAdd = value; }
        }

        /// <summary>Gets or sets the equipment custom defense agaisnt attack special critical damage checkBox.</summary>
        public bool CustomDefSpCritDamage
        {
            get { return customDefSpCritDamage; }
            set { customDefSpCritDamage = value; }
        }
        /// <summary>Gets or sets the equipment defense agaisnt attack special critical damage multiplier.</summary>
        public decimal DefSpCritDamageMul
        {
            get { return defSpCritDamageMul; }
            set { defSpCritDamageMul = value; }
        }
        /// <summary>Gets or sets the equipment defense agaisnt attack special critical damage plus.</summary>
        public int DefSpCritDamageAdd
        {
            get { return defSpCritDamageAdd; }
            set { defSpCritDamageAdd = value; }
        }

        #endregion

        #region Defense against skill critical

        /// <summary>Gets or sets the equipment custom defense agaisnt skill critical rate checkBox.</summary>
        public bool CustomDefSkillCritRate
        {
            get { return customDefSkillCritRate; }
            set { customDefSkillCritRate = value; }
        }
        /// <summary>Gets or sets the equipment defense agaisnt skill critical rate multiplier.</summary>
        public decimal DefSkillCritRateMul
        {
            get { return defSkillCritRateMul; }
            set { defSkillCritRateMul = value; }
        }
        /// <summary>Gets or sets the equipment defense agaisnt skill critical rate plus.</summary>
        public int DefSkillCritRateAdd
        {
            get { return defSkillCritRateAdd; }
            set { defSkillCritRateAdd = value; }
        }

        /// <summary>Gets or sets the equipment custom defense agaisnt skill critical damage checkBox.</summary>
        public bool CustomDefSkillCritDamage
        {
            get { return customDefSkillCritDamage; }
            set { customDefSkillCritDamage = value; }
        }
        /// <summary>Gets or sets the equipment defense agaisnt skill critical damage multiplier.</summary>
        public decimal DefSkillCritDamageMul
        {
            get { return defSkillCritDamageMul; }
            set { defSkillCritDamageMul = value; }
        }
        /// <summary>Gets or sets the equipment defense agaisnt skill critical damage plus.</summary>
        public int DefSkillCritDamageAdd
        {
            get { return defSkillCritDamageAdd; }
            set { defSkillCritDamageAdd = value; }
        }

        /// <summary>Gets or sets the equipment custom defense agaisnt skill special critical rate checkBox.</summary>
        public bool CustomDefSkillSpCritRate
        {
            get { return customDefSkillSpCritRate; }
            set { customDefSkillSpCritRate = value; }
        }
        /// <summary>Gets or sets the equipment defense agaisnt skill special critical rate multiplier.</summary>
        public decimal DefSkillSpCritRateMul
        {
            get { return defSkillSpCritRateMul; }
            set { defSkillSpCritRateMul = value; }
        }
        /// <summary>Gets or sets the equipment defense agaisnt skill special critical rate plus.</summary>
        public int DefSkillSpCritRateAdd
        {
            get { return defSkillSpCritRateAdd; }
            set { defSkillSpCritRateAdd = value; }
        }

        /// <summary>Gets or sets the equipment custom defense agaisnt skill special critical damage checkBox.</summary>
        public bool CustomDefSkillSpCritDamage
        {
            get { return customDefSkillSpCritDamage; }
            set { customDefSkillSpCritDamage = value; }
        }
        /// <summary>Gets or sets the equipment defense agaisnt skill special critical damage multiplier.</summary>
        public decimal DefSkillSpCritDamageMul
        {
            get { return defSkillSpCritDamageMul; }
            set { defSkillSpCritDamageMul = value; }
        }
        /// <summary>Gets or sets the equipment defense agaisnt skill special critical damage plus.</summary>
        public int DefSkillSpCritDamageAdd
        {
            get { return defSkillSpCritDamageAdd; }
            set { defSkillSpCritDamageAdd = value; }
        }

        #endregion

        #region Passive attack

        /// <summary>Gets or sets the equipment custom passive attack checkBox.</summary>
        public bool CustomPassiveAtk
        {
            get { return customPassiveAtk; }
            set { customPassiveAtk = value; }
        }
        /// <summary>Gets or sets the equipment passive attack multiplier.</summary>
        public decimal AtkMul
        {
            get { return atkMul; }
            set { atkMul = value; }
        }
        /// <summary>Gets or sets the equipment passive attack plus.</summary>
        public int AtkAdd
        {
            get { return atkAdd; }
            set { atkAdd = value; }
        }

        /// <summary>Gets or sets the equipment custom passive hit rate checkBox.</summary>
        public bool CustomPassiveHit
        {
            get { return customPassiveHit; }
            set { customPassiveHit = value; }
        }
        /// <summary>Gets or sets the equipment passive hit rate multiplier.</summary>
        public decimal HitMul
        {
            get { return hitMul; }
            set { hitMul = value; }
        }
        /// <summary>Gets or sets the equipment passive hit rate plus.</summary>
        public int HitAdd
        {
            get { return hitAdd; }
            set { hitAdd = value; }
        }

        #endregion

        #region Passive critical

        /// <summary>Gets or sets the equipment custom passive critical rate checkBox.</summary>
        public bool CustomPassiveCritRate
        {
            get { return customPassiveCritRate; }
            set { customPassiveCritRate = value; }
        }
        /// <summary>Gets or sets the equipment passive critical rate multiplier.</summary>
        public decimal CritRateMul
        {
            get { return critRateMul; }
            set { critRateMul = value; }
        }
        /// <summary>Gets or sets the equipment passive critical rate plus.</summary>
        public int CritRateAdd
        {
            get { return critRateAdd; }
            set { critRateAdd = value; }
        }

        /// <summary>Gets or sets the equipment custom passive critical damage checkBox.</summary>
        public bool CustomPassiveCritDamage
        {
            get { return customPassiveCritDamage; }
            set { customPassiveCritDamage = value; }
        }
        /// <summary>Gets or sets the equipment passive critical damage multiplier.</summary>
        public decimal CritDamageMul
        {
            get { return critDamageMul; }
            set { critDamageMul = value; }
        }
        /// <summary>Gets or sets the equipment passive critical damage plus.</summary>
        public int CritDamageAdd
        {
            get { return critDamageAdd; }
            set { critDamageAdd = value; }
        }

        /// <summary>Gets or sets the equipment custom passive critical guard rate reduction checkBox.</summary>
        public bool CustomPassiveCritDefGuard
        {
            get { return customPassiveCritDefGuard; }
            set { customPassiveCritDefGuard = value; }
        }
        /// <summary>Gets or sets the equipment passive critical guard rate reduction multiplier.</summary>
        public decimal CritDefGuardMul
        {
            get { return critDefGuardMul; }
            set { critDefGuardMul = value; }
        }
        /// <summary>Gets or sets the equipment passive critical guard rate reduction plus.</summary>
        public int CritDefGuardAdd
        {
            get { return critDefGuardAdd; }
            set { critDefGuardAdd = value; }
        }

        /// <summary>Gets or sets the equipment custom passive critical evasion rate reduction checkBox.</summary>
        public bool CustomPassiveCritDefEva
        {
            get { return customPassiveCritDefEva; }
            set { customPassiveCritDefEva = value; }
        }
        /// <summary>Gets or sets the equipment passive critical evasion rate reduction multiplier.</summary>
        public decimal CritDefEvaMul
        {
            get { return critDefEvaMul; }
            set { critDefEvaMul = value; }
        }
        /// <summary>Gets or sets the equipment passive critical evasion rate reduction plus.</summary>
        public int CritDefEvaAdd
        {
            get { return critDefEvaAdd; }
            set { critDefEvaAdd = value; }
        }

        #endregion

        #region Passive special critical

        /// <summary>Gets or sets the equipment custom passive critical rate checkBox.</summary>
        public bool CustomPassiveSpCritRate
        {
            get { return customPassiveSpCritRate; }
            set { customPassiveSpCritRate = value; }
        }
        /// <summary>Gets or sets the equipment passive critical rate multiplier.</summary>
        public decimal SpCritRateMul
        {
            get { return spCritRateMul; }
            set { spCritRateMul = value; }
        }
        /// <summary>Gets or sets the equipment passive critical rate plus.</summary>
        public int SpCritRateAdd
        {
            get { return spCritRateAdd; }
            set { spCritRateAdd = value; }
        }

        /// <summary>Gets or sets the equipment custom passive special critical damage checkBox.</summary>
        public bool CustomPassiveSpCritDamage
        {
            get { return customPassiveSpCritDamage; }
            set { customPassiveSpCritDamage = value; }
        }
        /// <summary>Gets or sets the equipment passive special critical damage multiplier.</summary>
        public decimal SpCritDamageMul
        {
            get { return spCritDamageMul; }
            set { spCritDamageMul = value; }
        }
        /// <summary>Gets or sets the equipment passive special critical damage plus.</summary>
        public int SpCritDamageAdd
        {
            get { return spCritDamageAdd; }
            set { spCritDamageAdd = value; }
        }

        /// <summary>Gets or sets the equipment custom passive special critical guard rate reduction checkBox.</summary>
        public bool CustomPassiveSpCritDefGuard
        {
            get { return customPassiveSpCritDefGuard; }
            set { customPassiveSpCritDefGuard = value; }
        }
        /// <summary>Gets or sets the equipment passive special critical guard rate reduction multiplier.</summary>
        public decimal SpCritDefGuardMul
        {
            get { return spCritDefGuardMul; }
            set { spCritDefGuardMul = value; }
        }
        /// <summary>Gets or sets the equipment passive special critical guard rate reduction plus.</summary>
        public int SpCritDefGuardAdd
        {
            get { return spCritDefGuardAdd; }
            set { spCritDefGuardAdd = value; }
        }

        /// <summary>Gets or sets the equipment custom passive special critical evasion rate reduction checkBox.</summary>
        public bool CustomPassiveSpCritDefEva
        {
            get { return customPassiveSpCritDefEva; }
            set { customPassiveSpCritDefEva = value; }
        }
        /// <summary>Gets or sets the equipment passive special critical evasion rate reduction multiplier.</summary>
        public decimal SpCritDefEvaMul
        {
            get { return spCritDefEvaMul; }
            set { spCritDefEvaMul = value; }
        }
        /// <summary>Gets or sets the equipment passive special critical evasion rate reduction plus.</summary>
        public int SpCritDefEvaAdd
        {
            get { return spCritDefEvaAdd; }
            set { spCritDefEvaAdd = value; }
        }

        #endregion

        #region Passive defense

        /// <summary>Gets or sets the equipment custom physical defense checkBox.</summary>
        public bool CustomPassivePDef
        {
            get { return customPassivePDef; }
            set { customPassivePDef = value; }
        }
        /// <summary>Gets or sets the equipment physical defense multiplier.</summary>
        public decimal PDefMul
        {
            get { return pdefMul; }
            set { pdefMul = value; }
        }
        /// <summary>Gets or sets the equipment physical defense plus.</summary>
        public int PDefAdd
        {
            get { return pdefAdd; }
            set { pdefAdd = value; }
        }

        /// <summary>Gets or sets the equipment custom magical defense checkBox.</summary>
        public bool CustomPassiveMDef
        {
            get { return customPassiveMDef; }
            set { customPassiveMDef = value; }
        }
        /// <summary>Gets or sets the equipment magical defense multiplier.</summary>
        public decimal MDefMul
        {
            get { return mdefMul; }
            set { mdefMul = value; }
        }
        /// <summary>Gets or sets the equipment magical defense plus.</summary>
        public int MDefAdd
        {
            get { return mdefAdd; }
            set { mdefAdd = value; }
        }

        #endregion

        #region Unarmed Attack

        /// <summary>Gets or sets the actor or class custom unarmed attack checkBox.</summary>
        public bool CustomUnarmedAtk
        {
            get { return customUnarmedAtk; }
            set { customUnarmedAtk = value; }
        }

        /// <summary>Gets or sets the actor or class unarmed attack Initial.</summary>
        public int AtkInitial
        {
            get { return atkInitial; }
            set { atkInitial = value; }
        }

        /// <summary>Gets or sets the actor or class unarmed attack Final.</summary>
        public int AtkFinal
        {
            get { return atkFinal; }
            set { atkFinal = value; }
        }

        /// <summary>Gets or sets the actor or class custom unarmed hit rate checkBox.</summary>
        public bool CustomUnarmedHit
        {
            get { return customUnarmedHit; }
            set { customUnarmedHit = value; }
        }

        /// <summary>Gets or sets the actor or class unarmed hit rate Initial.</summary>
        public decimal HitInitial
        {
            get { return hitInitial; }
            set { hitInitial = value; }
        }

        /// <summary>Gets or sets the actor or class unarmed hit rate Final.</summary>
        public decimal HitFinal
        {
            get { return hitFinal; }
            set { hitFinal = value; }
        }

        /// <summary>Gets or sets the actor or class custom unarmed hit rate checkBox.</summary>
        public bool CustomUnarmedAnim
        {
            get { return customUnarmedAnim; }
            set { customUnarmedAnim = value; }
        }

        /// <summary>Gets or sets the actor or class unarmed caster attack animation.</summary>
        public int AnimCaster
        {
            get { return animCaster; }
            set { animCaster = value; }
        }

        /// <summary>Gets or sets the actor or class unarmed target attack animation.</summary>
        public int AnimTarget
        {
            get { return animTarget; }
            set { animTarget = value; }
        }

        /// <summary>Gets or sets the actor or class custom parameter attack force checkBox.</summary>
        public bool CustomUnarmedParamForce
        {
            get { return customUnarmedParamForce; }
            set { customUnarmedParamForce = value; }
        }

        /// <summary>Gets or sets the actor or class strengh attack force.</summary>
        public decimal StrForce
        {
            get { return strForce; }
            set { strForce = value; }
        }

        /// <summary>Gets or sets the actor or class dexterity attack force.</summary>
        public decimal DexForce
        {
            get { return dexForce; }
            set { dexForce = value; }
        }

        /// <summary>Gets or sets the actor or class agility attack force.</summary>
        public decimal AgiForce
        {
            get { return agiForce; }
            set { agiForce = value; }
        }

        /// <summary>Gets or sets the actor or class intelligence attack force.</summary>
        public decimal IntForce
        {
            get { return intForce; }
            set { intForce = value; }
        }

        /// <summary>Gets or sets the actor or class custom unarmoured defense attack force checkBox.</summary>
        public bool CustomUnarmedDefenseForce
        {
            get { return customUnarmedDefenseForce; }
            set { customUnarmedDefenseForce = value; }
        }

        /// <summary>Gets or sets the actor or class unarmoured physical defense attack force.</summary>
        public decimal PDefForce
        {
            get { return pdefForce; }
            set { pdefForce = value; }
        }

        /// <summary>Gets or sets the actor or class unarmoured magical defense attack force.</summary>
        public decimal MDefForce
        {
            get { return mdefForce; }
            set { mdefForce = value; }
        }

        #endregion

        #region Unarmed critical

        /// <summary>Gets or sets the actor or class custom unarmed critical rate checkBox.</summary>
        public bool CustomUnarmedCritRate
        {
            get { return customUnarmedCritRate; }
            set { customUnarmedCritRate = value; }
        }

        /// <summary>Gets or sets the actor or class unarmed critical rate.</summary>
        public decimal CritRate
        {
            get { return critRate; }
            set { critRate = value; }
        }

        /// <summary>Gets or sets the actor or class custom unarmed critical damage checkBox.</summary>
        public bool CustomUnarmedCritDamage
        {
            get { return customUnarmedCritDamage; }
            set { customUnarmedCritDamage = value; }
        }

        /// <summary>Gets or sets the actor or class unarmed critical damage.</summary>
        public decimal CritDamage
        {
            get { return critDamage; }
            set { critDamage = value; }
        }

        /// <summary>Gets or sets the actor or class custom unarmed critical guard rate reduction checkBox.</summary>
        public bool CustomUnarmedCritDefGuard
        {
            get { return customUnarmedCritDefGuard; }
            set { customUnarmedCritDefGuard = value; }
        }

        /// <summary>Gets or sets the actor or class unarmed critical guard rate reduction.</summary>
        public decimal CritDefGuard
        {
            get { return critDefGuard; }
            set { critDefGuard = value; }
        }

        /// <summary>Gets or sets the actor or class custom unarmed critical evasion rate reduction checkBox.</summary>
        public bool CustomUnarmedCritDefEva
        {
            get { return customUnarmedCritDefEva; }
            set { customUnarmedCritDefEva = value; }
        }

        /// <summary>Gets or sets the actor or class unarmed critical evasion rate reduction.</summary>
        public decimal CritDefEva
        {
            get { return critDefEva; }
            set { critDefEva = value; }
        }

        #endregion

        #region Unarmed Special Critical

        /// <summary>Gets or sets the actor or class custom unarmed critical rate checkBox.</summary>
        public bool CustomUnarmedSpCritRate
        {
            get { return customUnarmedSpCritRate; }
            set { customUnarmedSpCritRate = value; }
        }

        /// <summary>Gets or sets the actor or class unarmed critical rate.</summary>
        public decimal SpCritRate
        {
            get { return spCritRate; }
            set { spCritRate = value; }
        }

        /// <summary>Gets or sets the actor or class custom unarmed special critical damage checkBox.</summary>
        public bool CustomUnarmedSpCritDamage
        {
            get { return customUnarmedSpCritDamage; }
            set { customUnarmedSpCritDamage = value; }
        }

        /// <summary>Gets or sets the actor or class unarmed special critical damage.</summary>
        public decimal SpCritDamage
        {
            get { return spCritDamage; }
            set { spCritDamage = value; }
        }

        /// <summary>Gets or sets the actor or class custom unarmed special critical guard rate reduction checkBox.</summary>
        public bool CustomUnarmedSpCritDefGuard
        {
            get { return customUnarmedSpCritDefGuard; }
            set { customUnarmedSpCritDefGuard = value; }
        }

        /// <summary>Gets or sets the actor or class unarmed special critical guard rate reduction.</summary>
        public decimal SpCritDefGuard
        {
            get { return spCritDefGuard; }
            set { spCritDefGuard = value; }
        }

        /// <summary>Gets or sets the actor or class custom unarmed special critical evasion rate reduction checkBox.</summary>
        public bool CustomUnarmedSpCritDefEva
        {
            get { return customUnarmedSpCritDefEva; }
            set { customUnarmedSpCritDefEva = value; }
        }

        /// <summary>Gets or sets the actor or class unarmed special critical evasion rate reduction.</summary>
        public decimal SpCritDefEva
        {
            get { return spCritDefEva; }
            set { spCritDefEva = value; }
        }

        #endregion

        #region Unarmoured Defense

        /// <summary>Gets or sets the actor or class custom unarmoured physical defense checkBox.</summary>
        public bool CustomUnarmouredPDef
        {
            get { return customUnarmouredPDef; }
            set { customUnarmouredPDef = value; }
        }

        /// <summary>Gets or sets the actor or class unarmoured physical defense Initial.</summary>
        public int PDefInitial
        {
            get { return pdefInitial; }
            set { pdefInitial = value; }
        }

        /// <summary>Gets or sets the actor or class unarmoured physical defense Final.</summary>
        public int PDefFinal
        {
            get { return pdefFinal; }
            set { pdefFinal = value; }
        }

        /// <summary>Gets or sets the actor or class custom unarmoured magical defense checkBox.</summary>
        public bool CustomUnarmouredMDef
        {
            get { return customUnarmouredMDef; }
            set { customUnarmouredMDef = value; }
        }

        /// <summary>Gets or sets the actor or class unarmoured magical defense Initial.</summary>
        public int MDefInitial
        {
            get { return mdefInitial; }
            set { mdefInitial = value; }
        }

        /// <summary>Gets or sets the actor or class unarmoured magical defense Final.</summary>
        public int MDefFinal
        {
            get { return mdefFinal; }
            set { mdefFinal = value; }
        }

        #endregion

        #endregion

        #region Behavior

        public DataPackClass()
        {
            this.id = 0;
            this.name = "";
            this.Reset();
        }

        public DataPackClass(int inpuntID)
        {
            this.id = inpuntID;
            this.name = defaultName;
            this.Reset();
        }

        public DataPackClass(int inpuntID, string inputName)
        {
            this.id = inpuntID;
            this.name = inputName;
            this.Reset();
        }

        /// <summary>Resets the configuration to default.</summary>
        public void Reset()
        {
            // In Family
            this.classFamily.Clear();

            #region Equipment

            // Equipment list
            this.customEquip = defaultCustomEquip;
            this.equipType.Clear();
            this.equipList.Clear();

            // Dual hold
            this.dualHold = defaultDualHold;

            // Dual hold name
            this.customDualHoldName = defaultCustomDualHoldName;
            this.dualHoldNameWeapon = defaultDualHoldNameWeapon;
            this.dualHoldNameShield = defaultDualHoldNameShield;

            // Dual hold multiplier
            this.customDualHoldMul = defaultCustomDualHoldMul;
            this.dualHoldMulWeapon = defaultDualHoldMulWeapon;
            this.dualHoldMulShield = defaultDualHoldMulShield;

            // Shield bypass
            this.shieldBypass = defaultShieldBypass;
            this.shieldBypassMul = defaultShieldBypassMul;

            // Weapon bypass
            this.weaponBypass = defaultWeaponBypass;
            this.weaponBypassMul = defaultWeaponBypassMul;

            // Reduce hand
            this.customReduceHand = defaultCustomReduceHand;
            this.reduceHand = defaultReduceHand;
            this.reduceHandMul = defaultReduceHandMul;

            #endregion

            #region Parameter

            // Maximum HP
            this.customMaxHP = defaultCustomMaxHP;
            this.maxHPMul = defaultMaxHPMul;
            this.maxHPAdd = defaultMaxHPAdd;

            // Maximum SP
            this.customMaxSP = defaultCustomMaxSP;
            this.maxSPMul = defaultMaxSPMul;
            this.maxSPAdd = defaultMaxSPAdd;

            // Strengh
            this.customStr = defaultCustomStr;
            this.strMul = defaultStrMul;
            this.strAdd = defaultStrAdd;

            // Dexterity
            this.customDex = defaultCustomDex;
            this.dexMul = defaultDexMul;
            this.dexAdd = defaultDexAdd;

            // Agility
            this.customAgi = defaultCustomAgi;
            this.agiMul = defaultAgiMul;
            this.agiAdd = defaultAgiAdd;

            // Intelligence
            this.customInt = defaultCustomInt;
            this.intMul = defaultIntMul;
            this.intAdd = defaultIntAdd;

            #endregion

            #region Parameter rate

            // Strengh attack rate
            this.customStrRate = defaultCustomStrRate;
            this.strRateMul = defaultStrRateMul;
            this.strRateAdd = defaultStrRateAdd;

            // Dexterity attack rate
            this.customDexRate = defaultCustomDexRate;
            this.dexRateMul = defaultDexRateMul;
            this.dexRateAdd = defaultDexRateAdd;

            // Agility attack rate
            this.customAgiRate = defaultCustomAgiRate;
            this.agiRateMul = defaultAgiRateMul;
            this.agiRateAdd = defaultAgiRateAdd;

            // Intelligence attack rate
            this.customIntRate = defaultCustomIntRate;
            this.intRateMul = defaultIntRateMul;
            this.intRateAdd = defaultIntRateAdd;

            // Physical defense attack rate
            this.customPDefRate = defaultCustomPDefRate;
            this.pdefRateMul = defaultPDefRateMul;
            this.pdefRateAdd = defaultPDefRateAdd;

            // Magical defense attack rate
            this.customMDefRate = defaultCustomMDefRate;
            this.mdefRateMul = defaultMDefRateMul;
            this.mdefRateAdd = defaultMDefRateAdd;

            // Guard rate
            this.customGuardRate = defaultCustomGuardRate;
            this.guardRateMul = defaultGuardRateMul;
            this.guardRateAdd = defaultGuardRateAdd;

            // Evasion rate
            this.customEvaRate = defaultCustomEvaRate;
            this.evaRateMul = defaultEvaRateMul;
            this.evaRateAdd = defaultEvaRateAdd;

            #endregion

            #region Defense against attack critical

            // Defense against attack critical rate
            this.customDefCritRate = defaultCustomDefCritRate;
            this.defCritRateMul = defaultDefCritRateMul;
            this.defCritRateAdd = defaultDefCritRateAdd;

            // Defense against attack critical damage
            this.customDefCritDamage = defaultCustomDefCritDamage;
            this.defCritDamageMul = defaultDefCritDamageMul;
            this.defCritDamageAdd = defaultDefCritDamageAdd;

            // Defense against attack special critical rate
            this.customDefSpCritRate = defaultCustomDefSpCritRate;
            this.defSpCritRateMul = defaultDefSpCritRateMul;
            this.defSpCritRateAdd = defaultDefSpCritRateAdd;

            // Defense against attack special critical damage
            this.customDefSpCritDamage = defaultCustomDefSpCritDamage;
            this.defSpCritDamageMul = defaultDefSpCritDamageMul;
            this.defSpCritDamageAdd = defaultDefSpCritDamageAdd;

            #endregion

            #region Defense against skill critical

            // Defense against skill critical rate
            this.customDefSkillCritRate = defaultCustomDefSkillCritRate;
            this.defSkillCritRateMul = defaultDefSkillCritRateMul;
            this.defSkillCritRateAdd = defaultDefSkillCritRateAdd;

            // Defense against skill critical damage
            this.customDefSkillCritDamage = defaultCustomDefSkillCritDamage;
            this.defSkillCritDamageMul = defaultDefSkillCritDamageMul;
            this.defSkillCritDamageAdd = defaultDefSkillCritDamageAdd;

            // Defense against skill special critical rate
            this.customDefSkillSpCritRate = defaultCustomDefSkillSpCritRate;
            this.defSkillSpCritRateMul = defaultDefSkillSpCritRateMul;
            this.defSkillSpCritRateAdd = defaultDefSkillSpCritRateAdd;

            // Defense against skill special critical damage
            this.customDefSkillSpCritDamage = defaultCustomDefSkillSpCritDamage;
            this.defSkillSpCritDamageMul = defaultDefSkillSpCritDamageMul;
            this.defSkillSpCritDamageAdd = defaultDefSkillSpCritDamageAdd;

            #endregion

            #region Passive attack

            // Attack
            this.customPassiveAtk = defaultCustomPassiveAtk;
            this.atkMul = defaultAtkMul;
            this.atkAdd = defaultAtkAdd;

            // Hit Rate
            this.customPassiveHit = defaultCustomPassiveHit;
            this.hitMul = defaultHitMul;
            this.hitAdd = defaultHitAdd;

            #endregion

            #region Passive critical

            // Critical rate
            this.customPassiveCritRate = defaultCustomPassiveCritRate;
            this.critRateMul = defaultCritRateMul;
            this.critRateAdd = defaultCritRateAdd;

            // Critical damage
            this.customPassiveCritDamage = defaultCustomPassiveCritDamage;
            this.critDamageMul = defaultCritDamageMul;
            this.critDamageAdd = defaultCritDamageAdd;

            // Critical guard reduction
            this.customPassiveCritDefGuard = defaultCustomPassiveCritDefGuard;
            this.critDefGuardMul = defaultCritDefGuardMul;
            this.critDefGuardAdd = defaultCritDefGuardAdd;

            // Critical evasion reduction
            this.customPassiveCritDefEva = defaultCustomPassiveCritDefEva;
            this.critDefEvaMul = defaultCritDefEvaMul;
            this.critDefEvaAdd = defaultCritDefEvaAdd;

            #endregion

            #region Passive special critical

            // Special critical rate
            this.customPassiveSpCritRate = defaultCustomPassiveSpCritRate;
            this.spCritRateMul = defaultSpCritRateMul;
            this.spCritRateAdd = defaultSpCritRateAdd;

            // Special critical damage
            this.customPassiveSpCritDamage = defaultCustomPassiveSpCritDamage;
            this.spCritDamageMul = defaultSpCritDamageMul;
            this.spCritDamageAdd = defaultSpCritDamageAdd;

            // Special critical guard reduction
            this.customPassiveSpCritDefGuard = defaultCustomPassiveSpCritDefGuard;
            this.spCritDefGuardMul = defaultSpCritDefGuardMul;
            this.spCritDefGuardAdd = defaultSpCritDefGuardAdd;

            // Special critical evasion reduction
            this.customPassiveSpCritDefEva = defaultCustomPassiveSpCritDefEva;
            this.spCritDefEvaMul = defaultSpCritDefEvaMul;
            this.spCritDefEvaAdd = defaultSpCritDefEvaAdd;

            #endregion

            #region Passive defense

            // Physical defense
            this.customPassivePDef = defaultCustomPassivePDef;
            this.pdefMul = defaultPDefMul;
            this.pdefAdd = defaultPDefAdd;

            // Magical defense
            this.customPassiveMDef = defaultCustomPassiveMDef;
            this.mdefMul = defaultMDefMul;
            this.mdefAdd = defaultMDefAdd;

            #endregion

            #region Unarmed attack

            // Attack
            this.customUnarmedAtk = defaultCustomUnarmedAtk;
            this.atkInitial = defaultAtkInitial;
            this.atkFinal = defaultAtkFinal;

            // Hit Rate
            this.customUnarmedHit = defaultCustomUnarmedHit;
            this.hitInitial = defaultHitInitial;
            this.hitFinal = defaultHitFinal;

            // Attack animation
            this.customUnarmedAnim = defaultCustomUnarmedAnim;
            this.animCaster = defaultAnimCaster;
            this.animTarget = defaultAnimTarget;

            // Parameter attack force
            this.customUnarmedParamForce = defaultCustomUnarmedParamForce;
            this.strForce = defaultStrForce;
            this.dexForce = defaultDexForce;
            this.agiForce = defaultAgiForce;
            this.intForce = defaultIntForce;

            // Defense attack force
            this.customUnarmedDefenseForce = defaultCustomUnarmedDefenseForce;
            this.pdefForce = defaultPDefForce;
            this.mdefForce = defaultMDefForce;

            #endregion

            #region Unarmed critical

            // Critical rate
            this.customUnarmedCritRate = defaultCustomUnarmedCritRate;
            this.critRate = defaultCritRate;

            // Critical damage
            this.customUnarmedCritDamage = defaultCustomUnarmedCritDamage;
            this.critDamage = defaultCritDamage;

            // Critical guard reduction
            this.customUnarmedCritDefGuard = defaultCustomUnarmedCritDefGuard;
            this.critDefGuard = defaultCritDefGuard;

            // Critical evasion reduction
            this.customUnarmedCritDefEva = defaultCustomUnarmedCritDefEva;
            this.critDefEva = defaultCritDefEva;

            #endregion

            #region Unarmed special critical

            // Special critical rate
            this.customUnarmedSpCritRate = defaultCustomUnarmedSpCritRate;
            this.spCritRate = defaultSpCritRate;

            // Special critical damage
            this.customUnarmedSpCritDamage = defaultCustomUnarmedSpCritDamage;
            this.spCritDamage = defaultSpCritDamage;

            // Special critical guard reduction
            this.customUnarmedSpCritDefGuard = defaultCustomUnarmedSpCritDefGuard;
            this.spCritDefGuard = defaultSpCritDefGuard;

            // Special critical evasion reduction
            this.customUnarmedSpCritDefEva = defaultCustomUnarmedSpCritDefEva;
            this.spCritDefEva = defaultSpCritDefEva;

            #endregion

            #region Unarmoured Defense

            // Physical defense
            this.customUnarmouredPDef = defaultCustomUnarmouredPDef;
            this.pdefInitial = defaultPDefInitial;
            this.pdefFinal = defaultPDefFinal;

            // Magical defense
            this.customUnarmouredMDef = defaultCustomUnarmouredMDef;
            this.mdefInitial = defaultMDefInitial;
            this.mdefFinal = defaultMDefFinal;

            #endregion
        }

        /// <summary>Compares configurations</summary>
        /// <param name="other">Other configuration of comparison</param>
        /// <returns>True or false.</returns>
        public bool Equals(DataPackClass other)
        {
            if (!this.Equals(other)) return false;

            // ID and name
            if (this.ID != other.ID) return false;
            if (this.Name != other.Name) return false;
            
            // In Family
            if (!this.ClassFamily.Equals(other.ClassFamily)) return false;

            #region Equipment

            // Equipment list
            if (this.CustomEquip != other.CustomEquip) return false;
            if (!this.EquipType.Equals(other.EquipType)) return false;
            if (!this.EquipList.Equals(other.EquipList)) return false;

            // Dual hold
            if (this.DualHold != other.DualHold) return false;

            // Dual hold name
            if (this.CustomDualHoldName != other.CustomDualHoldName) return false;
            if (this.DualHoldNameWeapon != other.DualHoldNameWeapon) return false;
            if (this.DualHoldNameShield != other.DualHoldNameShield) return false;

            // Dual hold multiplier
            if (this.CustomDualHoldMul != other.CustomDualHoldMul) return false;
            if (this.DualHoldMulWeapon != other.DualHoldMulWeapon) return false;
            if (this.DualHoldMulShield != other.DualHoldMulShield) return false;

            // Shield bypass
            if (this.ShieldBypass != other.ShieldBypass) return false;
            if (this.ShieldBypassMul != other.ShieldBypassMul) return false;

            // Weapon bypass
            if (this.WeaponBypass != other.WeaponBypass) return false;
            if (this.WeaponBypassMul != other.WeaponBypassMul) return false;

            // Reduce hand
            if (this.CustomReduceHand != other.CustomReduceHand) return false;
            if (this.ReduceHand != other.ReduceHand) return false;
            if (this.ReduceHandMul != other.ReduceHandMul) return false;

            #endregion

            #region Parameter

            // Maximum HP
            if (this.CustomMaxHP != other.CustomMaxHP) return false;
            if (this.MaxHPMul != other.MaxHPMul) return false;
            if (this.MaxHPAdd != other.MaxHPAdd) return false;

            // Maximum SP
            if (this.CustomMaxSP != other.CustomMaxSP) return false;
            if (this.MaxSPMul != other.MaxSPMul) return false;
            if (this.MaxSPAdd != other.MaxSPAdd) return false;

            // Strengh
            if (this.CustomStr != other.CustomStr) return false;
            if (this.StrMul != other.StrMul) return false;
            if (this.StrAdd != other.StrAdd) return false;

            // Dexterity
            if (this.CustomDex != other.CustomDex) return false;
            if (this.DexMul != other.DexMul) return false;
            if (this.DexAdd != other.DexAdd) return false;

            // Agility
            if (this.CustomAgi != other.CustomAgi) return false;
            if (this.AgiMul != other.AgiMul) return false;
            if (this.AgiAdd != other.AgiAdd) return false;

            // Intelligence
            if (this.CustomInt != other.CustomInt) return false;
            if (this.IntMul != other.IntMul) return false;
            if (this.IntAdd != other.IntAdd) return false;

            #endregion

            #region Parameter rate

            // Strengh attack rate
            if (this.CustomStrRate != other.CustomStrRate) return false;
            if (this.StrRateMul != other.StrRateMul) return false;
            if (this.StrRateAdd != other.StrRateAdd) return false;

            // Dexterity attack rate
            if (this.CustomDexRate != other.CustomDexRate) return false;
            if (this.DexRateMul != other.DexRateMul) return false;
            if (this.DexRateAdd != other.DexRateAdd) return false;

            // Agility attackrate
            if (this.CustomAgiRate != other.CustomAgiRate) return false;
            if (this.AgiRateMul != other.AgiRateMul) return false;
            if (this.AgiRateAdd != other.AgiRateAdd) return false;

            // Intelligence attack rate
            if (this.CustomIntRate != other.CustomIntRate) return false;
            if (this.IntRateMul != other.IntRateMul) return false;
            if (this.IntRateAdd != other.IntRateAdd) return false;

            // Physical defense attack rate
            if (this.CustomPDefRate != other.CustomPDefRate) return false;
            if (this.PDefRateMul != other.PDefRateMul) return false;
            if (this.PDefRateAdd != other.PDefRateAdd) return false;

            // Magical defense attack rate
            if (this.CustomMDefRate != other.CustomMDefRate) return false;
            if (this.MDefRateMul != other.MDefRateMul) return false;
            if (this.MDefRateAdd != other.MDefRateAdd) return false;

            // Guard rate
            if (this.CustomGuardRate != other.CustomGuardRate) return false;
            if (this.GuardRateMul != other.GuardRateMul) return false;
            if (this.GuardRateAdd != other.GuardRateAdd) return false;

            // Evasion rate
            if (this.CustomEvaRate != other.CustomEvaRate) return false;
            if (this.EvaRateMul != other.EvaRateMul) return false;
            if (this.EvaRateAdd != other.EvaRateAdd) return false;

            #endregion

            #region Defense against attack critical

            // Defense against attack critical rate
            if (this.CustomDefCritRate != other.CustomDefCritRate) return false;
            if (this.DefCritRateMul != other.DefCritRateMul) return false;
            if (this.DefCritRateAdd != other.DefCritRateAdd) return false;

            // Defense against attack critical damage
            if (this.CustomDefCritDamage != other.CustomDefCritDamage) return false;
            if (this.DefCritDamageMul != other.DefCritDamageMul) return false;
            if (this.DefCritDamageAdd != other.DefCritDamageAdd) return false;

            // Defense against attack special critical rate
            if (this.CustomDefSpCritRate != other.CustomDefSpCritRate) return false;
            if (this.DefSpCritRateMul != other.DefSpCritRateMul) return false;
            if (this.DefSpCritRateAdd != other.DefSpCritRateAdd) return false;

            // Defense against attack special critical damage
            if (this.CustomDefSpCritDamage != other.CustomDefSpCritDamage) return false;
            if (this.DefSpCritDamageMul != other.DefSpCritDamageMul) return false;
            if (this.DefSpCritDamageAdd != other.DefSpCritDamageAdd) return false;

            #endregion

            #region Defense against skill critical

            // Defense against skill critical rate
            if (this.CustomDefSkillCritRate != other.CustomDefSkillCritRate) return false;
            if (this.DefSkillCritRateMul != other.DefSkillCritRateMul) return false;
            if (this.DefSkillCritRateAdd != other.DefSkillCritRateAdd) return false;

            // Defense against skill critical damage
            if (this.CustomDefSkillCritDamage != other.CustomDefSkillCritDamage) return false;
            if (this.DefSkillCritDamageMul != other.DefSkillCritDamageMul) return false;
            if (this.DefSkillCritDamageAdd != other.DefSkillCritDamageAdd) return false;

            // Defense against skill special critical rate
            if (this.CustomDefSkillSpCritRate != other.CustomDefSkillSpCritRate) return false;
            if (this.DefSkillSpCritRateMul != other.DefSkillSpCritRateMul) return false;
            if (this.DefSkillSpCritRateAdd != other.DefSkillSpCritRateAdd) return false;

            // Defense against skill special critical Damage
            if (this.CustomDefSkillSpCritDamage != other.CustomDefSkillSpCritDamage) return false;
            if (this.DefSkillSpCritDamageMul != other.DefSkillSpCritDamageMul) return false;
            if (this.DefSkillSpCritDamageAdd != other.DefSkillSpCritDamageAdd) return false;

            #endregion

            #region Passive attack

            // Attack
            if (this.CustomPassiveAtk != other.CustomPassiveAtk) return false;
            if (this.AtkMul != other.AtkMul) return false;
            if (this.AtkAdd != other.AtkAdd) return false;

            // Hit rate
            if (this.CustomPassiveHit != other.CustomPassiveHit) return false;
            if (this.HitMul != other.HitMul) return false;
            if (this.HitAdd != other.HitAdd) return false;

            #endregion

            #region Passive critical

            // Critical Rate
            if (this.CustomPassiveCritRate != other.CustomPassiveCritRate) return false;
            if (this.CritRateMul != other.CritRateMul) return false;
            if (this.CritRateAdd != other.CritRateAdd) return false;

            // Critical Damage
            if (this.CustomPassiveCritDamage != other.CustomPassiveCritDamage) return false;
            if (this.CritDamageMul != other.CritDamageMul) return false;
            if (this.CritDamageAdd != other.CritDamageAdd) return false;

            // Critical Guard Reduction
            if (this.CustomPassiveCritDefGuard != other.CustomPassiveCritDefGuard) return false;
            if (this.CritDefGuardMul != other.CritDefGuardMul) return false;
            if (this.CritDefGuardAdd != other.CritDefGuardAdd) return false;

            // Critical Evasion Reduction
            if (this.CustomPassiveCritDefEva != other.CustomPassiveCritDefEva) return false;
            if (this.CritDefEvaMul != other.CritDefEvaMul) return false;
            if (this.CritDefEvaAdd != other.CritDefEvaAdd) return false;

            #endregion

            #region Passive special critical

            // Special critical rate
            if (this.CustomPassiveSpCritRate != other.CustomPassiveSpCritRate) return false;
            if (this.SpCritRateMul != other.SpCritRateMul) return false;
            if (this.SpCritRateAdd != other.SpCritRateAdd) return false;

            // Special critical damage
            if (this.CustomPassiveSpCritDamage != other.CustomPassiveSpCritDamage) return false;
            if (this.SpCritDamageMul != other.SpCritDamageMul) return false;
            if (this.SpCritDamageAdd != other.SpCritDamageAdd) return false;

            // Special critical guard reduction
            if (this.CustomPassiveSpCritDefGuard != other.CustomPassiveSpCritDefGuard) return false;
            if (this.SpCritDefGuardMul != other.SpCritDefGuardMul) return false;
            if (this.SpCritDefGuardAdd != other.SpCritDefGuardAdd) return false;

            // Special critical evasion reduction
            if (this.CustomPassiveSpCritDefEva != other.CustomPassiveSpCritDefEva) return false;
            if (this.SpCritDefEvaMul != other.SpCritDefEvaMul) return false;
            if (this.SpCritDefEvaAdd != other.SpCritDefEvaAdd) return false;

            #endregion

            #region Passive defense

            // Physical defense
            if (this.CustomPassivePDef != other.CustomPassivePDef) return false;
            if (this.PDefMul != other.PDefMul) return false;
            if (this.PDefAdd != other.PDefAdd) return false;

            // Magical defense
            if (this.CustomPassiveMDef != other.CustomPassiveMDef) return false;
            if (this.MDefMul != other.MDefMul) return false;
            if (this.MDefAdd != other.MDefAdd) return false;

            #endregion

            #region Unarmed Attack

            // Attack
            if (this.CustomUnarmedAtk != other.CustomUnarmedAtk) return false;
            if (this.AtkInitial != other.AtkInitial) return false;
            if (this.AtkFinal != other.AtkFinal) return false;

            // Hit rate
            if (this.CustomUnarmedHit != other.CustomUnarmedHit) return false;
            if (this.HitInitial != other.HitInitial) return false;
            if (this.HitFinal != other.HitFinal) return false;

            // Attack animation
            if (this.CustomUnarmedAnim != other.CustomUnarmedAnim) return false;
            if (this.AnimCaster != other.AnimCaster) return false;
            if (this.AnimTarget != other.AnimTarget) return false;

            // Parameter attack force
            if (this.CustomUnarmedParamForce != other.CustomUnarmedParamForce) return false;
            if (this.StrForce != other.StrForce) return false;
            if (this.DexForce != other.DexForce) return false;
            if (this.AgiForce != other.AgiForce) return false;
            if (this.IntForce != other.IntForce) return false;

            // Defense attack force
            if (this.CustomUnarmedDefenseForce != other.CustomUnarmedDefenseForce) return false;
            if (this.PDefForce != other.PDefForce) return false;
            if (this.MDefForce != other.MDefForce) return false;

            #endregion

            #region Unarmed Critical

            // Critical Rate
            if (this.CustomUnarmedCritRate != other.CustomUnarmedCritRate) return false;
            if (this.CritRate != other.CritRate) return false;

            // Critical Damage
            if (this.CustomUnarmedCritDamage != other.CustomUnarmedCritDamage) return false;
            if (this.CritDamage != other.CritDamage) return false;

            // Critical Guard Reduction
            if (this.CustomUnarmedCritDefGuard != other.CustomUnarmedCritDefGuard) return false;
            if (this.CritDefGuard != other.CritDefGuard) return false;

            // Critical Evasion Reduction
            if (this.CustomUnarmedCritDefEva != other.CustomUnarmedCritDefEva) return false;
            if (this.CritDefEva != other.CritDefEva) return false;

            #endregion

            #region Unarmed Special Critical

            // Special critical rate
            if (this.CustomUnarmedSpCritRate != other.CustomUnarmedSpCritRate) return false;
            if (this.SpCritRate != other.SpCritRate) return false;

            // Special critical damage
            if (this.CustomUnarmedSpCritDamage != other.CustomUnarmedSpCritDamage) return false;
            if (this.SpCritDamage != other.SpCritDamage) return false;

            // Special critical guard reduction
            if (this.CustomUnarmedSpCritDefGuard != other.CustomUnarmedSpCritDefGuard) return false;
            if (this.SpCritDefGuard != other.SpCritDefGuard) return false;

            // Special critical evasion reduction
            if (this.CustomUnarmedSpCritDefEva != other.CustomUnarmedSpCritDefEva) return false;
            if (this.SpCritDefEva != other.SpCritDefEva) return false;

            #endregion

            #region Unarmoured Defense

            // Physical defense
            if (this.CustomUnarmouredPDef != other.CustomUnarmouredPDef) return false;
            if (this.PDefInitial != other.PDefInitial) return false;
            if (this.PDefFinal != other.PDefFinal) return false;

            // Magical defense
            if (this.CustomUnarmouredMDef != other.CustomUnarmouredMDef) return false;
            if (this.MDefInitial != other.MDefInitial) return false;
            if (this.MDefFinal != other.MDefFinal) return false;

            #endregion

            return true;
        }

        #endregion

        #region Reset

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

        #region Check properties

        #region Parameter

        /// <summary>Check if the maximum HP bonus is different from the default value.</summary>
        public bool CheckMaxHP()
        {
            return (this.customMaxHP && (this.maxHPMul != this.defaultMaxHPMul || this.maxHPAdd != this.defaultMaxHPAdd));
        }

        /// <summary>Check if the maximum SP bonus is different from the default value.</summary>
        public bool CheckMaxSP()
        {
            return (this.customMaxSP && (this.maxSPMul != this.defaultMaxSPMul || this.maxSPAdd != this.defaultMaxSPAdd));
        }

        /// <summary>Check if the strengh bonus is different from the default value.</summary>
        public bool CheckStr()
        {
            return (this.customStr && (this.strMul != this.defaultStrMul || this.strAdd != this.defaultStrAdd));
        }

        /// <summary>Check if the dexterity bonus is different from the default value.</summary>
        public bool CheckDex()
        {
            return (this.customDex && (this.dexMul != this.defaultDexMul || this.dexAdd != this.defaultDexAdd));
        }

        /// <summary>Check if the agility bonus is different from the default value.</summary>
        public bool CheckAgi()
        {
            return (this.customAgi && (this.agiMul != this.defaultAgiMul || this.agiAdd != this.defaultAgiAdd));
        }

        /// <summary>Check if the intelligence bonus is different from the default value.</summary>
        public bool CheckInt()
        {
            return (this.customInt && (this.intMul != this.defaultIntMul || this.intAdd != this.defaultIntAdd));
        }

        /// <summary>Check if the guard rate bonus is different from the default value.</summary>
        public bool CheckGuardRate()
        {
            return (this.customGuardRate && (this.guardRateMul != this.defaultGuardRateMul || this.guardRateAdd != this.defaultGuardRateAdd));
        }

        /// <summary>Check if the evasion rate bonus is different from the default value.</summary>
        public bool CheckEvaRate()
        {
            return (this.customEvaRate && (this.evaRateMul != this.defaultEvaRateMul || this.evaRateAdd != this.defaultEvaRateAdd));
        }

        #endregion

        #region Parameter rate

        /// <summary>Check if the strengh attack rate bonus is different from the default value.</summary>
        public bool CheckStrRate()
        {
            return (this.customStrRate && (this.strRateMul != this.defaultStrRateMul || this.strRateAdd != this.defaultStrRateAdd));
        }

        /// <summary>Check if the dexterity attack rate bonus is different from the default value.</summary>
        public bool CheckDexRate()
        {
            return (this.customDexRate && (this.dexRateMul != this.defaultDexRateMul || this.dexRateAdd != this.defaultDexRateAdd));
        }

        /// <summary>Check if the agility attack rate bonus is different from the default value.</summary>
        public bool CheckAgiRate()
        {
            return (this.customAgiRate && (this.agiRateMul != this.defaultAgiRateMul || this.agiRateAdd != this.defaultAgiRateAdd));
        }

        /// <summary>Check if the intelligence attack rate bonus is different from the default value.</summary>
        public bool CheckIntRate()
        {
            return (this.customIntRate && (this.intRateMul != this.defaultIntRateMul || this.intRateAdd != this.defaultIntRateAdd));
        }

        /// <summary>Check if the physical defense attack rate bonus is different from the default value.</summary>
        public bool CheckPDefRate()
        {
            return (this.customPDefRate && (this.pdefRateMul != this.defaultPDefRateMul || this.pdefRateAdd != this.defaultPDefRateAdd));
        }

        /// <summary>Check if the magical defense attack rate bonus is different from the default value.</summary>
        public bool CheckMDefRate()
        {
            return (this.customMDefRate && (this.mdefRateMul != this.defaultMDefRateMul || this.mdefRateAdd != this.defaultMDefRateAdd));
        }

        #endregion

        #region Defense against attack critical

        /// <summary>Check if the defense against attack critical rate bonus is different from the default value.</summary>
        public bool CheckDefCritRate()
        {
            return (this.customDefCritRate && (this.defCritRateMul != this.defaultDefCritRateMul || this.defCritRateAdd != this.defaultDefCritRateAdd));
        }

        /// <summary>Check if the defense against attack critical damage bonus is different from the default value.</summary>
        public bool CheckDefCritDamage()
        {
            return (this.customDefCritDamage && (this.defCritDamageMul != this.defaultDefCritDamageMul || this.defCritDamageAdd != this.defaultDefCritDamageAdd));
        }

        /// <summary>Check if the defense against attack special critical rate bonus is different from the default value.</summary>
        public bool CheckDefSpCritRate()
        {
            return (this.customDefSpCritRate && (this.defSpCritRateMul != this.defaultDefSpCritRateMul || this.defSpCritRateAdd != this.defaultDefSpCritRateAdd));
        }

        /// <summary>Check if the defense against attack special critical damage bonus is different from the default value.</summary>
        public bool CheckDefSpCritDamage()
        {
            return (this.customDefSpCritDamage && (this.defSpCritDamageMul != this.defaultDefSpCritDamageMul || this.defSpCritDamageAdd != this.defaultDefSpCritDamageAdd));
        }

        #endregion

        #region Defense against skill critical

        /// <summary>Check if the defense against skill critical rate bonus is different from the default value.</summary>
        public bool CheckDefSkillCritRate()
        {
            return (this.customDefSkillCritRate && (this.defSkillCritRateMul != this.defaultDefSkillCritRateMul || this.defSkillCritRateAdd != this.defaultDefSkillCritRateAdd));
        }

        /// <summary>Check if the defense against skill critical damage bonus is different from the default value.</summary>
        public bool CheckDefSkillCritDamage()
        {
            return (this.customDefSkillCritDamage && (this.defSkillCritDamageMul != this.defaultDefSkillCritDamageMul || this.defSkillCritDamageAdd != this.defaultDefSkillCritDamageAdd));
        }

        /// <summary>Check if the defense against skill special critical rate bonus is different from the default value.</summary>
        public bool CheckDefSkillSpCritRate()
        {
            return (this.customDefSkillSpCritRate && (this.defSkillSpCritRateMul != this.defaultDefSkillSpCritRateMul || this.defSkillSpCritRateAdd != this.defaultDefSkillSpCritRateAdd));
        }

        /// <summary>Check if the defense against skill special critical damage bonus is different from the default value.</summary>
        public bool CheckDefSkillSpCritDamage()
        {
            return (this.customDefSkillSpCritDamage && (this.defSkillSpCritDamageMul != this.defaultDefSkillSpCritDamageMul || this.defSkillSpCritDamageAdd != this.defaultDefSkillSpCritDamageAdd));
        }

        #endregion

        #region Passive attack

        /// <summary>Check if the attack bonus is different from the default value.</summary>
        public bool CheckPassiveAtk()
        {
            return (this.customPassiveAtk && (this.atkMul != this.defaultAtkMul || this.atkAdd != this.defaultAtkAdd));
        }

        /// <summary>Check if the hit rate bonus is different from the default value.</summary>
        public bool CheckPassiveHit()
        {
            return (this.customPassiveHit && (this.hitMul != this.defaultHitMul || this.hitAdd != this.defaultHitAdd));
        }

        #endregion

        #region Passive critical

        /// <summary>Check if the critical rate bonus is different from the default value.</summary>
        public bool CheckPassiveCritRate()
        {
            return (this.customPassiveCritRate && (this.critRateMul != this.defaultCritRateMul || this.critRateAdd != this.defaultCritRateAdd));
        }

        /// <summary>Check if the critical damage bonus is different from the default value.</summary>
        public bool CheckPassiveCritDamage()
        {
            return (this.customPassiveCritDamage && (this.critDamageMul != this.defaultCritDamageMul || this.critDamageAdd != this.defaultCritDamageAdd));
        }

        /// <summary>Check if the critical guard rate reduction bonus is different from the default value.</summary>
        public bool CheckPassiveCritDefGuard()
        {
            return (this.customPassiveCritDefGuard && (this.critDefGuardMul != this.defaultCritDefGuardMul || this.critDefGuardAdd != this.defaultCritDefGuardAdd));
        }

        /// <summary>Check if the critical evasion rate reduction bonus is different from the default value.</summary>
        public bool CheckPassiveCritDefEva()
        {
            return (this.customPassiveCritDefEva && (this.critDefEvaMul != this.defaultCritDefEvaMul || this.critDefEvaAdd != this.defaultCritDefEvaAdd));
        }

        #endregion

        #region Passive special critical

        /// <summary>Check if the special critical rate bonus is different from the default value.</summary>
        public bool CheckPassiveSpCritRate()
        {
            return (this.customPassiveSpCritRate && (this.spCritRateMul != this.defaultSpCritRateMul || this.spCritRateAdd != this.defaultSpCritRateAdd));
        }

        /// <summary>Check if the special critical damage bonus is different from the default value.</summary>
        public bool CheckPassiveSpCritDamage()
        {
            return (this.customPassiveSpCritDamage && (this.spCritDamageMul != this.defaultSpCritDamageMul || this.spCritDamageAdd != this.defaultSpCritDamageAdd));
        }

        /// <summary>Check if the special critical guard rate reduction bonus is different from the default value.</summary>
        public bool CheckPassiveSpCritDefGuard()
        {
            return (this.customPassiveSpCritDefGuard && (this.spCritDefGuardMul != this.defaultSpCritDefGuardMul || this.spCritDefGuardAdd != this.defaultSpCritDefGuardAdd));
        }

        /// <summary>Check if the special critical evasion rate reduction bonus is different from the default value.</summary>
        public bool CheckPassiveSpCritDefEva()
        {
            return (this.customPassiveSpCritDefEva && (this.spCritDefEvaMul != this.defaultSpCritDefEvaMul || this.spCritDefEvaAdd != this.defaultSpCritDefEvaAdd));
        }

        #endregion

        #region Passive defense

        /// <summary>Check if the physical defense bonus is different from the default value.</summary>
        public bool CheckPassivePDef()
        {
            return (this.customPassivePDef && (this.pdefMul != this.defaultPDefMul || this.pdefAdd != this.defaultPDefAdd));
        }

        /// <summary>Check if the magical defense bonus is different from the default value.</summary>
        public bool CheckPassiveMDef()
        {
            return (this.customPassiveMDef && (this.mdefMul != this.defaultMDefMul || this.mdefAdd != this.defaultMDefAdd));
        }

        #endregion

        #endregion

        #region Clone

        /// <summary>Copy configurations</summary>
        /// <param name="other">Other configuration to copy</param>
        public virtual void CloneFrom(DataPackClass other)
        {
            // ID and Name
            this.ID = other.ID;
            this.Name = other.Name;

            #region In family

            this.ClassFamily.Clear();
            if (other.ClassFamily.Count > 0)
            {
                foreach (Class classes in other.ClassFamily)
                {
                    this.ClassFamily.Add(new Class(classes.ID, classes.Name));
                }
            }

            #endregion

            #region Equipment

            // Equipment list
            this.CustomEquip = other.CustomEquip;
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
            this.CustomDualHoldName = other.CustomDualHoldName;
            this.DualHoldNameWeapon = other.DualHoldNameWeapon;
            this.DualHoldNameShield = other.DualHoldNameShield;

            // Dual hold multiplier
            this.CustomDualHoldMul = other.CustomDualHoldMul;
            this.DualHoldMulWeapon = other.DualHoldMulWeapon;
            this.DualHoldMulShield = other.DualHoldMulShield;

            // Shield bypass
            this.ShieldBypass = other.ShieldBypass;
            this.ShieldBypassMul = other.ShieldBypassMul;

            // Weapon bypass
            this.WeaponBypass = other.WeaponBypass;
            this.WeaponBypassMul = other.WeaponBypassMul;

            // Reduce hand
            this.CustomReduceHand = other.CustomReduceHand;
            this.ReduceHand = other.ReduceHand;
            this.ReduceHandMul = other.ReduceHandMul;

            #endregion

            #region Parameter

            // Maximum HP
            this.CustomMaxHP = other.CustomMaxHP;
            this.MaxHPMul = other.MaxHPMul;
            this.MaxHPAdd = other.MaxHPAdd;

            // Maximum SP
            this.CustomMaxSP = other.CustomMaxSP;
            this.MaxSPMul = other.MaxSPMul;
            this.MaxSPAdd = other.MaxSPAdd;

            // Strengh
            this.CustomStr = other.CustomStr;
            this.StrMul = other.StrMul;
            this.StrAdd = other.StrAdd;

            // Dexterity
            this.CustomDex = other.CustomDex;
            this.DexMul = other.DexMul;
            this.DexAdd = other.DexAdd;

            // Agility
            this.CustomAgi = other.CustomAgi;
            this.AgiMul = other.AgiMul;
            this.AgiAdd = other.AgiAdd;

            // Intelligence
            this.CustomInt = other.CustomInt;
            this.IntMul = other.IntMul;
            this.IntAdd = other.IntAdd;

            // Guard rate
            this.CustomGuardRate = other.CustomGuardRate;
            this.GuardRateMul = other.GuardRateMul;
            this.GuardRateAdd = other.GuardRateAdd;

            // Evasion rate
            this.CustomEvaRate = other.CustomEvaRate;
            this.EvaRateMul = other.EvaRateMul;
            this.EvaRateAdd = other.EvaRateAdd;

            #endregion

            #region Parameter rate

            // Strengh rate
            this.CustomStrRate = other.CustomStrRate;
            this.StrRateMul = other.StrRateMul;
            this.StrRateAdd = other.StrRateAdd;

            // Dexterity rate
            this.CustomDexRate = other.CustomDexRate;
            this.DexRateMul = other.DexRateMul;
            this.DexRateAdd = other.DexRateAdd;

            // Agility rate
            this.CustomAgiRate = other.CustomAgiRate;
            this.AgiRateMul = other.AgiRateMul;
            this.AgiRateAdd = other.AgiRateAdd;

            // Intelligence rate
            this.CustomIntRate = other.CustomIntRate;
            this.IntRateMul = other.IntRateMul;
            this.IntRateAdd = other.IntRateAdd;

            // Physical defense rate
            this.CustomPDefRate = other.CustomPDefRate;
            this.PDefRateMul = other.PDefRateMul;
            this.PDefRateAdd = other.PDefRateAdd;

            // Magical defense rate
            this.CustomMDefRate = other.CustomMDefRate;
            this.MDefRateMul = other.MDefRateMul;
            this.MDefRateAdd = other.MDefRateAdd;

            #endregion

            #region Defense against attack critical

            // Defense against Attack Critical Rate
            this.CustomDefCritRate = other.CustomDefCritRate;
            this.DefCritRateMul = other.DefCritRateMul;
            this.DefCritRateAdd = other.DefCritRateAdd;

            // Defense against Attack Critical Damage
            this.CustomDefCritDamage = other.CustomDefCritDamage;
            this.DefCritDamageMul = other.DefCritDamageMul;
            this.DefCritDamageAdd = other.DefCritDamageAdd;

            // Defense against Attack Special Critical Rate
            this.CustomDefSpCritRate = other.CustomDefSpCritRate;
            this.DefSpCritRateMul = other.DefSpCritRateMul;
            this.DefSpCritRateAdd = other.DefSpCritRateAdd;

            // Defense against Attack Special Critical Damage
            this.CustomDefSpCritDamage = other.CustomDefSpCritDamage;
            this.DefSpCritDamageMul = other.DefSpCritDamageMul;
            this.DefSpCritDamageAdd = other.DefSpCritDamageAdd;

            #endregion

            #region Defense against skill critical

            // Defense against Skill Critical Rate
            this.CustomDefSkillCritRate = other.CustomDefSkillCritRate;
            this.DefSkillCritRateMul = other.DefSkillCritRateMul;
            this.DefSkillCritRateAdd = other.DefSkillCritRateAdd;

            // Defense against Skill Critical Damage
            this.CustomDefSkillCritDamage = other.CustomDefSkillCritDamage;
            this.DefSkillCritDamageMul = other.DefSkillCritDamageMul;
            this.DefSkillCritDamageAdd = other.DefSkillCritDamageAdd;

            // Defense against Skill Special Critical Rate
            this.CustomDefSkillSpCritRate = other.CustomDefSkillSpCritRate;
            this.DefSkillSpCritRateMul = other.DefSkillSpCritRateMul;
            this.DefSkillSpCritRateAdd = other.DefSkillSpCritRateAdd;

            // Defense against Skill Special Critical Damage
            this.CustomDefSkillSpCritDamage = other.CustomDefSkillSpCritDamage;
            this.DefSkillSpCritDamageMul = other.DefSkillSpCritDamageMul;
            this.DefSkillSpCritDamageAdd = other.DefSkillSpCritDamageAdd;

            #endregion

            #region Passive attack

            // Attack
            this.CustomPassiveAtk = other.CustomPassiveAtk;
            this.AtkMul = other.AtkMul;
            this.AtkAdd = other.AtkAdd;

            // Hit Rate
            this.CustomPassiveHit = other.CustomPassiveHit;
            this.HitMul = other.HitMul;
            this.HitAdd = other.HitAdd;

            #endregion

            #region Passive critical

            // Critical rate
            this.CustomPassiveCritRate = other.CustomPassiveCritRate;
            this.CritRateMul = other.CritRateMul;
            this.CritRateAdd = other.CritRateAdd;

            // Critical damage
            this.CustomPassiveCritDamage = other.CustomPassiveCritDamage;
            this.CritDamageMul = other.CritDamageMul;
            this.CritDamageAdd = other.CritDamageAdd;

            // Critical guard reduction
            this.CustomPassiveCritDefGuard = other.CustomPassiveCritDefGuard;
            this.CritDefGuardMul = other.CritDefGuardMul;
            this.CritDefGuardAdd = other.CritDefGuardAdd;

            // Critical evasion reduction
            this.CustomPassiveCritDefEva = other.CustomPassiveCritDefEva;
            this.CritDefEvaMul = other.CritDefEvaMul;
            this.CritDefEvaAdd = other.CritDefEvaAdd;

            #endregion

            #region Passive special critical

            // Special critical rate
            this.CustomPassiveSpCritRate = other.CustomPassiveSpCritRate;
            this.SpCritRateMul = other.SpCritRateMul;
            this.SpCritRateAdd = other.SpCritRateAdd;

            // Special critical damage
            this.CustomPassiveSpCritDamage = other.CustomPassiveSpCritDamage;
            this.SpCritDamageMul = other.SpCritDamageMul;
            this.SpCritDamageAdd = other.SpCritDamageAdd;

            // Special critical guard reduction
            this.CustomPassiveSpCritDefGuard = other.CustomPassiveSpCritDefGuard;
            this.SpCritDefGuardMul = other.SpCritDefGuardMul;
            this.SpCritDefGuardAdd = other.SpCritDefGuardAdd;

            // Special critical evasion reduction
            this.CustomPassiveSpCritDefEva = other.CustomPassiveSpCritDefEva;
            this.SpCritDefEvaMul = other.SpCritDefEvaMul;
            this.SpCritDefEvaAdd = other.SpCritDefEvaAdd;

            #endregion

            #region Passive defense

            // Physical defense
            this.CustomPassivePDef = other.CustomPassivePDef;
            this.PDefMul = other.PDefMul;
            this.PDefAdd = other.PDefAdd;

            // Magical defense
            this.CustomPassiveMDef = other.CustomPassiveMDef;
            this.MDefMul = other.MDefMul;
            this.MDefAdd = other.MDefAdd;

            #endregion

            #region Unarmed Attack

            // Attack
            this.CustomUnarmedAtk = other.CustomUnarmedAtk;
            this.AtkInitial = other.AtkInitial;
            this.AtkFinal = other.AtkFinal;

            // Hit Rate
            this.CustomUnarmedHit = other.CustomUnarmedHit;
            this.HitInitial = other.HitInitial;
            this.HitFinal = other.HitFinal;

            // Attack animation
            this.CustomUnarmedAnim = other.CustomUnarmedAnim;
            this.AnimCaster = other.AnimCaster;
            this.AnimTarget = other.AnimTarget;

            // Parameter force
            this.CustomUnarmedParamForce = other.CustomUnarmedParamForce;
            this.StrForce = other.StrForce;
            this.DexForce = other.DexForce;
            this.AgiForce = other.AgiForce;
            this.IntForce = other.IntForce;

            // Defense force
            this.CustomUnarmedDefenseForce = other.CustomUnarmedDefenseForce;
            this.PDefForce = other.PDefForce;
            this.MDefForce = other.MDefForce;

            #endregion

            #region Unarmed Critical

            // Critical rate
            this.CustomUnarmedCritRate = other.CustomUnarmedCritRate;
            this.CritRate = other.CritRate;

            // Critical damage
            this.CustomUnarmedCritDamage = other.CustomUnarmedCritDamage;
            this.CritDamage = other.CritDamage;

            // Critical guard reduction
            this.CustomUnarmedCritDefGuard = other.CustomUnarmedCritDefGuard;
            this.CritDefGuard = other.CritDefGuard;

            // Critical evasion reduction
            this.CustomUnarmedCritDefEva = other.CustomUnarmedCritDefEva;
            this.CritDefEva = other.CritDefEva;

            #endregion

            #region Unarmed Special Critical

            // Special critical rate
            this.CustomUnarmedSpCritRate = other.CustomUnarmedSpCritRate;
            this.SpCritRate = other.SpCritRate;

            // Special critical damage
            this.CustomUnarmedSpCritDamage = other.CustomUnarmedSpCritDamage;
            this.SpCritDamage = other.SpCritDamage;

            // Special critical guard reduction
            this.CustomUnarmedSpCritDefGuard = other.CustomUnarmedSpCritDefGuard;
            this.SpCritDefGuard = other.SpCritDefGuard;

            // Special critical evasion reduction
            this.CustomUnarmedSpCritDefEva = other.CustomUnarmedSpCritDefEva;
            this.SpCritDefEva = other.SpCritDefEva;

            #endregion

            #region Unarmoured Defense

            // Physical defense
            this.CustomUnarmouredPDef = other.CustomUnarmouredPDef;
            this.PDefInitial = other.PDefInitial;
            this.PDefFinal = other.PDefFinal;

            // Magical defense
            this.CustomUnarmouredMDef = other.CustomUnarmouredMDef;
            this.MDefInitial = other.MDefInitial;
            this.MDefFinal = other.MDefFinal;

            #endregion
        }

        /// <summary>Copy configurations from clipboard</summary>
        /// <param name="other">Other configuration to copy</param>
        public virtual void PasteFrom(DataPackClass other)
        {
            #region Equipment

            // Equipment list
            this.CustomEquip = other.CustomEquip;
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
            this.CustomDualHoldName = other.CustomDualHoldName;
            this.DualHoldNameWeapon = other.DualHoldNameWeapon;
            this.DualHoldNameShield = other.DualHoldNameShield;

            // Dual hold multiplier
            this.CustomDualHoldMul = other.CustomDualHoldMul;
            this.DualHoldMulWeapon = other.DualHoldMulWeapon;
            this.DualHoldMulShield = other.DualHoldMulShield;

            // Shield bypass
            this.ShieldBypass = other.ShieldBypass;
            this.ShieldBypassMul = other.ShieldBypassMul;

            // Weapon bypass
            this.WeaponBypass = other.WeaponBypass;
            this.WeaponBypassMul = other.WeaponBypassMul;

            // Reduce hand
            this.CustomReduceHand = other.CustomReduceHand;
            this.ReduceHand = other.ReduceHand;
            this.ReduceHandMul = other.ReduceHandMul;

            #endregion

            #region Parameter

            // Maximum HP
            this.CustomMaxHP = other.CustomMaxHP;
            this.MaxHPMul = other.MaxHPMul;
            this.MaxHPAdd = other.MaxHPAdd;

            // Maximum SP
            this.CustomMaxSP = other.CustomMaxSP;
            this.MaxSPMul = other.MaxSPMul;
            this.MaxSPAdd = other.MaxSPAdd;

            // Strengh
            this.CustomStr = other.CustomStr;
            this.StrMul = other.StrMul;
            this.StrAdd = other.StrAdd;

            // Dexterity
            this.CustomDex = other.CustomDex;
            this.DexMul = other.DexMul;
            this.DexAdd = other.DexAdd;

            // Agility
            this.CustomAgi = other.CustomAgi;
            this.AgiMul = other.AgiMul;
            this.AgiAdd = other.AgiAdd;

            // Intelligence
            this.CustomInt = other.CustomInt;
            this.IntMul = other.IntMul;
            this.IntAdd = other.IntAdd;

            // Guard rate
            this.CustomGuardRate = other.CustomGuardRate;
            this.GuardRateMul = other.GuardRateMul;
            this.GuardRateAdd = other.GuardRateAdd;

            // Evasion rate
            this.CustomEvaRate = other.CustomEvaRate;
            this.EvaRateMul = other.EvaRateMul;
            this.EvaRateAdd = other.EvaRateAdd;

            #endregion

            #region Parameter rate

            // Strengh rate
            this.CustomStrRate = other.CustomStrRate;
            this.StrRateMul = other.StrRateMul;
            this.StrRateAdd = other.StrRateAdd;

            // Dexterity rate
            this.CustomDexRate = other.CustomDexRate;
            this.DexRateMul = other.DexRateMul;
            this.DexRateAdd = other.DexRateAdd;

            // Agility rate
            this.CustomAgiRate = other.CustomAgiRate;
            this.AgiRateMul = other.AgiRateMul;
            this.AgiRateAdd = other.AgiRateAdd;

            // Intelligence rate
            this.CustomIntRate = other.CustomIntRate;
            this.IntRateMul = other.IntRateMul;
            this.IntRateAdd = other.IntRateAdd;

            // Physical defense rate
            this.CustomPDefRate = other.CustomPDefRate;
            this.PDefRateMul = other.PDefRateMul;
            this.PDefRateAdd = other.PDefRateAdd;

            // Magical defense rate
            this.CustomMDefRate = other.CustomMDefRate;
            this.MDefRateMul = other.MDefRateMul;
            this.MDefRateAdd = other.MDefRateAdd;

            #endregion

            #region Defense against attack critical

            // Defense against Attack Critical Rate
            this.CustomDefCritRate = other.CustomDefCritRate;
            this.DefCritRateMul = other.DefCritRateMul;
            this.DefCritRateAdd = other.DefCritRateAdd;

            // Defense against Attack Critical Damage
            this.CustomDefCritDamage = other.CustomDefCritDamage;
            this.DefCritDamageMul = other.DefCritDamageMul;
            this.DefCritDamageAdd = other.DefCritDamageAdd;

            // Defense against Attack Special Critical Rate
            this.CustomDefSpCritRate = other.CustomDefSpCritRate;
            this.DefSpCritRateMul = other.DefSpCritRateMul;
            this.DefSpCritRateAdd = other.DefSpCritRateAdd;

            // Defense against Attack Special Critical Damage
            this.CustomDefSpCritDamage = other.CustomDefSpCritDamage;
            this.DefSpCritDamageMul = other.DefSpCritDamageMul;
            this.DefSpCritDamageAdd = other.DefSpCritDamageAdd;

            #endregion

            #region Defense against skill critical

            // Defense against Skill Critical Rate
            this.CustomDefSkillCritRate = other.CustomDefSkillCritRate;
            this.DefSkillCritRateMul = other.DefSkillCritRateMul;
            this.DefSkillCritRateAdd = other.DefSkillCritRateAdd;

            // Defense against Skill Critical Damage
            this.CustomDefSkillCritDamage = other.CustomDefSkillCritDamage;
            this.DefSkillCritDamageMul = other.DefSkillCritDamageMul;
            this.DefSkillCritDamageAdd = other.DefSkillCritDamageAdd;

            // Defense against Skill Special Critical Rate
            this.CustomDefSkillSpCritRate = other.CustomDefSkillSpCritRate;
            this.DefSkillSpCritRateMul = other.DefSkillSpCritRateMul;
            this.DefSkillSpCritRateAdd = other.DefSkillSpCritRateAdd;

            // Defense against Skill Special Critical Damage
            this.CustomDefSkillSpCritDamage = other.CustomDefSkillSpCritDamage;
            this.DefSkillSpCritDamageMul = other.DefSkillSpCritDamageMul;
            this.DefSkillSpCritDamageAdd = other.DefSkillSpCritDamageAdd;

            #endregion

            #region Passive attack

            // Attack
            this.CustomPassiveAtk = other.CustomPassiveAtk;
            this.AtkMul = other.AtkMul;
            this.AtkAdd = other.AtkAdd;

            // Hit Rate
            this.CustomPassiveHit = other.CustomPassiveHit;
            this.HitMul = other.HitMul;
            this.HitAdd = other.HitAdd;

            #endregion

            #region Passive critical

            // Critical rate
            this.CustomPassiveCritRate = other.CustomPassiveCritRate;
            this.CritRateMul = other.CritRateMul;
            this.CritRateAdd = other.CritRateAdd;

            // Critical damage
            this.CustomPassiveCritDamage = other.CustomPassiveCritDamage;
            this.CritDamageMul = other.CritDamageMul;
            this.CritDamageAdd = other.CritDamageAdd;

            // Critical guard reduction
            this.CustomPassiveCritDefGuard = other.CustomPassiveCritDefGuard;
            this.CritDefGuardMul = other.CritDefGuardMul;
            this.CritDefGuardAdd = other.CritDefGuardAdd;

            // Critical evasion reduction
            this.CustomPassiveCritDefEva = other.CustomPassiveCritDefEva;
            this.CritDefEvaMul = other.CritDefEvaMul;
            this.CritDefEvaAdd = other.CritDefEvaAdd;

            #endregion

            #region Passive special critical

            // Special critical rate
            this.CustomPassiveSpCritRate = other.CustomPassiveSpCritRate;
            this.SpCritRateMul = other.SpCritRateMul;
            this.SpCritRateAdd = other.SpCritRateAdd;

            // Special critical damage
            this.CustomPassiveSpCritDamage = other.CustomPassiveSpCritDamage;
            this.SpCritDamageMul = other.SpCritDamageMul;
            this.SpCritDamageAdd = other.SpCritDamageAdd;

            // Special critical guard reduction
            this.CustomPassiveSpCritDefGuard = other.CustomPassiveSpCritDefGuard;
            this.SpCritDefGuardMul = other.SpCritDefGuardMul;
            this.SpCritDefGuardAdd = other.SpCritDefGuardAdd;

            // Special critical evasion reduction
            this.CustomPassiveSpCritDefEva = other.CustomPassiveSpCritDefEva;
            this.SpCritDefEvaMul = other.SpCritDefEvaMul;
            this.SpCritDefEvaAdd = other.SpCritDefEvaAdd;

            #endregion

            #region Passive defense

            // Physical defense
            this.CustomPassivePDef = other.CustomPassivePDef;
            this.PDefMul = other.PDefMul;
            this.PDefAdd = other.PDefAdd;

            // Magical defense
            this.CustomPassiveMDef = other.CustomPassiveMDef;
            this.MDefMul = other.MDefMul;
            this.MDefAdd = other.MDefAdd;

            #endregion

            #region Unarmed Attack

            // Attack
            this.CustomUnarmedAtk = other.CustomUnarmedAtk;
            this.AtkInitial = other.AtkInitial;
            this.AtkFinal = other.AtkFinal;

            // Hit Rate
            this.CustomUnarmedHit = other.CustomUnarmedHit;
            this.HitInitial = other.HitInitial;
            this.HitFinal = other.HitFinal;

            // Attack animation
            this.CustomUnarmedAnim = other.CustomUnarmedAnim;
            this.AnimCaster = other.AnimCaster;
            this.AnimTarget = other.AnimTarget;

            // Parameter force
            this.CustomUnarmedParamForce = other.CustomUnarmedParamForce;
            this.StrForce = other.StrForce;
            this.DexForce = other.DexForce;
            this.AgiForce = other.AgiForce;
            this.IntForce = other.IntForce;

            // Defense force
            this.CustomUnarmedDefenseForce = other.CustomUnarmedDefenseForce;
            this.PDefForce = other.PDefForce;
            this.MDefForce = other.MDefForce;

            #endregion

            #region Unarmed Critical

            // Critical rate
            this.CustomUnarmedCritRate = other.CustomUnarmedCritRate;
            this.CritRate = other.CritRate;

            // Critical damage
            this.CustomUnarmedCritDamage = other.CustomUnarmedCritDamage;
            this.CritDamage = other.CritDamage;

            // Critical guard reduction
            this.CustomUnarmedCritDefGuard = other.CustomUnarmedCritDefGuard;
            this.CritDefGuard = other.CritDefGuard;

            // Critical evasion reduction
            this.CustomUnarmedCritDefEva = other.CustomUnarmedCritDefEva;
            this.CritDefEva = other.CritDefEva;

            #endregion

            #region Unarmed Special Critical

            // Special critical rate
            this.CustomUnarmedSpCritRate = other.CustomUnarmedSpCritRate;
            this.SpCritRate = other.SpCritRate;

            // Special critical damage
            this.CustomUnarmedSpCritDamage = other.CustomUnarmedSpCritDamage;
            this.SpCritDamage = other.SpCritDamage;

            // Special critical guard reduction
            this.CustomUnarmedSpCritDefGuard = other.CustomUnarmedSpCritDefGuard;
            this.SpCritDefGuard = other.SpCritDefGuard;

            // Special critical evasion reduction
            this.CustomUnarmedSpCritDefEva = other.CustomUnarmedSpCritDefEva;
            this.SpCritDefEva = other.SpCritDefEva;

            #endregion

            #region Unarmoured Defense

            // Physical defense
            this.CustomUnarmouredPDef = other.CustomUnarmouredPDef;
            this.PDefInitial = other.PDefInitial;
            this.PDefFinal = other.PDefFinal;

            // Magical defense
            this.CustomUnarmouredMDef = other.CustomUnarmouredMDef;
            this.MDefInitial = other.MDefInitial;
            this.MDefFinal = other.MDefFinal;

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
