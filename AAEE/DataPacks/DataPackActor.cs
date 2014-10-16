using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using AAEE.Data;
using AAEE.Utility;

namespace AAEE.DataPacks
{
    /// <summary>This class serves as datapack for actor and class configuration.</summary>
    [XmlType("DataPackActor")]
    public class DataPackActor : IResetable
    {
        #region Defaults

        // Default name for family
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
        private int defaultMaxHPInitial = 1;
        private int defaultMaxHPFinal = 1;

        // Default maximum SP
        private bool defaultCustomMaxSP = false;
        private int defaultMaxSPInitial = 0;
        private int defaultMaxSPFinal = 0;

        // Default strengh
        private bool defaultCustomStr = false;
        private int defaultStrInitial = 1;
        private int defaultStrFinal = 1;

        // Default dexterity
        private bool defaultCustomDex = false;
        private int defaultDexInitial = 1;
        private int defaultDexFinal = 1;

        // Default agility
        private bool defaultCustomAgi = false;
        private int defaultAgiInitial = 1;
        private int defaultAgiFinal = 1;

        // Default intelligence
        private bool defaultCustomInt = false;
        private int defaultIntInitial = 1;
        private int defaultIntFinal = 1;

        #endregion

        #region Parameter attack rate

        // Default strengh attack rate
        private bool defaultCustomStrRate = false;
        private decimal defaultStrRate = 0.05m;

        // Default dexterity attack rate
        private bool defaultCustomDexRate = false;
        private decimal defaultDexRate = 0.05m;

        // Default agility attack rate
        private bool defaultCustomAgiRate = false;
        private decimal defaultAgiRate = 0.05m;

        // Default intelligence attack rate
        private bool defaultCustomIntRate = false;
        private decimal defaultIntRate = 0.05m;

        // Default physical defense attack rate
        private bool defaultCustomPDefRate = false;
        private decimal defaultPDefRate = 0.5m;

        // Default magical defense attack rate
        private bool defaultCustomMDefRate = false;
        private decimal defaultMDefRate = 0.5m;

        // Default guard rate
        private bool defaultCustomGuardRate = false;
        private decimal defaultGuardRate = 0.5m;

        // Default evasion rate
        private bool defaultCustomEvaRate = false;
        private decimal defaultEvaRate = 0.08m;

        #endregion

        #region Defense against Attack Critical

        // Default defense against attack critical rate
        private bool defaultCustomDefCritRate = false;
        private decimal defaultDefCritRate = 1.0m;

        // Default defense against attack critical damage
        private bool defaultCustomDefCritDamage = false;
        private decimal defaultDefCritDamage = 1.0m;

        // Default defense against attack special critical rate
        private bool defaultCustomDefSpCritRate = false;
        private decimal defaultDefSpCritRate = 1.0m;

        // Default defense against attack special critical damage
        private bool defaultCustomDefSpCritDamage = false;
        private decimal defaultDefSpCritDamage = 1.0m;

        #endregion

        #region Defense against Skill Critical

        // Default defense against skill critical rate
        private bool defaultCustomDefSkillCritRate = false;
        private decimal defaultDefSkillCritRate = 1.0m;

        // Default defense against skill critical damage
        private bool defaultCustomDefSkillCritDamage = false;
        private decimal defaultDefSkillCritDamage = 1.0m;

        // Default defense against skill special critical rate
        private bool defaultCustomDefSkillSpCritRate = false;
        private decimal defaultDefSkillSpCritRate = 1.0m;

        // Default defense against skill special critical damage
        private bool defaultCustomDefSkillSpCritDamage = false;
        private decimal defaultDefSkillSpCritDamage = 1.0m;

        #endregion

        #region Unarmed Attack

        // Default attack
        private bool defaultCustomAtk = false;
        private int defaultAtkInitial = 0;
        private int defaultAtkFinal = 0;

        // Default hit rate
        private bool defaultCustomHit = false;
        private decimal defaultHitInitial = 0.0m;
        private decimal defaultHitFinal = 0.0m;

        // Default attack animation
        private bool defaultCustomAnim = false;
        private int defaultAnimCaster = 0;
        private int defaultAnimTarget = 0;

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

        #region Unarmed Critical

        // Default critical rate
        private bool defaultCustomCritRate = false;
        private decimal defaultCritRate = 0.04m;

        // Default critical damage
        private bool defaultCustomCritDamage = false;
        private decimal defaultCritDamage = 2.0m;

        // Default critical guard rate reduction
        private bool defaultCustomCritDefGuard = false;
        private decimal defaultCritDefGuard = 1.0m;

        // Default critical evasion rate reduction
        private bool defaultCustomCritDefEva = false;
        private decimal defaultCritDefEva = 1.0m;

        #endregion

        #region Unarmed Special Critical

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

        #region Unarmoured Defense

        // Default physical defense
        private bool defaultCustomPDef = false;
        private int defaultPDefInitial = 0;
        private int defaultPDefFinal = 0;

        // Default magical defense
        private bool defaultCustomMDef = false;
        private int defaultMDefInitial = 0;
        private int defaultMDefFinal = 0;

        #endregion

        #endregion

        #region Fields

        // Id and name
        private int id;
        private string name;

        #region In Family
        
        private List<Actor> actorFamily = new List<Actor>();

        #endregion

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
        private int maxHPInitial;
        private int maxHPFinal;

        // Maximum SP
        private bool customMaxSP;
        private int maxSPInitial;
        private int maxSPFinal;

        // Strengh
        private bool customStr;
        private int strInitial;
        private int strFinal;

        // Dexterity
        private bool customDex;
        private int dexInitial;
        private int dexFinal;

        // Agility
        private bool customAgi;
        private int agiInitial;
        private int agiFinal;

        // Intelligence
        private bool customInt;
        private int intInitial;
        private int intFinal;

        #endregion

        #region Parameter Rate

        // Strengh attack rate
        private bool customStrRate;
        private decimal strRate;

        // Dexterity attack rate
        private bool customDexRate;
        private decimal dexRate;

        // Agility attack rate
        private bool customAgiRate;
        private decimal agiRate;

        // Intelligence attack rate
        private bool customIntRate;
        private decimal intRate;

        // Physical defense attack rate
        private bool customPDefRate;
        private decimal pdefRate;

        // Magical defense attack rate
        private bool customMDefRate;
        private decimal mdefRate;

        // Guard rate
        private bool customGuardRate;
        private decimal guardRate;

        // Evasion rate
        private bool customEvaRate;
        private decimal evaRate;

        #endregion

        #region Defense against Attack Critical

        // Defense against attack critical rate
        private bool customDefCritRate;
        private decimal defCritRate;

        // Defense against attack critical damage
        private bool customDefCritDamage;
        private decimal defCritDamage;

        // Defense against attack special critical rate
        private bool customDefSpCritRate;
        private decimal defSpCritRate;

        // Defense against attack special critical damage
        private bool customDefSpCritDamage;
        private decimal defSpCritDamage;

        #endregion

        #region Defense against Skill Critical

        // Defense against skill critical rate
        private bool customDefSkillCritRate;
        private decimal defSkillCritRate;

        // Defense against skill critical damage
        private bool customDefSkillCritDamage;
        private decimal defSkillCritDamage;

        // Defense against skill special critical rate
        private bool customDefSkillSpCritRate;
        private decimal defSkillSpCritRate;

        // Defense against skill special critical damage
        private bool customDefSkillSpCritDamage;
        private decimal defSkillSpCritDamage;

        #endregion

        #region Unarmed Attack

        // Attack
        private bool customAtk;
        private int atkInitial;
        private int atkFinal;

        // Hit rate
        private bool customHit;
        private decimal hitInitial;
        private decimal hitFinal;

        // Attack animation
        private bool customAnim;
        private int animCaster;
        private int animTarget;

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

        #region Unarmed Critical

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

        #region Unarmed Special Critical

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

        #region Unarmoured Defense

        // Physical defense
        private bool customPDef;
        private int pdefInitial;
        private int pdefFinal;

        // Magical defense
        private bool customMDef;
        private int mdefInitial;
        private int mdefFinal;

        #endregion

        #endregion

        #region Properties

        /// <summary>Gets or sets the actor or class ID.</summary>
        [XmlAttribute()]
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>Gets or sets the actor or class name.</summary>
        [XmlAttribute()]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        #region In Family

        /// <summary>Gets or sets the list of actor in the family.</summary>
        public List<Actor> ActorFamily
        {
            get { return actorFamily; }
            set { actorFamily = new List<Actor>(value); }
        }

        #endregion

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

        /// <summary>Gets or sets the actor or class custom maximum HP checkBox.</summary>
        public bool CustomMaxHP
        {
            get { return customMaxHP; }
            set { customMaxHP = value; }
        }

        /// <summary>Gets or sets the actor or class maximum HP Initial.</summary>
        public int MaxHPInitial
        {
            get { return maxHPInitial; }
            set { maxHPInitial = value; }
        }

        /// <summary>Gets or sets the actor or class maximum HP Final.</summary>
        public int MaxHPFinal
        {
            get { return maxHPFinal; }
            set { maxHPFinal = value; }
        }

        /// <summary>Gets or sets the actor or class custom maximum SP checkBox.</summary>
        public bool CustomMaxSP
        {
            get { return customMaxSP; }
            set { customMaxSP = value; }
        }

        /// <summary>Gets or sets the actor or class maximum SP Initial.</summary>
        public int MaxSPInitial
        {
            get { return maxSPInitial; }
            set { maxSPInitial = value; }
        }

        /// <summary>Gets or sets the actor or class maximum SP Final.</summary>
        public int MaxSPFinal
        {
            get { return maxSPFinal; }
            set { maxSPFinal = value; }
        }

        /// <summary>Gets or sets the actor or class custom strengh checkBox.</summary>
        public bool CustomStr
        {
            get { return customStr; }
            set { customStr = value; }
        }

        /// <summary>Gets or sets the actor or class strengh Initial.</summary>
        public int StrInitial
        {
            get { return strInitial; }
            set { strInitial = value; }
        }

        /// <summary>Gets or sets the actor or class strengh Final.</summary>
        public int StrFinal
        {
            get { return strFinal; }
            set { strFinal = value; }
        }

        /// <summary>Gets or sets the actor or class custom dexterity checkBox.</summary>
        public bool CustomDex
        {
            get { return customDex; }
            set { customDex = value; }
        }

        /// <summary>Gets or sets the actor or class dexterity Initial.</summary>
        public int DexInitial
        {
            get { return dexInitial; }
            set { dexInitial = value; }
        }

        /// <summary>Gets or sets the actor or class dexterity Final.</summary>
        public int DexFinal
        {
            get { return dexFinal; }
            set { dexFinal = value; }
        }

        /// <summary>Gets or sets the actor or class custom agility checkBox.</summary>
        public bool CustomAgi
        {
            get { return customAgi; }
            set { customAgi = value; }
        }

        /// <summary>Gets or sets the actor or class agility Initial.</summary>
        public int AgiInitial
        {
            get { return agiInitial; }
            set { agiInitial = value; }
        }

        /// <summary>Gets or sets the actor or class agility Final.</summary>
        public int AgiFinal
        {
            get { return agiFinal; }
            set { agiFinal = value; }
        }

        /// <summary>Gets or sets the actor or class custom intelligence checkBox.</summary>
        public bool CustomInt
        {
            get { return customInt; }
            set { customInt = value; }
        }

        /// <summary>Gets or sets the actor or class intelligence Initial.</summary>
        public int IntInitial
        {
            get { return intInitial; }
            set { intInitial = value; }
        }

        /// <summary>Gets or sets the actor or class intelligence Final.</summary>
        public int IntFinal
        {
            get { return intFinal; }
            set { intFinal = value; }
        }

        #endregion

        #region Parameter Rate

        /// <summary>Gets or sets the actor or class custom strengh attack rate checkBox.</summary>
        public bool CustomStrRate
        {
            get { return customStrRate; }
            set { customStrRate = value; }
        }

        /// <summary>Gets or sets the actor or class strengh attack rate.</summary>
        public decimal StrRate
        {
            get { return strRate; }
            set { strRate = value; }
        }

        /// <summary>Gets or sets the actor or class custom dexterity attack rate checkBox.</summary>
        public bool CustomDexRate
        {
            get { return customDexRate; }
            set { customDexRate = value; }
        }

        /// <summary>Gets or sets the actor or class dexterity attack rate.</summary>
        public decimal DexRate
        {
            get { return dexRate; }
            set { dexRate = value; }
        }

        /// <summary>Gets or sets the actor or class custom agility attack rate checkBox.</summary>
        public bool CustomAgiRate
        {
            get { return customAgiRate; }
            set { customAgiRate = value; }
        }

        /// <summary>Gets or sets the actor or class agility attack rate.</summary>
        public decimal AgiRate
        {
            get { return agiRate; }
            set { agiRate = value; }
        }

        /// <summary>Gets or sets the actor or class custom intelligence attack rate checkBox.</summary>
        public bool CustomIntRate
        {
            get { return customIntRate; }
            set { customIntRate = value; }
        }

        /// <summary>Gets or sets the actor or class intelligence attack rate.</summary>
        public decimal IntRate
        {
            get { return intRate; }
            set { intRate = value; }
        }

        /// <summary>Gets or sets the actor or class custom physical defense attack rate checkBox.</summary>
        public bool CustomPDefRate
        {
            get { return customPDefRate; }
            set { customPDefRate = value; }
        }

        /// <summary>Gets or sets the actor or class physical defense attack rate.</summary>
        public decimal PDefRate
        {
            get { return pdefRate; }
            set { pdefRate = value; }
        }

        /// <summary>Gets or sets the actor or class custom magical defense attack rate checkBox.</summary>
        public bool CustomMDefRate
        {
            get { return customMDefRate; }
            set { customMDefRate = value; }
        }

        /// <summary>Gets or sets the actor or class magical defense attack rate.</summary>
        public decimal MDefRate
        {
            get { return mdefRate; }
            set { mdefRate = value; }
        }

        /// <summary>Gets or sets the actor or class custom guard rate checkBox.</summary>
        public bool CustomGuardRate
        {
            get { return customGuardRate; }
            set { customGuardRate = value; }
        }

        /// <summary>Gets or sets the actor or class guard rate.</summary>
        public decimal GuardRate
        {
            get { return guardRate; }
            set { guardRate = value; }
        }

        /// <summary>Gets or sets the actor or class custom evasion rate checkBox.</summary>
        public bool CustomEvaRate
        {
            get { return customEvaRate; }
            set { customEvaRate = value; }
        }

        /// <summary>Gets or sets the actor or class evasion rate.</summary>
        public decimal EvaRate
        {
            get { return evaRate; }
            set { evaRate = value; }
        }

        #endregion

        #region Defense against Attack Critical

        /// <summary>Gets or sets the actor or class custom defense agaisnt attack critical rate checkBox.</summary>
        public bool CustomDefCritRate
        {
            get { return customDefCritRate; }
            set { customDefCritRate = value; }
        }

        /// <summary>Gets or sets the actor or class defense agaisnt attack critical rate.</summary>
        public decimal DefCritRate
        {
            get { return defCritRate; }
            set { defCritRate = value; }
        }

        /// <summary>Gets or sets the actor or class custom defense agaisnt attack critical damage checkBox.</summary>
        public bool CustomDefCritDamage
        {
            get { return customDefCritDamage; }
            set { customDefCritDamage = value; }
        }

        /// <summary>Gets or sets the actor or class defense agaisnt attack critical damage.</summary>
        public decimal DefCritDamage
        {
            get { return defCritDamage; }
            set { defCritDamage = value; }
        }

        /// <summary>Gets or sets the actor or class custom defense agaisnt attack special critical rate checkBox.</summary>
        public bool CustomDefSpCritRate
        {
            get { return customDefSpCritRate; }
            set { customDefSpCritRate = value; }
        }

        /// <summary>Gets or sets the actor or class defense agaisnt attack special critical rate.</summary>
        public decimal DefSpCritRate
        {
            get { return defSpCritRate; }
            set { defSpCritRate = value; }
        }

        /// <summary>Gets or sets the actor or class custom defense agaisnt attack special critical damage checkBox.</summary>
        public bool CustomDefSpCritDamage
        {
            get { return customDefSpCritDamage; }
            set { customDefSpCritDamage = value; }
        }

        /// <summary>Gets or sets the actor or class defense agaisnt attack special critical damage.</summary>
        public decimal DefSpCritDamage
        {
            get { return defSpCritDamage; }
            set { defSpCritDamage = value; }
        }

        #endregion

        #region Defense against Skill Critical

        /// <summary>Gets or sets the actor or class custom defense agaisnt skill critical rate checkBox.</summary>
        public bool CustomDefSkillCritRate
        {
            get { return customDefSkillCritRate; }
            set { customDefSkillCritRate = value; }
        }

        /// <summary>Gets or sets the actor or class defense agaisnt skill critical rate.</summary>
        public decimal DefSkillCritRate
        {
            get { return defSkillCritRate; }
            set { defSkillCritRate = value; }
        }

        /// <summary>Gets or sets the actor or class custom defense agaisnt skill critical damage checkBox.</summary>
        public bool CustomDefSkillCritDamage
        {
            get { return customDefSkillCritDamage; }
            set { customDefSkillCritDamage = value; }
        }

        /// <summary>Gets or sets the actor or class defense agaisnt skill critical damage.</summary>
        public decimal DefSkillCritDamage
        {
            get { return defSkillCritDamage; }
            set { defSkillCritDamage = value; }
        }

        /// <summary>Gets or sets the actor or class custom defense agaisnt skill special critical rate checkBox.</summary>
        public bool CustomDefSkillSpCritRate
        {
            get { return customDefSkillSpCritRate; }
            set { customDefSkillSpCritRate = value; }
        }

        /// <summary>Gets or sets the actor or class defense agaisnt skill special critical rate.</summary>
        public decimal DefSkillSpCritRate
        {
            get { return defSkillSpCritRate; }
            set { defSkillSpCritRate = value; }
        }

        /// <summary>Gets or sets the actor or class custom defense agaisnt skill special critical damage checkBox.</summary>
        public bool CustomDefSkillSpCritDamage
        {
            get { return customDefSkillSpCritDamage; }
            set { customDefSkillSpCritDamage = value; }
        }

        /// <summary>Gets or sets the actor or class defense agaisnt skill special critical damage.</summary>
        public decimal DefSkillSpCritDamage
        {
            get { return defSkillSpCritDamage; }
            set { defSkillSpCritDamage = value; }
        }

        #endregion

        #region Unarmed Attack

        /// <summary>Gets or sets the actor or class custom unarmed attack checkBox.</summary>
        public bool CustomAtk
        {
            get { return customAtk; }
            set { customAtk = value; }
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
        public bool CustomHit
        {
            get { return customHit; }
            set { customHit = value; }
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
        public bool CustomAnim
        {
            get { return customAnim; }
            set { customAnim = value; }
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
        public bool CustomParamForce
        {
            get { return customParamForce; }
            set { customParamForce = value; }
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
        public bool CustomDefenseForce
        {
            get { return customDefenseForce; }
            set { customDefenseForce = value; }
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

        #region Unarmed Critical

        /// <summary>Gets or sets the actor or class custom unarmed critical rate checkBox.</summary>
        public bool CustomCritRate
        {
            get { return customCritRate; }
            set { customCritRate = value; }
        }

        /// <summary>Gets or sets the actor or class unarmed critical rate.</summary>
        public decimal CritRate
        {
            get { return critRate; }
            set { critRate = value; }
        }

        /// <summary>Gets or sets the actor or class custom unarmed critical damage checkBox.</summary>
        public bool CustomCritDamage
        {
            get { return customCritDamage; }
            set { customCritDamage = value; }
        }

        /// <summary>Gets or sets the actor or class unarmed critical damage.</summary>
        public decimal CritDamage
        {
            get { return critDamage; }
            set { critDamage = value; }
        }

        /// <summary>Gets or sets the actor or class custom unarmed critical guard rate reduction checkBox.</summary>
        public bool CustomCritDefGuard
        {
            get { return customCritDefGuard; }
            set { customCritDefGuard = value; }
        }

        /// <summary>Gets or sets the actor or class unarmed critical guard rate reduction.</summary>
        public decimal CritDefGuard
        {
            get { return critDefGuard; }
            set { critDefGuard = value; }
        }

        /// <summary>Gets or sets the actor or class custom unarmed critical evasion rate reduction checkBox.</summary>
        public bool CustomCritDefEva
        {
            get { return customCritDefEva; }
            set { customCritDefEva = value; }
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
        public bool CustomSpCritRate
        {
            get { return customSpCritRate; }
            set { customSpCritRate = value; }
        }

        /// <summary>Gets or sets the actor or class unarmed critical rate.</summary>
        public decimal SpCritRate
        {
            get { return spCritRate; }
            set { spCritRate = value; }
        }

        /// <summary>Gets or sets the actor or class custom unarmed special critical damage checkBox.</summary>
        public bool CustomSpCritDamage
        {
            get { return customSpCritDamage; }
            set { customSpCritDamage = value; }
        }

        /// <summary>Gets or sets the actor or class unarmed special critical damage.</summary>
        public decimal SpCritDamage
        {
            get { return spCritDamage; }
            set { spCritDamage = value; }
        }

        /// <summary>Gets or sets the actor or class custom unarmed special critical guard rate reduction checkBox.</summary>
        public bool CustomSpCritDefGuard
        {
            get { return customSpCritDefGuard; }
            set { customSpCritDefGuard = value; }
        }

        /// <summary>Gets or sets the actor or class unarmed special critical guard rate reduction.</summary>
        public decimal SpCritDefGuard
        {
            get { return spCritDefGuard; }
            set { spCritDefGuard = value; }
        }

        /// <summary>Gets or sets the actor or class custom unarmed special critical evasion rate reduction checkBox.</summary>
        public bool CustomSpCritDefEva
        {
            get { return customSpCritDefEva; }
            set { customSpCritDefEva = value; }
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
        public bool CustomPDef
        {
            get { return customPDef; }
            set { customPDef = value; }
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
        public bool CustomMDef
        {
            get { return customMDef; }
            set { customMDef = value; }
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

        public DataPackActor()
        {
            this.id = 0;
            this.name = "";
            this.Reset();
        }

        public DataPackActor(int id)
        {
            this.id = id;
            this.name = defaultName;
            this.Reset();
        }

        public DataPackActor(int inpuntID, string inputName)
        {
            this.id = inpuntID;
            this.name = inputName;
            this.Reset();
        }

        /// <summary>Resets the configuration to default.</summary>
        public void Reset()
        {
            // In family
            this.actorFamily.Clear();

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
            this.maxHPInitial = defaultMaxHPInitial;
            this.maxHPFinal = defaultMaxHPFinal;

            // Maximum SP
            this.customMaxSP = defaultCustomMaxSP;
            this.maxSPInitial = defaultMaxSPInitial;
            this.maxSPFinal = defaultMaxSPFinal;

            // Strengh
            this.customStr = defaultCustomStr;
            this.strInitial = defaultStrInitial;
            this.strFinal = defaultStrFinal;

            // Dexterity
            this.customDex = defaultCustomDex;
            this.dexInitial = defaultDexInitial;
            this.dexFinal = defaultDexFinal;

            // Agility
            this.customAgi = defaultCustomAgi;
            this.agiInitial = defaultAgiInitial;
            this.agiFinal = defaultAgiFinal;

            // Intelligence
            this.customInt = defaultCustomInt;
            this.intInitial = defaultIntInitial;
            this.intFinal = defaultIntFinal;

            // Guard rate
            this.customGuardRate = defaultCustomGuardRate;
            this.guardRate = defaultGuardRate;

            // Evasion rate
            this.customEvaRate = defaultCustomEvaRate;
            this.evaRate = defaultEvaRate;

            #endregion

            #region Parameter Rate

            // Strengh attack rate
            this.customStrRate = defaultCustomStrRate;
            this.strRate = defaultStrRate;

            // Dexterity attack rate
            this.customDexRate = defaultCustomDexRate;
            this.dexRate = defaultDexRate;

            // Agility attack rate
            this.customAgiRate = defaultCustomAgiRate;
            this.agiRate = defaultAgiRate;

            // Intelligence attack rate
            this.customIntRate = defaultCustomIntRate;
            this.intRate = defaultIntRate;

            // Physical defense attack rate
            this.customPDefRate = defaultCustomPDefRate;
            this.pdefRate = defaultPDefRate;

            // Magical defense attack rate
            this.customMDefRate = defaultCustomMDefRate;
            this.mdefRate = defaultMDefRate;

            #endregion

            #region Defense against Attack Critical

            // Defense against attack critical rate
            this.customDefCritRate = defaultCustomDefCritRate;
            this.defCritRate = defaultDefCritRate;

            // Defense against attack critical damage
            this.customDefCritDamage = defaultCustomDefCritDamage;
            this.defCritDamage = defaultDefCritDamage;

            // Defense against attack special critical rate
            this.customDefSpCritRate = defaultCustomDefSpCritRate;
            this.defSpCritRate = defaultDefSpCritRate;

            // Defense against attack special critical damage
            this.customDefSpCritDamage = defaultCustomDefSpCritDamage;
            this.defSpCritDamage = defaultDefSpCritDamage;

            #endregion

            #region Defense against Skill Critical

            // Defense against skill critical rate
            this.customDefSkillCritRate = defaultCustomDefSkillCritRate;
            this.defSkillCritRate = defaultDefSkillCritRate;

            // Defense against skill critical damage
            this.customDefSkillCritDamage = defaultCustomDefSkillCritDamage;
            this.defSkillCritDamage = defaultDefSkillCritDamage;

            // Defense against skill special critical rate
            this.customDefSkillSpCritRate = defaultCustomDefSkillSpCritRate;
            this.defSkillSpCritRate = defaultDefSkillSpCritRate;

            // Defense against skill special critical damage
            this.customDefSkillSpCritDamage = defaultCustomDefSkillSpCritDamage;
            this.defSkillSpCritDamage = defaultDefSkillSpCritDamage;

            #endregion

            #region Unarmed Attack

            // Attack
            this.customAtk = defaultCustomAtk;
            this.atkInitial = defaultAtkInitial;
            this.atkFinal = defaultAtkFinal;

            // Hit Rate
            this.customHit = defaultCustomHit;
            this.hitInitial = defaultHitInitial;
            this.hitFinal = defaultHitFinal;

            // Attack animation
            this.customAnim = defaultCustomAnim;
            this.animCaster = defaultAnimCaster;
            this.animTarget = defaultAnimTarget;

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

            #region Unarmed Critical

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

            #region Unarmed Special Critical

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

            #region Unarmoured Defense

            // Physical defense
            this.customPDef = defaultCustomPDef;
            this.pdefInitial = defaultPDefInitial;
            this.pdefFinal = defaultPDefFinal;

            // Magical defense
            this.customMDef = defaultCustomMDef;
            this.mdefInitial = defaultMDefInitial;
            this.mdefFinal = defaultMDefFinal;

            #endregion
        }

        /// <summary>Compares configurations</summary>
        /// <param name="other">Other configuration of comparison</param>
        /// <returns>True or false.</returns>
        public bool Equals(DataPackActor other)
        {
            if (!this.Equals(other)) return false;

            // ID and name
            if (this.ID != other.ID) return false;
            if (this.Name != other.Name) return false;

            // In family
            if (!this.ActorFamily.Equals(other.ActorFamily)) return false;

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
            if (this.MaxHPInitial != other.MaxHPInitial) return false;
            if (this.MaxHPFinal != other.MaxHPFinal) return false;

            // Maximum SP
            if (this.CustomMaxSP != other.CustomMaxSP) return false;
            if (this.MaxSPInitial != other.MaxSPInitial) return false;
            if (this.MaxSPFinal != other.MaxSPFinal) return false;

            // Strengh
            if (this.CustomStr != other.CustomStr) return false;
            if (this.StrInitial != other.StrInitial) return false;
            if (this.StrFinal != other.StrFinal) return false;

            // Dexterity
            if (this.CustomDex != other.CustomDex) return false;
            if (this.DexInitial != other.DexInitial) return false;
            if (this.DexFinal != other.DexFinal) return false;

            // Agility
            if (this.CustomAgi != other.CustomAgi) return false;
            if (this.AgiInitial != other.AgiInitial) return false;
            if (this.AgiFinal != other.AgiFinal) return false;

            // Intelligence
            if (this.CustomInt != other.CustomInt) return false;
            if (this.IntInitial != other.IntInitial) return false;
            if (this.IntFinal != other.IntFinal) return false;

            // Guard rate
            if (this.CustomGuardRate != other.CustomGuardRate) return false;
            if (this.GuardRate != other.GuardRate) return false;

            // Evasion rate
            if (this.CustomEvaRate != other.CustomEvaRate) return false;
            if (this.EvaRate != other.EvaRate) return false;

            #endregion

            #region Parameter rate

            // Strengh attack rate
            if (this.CustomStrRate != other.CustomStrRate) return false;
            if (this.StrRate != other.StrRate) return false;

            // Dexterity attack rate
            if (this.CustomDexRate != other.CustomDexRate) return false;
            if (this.DexRate != other.DexRate) return false;

            // Agility attackrate
            if (this.CustomAgiRate != other.CustomAgiRate) return false;
            if (this.AgiRate != other.AgiRate) return false;

            // Intelligence attack rate
            if (this.CustomIntRate != other.CustomIntRate) return false;
            if (this.IntRate != other.IntRate) return false;

            // Physical defense attack rate
            if (this.CustomPDefRate != other.CustomPDefRate) return false;
            if (this.PDefRate != other.PDefRate) return false;

            // Magical defense attack rate
            if (this.CustomMDefRate != other.CustomMDefRate) return false;
            if (this.MDefRate != other.MDefRate) return false;

            #endregion

            #region Defense against Attack Critical

            // Defense against attack critical rate
            if (this.CustomDefCritRate != other.CustomDefCritRate) return false;
            if (this.DefCritRate != other.DefCritRate) return false;

            // Defense against attack critical damage
            if (this.CustomDefCritDamage != other.CustomDefCritDamage) return false;
            if (this.DefCritDamage != other.DefCritDamage) return false;

            // Defense against attack special critical rate
            if (this.CustomDefSpCritRate != other.CustomDefSpCritRate) return false;
            if (this.DefSpCritRate != other.DefSpCritRate) return false;

            // Defense against attack special critical damage
            if (this.CustomDefSpCritDamage != other.CustomDefSpCritDamage) return false;
            if (this.DefSpCritDamage != other.DefSpCritDamage) return false;

            #endregion

            #region Defense against Skill Critical

            // Defense against skill critical rate
            if (this.CustomDefSkillCritRate != other.CustomDefSkillCritRate) return false;
            if (this.DefSkillCritRate != other.DefSkillCritRate) return false;

            // Defense against skill critical damage
            if (this.CustomDefSkillCritDamage != other.CustomDefSkillCritDamage) return false;
            if (this.DefSkillCritDamage != other.DefSkillCritDamage) return false;

            // Defense against skill special critical rate
            if (this.CustomDefSkillSpCritRate != other.CustomDefSkillSpCritRate) return false;
            if (this.DefSkillSpCritRate != other.DefSkillSpCritRate) return false;

            // Defense against skill special critical Damage
            if (this.CustomDefSkillSpCritDamage != other.CustomDefSkillSpCritDamage) return false;
            if (this.DefSkillSpCritDamage != other.DefSkillSpCritDamage) return false;

            #endregion

            #region Unarmed Attack

            // Attack
            if (this.CustomAtk != other.CustomAtk) return false;
            if (this.AtkInitial != other.AtkInitial) return false;
            if (this.AtkFinal != other.AtkFinal) return false;

            // Hit rate
            if (this.CustomHit != other.CustomHit) return false;
            if (this.HitInitial != other.HitInitial) return false;
            if (this.HitFinal != other.HitFinal) return false;

            // Attack animation
            if (this.CustomAnim != other.CustomAnim) return false;
            if (this.AnimCaster != other.AnimCaster) return false;
            if (this.AnimTarget != other.AnimTarget) return false;

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

            #region Unarmed Critical

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

            #region Unarmed Special Critical

            // Special critical rate
            if (this.CustomSpCritRate != other.CustomSpCritRate) return false;
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

            #region Unarmoured Defense

            // Physical defense
            if (this.CustomPDef != other.CustomPDef) return false;
            if (this.PDefInitial != other.PDefInitial) return false;
            if (this.PDefFinal != other.PDefFinal) return false;

            // Magical defense
            if (this.CustomMDef != other.CustomMDef) return false;
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

        #region Clone

        /// <summary>Copy configurations</summary>
        /// <param name="other">Other configuration to copy</param>
        public virtual void CloneFrom(DataPackActor other)
        {
            // ID and Name
            this.ID = other.ID;
            this.Name = other.Name;
            
            #region In family

            this.ActorFamily.Clear();
            if (other.ActorFamily.Count > 0)
            {
                foreach (Actor actor in other.ActorFamily)
                {
                    this.ActorFamily.Add(new Actor(actor.ID, actor.Name));
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
            this.MaxHPInitial = other.MaxHPInitial;
            this.MaxHPFinal = other.MaxHPFinal;

            // Maximum SP
            this.CustomMaxSP = other.CustomMaxSP;
            this.MaxSPInitial = other.MaxSPInitial;
            this.MaxSPFinal = other.MaxSPFinal;

            // Strengh
            this.CustomStr = other.CustomStr;
            this.StrInitial = other.StrInitial;
            this.StrFinal = other.StrFinal;

            // Dexterity
            this.CustomDex = other.CustomDex;
            this.DexInitial = other.DexInitial;
            this.DexFinal = other.DexFinal;

            // Agility
            this.CustomAgi = other.CustomAgi;
            this.AgiInitial = other.AgiInitial;
            this.AgiFinal = other.AgiFinal;

            // Intelligence
            this.CustomInt = other.CustomInt;
            this.IntInitial = other.IntInitial;
            this.IntFinal = other.IntFinal;

            // Guard rate
            this.CustomGuardRate = other.CustomGuardRate;
            this.GuardRate = other.GuardRate;

            // Evasion rate
            this.CustomEvaRate = other.CustomEvaRate;
            this.EvaRate = other.EvaRate;

            #endregion

            #region Parameter rate

            // Strengh rate
            this.CustomStrRate = other.CustomStrRate;
            this.StrRate = other.StrRate;

            // Dexterity rate
            this.CustomDexRate = other.CustomDexRate;
            this.DexRate = other.DexRate;

            // Agility rate
            this.CustomAgiRate = other.CustomAgiRate;
            this.AgiRate = other.AgiRate;

            // Intelligence rate
            this.CustomIntRate = other.CustomIntRate;
            this.IntRate = other.IntRate;

            // Physical defense rate
            this.CustomPDefRate = other.CustomPDefRate;
            this.PDefRate = other.PDefRate;

            // Magical defense rate
            this.CustomMDefRate = other.CustomMDefRate;
            this.MDefRate = other.MDefRate;

            #endregion

            #region Defense against Attack Critical

            // Defense against Attack Critical Rate
            this.CustomDefCritRate = other.CustomDefCritRate;
            this.DefCritRate = other.DefCritRate;

            // Defense against Attack Critical Damage
            this.CustomDefCritDamage = other.CustomDefCritDamage;
            this.DefCritDamage = other.DefCritDamage;

            // Defense against Attack Special Critical Rate
            this.CustomDefSpCritRate = other.CustomDefSpCritRate;
            this.DefSpCritRate = other.DefSpCritRate;

            // Defense against Attack Special Critical Damage
            this.CustomDefSpCritDamage = other.CustomDefSpCritDamage;
            this.DefSpCritDamage = other.DefSpCritDamage;

            #endregion

            #region Defense against Skill Critical

            // Defense against Skill Critical Rate
            this.CustomDefSkillCritRate = other.CustomDefSkillCritRate;
            this.DefSkillCritRate = other.DefSkillCritRate;

            // Defense against Skill Critical Damage
            this.CustomDefSkillCritDamage = other.CustomDefSkillCritDamage;
            this.DefSkillCritDamage = other.DefSkillCritDamage;

            // Defense against Skill Special Critical Rate
            this.CustomDefSkillSpCritRate = other.CustomDefSkillSpCritRate;
            this.DefSkillSpCritRate = other.DefSkillSpCritRate;

            // Defense against Skill Special Critical Damage
            this.CustomDefSkillSpCritDamage = other.CustomDefSkillSpCritDamage;
            this.DefSkillSpCritDamage = other.DefSkillSpCritDamage;

            #endregion

            #region Unarmed Attack

            // Attack
            this.CustomAtk = other.CustomAtk;
            this.AtkInitial = other.AtkInitial;
            this.AtkFinal = other.AtkFinal;

            // Hit Rate
            this.CustomHit = other.CustomHit;
            this.HitInitial = other.HitInitial;
            this.HitFinal = other.HitFinal;

            // Attack animation
            this.CustomAnim = other.CustomAnim;
            this.AnimCaster = other.AnimCaster;
            this.AnimTarget = other.AnimTarget;

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

            #region Unarmed Critical

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

            #region Unarmed Special Critical

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

            #region Unarmoured Defense

            // Physical defense
            this.CustomPDef = other.CustomPDef;
            this.PDefInitial = other.PDefInitial;
            this.PDefFinal = other.PDefFinal;

            // Magical defense
            this.CustomMDef = other.CustomMDef;
            this.MDefInitial = other.MDefInitial;
            this.MDefFinal = other.MDefFinal;

            #endregion
        }

        /// <summary>Copy configurations from clipboard</summary>
        /// <param name="other">Other configuration to copy</param>
        public virtual void PasteFrom(DataPackActor other)
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
            this.MaxHPInitial = other.MaxHPInitial;
            this.MaxHPFinal = other.MaxHPFinal;

            // Maximum SP
            this.CustomMaxSP = other.CustomMaxSP;
            this.MaxSPInitial = other.MaxSPInitial;
            this.MaxSPFinal = other.MaxSPFinal;

            // Strengh
            this.CustomStr = other.CustomStr;
            this.StrInitial = other.StrInitial;
            this.StrFinal = other.StrFinal;

            // Dexterity
            this.CustomDex = other.CustomDex;
            this.DexInitial = other.DexInitial;
            this.DexFinal = other.DexFinal;

            // Agility
            this.CustomAgi = other.CustomAgi;
            this.AgiInitial = other.AgiInitial;
            this.AgiFinal = other.AgiFinal;

            // Intelligence
            this.CustomInt = other.CustomInt;
            this.IntInitial = other.IntInitial;
            this.IntFinal = other.IntFinal;

            // Guard rate
            this.CustomGuardRate = other.CustomGuardRate;
            this.GuardRate = other.GuardRate;

            // Evasion rate
            this.CustomEvaRate = other.CustomEvaRate;
            this.EvaRate = other.EvaRate;

            #endregion

            #region Parameter rate

            // Strengh rate
            this.CustomStrRate = other.CustomStrRate;
            this.StrRate = other.StrRate;

            // Dexterity rate
            this.CustomDexRate = other.CustomDexRate;
            this.DexRate = other.DexRate;

            // Agility rate
            this.CustomAgiRate = other.CustomAgiRate;
            this.AgiRate = other.AgiRate;

            // Intelligence rate
            this.CustomIntRate = other.CustomIntRate;
            this.IntRate = other.IntRate;

            // Physical defense rate
            this.CustomPDefRate = other.CustomPDefRate;
            this.PDefRate = other.PDefRate;

            // Magical defense rate
            this.CustomMDefRate = other.CustomMDefRate;
            this.MDefRate = other.MDefRate;

            #endregion

            #region Defense against Attack Critical

            // Defense against Attack Critical Rate
            this.CustomDefCritRate = other.CustomDefCritRate;
            this.DefCritRate = other.DefCritRate;

            // Defense against Attack Critical Damage
            this.CustomDefCritDamage = other.CustomDefCritDamage;
            this.DefCritDamage = other.DefCritDamage;

            // Defense against Attack Special Critical Rate
            this.CustomDefSpCritRate = other.CustomDefSpCritRate;
            this.DefSpCritRate = other.DefSpCritRate;

            // Defense against Attack Special Critical Damage
            this.CustomDefSpCritDamage = other.CustomDefSpCritDamage;
            this.DefSpCritDamage = other.DefSpCritDamage;

            #endregion

            #region Defense against Skill Critical

            // Defense against Skill Critical Rate
            this.CustomDefSkillCritRate = other.CustomDefSkillCritRate;
            this.DefSkillCritRate = other.DefSkillCritRate;

            // Defense against Skill Critical Damage
            this.CustomDefSkillCritDamage = other.CustomDefSkillCritDamage;
            this.DefSkillCritDamage = other.DefSkillCritDamage;

            // Defense against Skill Special Critical Rate
            this.CustomDefSkillSpCritRate = other.CustomDefSkillSpCritRate;
            this.DefSkillSpCritRate = other.DefSkillSpCritRate;

            // Defense against Skill Special Critical Damage
            this.CustomDefSkillSpCritDamage = other.CustomDefSkillSpCritDamage;
            this.DefSkillSpCritDamage = other.DefSkillSpCritDamage;

            #endregion

            #region Unarmed Attack

            // Attack
            this.CustomAtk = other.CustomAtk;
            this.AtkInitial = other.AtkInitial;
            this.AtkFinal = other.AtkFinal;

            // Hit Rate
            this.CustomHit = other.CustomHit;
            this.HitInitial = other.HitInitial;
            this.HitFinal = other.HitFinal;

            // Attack animation
            this.CustomAnim = other.CustomAnim;
            this.AnimCaster = other.AnimCaster;
            this.AnimTarget = other.AnimTarget;

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

            #region Unarmed Critical

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

            #region Unarmed Special Critical

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

            #region Unarmoured Defense

            // Physical defense
            this.CustomPDef = other.CustomPDef;
            this.PDefInitial = other.PDefInitial;
            this.PDefFinal = other.PDefFinal;

            // Magical defense
            this.CustomMDef = other.CustomMDef;
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
