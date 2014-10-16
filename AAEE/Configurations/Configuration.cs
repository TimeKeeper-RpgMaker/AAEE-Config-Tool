using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Xml.Serialization;

using AAEE.Data;
using AAEE.DataPacks;
using AAEE.Utility;

namespace AAEE.Configurations
{
    /// <summary>Represents the AAEE configuration.</summary>
    [XmlRoot("Configuration")]
    public class Configuration : IResetable
    {
        #region Fields

        private ConfigurationGeneral general;

        private ConfigurationDefault defaultCfg;

        #region Actor

        private List<DataPackActor> actorFamily = new List<DataPackActor>();
        private List<DataPackActor> actorID = new List<DataPackActor>();
        private DataPackActor actorDefault = new DataPackActor();

        #endregion

        #region Class

        private List<DataPackClass> classFamily = new List<DataPackClass>();
        private List<DataPackClass> classID = new List<DataPackClass>();
        private DataPackClass classDefault = new DataPackClass();

        #endregion

        #region Skill

        private List<DataPackSkill> skillFamily = new List<DataPackSkill>();
        private List<DataPackSkill> skillID = new List<DataPackSkill>();
        private DataPackSkill skillDefault = new DataPackSkill();

        #endregion

        #region Passive Skill

        private List<DataPackPassiveSkill> passiveSkillFamily = new List<DataPackPassiveSkill>();
        private List<DataPackPassiveSkill> passiveSkillID = new List<DataPackPassiveSkill>();
        private DataPackPassiveSkill passiveSkillDefault = new DataPackPassiveSkill();

        #endregion

        #region Weapon

        private List<DataPackEquipment> weaponFamily = new List<DataPackEquipment>();
        private List<DataPackEquipment> weaponID = new List<DataPackEquipment>();
        private DataPackEquipment weaponDefault = new DataPackEquipment();

        #endregion

        #region Armor

        private List<DataPackEquipment> armorFamily = new List<DataPackEquipment>();
        private List<DataPackEquipment> armorID = new List<DataPackEquipment>();
        private DataPackEquipment armorDefault = new DataPackEquipment();

        #endregion

        #region Enemy

        private List<DataPackEnemy> enemyFamily = new List<DataPackEnemy>();
        private List<DataPackEnemy> enemyID = new List<DataPackEnemy>();
        private DataPackEnemy enemyDefault = new DataPackEnemy();

        #endregion

        #region State

        private List<DataPackState> stateFamily = new List<DataPackState>();
        private List<DataPackState> stateID = new List<DataPackState>();
        private DataPackState stateDefault = new DataPackState();

        #endregion

        #region Available for Family

        private List<Actor> actorAvailable = new List<Actor>();
        private List<Class> classAvailable = new List<Class>();
        private List<Skill> skillAvailable = new List<Skill>();
        private List<Skill> passiveSkillAvailable = new List<Skill>();
        private List<Weapon> weaponAvailable = new List<Weapon>();
        private List<Armor> armorAvailable = new List<Armor>();
        private List<Enemy> enemyAvailable = new List<Enemy>();
        private List<State> stateAvailable = new List<State>();

        #endregion

        #endregion

        #region Properties

        /// <summary>Gets or sets the general configuration.</summary>
        public ConfigurationGeneral General
        {
            get { return general; }
            set { general = value; }
        }

        /// <summary>Gets or sets the default configuration.</summary>
        public ConfigurationDefault Default
        {
            get { return defaultCfg; }
            set { defaultCfg = value; }
        }

        #region Actor

        /// <summary>Get or sets the actor family configuration.</summary>
        public List<DataPackActor> ActorFamily
        {
            get { return actorFamily; }
            set { actorFamily = new List<DataPackActor>(value); }
        }

        /// <summary>Get or sets the actor ID configuration.</summary>
        public List<DataPackActor> ActorID
        {
            get { return actorID; }
            set { actorID = new List<DataPackActor>(value); }
        }

        /// <summary>Get or sets the actor default configuration.</summary>
        public DataPackActor ActorDefault
        {
            get { return actorDefault; }
            set { actorDefault = value; }
        }

        #endregion

        #region Class

        /// <summary>Get or sets the class family configuration.</summary>
        public List<DataPackClass> ClassFamily
        {
            get { return classFamily; }
            set { classFamily = new List<DataPackClass>(value); }
        }

        /// <summary>Get or sets the class ID configuration.</summary>
        public List<DataPackClass> ClassID
        {
            get { return classID; }
            set { classID = new List<DataPackClass>(value); }
        }

        /// <summary>Get or sets the class default configuration.</summary>
        public DataPackClass ClassDefault
        {
            get { return classDefault; }
            set { classDefault = value; }
        }

        #endregion

        #region Skill

        /// <summary>Get or sets the skill family configuration.</summary>
        public List<DataPackSkill> SkillFamily
        {
            get { return skillFamily; }
            set { skillFamily = new List<DataPackSkill>(value); }
        }

        /// <summary>Get or sets the skill ID configuration.</summary>
        public List<DataPackSkill> SkillID
        {
            get { return skillID; }
            set { skillID = new List<DataPackSkill>(value); }
        }

        /// <summary>Get or sets the skill default configuration.</summary>
        public DataPackSkill SkillDefault
        {
            get { return skillDefault; }
            set { skillDefault = value; }
        }

        #endregion

        #region Passive Skill

        /// <summary>Get or sets the passive skill family configuration.</summary>
        public List<DataPackPassiveSkill> PassiveSkillFamily
        {
            get { return passiveSkillFamily; }
            set { passiveSkillFamily = new List<DataPackPassiveSkill>(value); }
        }

        /// <summary>Get or sets the passive skill ID configuration.</summary>
        public List<DataPackPassiveSkill> PassiveSkillID
        {
            get { return passiveSkillID; }
            set { passiveSkillID = new List<DataPackPassiveSkill>(value); }
        }

        /// <summary>Get or sets the passive skill default configuration.</summary>
        public DataPackPassiveSkill PassiveSkillDefault
        {
            get { return passiveSkillDefault; }
            set { passiveSkillDefault = value; }
        }

        #endregion

        #region Weapon

        /// <summary>Get or sets the weapon family configuration.</summary>
        public List<DataPackEquipment> WeaponFamily
        {
            get { return weaponFamily; }
            set { weaponFamily = new List<DataPackEquipment>(value); }
        }

        /// <summary>Get or sets the weapon ID configuration.</summary>
        public List<DataPackEquipment> WeaponID
        {
            get { return weaponID; }
            set { weaponID = new List<DataPackEquipment>(value); }
        }

        /// <summary>Get or sets the weapon default configuration.</summary>
        public DataPackEquipment WeaponDefault
        {
            get { return weaponDefault; }
            set { weaponDefault = value; }
        }

        #endregion

        #region Armor

        /// <summary>Get or sets the armor family configuration.</summary>
        public List<DataPackEquipment> ArmorFamily
        {
            get { return armorFamily; }
            set { armorFamily = new List<DataPackEquipment>(value); }
        }

        /// <summary>Get or sets the armor ID configuration.</summary>
        public List<DataPackEquipment> ArmorID
        {
            get { return armorID; }
            set { armorID = new List<DataPackEquipment>(value); }
        }

        /// <summary>Get or sets the armor default configuration.</summary>
        public DataPackEquipment ArmorDefault
        {
            get { return armorDefault; }
            set { armorDefault = value; }
        }

        #endregion

        #region Enemy

        /// <summary>Get or sets the enemy family configuration.</summary>
        public List<DataPackEnemy> EnemyFamily
        {
            get { return enemyFamily; }
            set { enemyFamily = new List<DataPackEnemy>(value); }
        }

        /// <summary>Get or sets the enemy ID configuration.</summary>
        public List<DataPackEnemy> EnemyID
        {
            get { return enemyID; }
            set { enemyID = new List<DataPackEnemy>(value); }
        }

        /// <summary>Get or sets the enemy default configuration.</summary>
        public DataPackEnemy EnemyDefault
        {
            get { return enemyDefault; }
            set { enemyDefault = value; }
        }

        #endregion

        #region State

        /// <summary>Get or sets the state family configuration.</summary>
        public List<DataPackState> StateFamily
        {
            get { return stateFamily; }
            set { stateFamily = new List<DataPackState>(value); }
        }

        /// <summary>Get or sets the state ID configuration.</summary>
        public List<DataPackState> StateID
        {
            get { return stateID; }
            set { stateID = new List<DataPackState>(value); }
        }

        /// <summary>Get or sets the state default configuration.</summary>
        public DataPackState StateDefault
        {
            get { return stateDefault; }
            set { stateDefault = value; }
        }

        #endregion

        #region Available for Family

        /// <summary>Get or sets the actor family available actor.</summary>
        public List<Actor> ActorAvailable
        {
            get { return actorAvailable; }
            set { actorAvailable = new List<Actor>(value); }
        }

        /// <summary>Get or sets the class family available class.</summary>
        public List<Class> ClassAvailable
        {
            get { return classAvailable; }
            set { classAvailable = new List<Class>(value); }
        }

        /// <summary>Get or sets the skill family available skill.</summary>
        public List<Skill> SkillAvailable
        {
            get { return skillAvailable; }
            set { skillAvailable = new List<Skill>(value); }
        }

        /// <summary>Get or sets the passive skill family available skill.</summary>
        public List<Skill> PassiveSkillAvailable
        {
            get { return passiveSkillAvailable; }
            set { passiveSkillAvailable = new List<Skill>(value); }
        }

        /// <summary>Get or sets the weapon family available weapon.</summary>
        public List<Weapon> WeaponAvailable
        {
            get { return weaponAvailable; }
            set { weaponAvailable = new List<Weapon>(value); }
        }

        /// <summary>Get or sets the armor family available armor.</summary>
        public List<Armor> ArmorAvailable
        {
            get { return armorAvailable; }
            set { armorAvailable = new List<Armor>(value); }
        }

        /// <summary>Get or sets the enemy family available enemy.</summary>
        public List<Enemy> EnemyAvailable
        {
            get { return enemyAvailable; }
            set { enemyAvailable = new List<Enemy>(value); }
        }

        /// <summary>Get or sets the state family available state.</summary>
        public List<State> StateAvailable
        {
            get { return stateAvailable; }
            set { stateAvailable = new List<State>(value); }
        }

        #endregion

        #endregion

        #region Setup

        public Configuration()
        {
            Update();
            Reset();
        }

        /// <summary>Resets the configuration to default.</summary>
        public void Reset()
        {
            general.Reset();

            defaultCfg.Reset();

            #region Actor

            actorFamily.Clear();
            actorID.Clear();
            actorDefault.Reset();

            #endregion

            #region Class

            classFamily.Clear();
            classID.Clear();
            classDefault.Reset();

            #endregion

            #region Skill

            skillFamily.Clear();
            skillID.Clear();
            skillDefault.Reset();

            #endregion

            #region Passive Skill

            passiveSkillFamily.Clear();
            passiveSkillID.Clear();
            passiveSkillDefault.Reset();

            #endregion

            #region Weapon

            weaponFamily.Clear();
            weaponID.Clear();
            weaponDefault.Reset();

            #endregion

            #region Armor

            armorFamily.Clear();
            armorID.Clear();
            armorDefault.Reset();

            #endregion

            #region Enemy

            enemyFamily.Clear();
            enemyID.Clear();
            enemyDefault.Reset();

            #endregion

            #region State

            stateFamily.Clear();
            stateID.Clear();
            stateDefault.Reset();

            #endregion
        }

        /// <summary>Updates after the configuration has been loaded from a file.</summary>
        public void Update()
        {
            if (general == null) general = new ConfigurationGeneral();

            if (defaultCfg == null) defaultCfg = new ConfigurationDefault();

            #region Actor

            if (actorFamily == null) actorFamily = new List<DataPackActor>();
            if (actorID == null) actorID = new List<DataPackActor>();
            if (actorDefault == null) actorDefault = new DataPackActor();

            #endregion

            #region Class

            if (classFamily == null) classFamily = new List<DataPackClass>();
            if (classID == null) classID = new List<DataPackClass>();
            if (classDefault == null) classDefault = new DataPackClass();

            #endregion

            #region Skill

            if (skillFamily == null) skillFamily = new List<DataPackSkill>();
            if (skillID == null) skillID = new List<DataPackSkill>();
            if (skillDefault == null) skillDefault = new DataPackSkill();

            #endregion

            #region Passive Skill

            if (passiveSkillFamily == null) passiveSkillFamily = new List<DataPackPassiveSkill>();
            if (passiveSkillID == null) passiveSkillID = new List<DataPackPassiveSkill>();
            if (passiveSkillDefault == null) passiveSkillDefault = new DataPackPassiveSkill();

            #endregion

            #region Weapon

            if (weaponFamily == null) weaponFamily = new List<DataPackEquipment>();
            if (weaponID == null) weaponID = new List<DataPackEquipment>();
            if (weaponDefault == null) weaponDefault = new DataPackEquipment();

            #endregion

            #region Armor

            if (armorFamily == null) armorFamily = new List<DataPackEquipment>();
            if (armorID == null) armorID = new List<DataPackEquipment>();
            if (armorDefault == null) armorDefault = new DataPackEquipment();

            #endregion

            #region Enemy

            if (enemyFamily == null) enemyFamily = new List<DataPackEnemy>();
            if (enemyID == null) enemyID = new List<DataPackEnemy>();
            if (enemyDefault == null) enemyDefault = new DataPackEnemy();

            #endregion

            #region State

            if (stateFamily == null) stateFamily = new List<DataPackState>();
            if (stateID == null) stateID = new List<DataPackState>();
            if (stateDefault == null) stateDefault = new DataPackState();

            #endregion

            #region Available for Family

            if (actorAvailable == null) actorAvailable = new List<Actor>();
            if (classAvailable == null) classAvailable = new List<Class>();
            if (skillAvailable == null) skillAvailable = new List<Skill>();
            if (passiveSkillAvailable == null) passiveSkillAvailable = new List<Skill>();
            if (weaponAvailable == null) weaponAvailable = new List<Weapon>();
            if (armorAvailable == null) armorAvailable = new List<Armor>();
            if (enemyAvailable == null) enemyAvailable = new List<Enemy>();
            if (stateAvailable == null) stateAvailable = new List<State>();

            #endregion
        }

        #endregion

        #region Behavior

        /// <summary>Compares configurations</summary>
        /// <param name="other">Other configuration of comparison</param>
        /// <returns>True or false.</returns>
        public bool Equals(Configuration other)
        {
            if (!this.General.Equals(other.General)) return false;

            if (!this.Default.Equals(other.Default)) return false;

            #region Actor

            if (!this.ActorFamily.Equals(other.ActorFamily)) return false;
            if (!this.ActorID.Equals(other.ActorID)) return false;
            if (!this.ActorDefault.Equals(other.ActorDefault)) return false;

            #endregion

            #region Class

            if (!this.ClassFamily.Equals(other.ClassFamily)) return false;
            if (!this.ClassID.Equals(other.ClassID)) return false;
            if (!this.ClassDefault.Equals(other.ClassDefault)) return false;

            #endregion

            #region Skill

            if (!this.SkillFamily.Equals(other.SkillFamily)) return false;
            if (!this.SkillID.Equals(other.SkillID)) return false;
            if (!this.SkillDefault.Equals(other.SkillDefault)) return false;

            #endregion

            #region Passive Skill

            if (!this.PassiveSkillFamily.Equals(other.PassiveSkillFamily)) return false;
            if (!this.PassiveSkillID.Equals(other.PassiveSkillID)) return false;
            if (!this.PassiveSkillDefault.Equals(other.PassiveSkillDefault)) return false;

            #endregion

            #region Weapon

            if (!this.WeaponFamily.Equals(other.WeaponFamily)) return false;
            if (!this.WeaponID.Equals(other.WeaponID)) return false;
            if (!this.WeaponDefault.Equals(other.WeaponDefault)) return false;

            #endregion

            #region Armor

            if (!this.ArmorFamily.Equals(other.ArmorFamily)) return false;
            if (!this.ArmorID.Equals(other.ArmorID)) return false;
            if (!this.ArmorDefault.Equals(other.ArmorDefault)) return false;

            #endregion

            #region Enemy

            if (!this.EnemyFamily.Equals(other.EnemyFamily)) return false;
            if (!this.EnemyID.Equals(other.EnemyID)) return false;
            if (!this.EnemyDefault.Equals(other.EnemyDefault)) return false;

            #endregion

            #region State

            if (!this.StateFamily.Equals(other.StateFamily)) return false;
            if (!this.StateID.Equals(other.StateID)) return false;
            if (!this.StateDefault.Equals(other.StateDefault)) return false;

            #endregion

            #region Available for Family

            if (!this.ActorAvailable.Equals(other.ActorAvailable)) return false;
            if (!this.ClassAvailable.Equals(other.ClassAvailable)) return false;
            if (!this.SkillAvailable.Equals(other.SkillAvailable)) return false;
            if (!this.PassiveSkillAvailable.Equals(other.PassiveSkillAvailable)) return false;
            if (!this.WeaponAvailable.Equals(other.WeaponAvailable)) return false;
            if (!this.ArmorAvailable.Equals(other.ArmorAvailable)) return false;
            if (!this.EnemyAvailable.Equals(other.EnemyAvailable)) return false;
            if (!this.StateAvailable.Equals(other.StateAvailable)) return false;

            #endregion

            return true;
        }

        #endregion
    }
}
