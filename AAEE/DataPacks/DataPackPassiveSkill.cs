using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using AAEE.Data;
using AAEE.Utility;

namespace AAEE.DataPacks
{
    /// <summary>This class serves as datapack for passive skill configuration.</summary>
    [XmlType("DataPackPassiveSkill")]
    public class DataPackPassiveSkill : IResetable
    {
        #region Defaults

        // Default name
        private string defaultName = "New Family";

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

        #region Attack

        // Default attack
        private bool defaultCustomAtk = false;
        private decimal defaultAtkMul = 1.0m;
        private int defaultAtkAdd = 0;

        // Default hit rate
        private bool defaultCustomHit = false;
        private decimal defaultHitMul = 1.0m;
        private int defaultHitAdd = 0;

        #endregion

        #region Critical

        // Default critical rate
        private bool defaultCustomCritRate = false;
        private decimal defaultCritRateMul = 1.0m;
        private int defaultCritRateAdd = 0;

        // Default critical damage
        private bool defaultCustomCritDamage = false;
        private decimal defaultCritDamageMul = 1.0m;
        private int defaultCritDamageAdd = 0;

        // Default critical guard rate reduction
        private bool defaultCustomCritDefGuard = false;
        private decimal defaultCritDefGuardMul = 1.0m;
        private int defaultCritDefGuardAdd = 0;

        // Default critical evasion rate reduction
        private bool defaultCustomCritDefEva = false;
        private decimal defaultCritDefEvaMul = 1.0m;
        private int defaultCritDefEvaAdd = 0;

        #endregion

        #region Special critical

        // Default special critical rate
        private bool defaultCustomSpCritRate = false;
        private decimal defaultSpCritRateMul = 1.0m;
        private int defaultSpCritRateAdd = 0;

        // Default special critical damage
        private bool defaultCustomSpCritDamage = false;
        private decimal defaultSpCritDamageMul = 1.0m;
        private int defaultSpCritDamageAdd = 0;

        // Default special critical guard rate reduction
        private bool defaultCustomSpCritDefGuard = false;
        private decimal defaultSpCritDefGuardMul = 1.0m;
        private int defaultSpCritDefGuardAdd = 0;

        // Default special critical evasion rate reduction
        private bool defaultCustomSpCritDefEva = false;
        private decimal defaultSpCritDefEvaMul = 1.0m;
        private int defaultSpCritDefEvaAdd = 0;

        #endregion

        #region Defense

        // Default physical defense
        private bool defaultCustomPDef = false;
        private decimal defaultPDefMul = 1.0m;
        private int defaultPDefAdd = 0;

        // Default magical defense
        private bool defaultCustomMDef = false;
        private decimal defaultMDefMul = 1.0m;
        private int defaultMDefAdd = 0;

        #endregion

        #endregion

        #region Fields

        // Id and name
        private int id;
        private string name;

        #region In Family

        private List<Skill> passiveSkillFamily = new List<Skill>();

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

        #region Attack

        // Default attack
        private bool customAtk;
        private decimal atkMul;
        private int atkAdd;

        // Default hit rate
        private bool customHit;
        private decimal hitMul;
        private int hitAdd;

        #endregion

        #region Critical

        // Default critical rate
        private bool customCritRate;
        private decimal critRateMul;
        private int critRateAdd;

        // Default critical damage
        private bool customCritDamage;
        private decimal critDamageMul;
        private int critDamageAdd;

        // Default critical guard rate reduction
        private bool customCritDefGuard;
        private decimal critDefGuardMul;
        private int critDefGuardAdd;

        // Default critical evasion rate reduction
        private bool customCritDefEva;
        private decimal critDefEvaMul;
        private int critDefEvaAdd;

        #endregion

        #region Special critical

        // Default special critical rate
        private bool customSpCritRate;
        private decimal spCritRateMul;
        private int spCritRateAdd;

        // Default special critical damage
        private bool customSpCritDamage;
        private decimal spCritDamageMul;
        private int spCritDamageAdd;

        // Default special critical guard rate reduction
        private bool customSpCritDefGuard;
        private decimal spCritDefGuardMul;
        private int spCritDefGuardAdd;

        // Default special critical evasion rate reduction
        private bool customSpCritDefEva;
        private decimal spCritDefEvaMul;
        private int spCritDefEvaAdd;

        #endregion

        #region Defense

        // Default physical defense
        private bool customPDef;
        private decimal pdefMul;
        private int pdefAdd;

        // Default magical defense
        private bool customMDef;
        private decimal mdefMul;
        private int mdefAdd;

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

        #region In Family

        /// <summary>Gets or sets the list of passive skill in the family.</summary>
        public List<Skill> PassiveSkillFamily
        {
            get { return passiveSkillFamily; }
            set { passiveSkillFamily = new List<Skill>(value); }
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

        #region Attack

        /// <summary>Gets or sets the equipment custom unarmed attack checkBox.</summary>
        public bool CustomAtk
        {
            get { return customAtk; }
            set { customAtk = value; }
        }
        /// <summary>Gets or sets the equipment unarmed attack multiplier.</summary>
        public decimal AtkMul
        {
            get { return atkMul; }
            set { atkMul = value; }
        }
        /// <summary>Gets or sets the equipment unarmed attack plus.</summary>
        public int AtkAdd
        {
            get { return atkAdd; }
            set { atkAdd = value; }
        }
        /// <summary>Gets or sets the equipment custom unarmed hit rate checkBox.</summary>
        public bool CustomHit
        {
            get { return customHit; }
            set { customHit = value; }
        }
        /// <summary>Gets or sets the equipment unarmed hit rate multiplier.</summary>
        public decimal HitMul
        {
            get { return hitMul; }
            set { hitMul = value; }
        }
        /// <summary>Gets or sets the equipment unarmed hit rate plus.</summary>
        public int HitAdd
        {
            get { return hitAdd; }
            set { hitAdd = value; }
        }

        #endregion

        #region Critical

        /// <summary>Gets or sets the equipment custom unarmed critical rate checkBox.</summary>
        public bool CustomCritRate
        {
            get { return customCritRate; }
            set { customCritRate = value; }
        }
        /// <summary>Gets or sets the equipment unarmed critical rate multiplier.</summary>
        public decimal CritRateMul
        {
            get { return critRateMul; }
            set { critRateMul = value; }
        }
        /// <summary>Gets or sets the equipment unarmed critical rate plus.</summary>
        public int CritRateAdd
        {
            get { return critRateAdd; }
            set { critRateAdd = value; }
        }
        /// <summary>Gets or sets the equipment custom unarmed critical damage checkBox.</summary>
        public bool CustomCritDamage
        {
            get { return customCritDamage; }
            set { customCritDamage = value; }
        }
        /// <summary>Gets or sets the equipment unarmed critical damage multiplier.</summary>
        public decimal CritDamageMul
        {
            get { return critDamageMul; }
            set { critDamageMul = value; }
        }
        /// <summary>Gets or sets the equipment unarmed critical damage plus.</summary>
        public int CritDamageAdd
        {
            get { return critDamageAdd; }
            set { critDamageAdd = value; }
        }
        /// <summary>Gets or sets the equipment custom unarmed critical guard rate reduction checkBox.</summary>
        public bool CustomCritDefGuard
        {
            get { return customCritDefGuard; }
            set { customCritDefGuard = value; }
        }
        /// <summary>Gets or sets the equipment unarmed critical guard rate reduction multiplier.</summary>
        public decimal CritDefGuardMul
        {
            get { return critDefGuardMul; }
            set { critDefGuardMul = value; }
        }
        /// <summary>Gets or sets the equipment unarmed critical guard rate reduction plus.</summary>
        public int CritDefGuardAdd
        {
            get { return critDefGuardAdd; }
            set { critDefGuardAdd = value; }
        }
        /// <summary>Gets or sets the equipment custom unarmed critical evasion rate reduction checkBox.</summary>
        public bool CustomCritDefEva
        {
            get { return customCritDefEva; }
            set { customCritDefEva = value; }
        }
        /// <summary>Gets or sets the equipment unarmed critical evasion rate reduction multiplier.</summary>
        public decimal CritDefEvaMul
        {
            get { return critDefEvaMul; }
            set { critDefEvaMul = value; }
        }
        /// <summary>Gets or sets the equipment unarmed critical evasion rate reduction plus.</summary>
        public int CritDefEvaAdd
        {
            get { return critDefEvaAdd; }
            set { critDefEvaAdd = value; }
        }

        #endregion

        #region Special critical

        /// <summary>Gets or sets the equipment custom unarmed critical rate checkBox.</summary>
        public bool CustomSpCritRate
        {
            get { return customSpCritRate; }
            set { customSpCritRate = value; }
        }
        /// <summary>Gets or sets the equipment unarmed critical rate multiplier.</summary>
        public decimal SpCritRateMul
        {
            get { return spCritRateMul; }
            set { spCritRateMul = value; }
        }
        /// <summary>Gets or sets the equipment unarmed critical rate plus.</summary>
        public int SpCritRateAdd
        {
            get { return spCritRateAdd; }
            set { spCritRateAdd = value; }
        }
        /// <summary>Gets or sets the equipment custom unarmed special critical damage checkBox.</summary>
        public bool CustomSpCritDamage
        {
            get { return customSpCritDamage; }
            set { customSpCritDamage = value; }
        }
        /// <summary>Gets or sets the equipment unarmed special critical damage multiplier.</summary>
        public decimal SpCritDamageMul
        {
            get { return spCritDamageMul; }
            set { spCritDamageMul = value; }
        }
        /// <summary>Gets or sets the equipment unarmed special critical damage plus.</summary>
        public int SpCritDamageAdd
        {
            get { return spCritDamageAdd; }
            set { spCritDamageAdd = value; }
        }
        /// <summary>Gets or sets the equipment custom unarmed special critical guard rate reduction checkBox.</summary>
        public bool CustomSpCritDefGuard
        {
            get { return customSpCritDefGuard; }
            set { customSpCritDefGuard = value; }
        }
        /// <summary>Gets or sets the equipment unarmed special critical guard rate reduction multiplier.</summary>
        public decimal SpCritDefGuardMul
        {
            get { return spCritDefGuardMul; }
            set { spCritDefGuardMul = value; }
        }
        /// <summary>Gets or sets the equipment unarmed special critical guard rate reduction plus.</summary>
        public int SpCritDefGuardAdd
        {
            get { return spCritDefGuardAdd; }
            set { spCritDefGuardAdd = value; }
        }
        /// <summary>Gets or sets the equipment custom unarmed special critical evasion rate reduction checkBox.</summary>
        public bool CustomSpCritDefEva
        {
            get { return customSpCritDefEva; }
            set { customSpCritDefEva = value; }
        }
        /// <summary>Gets or sets the equipment unarmed special critical evasion rate reduction multiplier.</summary>
        public decimal SpCritDefEvaMul
        {
            get { return spCritDefEvaMul; }
            set { spCritDefEvaMul = value; }
        }
        /// <summary>Gets or sets the equipment unarmed special critical evasion rate reduction plus.</summary>
        public int SpCritDefEvaAdd
        {
            get { return spCritDefEvaAdd; }
            set { spCritDefEvaAdd = value; }
        }

        #endregion

        #region Defense

        /// <summary>Gets or sets the equipment custom unarmored physical defense checkBox.</summary>
        public bool CustomPDef
        {
            get { return customPDef; }
            set { customPDef = value; }
        }
        /// <summary>Gets or sets the equipment unarmored physical defense multiplier.</summary>
        public decimal PDefMul
        {
            get { return pdefMul; }
            set { pdefMul = value; }
        }
        /// <summary>Gets or sets the equipment unarmored physical defense plus.</summary>
        public int PDefAdd
        {
            get { return pdefAdd; }
            set { pdefAdd = value; }
        }
        /// <summary>Gets or sets the equipment custom unarmored magical defense checkBox.</summary>
        public bool CustomMDef
        {
            get { return customMDef; }
            set { customMDef = value; }
        }
        /// <summary>Gets or sets the equipment unarmored magical defense multiplier.</summary>
        public decimal MDefMul
        {
            get { return mdefMul; }
            set { mdefMul = value; }
        }
        /// <summary>Gets or sets the equipment unarmored magical defense plus.</summary>
        public int MDefAdd
        {
            get { return mdefAdd; }
            set { mdefAdd = value; }
        }

        #endregion

        #endregion

        #region Behavior

        public DataPackPassiveSkill()
        {
            this.id = 0;
            this.name = "";
            this.Reset();
        }

        public DataPackPassiveSkill(int inpuntID)
        {
            this.id = inpuntID;
            this.name = defaultName;
            this.Reset();
        }

        public DataPackPassiveSkill(int inpuntID, string inputName)
        {
            this.id = inpuntID;
            this.name = inputName;
            this.Reset();
        }

        /// <summary>Resets the configuration to default.</summary>
        public void Reset()
        {
            #region In Family

            this.passiveSkillFamily.Clear();

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

            #region Attack

            // Attack
            this.customAtk = defaultCustomAtk;
            this.atkMul = defaultAtkMul;
            this.atkAdd = defaultAtkAdd;

            // Hit Rate
            this.customHit = defaultCustomHit;
            this.hitMul = defaultHitMul;
            this.hitAdd = defaultHitAdd;

            #endregion

            #region Critical

            // Critical rate
            this.customCritRate = defaultCustomCritRate;
            this.critRateMul = defaultCritRateMul;
            this.critRateAdd = defaultCritRateAdd;

            // Critical damage
            this.customCritDamage = defaultCustomCritDamage;
            this.critDamageMul = defaultCritDamageMul;
            this.critDamageAdd = defaultCritDamageAdd;

            // Critical guard reduction
            this.customCritDefGuard = defaultCustomCritDefGuard;
            this.critDefGuardMul = defaultCritDefGuardMul;
            this.critDefGuardAdd = defaultCritDefGuardAdd;

            // Critical evasion reduction
            this.customCritDefEva = defaultCustomCritDefEva;
            this.critDefEvaMul = defaultCritDefEvaMul;
            this.critDefEvaAdd = defaultCritDefEvaAdd;

            #endregion

            #region Special critical

            // Special critical rate
            this.customSpCritRate = defaultCustomSpCritRate;
            this.spCritRateMul = defaultSpCritRateMul;
            this.spCritRateAdd = defaultSpCritRateAdd;

            // Special critical damage
            this.customSpCritDamage = defaultCustomSpCritDamage;
            this.spCritDamageMul = defaultSpCritDamageMul;
            this.spCritDamageAdd = defaultSpCritDamageAdd;

            // Special critical guard reduction
            this.customSpCritDefGuard = defaultCustomSpCritDefGuard;
            this.spCritDefGuardMul = defaultSpCritDefGuardMul;
            this.spCritDefGuardAdd = defaultSpCritDefGuardAdd;

            // Special critical evasion reduction
            this.customSpCritDefEva = defaultCustomSpCritDefEva;
            this.spCritDefEvaMul = defaultSpCritDefEvaMul;
            this.spCritDefEvaAdd = defaultSpCritDefEvaAdd;

            #endregion

            #region Defense

            // Physical defense
            this.customPDef = defaultCustomPDef;
            this.pdefMul = defaultPDefMul;
            this.pdefAdd = defaultPDefAdd;

            // Magical defense
            this.customMDef = defaultCustomMDef;
            this.mdefMul = defaultMDefMul;
            this.mdefAdd = defaultMDefAdd;

            #endregion
        }

        /// <summary>Compares configurations</summary>
        /// <param name="other">Other configuration of comparison</param>
        /// <returns>True or false.</returns>
        public bool Equals(DataPackPassiveSkill other)
        {
            if (!this.Equals(other)) return false;

            // ID and name
            if (this.ID != other.ID) return false;
            if (this.Name != other.Name) return false;

            #region In family

            if (!this.PassiveSkillFamily.Equals(other.PassiveSkillFamily)) return false;

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

            #region Attack

            // Attack
            if (this.CustomAtk != other.CustomAtk) return false;
            if (this.AtkMul != other.AtkMul) return false;
            if (this.AtkAdd != other.AtkAdd) return false;

            // Hit rate
            if (this.CustomHit != other.CustomHit) return false;
            if (this.HitMul != other.HitMul) return false;
            if (this.HitAdd != other.HitAdd) return false;

            #endregion

            #region Critical

            // Critical Rate
            if (this.CustomCritRate != other.CustomCritRate) return false;
            if (this.CritRateMul != other.CritRateMul) return false;
            if (this.CritRateAdd != other.CritRateAdd) return false;

            // Critical Damage
            if (this.CustomCritDamage != other.CustomCritDamage) return false;
            if (this.CritDamageMul != other.CritDamageMul) return false;
            if (this.CritDamageAdd != other.CritDamageAdd) return false;

            // Critical Guard Reduction
            if (this.CustomCritDefGuard != other.CustomCritDefGuard) return false;
            if (this.CritDefGuardMul != other.CritDefGuardMul) return false;
            if (this.CritDefGuardAdd != other.CritDefGuardAdd) return false;

            // Critical Evasion Reduction
            if (this.CustomCritDefEva != other.CustomCritDefEva) return false;
            if (this.CritDefEvaMul != other.CritDefEvaMul) return false;
            if (this.CritDefEvaAdd != other.CritDefEvaAdd) return false;

            #endregion

            #region Special critical

            // Special critical rate
            if (this.CustomSpCritRate != other.CustomSpCritRate) return false;
            if (this.SpCritRateMul != other.SpCritRateMul) return false;
            if (this.SpCritRateAdd != other.SpCritRateAdd) return false;

            // Special critical damage
            if (this.CustomSpCritDamage != other.CustomSpCritDamage) return false;
            if (this.SpCritDamageMul != other.SpCritDamageMul) return false;
            if (this.SpCritDamageAdd != other.SpCritDamageAdd) return false;

            // Special critical guard reduction
            if (this.CustomSpCritDefGuard != other.CustomSpCritDefGuard) return false;
            if (this.SpCritDefGuardMul != other.SpCritDefGuardMul) return false;
            if (this.SpCritDefGuardAdd != other.SpCritDefGuardAdd) return false;

            // Special critical evasion reduction
            if (this.CustomSpCritDefEva != other.CustomSpCritDefEva) return false;
            if (this.SpCritDefEvaMul != other.SpCritDefEvaMul) return false;
            if (this.SpCritDefEvaAdd != other.SpCritDefEvaAdd) return false;

            #endregion

            #region Defense

            // Physical defense
            if (this.CustomPDef != other.CustomPDef) return false;
            if (this.PDefMul != other.PDefMul) return false;
            if (this.PDefAdd != other.PDefAdd) return false;

            // Magical defense
            if (this.CustomMDef != other.CustomMDef) return false;
            if (this.MDefMul != other.MDefMul) return false;
            if (this.MDefAdd != other.MDefAdd) return false;

            #endregion

            return true;
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

        #region Attack

        /// <summary>Check if the attack bonus is different from the default value.</summary>
        public bool CheckAtk()
        {
            return (this.customAtk && (this.atkMul != this.defaultAtkMul || this.atkAdd != this.defaultAtkAdd));
        }

        /// <summary>Check if the hit rate bonus is different from the default value.</summary>
        public bool CheckHit()
        {
            return (this.customHit && (this.hitMul != this.defaultHitMul || this.hitAdd != this.defaultHitAdd));
        }

        #endregion

        #region Critical

        /// <summary>Check if the critical rate bonus is different from the default value.</summary>
        public bool CheckCritRate()
        {
            return (this.customCritRate && (this.critRateMul != this.defaultCritRateMul || this.critRateAdd != this.defaultCritRateAdd));
        }

        /// <summary>Check if the critical damage bonus is different from the default value.</summary>
        public bool CheckCritDamage()
        {
            return (this.customCritDamage && (this.critDamageMul != this.defaultCritDamageMul || this.critDamageAdd != this.defaultCritDamageAdd));
        }

        /// <summary>Check if the critical guard rate reduction bonus is different from the default value.</summary>
        public bool CheckCritDefGuard()
        {
            return (this.customCritDefGuard && (this.critDefGuardMul != this.defaultCritDefGuardMul || this.critDefGuardAdd != this.defaultCritDefGuardAdd));
        }

        /// <summary>Check if the critical evasion rate reduction bonus is different from the default value.</summary>
        public bool CheckCritDefEva()
        {
            return (this.customCritDefEva && (this.critDefEvaMul != this.defaultCritDefEvaMul || this.critDefEvaAdd != this.defaultCritDefEvaAdd));
        }

        #endregion

        #region Special critical

        /// <summary>Check if the special critical rate bonus is different from the default value.</summary>
        public bool CheckSpCritRate()
        {
            return (this.customSpCritRate && (this.spCritRateMul != this.defaultSpCritRateMul || this.spCritRateAdd != this.defaultSpCritRateAdd));
        }

        /// <summary>Check if the special critical damage bonus is different from the default value.</summary>
        public bool CheckSpCritDamage()
        {
            return (this.customSpCritDamage && (this.spCritDamageMul != this.defaultSpCritDamageMul || this.spCritDamageAdd != this.defaultSpCritDamageAdd));
        }

        /// <summary>Check if the special critical guard rate reduction bonus is different from the default value.</summary>
        public bool CheckSpCritDefGuard()
        {
            return (this.customSpCritDefGuard && (this.spCritDefGuardMul != this.defaultSpCritDefGuardMul || this.spCritDefGuardAdd != this.defaultSpCritDefGuardAdd));
        }

        /// <summary>Check if the special critical evasion rate reduction bonus is different from the default value.</summary>
        public bool CheckSpCritDefEva()
        {
            return (this.customSpCritDefEva && (this.spCritDefEvaMul != this.defaultSpCritDefEvaMul || this.spCritDefEvaAdd != this.defaultSpCritDefEvaAdd));
        }

        #endregion

        #region Defense

        /// <summary>Check if the physical defense bonus is different from the default value.</summary>
        public bool CheckPDef()
        {
            return (this.customPDef && (this.pdefMul != this.defaultPDefMul || this.pdefAdd != this.defaultPDefAdd));
        }

        /// <summary>Check if the magical defense bonus is different from the default value.</summary>
        public bool CheckMDef()
        {
            return (this.customMDef && (this.mdefMul != this.defaultMDefMul || this.mdefAdd != this.defaultMDefAdd));
        }

        #endregion

        #endregion

        #region Clone

        /// <summary>Copy configurations</summary>
        /// <param name="other">Other configuration to copy</param>
        public virtual void CloneFrom(DataPackPassiveSkill other)
        {
            // ID and Name
            this.ID = other.ID;
            this.Name = other.Name;

            #region In family

            this.PassiveSkillFamily.Clear();
            if (other.PassiveSkillFamily.Count > 0)
            {
                foreach (Skill skill in other.PassiveSkillFamily)
                {
                    this.PassiveSkillFamily.Add(new Skill(skill.ID, skill.Name));
                }
            }

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

            #region Attack

            // Attack
            this.CustomAtk = other.CustomAtk;
            this.AtkMul = other.AtkMul;
            this.AtkAdd = other.AtkAdd;

            // Hit Rate
            this.CustomHit = other.CustomHit;
            this.HitMul = other.HitMul;
            this.HitAdd = other.HitAdd;

            #endregion

            #region Critical

            // Critical rate
            this.CustomCritRate = other.CustomCritRate;
            this.CritRateMul = other.CritRateMul;
            this.CritRateAdd = other.CritRateAdd;

            // Critical damage
            this.CustomCritDamage = other.CustomCritDamage;
            this.CritDamageMul = other.CritDamageMul;
            this.CritDamageAdd = other.CritDamageAdd;

            // Critical guard reduction
            this.CustomCritDefGuard = other.CustomCritDefGuard;
            this.CritDefGuardMul = other.CritDefGuardMul;
            this.CritDefGuardAdd = other.CritDefGuardAdd;

            // Critical evasion reduction
            this.CustomCritDefEva = other.CustomCritDefEva;
            this.CritDefEvaMul = other.CritDefEvaMul;
            this.CritDefEvaAdd = other.CritDefEvaAdd;

            #endregion

            #region Special critical

            // Special critical rate
            this.CustomSpCritRate = other.CustomSpCritRate;
            this.SpCritRateMul = other.SpCritRateMul;
            this.SpCritRateAdd = other.SpCritRateAdd;

            // Special critical damage
            this.CustomSpCritDamage = other.CustomSpCritDamage;
            this.SpCritDamageMul = other.SpCritDamageMul;
            this.SpCritDamageAdd = other.SpCritDamageAdd;

            // Special critical guard reduction
            this.CustomSpCritDefGuard = other.CustomSpCritDefGuard;
            this.SpCritDefGuardMul = other.SpCritDefGuardMul;
            this.SpCritDefGuardAdd = other.SpCritDefGuardAdd;

            // Special critical evasion reduction
            this.CustomSpCritDefEva = other.CustomSpCritDefEva;
            this.SpCritDefEvaMul = other.SpCritDefEvaMul;
            this.SpCritDefEvaAdd = other.SpCritDefEvaAdd;

            #endregion

            #region Defense

            // Physical defense
            this.CustomPDef = other.CustomPDef;
            this.PDefMul = other.PDefMul;
            this.PDefAdd = other.PDefAdd;

            // Magical defense
            this.CustomMDef = other.CustomMDef;
            this.MDefMul = other.MDefMul;
            this.MDefAdd = other.MDefAdd;

            #endregion
        }

        /// <summary>Copy configurations from clipboard</summary>
        /// <param name="other">Other configuration to copy</param>
        public virtual void PasteFrom(DataPackPassiveSkill other)
        {
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

            #region Attack

            // Attack
            this.CustomAtk = other.CustomAtk;
            this.AtkMul = other.AtkMul;
            this.AtkAdd = other.AtkAdd;

            // Hit Rate
            this.CustomHit = other.CustomHit;
            this.HitMul = other.HitMul;
            this.HitAdd = other.HitAdd;

            #endregion

            #region Critical

            // Critical rate
            this.CustomCritRate = other.CustomCritRate;
            this.CritRateMul = other.CritRateMul;
            this.CritRateAdd = other.CritRateAdd;

            // Critical damage
            this.CustomCritDamage = other.CustomCritDamage;
            this.CritDamageMul = other.CritDamageMul;
            this.CritDamageAdd = other.CritDamageAdd;

            // Critical guard reduction
            this.CustomCritDefGuard = other.CustomCritDefGuard;
            this.CritDefGuardMul = other.CritDefGuardMul;
            this.CritDefGuardAdd = other.CritDefGuardAdd;

            // Critical evasion reduction
            this.CustomCritDefEva = other.CustomCritDefEva;
            this.CritDefEvaMul = other.CritDefEvaMul;
            this.CritDefEvaAdd = other.CritDefEvaAdd;

            #endregion

            #region Special critical

            // Special critical rate
            this.CustomSpCritRate = other.CustomSpCritRate;
            this.SpCritRateMul = other.SpCritRateMul;
            this.SpCritRateAdd = other.SpCritRateAdd;

            // Special critical damage
            this.CustomSpCritDamage = other.CustomSpCritDamage;
            this.SpCritDamageMul = other.SpCritDamageMul;
            this.SpCritDamageAdd = other.SpCritDamageAdd;

            // Special critical guard reduction
            this.CustomSpCritDefGuard = other.CustomSpCritDefGuard;
            this.SpCritDefGuardMul = other.SpCritDefGuardMul;
            this.SpCritDefGuardAdd = other.SpCritDefGuardAdd;

            // Special critical evasion reduction
            this.CustomSpCritDefEva = other.CustomSpCritDefEva;
            this.SpCritDefEvaMul = other.SpCritDefEvaMul;
            this.SpCritDefEvaAdd = other.SpCritDefEvaAdd;

            #endregion

            #region Defense

            // Physical defense
            this.CustomPDef = other.CustomPDef;
            this.PDefMul = other.PDefMul;
            this.PDefAdd = other.PDefAdd;

            // Magical defense
            this.CustomMDef = other.CustomMDef;
            this.MDefMul = other.MDefMul;
            this.MDefAdd = other.MDefAdd;

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
