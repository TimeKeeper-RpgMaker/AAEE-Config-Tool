using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using AAEE.Data;
using AAEE.Utility;

namespace AAEE.DataPacks
{
    /// <summary>This class serves as datapack for enemy configuration.</summary>
    [XmlType("DataPackEnemy")]
    public class DataPackEnemy : IResetable
    {
        #region Defaults

        // Default name
        private string defaultName = "New Family";

        #region Parameter

        // Default maximum HP
        private bool defaultCustomMaxHP = false;
        private int defaultMaxHPInitial = 0;

        // Default maximum SP
        private bool defaultCustomMaxSP = false;
        private int defaultMaxSPInitial = 0;

        // Default strengh
        private bool defaultCustomStr = false;
        private int defaultStrInitial = 0;

        // Default dexterity
        private bool defaultCustomDex = false;
        private int defaultDexInitial = 0;

        // Default agility
        private bool defaultCustomAgi = false;
        private int defaultAgiInitial = 0;

        // Default intelligence
        private bool defaultCustomInt = false;
        private int defaultIntInitial = 0;

        // Default guard rate
        private bool defaultCustomGuardRate = false;
        private decimal defaultGuardRate = 50;

        // Default evasion rate
        private bool defaultCustomEvaRate = false;
        private decimal defaultEvaRate = 8;

        #endregion

        #region Parameter attack rate

        // Default strengh attack rate
        private bool defaultCustomStrRate = false;
        private decimal defaultStrRate = 5;

        // Default dexterity attack rate
        private bool defaultCustomDexRate = false;
        private decimal defaultDexRate = 5;

        // Default agility attack rate
        private bool defaultCustomAgiRate = false;
        private decimal defaultAgiRate = 5;

        // Default intelligence attack rate
        private bool defaultCustomIntRate = false;
        private decimal defaultIntRate = 5;

        // Default physical defense attack rate
        private bool defaultCustomPDefRate = false;
        private decimal defaultPDefRate = 50;

        // Default magical defense attack rate
        private bool defaultCustomMDefRate = false;
        private decimal defaultMDefRate = 50;

        #endregion

        #region Defense against Attack Critical

        // Default defense against attack critical rate
        private bool defaultCustomDefCritRate = false;
        private decimal defaultDefCritRate = 0;

        // Default defense against attack critical damage
        private bool defaultCustomDefCritDamage = false;
        private decimal defaultDefCritDamage = 0;

        // Default defense against attack special critical rate
        private bool defaultCustomDefSpCritRate = false;
        private decimal defaultDefSpCritRate = 0;

        // Default defense against attack special critical damage
        private bool defaultCustomDefSpCritDamage = false;
        private decimal defaultDefSpCritDamage = 0;

        #endregion

        #region Defense against Skill Critical

        // Default defense against skill critical rate
        private bool defaultCustomDefSkillCritRate = false;
        private decimal defaultDefSkillCritRate = 0;

        // Default defense against skill critical damage
        private bool defaultCustomDefSkillCritDamage = false;
        private decimal defaultDefSkillCritDamage = 0;

        // Default defense against skill special critical rate
        private bool defaultCustomDefSkillSpCritRate = false;
        private decimal defaultDefSkillSpCritRate = 0;

        // Default defense against skill special critical damage
        private bool defaultCustomDefSkillSpCritDamage = false;
        private decimal defaultDefSkillSpCritDamage = 0;

        #endregion

        #region Attack

        // Default attack
        private bool defaultCustomAtk = false;
        private int defaultAtkInitial = 0;

        // Default hit rate
        private bool defaultCustomHit = false;
        private decimal defaultHitInitial = 0;

        // Default parameter attack force
        private bool defaultCustomParamForce = false;
        private decimal defaultStrForce = 100;
        private decimal defaultDexForce = 0;
        private decimal defaultAgiForce = 0;
        private decimal defaultIntForce = 0;

        // Default defense attack force
        private bool defaultCustomDefenseForce = false;
        private decimal defaultPDefForce = 100;
        private decimal defaultMDefForce = 0;

        #endregion

        #region Critical

        // Default critical rate
        private bool defaultCustomCritRate = false;
        private decimal defaultCritRate = 4;

        // Default critical damage
        private bool defaultCustomCritDamage = false;
        private decimal defaultCritDamage = 2;

        // Default critical guard rate reduction
        private bool defaultCustomCritDefGuard = false;
        private decimal defaultCritDefGuard = 0;

        // Default critical evasion rate reduction
        private bool defaultCustomCritDefEva = false;
        private decimal defaultCritDefEva = 0;

        #endregion

        #region Special Critical

        // Default special critical rate
        private bool defaultCustomSpCritRate = false;
        private decimal defaultSpCritRate = 5;

        // Default special critical damage
        private bool defaultCustomSpCritDamage = false;
        private decimal defaultSpCritDamage = 3;

        // Default special critical guard rate reduction
        private bool defaultCustomSpCritDefGuard = false;
        private decimal defaultSpCritDefGuard = 25;

        // Default special critical evasion rate reduction
        private bool defaultCustomSpCritDefEva = false;
        private decimal defaultSpCritDefEva = 50;

        #endregion

        #region Defense

        // Default physical defense
        private bool defaultCustomPDef = false;
        private int defaultPDefInitial = 0;

        // Default magical defense
        private bool defaultCustomMDef = false;
        private int defaultMDefInitial = 0;

        #endregion

        #endregion

        #region Fields

        // Id and name
        private int id;
        private string name;

        #region In Family

        private List<Enemy> enemyFamily = new List<Enemy>();

        #endregion

        #region Parameter

        // Maximum HP
        private bool customMaxHP;
        private int maxHPInitial;

        // Maximum SP
        private bool customMaxSP;
        private int maxSPInitial;

        // Strengh
        private bool customStr;
        private int strInitial;

        // Dexterity
        private bool customDex;
        private int dexInitial;

        // Agility
        private bool customAgi;
        private int agiInitial;

        // Intelligence
        private bool customInt;
        private int intInitial;

        // Guard rate
        private bool customGuardRate;
        private decimal guardRate;

        // Evasion rate
        private bool customEvaRate;
        private decimal evaRate;

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

        #region Defense

        // Physical defense
        private bool customPDef;
        private int pdefInitial;

        // Magical defense
        private bool customMDef;
        private int mdefInitial;

        #endregion

        #endregion

        #region Properties

        /// <summary>Gets or sets the enemy ID.</summary>
        [XmlAttribute()]
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>Gets or sets the enemy name.</summary>
        [XmlAttribute()]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        #region In Family

        /// <summary>Gets or sets the list of enemy in the family.</summary>
        public List<Enemy> EnemyFamily
        {
            get { return enemyFamily; }
            set { enemyFamily = new List<Enemy>(value); }
        }

        #endregion

        #region Parameter

        /// <summary>Gets or sets the enemy custom maximum HP checkBox.</summary>
        public bool CustomMaxHP
        {
            get { return customMaxHP; }
            set { customMaxHP = value; }
        }
        /// <summary>Gets or sets the enemy maximum HP Initial.</summary>
        public int MaxHPInitial
        {
            get { return maxHPInitial; }
            set { maxHPInitial = value; }
        }
        /// <summary>Gets or sets the enemy custom maximum SP checkBox.</summary>
        public bool CustomMaxSP
        {
            get { return customMaxSP; }
            set { customMaxSP = value; }
        }
        /// <summary>Gets or sets the enemy maximum SP Initial.</summary>
        public int MaxSPInitial
        {
            get { return maxSPInitial; }
            set { maxSPInitial = value; }
        }
        /// <summary>Gets or sets the enemy custom strengh checkBox.</summary>
        public bool CustomStr
        {
            get { return customStr; }
            set { customStr = value; }
        }
        /// <summary>Gets or sets the enemy strengh Initial.</summary>
        public int StrInitial
        {
            get { return strInitial; }
            set { strInitial = value; }
        }
        /// <summary>Gets or sets the enemy custom dexterity checkBox.</summary>
        public bool CustomDex
        {
            get { return customDex; }
            set { customDex = value; }
        }
        /// <summary>Gets or sets the enemy dexterity Initial.</summary>
        public int DexInitial
        {
            get { return dexInitial; }
            set { dexInitial = value; }
        }
        /// <summary>Gets or sets the enemy custom agility checkBox.</summary>
        public bool CustomAgi
        {
            get { return customAgi; }
            set { customAgi = value; }
        }
        /// <summary>Gets or sets the enemy agility Initial.</summary>
        public int AgiInitial
        {
            get { return agiInitial; }
            set { agiInitial = value; }
        }
        /// <summary>Gets or sets the enemy custom intelligence checkBox.</summary>
        public bool CustomInt
        {
            get { return customInt; }
            set { customInt = value; }
        }
        /// <summary>Gets or sets the enemy intelligence Initial.</summary>
        public int IntInitial
        {
            get { return intInitial; }
            set { intInitial = value; }
        }

        #endregion

        #region Parameter Rate

        /// <summary>Gets or sets the enemy custom strengh attack rate checkBox.</summary>
        public bool CustomStrRate
        {
            get { return customStrRate; }
            set { customStrRate = value; }
        }
        /// <summary>Gets or sets the enemy strengh attack rate.</summary>
        public decimal StrRate
        {
            get { return strRate; }
            set { strRate = value; }
        }
        /// <summary>Gets or sets the enemy custom dexterity attack rate checkBox.</summary>
        public bool CustomDexRate
        {
            get { return customDexRate; }
            set { customDexRate = value; }
        }
        /// <summary>Gets or sets the enemy dexterity attack rate.</summary>
        public decimal DexRate
        {
            get { return dexRate; }
            set { dexRate = value; }
        }
        /// <summary>Gets or sets the enemy custom agility attack rate checkBox.</summary>
        public bool CustomAgiRate
        {
            get { return customAgiRate; }
            set { customAgiRate = value; }
        }
        /// <summary>Gets or sets the enemy agility attack rate.</summary>
        public decimal AgiRate
        {
            get { return agiRate; }
            set { agiRate = value; }
        }
        /// <summary>Gets or sets the enemy custom intelligence attack rate checkBox.</summary>
        public bool CustomIntRate
        {
            get { return customIntRate; }
            set { customIntRate = value; }
        }
        /// <summary>Gets or sets the enemy intelligence attack rate.</summary>
        public decimal IntRate
        {
            get { return intRate; }
            set { intRate = value; }
        }
        /// <summary>Gets or sets the enemy custom unarmored physical defense attack rate checkBox.</summary>
        public bool CustomPDefRate
        {
            get { return customPDefRate; }
            set { customPDefRate = value; }
        }
        /// <summary>Gets or sets the enemy unarmored physical defense attack rate.</summary>
        public decimal PDefRate
        {
            get { return pdefRate; }
            set { pdefRate = value; }
        }
        /// <summary>Gets or sets the enemy custom unarmored magical defense attack rate checkBox.</summary>
        public bool CustomMDefRate
        {
            get { return customMDefRate; }
            set { customMDefRate = value; }
        }
        /// <summary>Gets or sets the enemy unarmored magical defense attack rate.</summary>
        public decimal MDefRate
        {
            get { return mdefRate; }
            set { mdefRate = value; }
        }
        /// <summary>Gets or sets the enemy custom guard rate checkBox.</summary>
        public bool CustomGuardRate
        {
            get { return customGuardRate; }
            set { customGuardRate = value; }
        }
        /// <summary>Gets or sets the enemy guard rate.</summary>
        public decimal GuardRate
        {
            get { return guardRate; }
            set { guardRate = value; }
        }
        /// <summary>Gets or sets the enemy custom evasion rate checkBox.</summary>
        public bool CustomEvaRate
        {
            get { return customEvaRate; }
            set { customEvaRate = value; }
        }
        /// <summary>Gets or sets the enemy evasion rate.</summary>
        public decimal EvaRate
        {
            get { return evaRate; }
            set { evaRate = value; }
        }

        #endregion

        #region Defense against Attack Critical

        /// <summary>Gets or sets the enemy custom defense agaisnt attack critical rate checkBox.</summary>
        public bool CustomDefCritRate
        {
            get { return customDefCritRate; }
            set { customDefCritRate = value; }
        }
        /// <summary>Gets or sets the enemy defense agaisnt attack critical rate.</summary>
        public decimal DefCritRate
        {
            get { return defCritRate; }
            set { defCritRate = value; }
        }
        /// <summary>Gets or sets the enemy custom defense agaisnt attack critical damage checkBox.</summary>
        public bool CustomDefCritDamage
        {
            get { return customDefCritDamage; }
            set { customDefCritDamage = value; }
        }
        /// <summary>Gets or sets the enemy defense agaisnt attack critical damage.</summary>
        public decimal DefCritDamage
        {
            get { return defCritDamage; }
            set { defCritDamage = value; }
        }
        /// <summary>Gets or sets the enemy custom defense agaisnt attack special critical rate checkBox.</summary>
        public bool CustomDefSpCritRate
        {
            get { return customDefSpCritRate; }
            set { customDefSpCritRate = value; }
        }
        /// <summary>Gets or sets the enemy defense agaisnt attack special critical rate.</summary>
        public decimal DefSpCritRate
        {
            get { return defSpCritRate; }
            set { defSpCritRate = value; }
        }
        /// <summary>Gets or sets the enemy custom defense agaisnt attack special critical damage checkBox.</summary>
        public bool CustomDefSpCritDamage
        {
            get { return customDefSpCritDamage; }
            set { customDefSpCritDamage = value; }
        }
        /// <summary>Gets or sets the enemy defense agaisnt attack special critical damage.</summary>
        public decimal DefSpCritDamage
        {
            get { return defSpCritDamage; }
            set { defSpCritDamage = value; }
        }

        #endregion

        #region Defense against Skill Critical

        /// <summary>Gets or sets the enemy custom defense agaisnt skill critical rate checkBox.</summary>
        public bool CustomDefSkillCritRate
        {
            get { return customDefSkillCritRate; }
            set { customDefSkillCritRate = value; }
        }
        /// <summary>Gets or sets the enemy defense agaisnt skill critical rate.</summary>
        public decimal DefSkillCritRate
        {
            get { return defSkillCritRate; }
            set { defSkillCritRate = value; }
        }
        /// <summary>Gets or sets the enemy custom defense agaisnt skill critical damage checkBox.</summary>
        public bool CustomDefSkillCritDamage
        {
            get { return customDefSkillCritDamage; }
            set { customDefSkillCritDamage = value; }
        }
        /// <summary>Gets or sets the enemy defense agaisnt skill critical damage.</summary>
        public decimal DefSkillCritDamage
        {
            get { return defSkillCritDamage; }
            set { defSkillCritDamage = value; }
        }
        /// <summary>Gets or sets the enemy custom defense agaisnt skill special critical rate checkBox.</summary>
        public bool CustomDefSkillSpCritRate
        {
            get { return customDefSkillSpCritRate; }
            set { customDefSkillSpCritRate = value; }
        }
        /// <summary>Gets or sets the enemy defense agaisnt skill special critical rate.</summary>
        public decimal DefSkillSpCritRate
        {
            get { return defSkillSpCritRate; }
            set { defSkillSpCritRate = value; }
        }
        /// <summary>Gets or sets the enemy custom defense agaisnt skill special critical damage checkBox.</summary>
        public bool CustomDefSkillSpCritDamage
        {
            get { return customDefSkillSpCritDamage; }
            set { customDefSkillSpCritDamage = value; }
        }
        /// <summary>Gets or sets the enemy defense agaisnt skill special critical damage.</summary>
        public decimal DefSkillSpCritDamage
        {
            get { return defSkillSpCritDamage; }
            set { defSkillSpCritDamage = value; }
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

        #region Defense

        /// <summary>Gets or sets the enemy custom unarmored physical defense checkBox.</summary>
        public bool CustomPDef
        {
            get { return customPDef; }
            set { customPDef = value; }
        }
        /// <summary>Gets or sets the enemy unarmored physical defense Initial.</summary>
        public int PDefInitial
        {
            get { return pdefInitial; }
            set { pdefInitial = value; }
        }
        /// <summary>Gets or sets the enemy custom unarmored magical defense checkBox.</summary>
        public bool CustomMDef
        {
            get { return customMDef; }
            set { customMDef = value; }
        }
        /// <summary>Gets or sets the enemy unarmored magical defense Initial.</summary>
        public int MDefInitial
        {
            get { return mdefInitial; }
            set { mdefInitial = value; }
        }

        #endregion

        #endregion

        #region Behavior

        public DataPackEnemy()
        {
            this.id = 0;
            this.name = "";
            this.Reset();
        }

        public DataPackEnemy(int id)
        {
            this.id = id;
            this.name = defaultName;
            this.Reset();
        }

        public DataPackEnemy(int inpuntID, string inputName)
        {
            this.id = inpuntID;
            this.name = inputName;
            this.Reset();
        }

        /// <summary>Resets the configuration to default.</summary>
        public void Reset()
        {
            #region In Family

            this.enemyFamily.Clear();

            #endregion

            #region Parameter

            // Maximum HP
            this.customMaxHP = defaultCustomMaxHP;
            this.maxHPInitial = defaultMaxHPInitial;

            // Maximum SP
            this.customMaxSP = defaultCustomMaxSP;
            this.maxSPInitial = defaultMaxSPInitial;

            // Strengh
            this.customStr = defaultCustomStr;
            this.strInitial = defaultStrInitial;

            // Dexterity
            this.customDex = defaultCustomDex;
            this.dexInitial = defaultDexInitial;

            // Agility
            this.customAgi = defaultCustomAgi;
            this.agiInitial = defaultAgiInitial;

            // Intelligence
            this.customInt = defaultCustomInt;
            this.intInitial = defaultIntInitial;

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

            #region Defense

            // Physical defense
            this.customPDef = defaultCustomPDef;
            this.pdefInitial = defaultPDefInitial;

            // Magical defense
            this.customMDef = defaultCustomMDef;
            this.mdefInitial = defaultMDefInitial;

            #endregion
        }

        /// <summary>Compares configurations</summary>
        /// <param name="other">Other configuration of comparison</param>
        /// <returns>True or false.</returns>
        public bool Equals(DataPackEnemy other)
        {
            if (!this.Equals(other)) return false;

            // ID and name
            if (this.ID != other.ID) return false;
            if (this.Name != other.Name) return false;

            #region In family

            if (!this.EnemyFamily.Equals(other.EnemyFamily)) return false;

            #endregion

            #region Parameter

            // Maximum HP
            if (this.CustomMaxHP != other.CustomMaxHP) return false;
            if (this.MaxHPInitial != other.MaxHPInitial) return false;

            // Maximum SP
            if (this.CustomMaxSP != other.CustomMaxSP) return false;
            if (this.MaxSPInitial != other.MaxSPInitial) return false;

            // Strengh
            if (this.CustomStr != other.CustomStr) return false;
            if (this.StrInitial != other.StrInitial) return false;

            // Dexterity
            if (this.CustomDex != other.CustomDex) return false;
            if (this.DexInitial != other.DexInitial) return false;

            // Agility
            if (this.CustomAgi != other.CustomAgi) return false;
            if (this.AgiInitial != other.AgiInitial) return false;

            // Intelligence
            if (this.CustomInt != other.CustomInt) return false;
            if (this.IntInitial != other.IntInitial) return false;

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

            #region Defense

            // Physical defense
            if (this.CustomPDef != other.CustomPDef) return false;
            if (this.PDefInitial != other.PDefInitial) return false;

            // Magical defense
            if (this.CustomMDef != other.CustomMDef) return false;
            if (this.MDefInitial != other.MDefInitial) return false;

            #endregion

            return true;
        }

        #endregion

        #region Clone

        /// <summary>Copy configurations</summary>
        /// <param name="other">Other configuration to copy</param>
        public virtual void CloneFrom(DataPackEnemy other)
        {
            // ID and Name
            this.ID = other.ID;
            this.Name = other.Name;
            
            #region In family

            this.EnemyFamily.Clear();
            if (other.EnemyFamily.Count > 0)
            {
                foreach (Enemy enemy in other.EnemyFamily)
                {
                    this.EnemyFamily.Add(new Enemy(enemy.ID, enemy.Name));
                }
            }

            #endregion

            #region Parameter

            // Maximum HP
            this.CustomMaxHP = other.CustomMaxHP;
            this.MaxHPInitial = other.MaxHPInitial;

            // Maximum SP
            this.CustomMaxSP = other.CustomMaxSP;
            this.MaxSPInitial = other.MaxSPInitial;

            // Strengh
            this.CustomStr = other.CustomStr;
            this.StrInitial = other.StrInitial;

            // Dexterity
            this.CustomDex = other.CustomDex;
            this.DexInitial = other.DexInitial;

            // Agility
            this.CustomAgi = other.CustomAgi;
            this.AgiInitial = other.AgiInitial;

            // Intelligence
            this.CustomInt = other.CustomInt;
            this.IntInitial = other.IntInitial;

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

            #region Defense

            // Physical defense
            this.CustomPDef = other.CustomPDef;
            this.PDefInitial = other.PDefInitial;

            // Magical defense
            this.CustomMDef = other.CustomMDef;
            this.MDefInitial = other.MDefInitial;

            #endregion
        }

        /// <summary>Copy configurations from clipboard</summary>
        /// <param name="other">Other configuration to copy</param>
        public virtual void PasteFrom(DataPackEnemy other)
        {
            #region Parameter

            // Maximum HP
            this.CustomMaxHP = other.CustomMaxHP;
            this.MaxHPInitial = other.MaxHPInitial;

            // Maximum SP
            this.CustomMaxSP = other.CustomMaxSP;
            this.MaxSPInitial = other.MaxSPInitial;

            // Strengh
            this.CustomStr = other.CustomStr;
            this.StrInitial = other.StrInitial;

            // Dexterity
            this.CustomDex = other.CustomDex;
            this.DexInitial = other.DexInitial;

            // Agility
            this.CustomAgi = other.CustomAgi;
            this.AgiInitial = other.AgiInitial;

            // Intelligence
            this.CustomInt = other.CustomInt;
            this.IntInitial = other.IntInitial;

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

            #region Defense

            // Physical defense
            this.CustomPDef = other.CustomPDef;
            this.PDefInitial = other.PDefInitial;

            // Magical defense
            this.CustomMDef = other.CustomMDef;
            this.MDefInitial = other.MDefInitial;

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
