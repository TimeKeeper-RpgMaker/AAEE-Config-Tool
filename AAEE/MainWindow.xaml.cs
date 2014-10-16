using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Xceed.Wpf.Toolkit;

using AAEE.Configurations;
using AAEE.Data;
using AAEE.DataPacks;
using AAEE.Utility;
using AAEE.Windows;

namespace AAEE
{
    /// <summary> Logique d'interaction pour MainWindow.xaml </summary>
    public partial class MainWindow : Window
    {
        #region WPF Window

        #region Fields

        // Flag
        private bool updating = false;
        private string lastFileName = "";

        // Index default
        private int lastIndexDefaultEquipType = 0;
        private int lastIndexDefaultEquipList = 0;

        // Index actor
        private int lastIndexActorFamily = 0;
        private int lastIndexActorID = 0;
        private bool lastIndexActorDefault = false;
        private int lastIndexActorEquipType = 0;
        private int lastIndexActorEquipList = 0;

        // Index class
        private int lastIndexClassFamily = 0;
        private int lastIndexClassID = 0;
        private bool lastIndexClassDefault = false;
        private int lastIndexClassEquipType = 0;
        private int lastIndexClassEquipList = 0;

        // Index passive skill
        private int lastIndexSkillFamily = 0;
        private int lastIndexSkillID = 0;
        private bool lastIndexSkillDefault = false;

        // Index passive skill
        private int lastIndexPassiveSkillFamily = 0;
        private int lastIndexPassiveSkillID = 0;
        private bool lastIndexPassiveSkillDefault = false;

        // Index weapon
        private int lastIndexWeaponFamily = 0;
        private int lastIndexWeaponID = 0;
        private bool lastIndexWeaponDefault = false;

        // Index armor
        private int lastIndexArmorFamily = 0;
        private int lastIndexArmorID = 0;
        private bool lastIndexArmorDefault = false;

        // Index enemy
        private int lastIndexEnemyFamily = 0;
        private int lastIndexEnemyID = 0;
        private bool lastIndexEnemyDefault = false;

        // Index state
        private int lastIndexStateFamily = 0;
        private int lastIndexStateID = 0;
        private bool lastIndexStateDefault = false;

        // Data
        public Configuration cfg = new Configuration();
        public GameData gameData = new GameData();

        // Clipboard
        private DataPackActor cachedActor;
        private DataPackClass cachedClass;
        private DataPackSkill cachedSkill;
        private DataPackPassiveSkill cachedPassiveSkill;
        private DataPackEquipment cachedWeapon;
        private DataPackEquipment cachedArmor;
        private DataPackEnemy cachedEnemy;
        private DataPackState cachedState;

        #endregion

        #region Init

        public MainWindow()
        {
            // Set updating flag
            updating = true;

            // Set culture to en-US for string conversion
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            // Initialze MainWindow
            InitializeComponent();

            // Set new title
            if (AAEEData.Version.BetaFlag)
            {
                this.Title = "Advanced Actor and Enemy Engine " + AAEEData.Version.Script + " Beta - Configuration Tool";
            }
            else
            {
                this.Title = "Advanced Actor and Enemy Engine " + AAEEData.Version.Script + " - Configuration Tool";
            }

            // Load data from RMXP data
            if (!reloadData())
            {
                Application.Current.Shutdown();
                return;
            }

            // Setup window element
            setupConfiguration();
            setupTabControl();
            setupObservableCollection();

            // Reset updating flag
            updating = false;
        }

        #endregion

        #region Setup

        private void setupTabControl()
        {
            // Default
            list_Default_EquipType.SelectedIndex = 0;
            list_Default_EquipList.SelectedIndex = 0;

            // Actor
            tree_Actor_Family.IsSelected = true;
            txt_Actor_NameEquipType.IsEnabled = false;
            txt_Actor_NameEquipList.IsEnabled = false;

            // Class
            tree_Class_Family.IsSelected = true;
            txt_Class_NameEquipType.IsEnabled = false;
            txt_Class_NameEquipList.IsEnabled = false;

            // Skill
            tree_Skill_Family.IsSelected = true;

            //Passive skill
            tree_PassiveSkill_Family.IsSelected = true;

            // Weapon
            tree_Weapon_Family.IsSelected = true;

            // Armor
            tree_Armor_Family.IsSelected = true;

            // Enemy
            tree_Enemy_Family.IsSelected = true;

            // State
            tree_State_Family.IsSelected = true;
        }

        #region ObservableCollection

        private void setupObservableCollection()
        {
            // General
            setupGeneralObservableCollection();

            // Default
            setupDefaultObservableCollection();

            // Actor
            setupActorObservableCollection();

            // Class
            setupClassObservableCollection();

            // Skill
            setupSkillObservableCollection();

            // Passive Skill
            setupPassiveSkillObservableCollection();

            // Weapon
            setupWeaponObservableCollection();

            // Armor
            setupArmorObservableCollection();

            // Enemy
            setupEnemyObservableCollection();

            // State
            setupStateObservableCollection();
        }

        #region General

        private ObservableCollection<ComboElement> generalParamRateType = new ObservableCollection<ComboElement>();
        private ObservableCollection<ComboElement> generalDefenseRateType = new ObservableCollection<ComboElement>();
        private ObservableCollection<ComboElement> generalOrderEquipmentList = new ObservableCollection<ComboElement>();
        private ObservableCollection<ComboElement> generalOrderEquipmentMultiplier = new ObservableCollection<ComboElement>();
        private ObservableCollection<ComboElement> generalOrderEquipmentFlags = new ObservableCollection<ComboElement>();
        private ObservableCollection<ComboElement> generalOrderHandReduce = new ObservableCollection<ComboElement>();
        private ObservableCollection<ComboElement> generalOrderUnarmedAttackForce = new ObservableCollection<ComboElement>();

        private void setupGeneralObservableCollection()
        {
            // Skill attack force behavior
            combo_General_ParamRateType.ItemsSource = generalParamRateType;
            combo_General_DefenseRateType.ItemsSource = generalDefenseRateType;

            // Actor class behavior
            combo_General_OrderEquipmentList.ItemsSource = generalOrderEquipmentList;
            combo_General_OrderEquipmentMultiplier.ItemsSource = generalOrderEquipmentMultiplier;
            combo_General_OrderEquipmentFlags.ItemsSource = generalOrderEquipmentFlags;
            combo_General_OrderHandReduce.ItemsSource = generalOrderHandReduce;
            combo_General_OrderUnarmedAttackForce.ItemsSource = generalOrderUnarmedAttackForce;

            // Skill parameter attack force behavior
            generalParamRateType.Add(new ComboElement(0, "Average"));
            generalParamRateType.Add(new ComboElement(1, "Maximum"));
            generalParamRateType.Add(new ComboElement(2, "Sum"));
            generalParamRateType.Add(new ComboElement(3, "Skill only"));
            generalParamRateType.Add(new ComboElement(4, "Weapon only"));

            // Skill defense attack force behavior
            generalDefenseRateType.Add(new ComboElement(0, "Average"));
            generalDefenseRateType.Add(new ComboElement(1, "Maximum"));
            generalDefenseRateType.Add(new ComboElement(2, "Sum"));
            generalDefenseRateType.Add(new ComboElement(3, "Skill only"));
            generalDefenseRateType.Add(new ComboElement(4, "Weapon only"));

            // Actor Class equipment list behavior
            generalOrderEquipmentList.Add(new ComboElement(0, "Class primary"));
            generalOrderEquipmentList.Add(new ComboElement(1, "Actor primary"));

            // Actor Class equipment multiplier behavior
            generalOrderEquipmentMultiplier.Add(new ComboElement(0, "Multiple"));
            generalOrderEquipmentMultiplier.Add(new ComboElement(1, "Class primary"));
            generalOrderEquipmentMultiplier.Add(new ComboElement(2, "Actor primary"));

            // Actor Class equipment flags behavior
            generalOrderEquipmentFlags.Add(new ComboElement(0, "Check for true"));
            generalOrderEquipmentFlags.Add(new ComboElement(1, "Class primary"));
            generalOrderEquipmentFlags.Add(new ComboElement(2, "Actor primary"));

            // Actor Class mutil-hand reduction
            generalOrderHandReduce.Add(new ComboElement(0, "Maximum"));
            generalOrderHandReduce.Add(new ComboElement(1, "Sum"));
            generalOrderHandReduce.Add(new ComboElement(2, "Class primary"));
            generalOrderHandReduce.Add(new ComboElement(3, "Actor primary"));

            // Actor Class equipment list behavior
            generalOrderUnarmedAttackForce.Add(new ComboElement(0, "Average"));
            generalOrderUnarmedAttackForce.Add(new ComboElement(1, "Maximum"));
            generalOrderUnarmedAttackForce.Add(new ComboElement(2, "Sum"));
            generalOrderUnarmedAttackForce.Add(new ComboElement(3, "Class primary"));
            generalOrderUnarmedAttackForce.Add(new ComboElement(4, "Actor primary"));
            
            // Skill attack force behavior
            combo_General_ParamRateType.SelectedIndex = 0;
            combo_General_DefenseRateType.SelectedIndex = 0;

            // Actor class behavior
            combo_General_OrderEquipmentList.SelectedIndex = 0;
            combo_General_OrderEquipmentMultiplier.SelectedIndex = 0;
            combo_General_OrderEquipmentFlags.SelectedIndex = 0;
            combo_General_OrderHandReduce.SelectedIndex = 0;
            combo_General_OrderUnarmedAttackForce.SelectedIndex = 0;
        }

        #endregion

        #region Default

        private ObservableCollection<EquipType> defaultEquipType = new ObservableCollection<EquipType>();
        private ObservableCollection<EquipType> defaultEquipList = new ObservableCollection<EquipType>();
        private ObservableCollection<Animation> defaultAnimCaster = new ObservableCollection<Animation>();
        private ObservableCollection<Animation> defaultAnimTarget = new ObservableCollection<Animation>();

        private void setupDefaultObservableCollection()
        {
            list_Default_EquipType.ItemsSource = defaultEquipType;
            list_Default_EquipList.ItemsSource = defaultEquipList;
            if (defaultEquipType.Count > 0) { list_Default_EquipType.SelectedIndex = 0; }
            if (defaultEquipList.Count > 0) { list_Default_EquipList.SelectedIndex = 0; }
            combo_Default_AnimCaster.ItemsSource = defaultAnimCaster;
            combo_Default_AnimTarget.ItemsSource = defaultAnimTarget;
            if (defaultAnimCaster.Count > 0) { combo_Default_AnimCaster.SelectedIndex = 0; }
            if (defaultAnimTarget.Count > 0) { combo_Default_AnimTarget.SelectedIndex = 0; }
        }

        #endregion

        #region Actor

        private ObservableCollection<Actor> actorAvailable = new ObservableCollection<Actor>();
        private ObservableCollection<Actor> actorInFamily = new ObservableCollection<Actor>();
        private ObservableCollection<EquipType> actorEquipType = new ObservableCollection<EquipType>();
        private ObservableCollection<EquipType> actorEquipList = new ObservableCollection<EquipType>();
        private ObservableCollection<Animation> actorAnimCaster = new ObservableCollection<Animation>();
        private ObservableCollection<Animation> actorAnimTarget = new ObservableCollection<Animation>();

        private void setupActorObservableCollection()
        {
            // Bind the actor available list
            list_Actor_Available.ItemsSource = actorAvailable;
            if (actorAvailable.Count > 0) { list_Actor_Available.SelectedIndex = 0; }

            // Bind the actor in family list
            list_Actor_InFamily.ItemsSource = actorInFamily;

            // Bind the actor equipment list
            list_Actor_EquipType.ItemsSource = actorEquipType;
            list_Actor_EquipList.ItemsSource = actorEquipList;

            // Bind the actor unarmed attack animation
            combo_Actor_AnimCaster.ItemsSource = actorAnimCaster;
            combo_Actor_AnimTarget.ItemsSource = actorAnimTarget;
            if (actorAnimCaster.Count > 0) { combo_Actor_AnimCaster.SelectedIndex = 0; }
            if (actorAnimTarget.Count > 0) { combo_Actor_AnimTarget.SelectedIndex = 0; }
        }
        #endregion

        #region Class

        private ObservableCollection<Class> classAvailable = new ObservableCollection<Class>();
        private ObservableCollection<Class> classInFamily = new ObservableCollection<Class>();
        private ObservableCollection<EquipType> classEquipType = new ObservableCollection<EquipType>();
        private ObservableCollection<EquipType> classEquipList = new ObservableCollection<EquipType>();
        private ObservableCollection<Animation> classAnimCaster = new ObservableCollection<Animation>();
        private ObservableCollection<Animation> classAnimTarget = new ObservableCollection<Animation>();

        private void setupClassObservableCollection()
        {
            // Bind the class available list
            list_Class_Available.ItemsSource = classAvailable;
            if (classAvailable.Count > 0) { list_Class_Available.SelectedIndex = 0; }

            // Bind the class in family list
            list_Class_InFamily.ItemsSource = classInFamily;

            // Bind the class equipment list
            list_Class_EquipType.ItemsSource = classEquipType;
            list_Class_EquipList.ItemsSource = classEquipList;

            // Bind the class unarmed attack animation
            combo_Class_AnimCaster.ItemsSource = classAnimCaster;
            combo_Class_AnimTarget.ItemsSource = classAnimTarget;
            if (classAnimCaster.Count > 0) { combo_Class_AnimCaster.SelectedIndex = 0; }
            if (classAnimTarget.Count > 0) { combo_Class_AnimTarget.SelectedIndex = 0; }
        }
        #endregion

        #region Skill

        public ObservableCollection<Skill> skillAvailable = new ObservableCollection<Skill>();
        public ObservableCollection<Skill> skillInFamily = new ObservableCollection<Skill>();

        private void setupSkillObservableCollection()
        {
            // Bind available list
            list_Skill_Available.ItemsSource = skillAvailable;
            if (skillAvailable.Count > 0) { list_Skill_Available.SelectedIndex = 0; }

            // Bind in family
            list_Skill_InFamily.ItemsSource = skillInFamily;
        }

        #endregion

        #region Passive Skill

        public ObservableCollection<Skill> passiveSkillAvailable = new ObservableCollection<Skill>();
        public ObservableCollection<Skill> passiveSkillInFamily = new ObservableCollection<Skill>();

        private void setupPassiveSkillObservableCollection()
        {
            list_PassiveSkill_Available.ItemsSource = passiveSkillAvailable;
            if (passiveSkillAvailable.Count > 0) { list_PassiveSkill_Available.SelectedIndex = 0; }

            // Bind in family
            list_PassiveSkill_InFamily.ItemsSource = passiveSkillInFamily;
        }

        #endregion

        #region Weapon

        public ObservableCollection<Weapon> weaponAvailable = new ObservableCollection<Weapon>();
        public ObservableCollection<Weapon> weaponInFamily = new ObservableCollection<Weapon>();
        public ObservableCollection<Weapon> weaponSwitchID = new ObservableCollection<Weapon>();

        private void setupWeaponObservableCollection()
        {
            // Bind available list
            list_Weapon_Available.ItemsSource = weaponAvailable;
            if (weaponAvailable.Count > 0) { list_Weapon_Available.SelectedIndex = 0; }

            // Bind in family
            list_Weapon_InFamily.ItemsSource = weaponInFamily;
            if (weaponInFamily.Count > 0) { list_Weapon_InFamily.SelectedIndex = 0; }

            // Bind default switch id coombo for family, id and default
            combo_Weapon_SwitchID.ItemsSource = weaponSwitchID;

        }

        #endregion

        #region Armor

        public ObservableCollection<Armor> armorAvailable = new ObservableCollection<Armor>();
        public ObservableCollection<Armor> armorInFamily = new ObservableCollection<Armor>();
        public ObservableCollection<EquipType> armorType = new ObservableCollection<EquipType>();
        public ObservableCollection<Armor> armorSwitchID = new ObservableCollection<Armor>();


        private void setupArmorObservableCollection()
        {
            // Bind list available
            list_Armor_Available.ItemsSource = armorAvailable;
            if (armorAvailable.Count > 0) { list_Armor_Available.SelectedIndex = 0; }

            // Bind list in family
            list_Armor_InFamily.ItemsSource = armorInFamily;
            if (armorInFamily.Count > 0) { list_Armor_InFamily.SelectedIndex = 0; }

            // Set default type coombo for family and id
            armorType.Add(new EquipType(0, "Default"));

            // Create type coombo for family and id
            if (cfg.Default.EquipType.Count > 5)
            {
                for (int i = 5; i < cfg.Default.EquipType.Count; i++)
                {
                    armorType.Add(new EquipType(i, cfg.Default.EquipType[i].Name));
                }
            }

            // Bind type coombo for family and id
            combo_Armor_Type.ItemsSource = armorType;

            // Bind switch id coombo for family, id and default
            combo_Armor_SwitchID.ItemsSource = armorSwitchID;
        }

        #endregion

        #region Enemy

        public ObservableCollection<Enemy> enemyAvailable = new ObservableCollection<Enemy>();
        public ObservableCollection<Enemy> enemyInFamily = new ObservableCollection<Enemy>();

        private void setupEnemyObservableCollection()
        {
            list_Enemy_Available.ItemsSource = enemyAvailable;
            if (enemyAvailable.Count > 0) { list_Enemy_Available.SelectedIndex = 0; }
            list_Enemy_InFamily.ItemsSource = enemyInFamily;
        }

        #endregion

        #region State

        public ObservableCollection<State> stateAvailable = new ObservableCollection<State>();
        public ObservableCollection<State> stateInFamily = new ObservableCollection<State>();

        private void setupStateObservableCollection()
        {
            list_State_Available.ItemsSource = stateAvailable;
            if (stateAvailable.Count > 0) { list_State_Available.SelectedIndex = 0; }
            list_State_InFamily.ItemsSource = stateInFamily;
        }

        #endregion

        #endregion

        #endregion

        #region File Data Load

        private bool reloadData()
        {
            FileStream stream;
            foreach (string file in AAEEData.FileNames)
            {
                try
                {
                    stream = File.Open(string.Format("Data\\{0}.rxdata", file), FileMode.Open);
                    List<string> names = new List<string>();
                    List<int> ids = new List<int>();
                    Marshal.LoadRMXPData(names, ids, stream);
                    switch (file)
                    {
                        case "Actors": for (int i = 0; i < names.Count; i++) gameData.AddActor(ids[i], names[i]); break;
                        case "Classes": for (int i = 0; i < names.Count; i++) gameData.AddClass(ids[i], names[i]); break;
                        case "Skills": for (int i = 0; i < names.Count; i++) gameData.AddSkill(ids[i], names[i]); break;
                        case "Items": for (int i = 0; i < names.Count; i++) gameData.AddItem(ids[i], names[i]); break;
                        case "Weapons": for (int i = 0; i < names.Count; i++) gameData.AddWeapon(ids[i], names[i]); break;
                        case "Armors": for (int i = 0; i < names.Count; i++) gameData.AddArmor(ids[i], names[i]); break;
                        case "Enemies": for (int i = 0; i < names.Count; i++) gameData.AddEnemy(ids[i], names[i]); break;
                        case "States": for (int i = 0; i < names.Count; i++) gameData.AddState(ids[i], names[i]); break;
                        case "Animations": for (int i = 0; i < names.Count; i++) gameData.AddAnimation(ids[i], names[i]); break;
                    }
                    stream.Close();
                }
                catch (IOException)
                {
                    string message = string.Format("The file {0}.rxdata is missing. The application must\n" +
                            "be located in the same folder as the game project.", file);
                    string title = string.Format("{0}.rxdata file missing", file);
                    Xceed.Wpf.Toolkit.MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }

            #region Actor

            // Empty class available list
            actorAvailable.Clear();
            cfg.ActorAvailable.Clear();

            // Empty class id list
            tree_Actor_Individual.Items.Clear();
            cfg.ActorID.Clear();

            // Add the data of each actor to all the individual list
            if (gameData.Classes.Count > 0)
            {
                foreach (Actor actors in gameData.Actors)
                {
                    // Create actor available list
                    actorAvailable.Add(new Actor(actors.ID, actors.Name));
                    cfg.ActorAvailable.Add(new Actor(actors.ID, actors.Name));

                    // Create actor id list
                    tree_Actor_Individual.Items.Add(new TreeViewItem() { Header = actors.ID.ToString("000") + ": " + actors.Name, Tag = actors.ID.ToString() });
                    cfg.ActorID.Add(new DataPackActor(actors.ID, actors.Name));
                }
            }

            #endregion

            #region Class

            // Empty class available list
            classAvailable.Clear();
            cfg.ClassAvailable.Clear();

            // Empty class id list
            tree_Class_Individual.Items.Clear();
            cfg.ClassID.Clear();

            // Add the data of each class to all the individual list
            if (gameData.Classes.Count > 0)
            {
                foreach (Class classes in gameData.Classes)
                {
                    // Create class available list
                    classAvailable.Add(new Class(classes.ID, classes.Name));
                    cfg.ClassAvailable.Add(new Class(classes.ID, classes.Name));

                    // Create class id list
                    tree_Class_Individual.Items.Add(new TreeViewItem() { Header = classes.ID.ToString("000") + ": " + classes.Name, Tag = classes.ID.ToString() });
                    cfg.ClassID.Add(new DataPackClass(classes.ID, classes.Name));
                }
            }

            #endregion

            #region Skill

            // Empty skill available list
            skillAvailable.Clear();
            cfg.SkillAvailable.Clear();

            // Empty skill id list
            tree_Skill_Individual.Items.Clear();
            cfg.SkillID.Clear();

            // Empty passive skill available list
            passiveSkillAvailable.Clear();
            cfg.PassiveSkillAvailable.Clear();

            // Empty passive skill id list
            tree_PassiveSkill_Individual.Items.Clear();
            cfg.PassiveSkillID.Clear();

            // Add the data of each skill to all the individual list
            if (gameData.Skills.Count > 0)
            {
                foreach (Skill skill in gameData.Skills)
                {
                    // Create skill available list
                    skillAvailable.Add(new Skill(skill.ID, skill.Name));
                    cfg.SkillAvailable.Add(new Skill(skill.ID, skill.Name));

                    // Create skill id list
                    tree_Skill_Individual.Items.Add(new TreeViewItem() { Header = skill.ID.ToString("000") + ": " + skill.Name, Tag = skill.ID.ToString() });
                    cfg.SkillID.Add(new DataPackSkill(skill.ID, skill.Name));

                    // Create passive skill available list
                    passiveSkillAvailable.Add(new Skill(skill.ID, skill.Name));
                    cfg.PassiveSkillAvailable.Add(new Skill(skill.ID, skill.Name));

                    // Create passive skill id list
                    tree_PassiveSkill_Individual.Items.Add(new TreeViewItem() { Header = skill.ID.ToString("000") + ": " + skill.Name, Tag = skill.ID.ToString() });
                    cfg.PassiveSkillID.Add(new DataPackPassiveSkill(skill.ID, skill.Name));
                }
            }

            #endregion

            #region Weapon

            // Empty weapon available list
            weaponAvailable.Clear();
            cfg.WeaponAvailable.Clear();

            // Empty weapon id list
            tree_Weapon_Individual.Items.Clear();
            cfg.WeaponID.Clear();

            // Clear switch id coombo for family, id and default
            weaponSwitchID.Clear();

            // Create switch id coombo for family, id and default
            weaponSwitchID.Add(new Weapon(0, "None"));

            if (gameData.Weapons.Count > 0)
            {
                foreach (Weapon weapon in gameData.Weapons)
                {
                    // Create weapon available list
                    weaponAvailable.Add(new Weapon(weapon.ID, weapon.Name));
                    cfg.WeaponAvailable.Add(new Weapon(weapon.ID, weapon.Name));

                    // Create weapon id list
                    tree_Weapon_Individual.Items.Add(new TreeViewItem() { Header = weapon.ID.ToString("000") + ": " + weapon.Name, Tag = weapon.ID.ToString() });
                    cfg.WeaponID.Add(new DataPackEquipment(weapon.ID, weapon.Name));

                    // Set switch id coombo for family, id and default
                    weaponSwitchID.Add(new Weapon(weapon.ID, weapon.Name));
                }
            }

            #endregion

            #region Armor

            // Empty armor available list
            armorAvailable.Clear();
            cfg.ArmorAvailable.Clear();

            // Empty armor id list
            tree_Armor_Individual.Items.Clear();
            cfg.ArmorID.Clear();

            // Clear switch id coombo for family, id and default
            armorSwitchID.Clear();

            // Create switch id coombo for family, id and default
            armorSwitchID.Add(new Armor(0, "None"));


            if (gameData.Armors.Count > 0)
            {
                foreach (Armor armor in gameData.Armors)
                {
                    // Create armor available list
                    armorAvailable.Add(new Armor(armor.ID, armor.Name));
                    cfg.ArmorAvailable.Add(new Armor(armor.ID, armor.Name));

                    // Create armor id list
                    tree_Armor_Individual.Items.Add(new TreeViewItem() { Header = armor.ID.ToString("000") + ": " + armor.Name, Tag = armor.ID.ToString() });
                    cfg.ArmorID.Add(new DataPackEquipment(armor.ID, armor.Name));

                    // Set switch id coombo for family, id and default
                    armorSwitchID.Add(new Armor(armor.ID, armor.Name));
                }
            }

            #endregion

            #region Enemy

            // Empty ef available list
            enemyAvailable.Clear();
            cfg.EnemyAvailable.Clear();

            // Empty ef id list
            tree_Enemy_Individual.Items.Clear();
            cfg.EnemyID.Clear();

            if (gameData.Enemies.Count > 0)
            {
                foreach (Enemy enemy in gameData.Enemies)
                {
                    // Create ef available list
                    enemyAvailable.Add(new Enemy(enemy.ID, enemy.Name));
                    cfg.EnemyAvailable.Add(new Enemy(enemy.ID, enemy.Name));

                    // Create ef id list
                    tree_Enemy_Individual.Items.Add(new TreeViewItem() { Header = enemy.ID.ToString("000") + ": " + enemy.Name, Tag = enemy.ID.ToString() });
                    cfg.EnemyID.Add(new DataPackEnemy(enemy.ID, enemy.Name));
                }
            }

            #endregion

            #region State

            // Empty state available list
            stateAvailable.Clear();
            cfg.StateAvailable.Clear();

            // Empty state id list
            tree_State_Individual.Items.Clear();
            cfg.StateID.Clear();

            if (gameData.States.Count > 0)
            {
                foreach (State state in gameData.States)
                {
                    // Create state available list
                    stateAvailable.Add(new State(state.ID, state.Name));
                    cfg.StateAvailable.Add(new State(state.ID, state.Name));

                    // Create state id list
                    tree_State_Individual.Items.Add(new TreeViewItem() { Header = state.ID.ToString("000") + ": " + state.Name, Tag = state.ID.ToString() });
                    cfg.StateID.Add(new DataPackState(state.ID, state.Name));
                }
            }

            #endregion

            #region Animation

            // Empty the animation ComboBox for the default
            defaultAnimCaster.Clear();
            defaultAnimCaster.Add(new Animation(0, "None"));
            defaultAnimTarget.Clear();
            defaultAnimTarget.Add(new Animation(0, "None"));

            // Empty the animation ComboBox for the actor
            actorAnimCaster.Clear();
            actorAnimCaster.Add(new Animation(0, "None"));
            actorAnimTarget.Clear();
            actorAnimTarget.Add(new Animation(0, "None"));

            // Empty the animation ComboBox for the class
            classAnimCaster.Clear();
            classAnimCaster.Add(new Animation(0, "None"));
            classAnimTarget.Clear();
            classAnimTarget.Add(new Animation(0, "None"));

            foreach (Animation anim in gameData.Animations)
            {
                // Create the animation ComboBox for the actor family
                defaultAnimCaster.Add(new Animation(anim.ID, anim.Name));
                defaultAnimTarget.Add(new Animation(anim.ID, anim.Name));

                // Create the animation ComboBox for the actor family
                actorAnimCaster.Add(new Animation(anim.ID, anim.Name));
                actorAnimTarget.Add(new Animation(anim.ID, anim.Name));

                // Create the animation ComboBox for the class family
                classAnimCaster.Add(new Animation(anim.ID, anim.Name));
                classAnimTarget.Add(new Animation(anim.ID, anim.Name));
            }

            #endregion

            return true;
        }

        #endregion

        #region Window Control

        #region Menu File

        private void menu_FileOpen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Initialize the open file window
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            openFileDialog.Filter = "Xml documents (.xml)|*.xml"; // Filter files by extension

            // Show open file dialog
            if (openFileDialog.ShowDialog() == true)
            {
                // Load the configuration object from the XML file using the custom class.
                Configuration loadCfg = ObjectXMLSerializer<Configuration>.Load(openFileDialog.FileName);

                if (loadCfg == null)
                {
                    Xceed.Wpf.Toolkit.MessageBox.Show("Unable to load configuration from file '" + openFileDialog.FileName + "'!", "Unable to load", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else  // Load configuration properties into the window
                {
                    loadConfiguration(loadCfg);
                }
            }
        }

        private void menu_FileSave_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            storeConfiguration();
            try
            {
                // Initialize the save file window
                Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
                saveFileDialog.DefaultExt = ".xml";
                saveFileDialog.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
                saveFileDialog.Filter = "Xml documents (.xml)|*.xml"; // Filter files by extension

                // If file already save
                if (lastFileName != "")
                {
                    ObjectXMLSerializer<Configuration>.Save(cfg, lastFileName);
                }
                else
                {
                    // If file not save show save dialog
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        lastFileName = saveFileDialog.FileName;
                        ObjectXMLSerializer<Configuration>.Save(cfg, saveFileDialog.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                // Show error message when save failed
                string lines = Environment.NewLine + Environment.NewLine;
                string error = ex.Message + lines + ex.InnerException.InnerException.InnerException;
                Xceed.Wpf.Toolkit.MessageBox.Show("Unable to save configuration!" + lines + error, "Unable to save", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void menu_FileSaveAs_Click(object sender, RoutedEventArgs e)
        {
            storeConfiguration();
            try
            {
                // Initialize the save file window
                Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
                saveFileDialog.DefaultExt = ".xml";
                saveFileDialog.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
                saveFileDialog.Filter = "Xml documents (.xml)|*.xml"; // Filter files by extension

                // Show save file dialog
                if (saveFileDialog.ShowDialog() == true)
                {
                    lastFileName = saveFileDialog.FileName;
                    ObjectXMLSerializer<Configuration>.Save(cfg, saveFileDialog.FileName);
                }

            }
            catch (Exception ex)
            {
                // Show error message when save failed
                string lines = Environment.NewLine + Environment.NewLine;
                string error = ex.Message + lines + ex.InnerException.InnerException.InnerException;
                Xceed.Wpf.Toolkit.MessageBox.Show("Unable to save configuration!" + lines + error, "Unable to save", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void menu_FileClose_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        #endregion

        #region Tools

        private void menu_Tools_ResetTab_Click(object sender, RoutedEventArgs e)
        {
            updating = true;

            if (Tab_Actor.IsSelected)
            {
                tree_Actor_Family.IsSelected = true;
                resetActorFamily();
                resetActorID();
                resetActorDefault();
                applyEmptyActor();
            }
            else if (Tab_Class.IsSelected)
            {
                tree_Class_Family.IsSelected = true;
                resetClassFamily();
                resetClassID();
                resetClassDefault();
                applyEmptyClass();
            }
            else if (Tab_Skill.IsSelected)
            {
                tree_Skill_Family.IsSelected = true;
                resetSkillFamily();
                resetSkillID();
                resetSkillDefault();
                applyEmptySkill();
            }
            else if (Tab_PassiveSkill.IsSelected)
            {
                tree_PassiveSkill_Family.IsSelected = true;
                resetPassiveSkillFamily();
                resetPassiveSkillID();
                resetPassiveSkillDefault();
                applyEmptyPassiveSkill();
            }
            else if (Tab_Weapon.IsSelected)
            {
                tree_Weapon_Family.IsSelected = true;
                resetWeaponFamily();
                resetWeaponID();
                resetWeaponDefault();
                applyEmptyWeapon();
            }
            else if (Tab_Armor.IsSelected)
            {
                tree_Armor_Family.IsSelected = true;
                resetArmorFamily();
                resetArmorID();
                resetArmorDefault();
                applyEmptyArmor();
            }
            else if (Tab_Enemy.IsSelected)
            {
                tree_Enemy_Family.IsSelected = true;
                resetEnemyFamily();
                resetEnemyID();
                resetEnemyDefault();
                applyEmptyEnemy();
            }
            else if (Tab_State.IsSelected)
            {
                tree_State_Family.IsSelected = true;
                resetStateFamily();
                resetStateID();
                resetStateDefault();
                applyEmptyState();
            }

            updating = false;
        }

        private void menu_Tools_ResetAll_Click(object sender, RoutedEventArgs e)
        {
            resetConfiguration();
            setupTabControl();
            applyConfiguration();
        }

        private void menu_Tools_Generate_Click(object sender, RoutedEventArgs e)
        {
            Tab_Generate.IsSelected = true;
            storeConfiguration();
            txt_Generate.Text = Generator.GenerateScript(cfg, gameData);
        }

        #endregion

        #region Help

        private void menu_Help_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            HelpWindow helpWindow = new HelpWindow();
            helpWindow.Owner = this;
            helpWindow.Show();
        }

        private void menu_About_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.Owner = this;
            aboutWindow.Show();
        }

        private void menu_VersionHistory_Click(object sender, RoutedEventArgs e)
        {
            VersionHistoryWindow versionHistoryWindow = new VersionHistoryWindow();
            versionHistoryWindow.Owner = this;
            versionHistoryWindow.Show();
        }

        #endregion

        #endregion

        #region Tab

        private void MainTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                storeConfiguration();
                applyConfiguration();
            }
        }

        #region General

        private void check_General_Parameter_Checked(object sender, RoutedEventArgs e)
        {
            if (updating) { return; }
            CheckBox checkBox = sender as CheckBox;
            storeGeneral(checkBox.Name);
            applyGeneral();
        }

        private void check_General_Parameter_Unchecked(object sender, RoutedEventArgs e)
        {
            if (updating) { return; }
            storeGeneral();
            applyGeneral();
        }

        #endregion

        #region Default

        #region Equip Type

        #region Update name

        private void list_Default_EquipType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Check if the equipment type list box can be updated
            if (updating || list_Default_EquipType.SelectedIndex < 0) return;

            // Set the updating flag
            updating = true;

            // Store the current index
            int current_index = list_Default_EquipType.SelectedIndex;

            // Store and update the selected equipment name
            if (lastIndexDefaultEquipList != current_index && current_index >= 5)
            {
                if (lastIndexDefaultEquipType >= 5)
                {
                    // Get the name of the last equip type
                    cfg.Default.EquipType[lastIndexDefaultEquipType].Name = txt_Default_NameEquipType.Text;

                    // Update the euipment type list
                    defaultEquipType.Clear();
                    if (cfg.Default.EquipType.Count > 0)
                    {
                        foreach (EquipType equip in cfg.Default.EquipType)
                        {
                            defaultEquipType.Add(new EquipType(equip.ID, equip.Name));
                        }
                    }

                    // Reset the list to the current index
                    list_Default_EquipType.SelectedIndex = current_index;
                }

                // Update and enable the equip type name text box
                txt_Default_NameEquipType.IsEnabled = true;
                txt_Default_NameEquipType.Text = defaultEquipType[current_index].Name;
            }
            else
            {
                // Empty and disable the equip type name text box
                txt_Default_NameEquipType.IsEnabled = false;
                txt_Default_NameEquipType.Text = "";
            }

            // Set the current index to the last index for the next update
            lastIndexDefaultEquipType = current_index;

            // Remove the updating flag
            updating = false;
        }

        #endregion

        #region Add equip type

        private void btn_Default_AddEquipType_Click(object sender, RoutedEventArgs e)
        {
            // Set the updating flag
            updating = true;

            // Get the next equipment type list index
            int index = cfg.Default.EquipType.Count;

            // Store the default data
            storeDefault();

            // Add the new equipment type in the default tab
            cfg.Default.EquipType.Add(new EquipType(index));

            // Add the new equipment type for all the actor family when the custom equip is check 
            if (cfg.ActorFamily.Count > 0)
            {
                foreach (DataPackActor actor in cfg.ActorFamily)
                {
                    if (actor.CustomEquip)
                    {
                        cfg.ActorFamily[actor.ID].EquipType.Add(new EquipType(index));
                    }
                }
            }

            // Add the new equipment type for all the actor individual when the custom equip is check 
            if (cfg.ActorID.Count > 0)
            {
                foreach (DataPackActor actorID in cfg.ActorID)
                {
                    if (actorID.CustomEquip)
                    {
                        actorID.EquipType.Add(new EquipType(index));
                    }
                }
            }

            // Add the new equipment type for all the actor default when the custom equip is check 
            if (cfg.ActorDefault.CustomEquip)
            {
                cfg.ActorDefault.EquipType.Add(new EquipType(index));
            }

            // Add the new equipment type for all the class family when the custom equip is check 
            if (cfg.ClassFamily.Count > 0)
            {
                foreach (DataPackClass classFamily in cfg.ClassFamily)
                {
                    if (classFamily.CustomEquip)
                    {
                        classFamily.EquipType.Add(new EquipType(index));
                    }
                }
            }

            // Add the new equipment type for all the class individual when the custom equip is check 
            if (cfg.ClassID.Count > 0)
            {
                foreach (DataPackClass classID in cfg.ClassID)
                {
                    if (classID.CustomEquip)
                    {
                        classID.EquipType.Add(new EquipType(index));
                    }
                }
            }

            // Add the new equipment type for all the class default when the custom equip is check 
            if (cfg.ClassDefault.CustomEquip)
            {
                cfg.ClassDefault.EquipType.Add(new EquipType(index));
            }

            // Add the new armor type in the equipment type checkbox in the armor tab
            armorType.Add(new EquipType(index));
            armorType.Sort((x, y) => x.ID.CompareTo(y.ID));

            // Apply the new configuration
            applyConfiguration();

            // Remove the updating flag
            updating = false;
        }

        #endregion

        #region Remove equip type

        private void btn_Default_RemoveEquipType_Click(object sender, RoutedEventArgs e)
        {
            // Check if the equipment type is a default one
            if (list_Default_EquipType.SelectedIndex < 5) { return; }

            // Set the updating flag
            updating = true;

            // Get the next equipment type list index
            int index = list_Default_EquipType.SelectedIndex;

            // Store the default data
            storeDefault();

            // Remove the selected equipment type in the default tab
            cfg.Default.ResetEquipListID(index);

            // Remove the selected equipment type from all the actor family
            if (cfg.ActorFamily.Count > 0)
            {
                foreach (DataPackActor actor in cfg.ActorFamily)
                {
                    if (actor.CustomEquip)
                    {
                        cfg.ActorFamily[actor.ID].ResetEquipListID(index);
                    }
                }
            }

            // Remove the selected equipment type from all the actor individual
            if (cfg.ActorID.Count > 0)
            {
                foreach (DataPackActor actorID in cfg.ActorID)
                {
                    if (actorID.CustomEquip)
                    {
                        actorID.ResetEquipListID(index);
                    }
                }
            }

            // Remove the selected equipment type from all the actor default
            if (cfg.ActorDefault.CustomEquip)
            {
                cfg.ActorDefault.ResetEquipListID(index);
            }

            // Remove the selected equipment type from all the class family
            if (cfg.ClassFamily.Count > 0)
            {
                foreach (DataPackClass classFamily in cfg.ClassFamily)
                {
                    if (classFamily.CustomEquip)
                    {
                        classFamily.ResetEquipListID(index);
                    }
                }
            }

            // Remove the selected equipment type from all the class individual
            if (cfg.ClassID.Count > 0)
            {
                foreach (DataPackClass classID in cfg.ClassID)
                {
                    if (classID.CustomEquip)
                    {
                        classID.ResetEquipListID(index);
                    }
                }
            }

            // Remove the selected equipment type from all the class default
            if (cfg.ClassDefault.CustomEquip)
            {
                cfg.ClassDefault.ResetEquipListID(index);
            }

            // Reset the equipment type for all the armor family
            if (cfg.ArmorFamily.Count > 0)
            {
                foreach (DataPackEquipment armorFamily in cfg.ArmorFamily)
                {
                    if (armorFamily.Type == index)
                    {
                        armorFamily.Type = 0;
                    }
                    else if (armorFamily.Type > index)
                    {
                        armorFamily.Type = armorFamily.Type - 1;
                    }
                }
            }

            // Reset the equipment type for all the armor individual
            if (cfg.ArmorID.Count > 0)
            {
                foreach (DataPackEquipment armorID in cfg.ArmorID)
                {
                    if (armorID.Type == index)
                    {
                        armorID.Type = 0;
                    }
                    else if (armorID.Type > index)
                    {
                        armorID.Type = armorID.Type - 1;
                    }
                }
            }

            // Remove the selected armor type in the equipment type checkbox in the armor tab and update the index number
            armorType.RemoveAt(index - 4);
            armorType.Sort((x, y) => x.ID.CompareTo(y.ID));
            if (armorType.Count > 1)
            {
                foreach (EquipType equipType in armorType)
                {
                    if (equipType.ID > index)
                    {
                        equipType.ID -= 1;
                    }
                }
            }

            // Apply the new configuration
            applyDefault();

            // Remove the updating flag
            updating = false;
        }

        #endregion

        #endregion

        #region Equip List

        #region Update name

        private void list_Default_EquipList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Check if the equipment type list box can be updated
            if (updating || list_Default_EquipList.SelectedIndex < 0) return;

            // Set the updating flag
            updating = true;

            // Store the current index
            int current_index = list_Default_EquipList.SelectedIndex;

            if (lastIndexDefaultEquipList != current_index)
            {
                // Get the name of the last equip type
                cfg.Default.EquipList[lastIndexDefaultEquipList].Name = txt_Default_NameEquipList.Text;

                // Update the euipment type list
                defaultEquipList.Clear();
                if (cfg.Default.EquipList.Count > 0)
                {
                    foreach (EquipType equip in cfg.Default.EquipList)
                    {
                        defaultEquipList.Add(new EquipType(equip.ID, equip.Name));
                    }
                }

                // Reset the list to the current index
                list_Default_EquipList.SelectedIndex = current_index;
            }

            // Update the equip type name text box
            txt_Default_NameEquipList.Text = defaultEquipList[current_index].Name;

            // Set the current index to the last index for the next update
            lastIndexDefaultEquipList = current_index;

            // Remove the updating flag
            updating = false;
        }
        
        #endregion

        #region Add and remove equipment

        private void btn_Default_AddEquipList_Click(object sender, RoutedEventArgs e)
        {
            // Check if an element of the equip type list is selected
            if (list_Default_EquipType.SelectedIndex < 0) { return; }

            // Add the selected element to the ui list
            defaultEquipList.Add(new EquipType(defaultEquipType[list_Default_EquipType.SelectedIndex].ID, defaultEquipType[list_Default_EquipType.SelectedIndex].Name));

            // Add the selected element to the cfg list
            cfg.Default.EquipList.Add(new EquipType(defaultEquipType[list_Default_EquipType.SelectedIndex].ID, defaultEquipType[list_Default_EquipType.SelectedIndex].Name));
        }

        private void btn_Default_RemoveEquipList_Click(object sender, RoutedEventArgs e)
        {
            // Check if an element of the equip list box is selected
            if (list_Default_EquipList.SelectedIndex < 0) { return; }

            // Remove the selected element from the cfg list
            cfg.Default.EquipList.RemoveAt(list_Default_EquipList.SelectedIndex);

            // Remove the selected element from the ui list
            defaultEquipList.RemoveAt(list_Default_EquipList.SelectedIndex);
        }

        #endregion

        #region Move equipment in list

        private void btn_Default_UpEquipList_Click(object sender, RoutedEventArgs e)
        {
            // Check if the equipment type list box can be updated
            if (updating || list_Default_EquipList.SelectedIndex < 0) return;

            // Check if the selected index is before the first element
            if (list_Default_EquipList.SelectedIndex > 0)
            {
                // Move the selected equipment down
                defaultEquipList.Move(list_Default_EquipList.SelectedIndex, list_Default_EquipList.SelectedIndex - 1);
            }
            else
            {
                // Return the selected index to the bottom
                defaultEquipList.Move(list_Default_EquipList.SelectedIndex, defaultEquipList.Count - 1);
            }

            // Store the new default data
            storeDefault();
        }

        private void btn_Default_DownEquipList_Click(object sender, RoutedEventArgs e)
        {
            // Check if the equipment type list box can be updated
            if (updating || list_Default_EquipList.SelectedIndex < 0) { return; }

            // Check if the selected index is before the last element
            if (list_Default_EquipList.SelectedIndex < defaultEquipList.Count - 1)
            {
                // Move the selected equipment down
                defaultEquipList.Move(list_Default_EquipList.SelectedIndex, list_Default_EquipList.SelectedIndex + 1);
            }
            else
            {
                // Return the selected index to the top
                defaultEquipList.Move(list_Default_EquipList.SelectedIndex, 0);
            }

            // Store the new default data
            storeDefault();
        }

        #endregion

        #region Reset Name

        private void btn_Default_ResetNameEquipList_Click(object sender, RoutedEventArgs e)
        {
            // Check if the equipment type list box can be updated
            if (updating || list_Default_EquipList.SelectedIndex < 0) return;

            // Set the updating flag
            updating = true;

            // Store the current index
            int current_index = list_Default_EquipList.SelectedIndex;

            // Get the default name for the selected equipment
            cfg.Default.EquipList[current_index].Name = cfg.Default.EquipType[cfg.Default.EquipList[current_index].ID].Name;

            // Update the euipment type list
            defaultEquipList.Clear();
            if (cfg.Default.EquipList.Count > 0)
            {
                foreach (EquipType equip in cfg.Default.EquipList)
                {
                    defaultEquipList.Add(new EquipType(equip.ID, equip.Name));
                }
            }

            // Reset the list to the current index
            list_Default_EquipList.SelectedIndex = current_index;

            // Update the equip type name text box
            txt_Default_NameEquipList.Text = defaultEquipList[current_index].Name;

            // Set the current index to the last index for the next update
            lastIndexDefaultEquipList = list_Default_EquipList.SelectedIndex;

            // Remove the updating flag
            updating = false;
        }

        private void btn_Default_ResetAllNameEquipList_Click(object sender, RoutedEventArgs e)
        {
            // Check if the equipment type list box can be updated
            if (updating || list_Default_EquipList.SelectedIndex < 0) return;

            // Set the updating flag
            updating = true;

            // Store the current index
            int current_index = list_Default_EquipList.SelectedIndex;

            // Get the default name for the all equipment
            if (cfg.Default.EquipList.Count > 0)
            {
                foreach (EquipType equip in cfg.Default.EquipList)
                {
                    equip.Name = cfg.Default.EquipType[equip.ID].Name;
                }
            }

            // Update the euipment type list
            defaultEquipList.Clear();
            if (cfg.Default.EquipList.Count > 0)
            {
                foreach (EquipType equip in cfg.Default.EquipList)
                {
                    defaultEquipList.Add(new EquipType(equip.ID, equip.Name));
                }
            }

            // Reset the list to the current index
            list_Default_EquipList.SelectedIndex = current_index;

            // Update the equip type name text box
            txt_Default_NameEquipList.Text = defaultEquipList[current_index].Name;

            // Set the current index to the last index for the next update
            lastIndexDefaultEquipList = list_Default_EquipList.SelectedIndex;

            // Remove the updating flag
            updating = false;
        }

        #endregion

        #endregion

        #endregion

        #region Actor

        #region TreeView

        # region Get Selected

        private bool tree_Actor_IsFamily()
        {
            // Get the selected item data
            TreeViewItem tvi = (TreeViewItem)tree_Actor.SelectedItem;

            // Check if the selected item is a member of the family branch
            if (tree_Actor_Family.IsSelected == false && tree_Actor_Individual.IsSelected == false && tree_Actor_Default.IsSelected == false && tree_Actor_Family.IsAncestorOf(tvi))
            {
                return true;
            }

            return false;
        }

        private bool tree_Actor_IsIndividual()
        {
            // Get the selected item data
            TreeViewItem tvi = (TreeViewItem)tree_Actor.SelectedItem;

            // Check if the selected item is a member of the individual branch
            if (tree_Actor_Family.IsSelected == false && tree_Actor_Individual.IsSelected == false && tree_Actor_Default.IsSelected == false && tree_Actor_Individual.IsAncestorOf(tvi))
            {
                return true;
            }

            return false;
        }

        private int tree_Actor_ID()
        {
            // Set the variable use to convert the string to an integer
            int result;
            int id = -1;

            // Get the selected item data
            TreeViewItem tvi = (TreeViewItem)tree_Actor.SelectedItem;

            // Check if you the selected item is a member of the family branch or the individual branch
            if ((tree_Actor_IsFamily() || tree_Actor_IsIndividual()) && tree_Actor_Default.IsSelected == false)
            {
                // Try to convert the string to an integer
                if (Int32.TryParse(tvi.Tag.ToString(), out result))
                {
                    id = result;
                }
            }

            return id;
        }

        #endregion

        #region Update data

        private void tree_Actor_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            // Check if the the treeview can be updated
            if (updating) { return; }

            // Store the actor family data if the previous selected item was a member of the family branch
            if (lastIndexActorFamily > 0)
            {
                // Store the data
                storeActorFamily(lastIndexActorFamily);

                // Update the navigation tree
                int i = 0;
                foreach (TreeViewItem treeItem in tree_Actor_Family.Items)
                {
                    treeItem.Header = cfg.ActorFamily[i].Name;
                    i++;
                }
            }

            // Store the actor individual data if the previous selected item was a member of the individual branch
            if (lastIndexActorID > 0)
            {
                storeActorID(lastIndexActorID);
            }

            // Store the actor default data if the previous selected item was the default member
            if (lastIndexActorDefault)
            {
                storeActorDefault();
            }

            // Update the previous index
            lastIndexActorFamily = 0;
            lastIndexActorID = 0;
            lastIndexActorDefault = false;

            // Check if the selected item is a member of the family branch
            if (tree_Actor_IsFamily())
            {
                // Update the previous index
                lastIndexActorFamily = tree_Actor_ID();

                // Apply the new actor family data
                applyActorFamily();
            }
            else if (tree_Actor_IsIndividual())
            {
                // Update the previous index
                lastIndexActorID = tree_Actor_ID();

                // Apply the new actor individual data
                applyActorID();
            }
            else if (tree_Actor_Default.IsSelected)
            {
                // Update the previous index
                lastIndexActorDefault = true;

                // Apply the new actor default data
                applyActorDefault();
            }
            else
            {
                // Empty the actor data when their is no valid item selected
                applyEmptyActor();
            }
        }

        #endregion

        #region Manage data

        private void btn_Actor_Add_Click(object sender, RoutedEventArgs e)
        {
            // Get the new index
            int id = tree_Actor_Family.Items.Count + 1;

            // Create the new item in the tree and the configuration
            cfg.ActorFamily.Add(new DataPackActor(id));
            tree_Actor_Family.Items.Add(new TreeViewItem() { Header = cfg.ActorFamily[id - 1].Name, Tag = id.ToString() });
        }

        private void btn_Actor_Remove_Click(object sender, RoutedEventArgs e)
        {
            // Set the variable for the new item index
            int id;

            // Check if the selected tree item is a member of family 
            if (tree_Actor_IsFamily() && tree_Actor_ID() > 0 && cfg.ActorFamily.Count > 0)
            {
                // Set the updating flag
                updating = true;

                // Get the selected item data
                TreeViewItem tvi = (TreeViewItem)tree_Actor.SelectedItem;

                // Return the actor in the family to the available list
                if (cfg.ActorFamily[tree_Actor_ID() - 1].ActorFamily.Count > 0)
                {
                    foreach (Actor actor in cfg.ActorFamily[tree_Actor_ID() - 1].ActorFamily)
                    {
                        cfg.ActorAvailable.Add(new Actor(actor.ID, actor.Name));
                    }
                }

                // Remove the selected item from the tree and the configuration
                cfg.ActorFamily.RemoveAt(tree_Actor_ID() - 1);
                tree_Actor_Family.Items.Remove(tvi);

                // Renumber the id of the tree items
                id = 1;
                foreach (TreeViewItem tv in tree_Actor_Family.Items)
                {
                    tv.Tag = id.ToString();
                    id++;
                }

                // Renumber the id of the conguguration
                id = 1;
                foreach (DataPackActor actorFamily in cfg.ActorFamily)
                {
                    actorFamily.ID = id;
                    id++;
                }

                // Empty the actor configuration screen
                tree_Actor_Family.IsSelected = true;
                applyEmptyActor();

                // Remove the updating Flag
                updating = false;
            }
        }

        private void btn_Actor_Reset_Click(object sender, RoutedEventArgs e)
        {
            if (tree_Actor_IsFamily())
            {
                cfg.ActorFamily[tree_Actor_ID() - 1].Reset();
                applyActorFamily();
            }
            else if (tree_Actor_IsIndividual())
            {
                cfg.ActorID[tree_Actor_ID() - 1].Reset();
                applyActorID();
            }
            else if (tree_Actor_Default.IsSelected == true)
            {
                cfg.ActorDefault.Reset();
                applyActorDefault();
            }
        }

        #endregion

        #region Clipboard

        private void tree_Actor_Copy_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Check if the selected items is valid
            if (tree_Actor_IsFamily() || tree_Actor_IsIndividual() || tree_Actor_Default.IsSelected == true)
            {
                // Empty the previous clipboard
                cachedActor = null;
                cachedClass = null;
                cachedSkill = null;
                cachedPassiveSkill = null;
                cachedWeapon = null;
                cachedArmor = null;
                cachedEnemy = null;
                cachedState = null;

                // Copy the data from the family
                if (tree_Actor_IsFamily())
                {
                    storeActorFamily();
                    cachedActor = cfg.ActorFamily[tree_Actor_ID() - 1];
                }

                // Copy the data from the individual
                else if (tree_Actor_IsIndividual())
                {
                    storeActorID();
                    cachedActor = cfg.ActorID[tree_Actor_ID() - 1];
                }

                // Copy the data from the default
                else if (tree_Actor_Default.IsSelected == true)
                {
                    storeActorDefault();
                    cachedActor = cfg.ActorDefault;
                }
            }
        }

        private void tree_Actor_Paste_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Check if the selected items is valid
            if (tree_Actor_IsFamily() || tree_Actor_IsIndividual() || tree_Actor_Default.IsSelected == true)
            {
                // Check if an item was copy
                if (cachedActor != null)
                {
                    // Paste the data from the family
                    if (tree_Actor_IsFamily())
                    {
                        cfg.ActorFamily[tree_Actor_ID() - 1].PasteFrom(cachedActor);
                        applyActorFamily();
                    }

                    // Paste the data from the individual
                    else if (tree_Actor_IsIndividual())
                    {
                        cfg.ActorID[tree_Actor_ID() - 1].PasteFrom(cachedActor);
                        applyActorID();
                    }

                    // Paste the data from the default
                    else if (tree_Actor_Default.IsSelected == true)
                    {
                        cfg.ActorDefault.PasteFrom(cachedActor);
                        applyActorDefault();
                    }
                }
            }
        }

        #endregion

        #endregion

        #region Family Configuration

        private void btn_Actor_AddToFamily_Click(object sender, RoutedEventArgs e)
        {
            // Check if you are in a valide time to add an actor to the family
            if (updating || tree_Actor_IsFamily() == false || tree_Actor_IsIndividual() == true || tree_Actor_Default.IsSelected == true || list_Actor_Available.SelectedIndex < 0) { return; }

            // Get the index of the selected actor
            int index = list_Actor_Available.SelectedIndex;

            // Add the actor to the actor in family list
            actorInFamily.Add(new Actor(actorAvailable[index].ID, actorAvailable[index].Name));
            actorInFamily.Sort((x, y) => x.ID.CompareTo(y.ID));

            // Remove the actor from the available list
            actorAvailable.RemoveAt(index);

            // Update the available list index
            if (index > 0) { list_Actor_Available.SelectedIndex = index - 1; }
            else { list_Actor_Available.SelectedIndex = 0; }

            // Store the modification
            storeActorFamily();
        }

        private void btn_Actor_RemoveFromFamily_Click(object sender, RoutedEventArgs e)
        {
            // Check if you are in a valide time to remove from actor to the family
            if (updating || tree_Actor_IsFamily() == false || tree_Actor_IsIndividual() == true || tree_Actor_Default.IsSelected == true || list_Actor_InFamily.SelectedIndex < 0) { return; }

            // Get the index of the selected actor
            int index = list_Actor_InFamily.SelectedIndex;

            // Add the actor to the available list
            actorAvailable.Add(new Actor(actorInFamily[index].ID, actorInFamily[index].Name));
            actorAvailable.Sort((x, y) => x.ID.CompareTo(y.ID));

            // Remove the actor from the actor in family list
            actorInFamily.RemoveAt(index);

            // Update the available list index
            if (index > 0) { list_Actor_InFamily.SelectedIndex = index - 1; }
            else { list_Actor_InFamily.SelectedIndex = 0; }

            // Store the modification
            storeActorFamily();
        }

        #endregion

        #region Equipment Configuration

        #region CheckBox

        private void check_Actor_CustomEquip_Checked(object sender, RoutedEventArgs e)
        {
            // Check if you are in a valide time to update the check_Actor_CustomEquip
            if (updating || (tree_Actor_IsFamily() == false && tree_Actor_IsIndividual() == false && tree_Actor_Default.IsSelected == false)) { return; }

            // Set the updating flag
            updating = true;

            // Empty the window equipment list
            actorEquipType.Clear();
            actorEquipList.Clear();

            // Empty the actor family equipment list
            if (tree_Actor_IsFamily())
            {
                cfg.ActorFamily[tree_Actor_ID() - 1].EquipType.Clear();
                cfg.ActorFamily[tree_Actor_ID() - 1].EquipList.Clear();
            }

            // Empty the actor family equipment list
            if (tree_Actor_IsIndividual())
            {
                cfg.ActorID[tree_Actor_ID() - 1].EquipType.Clear();
                cfg.ActorID[tree_Actor_ID() - 1].EquipList.Clear();
            }

            // Empty the actor family equipment list
            if (tree_Actor_Default.IsSelected)
            {
                cfg.ActorDefault.EquipType.Clear();
                cfg.ActorDefault.EquipList.Clear();
            }

            // Add the content of the default equipment type list when it's not empty to the actor equipment type list
            if (cfg.Default.EquipType.Count > 0)
            {
                foreach (EquipType equip in cfg.Default.EquipType)
                {
                    if (tree_Actor_IsFamily())
                    {
                        cfg.ActorFamily[tree_Actor_ID() - 1].EquipType.Add(new EquipType(equip.ID, equip.Name));
                    }
                    else if (tree_Actor_IsIndividual())
                    {
                        cfg.ActorID[tree_Actor_ID() - 1].EquipType.Add(new EquipType(equip.ID, equip.Name));
                    }
                    else if (tree_Actor_Default.IsSelected)
                    {
                        cfg.ActorDefault.EquipType.Add(new EquipType(equip.ID, equip.Name));
                    }
                    actorEquipType.Add(new EquipType(equip.ID, equip.Name));
                }

                // Update the type textbox
                list_Actor_EquipType.SelectedIndex = 0;
                txt_Actor_NameEquipType.IsEnabled = false;
                txt_Actor_NameEquipType.Text = "";
            }

            // Add the content of the default equipment id list when it's not empty to the actor equipment id list
            if (cfg.Default.EquipList.Count > 0)
            {
                foreach (EquipType equip in cfg.Default.EquipList)
                {
                    if (tree_Actor_IsFamily())
                    {
                        cfg.ActorFamily[tree_Actor_ID() - 1].EquipList.Add(new EquipType(equip.ID, equip.Name));
                    }
                    else if (tree_Actor_IsIndividual())
                    {
                        cfg.ActorID[tree_Actor_ID() - 1].EquipList.Add(new EquipType(equip.ID, equip.Name));
                    }
                    else if (tree_Actor_Default.IsSelected)
                    {
                        cfg.ActorDefault.EquipList.Add(new EquipType(equip.ID, equip.Name));
                    }
                    actorEquipList.Add(new EquipType(equip.ID, cfg.Default.EquipType[equip.ID].Name));
                }

                // Update the list textbox
                list_Actor_EquipList.SelectedIndex = 0;
                txt_Actor_NameEquipList.IsEnabled = true;
                txt_Actor_NameEquipList.Text = actorEquipList[0].Name;

            }

            // Remove the updating Flag
            updating = false;
        }

        private void check_Actor_CustomEquip_Unchecked(object sender, RoutedEventArgs e)
        {
            // Check if you are in a valide time to update the check_Actor_CustomEquip
            if (updating || (tree_Actor_IsFamily() == false && tree_Actor_IsIndividual() == false && tree_Actor_Default.IsSelected == false)) { return; }

            // Empty the window equipment list
            actorEquipType.Clear();
            actorEquipList.Clear();

            // Empty the actor family equipment list
            if (tree_Actor_IsFamily())
            {
                cfg.ActorFamily[tree_Actor_ID() - 1].EquipType.Clear();
                cfg.ActorFamily[tree_Actor_ID() - 1].EquipList.Clear();
            }

            // Empty the actor family equipment list
            if (tree_Actor_IsIndividual())
            {
                cfg.ActorID[tree_Actor_ID() - 1].EquipType.Clear();
                cfg.ActorID[tree_Actor_ID() - 1].EquipList.Clear();
            }

            // Empty the actor family equipment list
            if (tree_Actor_Default.IsSelected)
            {
                cfg.ActorDefault.EquipType.Clear();
                cfg.ActorDefault.EquipList.Clear();
            }

            // Update the type textbox
            txt_Actor_NameEquipType.IsEnabled = false;
            txt_Actor_NameEquipType.Text = "";

            // Update the list textbox
            txt_Actor_NameEquipList.IsEnabled = false;
            txt_Actor_NameEquipList.Text = "";
        }

        #endregion

        #region Equip Type

        private void list_Actor_EquipType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Check if the equipment type list box can be updated
            if (updating || (tree_Actor_IsFamily() == false && tree_Actor_IsIndividual() == false && tree_Actor_Default.IsSelected == false) || check_Actor_CustomEquip.IsChecked == false || list_Actor_EquipType.SelectedIndex < 0) return;

            // Set the updating flag
            updating = true;

            // Store the current index
            int current_index = list_Actor_EquipType.SelectedIndex;

            // Store and update the selected equipment name
            if (lastIndexActorEquipType != current_index && current_index >= 5)
            {
                // Check if the last selected index was a new equip type
                if (lastIndexActorEquipType >= 5)
                {
                    // Check for the selected actor family branch is selected
                    if (tree_Actor_IsFamily())
                    {
                        // Get the name of the last equip type
                        cfg.ActorFamily[tree_Actor_ID() - 1].EquipType[lastIndexActorEquipType].Name = txt_Actor_NameEquipType.Text;

                        // Update the euipment type list
                        actorEquipType.Clear();
                        if (cfg.ActorFamily[tree_Actor_ID() - 1].EquipType.Count > 0)
                        {
                            foreach (EquipType equip in cfg.ActorFamily[tree_Actor_ID() - 1].EquipType)
                            {
                                actorEquipType.Add(new EquipType(equip.ID, equip.Name));
                            }
                        }
                    }
                    // Check for the selected actor individual branch is selected
                    else if (tree_Actor_IsIndividual())
                    {
                        // Get the name of the last equip type
                        cfg.ActorID[tree_Actor_ID() - 1].EquipType[lastIndexActorEquipType].Name = txt_Actor_NameEquipType.Text;

                        // Update the euipment type list
                        actorEquipType.Clear();
                        if (cfg.ActorID[tree_Actor_ID() - 1].EquipType.Count > 0)
                        {
                            foreach (EquipType equip in cfg.ActorID[tree_Actor_ID() - 1].EquipType)
                            {
                                actorEquipType.Add(new EquipType(equip.ID, equip.Name));
                            }
                        }
                    }
                    // Check for the selected actor default branch is selected
                    else if (tree_Actor_Default.IsSelected == true)
                    {
                        // Get the name of the last equip type
                        cfg.ActorDefault.EquipType[lastIndexActorEquipType].Name = txt_Actor_NameEquipType.Text;

                        // Update the euipment type list
                        actorEquipType.Clear();
                        if (cfg.ActorDefault.EquipType.Count > 0)
                        {
                            foreach (EquipType equip in cfg.ActorDefault.EquipType)
                            {
                                actorEquipType.Add(new EquipType(equip.ID, equip.Name));
                            }
                        }
                    }

                    // Reset the list to the current index
                    list_Actor_EquipType.SelectedIndex = current_index;
                }

                // Update and enable the equip type name text box
                txt_Actor_NameEquipType.IsEnabled = true;
                txt_Actor_NameEquipType.Text = actorEquipType[current_index].Name;
            }
            else
            {
                // Empty and disable the equip type name text box
                txt_Actor_NameEquipType.IsEnabled = false;
                txt_Actor_NameEquipType.Text = "";
            }

            // Set the current index to the last index for the next update
            lastIndexActorEquipType = current_index;

            // Remove the updating flag
            updating = false;
        }

        #endregion

        #region Equip List

        #region Update name

        private void list_Actor_EquipList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Check if the equipment type list box can be updated
            if (updating || (tree_Actor_IsFamily() == false && tree_Actor_IsIndividual() == false && tree_Actor_Default.IsSelected == false) || check_Actor_CustomEquip.IsChecked == false || list_Actor_EquipList.SelectedIndex < 0) return;

            // Set the updating flag
            updating = true;

            // Store the current index
            int current_index = list_Actor_EquipList.SelectedIndex;

            if (lastIndexActorEquipList != current_index)
            {
                // Check for the selected actor family branch is selected
                if (tree_Actor_IsFamily())
                {
                    // Get the name of the last equip type
                    cfg.ActorFamily[tree_Actor_ID() - 1].EquipList[lastIndexActorEquipList].Name = txt_Actor_NameEquipList.Text;

                    // Update the euipment type list
                    actorEquipList.Clear();
                    if (cfg.ActorFamily[tree_Actor_ID() - 1].EquipList.Count > 0)
                    {
                        foreach (EquipType equip in cfg.ActorFamily[tree_Actor_ID() - 1].EquipList)
                        {
                            actorEquipList.Add(new EquipType(equip.ID, equip.Name));
                        }
                    }
                }
                // Check for the selected actor individual branch is selected
                else if (tree_Actor_IsIndividual())
                {
                    // Get the name of the last equip type
                    cfg.ActorID[tree_Actor_ID() - 1].EquipList[lastIndexActorEquipList].Name = txt_Actor_NameEquipList.Text;

                    // Update the euipment type list
                    actorEquipList.Clear();
                    if (cfg.ActorID[tree_Actor_ID() - 1].EquipList.Count > 0)
                    {
                        foreach (EquipType equip in cfg.ActorID[tree_Actor_ID() - 1].EquipList)
                        {
                            actorEquipList.Add(new EquipType(equip.ID, equip.Name));
                        }
                    }
                }
                // Check for the selected actor default branch is selected
                else if (tree_Actor_Default.IsSelected == true)
                {
                    // Get the name of the last equip type
                    cfg.ActorDefault.EquipList[lastIndexActorEquipList].Name = txt_Actor_NameEquipList.Text;

                    // Update the euipment type list
                    actorEquipList.Clear();
                    if (cfg.ActorDefault.EquipList.Count > 0)
                    {
                        foreach (EquipType equip in cfg.ActorDefault.EquipList)
                        {
                            actorEquipList.Add(new EquipType(equip.ID, equip.Name));
                        }
                    }
                }

                // Reset the list to the current index
                list_Actor_EquipList.SelectedIndex = current_index;
            }

            // Update the equip type name text box
            txt_Actor_NameEquipList.Text = actorEquipList[current_index].Name;

            // Set the current index to the last index for the next update
            lastIndexActorEquipList = current_index;

            // Remove the updating flag
            updating = false;
        }

        #endregion

        #region Add and remove equipment

        private void btn_Actor_AddEquipList_Click(object sender, RoutedEventArgs e)
        {
            // Check if an element of the equip type list is selected
            if (updating || (tree_Actor_IsFamily() == false && tree_Actor_IsIndividual() == false && tree_Actor_Default.IsSelected == false) || check_Actor_CustomEquip.IsChecked == false || list_Actor_EquipType.SelectedIndex < 0) return;
            
            // Add the selected element to the ui list
            actorEquipList.Add(new EquipType(actorEquipType[list_Actor_EquipType.SelectedIndex].ID, actorEquipType[list_Actor_EquipType.SelectedIndex].Name));

            // Check for the selected actor branch
            if (tree_Actor_IsFamily())
            {
                // Add the selected element to the actor family list
                cfg.ActorFamily[tree_Actor_ID() - 1].EquipList.Add(new EquipType(actorEquipType[list_Actor_EquipType.SelectedIndex].ID, actorEquipType[list_Actor_EquipType.SelectedIndex].Name));
            }
            else if (tree_Actor_IsIndividual())
            {
                // Add the selected element to the actor individual list
                cfg.ActorID[tree_Actor_ID() - 1].EquipList.Add(new EquipType(actorEquipType[list_Actor_EquipType.SelectedIndex].ID, actorEquipType[list_Actor_EquipType.SelectedIndex].Name));
            }
            else if (tree_Actor_Default.IsSelected == true)
            {
                // Add the selected element to the actor default list
                cfg.ActorDefault.EquipList.Add(new EquipType(actorEquipType[list_Actor_EquipType.SelectedIndex].ID, actorEquipType[list_Actor_EquipType.SelectedIndex].Name));
            }
        }

        private void btn_Actor_RemoveEquipList_Click(object sender, RoutedEventArgs e)
        {
            // Check if an element of the equip list box is selected
            if (updating || (tree_Actor_IsFamily() == false && tree_Actor_IsIndividual() == false && tree_Actor_Default.IsSelected == false) || check_Actor_CustomEquip.IsChecked == false || list_Actor_EquipList.SelectedIndex < 0) return;

            // Check for the selected actor branch
            if (tree_Actor_IsFamily())
            {
                // Remove the selected element from the actor family list
                cfg.ActorFamily[tree_Actor_ID() - 1].EquipList.RemoveAt(list_Actor_EquipList.SelectedIndex);
            }
            else if (tree_Actor_IsIndividual())
            {
                // Remove the selected element from the actor individual list
                cfg.ActorID[tree_Actor_ID() - 1].EquipList.RemoveAt(list_Actor_EquipList.SelectedIndex);
            }
            else if (tree_Actor_Default.IsSelected == true)
            {
                // Remove the selected element from the actor default list
                cfg.ActorDefault.EquipList.RemoveAt(list_Actor_EquipList.SelectedIndex);
            }

            // Remove the selected element from the ui list
            actorEquipList.RemoveAt(list_Actor_EquipList.SelectedIndex);
        }

        #endregion

        #region Move equipment in list

        private void btn_Actor_UpEquipList_Click(object sender, RoutedEventArgs e)
        {
            // Check if the equipment type list box can be updated
            if (updating || list_Actor_EquipList.SelectedIndex < 0) return;

            // Check if the selected index is before the first element
            if (list_Actor_EquipList.SelectedIndex > 0)
            {
                // Move the selected equipment down
                actorEquipList.Move(list_Actor_EquipList.SelectedIndex, list_Actor_EquipList.SelectedIndex - 1);
            }
            else
            {
                // Return the selected index to the bottom
                actorEquipList.Move(list_Actor_EquipList.SelectedIndex, actorEquipList.Count - 1);
            }

            // Check for the selected actor branch
            if (tree_Actor_IsFamily())
            {
                // Store the new actor family data
                storeActorFamily();
            }
            else if (tree_Actor_IsIndividual())
            {
                // Store the new actor individual data
                storeActorID();
            }
            else if (tree_Actor_Default.IsSelected == true)
            {
                // Store the new actor default data
                storeActorDefault();
            }
        }

        private void btn_Actor_DownEquipList_Click(object sender, RoutedEventArgs e)
        {
            // Check if the equipment type list box can be updated
            if (updating || list_Actor_EquipList.SelectedIndex < 0) { return; }

            // Check if the selected index is before the last element
            if (list_Actor_EquipList.SelectedIndex < actorEquipList.Count - 1)
            {
                // Move the selected equipment down
                actorEquipList.Move(list_Actor_EquipList.SelectedIndex, list_Actor_EquipList.SelectedIndex + 1);
            }
            else
            {
                // Return the selected index to the top
                actorEquipList.Move(list_Actor_EquipList.SelectedIndex, 0);
            }

            // Check for the selected actor branch
            if (tree_Actor_IsFamily())
            {
                // Store the new actor family data
                storeActorFamily();
            }
            else if (tree_Actor_IsIndividual())
            {
                // Store the new actor individual data
                storeActorID();
            }
            else if (tree_Actor_Default.IsSelected == true)
            {
                // Store the new actor default data
                storeActorDefault();
            }
        }

        #endregion

        #region Reset Name

        private void btn_Actor_ResetNameEquipList_Click(object sender, RoutedEventArgs e)
        {
            // Check if the equipment type list box can be updated
            if (updating || list_Actor_EquipList.SelectedIndex < 0) return;

            // Set the updating flag
            updating = true;

            // Store the current index
            int current_index = list_Actor_EquipList.SelectedIndex;

            // Check for the selected actor family branch is selected
            if (tree_Actor_IsFamily())
            {
                // Get the default name for the selected equipment
                cfg.ActorFamily[tree_Actor_ID() - 1].EquipList[current_index].Name = cfg.ActorFamily[tree_Actor_ID() - 1].EquipType[cfg.ActorFamily[tree_Actor_ID() - 1].EquipList[current_index].ID].Name;

                // Update the euipment type list
                actorEquipList.Clear();
                if (cfg.ActorFamily[tree_Actor_ID() - 1].EquipList.Count > 0)
                {
                    foreach (EquipType equip in cfg.ActorFamily[tree_Actor_ID() - 1].EquipList)
                    {
                        actorEquipList.Add(new EquipType(equip.ID, equip.Name));
                    }
                }
            }
            // Check for the selected actor individual branch is selected
            else if (tree_Actor_IsIndividual())
            {
                // Get the default name for the selected equipment
                cfg.ActorID[tree_Actor_ID() - 1].EquipList[current_index].Name = cfg.ActorID[tree_Actor_ID() - 1].EquipType[cfg.ActorID[tree_Actor_ID() - 1].EquipList[current_index].ID].Name;

                // Update the euipment type list
                actorEquipList.Clear();
                if (cfg.ActorID[tree_Actor_ID() - 1].EquipList.Count > 0)
                {
                    foreach (EquipType equip in cfg.ActorID[tree_Actor_ID() - 1].EquipList)
                    {
                        actorEquipList.Add(new EquipType(equip.ID, equip.Name));
                    }
                }
            }
            // Check for the selected actor default branch is selected
            else if (tree_Actor_Default.IsSelected == true)
            {
                // Get the default name for the selected equipment
                cfg.ActorDefault.EquipList[current_index].Name = cfg.ActorDefault.EquipType[cfg.ActorDefault.EquipList[current_index].ID].Name;

                // Update the euipment type list
                actorEquipList.Clear();
                if (cfg.ActorDefault.EquipList.Count > 0)
                {
                    foreach (EquipType equip in cfg.ActorDefault.EquipList)
                    {
                        actorEquipList.Add(new EquipType(equip.ID, equip.Name));
                    }
                }
            }

            // Reset the list to the current index
            list_Actor_EquipList.SelectedIndex = current_index;

            // Update the equip type name text box
            txt_Actor_NameEquipList.Text = actorEquipList[current_index].Name;

            // Set the current index to the last index for the next update
            lastIndexActorEquipList = list_Actor_EquipList.SelectedIndex;

            // Remove the updating flag
            updating = false;
        }

        private void btn_Actor_ResetAllNameEquipList_Click(object sender, RoutedEventArgs e)
        {
            // Check if the equipment type list box can be updated
            if (updating || list_Actor_EquipList.SelectedIndex < 0) return;

            // Set the updating flag
            updating = true;

            // Store the current index
            int current_index = list_Actor_EquipList.SelectedIndex;
            
            // Check for the selected actor family branch is selected
            if (tree_Actor_IsFamily())
            {
                // Get the default name for the all equipment
                if (cfg.ActorFamily[tree_Actor_ID() - 1].EquipList.Count > 0)
                {
                    foreach (EquipType equip in cfg.ActorFamily[tree_Actor_ID() - 1].EquipList)
                    {
                        equip.Name = cfg.ActorFamily[tree_Actor_ID() - 1].EquipType[equip.ID].Name;
                    }
                }

                // Update the euipment type list
                actorEquipList.Clear();
                if (cfg.ActorFamily[tree_Actor_ID() - 1].EquipList.Count > 0)
                {
                    foreach (EquipType equip in cfg.ActorFamily[tree_Actor_ID() - 1].EquipList)
                    {
                        actorEquipList.Add(new EquipType(equip.ID, equip.Name));
                    }
                }
            }
            // Check for the selected actor individual branch is selected
            else if (tree_Actor_IsIndividual())
            {
                // Get the default name for the all equipment
                if (cfg.ActorID[tree_Actor_ID() - 1].EquipList.Count > 0)
                {
                    foreach (EquipType equip in cfg.ActorID[tree_Actor_ID() - 1].EquipList)
                    {
                        equip.Name = cfg.ActorID[tree_Actor_ID() - 1].EquipType[equip.ID].Name;
                    }
                }

                // Update the euipment type list
                actorEquipList.Clear();
                if (cfg.ActorID[tree_Actor_ID() - 1].EquipList.Count > 0)
                {
                    foreach (EquipType equip in cfg.ActorID[tree_Actor_ID() - 1].EquipList)
                    {
                        actorEquipList.Add(new EquipType(equip.ID, equip.Name));
                    }
                }
            }
            // Check for the selected actor default branch is selected
            else if (tree_Actor_Default.IsSelected == true)
            {
                // Get the default name for the all equipment
                if (cfg.ActorDefault.EquipList.Count > 0)
                {
                    foreach (EquipType equip in cfg.ActorDefault.EquipList)
                    {
                        equip.Name = cfg.ActorDefault.EquipType[equip.ID].Name;
                    }
                }

                // Update the euipment type list
                actorEquipList.Clear();
                if (cfg.ActorDefault.EquipList.Count > 0)
                {
                    foreach (EquipType equip in cfg.ActorDefault.EquipList)
                    {
                        actorEquipList.Add(new EquipType(equip.ID, equip.Name));
                    }
                }
            }

            // Reset the list to the current index
            list_Actor_EquipList.SelectedIndex = current_index;

            // Update the equip type name text box
            txt_Actor_NameEquipList.Text = actorEquipList[current_index].Name;

            // Set the current index to the last index for the next update
            lastIndexActorEquipList = list_Actor_EquipList.SelectedIndex;

            // Remove the updating flag
            updating = false;
        }

        #endregion

        #endregion

        #endregion

        #region Parameter Configuration

        private void check_Actor_Parameter_Checked(object sender, RoutedEventArgs e)
        {
            // Check if you are in a valide time to update the actor parameter
            if (updating || (tree_Actor_IsFamily() == false && tree_Actor_IsIndividual() == false && tree_Actor_Default.IsSelected == false)) { return; }

            // Get the currently modified checkbox
            CheckBox checkBox = sender as CheckBox;

            // Store the data of the actor family and reload it except for the modified checkbox
            if (tree_Actor_IsFamily())
            {
                storeActorFamily(checkBox.Name);
                applyActorFamily();
            }
            // Store the data of the actor individual and reload it except for the modified checkbox
            else if (tree_Actor_IsIndividual())
            {
                storeActorID(checkBox.Name);
                applyActorID();
            }
            // Store the data of the actor default and reload it except for the modified checkbox
            else if (tree_Actor_Default.IsSelected)
            {
                storeActorDefault(checkBox.Name);
                applyActorDefault();
            }
        }

        private void check_Actor_Parameter_Unchecked(object sender, RoutedEventArgs e)
        {
            // Check if you are in a valide time to update the actor parameter
            if (updating || (tree_Actor_IsFamily() == false && tree_Actor_IsIndividual() == false && tree_Actor_Default.IsSelected == false)) { return; }

            // Store the data of the actor family and reload it
            if (tree_Actor_IsFamily())
            {
                storeActorFamily();
                applyActorFamily();
            }
            // Store the data of the actor individual and reload it
            else if (tree_Actor_IsIndividual())
            {
                storeActorID();
                applyActorID();
            }
            // Store the data of the actor default and reload it
            else if (tree_Actor_Default.IsSelected)
            {
                storeActorDefault();
                applyActorDefault();
            }
        }

        #endregion

        #endregion

        #region Class

        #region TreeView

        # region Get Selected

        private bool tree_Class_IsFamily()
        {
            // Get the selected item data
            TreeViewItem tvi = (TreeViewItem)tree_Class.SelectedItem;

            // Check if the selected item is a member of the family branch
            if (tree_Class_Family.IsSelected == false && tree_Class_Individual.IsSelected == false && tree_Class_Default.IsSelected == false && tree_Class_Family.IsAncestorOf(tvi))
            {
                return true;
            }

            return false;
        }

        private bool tree_Class_IsIndividual()
        {
            // Get the selected item data
            TreeViewItem tvi = (TreeViewItem)tree_Class.SelectedItem;

            // Check if the selected item is a member of the individual branch
            if (tree_Class_Family.IsSelected == false && tree_Class_Individual.IsSelected == false && tree_Class_Default.IsSelected == false && tree_Class_Individual.IsAncestorOf(tvi))
            {
                return true;
            }

            return false;
        }

        private int tree_Class_ID()
        {
            // Set the variable use to convert the string to an integer
            int result;
            int id = -1;

            // Get the selected item data
            TreeViewItem tvi = (TreeViewItem)tree_Class.SelectedItem;

            // Check if you the selected item is a member of the family branch or the individual branch
            if ((tree_Class_IsFamily() || tree_Class_IsIndividual()) && tree_Class_Default.IsSelected == false)
            {
                // Try to convert the string to an integer
                if (Int32.TryParse(tvi.Tag.ToString(), out result))
                {
                    id = result;
                }
            }

            return id;
        }

        #endregion

        #region Update data

        private void tree_Class_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            // Check if the the treeview can be updated
            if (updating) { return; }

            // Store the class family data if the previous selected item was a member of the family branch
            if (lastIndexClassFamily > 0)
            {
                storeClassFamily(lastIndexClassFamily);

                // Update the navigation tree
                int i = 0;
                foreach (TreeViewItem treeItem in tree_Class_Family.Items)
                {
                    treeItem.Header = cfg.ClassFamily[i].Name;
                    i++;
                }
            }

            // Store the class individual data if the previous selected item was a member of the individual branch
            if (lastIndexClassID > 0)
            {
                storeClassID(lastIndexClassID);
            }

            // Store the class default data if the previous selected item was the default member
            if (lastIndexClassDefault)
            {
                storeClassDefault();
            }

            // Update the previous index
            lastIndexClassFamily = 0;
            lastIndexClassID = 0;
            lastIndexClassDefault = false;


            // Check if the selected item is a member of the family branch
            if (tree_Class_IsFamily())
            {
                // Update the previous index
                lastIndexClassFamily = tree_Class_ID();

                // Apply the new class family data
                applyClassFamily();
            }
            else if (tree_Class_IsIndividual())
            {
                // Update the previous index
                lastIndexClassID = tree_Class_ID();

                // Apply the new class individual data
                applyClassID();
            }
            else if (tree_Class_Default.IsSelected)
            {
                // Update the previous index
                lastIndexClassDefault = true;

                // Apply the new class default data
                applyClassDefault();
            }
            else
            {
                // Empty the class data when their is no valid item selected
                applyEmptyClass();
            }
        }

        #endregion

        #region Manage data

        private void btn_Class_Add_Click(object sender, RoutedEventArgs e)
        {
            // Get the new index
            int id = tree_Class_Family.Items.Count + 1;

            // Create the new item in the tree and the configuration
            cfg.ClassFamily.Add(new DataPackClass(id));
            tree_Class_Family.Items.Add(new TreeViewItem() { Header = cfg.ClassFamily[id - 1].Name, Tag = id.ToString() });
        }

        private void btn_Class_Remove_Click(object sender, RoutedEventArgs e)
        {
            // Set the variable for the new item index
            int id;

            // Check if the selected tree item is a member of family 
            if (tree_Class_IsFamily() && tree_Class_ID() > 0 && cfg.ClassFamily.Count > 0)
            {
                // Set the updating flag
                updating = true;

                // Get the selected item data
                TreeViewItem tvi = (TreeViewItem)tree_Class.SelectedItem;

                // Return the Class in the family to the available list
                if (cfg.ClassFamily[tree_Class_ID() - 1].ClassFamily.Count > 0)
                {
                    foreach (Class classes in cfg.ClassFamily[tree_Class_ID() - 1].ClassFamily)
                    {
                        cfg.ClassAvailable.Add(new Class(classes.ID, classes.Name));
                    }
                }

                // Remove the selected item from the tree and the configuration
                cfg.ClassFamily.RemoveAt(tree_Class_ID() - 1);
                tree_Class_Family.Items.Remove(tvi);

                // Renumber the id of the tree items
                id = 1;
                foreach (TreeViewItem tv in tree_Class_Family.Items)
                {
                    tv.Tag = id.ToString();
                    id++;
                }

                // Renumber the id of the conguguration
                id = 1;
                foreach (DataPackClass classFamily in cfg.ClassFamily)
                {
                    classFamily.ID = id;
                    id++;
                }

                // Empty the class configuration screen
                tree_Class_Family.IsSelected = true;
                applyEmptyClass();

                // Remove the updating Flag
                updating = false;
            }
        }

        private void btn_Class_Reset_Click(object sender, RoutedEventArgs e)
        {
            if (tree_Class_IsFamily())
            {
                cfg.ActorFamily[tree_Class_ID() - 1].Reset();
                applyActorFamily();
            }
            else if (tree_Class_IsIndividual())
            {
                cfg.ActorID[tree_Class_ID() - 1].Reset();
                applyActorID();
            }
            else if (tree_Class_Default.IsSelected == true)
            {
                cfg.ActorDefault.Reset();
                applyActorDefault();
            }
        }

        #endregion

        #region Clipboard

        private void tree_Class_Copy_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Check if the selected items is valid
            if (tree_Class_IsFamily() || tree_Class_IsIndividual() || tree_Class_Default.IsSelected == true)
            {
                // Empty the previous clipboard
                cachedActor = null;
                cachedClass = null;
                cachedSkill = null;
                cachedPassiveSkill = null;
                cachedWeapon = null;
                cachedArmor = null;
                cachedEnemy = null;
                cachedState = null;

                // Copy the data from the family
                if (tree_Class_IsFamily())
                {
                    storeClassFamily();
                    cachedClass = cfg.ClassFamily[tree_Class_ID() - 1];
                }

                // Copy the data from the individual
                else if (tree_Class_IsIndividual())
                {
                    storeClassID();
                    cachedClass = cfg.ClassID[tree_Class_ID() - 1];
                }

                // Copy the data from the default
                else if (tree_Class_Default.IsSelected == true)
                {
                    storeClassDefault();
                    cachedClass = cfg.ClassDefault;
                }
            }
        }

        private void tree_Class_Paste_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Check if the selected items is valid
            if (tree_Class_IsFamily() || tree_Class_IsIndividual() || tree_Class_Default.IsSelected == true)
            {
                // Check if an item was copy
                if (cachedClass != null)
                {
                    // Paste the data from the family
                    if (tree_Class_IsFamily())
                    {
                        cfg.ClassFamily[tree_Class_ID() - 1].PasteFrom(cachedClass);
                        applyClassFamily();
                    }

                    // Paste the data from the individual
                    else if (tree_Class_IsIndividual())
                    {
                        cfg.ClassID[tree_Class_ID() - 1].PasteFrom(cachedClass);
                        applyClassID();
                    }

                    // Paste the data from the default
                    else if (tree_Class_Default.IsSelected == true)
                    {
                        cfg.ClassDefault.PasteFrom(cachedClass);
                        applyClassDefault();
                    }
                }
            }
        }

        #endregion

        #endregion

        #region Family Configuration

        private void btn_Class_AddToFamily_Click(object sender, RoutedEventArgs e)
        {
            // Check if you are in a valide time to add an class to the family
            if (updating || tree_Class_IsFamily() == false || tree_Class_IsIndividual() == true || tree_Class_Default.IsSelected == true || list_Class_Available.SelectedIndex < 0) { return; }

            // Get the index of the selected class
            int index = list_Class_Available.SelectedIndex;

            // Add the class to the class in family list
            classInFamily.Add(new Class(classAvailable[index].ID, classAvailable[index].Name));
            classInFamily.Sort((x, y) => x.ID.CompareTo(y.ID));

            // Remove the class from the available list
            classAvailable.RemoveAt(index);

            // Update the available list index
            if (index > 0) { list_Class_Available.SelectedIndex = index - 1; }
            else { list_Class_Available.SelectedIndex = 0; }

            // Store the modification
            storeClassFamily();
        }

        private void btn_Class_RemoveFromFamily_Click(object sender, RoutedEventArgs e)
        {
            // Check if you are in a valide time to remove from class to the family
            if (updating || tree_Class_IsFamily() == false || tree_Class_IsIndividual() == true || tree_Class_Default.IsSelected == true || list_Class_InFamily.SelectedIndex < 0) { return; }

            // Get the index of the selected class
            int index = list_Class_InFamily.SelectedIndex;

            // Add the class to the available list
            classAvailable.Add(new Class(classInFamily[index].ID, classInFamily[index].Name));
            classAvailable.Sort((x, y) => x.ID.CompareTo(y.ID));

            // Remove the class from the class in family list
            classInFamily.RemoveAt(index);

            // Update the available list index
            if (index > 0) { list_Class_InFamily.SelectedIndex = index - 1; }
            else { list_Class_InFamily.SelectedIndex = 0; }

            // Store the modification
            storeClassFamily();
        }

        #endregion

        #region Equipment Configuration

        #region CheckBox

        private void check_Class_CustomEquip_Checked(object sender, RoutedEventArgs e)
        {
            // Check if you are in a valide time to update the check_Class_CustomEquip
            if (updating || (tree_Class_IsFamily() == false && tree_Class_IsIndividual() == false && tree_Class_Default.IsSelected == false)) { return; }

            // Set the updating flag
            updating = true;

            // Empty the window equipment list
            classEquipType.Clear();
            classEquipList.Clear();

            // Empty the class family equipment list
            if (tree_Class_IsFamily())
            {
                cfg.ClassFamily[tree_Class_ID() - 1].EquipType.Clear();
                cfg.ClassFamily[tree_Class_ID() - 1].EquipList.Clear();
            }

            // Empty the class family equipment list
            if (tree_Class_IsIndividual())
            {
                cfg.ClassID[tree_Class_ID() - 1].EquipType.Clear();
                cfg.ClassID[tree_Class_ID() - 1].EquipList.Clear();
            }

            // Empty the class family equipment list
            if (tree_Class_Default.IsSelected)
            {
                cfg.ClassDefault.EquipType.Clear();
                cfg.ClassDefault.EquipList.Clear();
            }

            // Add the content of the default equipment type list when it's not empty to the class equipment type list
            if (cfg.Default.EquipType.Count > 0)
            {
                foreach (EquipType equip in cfg.Default.EquipType)
                {
                    if (tree_Class_IsFamily())
                    {
                        cfg.ClassFamily[tree_Class_ID() - 1].EquipType.Add(new EquipType(equip.ID, equip.Name));
                    }
                    else if (tree_Class_IsIndividual())
                    {
                        cfg.ClassID[tree_Class_ID() - 1].EquipType.Add(new EquipType(equip.ID, equip.Name));
                    }
                    else if (tree_Class_Default.IsSelected)
                    {
                        cfg.ClassDefault.EquipType.Add(new EquipType(equip.ID, equip.Name));
                    }
                    classEquipType.Add(new EquipType(equip.ID, equip.Name));
                }

                // Update the type textbox
                list_Class_EquipType.SelectedIndex = 0;
                txt_Class_NameEquipType.IsEnabled = false;
                txt_Class_NameEquipType.Text = "";
            }

            // Add the content of the default equipment id list when it's not empty to the class equipment id list
            if (cfg.Default.EquipList.Count > 0)
            {
                foreach (EquipType equip in cfg.Default.EquipList)
                {
                    if (tree_Class_IsFamily())
                    {
                        cfg.ClassFamily[tree_Class_ID() - 1].EquipList.Add(new EquipType(equip.ID, equip.Name));
                    }
                    else if (tree_Class_IsIndividual())
                    {
                        cfg.ClassID[tree_Class_ID() - 1].EquipList.Add(new EquipType(equip.ID, equip.Name));
                    }
                    else if (tree_Class_Default.IsSelected)
                    {
                        cfg.ClassDefault.EquipList.Add(new EquipType(equip.ID, equip.Name));
                    }
                    classEquipList.Add(new EquipType(equip.ID, cfg.Default.EquipType[equip.ID].Name));
                }

                // Update the list textbox
                list_Class_EquipList.SelectedIndex = 0;
                txt_Class_NameEquipList.IsEnabled = true;
                txt_Class_NameEquipList.Text = classEquipList[0].Name;

            }

            // Remove the updating Flag
            updating = false;
        }

        private void check_Class_CustomEquip_Unchecked(object sender, RoutedEventArgs e)
        {
            // Check if you are in a valide time to update the check_Class_CustomEquip
            if (updating || (tree_Class_IsFamily() == false && tree_Class_IsIndividual() == false && tree_Class_Default.IsSelected == false)) { return; }

            // Empty the window equipment list
            classEquipType.Clear();
            classEquipList.Clear();

            // Empty the class family equipment list
            if (tree_Class_IsFamily())
            {
                cfg.ClassFamily[tree_Class_ID() - 1].EquipType.Clear();
                cfg.ClassFamily[tree_Class_ID() - 1].EquipList.Clear();
            }

            // Empty the class family equipment list
            if (tree_Class_IsIndividual())
            {
                cfg.ClassID[tree_Class_ID() - 1].EquipType.Clear();
                cfg.ClassID[tree_Class_ID() - 1].EquipList.Clear();
            }

            // Empty the class family equipment list
            if (tree_Class_Default.IsSelected)
            {
                cfg.ClassDefault.EquipType.Clear();
                cfg.ClassDefault.EquipList.Clear();
            }

            // Update the type textbox
            txt_Class_NameEquipType.IsEnabled = false;
            txt_Class_NameEquipType.Text = "";

            // Update the list textbox
            txt_Class_NameEquipList.IsEnabled = false;
            txt_Class_NameEquipList.Text = "";
        }

        #endregion

        #region Equip Type

        private void list_Class_EquipType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Check if the equipment type list box can be updated
            if (updating || (tree_Class_IsFamily() == false && tree_Class_IsIndividual() == false && tree_Class_Default.IsSelected == false) || check_Class_CustomEquip.IsChecked == false || list_Class_EquipType.SelectedIndex < 0) return;

            // Set the updating flag
            updating = true;

            // Store the current index
            int current_index = list_Class_EquipType.SelectedIndex;

            // Store and update the selected equipment name
            if (lastIndexClassEquipType != current_index && current_index >= 5)
            {
                // Check if the last selected index was a new equip type
                if (lastIndexClassEquipType >= 5)
                {
                    // Check for the selected class family branch is selected
                    if (tree_Class_IsFamily())
                    {
                        // Get the name of the last equip type
                        cfg.ClassFamily[tree_Class_ID() - 1].EquipType[lastIndexClassEquipType].Name = txt_Class_NameEquipType.Text;

                        // Update the euipment type list
                        classEquipType.Clear();
                        if (cfg.ClassFamily[tree_Class_ID() - 1].EquipType.Count > 0)
                        {
                            foreach (EquipType equip in cfg.ClassFamily[tree_Class_ID() - 1].EquipType)
                            {
                                classEquipType.Add(new EquipType(equip.ID, equip.Name));
                            }
                        }
                    }
                    // Check for the selected class individual branch is selected
                    else if (tree_Class_IsIndividual())
                    {
                        // Get the name of the last equip type
                        cfg.ClassID[tree_Class_ID() - 1].EquipType[lastIndexClassEquipType].Name = txt_Class_NameEquipType.Text;

                        // Update the euipment type list
                        classEquipType.Clear();
                        if (cfg.ClassID[tree_Class_ID() - 1].EquipType.Count > 0)
                        {
                            foreach (EquipType equip in cfg.ClassID[tree_Class_ID() - 1].EquipType)
                            {
                                classEquipType.Add(new EquipType(equip.ID, equip.Name));
                            }
                        }
                    }
                    // Check for the selected class default branch is selected
                    else if (tree_Class_Default.IsSelected == true)
                    {
                        // Get the name of the last equip type
                        cfg.ClassDefault.EquipType[lastIndexClassEquipType].Name = txt_Class_NameEquipType.Text;

                        // Update the euipment type list
                        classEquipType.Clear();
                        if (cfg.ClassDefault.EquipType.Count > 0)
                        {
                            foreach (EquipType equip in cfg.ClassDefault.EquipType)
                            {
                                classEquipType.Add(new EquipType(equip.ID, equip.Name));
                            }
                        }
                    }

                    // Reset the list to the current index
                    list_Class_EquipType.SelectedIndex = current_index;
                }

                // Update and enable the equip type name text box
                txt_Class_NameEquipType.IsEnabled = true;
                txt_Class_NameEquipType.Text = classEquipType[current_index].Name;
            }
            else
            {
                // Empty and disable the equip type name text box
                txt_Class_NameEquipType.IsEnabled = false;
                txt_Class_NameEquipType.Text = "";
            }

            // Set the current index to the last index for the next update
            lastIndexClassEquipType = current_index;

            // Remove the updating flag
            updating = false;
        }

        #endregion

        #region Equip List

        #region Update name

        private void list_Class_EquipList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Check if the equipment type list box can be updated
            if (updating || (tree_Class_IsFamily() == false && tree_Class_IsIndividual() == false && tree_Class_Default.IsSelected == false) || check_Class_CustomEquip.IsChecked == false || list_Class_EquipList.SelectedIndex < 0) return;

            // Set the updating flag
            updating = true;

            // Store the current index
            int current_index = list_Class_EquipList.SelectedIndex;

            if (lastIndexClassEquipList != current_index)
            {
                // Check for the selected class family branch is selected
                if (tree_Class_IsFamily())
                {
                    // Get the name of the last equip type
                    cfg.ClassFamily[tree_Class_ID() - 1].EquipList[lastIndexClassEquipList].Name = txt_Class_NameEquipList.Text;

                    // Update the euipment type list
                    classEquipList.Clear();
                    if (cfg.ClassFamily[tree_Class_ID() - 1].EquipList.Count > 0)
                    {
                        foreach (EquipType equip in cfg.ClassFamily[tree_Class_ID() - 1].EquipList)
                        {
                            classEquipList.Add(new EquipType(equip.ID, equip.Name));
                        }
                    }
                }
                // Check for the selected class individual branch is selected
                else if (tree_Class_IsIndividual())
                {
                    // Get the name of the last equip type
                    cfg.ClassID[tree_Class_ID() - 1].EquipList[lastIndexClassEquipList].Name = txt_Class_NameEquipList.Text;

                    // Update the euipment type list
                    classEquipList.Clear();
                    if (cfg.ClassID[tree_Class_ID() - 1].EquipList.Count > 0)
                    {
                        foreach (EquipType equip in cfg.ClassID[tree_Class_ID() - 1].EquipList)
                        {
                            classEquipList.Add(new EquipType(equip.ID, equip.Name));
                        }
                    }
                }
                // Check for the selected class default branch is selected
                else if (tree_Class_Default.IsSelected == true)
                {
                    // Get the name of the last equip type
                    cfg.ClassDefault.EquipList[lastIndexClassEquipList].Name = txt_Class_NameEquipList.Text;

                    // Update the euipment type list
                    classEquipList.Clear();
                    if (cfg.ClassDefault.EquipList.Count > 0)
                    {
                        foreach (EquipType equip in cfg.ClassDefault.EquipList)
                        {
                            classEquipList.Add(new EquipType(equip.ID, equip.Name));
                        }
                    }
                }

                // Reset the list to the current index
                list_Class_EquipList.SelectedIndex = current_index;
            }

            // Update the equip type name text box
            txt_Class_NameEquipList.Text = classEquipList[current_index].Name;

            // Set the current index to the last index for the next update
            lastIndexClassEquipList = current_index;

            // Remove the updating flag
            updating = false;
        }

        #endregion

        #region Add and remove equipment

        private void btn_Class_AddEquipList_Click(object sender, RoutedEventArgs e)
        {
            // Check if an element of the equip type list is selected
            if (updating || (tree_Class_IsFamily() == false && tree_Class_IsIndividual() == false && tree_Class_Default.IsSelected == false) || check_Class_CustomEquip.IsChecked == false || list_Class_EquipType.SelectedIndex < 0) return;

            // Add the selected element to the ui list
            classEquipList.Add(new EquipType(classEquipType[list_Class_EquipType.SelectedIndex].ID, classEquipType[list_Class_EquipType.SelectedIndex].Name));

            // Check for the selected class branch
            if (tree_Class_IsFamily())
            {
                // Add the selected element to the class family list
                cfg.ClassFamily[tree_Class_ID() - 1].EquipList.Add(new EquipType(classEquipType[list_Class_EquipType.SelectedIndex].ID, classEquipType[list_Class_EquipType.SelectedIndex].Name));
            }
            else if (tree_Class_IsIndividual())
            {
                // Add the selected element to the class individual list
                cfg.ClassID[tree_Class_ID() - 1].EquipList.Add(new EquipType(classEquipType[list_Class_EquipType.SelectedIndex].ID, classEquipType[list_Class_EquipType.SelectedIndex].Name));
            }
            else if (tree_Class_Default.IsSelected == true)
            {
                // Add the selected element to the class default list
                cfg.ClassDefault.EquipList.Add(new EquipType(classEquipType[list_Class_EquipType.SelectedIndex].ID, classEquipType[list_Class_EquipType.SelectedIndex].Name));
            }
        }

        private void btn_Class_RemoveEquipList_Click(object sender, RoutedEventArgs e)
        {
            // Check if an element of the equip list box is selected
            if (updating || (tree_Class_IsFamily() == false && tree_Class_IsIndividual() == false && tree_Class_Default.IsSelected == false) || check_Class_CustomEquip.IsChecked == false || list_Class_EquipList.SelectedIndex < 0) return;

            // Check for the selected class branch
            if (tree_Class_IsFamily())
            {
                // Remove the selected element from the class family list
                cfg.ClassFamily[tree_Class_ID() - 1].EquipList.RemoveAt(list_Class_EquipList.SelectedIndex);
            }
            else if (tree_Class_IsIndividual())
            {
                // Remove the selected element from the class individual list
                cfg.ClassID[tree_Class_ID() - 1].EquipList.RemoveAt(list_Class_EquipList.SelectedIndex);
            }
            else if (tree_Class_Default.IsSelected == true)
            {
                // Remove the selected element from the class default list
                cfg.ClassDefault.EquipList.RemoveAt(list_Class_EquipList.SelectedIndex);
            }

            // Remove the selected element from the ui list
            classEquipList.RemoveAt(list_Class_EquipList.SelectedIndex);
        }

        #endregion

        #region Move equipment in list

        private void btn_Class_UpEquipList_Click(object sender, RoutedEventArgs e)
        {
            // Check if the equipment type list box can be updated
            if (updating || list_Class_EquipList.SelectedIndex < 0) return;

            // Check if the selected index is before the first element
            if (list_Class_EquipList.SelectedIndex > 0)
            {
                // Move the selected equipment down
                classEquipList.Move(list_Class_EquipList.SelectedIndex, list_Class_EquipList.SelectedIndex - 1);
            }
            else
            {
                // Return the selected index to the bottom
                classEquipList.Move(list_Class_EquipList.SelectedIndex, classEquipList.Count - 1);
            }

            // Check for the selected class branch
            if (tree_Class_IsFamily())
            {
                // Store the new class family data
                storeClassFamily();
            }
            else if (tree_Class_IsIndividual())
            {
                // Store the new class individual data
                storeClassID();
            }
            else if (tree_Class_Default.IsSelected == true)
            {
                // Store the new class default data
                storeClassDefault();
            }
        }

        private void btn_Class_DownEquipList_Click(object sender, RoutedEventArgs e)
        {
            // Check if the equipment type list box can be updated
            if (updating || list_Class_EquipList.SelectedIndex < 0) { return; }

            // Check if the selected index is before the last element
            if (list_Class_EquipList.SelectedIndex < classEquipList.Count - 1)
            {
                // Move the selected equipment down
                classEquipList.Move(list_Class_EquipList.SelectedIndex, list_Class_EquipList.SelectedIndex + 1);
            }
            else
            {
                // Return the selected index to the top
                classEquipList.Move(list_Class_EquipList.SelectedIndex, 0);
            }

            // Check for the selected class branch
            if (tree_Class_IsFamily())
            {
                // Store the new class family data
                storeClassFamily();
            }
            else if (tree_Class_IsIndividual())
            {
                // Store the new class individual data
                storeClassID();
            }
            else if (tree_Class_Default.IsSelected == true)
            {
                // Store the new class default data
                storeClassDefault();
            }
        }

        #endregion

        #region Reset Name

        private void btn_Class_ResetNameEquipList_Click(object sender, RoutedEventArgs e)
        {
            // Check if the equipment type list box can be updated
            if (updating || list_Class_EquipList.SelectedIndex < 0) return;

            // Set the updating flag
            updating = true;

            // Store the current index
            int current_index = list_Class_EquipList.SelectedIndex;

            // Check for the selected class family branch is selected
            if (tree_Class_IsFamily())
            {
                // Get the default name for the selected equipment
                cfg.ClassFamily[tree_Class_ID() - 1].EquipList[current_index].Name = cfg.ClassFamily[tree_Class_ID() - 1].EquipType[cfg.ClassFamily[tree_Class_ID() - 1].EquipList[current_index].ID].Name;

                // Update the euipment type list
                classEquipList.Clear();
                if (cfg.ClassFamily[tree_Class_ID() - 1].EquipList.Count > 0)
                {
                    foreach (EquipType equip in cfg.ClassFamily[tree_Class_ID() - 1].EquipList)
                    {
                        classEquipList.Add(new EquipType(equip.ID, equip.Name));
                    }
                }
            }
            // Check for the selected class individual branch is selected
            else if (tree_Class_IsIndividual())
            {
                // Get the default name for the selected equipment
                cfg.ClassID[tree_Class_ID() - 1].EquipList[current_index].Name = cfg.ClassID[tree_Class_ID() - 1].EquipType[cfg.ClassID[tree_Class_ID() - 1].EquipList[current_index].ID].Name;

                // Update the euipment type list
                classEquipList.Clear();
                if (cfg.ClassID[tree_Class_ID() - 1].EquipList.Count > 0)
                {
                    foreach (EquipType equip in cfg.ClassID[tree_Class_ID() - 1].EquipList)
                    {
                        classEquipList.Add(new EquipType(equip.ID, equip.Name));
                    }
                }
            }
            // Check for the selected class default branch is selected
            else if (tree_Class_Default.IsSelected == true)
            {
                // Get the default name for the selected equipment
                cfg.ClassDefault.EquipList[current_index].Name = cfg.ClassDefault.EquipType[cfg.ClassDefault.EquipList[current_index].ID].Name;

                // Update the euipment type list
                classEquipList.Clear();
                if (cfg.ClassDefault.EquipList.Count > 0)
                {
                    foreach (EquipType equip in cfg.ClassDefault.EquipList)
                    {
                        classEquipList.Add(new EquipType(equip.ID, equip.Name));
                    }
                }
            }

            // Reset the list to the current index
            list_Class_EquipList.SelectedIndex = current_index;

            // Update the equip type name text box
            txt_Class_NameEquipList.Text = classEquipList[current_index].Name;

            // Set the current index to the last index for the next update
            lastIndexClassEquipList = list_Class_EquipList.SelectedIndex;

            // Remove the updating flag
            updating = false;
        }

        private void btn_Class_ResetAllNameEquipList_Click(object sender, RoutedEventArgs e)
        {
            // Check if the equipment type list box can be updated
            if (updating || list_Class_EquipList.SelectedIndex < 0) return;

            // Set the updating flag
            updating = true;

            // Store the current index
            int current_index = list_Class_EquipList.SelectedIndex;

            // Check for the selected class family branch is selected
            if (tree_Class_IsFamily())
            {
                // Get the default name for the all equipment
                if (cfg.ClassFamily[tree_Class_ID() - 1].EquipList.Count > 0)
                {
                    foreach (EquipType equip in cfg.ClassFamily[tree_Class_ID() - 1].EquipList)
                    {
                        equip.Name = cfg.ClassFamily[tree_Class_ID() - 1].EquipType[equip.ID].Name;
                    }
                }

                // Update the euipment type list
                classEquipList.Clear();
                if (cfg.ClassFamily[tree_Class_ID() - 1].EquipList.Count > 0)
                {
                    foreach (EquipType equip in cfg.ClassFamily[tree_Class_ID() - 1].EquipList)
                    {
                        classEquipList.Add(new EquipType(equip.ID, equip.Name));
                    }
                }
            }
            // Check for the selected class individual branch is selected
            else if (tree_Class_IsIndividual())
            {
                // Get the default name for the all equipment
                if (cfg.ClassID[tree_Class_ID() - 1].EquipList.Count > 0)
                {
                    foreach (EquipType equip in cfg.ClassID[tree_Class_ID() - 1].EquipList)
                    {
                        equip.Name = cfg.ClassID[tree_Class_ID() - 1].EquipType[equip.ID].Name;
                    }
                }

                // Update the euipment type list
                classEquipList.Clear();
                if (cfg.ClassID[tree_Class_ID() - 1].EquipList.Count > 0)
                {
                    foreach (EquipType equip in cfg.ClassID[tree_Class_ID() - 1].EquipList)
                    {
                        classEquipList.Add(new EquipType(equip.ID, equip.Name));
                    }
                }
            }
            // Check for the selected class default branch is selected
            else if (tree_Class_Default.IsSelected == true)
            {
                // Get the default name for the all equipment
                if (cfg.ClassDefault.EquipList.Count > 0)
                {
                    foreach (EquipType equip in cfg.ClassDefault.EquipList)
                    {
                        equip.Name = cfg.ClassDefault.EquipType[equip.ID].Name;
                    }
                }

                // Update the euipment type list
                classEquipList.Clear();
                if (cfg.ClassDefault.EquipList.Count > 0)
                {
                    foreach (EquipType equip in cfg.ClassDefault.EquipList)
                    {
                        classEquipList.Add(new EquipType(equip.ID, equip.Name));
                    }
                }
            }

            // Reset the list to the current index
            list_Class_EquipList.SelectedIndex = current_index;

            // Update the equip type name text box
            txt_Class_NameEquipList.Text = classEquipList[current_index].Name;

            // Set the current index to the last index for the next update
            lastIndexClassEquipList = list_Actor_EquipList.SelectedIndex;

            // Remove the updating flag
            updating = false;
        }

        #endregion

        #endregion

        #endregion

        #region Parameter Configuration

        private void check_Class_Parameter_Checked(object sender, RoutedEventArgs e)
        {
            // Check if you are in a valide time to update the class parameter
            if (updating || (tree_Class_IsFamily() == false && tree_Class_IsIndividual() == false && tree_Class_Default.IsSelected == false)) { return; }

            // Get the currently modified checkbox
            CheckBox checkBox = sender as CheckBox;

            // Store the data of the class family and reload it except for the modified checkbox
            if (tree_Class_IsFamily())
            {
                storeClassFamily(checkBox.Name);
                applyClassFamily();
            }

            // Store the data of the class individual and reload it except for the modified checkbox
            if (tree_Class_IsIndividual())
            {
                storeClassID(checkBox.Name);
                applyClassID();
            }

            // Store the data of the class default and reload it except for the modified checkbox
            if (tree_Class_Default.IsSelected)
            {
                storeClassDefault(checkBox.Name);
                applyClassDefault();
            }
        }

        private void check_Class_Parameter_Unchecked(object sender, RoutedEventArgs e)
        {
            // Check if you are in a valide time to update the class parameter
            if (updating || (tree_Class_IsFamily() == false && tree_Class_IsIndividual() == false && tree_Class_Default.IsSelected == false)) { return; }

            // Store the data of the class family and reload it
            if (tree_Class_IsFamily())
            {
                storeClassFamily();
                applyClassFamily();
            }

            // Store the data of the class individual and reload it
            if (tree_Class_IsIndividual())
            {
                storeClassID();
                applyClassID();
            }

            // Store the data of the class default and reload it
            if (tree_Class_Default.IsSelected)
            {
                storeClassDefault();
                applyClassDefault();
            }
        }

        #endregion

        #endregion

        #region Skill

        #region TreeView

        # region Get Selected

        private bool tree_Skill_IsFamily()
        {
            // Get the selected item data
            TreeViewItem tvi = (TreeViewItem)tree_Skill.SelectedItem;

            // Check if the selected item is a member of the family branch
            if (tree_Skill_Family.IsSelected == false && tree_Skill_Individual.IsSelected == false && tree_Skill_Default.IsSelected == false && tree_Skill_Family.IsAncestorOf(tvi))
            {
                return true;
            }

            return false;
        }

        private bool tree_Skill_IsIndividual()
        {
            // Get the selected item data
            TreeViewItem tvi = (TreeViewItem)tree_Skill.SelectedItem;

            // Check if the selected item is a member of the individual branch
            if (tree_Skill_Family.IsSelected == false && tree_Skill_Individual.IsSelected == false && tree_Skill_Default.IsSelected == false && tree_Skill_Individual.IsAncestorOf(tvi))
            {
                return true;
            }

            return false;
        }

        private int tree_Skill_ID()
        {
            // Set the variable use to convert the string to an integer
            int result;
            int id = -1;

            // Get the selected item data
            TreeViewItem tvi = (TreeViewItem)tree_Skill.SelectedItem;

            // Check if you the selected item is a member of the family branch or the individual branch
            if ((tree_Skill_IsFamily() || tree_Skill_IsIndividual()) && tree_Skill_Default.IsSelected == false)
            {
                // Try to convert the string to an integer
                if (Int32.TryParse(tvi.Tag.ToString(), out result))
                {
                    id = result;
                }
            }

            return id;
        }

        #endregion

        #region Update data

        private void tree_Skill_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            // Check if the the treeview can be updated
            if (updating) { return; }

            // Store the skill family data if the previous selected item was a member of the family branch
            if (lastIndexSkillFamily > 0)
            {
                storeSkillFamily(lastIndexSkillFamily);

                // Update the navigation tree
                int i = 0;
                foreach (TreeViewItem treeItem in tree_Skill_Family.Items)
                {
                    treeItem.Header = cfg.SkillFamily[i].Name;
                    i++;
                }
            }

            // Store the skill individual data if the previous selected item was a member of the individual branch
            if (lastIndexSkillID > 0)
            {
                storeSkillID(lastIndexSkillID);
            }

            // Store the skill default data if the previous selected item was the default member
            if (lastIndexSkillDefault)
            {
                storeSkillDefault();
            }

            // Update the previous index
            lastIndexSkillFamily = 0;
            lastIndexSkillID = 0;
            lastIndexSkillDefault = false;

            // Check if the selected item is a member of the family branch
            if (tree_Skill_IsFamily())
            {
                // Update the previous index
                lastIndexSkillFamily = tree_Skill_ID();

                // Apply the new skill family data
                applySkillFamily();
            }
            else if (tree_Skill_IsIndividual())
            {
                // Update the previous index
                lastIndexSkillID = tree_Skill_ID();

                // Apply the new skill individual data
                applySkillID();
            }
            else if (tree_Skill_Default.IsSelected)
            {
                // Update the previous index
                lastIndexSkillDefault = true;

                // Apply the new skikll default data
                applySkillDefault();
            }
            else
            {
                // Empty the skill data when their is no valid item selected
                applyEmptySkill();
            }
        }

        #endregion

        #region Manage data

        private void btn_Skill_Add_Click(object sender, RoutedEventArgs e)
        {
            // Get the new index
            int id = tree_Skill_Family.Items.Count + 1;

            // Create the new item in the tree and the configuration
            cfg.SkillFamily.Add(new DataPackSkill(id));
            tree_Skill_Family.Items.Add(new TreeViewItem() { Header = cfg.SkillFamily[id - 1].Name, Tag = id.ToString() });
        }

        private void btn_Skill_Remove_Click(object sender, RoutedEventArgs e)
        {
            // Set the variable for the new item index
            int id;

            // Check if the selected tree item is a member of family 
            if (tree_Skill_IsFamily() && tree_Skill_ID() > 0 && cfg.SkillFamily.Count > 0)
            {
                // Set the updating flag
                updating = true;

                // Get the selected item data
                TreeViewItem tvi = (TreeViewItem)tree_Skill.SelectedItem;

                // Return the skill in the family to the available list
                if (cfg.SkillFamily[tree_Skill_ID() - 1].SkillFamily.Count > 0)
                {
                    foreach (Skill skill in cfg.SkillFamily[tree_Skill_ID() - 1].SkillFamily)
                    {
                        cfg.SkillAvailable.Add(new Skill(skill.ID, skill.Name));
                    }
                }

                // Remove the selected item from the tree and the configuration
                cfg.SkillFamily.RemoveAt(tree_Skill_ID() - 1);
                tree_Skill_Family.Items.Remove(tvi);

                // Renumber the id of the tree items
                id = 1;
                foreach (TreeViewItem tv in tree_Skill_Family.Items)
                {
                    tv.Tag = id.ToString();
                    id++;
                }

                // Renumber the id of the conguguration
                id = 1;
                foreach (DataPackSkill skillFamily in cfg.SkillFamily)
                {
                    skillFamily.ID = id;
                    id++;
                }

                // Empty the skill configuration screen
                tree_Skill_Family.IsSelected = true;
                applyEmptySkill();

                // Remove the updating Flag
                updating = false;
            }
        }

        private void btn_Skill_Reset_Click(object sender, RoutedEventArgs e)
        {
            if (tree_Skill_IsFamily())
            {
                cfg.SkillFamily[tree_Skill_ID() - 1].Reset();
                applySkillFamily();
            }
            else if (tree_Skill_IsIndividual())
            {
                cfg.SkillID[tree_Skill_ID() - 1].Reset();
                applySkillID();
            }
            else if (tree_Skill_Default.IsSelected == true)
            {
                cfg.SkillDefault.Reset();
                applySkillDefault();
            }
        }

        #endregion

        #region Clipboard

        private void tree_Skill_Copy_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Check if the selected items is valid
            if (tree_Skill_IsFamily() || tree_Skill_IsIndividual() || tree_Skill_Default.IsSelected == true)
            {
                // Empty the previous clipboard
                cachedActor = null;
                cachedClass = null;
                cachedSkill = null;
                cachedPassiveSkill = null;
                cachedWeapon = null;
                cachedArmor = null;
                cachedEnemy = null;
                cachedState = null;

                // Copy the data from the family
                if (tree_Skill_IsFamily())
                {
                    storeSkillFamily();
                    cachedSkill = cfg.SkillFamily[tree_Skill_ID() - 1];
                }

                // Copy the data from the individual
                else if (tree_Skill_IsIndividual())
                {
                    storeSkillID();
                    cachedSkill = cfg.SkillID[tree_Skill_ID() - 1];
                }

                // Copy the data from the default
                else if (tree_Skill_Default.IsSelected == true)
                {
                    storeSkillDefault();
                    cachedSkill = cfg.SkillDefault;
                }
            }
        }

        private void tree_Skill_Paste_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Check if the selected items is valid
            if (tree_Skill_IsFamily() || tree_Skill_IsIndividual() || tree_Skill_Default.IsSelected == true)
            {
                // Check if an item was copy
                if (cachedSkill != null)
                {
                    // Paste the data from the family
                    if (tree_Skill_IsFamily())
                    {
                        cfg.SkillFamily[tree_Skill_ID() - 1].PasteFrom(cachedSkill);
                        applySkillFamily();
                    }

                    // Paste the data from the individual
                    else if (tree_Skill_IsIndividual())
                    {
                        cfg.SkillID[tree_Skill_ID() - 1].PasteFrom(cachedSkill);
                        applySkillID();
                    }

                    // Paste the data from the default
                    else if (tree_Skill_Default.IsSelected == true)
                    {
                        cfg.SkillDefault.PasteFrom(cachedSkill);
                        applySkillDefault();
                    }
                }
            }
        }

        #endregion

        #endregion

        #region Family Configuration

        private void btn_Skill_AddToFamily_Click(object sender, RoutedEventArgs e)
        {
            // Check if you are in a valide time to add an skill to the family
            if (updating || tree_Skill_IsFamily() == false || tree_Skill_IsIndividual() == true || tree_Skill_Default.IsSelected == true || list_Skill_Available.SelectedIndex < 0) { return; }

            // Get the index of the selected skill
            int index = list_Skill_Available.SelectedIndex;

            // Add the skill to the skill in family list
            skillInFamily.Add(new Skill(skillAvailable[index].ID, skillAvailable[index].Name));
            skillInFamily.Sort((x, y) => x.ID.CompareTo(y.ID));

            // Remove the skill from the available list
            skillAvailable.RemoveAt(index);

            // Update the available list index
            if (index > 0) { list_Skill_Available.SelectedIndex = index - 1; }
            else { list_Skill_Available.SelectedIndex = 0; }

            // Store the modification
            storeSkillFamily();
        }

        private void btn_Skill_RemoveFromFamily_Click(object sender, RoutedEventArgs e)
        {
            // Check if you are in a valide time to remove from skill to the family
            if (updating || tree_Skill_IsFamily() == false || tree_Skill_IsIndividual() == true || tree_Skill_Default.IsSelected == true || list_Skill_InFamily.SelectedIndex < 0) { return; }

            // Get the index of the selected skill
            int index = list_Skill_InFamily.SelectedIndex;

            // Add the skill to the available list
            skillAvailable.Add(new Skill(skillInFamily[index].ID, skillInFamily[index].Name));
            skillAvailable.Sort((x, y) => x.ID.CompareTo(y.ID));

            // Remove the skill from the skill in family list
            skillInFamily.RemoveAt(index);

            // Update the available list index
            if (index > 0) { list_Skill_InFamily.SelectedIndex = index - 1; }
            else { list_Skill_InFamily.SelectedIndex = 0; }

            // Store the modification
            storeSkillFamily();
        }

        #endregion

        #region Parameter Configuration

        private void check_Skill_Parameter_Checked(object sender, RoutedEventArgs e)
        {
            // Check if you are in a valide time to update the skill parameter
            if (updating || (tree_Skill_IsFamily() == false && tree_Skill_IsIndividual() == false && tree_Skill_Default.IsSelected == false)) { return; }

            // Get the currently modified checkbox
            CheckBox checkBox = sender as CheckBox;

            // Store the data of the skill family and reload it except for the modified checkbox
            if (tree_Skill_IsFamily())
            {
                storeSkillFamily(checkBox.Name);
                applySkillFamily();
            }

            // Store the data of the skill individual and reload it except for the modified checkbox
            if (tree_Skill_IsIndividual())
            {
                storeSkillID(checkBox.Name);
                applySkillID();
            }

            // Store the data of the skill default and reload it except for the modified checkbox
            if (tree_Skill_Default.IsSelected)
            {
                storeSkillDefault(checkBox.Name);
                applySkillDefault();
            }
        }

        private void check_Skill_Parameter_Unchecked(object sender, RoutedEventArgs e)
        {
            // Check if you are in a valide time to update the skill parameter
            if (updating || (tree_Skill_IsFamily() == false && tree_Skill_IsIndividual() == false && tree_Skill_Default.IsSelected == false)) { return; }

            // Store the data of the skill family and reload it
            if (tree_Skill_IsFamily())
            {
                storeSkillFamily();
                applySkillFamily();
            }

            // Store the data of the skill individual and reload it
            if (tree_Skill_IsIndividual())
            {
                storeSkillID();
                applySkillID();
            }

            // Store the data of the skill default and reload it
            if (tree_Skill_Default.IsSelected)
            {
                storeSkillDefault();
                applySkillDefault();
            }
        }

        #endregion

        #endregion

        #region Passive Skill

        #region TreeView

        # region Get Selected

        private bool tree_PassiveSkill_IsFamily()
        {
            // Get the selected item data
            TreeViewItem tvi = (TreeViewItem)tree_PassiveSkill.SelectedItem;

            // Check if the selected item is a member of the family branch
            if (tree_PassiveSkill_Family.IsSelected == false && tree_PassiveSkill_Individual.IsSelected == false && tree_PassiveSkill_Default.IsSelected == false && tree_PassiveSkill_Family.IsAncestorOf(tvi))
            {
                return true;
            }

            return false;
        }

        private bool tree_PassiveSkill_IsIndividual()
        {
            // Get the selected item data
            TreeViewItem tvi = (TreeViewItem)tree_PassiveSkill.SelectedItem;

            // Check if the selected item is a member of the individual branch
            if (tree_PassiveSkill_Family.IsSelected == false && tree_PassiveSkill_Individual.IsSelected == false && tree_PassiveSkill_Default.IsSelected == false && tree_PassiveSkill_Individual.IsAncestorOf(tvi))
            {
                return true;
            }

            return false;
        }

        private int tree_PassiveSkill_ID()
        {
            // Set the variable use to convert the string to an integer
            int result;
            int id = -1;

            // Get the selected item data
            TreeViewItem tvi = (TreeViewItem)tree_PassiveSkill.SelectedItem;

            // Check if you the selected item is a member of the family branch or the individual branch
            if ((tree_PassiveSkill_IsFamily() || tree_PassiveSkill_IsIndividual()) && tree_PassiveSkill_Default.IsSelected == false)
            {
                // Try to convert the string to an integer
                if (Int32.TryParse(tvi.Tag.ToString(), out result))
                {
                    id = result;
                }
            }

            return id;
        }

        #endregion

        #region Update data

        private void tree_PassiveSkill_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            // Check if the the treeview can be updated
            if (updating) { return; }

            // Store the passive skill family data if the previous selected item was a member of the family branch
            if (lastIndexPassiveSkillFamily > 0)
            {
                storePassiveSkillFamily(lastIndexPassiveSkillFamily);

                // Update the navigation tree
                int i = 0;
                foreach (TreeViewItem treeItem in tree_PassiveSkill_Family.Items)
                {
                    treeItem.Header = cfg.PassiveSkillFamily[i].Name;
                    i++;
                }
            }

            // Store the passive skill individual data if the previous selected item was a member of the individual branch
            if (lastIndexPassiveSkillID > 0)
            {
                storePassiveSkillID(lastIndexPassiveSkillID);
            }

            // Store the passive skill default data if the previous selected item was the default member
            if (lastIndexPassiveSkillDefault)
            {
                storePassiveSkillDefault();
            }

            // Update the previous index
            lastIndexPassiveSkillFamily = 0;
            lastIndexPassiveSkillID = 0;
            lastIndexPassiveSkillDefault = false;

            // Check if the selected item is a member of the family branch
            if (tree_PassiveSkill_IsFamily())
            {
                // Update the previous index
                lastIndexPassiveSkillFamily = tree_PassiveSkill_ID();

                // Apply the new passive skill family data
                applyPassiveSkillFamily();
            }
            else if (tree_PassiveSkill_IsIndividual())
            {
                // Update the previous index
                lastIndexPassiveSkillID = tree_PassiveSkill_ID();

                // Apply the new passive skill individual data
                applyPassiveSkillID();
            }
            else if (tree_PassiveSkill_Default.IsSelected)
            {
                // Update the previous index
                lastIndexPassiveSkillDefault = true;

                // Apply the new skikll default data
                applyPassiveSkillDefault();
            }
            else
            {
                // Empty the passive skill data when their is no valid item selected
                applyEmptyPassiveSkill();
            }
        }

        #endregion

        #region Manage data

        private void btn_PassiveSkill_Add_Click(object sender, RoutedEventArgs e)
        {
            // Get the new index
            int id = tree_PassiveSkill_Family.Items.Count + 1;

            // Create the new item in the tree and the configuration
            cfg.PassiveSkillFamily.Add(new DataPackPassiveSkill(id));
            tree_PassiveSkill_Family.Items.Add(new TreeViewItem() { Header = cfg.PassiveSkillFamily[id - 1].Name, Tag = id.ToString() });
        }

        private void btn_PassiveSkill_Remove_Click(object sender, RoutedEventArgs e)
        {
            // Set the variable for the new item index
            int id;

            // Check if the selected tree item is a member of family 
            if (tree_PassiveSkill_IsFamily() && tree_PassiveSkill_ID() > 0 && cfg.PassiveSkillFamily.Count > 0)
            {
                // Set the updating flag
                updating = true;

                // Get the selected item data
                TreeViewItem tvi = (TreeViewItem)tree_PassiveSkill.SelectedItem;

                // Return the passive skill in the family to the available list
                if (cfg.PassiveSkillFamily[tree_PassiveSkill_ID() - 1].PassiveSkillFamily.Count > 0)
                {
                    foreach (Skill passiveSkill in cfg.PassiveSkillFamily[tree_PassiveSkill_ID() - 1].PassiveSkillFamily)
                    {
                        cfg.PassiveSkillAvailable.Add(new Skill(passiveSkill.ID, passiveSkill.Name));
                    }
                }

                // Remove the selected item from the tree and the configuration
                cfg.PassiveSkillFamily.RemoveAt(tree_PassiveSkill_ID() - 1);
                tree_PassiveSkill_Family.Items.Remove(tvi);

                // Renumber the id of the tree items
                id = 1;
                foreach (TreeViewItem tv in tree_PassiveSkill_Family.Items)
                {
                    tv.Tag = id.ToString();
                    id++;
                }

                // Renumber the id of the conguguration
                id = 1;
                foreach (DataPackPassiveSkill passiveSkillFamily in cfg.PassiveSkillFamily)
                {
                    passiveSkillFamily.ID = id;
                    id++;
                }

                // Empty the passive skill configuration screen
                tree_PassiveSkill_Family.IsSelected = true;
                applyEmptyPassiveSkill();

                // Remove the updating Flag
                updating = false;
            }
        }

        private void btn_PassiveSkill_Reset_Click(object sender, RoutedEventArgs e)
        {
            if (tree_PassiveSkill_IsFamily())
            {
                cfg.ActorFamily[tree_PassiveSkill_ID() - 1].Reset();
                applyActorFamily();
            }
            else if (tree_PassiveSkill_IsIndividual())
            {
                cfg.ActorID[tree_PassiveSkill_ID() - 1].Reset();
                applyActorID();
            }
            else if (tree_PassiveSkill_Default.IsSelected == true)
            {
                cfg.ActorDefault.Reset();
                applyActorDefault();
            }
        }

        #endregion

        #region Clipboard

        private void tree_PassiveSkill_Copy_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Check if the selected items is valid
            if (tree_PassiveSkill_IsFamily() || tree_PassiveSkill_IsIndividual() || tree_PassiveSkill_Default.IsSelected == true)
            {
                // Empty the previous clipboard
                cachedActor = null;
                cachedClass = null;
                cachedSkill = null;
                cachedPassiveSkill = null;
                cachedWeapon = null;
                cachedArmor = null;
                cachedEnemy = null;
                cachedState = null;

                // Copy the data from the family
                if (tree_PassiveSkill_IsFamily())
                {
                    storePassiveSkillFamily();
                    cachedPassiveSkill = cfg.PassiveSkillFamily[tree_PassiveSkill_ID() - 1];
                }

                // Copy the data from the individual
                else if (tree_PassiveSkill_IsIndividual())
                {
                    storePassiveSkillID();
                    cachedPassiveSkill = cfg.PassiveSkillID[tree_PassiveSkill_ID() - 1];
                }

                // Copy the data from the default
                else if (tree_PassiveSkill_Default.IsSelected == true)
                {
                    storePassiveSkillDefault();
                    cachedPassiveSkill = cfg.PassiveSkillDefault;
                }
            }
        }

        private void tree_PassiveSkill_Paste_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Check if the selected items is valid
            if (tree_PassiveSkill_IsFamily() || tree_PassiveSkill_IsIndividual() || tree_PassiveSkill_Default.IsSelected == true)
            {
                // Check if an item was copy
                if (cachedPassiveSkill != null)
                {
                    // Paste the data from the family
                    if (tree_PassiveSkill_IsFamily())
                    {
                        cfg.PassiveSkillFamily[tree_PassiveSkill_ID() - 1].PasteFrom(cachedPassiveSkill);
                        applyPassiveSkillFamily();
                    }

                    // Paste the data from the individual
                    else if (tree_PassiveSkill_IsIndividual())
                    {
                        cfg.PassiveSkillID[tree_PassiveSkill_ID() - 1].PasteFrom(cachedPassiveSkill);
                        applyPassiveSkillID();
                    }

                    // Paste the data from the default
                    else if (tree_PassiveSkill_Default.IsSelected == true)
                    {
                        cfg.PassiveSkillDefault.PasteFrom(cachedPassiveSkill);
                        applyPassiveSkillDefault();
                    }
                }
            }
        }

        #endregion

        #endregion

        #region Family Configuration

        private void btn_PassiveSkill_AddToFamily_Click(object sender, RoutedEventArgs e)
        {
            // Check if you are in a valide time to add an passive skill to the family
            if (updating || tree_PassiveSkill_IsFamily() == false || tree_PassiveSkill_IsIndividual() == true || tree_PassiveSkill_Default.IsSelected == true || list_PassiveSkill_Available.SelectedIndex < 0) { return; }

            // Get the index of the selected passive skill
            int index = list_PassiveSkill_Available.SelectedIndex;

            // Add the passive skill to the passive skill in family list
            passiveSkillInFamily.Add(new Skill(passiveSkillAvailable[index].ID, passiveSkillAvailable[index].Name));
            passiveSkillInFamily.Sort((x, y) => x.ID.CompareTo(y.ID));

            // Remove the passive skill from the available list
            passiveSkillAvailable.RemoveAt(index);

            // Update the available list index
            if (index > 0) { list_PassiveSkill_Available.SelectedIndex = index - 1; }
            else { list_PassiveSkill_Available.SelectedIndex = 0; }

            // Store the modification
            storePassiveSkillFamily();
        }

        private void btn_PassiveSkill_RemoveFromFamily_Click(object sender, RoutedEventArgs e)
        {
            // Check if you are in a valide time to remove from passive skill to the family
            if (updating || tree_PassiveSkill_IsFamily() == false || tree_PassiveSkill_IsIndividual() == true || tree_PassiveSkill_Default.IsSelected == true || list_PassiveSkill_InFamily.SelectedIndex < 0) { return; }

            // Get the index of the selected passive skill
            int index = list_PassiveSkill_InFamily.SelectedIndex;

            // Add the passive skill to the available list
            passiveSkillAvailable.Add(new Skill(passiveSkillInFamily[index].ID, passiveSkillInFamily[index].Name));
            passiveSkillAvailable.Sort((x, y) => x.ID.CompareTo(y.ID));

            // Remove the passive skill from the passive skill in family list
            skillInFamily.RemoveAt(index);

            // Update the available list index
            if (index > 0) { list_PassiveSkill_InFamily.SelectedIndex = index - 1; }
            else { list_PassiveSkill_InFamily.SelectedIndex = 0; }

            // Store the modification
            storePassiveSkillFamily();
        }

        #endregion

        #region Parameter Configuration

        private void check_PassiveSkill_Parameter_Checked(object sender, RoutedEventArgs e)
        {
            // Check if you are in a valide time to update the passive skill parameter
            if (updating || (tree_PassiveSkill_IsFamily() == false && tree_PassiveSkill_IsIndividual() == false && tree_PassiveSkill_Default.IsSelected == false)) { return; }

            // Get the currently modified checkbox
            CheckBox checkBox = sender as CheckBox;

            // Store the data of the passive skill family and reload it except for the modified checkbox
            if (tree_PassiveSkill_IsFamily())
            {
                storePassiveSkillFamily(checkBox.Name);
                applyPassiveSkillFamily();
            }

            // Store the data of the passive skill individual and reload it except for the modified checkbox
            if (tree_PassiveSkill_IsIndividual())
            {
                storePassiveSkillID(checkBox.Name);
                applyPassiveSkillID();
            }

            // Store the data of the passive skill default and reload it except for the modified checkbox
            if (tree_PassiveSkill_Default.IsSelected)
            {
                storePassiveSkillDefault(checkBox.Name);
                applyPassiveSkillDefault();
            }
        }

        private void check_PassiveSkill_Parameter_Unchecked(object sender, RoutedEventArgs e)
        {
            // Check if you are in a valide time to update the passive skill parameter
            if (updating || (tree_PassiveSkill_IsFamily() == false && tree_PassiveSkill_IsIndividual() == false && tree_PassiveSkill_Default.IsSelected == false)) { return; }

            // Store the data of the passive skill family and reload it
            if (tree_PassiveSkill_IsFamily())
            {
                storePassiveSkillFamily();
                applyPassiveSkillFamily();
            }

            // Store the data of the passive skill individual and reload it
            if (tree_PassiveSkill_IsIndividual())
            {
                storePassiveSkillID();
                applyPassiveSkillID();
            }

            // Store the data of the passive skill default and reload it
            if (tree_PassiveSkill_Default.IsSelected)
            {
                storePassiveSkillDefault();
                applyPassiveSkillDefault();
            }
        }

        #endregion

        #endregion

        #region Weapon

        #region TreeView

        # region Get Selected

        private bool tree_Weapon_IsFamily()
        {
            // Get the selected item data
            TreeViewItem tvi = (TreeViewItem)tree_Weapon.SelectedItem;

            // Check if the selected item is a member of the family branch
            if (tree_Weapon_Family.IsSelected == false && tree_Weapon_Individual.IsSelected == false && tree_Weapon_Default.IsSelected == false && tree_Weapon_Family.IsAncestorOf(tvi))
            {
                return true;
            }

            return false;
        }

        private bool tree_Weapon_IsIndividual()
        {
            // Get the selected item data
            TreeViewItem tvi = (TreeViewItem)tree_Weapon.SelectedItem;

            // Check if the selected item is a member of the individual branch
            if (tree_Weapon_Family.IsSelected == false && tree_Weapon_Individual.IsSelected == false && tree_Weapon_Default.IsSelected == false && tree_Weapon_Individual.IsAncestorOf(tvi))
            {
                return true;
            }

            return false;
        }

        private int tree_Weapon_ID()
        {
            // Set the variable use to convert the string to an integer
            int result;
            int id = -1;

            // Get the selected item data
            TreeViewItem tvi = (TreeViewItem)tree_Weapon.SelectedItem;

            // Check if you the selected item is a member of the family branch or the individual branch
            if ((tree_Weapon_IsFamily() || tree_Weapon_IsIndividual()) && tree_Weapon_Default.IsSelected == false)
            {
                // Try to convert the string to an integer
                if (Int32.TryParse(tvi.Tag.ToString(), out result))
                {
                    id = result;
                }
            }

            return id;
        }

        #endregion

        #region Update data

        private void tree_Weapon_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            // Check if the the treeview can be updated
            if (updating) { return; }

            // Store the passive skill family data if the previous selected item was a member of the family branch
            if (lastIndexWeaponFamily > 0)
            {
                storeWeaponFamily(lastIndexWeaponFamily);

                // Update the navigation tree
                int i = 0;
                foreach (TreeViewItem treeItem in tree_Weapon_Family.Items)
                {
                    treeItem.Header = cfg.WeaponFamily[i].Name;
                    i++;
                }
            }

            // Store the passive skill individual data if the previous selected item was a member of the individual branch
            if (lastIndexWeaponID > 0)
            {
                storeWeaponID(lastIndexWeaponID);
            }

            // Store the passive skill default data if the previous selected item was the default member
            if (lastIndexWeaponDefault)
            {
                storeWeaponDefault();
            }

            // Update the previous index
            lastIndexWeaponFamily = 0;
            lastIndexWeaponID = 0;
            lastIndexWeaponDefault = false;

            // Check if the selected item is a member of the family branch
            if (tree_Weapon_IsFamily())
            {
                // Update the previous index
                lastIndexWeaponFamily = tree_Weapon_ID();

                // Apply the new passive skill family data
                applyWeaponFamily();
            }
            else if (tree_Weapon_IsIndividual())
            {
                // Update the previous index
                lastIndexWeaponID = tree_Weapon_ID();

                // Apply the new passive skill individual data
                applyWeaponID();
            }
            else if (tree_Weapon_Default.IsSelected)
            {
                // Update the previous index
                lastIndexWeaponDefault = true;

                // Apply the new skikll default data
                applyWeaponDefault();
            }
            else
            {
                // Empty the passive skill data when their is no valid item selected
                applyEmptyWeapon();
            }
        }

        #endregion

        #region Manage data

        private void btn_Weapon_Add_Click(object sender, RoutedEventArgs e)
        {
            // Get the new index
            int id = tree_Weapon_Family.Items.Count + 1;

            // Create the new item in the tree and the configuration
            cfg.WeaponFamily.Add(new DataPackEquipment(id));
            tree_Weapon_Family.Items.Add(new TreeViewItem() { Header = cfg.WeaponFamily[id - 1].Name, Tag = id.ToString() });
        }

        private void btn_Weapon_Remove_Click(object sender, RoutedEventArgs e)
        {
            // Set the variable for the new item index
            int id;

            // Check if the selected tree item is a member of family 
            if (tree_Weapon_IsFamily() && tree_Weapon_ID() > 0 && cfg.WeaponFamily.Count > 0)
            {
                // Set the updating flag
                updating = true;

                // Get the selected item data
                TreeViewItem tvi = (TreeViewItem)tree_Weapon.SelectedItem;

                // Return the passive skill in the family to the available list
                if (cfg.WeaponFamily[tree_Weapon_ID() - 1].WeaponFamily.Count > 0)
                {
                    foreach (Weapon weapon in cfg.WeaponFamily[tree_Weapon_ID() - 1].WeaponFamily)
                    {
                        cfg.WeaponAvailable.Add(new Weapon(weapon.ID, weapon.Name));
                    }
                }

                // Remove the selected item from the tree and the configuration
                cfg.WeaponFamily.RemoveAt(tree_Weapon_ID() - 1);
                tree_Weapon_Family.Items.Remove(tvi);

                // Renumber the id of the tree items
                id = 1;
                foreach (TreeViewItem tv in tree_Weapon_Family.Items)
                {
                    tv.Tag = id.ToString();
                    id++;
                }

                // Renumber the id of the conguguration
                id = 1;
                foreach (DataPackEquipment weaponFamily in cfg.WeaponFamily)
                {
                    weaponFamily.ID = id;
                    id++;
                }

                // Empty the passive skill configuration screen
                tree_Weapon_Family.IsSelected = true;
                applyEmptyWeapon();

                // Remove the updating Flag
                updating = false;
            }
        }

        private void btn_Weapon_Reset_Click(object sender, RoutedEventArgs e)
        {
            if (tree_Weapon_IsFamily())
            {
                cfg.ActorFamily[tree_Weapon_ID() - 1].Reset();
                applyActorFamily();
            }
            else if (tree_Weapon_IsIndividual())
            {
                cfg.ActorID[tree_Weapon_ID() - 1].Reset();
                applyActorID();
            }
            else if (tree_Weapon_Default.IsSelected == true)
            {
                cfg.ActorDefault.Reset();
                applyActorDefault();
            }
        }

        #endregion

        #region Clipboard

        private void tree_Weapon_Copy_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Check if the selected items is valid
            if (tree_Weapon_IsFamily() || tree_Weapon_IsIndividual() || tree_Weapon_Default.IsSelected == true)
            {
                // Empty the previous clipboard
                cachedActor = null;
                cachedClass = null;
                cachedSkill = null;
                cachedPassiveSkill = null;
                cachedWeapon = null;
                cachedArmor = null;
                cachedEnemy = null;
                cachedState = null;

                // Copy the data from the family
                if (tree_Weapon_IsFamily())
                {
                    storeWeaponFamily();
                    cachedWeapon = cfg.WeaponFamily[tree_Weapon_ID() - 1];
                }

                // Copy the data from the individual
                else if (tree_Weapon_IsIndividual())
                {
                    storeWeaponID();
                    cachedWeapon = cfg.WeaponID[tree_Weapon_ID() - 1];
                }

                // Copy the data from the default
                else if (tree_Weapon_Default.IsSelected == true)
                {
                    storeWeaponDefault();
                    cachedWeapon = cfg.WeaponDefault;
                }
            }
        }

        private void tree_Weapon_Paste_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Check if the selected items is valid
            if (tree_Weapon_IsFamily() || tree_Weapon_IsIndividual() || tree_Weapon_Default.IsSelected == true)
            {
                // Check if an item was copy
                if (cachedWeapon != null)
                {
                    // Paste the data from the family
                    if (tree_Weapon_IsFamily())
                    {
                        cfg.WeaponFamily[tree_Weapon_ID() - 1].PasteFrom(cachedWeapon);
                        applyWeaponFamily();
                    }

                    // Paste the data from the individual
                    else if (tree_Weapon_IsIndividual())
                    {
                        cfg.WeaponID[tree_Weapon_ID() - 1].PasteFrom(cachedWeapon);
                        applyWeaponID();
                    }

                    // Paste the data from the default
                    else if (tree_Weapon_Default.IsSelected == true)
                    {
                        cfg.WeaponDefault.PasteFrom(cachedWeapon);
                        applyWeaponDefault();
                    }
                }
            }
        }

        #endregion

        #endregion

        #region Family Configuration

        private void btn_Weapon_AddToFamily_Click(object sender, RoutedEventArgs e)
        {
            // Check if you are in a valide time to add an passive skill to the family
            if (updating || tree_Weapon_IsFamily() == false || tree_Weapon_IsIndividual() == true || tree_Weapon_Default.IsSelected == true || list_Weapon_Available.SelectedIndex < 0) { return; }

            // Get the index of the selected passive skill
            int index = list_Weapon_Available.SelectedIndex;

            // Add the passive skill to the passive skill in family list
            weaponInFamily.Add(new Weapon(weaponAvailable[index].ID, weaponAvailable[index].Name));
            weaponInFamily.Sort((x, y) => x.ID.CompareTo(y.ID));

            // Remove the passive skill from the available list
            weaponAvailable.RemoveAt(index);

            // Update the available list index
            if (index > 0) { list_Weapon_Available.SelectedIndex = index - 1; }
            else { list_Weapon_Available.SelectedIndex = 0; }

            // Store the modification
            storeWeaponFamily();
        }

        private void btn_Weapon_RemoveFromFamily_Click(object sender, RoutedEventArgs e)
        {
            // Check if you are in a valide time to remove from passive skill to the family
            if (updating || tree_Weapon_IsFamily() == false || tree_Weapon_IsIndividual() == true || tree_Weapon_Default.IsSelected == true || list_Weapon_InFamily.SelectedIndex < 0) { return; }

            // Get the index of the selected passive skill
            int index = list_Weapon_InFamily.SelectedIndex;

            // Add the passive skill to the available list
            weaponAvailable.Add(new Weapon(weaponInFamily[index].ID, weaponInFamily[index].Name));
            weaponAvailable.Sort((x, y) => x.ID.CompareTo(y.ID));

            // Remove the passive skill from the passive skill in family list
            skillInFamily.RemoveAt(index);

            // Update the available list index
            if (index > 0) { list_Weapon_InFamily.SelectedIndex = index - 1; }
            else { list_Weapon_InFamily.SelectedIndex = 0; }

            // Store the modification
            storeWeaponFamily();
        }

        #endregion

        #region Parameter Configuration

        private void check_Weapon_Parameter_Checked(object sender, RoutedEventArgs e)
        {
            // Check if you are in a valide time to update the passive skill parameter
            if (updating || (tree_Weapon_IsFamily() == false && tree_Weapon_IsIndividual() == false && tree_Weapon_Default.IsSelected == false)) { return; }

            // Get the currently modified checkbox
            CheckBox checkBox = sender as CheckBox;

            // Store the data of the passive skill family and reload it except for the modified checkbox
            if (tree_Weapon_IsFamily())
            {
                storeWeaponFamily(checkBox.Name);
                applyWeaponFamily();
            }

            // Store the data of the passive skill individual and reload it except for the modified checkbox
            if (tree_Weapon_IsIndividual())
            {
                storeWeaponID(checkBox.Name);
                applyWeaponID();
            }

            // Store the data of the passive skill default and reload it except for the modified checkbox
            if (tree_Weapon_Default.IsSelected)
            {
                storeWeaponDefault(checkBox.Name);
                applyWeaponDefault();
            }
        }

        private void check_Weapon_Parameter_Unchecked(object sender, RoutedEventArgs e)
        {
            // Check if you are in a valide time to update the passive skill parameter
            if (updating || (tree_Weapon_IsFamily() == false && tree_Weapon_IsIndividual() == false && tree_Weapon_Default.IsSelected == false)) { return; }

            // Store the data of the passive skill family and reload it
            if (tree_Weapon_IsFamily())
            {
                storeWeaponFamily();
                applyWeaponFamily();
            }

            // Store the data of the passive skill individual and reload it
            if (tree_Weapon_IsIndividual())
            {
                storeWeaponID();
                applyWeaponID();
            }

            // Store the data of the passive skill default and reload it
            if (tree_Weapon_Default.IsSelected)
            {
                storeWeaponDefault();
                applyWeaponDefault();
            }
        }

        #endregion

        #endregion

        #region Armor

        #region TreeView

        # region Get Selected

        private bool tree_Armor_IsFamily()
        {
            // Get the selected item data
            TreeViewItem tvi = (TreeViewItem)tree_Armor.SelectedItem;

            // Check if the selected item is a member of the family branch
            if (tree_Armor_Family.IsSelected == false && tree_Armor_Individual.IsSelected == false && tree_Armor_Default.IsSelected == false && tree_Armor_Family.IsAncestorOf(tvi))
            {
                return true;
            }

            return false;
        }

        private bool tree_Armor_IsIndividual()
        {
            // Get the selected item data
            TreeViewItem tvi = (TreeViewItem)tree_Armor.SelectedItem;

            // Check if the selected item is a member of the individual branch
            if (tree_Armor_Family.IsSelected == false && tree_Armor_Individual.IsSelected == false && tree_Armor_Default.IsSelected == false && tree_Armor_Individual.IsAncestorOf(tvi))
            {
                return true;
            }

            return false;
        }

        private int tree_Armor_ID()
        {
            // Set the variable use to convert the string to an integer
            int result;
            int id = -1;

            // Get the selected item data
            TreeViewItem tvi = (TreeViewItem)tree_Armor.SelectedItem;

            // Check if you the selected item is a member of the family branch or the individual branch
            if ((tree_Armor_IsFamily() || tree_Armor_IsIndividual()) && tree_Armor_Default.IsSelected == false)
            {
                // Try to convert the string to an integer
                if (Int32.TryParse(tvi.Tag.ToString(), out result))
                {
                    id = result;
                }
            }

            return id;
        }

        #endregion

        #region Update data

        private void tree_Armor_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            // Check if the the treeview can be updated
            if (updating) { return; }

            // Store the passive skill family data if the previous selected item was a member of the family branch
            if (lastIndexArmorFamily > 0)
            {
                storeArmorFamily(lastIndexArmorFamily);

                // Update the navigation tree
                int i = 0;
                foreach (TreeViewItem treeItem in tree_Armor_Family.Items)
                {
                    treeItem.Header = cfg.ArmorFamily[i].Name;
                    i++;
                }
            }

            // Store the passive skill individual data if the previous selected item was a member of the individual branch
            if (lastIndexArmorID > 0)
            {
                storeArmorID(lastIndexArmorID);
            }

            // Store the passive skill default data if the previous selected item was the default member
            if (lastIndexArmorDefault)
            {
                storeArmorDefault();
            }

            // Update the previous index
            lastIndexArmorFamily = 0;
            lastIndexArmorID = 0;
            lastIndexArmorDefault = false;

            // Check if the selected item is a member of the family branch
            if (tree_Armor_IsFamily())
            {
                // Update the previous index
                lastIndexArmorFamily = tree_Armor_ID();

                // Apply the new passive skill family data
                applyArmorFamily();
            }
            else if (tree_Armor_IsIndividual())
            {
                // Update the previous index
                lastIndexArmorID = tree_Armor_ID();

                // Apply the new passive skill individual data
                applyArmorID();
            }
            else if (tree_Armor_Default.IsSelected)
            {
                // Update the previous index
                lastIndexArmorDefault = true;

                // Apply the new skikll default data
                applyArmorDefault();
            }
            else
            {
                // Empty the passive skill data when their is no valid item selected
                applyEmptyArmor();
            }
        }

        #endregion

        #region Manage data

        private void btn_Armor_Add_Click(object sender, RoutedEventArgs e)
        {
            // Get the new index
            int id = tree_Armor_Family.Items.Count + 1;

            // Create the new item in the tree and the configuration
            cfg.ArmorFamily.Add(new DataPackEquipment(id));
            tree_Armor_Family.Items.Add(new TreeViewItem() { Header = cfg.ArmorFamily[id - 1].Name, Tag = id.ToString() });
        }

        private void btn_Armor_Remove_Click(object sender, RoutedEventArgs e)
        {
            // Set the variable for the new item index
            int id;

            // Check if the selected tree item is a member of family 
            if (tree_Armor_IsFamily() && tree_Armor_ID() > 0 && cfg.ArmorFamily.Count > 0)
            {
                // Set the updating flag
                updating = true;

                // Get the selected item data
                TreeViewItem tvi = (TreeViewItem)tree_Armor.SelectedItem;

                // Return the passive skill in the family to the available list
                if (cfg.ArmorFamily[tree_Armor_ID() - 1].ArmorFamily.Count > 0)
                {
                    foreach (Armor armor in cfg.ArmorFamily[tree_Armor_ID() - 1].ArmorFamily)
                    {
                        cfg.ArmorAvailable.Add(new Armor(armor.ID, armor.Name));
                    }
                }

                // Remove the selected item from the tree and the configuration
                cfg.ArmorFamily.RemoveAt(tree_Armor_ID() - 1);
                tree_Armor_Family.Items.Remove(tvi);

                // Renumber the id of the tree items
                id = 1;
                foreach (TreeViewItem tv in tree_Armor_Family.Items)
                {
                    tv.Tag = id.ToString();
                    id++;
                }

                // Renumber the id of the conguguration
                id = 1;
                foreach (DataPackEquipment armorFamily in cfg.ArmorFamily)
                {
                    armorFamily.ID = id;
                    id++;
                }

                // Empty the passive skill configuration screen
                tree_Armor_Family.IsSelected = true;
                applyEmptyArmor();

                // Remove the updating Flag
                updating = false;
            }
        }

        private void btn_Armor_Reset_Click(object sender, RoutedEventArgs e)
        {
            if (tree_Armor_IsFamily())
            {
                cfg.ActorFamily[tree_Armor_ID() - 1].Reset();
                applyActorFamily();
            }
            else if (tree_Armor_IsIndividual())
            {
                cfg.ActorID[tree_Armor_ID() - 1].Reset();
                applyActorID();
            }
            else if (tree_Armor_Default.IsSelected == true)
            {
                cfg.ActorDefault.Reset();
                applyActorDefault();
            }
        }

        #endregion

        #region Clipboard

        private void tree_Armor_Copy_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Check if the selected items is valid
            if (tree_Armor_IsFamily() || tree_Armor_IsIndividual() || tree_Armor_Default.IsSelected == true)
            {
                // Empty the previous clipboard
                cachedActor = null;
                cachedClass = null;
                cachedSkill = null;
                cachedPassiveSkill = null;
                cachedWeapon = null;
                cachedArmor = null;
                cachedEnemy = null;
                cachedState = null;

                // Copy the data from the family
                if (tree_Armor_IsFamily())
                {
                    storeArmorFamily();
                    cachedArmor = cfg.ArmorFamily[tree_Armor_ID() - 1];
                }

                // Copy the data from the individual
                else if (tree_Armor_IsIndividual())
                {
                    storeArmorID();
                    cachedArmor = cfg.ArmorID[tree_Armor_ID() - 1];
                }

                // Copy the data from the default
                else if (tree_Armor_Default.IsSelected == true)
                {
                    storeArmorDefault();
                    cachedArmor = cfg.ArmorDefault;
                }
            }
        }

        private void tree_Armor_Paste_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Check if the selected items is valid
            if (tree_Armor_IsFamily() || tree_Armor_IsIndividual() || tree_Armor_Default.IsSelected == true)
            {
                // Check if an item was copy
                if (cachedArmor != null)
                {
                    // Paste the data from the family
                    if (tree_Armor_IsFamily())
                    {
                        cfg.ArmorFamily[tree_Armor_ID() - 1].PasteFrom(cachedArmor);
                        applyArmorFamily();
                    }

                    // Paste the data from the individual
                    else if (tree_Armor_IsIndividual())
                    {
                        cfg.ArmorID[tree_Armor_ID() - 1].PasteFrom(cachedArmor);
                        applyArmorID();
                    }

                    // Paste the data from the default
                    else if (tree_Armor_Default.IsSelected == true)
                    {
                        cfg.ArmorDefault.PasteFrom(cachedArmor);
                        applyArmorDefault();
                    }
                }
            }
        }

        #endregion

        #endregion

        #region Family Configuration

        private void btn_Armor_AddToFamily_Click(object sender, RoutedEventArgs e)
        {
            // Check if you are in a valide time to add an passive skill to the family
            if (updating || tree_Armor_IsFamily() == false || tree_Armor_IsIndividual() == true || tree_Armor_Default.IsSelected == true || list_Armor_Available.SelectedIndex < 0) { return; }

            // Get the index of the selected passive skill
            int index = list_Armor_Available.SelectedIndex;

            // Add the passive skill to the passive skill in family list
            armorInFamily.Add(new Armor(armorAvailable[index].ID, armorAvailable[index].Name));
            armorInFamily.Sort((x, y) => x.ID.CompareTo(y.ID));

            // Remove the passive skill from the available list
            armorAvailable.RemoveAt(index);

            // Update the available list index
            if (index > 0) { list_Armor_Available.SelectedIndex = index - 1; }
            else { list_Armor_Available.SelectedIndex = 0; }

            // Store the modification
            storeArmorFamily();
        }

        private void btn_Armor_RemoveFromFamily_Click(object sender, RoutedEventArgs e)
        {
            // Check if you are in a valide time to remove from passive skill to the family
            if (updating || tree_Armor_IsFamily() == false || tree_Armor_IsIndividual() == true || tree_Armor_Default.IsSelected == true || list_Armor_InFamily.SelectedIndex < 0) { return; }

            // Get the index of the selected passive skill
            int index = list_Armor_InFamily.SelectedIndex;

            // Add the passive skill to the available list
            armorAvailable.Add(new Armor(armorInFamily[index].ID, armorInFamily[index].Name));
            armorAvailable.Sort((x, y) => x.ID.CompareTo(y.ID));

            // Remove the passive skill from the passive skill in family list
            skillInFamily.RemoveAt(index);

            // Update the available list index
            if (index > 0) { list_Armor_InFamily.SelectedIndex = index - 1; }
            else { list_Armor_InFamily.SelectedIndex = 0; }

            // Store the modification
            storeArmorFamily();
        }

        #endregion

        #region Parameter Configuration

        private void check_Armor_Parameter_Checked(object sender, RoutedEventArgs e)
        {
            // Check if you are in a valide time to update the passive skill parameter
            if (updating || (tree_Armor_IsFamily() == false && tree_Armor_IsIndividual() == false && tree_Armor_Default.IsSelected == false)) { return; }

            // Get the currently modified checkbox
            CheckBox checkBox = sender as CheckBox;

            // Store the data of the passive skill family and reload it except for the modified checkbox
            if (tree_Armor_IsFamily())
            {
                storeArmorFamily(checkBox.Name);
                applyArmorFamily();
            }

            // Store the data of the passive skill individual and reload it except for the modified checkbox
            if (tree_Armor_IsIndividual())
            {
                storeArmorID(checkBox.Name);
                applyArmorID();
            }

            // Store the data of the passive skill default and reload it except for the modified checkbox
            if (tree_Armor_Default.IsSelected)
            {
                storeArmorDefault(checkBox.Name);
                applyArmorDefault();
            }
        }

        private void check_Armor_Parameter_Unchecked(object sender, RoutedEventArgs e)
        {
            // Check if you are in a valide time to update the passive skill parameter
            if (updating || (tree_Armor_IsFamily() == false && tree_Armor_IsIndividual() == false && tree_Armor_Default.IsSelected == false)) { return; }

            // Store the data of the passive skill family and reload it
            if (tree_Armor_IsFamily())
            {
                storeArmorFamily();
                applyArmorFamily();
            }

            // Store the data of the passive skill individual and reload it
            if (tree_Armor_IsIndividual())
            {
                storeArmorID();
                applyArmorID();
            }

            // Store the data of the passive skill default and reload it
            if (tree_Armor_Default.IsSelected)
            {
                storeArmorDefault();
                applyArmorDefault();
            }
        }

        #endregion

        #endregion

        #region Enemy

        #region TreeView

        # region Get Selected

        private bool tree_Enemy_IsFamily()
        {
            // Get the selected item data
            TreeViewItem tvi = (TreeViewItem)tree_Enemy.SelectedItem;

            // Check if the selected item is a member of the family branch
            if (tree_Enemy_Family.IsSelected == false && tree_Enemy_Individual.IsSelected == false && tree_Enemy_Default.IsSelected == false && tree_Enemy_Family.IsAncestorOf(tvi))
            {
                return true;
            }

            return false;
        }

        private bool tree_Enemy_IsIndividual()
        {
            // Get the selected item data
            TreeViewItem tvi = (TreeViewItem)tree_Enemy.SelectedItem;

            // Check if the selected item is a member of the individual branch
            if (tree_Enemy_Family.IsSelected == false && tree_Enemy_Individual.IsSelected == false && tree_Enemy_Default.IsSelected == false && tree_Enemy_Individual.IsAncestorOf(tvi))
            {
                return true;
            }

            return false;
        }

        private int tree_Enemy_ID()
        {
            // Set the variable use to convert the string to an integer
            int result;
            int id = -1;

            // Get the selected item data
            TreeViewItem tvi = (TreeViewItem)tree_Enemy.SelectedItem;

            // Check if you the selected item is a member of the family branch or the individual branch
            if ((tree_Enemy_IsFamily() || tree_Enemy_IsIndividual()) && tree_Enemy_Default.IsSelected == false)
            {
                // Try to convert the string to an integer
                if (Int32.TryParse(tvi.Tag.ToString(), out result))
                {
                    id = result;
                }
            }

            return id;
        }

        #endregion

        #region Update data

        private void tree_Enemy_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            // Check if the the treeview can be updated
            if (updating) { return; }

            // Store the passive skill family data if the previous selected item was a member of the family branch
            if (lastIndexEnemyFamily > 0)
            {
                storeEnemyFamily(lastIndexEnemyFamily);

                // Update the navigation tree
                int i = 0;
                foreach (TreeViewItem treeItem in tree_Enemy_Family.Items)
                {
                    treeItem.Header = cfg.EnemyFamily[i].Name;
                    i++;
                }
            }

            // Store the passive skill individual data if the previous selected item was a member of the individual branch
            if (lastIndexEnemyID > 0)
            {
                storeEnemyID(lastIndexEnemyID);
            }

            // Store the passive skill default data if the previous selected item was the default member
            if (lastIndexEnemyDefault)
            {
                storeEnemyDefault();
            }

            // Update the previous index
            lastIndexEnemyFamily = 0;
            lastIndexEnemyID = 0;
            lastIndexEnemyDefault = false;

            // Check if the selected item is a member of the family branch
            if (tree_Enemy_IsFamily())
            {
                // Update the previous index
                lastIndexEnemyFamily = tree_Enemy_ID();

                // Apply the new passive skill family data
                applyEnemyFamily();
            }
            else if (tree_Enemy_IsIndividual())
            {
                // Update the previous index
                lastIndexEnemyID = tree_Enemy_ID();

                // Apply the new passive skill individual data
                applyEnemyID();
            }
            else if (tree_Enemy_Default.IsSelected)
            {
                // Update the previous index
                lastIndexEnemyDefault = true;

                // Apply the new skikll default data
                applyEnemyDefault();
            }
            else
            {
                // Empty the passive skill data when their is no valid item selected
                applyEmptyEnemy();
            }
        }

        #endregion

        #region Manage data

        private void btn_Enemy_Add_Click(object sender, RoutedEventArgs e)
        {
            // Get the new index
            int id = tree_Enemy_Family.Items.Count + 1;

            // Create the new item in the tree and the configuration
            cfg.EnemyFamily.Add(new DataPackEnemy(id));
            tree_Enemy_Family.Items.Add(new TreeViewItem() { Header = cfg.EnemyFamily[id - 1].Name, Tag = id.ToString() });
        }

        private void btn_Enemy_Remove_Click(object sender, RoutedEventArgs e)
        {
            // Set the variable for the new item index
            int id;

            // Check if the selected tree item is a member of family 
            if (tree_Enemy_IsFamily() && tree_Enemy_ID() > 0 && cfg.EnemyFamily.Count > 0)
            {
                // Set the updating flag
                updating = true;

                // Get the selected item data
                TreeViewItem tvi = (TreeViewItem)tree_Enemy.SelectedItem;

                // Return the passive skill in the family to the available list
                if (cfg.EnemyFamily[tree_Enemy_ID() - 1].EnemyFamily.Count > 0)
                {
                    foreach (Enemy enemy in cfg.EnemyFamily[tree_Enemy_ID() - 1].EnemyFamily)
                    {
                        cfg.EnemyAvailable.Add(new Enemy(enemy.ID, enemy.Name));
                    }
                }

                // Remove the selected item from the tree and the configuration
                cfg.EnemyFamily.RemoveAt(tree_Enemy_ID() - 1);
                tree_Enemy_Family.Items.Remove(tvi);

                // Renumber the id of the tree items
                id = 1;
                foreach (TreeViewItem tv in tree_Enemy_Family.Items)
                {
                    tv.Tag = id.ToString();
                    id++;
                }

                // Renumber the id of the conguguration
                id = 1;
                foreach (DataPackEnemy enemyFamily in cfg.EnemyFamily)
                {
                    enemyFamily.ID = id;
                    id++;
                }

                // Empty the passive skill configuration screen
                tree_Enemy_Family.IsSelected = true;
                applyEmptyEnemy();

                // Remove the updating Flag
                updating = false;
            }
        }

        private void btn_Enemy_Reset_Click(object sender, RoutedEventArgs e)
        {
            if (tree_Enemy_IsFamily())
            {
                cfg.ActorFamily[tree_Enemy_ID() - 1].Reset();
                applyActorFamily();
            }
            else if (tree_Enemy_IsIndividual())
            {
                cfg.ActorID[tree_Enemy_ID() - 1].Reset();
                applyActorID();
            }
            else if (tree_Enemy_Default.IsSelected == true)
            {
                cfg.ActorDefault.Reset();
                applyActorDefault();
            }
        }

        #endregion

        #region Clipboard

        private void tree_Enemy_Copy_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Check if the selected items is valid
            if (tree_Enemy_IsFamily() || tree_Enemy_IsIndividual() || tree_Enemy_Default.IsSelected == true)
            {
                // Empty the previous clipboard
                cachedActor = null;
                cachedClass = null;
                cachedSkill = null;
                cachedPassiveSkill = null;
                cachedWeapon = null;
                cachedArmor = null;
                cachedEnemy = null;
                cachedState = null;

                // Copy the data from the family
                if (tree_Enemy_IsFamily())
                {
                    storeEnemyFamily();
                    cachedEnemy = cfg.EnemyFamily[tree_Enemy_ID() - 1];
                }

                // Copy the data from the individual
                else if (tree_Enemy_IsIndividual())
                {
                    storeEnemyID();
                    cachedEnemy = cfg.EnemyID[tree_Enemy_ID() - 1];
                }

                // Copy the data from the default
                else if (tree_Enemy_Default.IsSelected == true)
                {
                    storeEnemyDefault();
                    cachedEnemy = cfg.EnemyDefault;
                }
            }
        }

        private void tree_Enemy_Paste_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Check if the selected items is valid
            if (tree_Enemy_IsFamily() || tree_Enemy_IsIndividual() || tree_Enemy_Default.IsSelected == true)
            {
                // Check if an item was copy
                if (cachedEnemy != null)
                {
                    // Paste the data from the family
                    if (tree_Enemy_IsFamily())
                    {
                        cfg.EnemyFamily[tree_Enemy_ID() - 1].PasteFrom(cachedEnemy);
                        applyEnemyFamily();
                    }

                    // Paste the data from the individual
                    else if (tree_Enemy_IsIndividual())
                    {
                        cfg.EnemyID[tree_Enemy_ID() - 1].PasteFrom(cachedEnemy);
                        applyEnemyID();
                    }

                    // Paste the data from the default
                    else if (tree_Enemy_Default.IsSelected == true)
                    {
                        cfg.EnemyDefault.PasteFrom(cachedEnemy);
                        applyEnemyDefault();
                    }
                }
            }
        }

        #endregion

        #endregion

        #region Family Configuration

        private void btn_Enemy_AddToFamily_Click(object sender, RoutedEventArgs e)
        {
            // Check if you are in a valide time to add an passive skill to the family
            if (updating || tree_Enemy_IsFamily() == false || tree_Enemy_IsIndividual() == true || tree_Enemy_Default.IsSelected == true || list_Enemy_Available.SelectedIndex < 0) { return; }

            // Get the index of the selected passive skill
            int index = list_Enemy_Available.SelectedIndex;

            // Add the passive skill to the passive skill in family list
            enemyInFamily.Add(new Enemy(enemyAvailable[index].ID, enemyAvailable[index].Name));
            enemyInFamily.Sort((x, y) => x.ID.CompareTo(y.ID));

            // Remove the passive skill from the available list
            enemyAvailable.RemoveAt(index);

            // Update the available list index
            if (index > 0) { list_Enemy_Available.SelectedIndex = index - 1; }
            else { list_Enemy_Available.SelectedIndex = 0; }

            // Store the modification
            storeEnemyFamily();
        }

        private void btn_Enemy_RemoveFromFamily_Click(object sender, RoutedEventArgs e)
        {
            // Check if you are in a valide time to remove from passive skill to the family
            if (updating || tree_Enemy_IsFamily() == false || tree_Enemy_IsIndividual() == true || tree_Enemy_Default.IsSelected == true || list_Enemy_InFamily.SelectedIndex < 0) { return; }

            // Get the index of the selected passive skill
            int index = list_Enemy_InFamily.SelectedIndex;

            // Add the passive skill to the available list
            enemyAvailable.Add(new Enemy(enemyInFamily[index].ID, enemyInFamily[index].Name));
            enemyAvailable.Sort((x, y) => x.ID.CompareTo(y.ID));

            // Remove the passive skill from the passive skill in family list
            skillInFamily.RemoveAt(index);

            // Update the available list index
            if (index > 0) { list_Enemy_InFamily.SelectedIndex = index - 1; }
            else { list_Enemy_InFamily.SelectedIndex = 0; }

            // Store the modification
            storeEnemyFamily();
        }

        #endregion

        #region Parameter Configuration

        private void check_Enemy_Parameter_Checked(object sender, RoutedEventArgs e)
        {
            // Check if you are in a valide time to update the passive skill parameter
            if (updating || (tree_Enemy_IsFamily() == false && tree_Enemy_IsIndividual() == false && tree_Enemy_Default.IsSelected == false)) { return; }

            // Get the currently modified checkbox
            CheckBox checkBox = sender as CheckBox;

            // Store the data of the passive skill family and reload it except for the modified checkbox
            if (tree_Enemy_IsFamily())
            {
                storeEnemyFamily(checkBox.Name);
                applyEnemyFamily();
            }

            // Store the data of the passive skill individual and reload it except for the modified checkbox
            if (tree_Enemy_IsIndividual())
            {
                storeEnemyID(checkBox.Name);
                applyEnemyID();
            }

            // Store the data of the passive skill default and reload it except for the modified checkbox
            if (tree_Enemy_Default.IsSelected)
            {
                storeEnemyDefault(checkBox.Name);
                applyEnemyDefault();
            }
        }

        private void check_Enemy_Parameter_Unchecked(object sender, RoutedEventArgs e)
        {
            // Check if you are in a valide time to update the passive skill parameter
            if (updating || (tree_Enemy_IsFamily() == false && tree_Enemy_IsIndividual() == false && tree_Enemy_Default.IsSelected == false)) { return; }

            // Store the data of the passive skill family and reload it
            if (tree_Enemy_IsFamily())
            {
                storeEnemyFamily();
                applyEnemyFamily();
            }

            // Store the data of the passive skill individual and reload it
            if (tree_Enemy_IsIndividual())
            {
                storeEnemyID();
                applyEnemyID();
            }

            // Store the data of the passive skill default and reload it
            if (tree_Enemy_Default.IsSelected)
            {
                storeEnemyDefault();
                applyEnemyDefault();
            }
        }

        #endregion

        #endregion

        #region State

        #region TreeView

        # region Get Selected

        private bool tree_State_IsFamily()
        {
            // Get the selected item data
            TreeViewItem tvi = (TreeViewItem)tree_State.SelectedItem;

            // Check if the selected item is a member of the family branch
            if (tree_State_Family.IsSelected == false && tree_State_Individual.IsSelected == false && tree_State_Default.IsSelected == false && tree_State_Family.IsAncestorOf(tvi))
            {
                return true;
            }

            return false;
        }

        private bool tree_State_IsIndividual()
        {
            // Get the selected item data
            TreeViewItem tvi = (TreeViewItem)tree_State.SelectedItem;

            // Check if the selected item is a member of the individual branch
            if (tree_State_Family.IsSelected == false && tree_State_Individual.IsSelected == false && tree_State_Default.IsSelected == false && tree_State_Individual.IsAncestorOf(tvi))
            {
                return true;
            }

            return false;
        }

        private int tree_State_ID()
        {
            // Set the variable use to convert the string to an integer
            int result;
            int id = -1;

            // Get the selected item data
            TreeViewItem tvi = (TreeViewItem)tree_State.SelectedItem;

            // Check if you the selected item is a member of the family branch or the individual branch
            if ((tree_State_IsFamily() || tree_State_IsIndividual()) && tree_State_Default.IsSelected == false)
            {
                // Try to convert the string to an integer
                if (Int32.TryParse(tvi.Tag.ToString(), out result))
                {
                    id = result;
                }
            }

            return id;
        }

        #endregion

        #region Update data

        private void tree_State_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            // Check if the the treeview can be updated
            if (updating) { return; }

            // Store the passive skill family data if the previous selected item was a member of the family branch
            if (lastIndexStateFamily > 0)
            {
                storeStateFamily(lastIndexStateFamily);

                // Update the navigation tree
                int i = 0;
                foreach (TreeViewItem treeItem in tree_State_Family.Items)
                {
                    treeItem.Header = cfg.StateFamily[i].Name;
                    i++;
                }
            }

            // Store the passive skill individual data if the previous selected item was a member of the individual branch
            if (lastIndexStateID > 0)
            {
                storeStateID(lastIndexStateID);
            }

            // Store the passive skill default data if the previous selected item was the default member
            if (lastIndexStateDefault)
            {
                storeStateDefault();
            }

            // Update the previous index
            lastIndexStateFamily = 0;
            lastIndexStateID = 0;
            lastIndexStateDefault = false;

            // Check if the selected item is a member of the family branch
            if (tree_State_IsFamily())
            {
                // Update the previous index
                lastIndexStateFamily = tree_State_ID();

                // Apply the new passive skill family data
                applyStateFamily();
            }
            else if (tree_State_IsIndividual())
            {
                // Update the previous index
                lastIndexStateID = tree_State_ID();

                // Apply the new passive skill individual data
                applyStateID();
            }
            else if (tree_State_Default.IsSelected)
            {
                // Update the previous index
                lastIndexStateDefault = true;

                // Apply the new skikll default data
                applyStateDefault();
            }
            else
            {
                // Empty the passive skill data when their is no valid item selected
                applyEmptyState();
            }
        }

        #endregion

        #region Manage data

        private void btn_State_Add_Click(object sender, RoutedEventArgs e)
        {
            // Get the new index
            int id = tree_State_Family.Items.Count + 1;

            // Create the new item in the tree and the configuration
            cfg.StateFamily.Add(new DataPackState(id));
            tree_State_Family.Items.Add(new TreeViewItem() { Header = cfg.StateFamily[id - 1].Name, Tag = id.ToString() });
        }

        private void btn_State_Remove_Click(object sender, RoutedEventArgs e)
        {
            // Set the variable for the new item index
            int id;

            // Check if the selected tree item is a member of family 
            if (tree_State_IsFamily() && tree_State_ID() > 0 && cfg.StateFamily.Count > 0)
            {
                // Set the updating flag
                updating = true;

                // Get the selected item data
                TreeViewItem tvi = (TreeViewItem)tree_State.SelectedItem;

                // Return the passive skill in the family to the available list
                if (cfg.StateFamily[tree_State_ID() - 1].StateFamily.Count > 0)
                {
                    foreach (State state in cfg.StateFamily[tree_State_ID() - 1].StateFamily)
                    {
                        cfg.StateAvailable.Add(new State(state.ID, state.Name));
                    }
                }

                // Remove the selected item from the tree and the configuration
                cfg.StateFamily.RemoveAt(tree_State_ID() - 1);
                tree_State_Family.Items.Remove(tvi);

                // Renumber the id of the tree items
                id = 1;
                foreach (TreeViewItem tv in tree_State_Family.Items)
                {
                    tv.Tag = id.ToString();
                    id++;
                }

                // Renumber the id of the conguguration
                id = 1;
                foreach (DataPackState stateFamily in cfg.StateFamily)
                {
                    stateFamily.ID = id;
                    id++;
                }

                // Empty the passive skill configuration screen
                tree_State_Family.IsSelected = true;
                applyEmptyState();

                // Remove the updating Flag
                updating = false;
            }
        }

        private void btn_State_Reset_Click(object sender, RoutedEventArgs e)
        {
            if (tree_State_IsFamily())
            {
                cfg.ActorFamily[tree_State_ID() - 1].Reset();
                applyActorFamily();
            }
            else if (tree_State_IsIndividual())
            {
                cfg.ActorID[tree_State_ID() - 1].Reset();
                applyActorID();
            }
            else if (tree_State_Default.IsSelected == true)
            {
                cfg.ActorDefault.Reset();
                applyActorDefault();
            }
        }


        #endregion

        #region Clipboard

        private void tree_State_Copy_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Check if the selected items is valid
            if (tree_State_IsFamily() || tree_State_IsIndividual() || tree_State_Default.IsSelected == true)
            {
                // Empty the previous clipboard
                cachedActor = null;
                cachedClass = null;
                cachedSkill = null;
                cachedPassiveSkill = null;
                cachedWeapon = null;
                cachedArmor = null;
                cachedEnemy = null;
                cachedState = null;

                // Copy the data from the family
                if (tree_State_IsFamily())
                {
                    storeStateFamily();
                    cachedState = cfg.StateFamily[tree_State_ID() - 1];
                }

                // Copy the data from the individual
                else if (tree_State_IsIndividual())
                {
                    storeStateID();
                    cachedState = cfg.StateID[tree_State_ID() - 1];
                }

                // Copy the data from the default
                else if (tree_State_Default.IsSelected == true)
                {
                    storeStateDefault();
                    cachedState = cfg.StateDefault;
                }
            }
        }

        private void tree_State_Paste_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Check if the selected items is valid
            if (tree_State_IsFamily() || tree_State_IsIndividual() || tree_State_Default.IsSelected == true)
            {
                // Check if an item was copy
                if (cachedState != null)
                {
                    // Paste the data from the family
                    if (tree_State_IsFamily())
                    {
                        cfg.StateFamily[tree_State_ID() - 1].PasteFrom(cachedState);
                        applyStateFamily();
                    }

                    // Paste the data from the individual
                    else if (tree_State_IsIndividual())
                    {
                        cfg.StateID[tree_State_ID() - 1].PasteFrom(cachedState);
                        applyStateID();
                    }

                    // Paste the data from the default
                    else if (tree_State_Default.IsSelected == true)
                    {
                        cfg.StateDefault.PasteFrom(cachedState);
                        applyStateDefault();
                    }
                }
            }
        }

        #endregion

        #endregion

        #region Family Configuration

        private void btn_State_AddToFamily_Click(object sender, RoutedEventArgs e)
        {
            // Check if you are in a valide time to add an passive skill to the family
            if (updating || tree_State_IsFamily() == false || tree_State_IsIndividual() == true || tree_State_Default.IsSelected == true || list_State_Available.SelectedIndex < 0) { return; }

            // Get the index of the selected passive skill
            int index = list_State_Available.SelectedIndex;

            // Add the passive skill to the passive skill in family list
            stateInFamily.Add(new State(stateAvailable[index].ID, stateAvailable[index].Name));
            stateInFamily.Sort((x, y) => x.ID.CompareTo(y.ID));

            // Remove the passive skill from the available list
            stateAvailable.RemoveAt(index);

            // Update the available list index
            if (index > 0) { list_State_Available.SelectedIndex = index - 1; }
            else { list_State_Available.SelectedIndex = 0; }

            // Store the modification
            storeStateFamily();
        }

        private void btn_State_RemoveFromFamily_Click(object sender, RoutedEventArgs e)
        {
            // Check if you are in a valide time to remove from passive skill to the family
            if (updating || tree_State_IsFamily() == false || tree_State_IsIndividual() == true || tree_State_Default.IsSelected == true || list_State_InFamily.SelectedIndex < 0) { return; }

            // Get the index of the selected passive skill
            int index = list_State_InFamily.SelectedIndex;

            // Add the passive skill to the available list
            stateAvailable.Add(new State(stateInFamily[index].ID, stateInFamily[index].Name));
            stateAvailable.Sort((x, y) => x.ID.CompareTo(y.ID));

            // Remove the passive skill from the passive skill in family list
            skillInFamily.RemoveAt(index);

            // Update the available list index
            if (index > 0) { list_State_InFamily.SelectedIndex = index - 1; }
            else { list_State_InFamily.SelectedIndex = 0; }

            // Store the modification
            storeStateFamily();
        }

        #endregion

        #region Parameter Configuration

        private void check_State_Parameter_Checked(object sender, RoutedEventArgs e)
        {
            // Check if you are in a valide time to update the passive skill parameter
            if (updating || (tree_State_IsFamily() == false && tree_State_IsIndividual() == false && tree_State_Default.IsSelected == false)) { return; }

            // Get the currently modified checkbox
            CheckBox checkBox = sender as CheckBox;

            // Store the data of the passive skill family and reload it except for the modified checkbox
            if (tree_State_IsFamily())
            {
                storeStateFamily(checkBox.Name);
                applyStateFamily();
            }

            // Store the data of the passive skill individual and reload it except for the modified checkbox
            if (tree_State_IsIndividual())
            {
                storeStateID(checkBox.Name);
                applyStateID();
            }

            // Store the data of the passive skill default and reload it except for the modified checkbox
            if (tree_State_Default.IsSelected)
            {
                storeStateDefault(checkBox.Name);
                applyStateDefault();
            }
        }

        private void check_State_Parameter_Unchecked(object sender, RoutedEventArgs e)
        {
            // Check if you are in a valide time to update the passive skill parameter
            if (updating || (tree_State_IsFamily() == false && tree_State_IsIndividual() == false && tree_State_Default.IsSelected == false)) { return; }

            // Store the data of the passive skill family and reload it
            if (tree_State_IsFamily())
            {
                storeStateFamily();
                applyStateFamily();
            }

            // Store the data of the passive skill individual and reload it
            if (tree_State_IsIndividual())
            {
                storeStateID();
                applyStateID();
            }

            // Store the data of the passive skill default and reload it
            if (tree_State_Default.IsSelected)
            {
                storeStateDefault();
                applyStateDefault();
            }
        }

        #endregion

        #endregion

        #region Generate

        private void btn_Generate_Click(object sender, RoutedEventArgs e)
        {
            storeConfiguration();
            txt_Generate.Text = Generator.GenerateScript(cfg, gameData);
        }

        private void btn_Gen_Copy_Click(object sender, RoutedEventArgs e)
        {
            if (txt_Generate.Text != "")
            {
                Clipboard.SetText(txt_Generate.Text);
            }
        }

        #endregion

        #endregion

        #endregion

        #region Configuration Control

        #region Setup Configuration

        private void setupConfiguration()
        {
            #region General

            resetGeneral();

            #endregion

            #region Default

            resetDefault();

            #endregion

            #region Actor

            resetActorFamily();
            resetActorID();
            resetActorDefault();

            #endregion

            #region Class

            resetClassFamily();
            resetClassID();
            resetClassDefault();

            #endregion

            #region Skill

            resetSkillFamily();
            resetSkillID();
            resetSkillDefault();

            #endregion

            #region Passive Skill

            resetPassiveSkillFamily();
            resetPassiveSkillID();
            resetPassiveSkillDefault();

            #endregion

            #region Weapon

            resetWeaponFamily();
            resetWeaponID();
            resetWeaponDefault();

            #endregion

            #region Armor

            resetArmorFamily();
            resetArmorID();
            resetArmorDefault();

            #endregion

            #region Enemy

            resetEnemyFamily();
            resetEnemyID();
            resetEnemyDefault();

            #endregion

            #region State

            resetStateFamily();
            resetStateID();
            resetStateDefault();

            #endregion
        }

        #endregion

        #region Apply Configuration

        private void applyConfiguration()
        {
            #region General

            applyGeneral();

            #endregion

            #region Default

            applyDefault();

            #endregion

            #region Actor

            if (tree_Actor_IsFamily())
            {
                applyActorFamily();
            }
            else if (tree_Actor_IsIndividual())
            {
                applyActorID();
            }
            else if (tree_Actor_Default.IsSelected)
            {
                applyActorDefault();
            }
            else
            {
                applyEmptyActor();
            }

            #endregion

            #region Class

            if (tree_Class_IsFamily())
            {
                applyClassFamily();
            }
            else if (tree_Class_IsIndividual())
            {
                applyClassID();
            }
            else if (tree_Class_Default.IsSelected)
            {
                applyClassDefault();
            }
            else
            {
                applyEmptyClass();
            }

            #endregion

            #region Skill

            if (tree_Skill_IsFamily())
            {
                applySkillFamily();
            }
            else if (tree_Skill_IsIndividual())
            {
                applySkillID();
            }
            else if (tree_Skill_Default.IsSelected)
            {
                applySkillDefault();
            }
            else
            {
                applyEmptySkill();
            }

            #endregion

            #region Passive Skill

            if (tree_PassiveSkill_IsFamily())
            {
                applyPassiveSkillFamily();
            }
            else if (tree_PassiveSkill_IsIndividual())
            {
                applyPassiveSkillID();
            }
            else if (tree_PassiveSkill_Default.IsSelected)
            {
                applyPassiveSkillDefault();
            }
            else
            {
                applyPassiveSkillDefault();
            }

            #endregion

            #region Weapon

            if (tree_Weapon_IsFamily())
            {
                applyWeaponFamily();
            }
            else if (tree_Weapon_IsIndividual())
            {
                applyWeaponID();
            }
            else if (tree_Weapon_Default.IsSelected)
            {
                applyWeaponDefault();
            }
            else
            {
                applyWeaponDefault();
            }

            #endregion

            #region Armor

            if (tree_Armor_IsFamily())
            {
                applyArmorFamily();
            }
            else if (tree_Armor_IsIndividual())
            {
                applyArmorID();
            }
            else if (tree_Armor_Default.IsSelected)
            {
                applyArmorDefault();
            }
            else
            {
                applyArmorDefault();
            }

            #endregion

            #region Enemy

            if (tree_Enemy_IsFamily())
            {
                applyEnemyFamily();
            }
            else if (tree_Enemy_IsIndividual())
            {
                applyEnemyID();
            }
            else if (tree_Enemy_Default.IsSelected)
            {
                applyEnemyDefault();
            }
            else
            {
                applyEnemyDefault();
            }

            #endregion

            #region State

            if (tree_State_IsFamily())
            {
                applyStateFamily();
            }
            else if (tree_State_IsIndividual())
            {
                applyStateID();
            }
            else if (tree_State_Default.IsSelected)
            {
                applyStateDefault();
            }
            else
            {
                applyStateDefault();
            }

            #endregion
        }

        #region General

        private void applyGeneral()
        {
            // Set the updating flag
            updating = true;

            // Stat limit bypass
            check_General_StatLimitBypass.IsChecked = cfg.General.SetLimitBypass;
            if (cfg.General.SetLimitBypass)
            {
                intud_General_HPSPMax.IsEnabled = false;
                intud_General_StatMax.IsEnabled = false;
                intud_General_HPSPMax.Text = "";
                intud_General_StatMax.Text = "";
            }
            else
            {
                intud_General_HPSPMax.IsEnabled = true;
                intud_General_StatMax.IsEnabled = true;
                intud_General_HPSPMax.Value = cfg.General.SetHPSPMaxLimit;
                intud_General_StatMax.Value = cfg.General.SetStatMaxLimit;
            }

            // Attack skill rate behavior
            combo_General_ParamRateType.SelectedIndex = cfg.General.SkillParamRateType;
            combo_General_DefenseRateType.SelectedIndex = cfg.General.SkillDefenseRateType;

            // Actor class behavior
            combo_General_OrderEquipmentList.SelectedIndex = cfg.General.OrderEquipmentList;
            combo_General_OrderEquipmentMultiplier.SelectedIndex = cfg.General.OrderEquipmentMultiplier;
            combo_General_OrderEquipmentFlags.SelectedIndex = cfg.General.OrderEquipmentFlags;
            combo_General_OrderHandReduce.SelectedIndex = cfg.General.OrderHandReduce;
            combo_General_OrderUnarmedAttackForce.SelectedIndex = cfg.General.OrderUnarmedAttackForce;

            // Cursed color
            color_General_Cursed.R = cfg.General.CursedColorRed;
            color_General_Cursed.G = cfg.General.CursedColorGreen;
            color_General_Cursed.B = cfg.General.CursedColorBlue;
            color_General_Cursed.A = cfg.General.CursedColorAlpha;

            // Cursed equipment setting
            check_General_ShowCursed.IsChecked = cfg.General.SetShowCursed;
            if (cfg.General.SetShowCursed)
            {
                check_General_BlockCursed.IsChecked = cfg.General.SetBlockCursed;
            }
            else
            {
                check_General_BlockCursed.IsChecked = false;
            }

            // Remove the updating flag
            updating = false;
        }

        #endregion

        #region Default

        private void applyDefault()
        {
            // Set the updating flag
            updating = true;

            #region Equipment

            // Create the equipment name list
            defaultEquipType.Clear();
            if (cfg.Default.EquipType.Count > 0)
            {
                // Update the equipment type list
                foreach (EquipType equip in cfg.Default.EquipType)
                {
                    defaultEquipType.Add(new EquipType(equip.ID, equip.Name));
                }

                // Update the equipment type name box
                if (lastIndexDefaultEquipType >= 5)
                {
                    list_Default_EquipType.SelectedIndex = lastIndexDefaultEquipType;
                    txt_Default_NameEquipType.IsEnabled = true;
                    txt_Default_NameEquipType.Text = defaultEquipType[lastIndexDefaultEquipType].Name;
                }
                else
                {
                    list_Default_EquipType.SelectedIndex = lastIndexDefaultEquipType;
                    txt_Default_NameEquipType.IsEnabled = false;
                    txt_Default_NameEquipType.Text = "";
                }
            }
            else
            {
                list_Default_EquipType.SelectedIndex = -1;
                txt_Default_NameEquipType.IsEnabled = false;
                txt_Default_NameEquipType.Text = "";
            }

            // Create the equipment id list
            defaultEquipList.Clear();
            if (cfg.Default.EquipList.Count > 0)
            {
                foreach (EquipType equip in cfg.Default.EquipList)
                {
                    defaultEquipList.Add(new EquipType(equip.ID, equip.Name));
                }

                // Update the equipment id name box
                list_Default_EquipList.SelectedIndex = lastIndexDefaultEquipList;
                txt_Default_NameEquipList.IsEnabled = true;
                txt_Default_NameEquipList.Text = defaultEquipList[lastIndexDefaultEquipList].Name;
            }
            else
            {
                list_Default_EquipList.SelectedIndex = -1;
                txt_Default_NameEquipList.IsEnabled = false;
                txt_Default_NameEquipList.Text = "";
            }

            // Apply dual hold
            check_Default_DualHold.IsChecked = cfg.Default.DualHold;

            // Apply dual hold name
            txt_Default_DualHoldNameWeapon.Text = cfg.Default.DualHoldNameWeapon;
            txt_Default_DualHoldNameShield.Text = cfg.Default.DualHoldNameShield;

            // Apply dual hold multiplier
            decud_Default_DualHoldMulWeapon.Value = cfg.Default.DualHoldMulWeapon;
            decud_Default_DualHoldMulShield.Value = cfg.Default.DualHoldMulShield;

            // Apply shield bypass
            check_Default_ShieldBypass.IsChecked = cfg.Default.ShieldBypass;
            decud_Default_ShieldBypass.Value = cfg.Default.ShieldBypassMul;

            // Apply weapon bypass
            check_Default_WeaponBypass.IsChecked = cfg.Default.WeaponBypass;
            decud_Default_WeaponBypass.Value = cfg.Default.WeaponBypassMul;

            // Apply reduce hand
            intud_Default_ReduceHand.Value = cfg.Default.ReduceHand;
            decud_Default_ReduceHandMul.Value = cfg.Default.ReduceHandMul;

            #endregion

            #region Parameter

            // Apply the maximum HP
            intud_Default_MaxHPInitial.Value = cfg.Default.MaxHPInitial;
            intud_Default_MaxHPFinal.Value = cfg.Default.MaxHPFinal;

            // Apply the maximum SP
            intud_Default_MaxSPInitial.Value = cfg.Default.MaxSPInitial;
            intud_Default_MaxSPFinal.Value = cfg.Default.MaxSPFinal;

            // Apply the strengh
            intud_Default_StrInitial.Value = cfg.Default.StrInitial;
            intud_Default_StrFinal.Value = cfg.Default.StrFinal;

            // Apply the dexterity
            intud_Default_DexInitial.Value = cfg.Default.DexInitial;
            intud_Default_DexFinal.Value = cfg.Default.DexFinal;

            // Apply the agility
            intud_Default_AgiInitial.Value = cfg.Default.AgiInitial;
            intud_Default_AgiFinal.Value = cfg.Default.AgiFinal;

            // Apply the intelligence
            intud_Default_IntInitial.Value = cfg.Default.IntInitial;
            intud_Default_IntFinal.Value = cfg.Default.IntFinal;

            #endregion

            #region Parameter Rate

            // Apply the strengh rate
            decud_Default_StrRate.Value = cfg.Default.StrRate;

            // Apply the dexterity rate
            decud_Default_DexRate.Value = cfg.Default.DexRate;

            // Apply the agility rate
            decud_Default_AgiRate.Value = cfg.Default.AgiRate;

            // Apply the intelligence rate
            decud_Default_IntRate.Value = cfg.Default.IntRate;

            // Apply the physical defense rate
            decud_Default_PDefRate.Value = cfg.Default.PDefRate;

            // Apply the magical defense rate
            decud_Default_MDefRate.Value = cfg.Default.MDefRate;

            // Apply the guard rate
            decud_Default_GuardRate.Value = cfg.Default.GuardRate;

            // Apply the evasion rate
            decud_Default_EvaRate.Value = cfg.Default.EvaRate;

            #endregion

            #region Defense against Attack Critical

            // Apply the defense against attack critical rate
            decud_Default_DefCritRate.Value = cfg.Default.DefCritRate;

            // Apply the defense against attack critical damage
            decud_Default_DefCritDamage.Value = cfg.Default.DefCritDamage;

            // Apply the defense against attack special critical rate
            decud_Default_DefSpCritRate.Value = cfg.Default.DefSpCritRate;

            // Apply the defense against attack special critical damage
            decud_Default_DefSpCritDamage.Value = cfg.Default.DefSpCritDamage;

            #endregion

            #region Defense against Skill Critical

            // Apply the defense against skill critical rate
            decud_Default_DefSkillCritRate.Value = cfg.Default.DefSkillCritRate;

            // Apply the defense against skill critical damage
            decud_Default_DefSkillCritDamage.Value = cfg.Default.DefSkillCritDamage;

            // Apply the defense against skill special critical rate
            decud_Default_DefSkillSpCritRate.Value = cfg.Default.DefSkillSpCritRate;

            // Apply the defense against skill special critical damage
            decud_Default_DefSkillSpCritDamage.Value = cfg.Default.DefSkillSpCritDamage;

            #endregion

            #region Attack

            // Apply the attack
            intud_Default_AtkInitial.Value = cfg.Default.AtkInitial;
            intud_Default_AtkFinal.Value = cfg.Default.AtkFinal;

            // Apply the hit rate
            decud_Default_HitInitial.Value = cfg.Default.HitInitial;
            decud_Default_HitFinal.Value = cfg.Default.HitFinal;

            // Apply unarmed attack animation
            combo_Default_AnimCaster.SelectedIndex = cfg.Default.AnimCaster;
            combo_Default_AnimTarget.SelectedIndex = cfg.Default.AnimTarget;

            // Apply the parameter attack force
            decud_Default_StrForce.Value = cfg.Default.StrForce;
            decud_Default_DexForce.Value = cfg.Default.DexForce;
            decud_Default_AgiForce.Value = cfg.Default.AgiForce;
            decud_Default_IntForce.Value = cfg.Default.IntForce;

            // Apply the defense attack force
            decud_Default_PDefForce.Value = cfg.Default.PDefForce;
            decud_Default_MDefForce.Value = cfg.Default.MDefForce;

            #endregion

            #region Critical

            // Apply the critcal rate
            decud_Default_CritRate.Value = cfg.Default.CritRate;

            // Apply the critcal damage
            decud_Default_CritDamage.Value = cfg.Default.CritDamage;

            // Apply the critcal guard rate reduction
            decud_Default_CritDefGuard.Value = cfg.Default.CritDefGuard;

            // Apply the critcal evasion rate reduction
            decud_Default_CritDefEva.Value = cfg.Default.CritDefEva;

            #endregion

            #region Special Critical

            // Apply the special critcal rate
            decud_Default_SpCritRate.Value = cfg.Default.SpCritRate;

            // Apply the special critcal damage
            decud_Default_SpCritDamage.Value = cfg.Default.SpCritDamage;

            // Apply the special critcal guard rate reduction
            decud_Default_SpCritDefGuard.Value = cfg.Default.SpCritDefGuard;

            // Apply the special critcal evasion rate reduction
            decud_Default_SpCritDefEva.Value = cfg.Default.SpCritDefEva;

            #endregion

            #region Defense

            // Apply the physical defense
            intud_Default_PDefInitial.IsEnabled = true;
            intud_Default_PDefInitial.Value = cfg.Default.PDefInitial;

            // Apply the magical defense
            intud_Default_MDefInitial.IsEnabled = true;
            intud_Default_MDefInitial.Value = cfg.Default.MDefInitial;

            // Apply the physical defense
            intud_Default_PDefFinal.Value = cfg.Default.PDefFinal;

            // Apply the magical defense
            intud_Default_MDefFinal.Value = cfg.Default.MDefFinal;

            #endregion

            // Remove the updating flag
            updating = false;
        }

        #endregion

        #region Actor

        #region Set Data

        private void setActorData(DataPackActor actor)
        {
            // Apply the actor family name
            txt_Actor_Name.Text = actor.Name;

            #region In Family

            // Create the list of actor in the family
            actorInFamily.Clear();
            if (actor.ActorFamily.Count > 0)
            {
                foreach (Actor actors in actor.ActorFamily)
                {
                    actorInFamily.Add(new Actor(actors.ID, actors.Name));
                }
                actorInFamily.Sort((x, y) => x.ID.CompareTo(y.ID));
            }

            actorAvailable.Clear();
            if (cfg.ActorAvailable.Count > 0)
            {
                foreach (Actor actors in cfg.ActorAvailable)
                {
                    actorAvailable.Add(new Actor(actors.ID, actors.Name));
                }
                actorAvailable.Sort((x, y) => x.ID.CompareTo(y.ID));
            }

            #endregion

            #region Equipment

            // Create the equip name and id list
            check_Actor_CustomEquip.IsChecked = actor.CustomEquip;
            actorEquipType.Clear();
            actorEquipList.Clear();

            // Create the equipment name list
            if (actor.CustomEquip)
            {
                if (actor.EquipType.Count > 0)
                {
                    // Update the equipment type list
                    foreach (EquipType equip in actor.EquipType)
                    {
                        actorEquipType.Add(new EquipType(equip.ID, equip.Name));
                    }
                    actorEquipType.Sort((x, y) => x.ID.CompareTo(y.ID));

                    // Update the equipment type name box
                    if (lastIndexActorEquipType >= 5)
                    {
                        list_Actor_EquipType.SelectedIndex = lastIndexActorEquipType;
                        txt_Actor_NameEquipType.IsEnabled = true;
                        txt_Actor_NameEquipType.Text = actorEquipType[lastIndexActorEquipType].Name;
                    }
                    else
                    {
                        list_Actor_EquipType.SelectedIndex = lastIndexActorEquipType;
                        txt_Actor_NameEquipType.IsEnabled = false;
                        txt_Actor_NameEquipType.Text = "";
                    }
                }
                else
                {
                    list_Actor_EquipType.SelectedIndex = -1;
                    txt_Actor_NameEquipType.IsEnabled = false;
                    txt_Actor_NameEquipType.Text = "";
                }

                // Create the equipment id list
                if (actor.EquipList.Count > 0)
                {
                    foreach (EquipType equip in actor.EquipList)
                    {
                        if (equip.ID <= actor.EquipType.Count - 1)
                        {
                            actorEquipList.Add(new EquipType(equip.ID, equip.Name));
                        }
                    }

                    // Update the equipment id name box
                    list_Actor_EquipList.SelectedIndex = lastIndexActorEquipList;
                    txt_Actor_NameEquipList.IsEnabled = true;
                    txt_Actor_NameEquipList.Text = actorEquipList[lastIndexActorEquipList].Name;
                }
                else
                {
                    list_Actor_EquipList.SelectedIndex = -1;
                    txt_Actor_NameEquipList.IsEnabled = false;
                    txt_Actor_NameEquipList.Text = "";
                }
            }
            else
            {
                list_Actor_EquipType.SelectedIndex = -1;
                txt_Actor_NameEquipType.IsEnabled = false;
                txt_Actor_NameEquipType.Text = "";

                list_Actor_EquipList.SelectedIndex = -1;
                txt_Actor_NameEquipList.IsEnabled = false;
                txt_Actor_NameEquipList.Text = "";
            }

            // Apply dual hold
            check_Actor_DualHold.IsChecked = actor.DualHold;
            if (actor.DualHold)
            {
                check_Actor_DualHoldName.IsChecked = actor.CustomDualHoldName;
                // Apply dual hold name
                if (actor.CustomDualHoldName)
                {
                    txt_Actor_DualHoldNameWeapon.Text = actor.DualHoldNameWeapon;
                    txt_Actor_DualHoldNameShield.Text = actor.DualHoldNameShield;
                }
                else
                {
                    txt_Actor_DualHoldNameWeapon.Text = "";
                    txt_Actor_DualHoldNameShield.Text = "";
                }

                // Apply dual hold multiplier
                check_Actor_DualHoldMul.IsChecked = actor.CustomDualHoldMul;
                if (actor.CustomDualHoldMul)
                {
                    decud_Actor_DualHoldMulWeapon.Value = actor.DualHoldMulWeapon;
                    decud_Actor_DualHoldMulShield.Value = actor.DualHoldMulShield;
                }
                else
                {
                    decud_Actor_DualHoldMulWeapon.Text = "";
                    decud_Actor_DualHoldMulShield.Text = "";
                }

                // Apply shield bypass
                check_Actor_ShieldBypass.IsChecked = actor.ShieldBypass;
                if (actor.ShieldBypass)
                {
                    decud_Actor_ShieldBypass.Value = actor.ShieldBypassMul;
                }
                else
                {
                    decud_Actor_ShieldBypass.Text = "";
                }
            }
            else
            {
                // Apply empty dual hold
                check_Actor_DualHold.IsChecked = false;

                // Apply empty dual hold name
                check_Actor_DualHoldName.IsChecked = false;
                txt_Actor_DualHoldNameWeapon.Text = "";
                txt_Actor_DualHoldNameShield.Text = "";

                // Apply empty dual hold multiplier
                check_Actor_DualHoldMul.IsChecked = false;
                decud_Actor_DualHoldMulWeapon.Text = "";
                decud_Actor_DualHoldMulShield.Text = "";

                // Apply empty shield bypass
                check_Actor_ShieldBypass.IsChecked = false;
                decud_Actor_ShieldBypass.Text = "";
            }

            // Apply weapon bypass
            check_Actor_WeaponBypass.IsChecked = actor.WeaponBypass;
            if (actor.WeaponBypass)
            {
                decud_Actor_WeaponBypass.Value = actor.WeaponBypassMul;
            }
            else
            {
                decud_Actor_WeaponBypass.Text = "";
            }

            // Apply reduce hand
            check_Actor_ReduceHand.IsChecked = actor.CustomReduceHand;
            if (actor.CustomReduceHand)
            {
                decud_Actor_ReduceHand.Value = actor.ReduceHand;
                decud_Actor_ReduceHandMul.Value = actor.ReduceHandMul;
            }
            else
            {
                decud_Actor_ReduceHand.Text = "";
                decud_Actor_ReduceHandMul.Text = "";
            }

            #endregion

            #region Parameter

            // Apply the maximum HP
            check_Actor_MaxHP.IsChecked = actor.CustomMaxHP;
            if (actor.CustomMaxHP == true)
            {
                intud_Actor_MaxHPInitial.Value = actor.MaxHPInitial;
                intud_Actor_MaxHPFinal.Value = actor.MaxHPFinal;
            }
            else
            {
                intud_Actor_MaxHPInitial.Text = "";
                intud_Actor_MaxHPFinal.Text = "";
            }

            // Apply the maximum SP
            check_Actor_MaxSP.IsChecked = actor.CustomMaxSP;
            if (actor.CustomMaxSP == true)
            {
                intud_Actor_MaxSPInitial.Value = actor.MaxSPInitial;
                intud_Actor_MaxSPFinal.Value = actor.MaxSPFinal;
            }
            else
            {
                intud_Actor_MaxSPInitial.Text = "";
                intud_Actor_MaxSPFinal.Text = "";
            }

            // Apply the strengh
            check_Actor_Str.IsChecked = actor.CustomStr;
            if (actor.CustomStr)
            {
                intud_Actor_StrInitial.Value = actor.StrInitial;
                intud_Actor_StrFinal.Value = actor.StrFinal;
            }
            else
            {
                intud_Actor_StrInitial.Text = "";
                intud_Actor_StrFinal.Text = "";
            }

            // Apply the dexterity
            check_Actor_Dex.IsChecked = actor.CustomDex;
            if (actor.CustomDex)
            {
                intud_Actor_DexInitial.Value = actor.DexInitial;
                intud_Actor_DexFinal.Value = actor.DexFinal;
            }
            else
            {
                intud_Actor_DexInitial.Text = "";
                intud_Actor_DexFinal.Text = "";
            }

            // Apply the agility
            check_Actor_Agi.IsChecked = actor.CustomAgi;
            if (actor.CustomAgi)
            {
                intud_Actor_AgiInitial.Value = actor.AgiInitial;
                intud_Actor_AgiFinal.Value = actor.AgiFinal;
            }
            else
            {
                intud_Actor_AgiInitial.Text = "";
                intud_Actor_AgiFinal.Text = "";
            }

            // Apply the intelligence
            check_Actor_Int.IsChecked = actor.CustomInt;
            if (actor.CustomInt)
            {
                intud_Actor_IntInitial.Value = actor.IntInitial;
                intud_Actor_IntFinal.Value = actor.IntFinal;
            }
            else
            {
                intud_Actor_IntInitial.Text = "";
                intud_Actor_IntFinal.Text = "";
            }

            #endregion

            #region Parameter Rate

            // Apply the strengh rate
            check_Actor_StrRate.IsChecked = actor.CustomStrRate;
            if (actor.CustomStrRate)
            {
                decud_Actor_StrRate.Value = actor.StrRate;
            }
            else
            {
                decud_Actor_StrRate.Text = "";
            }

            // Apply the dexterity rate
            check_Actor_DexRate.IsChecked = actor.CustomDexRate;
            if (actor.CustomDexRate)
            {
                decud_Actor_DexRate.Value = actor.DexRate;
            }
            else
            {
                decud_Actor_DexRate.Text = "";
            }

            // Apply the agility rate
            check_Actor_AgiRate.IsChecked = actor.CustomAgiRate;
            if (actor.CustomAgiRate)
            {
                decud_Actor_AgiRate.Value = actor.AgiRate;
            }
            else
            {
                decud_Actor_AgiRate.Text = "";
            }

            // Apply the intelligence rate
            check_Actor_IntRate.IsChecked = actor.CustomIntRate;
            if (actor.CustomIntRate)
            {
                decud_Actor_IntRate.Value = actor.IntRate;
            }
            else
            {
                decud_Actor_IntRate.Text = "";
            }

            // Apply the physical defense rate
            check_Actor_PDefRate.IsChecked = actor.CustomPDefRate;
            if (actor.CustomPDefRate)
            {
                decud_Actor_PDefRate.Value = actor.PDefRate;
            }
            else
            {
                decud_Actor_PDefRate.Text = "";
            }

            // Apply the magical defense rate
            check_Actor_MDefRate.IsChecked = actor.CustomMDefRate;
            if (actor.CustomMDefRate)
            {
                decud_Actor_MDefRate.Value = actor.MDefRate;
            }
            else
            {
                decud_Actor_MDefRate.Text = "";
            }

            // Apply the guard rate
            check_Actor_GuardRate.IsChecked = actor.CustomGuardRate;
            if (actor.CustomGuardRate)
            {
                decud_Actor_GuardRate.Value = actor.GuardRate;
            }
            else
            {
                decud_Actor_GuardRate.Text = "";
            }

            // Apply the evasion rate
            check_Actor_EvaRate.IsChecked = actor.CustomEvaRate;
            if (actor.CustomEvaRate)
            {
                decud_Actor_EvaRate.Value = actor.EvaRate;
            }
            else
            {
                decud_Actor_EvaRate.Text = "";
            }

            #endregion

            #region Defense against Attack Critical

            // Apply the defense against attack critical rate
            check_Actor_DefCritRate.IsChecked = actor.CustomDefCritRate;
            if (actor.CustomDefCritRate)
            {
                decud_Actor_DefCritRate.Value = actor.DefCritRate;
            }
            else
            {
                decud_Actor_DefCritRate.Text = "";
            }

            // Apply the defense against attack critical damage
            check_Actor_DefCritDamage.IsChecked = actor.CustomDefCritDamage;
            if (actor.CustomDefCritDamage)
            {
                decud_Actor_DefCritDamage.Value = actor.DefCritDamage;
            }
            else
            {
                decud_Actor_DefCritDamage.Text = "";
            }

            // Apply the defense against attack special critical rate
            check_Actor_DefSpCritRate.IsChecked = actor.CustomDefSpCritRate;
            if (actor.CustomDefSpCritRate)
            {
                decud_Actor_DefSpCritRate.Value = actor.DefSpCritRate;
            }
            else
            {
                decud_Actor_DefSpCritRate.Text = "";
            }

            // Apply the defense against attack special critical damage
            check_Actor_DefSpCritDamage.IsChecked = actor.CustomDefSpCritDamage;
            if (actor.CustomDefSpCritDamage)
            {
                decud_Actor_DefSpCritDamage.Value = actor.DefSpCritDamage;
            }
            else
            {
                decud_Actor_DefSpCritDamage.Text = "";
            }

            #endregion

            #region Defense against Skill Critical

            // Apply the defense against skill critical rate
            check_Actor_DefSkillCritRate.IsChecked = actor.CustomDefSkillCritRate;
            if (actor.CustomDefSkillCritRate)
            {
                decud_Actor_DefSkillCritRate.Value = actor.DefSkillCritRate;
            }
            else
            {
                decud_Actor_DefSkillCritRate.Text = "";
            }

            // Apply the defense against skill critical damage
            check_Actor_DefSkillCritDamage.IsChecked = actor.CustomDefSkillCritDamage;
            if (actor.CustomDefSkillCritDamage)
            {
                decud_Actor_DefSkillCritDamage.Value = actor.DefSkillCritDamage;
            }
            else
            {
                decud_Actor_DefSkillCritDamage.Text = "";
            }

            // Apply the defense against skill special critical rate
            check_Actor_DefSkillSpCritRate.IsChecked = actor.CustomDefSkillSpCritRate;
            if (actor.CustomDefSkillSpCritRate)
            {
                decud_Actor_DefSkillSpCritRate.Value = actor.DefSkillSpCritRate;
            }
            else
            {
                decud_Actor_DefSkillSpCritRate.Text = "";
            }

            // Apply the defense against skill special critical damage
            check_Actor_DefSkillSpCritDamage.IsChecked = actor.CustomDefSkillSpCritDamage;
            if (actor.CustomDefSkillSpCritDamage)
            {
                decud_Actor_DefSkillSpCritDamage.Value = actor.DefSkillSpCritDamage;
            }
            else
            {
                decud_Actor_DefSkillSpCritDamage.Text = "";
            }

            #endregion

            #region Unarmed Attack

            // Apply the unarmed attack
            check_Actor_Atk.IsChecked = actor.CustomAtk;
            if (actor.CustomAtk)
            {
                intud_Actor_AtkInitial.Value = actor.AtkInitial;
                intud_Actor_AtkFinal.Value = actor.AtkFinal;
            }
            else
            {
                intud_Actor_AtkInitial.Text = "";
                intud_Actor_AtkFinal.Text = "";
            }

            // Apply the unarmed hit rate
            check_Actor_Hit.IsChecked = actor.CustomHit;
            if (actor.CustomHit)
            {
                decud_Actor_HitInitial.Value = actor.HitInitial;
                decud_Actor_HitFinal.Value = actor.HitFinal;
            }
            else
            {
                decud_Actor_HitInitial.Text = "";
                decud_Actor_HitFinal.Text = "";
            }

            // Apply the unarmed attack animation
            check_Actor_Anim.IsChecked = actor.CustomAnim;
            if (actor.CustomAnim)
            {
                combo_Actor_AnimCaster.SelectedIndex = actor.AnimCaster;
                combo_Actor_AnimTarget.SelectedIndex = actor.AnimTarget;
            }
            else
            {
                combo_Actor_AnimCaster.SelectedIndex = 0;
                combo_Actor_AnimTarget.SelectedIndex = 0;
            }

            // Apply the unarmed parameter attack force
            check_Actor_ParamForce.IsChecked = actor.CustomParamForce;
            if (actor.CustomParamForce)
            {
                decud_Actor_StrForce.Value = actor.StrForce;
                decud_Actor_DexForce.Value = actor.DexForce;
                decud_Actor_AgiForce.Value = actor.AgiForce;
                decud_Actor_IntForce.Value = actor.IntForce;
            }
            else
            {
                decud_Actor_StrForce.Text = "";
                decud_Actor_DexForce.Text = "";
                decud_Actor_AgiForce.Text = "";
                decud_Actor_IntForce.Text = "";
            }

            // Apply the unarmed defense attack force
            check_Actor_DefenseForce.IsChecked = actor.CustomDefenseForce;
            if (actor.CustomDefenseForce)
            {
                decud_Actor_PDefForce.Value = actor.PDefForce;
                decud_Actor_MDefForce.Value = actor.MDefForce;
            }
            else
            {
                decud_Actor_PDefForce.Text = "";
                decud_Actor_MDefForce.Text = "";
            }

            #endregion

            #region Unarmed Critical

            // Apply the unarmed critcal rate
            check_Actor_CritRate.IsChecked = actor.CustomCritRate;
            if (actor.CustomCritRate)
            {
                decud_Actor_CritRate.Value = actor.CritRate;
            }
            else
            {
                decud_Actor_CritRate.Text = "";
            }

            // Apply the unarmed critcal damage
            check_Actor_CritDamage.IsChecked = actor.CustomCritDamage;
            if (actor.CustomCritDamage)
            {
                decud_Actor_CritDamage.Value = actor.CritDamage;
            }
            else
            {
                decud_Actor_CritDamage.Text = "";
            }

            // Apply the unarmed critcal guard rate reduction
            check_Actor_CritDefGuard.IsChecked = actor.CustomCritDefGuard;
            if (actor.CustomCritDefGuard)
            {
                decud_Actor_CritDefGuard.Value = actor.CritDefGuard;
            }
            else
            {
                decud_Actor_CritDefGuard.Text = "";
            }

            // Apply the unarmed critcal evasion rate reduction
            check_Actor_CritDefEva.IsChecked = actor.CustomCritDefEva;
            if (actor.CustomCritDefEva)
            {
                decud_Actor_CritDefEva.Value = actor.CritDefEva;
            }
            else
            {
                decud_Actor_CritDefEva.Text = "";
            }

            #endregion

            #region Unarmed Special Critical

            // Apply the unarmed special critcal rate
            check_Actor_SpCritRate.IsChecked = actor.CustomSpCritRate;
            if (actor.CustomSpCritRate)
            {
                decud_Actor_SpCritRate.Value = actor.SpCritRate;
            }
            else
            {
                decud_Actor_SpCritRate.Text = "";
            }

            // Apply the unarmed special critcal damage
            check_Actor_SpCritDamage.IsChecked = actor.CustomSpCritDamage;
            if (actor.CustomSpCritDamage)
            {
                decud_Actor_SpCritDamage.Value = actor.SpCritDamage;
            }
            else
            {
                decud_Actor_SpCritDamage.Text = "";
            }

            // Apply the unarmed special critcal guard rate reduction
            check_Actor_SpCritDefGuard.IsChecked = actor.CustomSpCritDefGuard;
            if (actor.CustomSpCritDefGuard)
            {
                decud_Actor_SpCritDefGuard.Value = actor.SpCritDefGuard;
            }
            else
            {
                decud_Actor_SpCritDefGuard.Text = "";
            }

            // Apply the unarmed special critcal evasion rate reduction
            check_Actor_SpCritDefEva.IsChecked = actor.CustomSpCritDefEva;
            if (actor.CustomSpCritDefEva)
            {
                decud_Actor_SpCritDefEva.Value = actor.SpCritDefEva;
            }
            else
            {
                decud_Actor_SpCritDefEva.Text = "";
            }

            #endregion

            #region Unarmoured Defense

            // Apply the unarmoured physical defense
            check_Actor_PDef.IsChecked = actor.CustomPDef;
            if (actor.CustomPDef)
            {
                intud_Actor_PDefInitial.Value = actor.PDefInitial;
                intud_Actor_PDefFinal.Value = actor.PDefFinal;
            }
            else
            {
                intud_Actor_PDefInitial.Text = "";
                intud_Actor_PDefFinal.Text = "";
            }

            // Apply the unarmoured magical defense
            check_Actor_MDef.IsChecked = actor.CustomMDef;
            if (actor.CustomMDef)
            {
                intud_Actor_MDefInitial.Value = actor.MDefInitial;
                intud_Actor_MDefFinal.Value = actor.MDefFinal;
            }
            else
            {
                intud_Actor_MDefInitial.Text = "";
                intud_Actor_MDefFinal.Text = "";
            }

            #endregion
        }

        #endregion

        #region Apply Empty Data

        private void applyEmptyActor()
        {
            // Set the updating flag
            updating = true;

            // Disable the family configuration
            exp_Actor_Family.IsEnabled = false;
            exp_Actor_Family.IsExpanded = false;

            // Apply the empty actor family name
            txt_Actor_Name.Text = "";

            #region In Family

            // In family
            actorInFamily.Clear();

            actorAvailable.Clear();
            if (cfg.ActorAvailable.Count > 0)
            {
                foreach (Actor actor in cfg.ActorAvailable)
                {
                    actorAvailable.Add(new Actor(actor.ID, actor.Name));
                }
                actorAvailable.Sort((x, y) => x.ID.CompareTo(y.ID));
            }

            #endregion

            #region Equipment

            // Create the empty equip name and id list
            check_Actor_CustomEquip.IsChecked = false;
            actorEquipType.Clear();
            actorEquipList.Clear();

            // Apply empty dual hold
            check_Actor_DualHold.IsChecked = false;

            // Apply empty dual hold name
            check_Actor_DualHoldName.IsChecked = false;
            txt_Actor_DualHoldNameWeapon.Text = "";
            txt_Actor_DualHoldNameShield.Text = "";

            // Apply empty dual hold multiplier
            check_Actor_DualHoldMul.IsChecked = false;
            decud_Actor_DualHoldMulWeapon.Text = "";
            decud_Actor_DualHoldMulShield.Text = "";

            // Apply empty shield bypass
            check_Actor_ShieldBypass.IsChecked = false;
            decud_Actor_ShieldBypass.Text = "";

            // Apply empty weapon bypass
            check_Actor_WeaponBypass.IsChecked = false;
            decud_Actor_WeaponBypass.Text = "";

            // Apply empty reduce hand
            check_Actor_ReduceHand.IsChecked = false;
            decud_Actor_ReduceHand.Text = "";
            decud_Actor_ReduceHandMul.Text = "";

            #endregion

            #region Parameter

            // Apply the empty maximum HP
            check_Actor_MaxHP.IsChecked = false;
            intud_Actor_MaxHPInitial.Text = "";
            intud_Actor_MaxHPFinal.Text = "";

            // Apply the empty maximum SP
            check_Actor_MaxSP.IsChecked = false;
            intud_Actor_MaxSPInitial.Text = "";
            intud_Actor_MaxSPFinal.Text = "";

            // Apply the empty strengh
            check_Actor_Str.IsChecked = false;
            intud_Actor_StrInitial.Text = "";
            intud_Actor_StrFinal.Text = "";

            // Apply the empty dexterity
            check_Actor_Dex.IsChecked = false;
            intud_Actor_DexInitial.Text = "";
            intud_Actor_DexFinal.Text = "";

            // Apply the empty agility
            check_Actor_Agi.IsChecked = false;
            intud_Actor_AgiInitial.Text = "";
            intud_Actor_AgiFinal.Text = "";

            // Apply the empty intelligence
            check_Actor_Int.IsChecked = false;
            intud_Actor_IntInitial.Text = "";
            intud_Actor_IntFinal.Text = "";

            #endregion

            #region Parameter Rate

            // Apply the empty strengh rate
            check_Actor_StrRate.IsChecked = false;
            decud_Actor_StrRate.Text = "";

            // Apply the empty dexterity rate
            check_Actor_DexRate.IsChecked = false;
            decud_Actor_DexRate.Text = "";

            // Apply the empty agility rate
            check_Actor_AgiRate.IsChecked = false;
            decud_Actor_AgiRate.Text = "";

            // Apply the empty intelligence rate
            check_Actor_IntRate.IsChecked = false;
            decud_Actor_IntRate.Text = "";

            // Apply the empty physical defense rate
            check_Actor_PDefRate.IsChecked = false;
            decud_Actor_PDefRate.Text = "";

            // Apply the empty magical defense rate
            check_Actor_MDefRate.IsChecked = false;
            decud_Actor_MDefRate.Text = "";

            // Apply the empty guard rate
            check_Actor_GuardRate.IsChecked = false;
            decud_Actor_GuardRate.Text = "";

            // Apply the empty evasion rate
            check_Actor_EvaRate.IsChecked = false;
            decud_Actor_EvaRate.Text = "";

            #endregion

            #region Defense against Attack Critical

            // Apply the defense against attack critical rate
            check_Actor_DefCritRate.IsChecked = false;
            decud_Actor_DefCritRate.Text = "";

            // Apply the defense against attack critical damage
            check_Actor_DefCritDamage.IsChecked = false;
            decud_Actor_DefCritDamage.Text = "";

            // Apply the defense against attack special critical rate
            check_Actor_DefSpCritRate.IsChecked = false;
            decud_Actor_DefSpCritRate.Text = "";

            // Apply the defense against attack special critical damage
            check_Actor_DefSpCritDamage.IsChecked = false;
            decud_Actor_DefSpCritDamage.Text = "";

            #endregion

            #region Defense against Skill Critical

            // Apply the defense against skill critical rate
            check_Actor_DefSkillCritRate.IsChecked = false;
            decud_Actor_DefSkillCritRate.Text = "";

            // Apply the defense against skill critical damage
            check_Actor_DefSkillCritDamage.IsChecked = false;
            decud_Actor_DefSkillCritDamage.Text = "";

            // Apply the defense against skill special critical rate
            check_Actor_DefSkillSpCritRate.IsChecked = false;
            decud_Actor_DefSkillSpCritRate.Text = "";

            // Apply the defense against skill special critical damage
            check_Actor_DefSkillSpCritDamage.IsChecked = false;
            decud_Actor_DefSkillSpCritDamage.Text = "";

            #endregion

            #region Unarmed Attack

            // Apply the empty unarmed attack
            check_Actor_Atk.IsChecked = false;
            intud_Actor_AtkInitial.Text = "";
            intud_Actor_AtkFinal.Text = "";

            // Apply the empty unarmed hit rate
            check_Actor_Hit.IsChecked = false;
            decud_Actor_HitInitial.Text = "";
            decud_Actor_HitFinal.Text = "";

            // Apply the empty unarmed attack animation
            check_Actor_Anim.IsChecked = false;
            combo_Actor_AnimCaster.SelectedIndex = 0;
            combo_Actor_AnimTarget.SelectedIndex = 0;

            // Apply the empty unarmed parameter attack force
            check_Actor_ParamForce.IsChecked = false;
            decud_Actor_StrForce.Text = "";
            decud_Actor_DexForce.Text = "";
            decud_Actor_AgiForce.Text = "";
            decud_Actor_IntForce.Text = "";

            // Apply the empty unarmed defense attack force
            check_Actor_DefenseForce.IsChecked = false;
            decud_Actor_PDefForce.Text = "";
            decud_Actor_MDefForce.Text = "";

            #endregion

            #region Unarmed Critical

            // Apply the unarmed critcal rate
            check_Actor_CritRate.IsChecked = false;
            decud_Actor_CritRate.Text = "";

            // Apply the unarmed critcal damage
            check_Actor_CritDamage.IsChecked = false;
            decud_Actor_CritDamage.Text = "";

            // Apply the unarmed critcal guard rate reduction
            check_Actor_CritDefGuard.IsChecked = false;
            decud_Actor_CritDefGuard.Text = "";

            // Apply the unarmed critcal evasion rate reduction
            check_Actor_CritDefEva.IsChecked = false;
            decud_Actor_CritDefEva.Text = "";

            #endregion

            #region Unarmed Special Critical

            // Apply the unarmed special critcal rate
            check_Actor_SpCritRate.IsChecked = false;
            decud_Actor_SpCritRate.Text = "";

            // Apply the unarmed special critcal damage
            check_Actor_SpCritDamage.IsChecked = false;
            decud_Actor_SpCritDamage.Text = "";

            // Apply the unarmed special critcal guard rate reduction
            check_Actor_SpCritDefGuard.IsChecked = false;
            decud_Actor_SpCritDefGuard.Text = "";

            // Apply the unarmed special critcal evasion rate reduction
            check_Actor_SpCritDefEva.IsChecked = false;
            decud_Actor_SpCritDefEva.Text = "";

            #endregion

            #region Unarmoured Defense

            // Apply the empty unarmoured physical defense
            check_Actor_PDef.IsChecked = false;
            intud_Actor_PDefInitial.Text = "";
            intud_Actor_PDefFinal.Text = "";

            // Apply the empty unarmoured magical defense
            check_Actor_MDef.IsChecked = false;
            intud_Actor_MDefInitial.Text = "";
            intud_Actor_MDefFinal.Text = "";

            #endregion

            // Remove the updating Flag
            updating = false;
        }

        #endregion

        #region Apply Family Data

        public void applyActorFamily(int index)
        {
            // Set the updating flag
            updating = true;

            // Load the new actor parameter
            if (tree_Actor_IsFamily() && index > 0 && index <= cfg.ActorFamily.Count && cfg.ActorFamily.Count > 0)
            {
                exp_Actor_Family.IsEnabled = true;
                setActorData(cfg.ActorFamily[index - 1]);
            }
            else
            {
                // Empty the actor parameter
                applyEmptyActor();
            }

            // Remove the updating Flag
            updating = false;
        }

        private void applyActorFamily()
        {
            applyActorFamily(tree_Actor_ID());
        }

        #endregion

        #region Apply Individual Data

        public void applyActorID(int index)
        {
            // Set the updating flag
            updating = true;

            // Load the new actor parameter
            if (tree_Actor_IsIndividual() && index > 0 && index <= cfg.ActorID.Count && cfg.ActorID.Count > 0)
            {
                exp_Actor_Family.IsEnabled = false;
                exp_Actor_Family.IsExpanded = false;
                setActorData(cfg.ActorID[index - 1]);
            }
            else
            {
                // Empty the actor parameter
                applyEmptyActor();
            }

            // Remove the updating Flag
            updating = false;
        }

        private void applyActorID()
        {
            applyActorID(tree_Actor_ID());
        }

        #endregion

        #region Apply Default Data

        public void applyActorDefault()
        {
            // Set the updating flag
            updating = true;

            // Load the new actor parameter
            if (tree_Actor_Default.IsSelected)
            {
                exp_Actor_Family.IsEnabled = false;
                exp_Actor_Family.IsExpanded = false;
                setActorData(cfg.ActorDefault);
            }
            else
            {
                // Empty the actor parameter
                applyEmptyActor();
            }

            // Remove the updating Flag
            updating = false;
        }

        #endregion

        #endregion

        #region Class

        #region Set Data

        private void setClassData(DataPackClass classes)
        {
            // Apply the class family name
            txt_Class_Name.Text = classes.Name;

            #region In Family

            // Create the list of class in the family
            classInFamily.Clear();
            if (classes.ClassFamily.Count > 0)
            {
                foreach (Class classF in classes.ClassFamily)
                {
                    classInFamily.Add(new Class(classF.ID, classF.Name));
                }
                classInFamily.Sort((x, y) => x.ID.CompareTo(y.ID));
            }

            classAvailable.Clear();
            if (cfg.ClassAvailable.Count > 0)
            {
                foreach (Class classF in cfg.ClassAvailable)
                {
                    classAvailable.Add(new Class(classF.ID, classF.Name));
                }
                classAvailable.Sort((x, y) => x.ID.CompareTo(y.ID));
            }

            #endregion

            #region Equipment

            // Create the equip name and id list
            check_Class_CustomEquip.IsChecked = classes.CustomEquip;
            classEquipType.Clear();
            classEquipList.Clear();
            if (classes.CustomEquip)
            {
                if (classes.EquipType.Count > 0)
                {
                    foreach (EquipType equip in classes.EquipType)
                    {
                        classEquipType.Add(new EquipType(equip.ID, equip.Name));
                    }
                    classEquipType.Sort((x, y) => x.ID.CompareTo(y.ID));
                }
                if (classes.EquipList.Count > 0)
                {
                    foreach (EquipType equip in classes.EquipList)
                    {
                        if (equip.ID <= classes.EquipType.Count - 1)
                        {
                            classEquipList.Add(new EquipType(equip.ID, equip.Name));
                        }
                    }
                }
            }

            // Apply dual hold
            check_Class_DualHold.IsChecked = classes.DualHold;
            if (classes.DualHold)
            {
                check_Class_DualHoldName.IsChecked = classes.CustomDualHoldName;
                // Apply dual hold name
                if (classes.CustomDualHoldName)
                {
                    txt_Class_DualHoldNameWeapon.Text = classes.DualHoldNameWeapon;
                    txt_Class_DualHoldNameShield.Text = classes.DualHoldNameShield;
                }
                else
                {
                    txt_Class_DualHoldNameWeapon.Text = "";
                    txt_Class_DualHoldNameShield.Text = "";
                }

                // Apply dual hold multiplier
                check_Class_DualHoldMul.IsChecked = classes.CustomDualHoldMul;
                if (classes.CustomDualHoldMul)
                {
                    decud_Class_DualHoldMulWeapon.Value = classes.DualHoldMulWeapon;
                    decud_Class_DualHoldMulShield.Value = classes.DualHoldMulShield;
                }
                else
                {
                    decud_Class_DualHoldMulWeapon.Text = "";
                    decud_Class_DualHoldMulShield.Text = "";
                }

                // Apply shield bypass
                check_Class_ShieldBypass.IsChecked = classes.ShieldBypass;
                if (classes.ShieldBypass)
                {
                    decud_Class_ShieldBypass.Value = classes.ShieldBypassMul;
                }
                else
                {
                    decud_Class_ShieldBypass.Text = "";
                }
            }
            else
            {
                // Apply empty dual hold
                check_Class_DualHold.IsChecked = false;

                // Apply empty dual hold name
                check_Class_DualHoldName.IsChecked = false;
                txt_Class_DualHoldNameWeapon.Text = "";
                txt_Class_DualHoldNameShield.Text = "";

                // Apply empty dual hold multiplier
                check_Class_DualHoldMul.IsChecked = false;
                decud_Class_DualHoldMulWeapon.Text = "";
                decud_Class_DualHoldMulShield.Text = "";

                // Apply empty shield bypass
                check_Class_ShieldBypass.IsChecked = false;
                decud_Class_ShieldBypass.Text = "";
            }

            // Apply weapon bypass
            check_Class_WeaponBypass.IsChecked = classes.WeaponBypass;
            if (classes.WeaponBypass)
            {
                decud_Class_WeaponBypass.Value = classes.WeaponBypassMul;
            }
            else
            {
                decud_Class_WeaponBypass.Text = "";
            }

            // Apply reduce hand
            check_Class_ReduceHand.IsChecked = classes.CustomReduceHand;
            if (classes.CustomReduceHand)
            {
                decud_Class_ReduceHand.Value = classes.ReduceHand;
                decud_Class_ReduceHandMul.Value = classes.ReduceHandMul;
            }
            else
            {
                decud_Class_ReduceHand.Text = "";
                decud_Class_ReduceHandMul.Text = "";
            }

            #endregion

            #region Parameter

            // Apply the maximum HP
            check_Class_MaxHP.IsChecked = classes.CustomMaxHP;
            if (classes.CustomMaxHP == true)
            {
                decud_Class_MaxHPMul.Value = classes.MaxHPMul;
                intud_Class_MaxHPAdd.Value = classes.MaxHPAdd;
            }
            else
            {
                decud_Class_MaxHPMul.Text = "";
                intud_Class_MaxHPAdd.Text = "";
            }

            // Apply the maximum SP
            check_Class_MaxSP.IsChecked = classes.CustomMaxSP;
            if (classes.CustomMaxSP == true)
            {
                decud_Class_MaxSPMul.Value = classes.MaxSPMul;
                intud_Class_MaxSPAdd.Value = classes.MaxSPAdd;
            }
            else
            {
                decud_Class_MaxSPMul.Text = "";
                intud_Class_MaxSPAdd.Text = "";
            }

            // Apply the strengh
            check_Class_Str.IsChecked = classes.CustomStr;
            if (classes.CustomStr)
            {
                decud_Class_StrMul.Value = classes.StrMul;
                intud_Class_StrAdd.Value = classes.StrAdd;
            }
            else
            {
                decud_Class_StrMul.Text = "";
                intud_Class_StrAdd.Text = "";
            }

            // Apply the dexterity
            check_Class_Dex.IsChecked = classes.CustomDex;
            if (classes.CustomDex)
            {
                decud_Class_DexMul.Value = classes.DexMul;
                intud_Class_DexAdd.Value = classes.DexAdd;
            }
            else
            {
                decud_Class_DexMul.Text = "";
                intud_Class_DexAdd.Text = "";
            }

            // Apply the agility
            check_Class_Agi.IsChecked = classes.CustomAgi;
            if (classes.CustomAgi)
            {
                decud_Class_AgiMul.Value = classes.AgiMul;
                intud_Class_AgiAdd.Value = classes.AgiAdd;
            }
            else
            {
                decud_Class_AgiMul.Text = "";
                intud_Class_AgiAdd.Text = "";
            }

            // Apply the intelligence
            check_Class_Int.IsChecked = classes.CustomInt;
            if (classes.CustomInt)
            {
                decud_Class_IntMul.Value = classes.IntMul;
                intud_Class_IntAdd.Value = classes.IntAdd;
            }
            else
            {
                decud_Class_IntMul.Text = "";
                intud_Class_IntAdd.Text = "";
            }

            #endregion

            #region Parameter Rate

            // Apply the strengh rate
            check_Class_StrRate.IsChecked = classes.CustomStrRate;
            if (classes.CustomStrRate)
            {
                decud_Class_StrRateMul.Value = classes.StrRateMul;
                intud_Class_StrRateAdd.Value = classes.StrRateAdd;
            }
            else
            {
                decud_Class_StrRateMul.Text = "";
                intud_Class_StrRateAdd.Text = "";
            }

            // Apply the dexterity rate
            check_Class_DexRate.IsChecked = classes.CustomDexRate;
            if (classes.CustomDexRate)
            {
                decud_Class_DexRateMul.Value = classes.DexRateMul;
                intud_Class_DexRateAdd.Value = classes.DexRateAdd;
            }
            else
            {
                decud_Class_DexRateMul.Text = "";
                intud_Class_DexRateAdd.Text = "";
            }

            // Apply the agility rate
            check_Class_AgiRate.IsChecked = classes.CustomAgiRate;
            if (classes.CustomAgiRate)
            {
                decud_Class_AgiRateMul.Value = classes.AgiRateMul;
                intud_Class_AgiRateAdd.Value = classes.AgiRateAdd;
            }
            else
            {
                decud_Class_AgiRateMul.Text = "";
                intud_Class_AgiRateAdd.Text = "";
            }

            // Apply the intelligence rate
            check_Class_IntRate.IsChecked = classes.CustomIntRate;
            if (classes.CustomIntRate)
            {
                decud_Class_IntRateMul.Value = classes.IntRateMul;
                intud_Class_IntRateAdd.Value = classes.IntRateAdd;
            }
            else
            {
                decud_Class_IntRateMul.Text = "";
                intud_Class_IntRateAdd.Text = "";
            }

            // Apply the physical defense rate
            check_Class_PDefRate.IsChecked = classes.CustomPDefRate;
            if (classes.CustomPDefRate)
            {
                decud_Class_PDefRateMul.Value = classes.PDefRateMul;
                intud_Class_PDefRateAdd.Value = classes.PDefRateAdd;
            }
            else
            {
                decud_Class_PDefRateMul.Text = "";
                intud_Class_PDefRateAdd.Text = "";
            }

            // Apply the magical defense rate
            check_Class_MDefRate.IsChecked = classes.CustomMDefRate;
            if (classes.CustomMDefRate)
            {
                decud_Class_MDefRateMul.Value = classes.MDefRateMul;
                intud_Class_MDefRateAdd.Value = classes.MDefRateAdd;
            }
            else
            {
                decud_Class_MDefRateMul.Text = "";
                intud_Class_MDefRateAdd.Text = "";
            }

            // Apply the guard rate
            check_Class_GuardRate.IsChecked = classes.CustomGuardRate;
            if (classes.CustomGuardRate)
            {
                decud_Class_GuardRateMul.Value = classes.GuardRateMul;
                intud_Class_GuardRateAdd.Value = classes.GuardRateAdd;
            }
            else
            {
                decud_Class_GuardRateMul.Text = "";
                intud_Class_GuardRateAdd.Text = "";
            }

            // Apply the evasion rate
            check_Class_EvaRate.IsChecked = classes.CustomEvaRate;
            if (classes.CustomEvaRate)
            {
                decud_Class_EvaRateMul.Value = classes.EvaRateMul;
                intud_Class_EvaRateAdd.Value = classes.EvaRateAdd;
            }
            else
            {
                decud_Class_EvaRateMul.Text = "";
                intud_Class_EvaRateAdd.Text = "";
            }

            #endregion

            #region Defense against Attack Critical

            // Apply the defense against attack critical rate
            check_Class_DefCritRate.IsChecked = classes.CustomDefCritRate;
            if (classes.CustomDefCritRate)
            {
                decud_Class_DefCritRateMul.Value = classes.DefCritRateMul;
                intud_Class_DefCritRateAdd.Value = classes.DefCritRateAdd;
            }
            else
            {
                decud_Class_DefCritRateMul.Text = "";
                intud_Class_DefCritRateAdd.Text = "";
            }

            // Apply the defense against attack critical damage
            check_Class_DefCritDamage.IsChecked = classes.CustomDefCritDamage;
            if (classes.CustomDefCritDamage)
            {
                decud_Class_DefCritDamageMul.Value = classes.DefCritDamageMul;
                intud_Class_DefCritDamageAdd.Value = classes.DefCritDamageAdd;
            }
            else
            {
                decud_Class_DefCritDamageMul.Text = "";
                intud_Class_DefCritDamageAdd.Text = "";
            }

            // Apply the defense against attack critical rate
            check_Class_DefSpCritRate.IsChecked = classes.CustomDefSpCritRate;
            if (classes.CustomDefSpCritRate)
            {
                decud_Class_DefSpCritRateMul.Value = classes.DefSpCritRateMul;
                intud_Class_DefSpCritRateAdd.Value = classes.DefSpCritRateAdd;
            }
            else
            {
                decud_Class_DefSpCritRateMul.Text = "";
                intud_Class_DefSpCritRateAdd.Text = "";
            }

            // Apply the defense against attack critical damage
            check_Class_DefSpCritDamage.IsChecked = classes.CustomDefSpCritDamage;
            if (classes.CustomDefSpCritDamage)
            {
                decud_Class_DefSpCritDamageMul.Value = classes.DefSpCritDamageMul;
                intud_Class_DefSpCritDamageAdd.Value = classes.DefSpCritDamageAdd;
            }
            else
            {
                decud_Class_DefSpCritDamageMul.Text = "";
                intud_Class_DefSpCritDamageAdd.Text = "";
            }

            #endregion

            #region Defense against Skill Critical

            // Apply the defense against skill critical rate
            check_Class_DefSkillCritRate.IsChecked = classes.CustomDefSkillCritRate;
            if (classes.CustomDefSkillCritRate)
            {
                decud_Class_DefSkillCritRateMul.Value = classes.DefSkillCritRateMul;
                intud_Class_DefSkillCritRateAdd.Value = classes.DefSkillCritRateAdd;
            }
            else
            {
                decud_Class_DefSkillCritRateMul.Text = "";
                intud_Class_DefSkillCritRateAdd.Text = "";
            }

            // Apply the defense against attack critical damage
            check_Class_DefSkillCritDamage.IsChecked = classes.CustomDefSkillCritDamage;
            if (classes.CustomDefSkillCritDamage)
            {
                decud_Class_DefSkillCritDamageMul.Value = classes.DefSkillCritDamageMul;
                intud_Class_DefSkillCritDamageAdd.Value = classes.DefSkillCritDamageAdd;
            }
            else
            {
                decud_Class_DefSkillCritDamageMul.Text = "";
                intud_Class_DefSkillCritDamageAdd.Text = "";
            }

            // Apply the defense against attack critical rate
            check_Class_DefSkillSpCritRate.IsChecked = classes.CustomDefSkillSpCritRate;
            if (classes.CustomDefSkillSpCritRate)
            {
                decud_Class_DefSkillSpCritRateMul.Value = classes.DefSkillSpCritRateMul;
                intud_Class_DefSkillSpCritRateAdd.Value = classes.DefSkillSpCritRateAdd;
            }
            else
            {
                decud_Class_DefSkillSpCritRateMul.Text = "";
                intud_Class_DefSkillSpCritRateAdd.Text = "";
            }

            // Apply the defense against attack critical damage
            check_Class_DefSkillSpCritDamage.IsChecked = classes.CustomDefSkillSpCritDamage;
            if (classes.CustomDefSkillSpCritDamage)
            {
                decud_Class_DefSkillSpCritDamageMul.Value = classes.DefSkillSpCritDamageMul;
                intud_Class_DefSkillSpCritDamageAdd.Value = classes.DefSkillSpCritDamageAdd;
            }
            else
            {
                decud_Class_DefSkillSpCritDamageMul.Text = "";
                intud_Class_DefSkillSpCritDamageAdd.Text = "";
            }

            #endregion

            #region Passive attack

            // Apply the attack
            check_Class_PassiveAtk.IsChecked = classes.CustomPassiveAtk;
            if (classes.CustomPassiveAtk)
            {
                decud_Class_AtkMul.Value = classes.AtkMul;
                intud_Class_AtkAdd.Value = classes.AtkAdd;
            }
            else
            {
                decud_Class_AtkMul.Text = "";
                intud_Class_AtkAdd.Text = "";
            }

            // Apply the hit rate
            check_Class_PassiveHit.IsChecked = classes.CustomPassiveHit;
            if (classes.CustomPassiveHit)
            {
                decud_Class_HitMul.Value = classes.HitMul;
                intud_Class_HitAdd.Value = classes.HitAdd;
            }
            else
            {
                decud_Class_HitMul.Text = "";
                intud_Class_HitAdd.Text = "";
            }

            #endregion

            #region Passive critical

            // Apply the critcal rate
            check_Class_PassiveCritRate.IsChecked = classes.CustomPassiveCritRate;
            if (classes.CustomPassiveCritRate)
            {
                decud_Class_CritRateMul.Value = classes.CritRateMul;
                intud_Class_CritRateAdd.Value = classes.CritRateAdd;
            }
            else
            {
                decud_Class_CritRateMul.Text = "";
                intud_Class_CritRateAdd.Text = "";
            }

            // Apply the critcal damage
            check_Class_PassiveCritDamage.IsChecked = classes.CustomPassiveCritDamage;
            if (classes.CustomPassiveCritDamage)
            {
                decud_Class_CritDamageMul.Value = classes.CritDamageMul;
                intud_Class_CritDamageAdd.Value = classes.CritDamageAdd;
            }
            else
            {
                decud_Class_CritDamageMul.Text = "";
                intud_Class_CritDamageAdd.Text = "";
            }

            // Apply the critcal guard rate reduction
            check_Class_PassiveCritDefGuard.IsChecked = classes.CustomPassiveCritDefGuard;
            if (classes.CustomPassiveCritDefGuard)
            {
                decud_Class_CritDefGuardMul.Value = classes.CritDefGuardMul;
                intud_Class_CritDefGuardAdd.Value = classes.CritDefGuardAdd;
            }
            else
            {
                decud_Class_CritDefGuardMul.Text = "";
                intud_Class_CritDefGuardAdd.Text = "";
            }

            // Apply the critcal evasion rate reduction
            check_Class_PassiveCritDefEva.IsChecked = classes.CustomPassiveCritDefEva;
            if (classes.CustomPassiveCritDefEva)
            {
                decud_Class_CritDefEvaMul.Value = classes.CritDefEvaMul;
                intud_Class_CritDefEvaAdd.Value = classes.CritDefEvaAdd;
            }
            else
            {
                decud_Class_CritDefEvaMul.Text = "";
                intud_Class_CritDefEvaAdd.Text = "";
            }

            #endregion

            #region Passive special critical

            // Apply the special critcal rate
            check_Class_PassiveSpCritRate.IsChecked = classes.CustomPassiveSpCritRate;
            if (classes.CustomPassiveSpCritRate)
            {
                decud_Class_SpCritRateMul.Value = classes.SpCritRateMul;
                intud_Class_SpCritRateAdd.Value = classes.SpCritRateAdd;
            }
            else
            {
                decud_Class_SpCritRateMul.Text = "";
                intud_Class_SpCritRateAdd.Text = "";
            }

            // Apply the special critcal damage
            check_Class_PassiveSpCritDamage.IsChecked = classes.CustomPassiveSpCritDamage;
            if (classes.CustomPassiveSpCritDamage)
            {
                decud_Class_SpCritDamageMul.Value = classes.SpCritDamageMul;
                intud_Class_SpCritDamageAdd.Value = classes.SpCritDamageAdd;
            }
            else
            {
                decud_Class_SpCritDamageMul.Text = "";
                intud_Class_SpCritDamageAdd.Text = "";
            }

            // Apply the special critcal guard rate reduction
            check_Class_PassiveSpCritDefGuard.IsChecked = classes.CustomPassiveSpCritDefGuard;
            if (classes.CustomPassiveSpCritDefGuard)
            {
                decud_Class_SpCritDefGuardMul.Value = classes.SpCritDefGuardMul;
                intud_Class_SpCritDefGuardAdd.Value = classes.SpCritDefGuardAdd;
            }
            else
            {
                decud_Class_SpCritDefGuardMul.Text = "";
                intud_Class_SpCritDefGuardAdd.Text = "";
            }

            // Apply the special critcal evasion rate reduction
            check_Class_PassiveSpCritDefEva.IsChecked = classes.CustomPassiveSpCritDefEva;
            if (classes.CustomPassiveSpCritDefEva)
            {
                decud_Class_SpCritDefEvaMul.Value = classes.SpCritDefEvaMul;
                intud_Class_SpCritDefEvaAdd.Value = classes.SpCritDefEvaAdd;
            }
            else
            {
                decud_Class_SpCritDefEvaMul.Text = "";
                intud_Class_SpCritDefEvaAdd.Text = "";
            }

            #endregion

            #region Passive defense

            // Apply the physical defense
            check_Class_PassivePDef.IsChecked = classes.CustomPassivePDef;
            if (classes.CustomPassivePDef)
            {
                decud_Class_PDefMul.Value = classes.PDefMul;
                intud_Class_PDefAdd.Value = classes.PDefAdd;
            }
            else
            {
                decud_Class_PDefMul.Text = "";
                intud_Class_PDefAdd.Text = "";
            }

            // Apply the magical defense
            check_Class_PassiveMDef.IsChecked = classes.CustomPassiveMDef;
            if (classes.CustomPassiveMDef)
            {
                decud_Class_MDefMul.Value = classes.MDefMul;
                intud_Class_MDefAdd.Value = classes.MDefAdd;
            }
            else
            {
                decud_Class_MDefMul.Text = "";
                intud_Class_MDefAdd.Text = "";
            }

            #endregion

            #region Unarmed attack

            // Apply the unarmed attack
            check_Class_UnarmedAtk.IsChecked = classes.CustomUnarmedAtk;
            if (classes.CustomUnarmedAtk)
            {
                intud_Class_AtkInitial.Value = classes.AtkInitial;
                intud_Class_AtkFinal.Value = classes.AtkFinal;
            }
            else
            {
                intud_Class_AtkInitial.Text = "";
                intud_Class_AtkFinal.Text = "";
            }

            // Apply the unarmed hit rate
            check_Class_UnarmedHit.IsChecked = classes.CustomUnarmedHit;
            if (classes.CustomUnarmedHit)
            {
                decud_Class_HitInitial.Value = classes.HitInitial;
                decud_Class_HitFinal.Value = classes.HitFinal;
            }
            else
            {
                decud_Class_HitInitial.Text = "";
                decud_Class_HitFinal.Text = "";
            }

            // Apply the unarmed attack animation
            check_Class_UnarmedAnim.IsChecked = classes.CustomUnarmedAnim;
            if (classes.CustomUnarmedAnim)
            {
                combo_Class_AnimCaster.SelectedIndex = classes.AnimCaster;
                combo_Class_AnimTarget.SelectedIndex = classes.AnimTarget;
            }
            else
            {
                combo_Class_AnimCaster.SelectedIndex = 0;
                combo_Class_AnimTarget.SelectedIndex = 0;
            }

            // Apply the unarmed parameter attack force
            check_Class_UnarmedParamForce.IsChecked = classes.CustomUnarmedParamForce;
            if (classes.CustomUnarmedParamForce)
            {
                decud_Class_StrForce.Value = classes.StrForce;
                decud_Class_DexForce.Value = classes.DexForce;
                decud_Class_AgiForce.Value = classes.AgiForce;
                decud_Class_IntForce.Value = classes.IntForce;
            }
            else
            {
                decud_Class_StrForce.Text = "";
                decud_Class_DexForce.Text = "";
                decud_Class_AgiForce.Text = "";
                decud_Class_IntForce.Text = "";
            }

            // Apply the unarmed defense attack force
            check_Class_UnarmedDefenseForce.IsChecked = classes.CustomUnarmedDefenseForce;
            if (classes.CustomUnarmedDefenseForce)
            {
                decud_Class_PDefForce.Value = classes.PDefForce;
                decud_Class_MDefForce.Value = classes.MDefForce;
            }
            else
            {
                decud_Class_PDefForce.Text = "";
                decud_Class_MDefForce.Text = "";
            }

            #endregion

            #region Unarmed critical

            // Apply the unarmed critcal rate
            check_Class_UnarmedCritRate.IsChecked = classes.CustomUnarmedCritRate;
            if (classes.CustomUnarmedCritRate)
            {
                decud_Class_CritRate.Value = classes.CritRate;
            }
            else
            {
                decud_Class_CritRate.Text = "";
            }

            // Apply the unarmed critcal damage
            check_Class_UnarmedCritDamage.IsChecked = classes.CustomUnarmedCritDamage;
            if (classes.CustomUnarmedCritDamage)
            {
                decud_Class_CritDamage.Value = classes.CritDamage;
            }
            else
            {
                decud_Class_CritDamage.Text = "";
            }

            // Apply the unarmed critcal guard rate reduction
            check_Class_UnarmedCritDefGuard.IsChecked = classes.CustomUnarmedCritDefGuard;
            if (classes.CustomUnarmedCritDefGuard)
            {
                decud_Class_CritDefGuard.Value = classes.CritDefGuard;
            }
            else
            {
                decud_Class_CritDefGuard.Text = "";
            }

            // Apply the unarmed critcal evasion rate reduction
            check_Class_UnarmedCritDefEva.IsChecked = classes.CustomUnarmedCritDefEva;
            if (classes.CustomUnarmedCritDefEva)
            {
                decud_Class_CritDefEva.Value = classes.CritDefEva;
            }
            else
            {
                decud_Class_CritDefEva.Text = "";
            }

            #endregion

            #region Unarmed Special Critical

            // Apply the unarmed special critcal rate
            check_Class_UnarmedSpCritRate.IsChecked = classes.CustomUnarmedSpCritRate;
            if (classes.CustomUnarmedSpCritRate)
            {
                decud_Class_SpCritRate.Value = classes.SpCritRate;
            }
            else
            {
                decud_Class_SpCritRate.Text = "";
            }

            // Apply the unarmed special critcal damage
            check_Class_UnarmedSpCritDamage.IsChecked = classes.CustomUnarmedSpCritDamage;
            if (classes.CustomUnarmedSpCritDamage)
            {
                decud_Class_SpCritDamage.Value = classes.SpCritDamage;
            }
            else
            {
                decud_Class_SpCritDamage.Text = "";
            }

            // Apply the unarmed special critcal guard rate reduction
            check_Class_UnarmedSpCritDefGuard.IsChecked = classes.CustomUnarmedSpCritDefGuard;
            if (classes.CustomUnarmedSpCritDefGuard)
            {
                decud_Class_SpCritDefGuard.Value = classes.SpCritDefGuard;
            }
            else
            {
                decud_Class_SpCritDefGuard.Text = "";
            }

            // Apply the unarmed special critcal evasion rate reduction
            check_Class_UnarmedSpCritDefEva.IsChecked = classes.CustomUnarmedSpCritDefEva;
            if (classes.CustomUnarmedSpCritDefEva)
            {
                decud_Class_SpCritDefEva.Value = classes.SpCritDefEva;
            }
            else
            {
                decud_Class_SpCritDefEva.Text = "";
            }

            #endregion

            #region Unarmoured Defense

            // Apply the unarmoured physical defense
            check_Class_UnarmouredPDef.IsChecked = classes.CustomUnarmouredPDef;
            if (classes.CustomUnarmouredPDef)
            {
                intud_Class_PDefInitial.Value = classes.PDefInitial;
                intud_Class_PDefFinal.Value = classes.PDefFinal;
            }
            else
            {
                intud_Class_PDefInitial.Text = "";
                intud_Class_PDefFinal.Text = "";
            }

            // Apply the unarmoured magical defense
            check_Class_UnarmouredMDef.IsChecked = classes.CustomUnarmouredMDef;
            if (classes.CustomUnarmouredMDef)
            {
                intud_Class_MDefInitial.Value = classes.MDefInitial;
                intud_Class_MDefFinal.Value = classes.MDefFinal;
            }
            else
            {
                intud_Class_MDefInitial.Text = "";
                intud_Class_MDefFinal.Text = "";
            }

            #endregion

            #region Unarmoured Defense

            // Apply the empty unarmoured physical defense
            check_Class_UnarmouredPDef.IsChecked = false;
            intud_Class_PDefInitial.Text = "";
            intud_Class_PDefFinal.Text = "";

            // Apply the empty unarmoured magical defense
            check_Class_UnarmouredMDef.IsChecked = false;
            intud_Class_MDefInitial.Text = "";
            intud_Class_MDefFinal.Text = "";

            #endregion
        }

        #endregion

        #region Apply Empty Data

        private void applyEmptyClass()
        {
            // Set the updating flag
            updating = true;

            // Disable the family configuration
            exp_Class_Family.IsEnabled = false;
            exp_Class_Family.IsExpanded = false;

            // Apply the empty class family name
            txt_Class_Name.Text = "";

            #region In Family

            // In family
            classInFamily.Clear();

            classAvailable.Clear();
            if (cfg.ClassAvailable.Count > 0)
            {
                foreach (Class classes in cfg.ClassAvailable)
                {
                    classAvailable.Add(new Class(classes.ID, classes.Name));
                }
                classAvailable.Sort((x, y) => x.ID.CompareTo(y.ID));
            }

            #endregion

            #region Equipment

            // Create the empty equip name and id list
            check_Class_CustomEquip.IsChecked = false;
            classEquipType.Clear();
            classEquipList.Clear();

            // Apply empty dual hold
            check_Class_DualHold.IsChecked = false;

            // Apply empty dual hold name
            check_Class_DualHoldName.IsChecked = false;
            txt_Class_DualHoldNameWeapon.Text = "";
            txt_Class_DualHoldNameShield.Text = "";

            // Apply empty dual hold multiplier
            check_Class_DualHoldMul.IsChecked = false;
            decud_Class_DualHoldMulWeapon.Text = "";
            decud_Class_DualHoldMulShield.Text = "";

            // Apply empty shield bypass
            check_Class_ShieldBypass.IsChecked = false;
            decud_Class_ShieldBypass.Text = "";

            // Apply empty weapon bypass
            check_Class_WeaponBypass.IsChecked = false;
            decud_Class_WeaponBypass.Text = "";

            // Apply empty reduce hand
            check_Class_ReduceHand.IsChecked = false;
            decud_Class_ReduceHand.Text = "";
            decud_Class_ReduceHandMul.Text = "";

            #endregion

            #region Parameter

            // Apply the empty maximum HP
            check_Class_MaxHP.IsChecked = false;
            decud_Class_MaxHPMul.Text = "";
            intud_Class_MaxHPAdd.Text = "";

            // Apply the empty maximum SP
            check_Class_MaxSP.IsChecked = false;
            decud_Class_MaxSPMul.Text = "";
            intud_Class_MaxSPAdd.Text = "";

            // Apply the empty strengh
            check_Class_Str.IsChecked = false;
            decud_Class_StrMul.Text = "";
            intud_Class_StrAdd.Text = "";

            // Apply the empty dexterity
            check_Class_Dex.IsChecked = false;
            decud_Class_DexMul.Text = "";
            intud_Class_DexAdd.Text = "";

            // Apply the empty agility
            check_Class_Agi.IsChecked = false;
            decud_Class_AgiMul.Text = "";
            intud_Class_AgiAdd.Text = "";

            // Apply the empty intelligence
            check_Class_Int.IsChecked = false;
            decud_Class_IntMul.Text = "";
            intud_Class_IntAdd.Text = "";

            // Apply the empty guard rate
            check_Class_GuardRate.IsChecked = false;
            decud_Class_GuardRateMul.Text = "";
            intud_Class_GuardRateAdd.Text = "";

            // Apply the empty evasion rate
            check_Class_EvaRate.IsChecked = false;
            decud_Class_EvaRateMul.Text = "";
            intud_Class_EvaRateAdd.Text = "";

            #endregion

            #region Parameter Rate

            // Apply the empty strengh rate
            check_Class_StrRate.IsChecked = false;
            decud_Class_StrRateMul.Text = "";
            intud_Class_StrRateAdd.Text = "";

            // Apply the empty dexterity rate
            check_Class_DexRate.IsChecked = false;
            decud_Class_DexRateMul.Text = "";
            intud_Class_DexRateAdd.Text = "";

            // Apply the empty agility rate
            check_Class_AgiRate.IsChecked = false;
            decud_Class_AgiRateMul.Text = "";
            intud_Class_AgiRateAdd.Text = "";

            // Apply the empty intelligence rate
            check_Class_IntRate.IsChecked = false;
            decud_Class_IntRateMul.Text = "";
            intud_Class_IntRateAdd.Text = "";

            // Apply the empty physical defense rate
            check_Class_PDefRate.IsChecked = false;
            decud_Class_PDefRateMul.Text = "";
            intud_Class_PDefRateAdd.Text = "";

            // Apply the empty magical defense rate
            check_Class_MDefRate.IsChecked = false;
            decud_Class_MDefRateMul.Text = "";
            intud_Class_MDefRateAdd.Text = "";

            #endregion

            #region Defense against Attack Critical

            // Apply the defense against attack critical rate
            check_Class_DefCritRate.IsChecked = false;
            decud_Class_DefCritRateMul.Text = "";
            intud_Class_DefCritRateAdd.Text = "";

            // Apply the defense against attack critical damage
            check_Class_DefCritDamage.IsChecked = false;
            decud_Class_DefCritDamageMul.Text = "";
            intud_Class_DefCritDamageAdd.Text = "";

            // Apply the defense against attack critical rate
            check_Class_DefSpCritRate.IsChecked = false;
            decud_Class_DefSpCritRateMul.Text = "";
            intud_Class_DefSpCritRateAdd.Text = "";

            // Apply the defense against attack critical damage
            check_Class_DefSpCritDamage.IsChecked = false;
            decud_Class_DefSpCritDamageMul.Text = "";
            intud_Class_DefSpCritDamageAdd.Text = "";

            #endregion

            #region Defense against Skill Critical

            // Apply the defense against skill critical rate
            check_Class_DefSkillCritRate.IsChecked = false;
            decud_Class_DefSkillCritRateMul.Text = "";
            intud_Class_DefSkillCritRateAdd.Text = "";

            // Apply the defense against attack critical damage
            check_Class_DefSkillCritDamage.IsChecked = false;
            decud_Class_DefSkillCritDamageMul.Text = "";
            intud_Class_DefSkillCritDamageAdd.Text = "";

            // Apply the defense against attack critical rate
            check_Class_DefSkillSpCritRate.IsChecked = false;
            decud_Class_DefSkillSpCritRateMul.Text = "";
            intud_Class_DefSkillSpCritRateAdd.Text = "";

            // Apply the defense against attack critical damage
            check_Class_DefSkillSpCritDamage.IsChecked = false;
            decud_Class_DefSkillSpCritDamageMul.Text = "";
            intud_Class_DefSkillSpCritDamageAdd.Text = "";

            #endregion

            #region Passive attack

            // Apply the empty attack
            check_Class_PassiveAtk.IsChecked = false;
            decud_Class_AtkMul.Text = "";
            intud_Class_AtkAdd.Text = "";

            // Apply the empty hit rate
            check_Class_PassiveHit.IsChecked = false;
            decud_Class_HitMul.Text = "";
            intud_Class_HitAdd.Text = "";

            #endregion

            #region Passive critical

            // Apply the critcal rate
            check_Class_PassiveCritRate.IsChecked = false;
            decud_Class_CritRateMul.Text = "";
            intud_Class_CritRateAdd.Text = "";

            // Apply the critcal damage
            check_Class_PassiveCritDamage.IsChecked = false;
            decud_Class_CritDamageMul.Text = "";
            intud_Class_CritDamageAdd.Text = "";

            // Apply the critcal guard rate reduction
            check_Class_PassiveCritDefGuard.IsChecked = false;
            decud_Class_CritDefGuardMul.Text = "";
            intud_Class_CritDefGuardAdd.Text = "";

            // Apply the critcal evasion rate reduction
            check_Class_PassiveCritDefEva.IsChecked = false;
            decud_Class_CritDefEvaMul.Text = "";
            intud_Class_CritDefEvaAdd.Text = "";

            #endregion

            #region Passive special critical

            // Apply the special critcal rate
            check_Class_PassiveSpCritRate.IsChecked = false;
            decud_Class_SpCritRateMul.Text = "";
            intud_Class_SpCritRateAdd.Text = "";

            // Apply the special critcal damage
            check_Class_PassiveSpCritDamage.IsChecked = false;
            decud_Class_SpCritDamageMul.Text = "";
            intud_Class_SpCritDamageAdd.Text = "";

            // Apply the special critcal guard rate reduction
            check_Class_PassiveSpCritDefGuard.IsChecked = false;
            decud_Class_SpCritDefGuardMul.Text = "";
            intud_Class_SpCritDefGuardAdd.Text = "";

            // Apply the special critcal evasion rate reduction
            check_Class_PassiveSpCritDefEva.IsChecked = false;
            decud_Class_SpCritDefEvaMul.Text = "";
            intud_Class_SpCritDefEvaAdd.Text = "";

            #endregion

            #region Passive defense

            // Apply the empty physical defense
            check_Class_PassivePDef.IsChecked = false;
            decud_Class_PDefMul.Text = "";
            intud_Class_PDefAdd.Text = "";

            // Apply the empty magical defense
            check_Class_PassiveMDef.IsChecked = false;
            decud_Class_MDefMul.Text = "";
            intud_Class_MDefAdd.Text = "";

            #endregion

            #region Unarmed Attack

            // Apply the empty unarmed attack
            check_Class_UnarmedAtk.IsChecked = false;
            intud_Class_AtkInitial.Text = "";
            intud_Class_AtkFinal.Text = "";

            // Apply the empty unarmed hit rate
            check_Class_UnarmedHit.IsChecked = false;
            decud_Class_HitInitial.Text = "";
            decud_Class_HitFinal.Text = "";

            // Apply the empty unarmed attack animation
            check_Class_UnarmedAnim.IsChecked = false;
            combo_Class_AnimCaster.SelectedIndex = 0;
            combo_Class_AnimTarget.SelectedIndex = 0;

            // Apply the empty unarmed parameter attack force
            check_Class_UnarmedParamForce.IsChecked = false;
            decud_Class_StrForce.Text = "";
            decud_Class_DexForce.Text = "";
            decud_Class_AgiForce.Text = "";
            decud_Class_IntForce.Text = "";

            // Apply the empty unarmed defense attack force
            check_Class_UnarmedDefenseForce.IsChecked = false;
            decud_Class_PDefForce.Text = "";
            decud_Class_MDefForce.Text = "";

            #endregion

            #region Unarmed Critical

            // Apply the unarmed critcal rate
            check_Class_UnarmedCritRate.IsChecked = false;
            decud_Class_CritRate.Text = "";

            // Apply the unarmed critcal damage
            check_Class_UnarmedCritDamage.IsChecked = false;
            decud_Class_CritDamage.Text = "";

            // Apply the unarmed critcal guard rate reduction
            check_Class_UnarmedCritDefGuard.IsChecked = false;
            decud_Class_CritDefGuard.Text = "";

            // Apply the unarmed critcal evasion rate reduction
            check_Class_UnarmedCritDefEva.IsChecked = false;
            decud_Class_CritDefEva.Text = "";

            #endregion

            #region Unarmed Special Critical

            // Apply the unarmed special critcal rate
            check_Class_UnarmedSpCritRate.IsChecked = false;
            decud_Class_SpCritRate.Text = "";

            // Apply the unarmed special critcal damage
            check_Class_UnarmedSpCritDamage.IsChecked = false;
            decud_Class_SpCritDamage.Text = "";

            // Apply the unarmed special critcal guard rate reduction
            check_Class_UnarmedSpCritDefGuard.IsChecked = false;
            decud_Class_SpCritDefGuard.Text = "";

            // Apply the unarmed special critcal evasion rate reduction
            check_Class_UnarmedSpCritDefEva.IsChecked = false;
            decud_Class_SpCritDefEva.Text = "";

            #endregion

            // Remove the updating Flag
            updating = false;
        }

        #endregion

        #region Apply Family Data

        public void applyClassFamily(int index)
        {
            // Set the updating flag
            updating = true;

            // Load the new class parameter
            if (tree_Class_IsFamily() && index > 0 && index <= cfg.ClassFamily.Count && cfg.ClassFamily.Count > 0)
            {
                exp_Class_Family.IsEnabled = true;
                setClassData(cfg.ClassFamily[index - 1]);
            }
            else
            {
                // Empty the class parameter
                applyEmptyClass();
            }

            // Remove the updating Flag
            updating = false;
        }

        private void applyClassFamily()
        {
            applyClassFamily(tree_Class_ID());
        }

        #endregion

        #region Apply Individual Data

        public void applyClassID(int index)
        {
            // Set the updating flag
            updating = true;

            // Load the new class parameter
            if (tree_Class_IsIndividual() && index > 0 && index <= cfg.ClassID.Count && cfg.ClassID.Count > 0)
            {
                exp_Class_Family.IsEnabled = false;
                exp_Class_Family.IsExpanded = false;
                setClassData(cfg.ClassID[index - 1]);
            }
            else
            {
                // Empty the class parameter
                applyEmptyClass();
            }

            // Remove the updating Flag
            updating = false;
        }

        private void applyClassID()
        {
            applyClassID(tree_Class_ID());
        }

        #endregion

        #region Apply Default Data

        public void applyClassDefault()
        {
            // Set the updating flag
            updating = true;

            // Load the new class parameter
            if (tree_Class_Default.IsSelected)
            {
                exp_Class_Family.IsEnabled = false;
                exp_Class_Family.IsExpanded = false;
                setClassData(cfg.ClassDefault);
            }
            else
            {
                // Empty the class parameter
                applyEmptyClass();
            }

            // Remove the updating Flag
            updating = false;
        }

        #endregion

        #endregion

        #region Skill

        #region Set Data

        private void setSkillData(DataPackSkill skill)
        {
            // Apply the skill family name
            txt_Skill_Name.Text = skill.Name;

            #region In Family

            // Create the list of skill in the family
            skillInFamily.Clear();
            if (skill.SkillFamily.Count > 0)
            {
                foreach (Skill skills in skill.SkillFamily)
                {
                    skillInFamily.Add(new Skill(skills.ID, skills.Name));
                }
                skillInFamily.Sort((x, y) => x.ID.CompareTo(y.ID));
            }

            skillAvailable.Clear();
            if (cfg.SkillAvailable.Count > 0)
            {
                foreach (Skill skills in cfg.SkillAvailable)
                {
                    skillAvailable.Add(new Skill(skills.ID, skills.Name));
                }
                skillAvailable.Sort((x, y) => x.ID.CompareTo(y.ID));
            }

            #endregion

            #region Attack

            // Apply the attack
            check_Skill_Atk.IsChecked = skill.CustomAtk;
            if (skill.CustomAtk)
            {
                intud_Skill_AtkInitial.Value = skill.AtkInitial;
            }
            else
            {
                intud_Skill_AtkInitial.Text = "";
            }

            // Apply the hit rate
            check_Skill_Hit.IsChecked = skill.CustomHit;
            if (skill.CustomHit)
            {
                decud_Skill_HitInitial.Value = skill.HitInitial;
            }
            else
            {
                decud_Skill_HitInitial.Text = "";
            }

            // Apply the strengh attack force
            check_Skill_ParamForce.IsChecked = skill.CustomParamForce;
            if (skill.CustomParamForce)
            {
                decud_Skill_StrForce.Value = skill.StrForce;
                decud_Skill_DexForce.Value = skill.DexForce;
                decud_Skill_AgiForce.Value = skill.AgiForce;
                decud_Skill_IntForce.Value = skill.IntForce;
            }
            else
            {
                decud_Skill_StrForce.Text = "";
                decud_Skill_DexForce.Text = "";
                decud_Skill_AgiForce.Text = "";
                decud_Skill_IntForce.Text = "";
            }

            // Apply the defense attack force
            check_Skill_DefenseForce.IsChecked = skill.CustomDefenseForce;
            if (skill.CustomDefenseForce)
            {
                decud_Skill_PDefForce.Value = skill.PDefForce;
                decud_Skill_MDefForce.Value = skill.MDefForce;
            }
            else
            {
                decud_Skill_PDefForce.Text = "";
                decud_Skill_MDefForce.Text = "";
            }

            #endregion

            #region Critical

            // Apply the critcal rate
            check_Skill_CritRate.IsChecked = skill.CustomCritRate;
            if (skill.CustomCritRate)
            {
                decud_Skill_CritRate.Value = skill.CritRate;
            }
            else
            {
                decud_Skill_CritRate.Text = "";
            }

            // Apply the critcal damage
            check_Skill_CritDamage.IsChecked = skill.CustomCritDamage;
            if (skill.CustomCritDamage)
            {
                decud_Skill_CritDamage.Value = skill.CritDamage;
            }
            else
            {
                decud_Skill_CritDamage.Text = "";
            }

            // Apply the critcal guard rate reduction
            check_Skill_CritDefGuard.IsChecked = skill.CustomCritDefGuard;
            if (skill.CustomCritDefGuard)
            {
                decud_Skill_CritDefGuard.Value = skill.CritDefGuard;
            }
            else
            {
                decud_Skill_CritDefGuard.Text = "";
            }

            // Apply the critcal evasion rate reduction
            check_Skill_CritDefEva.IsChecked = skill.CustomCritDefEva;
            if (skill.CustomCritDefEva)
            {
                decud_Skill_CritDefEva.Value = skill.CritDefEva;
            }
            else
            {
                decud_Skill_CritDefEva.Text = "";
            }

            #endregion

            #region Special Critical

            // Apply the special critcal rate
            check_Skill_SpCritRate.IsChecked = skill.CustomSpCritRate;
            if (skill.CustomSpCritRate)
            {
                decud_Skill_SpCritRate.Value = skill.SpCritRate;
            }
            else
            {
                decud_Skill_SpCritRate.Text = "";
            }

            // Apply the special critcal damage
            check_Skill_SpCritDamage.IsChecked = skill.CustomSpCritDamage;
            if (skill.CustomSpCritDamage)
            {
                decud_Skill_SpCritDamage.Value = skill.SpCritDamage;
            }
            else
            {
                decud_Skill_SpCritDamage.Text = "";
            }

            // Apply the special critcal guard rate reduction
            check_Skill_SpCritDefGuard.IsChecked = skill.CustomSpCritDefGuard;
            if (skill.CustomSpCritDefGuard)
            {
                decud_Skill_SpCritDefGuard.Value = skill.SpCritDefGuard;
            }
            else
            {
                decud_Skill_SpCritDefGuard.Text = "";
            }

            // Apply the special critcal evasion rate reduction
            check_Skill_SpCritDefEva.IsChecked = skill.CustomSpCritDefEva;
            if (skill.CustomSpCritDefEva)
            {
                decud_Skill_SpCritDefEva.Value = skill.SpCritDefEva;
            }
            else
            {
                decud_Skill_SpCritDefEva.Text = "";
            }

            #endregion
        }

        #endregion

        #region Apply Empty Data

        public void applyEmptySkill()
        {
            // Set the updating flag
            updating = true;

            // Disable the family configuration
            exp_Skill_Family.IsEnabled = false;
            exp_Skill_Family.IsExpanded = false;

            // Apply the empty skill family name
            txt_Skill_Name.Text = "";

            #region In Family

            skillInFamily.Clear();

            skillAvailable.Clear();
            if (cfg.SkillAvailable.Count > 0)
            {
                foreach (Skill skill in cfg.SkillAvailable)
                {
                    skillAvailable.Add(new Skill(skill.ID, skill.Name));
                }
                skillAvailable.Sort((x, y) => x.ID.CompareTo(y.ID));
            }

            #endregion

            #region Attack

            // Apply the empty attack
            check_Skill_Atk.IsChecked = false;
            intud_Skill_AtkInitial.Text = "";

            // Apply the empty hit rate
            check_Skill_Hit.IsChecked = false;
            decud_Skill_HitInitial.Text = "";

            // Apply the empty parameter attack force
            check_Skill_ParamForce.IsChecked = false;
            decud_Skill_StrForce.Text = "";
            decud_Skill_DexForce.Text = "";
            decud_Skill_AgiForce.Text = "";
            decud_Skill_IntForce.Text = "";

            // Apply the empty empty defense attack force
            check_Skill_DefenseForce.IsChecked = false;
            decud_Skill_PDefForce.Text = "";
            decud_Skill_MDefForce.Text = "";

            #endregion

            #region Critical

            // Apply the empty critcal rate
            check_Skill_CritRate.IsChecked = false;
            decud_Skill_CritRate.Text = "";

            // Apply the empty critcal damage
            check_Skill_CritDamage.IsChecked = false;
            decud_Skill_CritDamage.Text = "";

            // Apply the empty critcal guard rate reduction
            check_Skill_CritDefGuard.IsChecked = false;
            decud_Skill_CritDefGuard.Text = "";

            // Apply the empty critcal evasion rate reduction
            check_Skill_CritDefEva.IsChecked = false;
            decud_Skill_CritDefEva.Text = "";

            #endregion

            #region Special Critical

            // Apply the empty special critcal rate
            check_Skill_SpCritRate.IsChecked = false;
            decud_Skill_SpCritRate.Text = "";

            // Apply the empty special critcal damage
            check_Skill_SpCritDamage.IsChecked = false;
            decud_Skill_SpCritDamage.Text = "";

            // Apply the empty special critcal guard rate reduction
            check_Skill_SpCritDefGuard.IsChecked = false;
            decud_Skill_SpCritDefGuard.Text = "";

            // Apply the empty special critcal evasion rate reduction
            check_Skill_SpCritDefEva.IsChecked = false;
            decud_Skill_SpCritDefEva.Text = "";

            #endregion

            // Remove the updating Flag
            updating = false;
        }

        #endregion

        #region Apply Family Data

        public void applySkillFamily(int index)
        {
            // Set the updating flag
            updating = true;

            // Load the new skill parameter
            if (tree_Skill_IsFamily() && index > 0 && index <= cfg.SkillFamily.Count && cfg.SkillFamily.Count > 0)
            {
                exp_Skill_Family.IsEnabled = true;
                setSkillData(cfg.SkillFamily[index - 1]);
            }
            else
            {
                // Empty the skill parameter
                applyEmptySkill();
            }

            // Remove the updating Flag
            updating = false;
        }

        private void applySkillFamily()
        {
            applySkillFamily(tree_Skill_ID());
        }

        #endregion

        #region Apply Individual Data

        public void applySkillID(int index)
        {
            // Set the updating flag
            updating = true;
            // Load the new skill parameter
            if (tree_Skill_IsIndividual() && index > 0 && index <= cfg.SkillID.Count && cfg.SkillID.Count > 0)
            {
                exp_Skill_Family.IsEnabled = false;
                exp_Skill_Family.IsExpanded = false;
                setSkillData(cfg.SkillID[index - 1]);
            }
            else
            {
                // Empty the skill parameter
                applyEmptySkill();
            }

            // Remove the updating Flag
            updating = false;
        }

        private void applySkillID()
        {
            applySkillID(tree_Skill_ID());
        }

        #endregion

        #region Apply Default Data

        public void applySkillDefault()
        {
            // Set the updating flag
            updating = true;

            // Load the new skill parameter
            if (tree_Skill_Default.IsSelected)
            {
                exp_Skill_Family.IsEnabled = false;
                exp_Skill_Family.IsExpanded = false;
                setSkillData(cfg.SkillDefault);
            }
            else
            {
                // Empty the skill parameter
                applyEmptySkill();
            }

            // Remove the updating Flag
            updating = false;
        }

        #endregion

        #endregion

        #region Passive Skill

        #region Set Data

        private void setPassiveSkillData(DataPackPassiveSkill passiveSkill)
        {
            // Apply the passive skill family name
            txt_PassiveSkill_Name.Text = passiveSkill.Name;

            #region In Family

            // Create the list of passive skill in the family
            passiveSkillInFamily.Clear();
            if (passiveSkill.PassiveSkillFamily.Count > 0)
            {
                foreach (Skill skill in passiveSkill.PassiveSkillFamily)
                {
                    passiveSkillInFamily.Add(new Skill(skill.ID, skill.Name));
                }
                passiveSkillInFamily.Sort((x, y) => x.ID.CompareTo(y.ID));
            }

            passiveSkillAvailable.Clear();
            if (cfg.PassiveSkillAvailable.Count > 0)
            {
                foreach (Skill skill in cfg.PassiveSkillAvailable)
                {
                    passiveSkillAvailable.Add(new Skill(skill.ID, skill.Name));
                }
                passiveSkillAvailable.Sort((x, y) => x.ID.CompareTo(y.ID));
            }

            #endregion

            #region Parameter

            // Apply the maximum HP
            check_PassiveSkill_MaxHP.IsChecked = passiveSkill.CustomMaxHP;
            if (passiveSkill.CustomMaxHP == true)
            {
                decud_PassiveSkill_MaxHPMul.Value = passiveSkill.MaxHPMul;
                intud_PassiveSkill_MaxHPAdd.Value = passiveSkill.MaxHPAdd;
            }
            else
            {
                decud_PassiveSkill_MaxHPMul.Text = "";
                intud_PassiveSkill_MaxHPAdd.Text = "";
            }

            // Apply the maximum SP
            check_PassiveSkill_MaxSP.IsChecked = passiveSkill.CustomMaxSP;
            if (passiveSkill.CustomMaxSP == true)
            {
                decud_PassiveSkill_MaxSPMul.Value = passiveSkill.MaxSPMul;
                intud_PassiveSkill_MaxSPAdd.Value = passiveSkill.MaxSPAdd;
            }
            else
            {
                decud_PassiveSkill_MaxSPMul.Text = "";
                intud_PassiveSkill_MaxSPAdd.Text = "";
            }

            // Apply the strengh
            check_PassiveSkill_Str.IsChecked = passiveSkill.CustomStr;
            if (passiveSkill.CustomStr)
            {
                decud_PassiveSkill_StrMul.Value = passiveSkill.StrMul;
                intud_PassiveSkill_StrAdd.Value = passiveSkill.StrAdd;
            }
            else
            {
                decud_PassiveSkill_StrMul.Text = "";
                intud_PassiveSkill_StrAdd.Text = "";
            }

            // Apply the dexterity
            check_PassiveSkill_Dex.IsChecked = passiveSkill.CustomDex;
            if (passiveSkill.CustomDex)
            {
                decud_PassiveSkill_DexMul.Value = passiveSkill.DexMul;
                intud_PassiveSkill_DexAdd.Value = passiveSkill.DexAdd;
            }
            else
            {
                decud_PassiveSkill_DexMul.Text = "";
                intud_PassiveSkill_DexAdd.Text = "";
            }

            // Apply the agility
            check_PassiveSkill_Agi.IsChecked = passiveSkill.CustomAgi;
            if (passiveSkill.CustomAgi)
            {
                decud_PassiveSkill_AgiMul.Value = passiveSkill.AgiMul;
                intud_PassiveSkill_AgiAdd.Value = passiveSkill.AgiAdd;
            }
            else
            {
                decud_PassiveSkill_AgiMul.Text = "";
                intud_PassiveSkill_AgiAdd.Text = "";
            }

            // Apply the intelligence
            check_PassiveSkill_Int.IsChecked = passiveSkill.CustomInt;
            if (passiveSkill.CustomInt)
            {
                decud_PassiveSkill_IntMul.Value = passiveSkill.IntMul;
                intud_PassiveSkill_IntAdd.Value = passiveSkill.IntAdd;
            }
            else
            {
                decud_PassiveSkill_IntMul.Text = "";
                intud_PassiveSkill_IntAdd.Text = "";
            }

            #endregion

            #region Parameter Rate

            // Apply the strengh rate
            check_PassiveSkill_StrRate.IsChecked = passiveSkill.CustomStrRate;
            if (passiveSkill.CustomStrRate)
            {
                decud_PassiveSkill_StrRateMul.Value = passiveSkill.StrRateMul;
                intud_PassiveSkill_StrRateAdd.Value = passiveSkill.StrRateAdd;
            }
            else
            {
                decud_PassiveSkill_StrRateMul.Text = "";
                intud_PassiveSkill_StrRateAdd.Text = "";
            }

            // Apply the dexterity rate
            check_PassiveSkill_DexRate.IsChecked = passiveSkill.CustomDexRate;
            if (passiveSkill.CustomDexRate)
            {
                decud_PassiveSkill_DexRateMul.Value = passiveSkill.DexRateMul;
                intud_PassiveSkill_DexRateAdd.Value = passiveSkill.DexRateAdd;
            }
            else
            {
                decud_PassiveSkill_DexRateMul.Text = "";
                intud_PassiveSkill_DexRateAdd.Text = "";
            }

            // Apply the agility rate
            check_PassiveSkill_AgiRate.IsChecked = passiveSkill.CustomAgiRate;
            if (passiveSkill.CustomAgiRate)
            {
                decud_PassiveSkill_AgiRateMul.Value = passiveSkill.AgiRateMul;
                intud_PassiveSkill_AgiRateAdd.Value = passiveSkill.AgiRateAdd;
            }
            else
            {
                decud_PassiveSkill_AgiRateMul.Text = "";
                intud_PassiveSkill_AgiRateAdd.Text = "";
            }

            // Apply the intelligence rate
            check_PassiveSkill_IntRate.IsChecked = passiveSkill.CustomIntRate;
            if (passiveSkill.CustomIntRate)
            {
                decud_PassiveSkill_IntRateMul.Value = passiveSkill.IntRateMul;
                intud_PassiveSkill_IntRateAdd.Value = passiveSkill.IntRateAdd;
            }
            else
            {
                decud_PassiveSkill_IntRateMul.Text = "";
                intud_PassiveSkill_IntRateAdd.Text = "";
            }

            // Apply the physical defense rate
            check_PassiveSkill_PDefRate.IsChecked = passiveSkill.CustomPDefRate;
            if (passiveSkill.CustomPDefRate)
            {
                decud_PassiveSkill_PDefRateMul.Value = passiveSkill.PDefRateMul;
                intud_PassiveSkill_PDefRateAdd.Value = passiveSkill.PDefRateAdd;
            }
            else
            {
                decud_PassiveSkill_PDefRateMul.Text = "";
                intud_PassiveSkill_PDefRateAdd.Text = "";
            }

            // Apply the magical defense rate
            check_PassiveSkill_MDefRate.IsChecked = passiveSkill.CustomMDefRate;
            if (passiveSkill.CustomMDefRate)
            {
                decud_PassiveSkill_MDefRateMul.Value = passiveSkill.MDefRateMul;
                intud_PassiveSkill_MDefRateAdd.Value = passiveSkill.MDefRateAdd;
            }
            else
            {
                decud_PassiveSkill_MDefRateMul.Text = "";
                intud_PassiveSkill_MDefRateAdd.Text = "";
            }

            // Apply the guard rate
            check_PassiveSkill_GuardRate.IsChecked = passiveSkill.CustomGuardRate;
            if (passiveSkill.CustomGuardRate)
            {
                decud_PassiveSkill_GuardRateMul.Value = passiveSkill.GuardRateMul;
                intud_PassiveSkill_GuardRateAdd.Value = passiveSkill.GuardRateAdd;
            }
            else
            {
                decud_PassiveSkill_GuardRateMul.Text = "";
                intud_PassiveSkill_GuardRateAdd.Text = "";
            }

            // Apply the evasion rate
            check_PassiveSkill_EvaRate.IsChecked = passiveSkill.CustomEvaRate;
            if (passiveSkill.CustomEvaRate)
            {
                decud_PassiveSkill_EvaRateMul.Value = passiveSkill.EvaRateMul;
                intud_PassiveSkill_EvaRateAdd.Value = passiveSkill.EvaRateAdd;
            }
            else
            {
                decud_PassiveSkill_EvaRateMul.Text = "";
                intud_PassiveSkill_EvaRateAdd.Text = "";
            }

            #endregion

            #region Defense against Attack Critical

            // Apply the defense against attack critical rate
            check_PassiveSkill_DefCritRate.IsChecked = passiveSkill.CustomDefCritRate;
            if (passiveSkill.CustomDefCritRate)
            {
                decud_PassiveSkill_DefCritRateMul.Value = passiveSkill.DefCritRateMul;
                intud_PassiveSkill_DefCritRateAdd.Value = passiveSkill.DefCritRateAdd;
            }
            else
            {
                decud_PassiveSkill_DefCritRateMul.Text = "";
                intud_PassiveSkill_DefCritRateAdd.Text = "";
            }

            // Apply the defense against attack critical damage
            check_PassiveSkill_DefCritDamage.IsChecked = passiveSkill.CustomDefCritDamage;
            if (passiveSkill.CustomDefCritDamage)
            {
                decud_PassiveSkill_DefCritDamageMul.Value = passiveSkill.DefCritDamageMul;
                intud_PassiveSkill_DefCritDamageAdd.Value = passiveSkill.DefCritDamageAdd;
            }
            else
            {
                decud_PassiveSkill_DefCritDamageMul.Text = "";
                intud_PassiveSkill_DefCritDamageAdd.Text = "";
            }

            // Apply the defense against attack critical rate
            check_PassiveSkill_DefSpCritRate.IsChecked = passiveSkill.CustomDefSpCritRate;
            if (passiveSkill.CustomDefSpCritRate)
            {
                decud_PassiveSkill_DefSpCritRateMul.Value = passiveSkill.DefSpCritRateMul;
                intud_PassiveSkill_DefSpCritRateAdd.Value = passiveSkill.DefSpCritRateAdd;
            }
            else
            {
                decud_PassiveSkill_DefSpCritRateMul.Text = "";
                intud_PassiveSkill_DefSpCritRateAdd.Text = "";
            }

            // Apply the defense against attack critical damage
            check_PassiveSkill_DefSpCritDamage.IsChecked = passiveSkill.CustomDefSpCritDamage;
            if (passiveSkill.CustomDefSpCritDamage)
            {
                decud_PassiveSkill_DefSpCritDamageMul.Value = passiveSkill.DefSpCritDamageMul;
                intud_PassiveSkill_DefSpCritDamageAdd.Value = passiveSkill.DefSpCritDamageAdd;
            }
            else
            {
                decud_PassiveSkill_DefSpCritDamageMul.Text = "";
                intud_PassiveSkill_DefSpCritDamageAdd.Text = "";
            }

            #endregion

            #region Defense against Skill Critical

            // Apply the defense against skill critical rate
            check_PassiveSkill_DefSkillCritRate.IsChecked = passiveSkill.CustomDefSkillCritRate;
            if (passiveSkill.CustomDefSkillCritRate)
            {
                decud_PassiveSkill_DefSkillCritRateMul.Value = passiveSkill.DefSkillCritRateMul;
                intud_PassiveSkill_DefSkillCritRateAdd.Value = passiveSkill.DefSkillCritRateAdd;
            }
            else
            {
                decud_PassiveSkill_DefSkillCritRateMul.Text = "";
                intud_PassiveSkill_DefSkillCritRateAdd.Text = "";
            }

            // Apply the defense against attack critical damage
            check_PassiveSkill_DefSkillCritDamage.IsChecked = passiveSkill.CustomDefSkillCritDamage;
            if (passiveSkill.CustomDefSkillCritDamage)
            {
                decud_PassiveSkill_DefSkillCritDamageMul.Value = passiveSkill.DefSkillCritDamageMul;
                intud_PassiveSkill_DefSkillCritDamageAdd.Value = passiveSkill.DefSkillCritDamageAdd;
            }
            else
            {
                decud_PassiveSkill_DefSkillCritDamageMul.Text = "";
                intud_PassiveSkill_DefSkillCritDamageAdd.Text = "";
            }

            // Apply the defense against attack critical rate
            check_PassiveSkill_DefSkillSpCritRate.IsChecked = passiveSkill.CustomDefSkillSpCritRate;
            if (passiveSkill.CustomDefSkillSpCritRate)
            {
                decud_PassiveSkill_DefSkillSpCritRateMul.Value = passiveSkill.DefSkillSpCritRateMul;
                intud_PassiveSkill_DefSkillSpCritRateAdd.Value = passiveSkill.DefSkillSpCritRateAdd;
            }
            else
            {
                decud_PassiveSkill_DefSkillSpCritRateMul.Text = "";
                intud_PassiveSkill_DefSkillSpCritRateAdd.Text = "";
            }

            // Apply the defense against attack critical damage
            check_PassiveSkill_DefSkillSpCritDamage.IsChecked = passiveSkill.CustomDefSkillSpCritDamage;
            if (passiveSkill.CustomDefSkillSpCritDamage)
            {
                decud_PassiveSkill_DefSkillSpCritDamageMul.Value = passiveSkill.DefSkillSpCritDamageMul;
                intud_PassiveSkill_DefSkillSpCritDamageAdd.Value = passiveSkill.DefSkillSpCritDamageAdd;
            }
            else
            {
                decud_PassiveSkill_DefSkillSpCritDamageMul.Text = "";
                intud_PassiveSkill_DefSkillSpCritDamageAdd.Text = "";
            }

            #endregion

            #region Attack

            // Apply the attack
            check_PassiveSkill_Atk.IsChecked = passiveSkill.CustomAtk;
            if (passiveSkill.CustomAtk)
            {
                decud_PassiveSkill_AtkMul.Value = passiveSkill.AtkMul;
                intud_PassiveSkill_AtkAdd.Value = passiveSkill.AtkAdd;
            }
            else
            {
                decud_PassiveSkill_AtkMul.Text = "";
                intud_PassiveSkill_AtkAdd.Text = "";
            }

            // Apply the hit rate
            check_PassiveSkill_Hit.IsChecked = passiveSkill.CustomHit;
            if (passiveSkill.CustomHit)
            {
                decud_PassiveSkill_HitMul.Value = passiveSkill.HitMul;
                intud_PassiveSkill_HitAdd.Value = passiveSkill.HitAdd;
            }
            else
            {
                decud_PassiveSkill_HitMul.Text = "";
                intud_PassiveSkill_HitAdd.Text = "";
            }

            #endregion

            #region Critical

            // Apply the critcal rate
            check_PassiveSkill_CritRate.IsChecked = passiveSkill.CustomCritRate;
            if (passiveSkill.CustomCritRate)
            {
                decud_PassiveSkill_CritRateMul.Value = passiveSkill.CritRateMul;
                intud_PassiveSkill_CritRateAdd.Value = passiveSkill.CritRateAdd;
            }
            else
            {
                decud_PassiveSkill_CritRateMul.Text = "";
                intud_PassiveSkill_CritRateAdd.Text = "";
            }

            // Apply the critcal damage
            check_PassiveSkill_CritDamage.IsChecked = passiveSkill.CustomCritDamage;
            if (passiveSkill.CustomCritDamage)
            {
                decud_PassiveSkill_CritDamageMul.Value = passiveSkill.CritDamageMul;
                intud_PassiveSkill_CritDamageAdd.Value = passiveSkill.CritDamageAdd;
            }
            else
            {
                decud_PassiveSkill_CritDamageMul.Text = "";
                intud_PassiveSkill_CritDamageAdd.Text = "";
            }

            // Apply the critcal guard rate reduction
            check_PassiveSkill_CritDefGuard.IsChecked = passiveSkill.CustomCritDefGuard;
            if (passiveSkill.CustomCritDefGuard)
            {
                decud_PassiveSkill_CritDefGuardMul.Value = passiveSkill.CritDefGuardMul;
                intud_PassiveSkill_CritDefGuardAdd.Value = passiveSkill.CritDefGuardAdd;
            }
            else
            {
                decud_PassiveSkill_CritDefGuardMul.Text = "";
                intud_PassiveSkill_CritDefGuardAdd.Text = "";
            }

            // Apply the critcal evasion rate reduction
            check_PassiveSkill_CritDefEva.IsChecked = passiveSkill.CustomCritDefEva;
            if (passiveSkill.CustomCritDefEva)
            {
                decud_PassiveSkill_CritDefEvaMul.Value = passiveSkill.CritDefEvaMul;
                intud_PassiveSkill_CritDefEvaAdd.Value = passiveSkill.CritDefEvaAdd;
            }
            else
            {
                decud_PassiveSkill_CritDefEvaMul.Text = "";
                intud_PassiveSkill_CritDefEvaAdd.Text = "";
            }

            #endregion

            #region Special Critical

            // Apply the special critcal rate
            check_PassiveSkill_SpCritRate.IsChecked = passiveSkill.CustomSpCritRate;
            if (passiveSkill.CustomSpCritRate)
            {
                decud_PassiveSkill_SpCritRateMul.Value = passiveSkill.SpCritRateMul;
                intud_PassiveSkill_SpCritRateAdd.Value = passiveSkill.SpCritRateAdd;
            }
            else
            {
                decud_PassiveSkill_SpCritRateMul.Text = "";
                intud_PassiveSkill_SpCritRateAdd.Text = "";
            }

            // Apply the special critcal damage
            check_PassiveSkill_SpCritDamage.IsChecked = passiveSkill.CustomSpCritDamage;
            if (passiveSkill.CustomSpCritDamage)
            {
                decud_PassiveSkill_SpCritDamageMul.Value = passiveSkill.SpCritDamageMul;
                intud_PassiveSkill_SpCritDamageAdd.Value = passiveSkill.SpCritDamageAdd;
            }
            else
            {
                decud_PassiveSkill_SpCritDamageMul.Text = "";
                intud_PassiveSkill_SpCritDamageAdd.Text = "";
            }

            // Apply the special critcal guard rate reduction
            check_PassiveSkill_SpCritDefGuard.IsChecked = passiveSkill.CustomSpCritDefGuard;
            if (passiveSkill.CustomSpCritDefGuard)
            {
                decud_PassiveSkill_SpCritDefGuardMul.Value = passiveSkill.SpCritDefGuardMul;
                intud_PassiveSkill_SpCritDefGuardAdd.Value = passiveSkill.SpCritDefGuardAdd;
            }
            else
            {
                decud_PassiveSkill_SpCritDefGuardMul.Text = "";
                intud_PassiveSkill_SpCritDefGuardAdd.Text = "";
            }

            // Apply the special critcal evasion rate reduction
            check_PassiveSkill_SpCritDefEva.IsChecked = passiveSkill.CustomSpCritDefEva;
            if (passiveSkill.CustomSpCritDefEva)
            {
                decud_PassiveSkill_SpCritDefEvaMul.Value = passiveSkill.SpCritDefEvaMul;
                intud_PassiveSkill_SpCritDefEvaAdd.Value = passiveSkill.SpCritDefEvaAdd;
            }
            else
            {
                decud_PassiveSkill_SpCritDefEvaMul.Text = "";
                intud_PassiveSkill_SpCritDefEvaAdd.Text = "";
            }

            #endregion

            #region Defense

            // Apply the physical defense
            check_PassiveSkill_PDef.IsChecked = passiveSkill.CustomPDef;
            if (passiveSkill.CustomPDef)
            {
                decud_PassiveSkill_PDefMul.Value = passiveSkill.PDefMul;
                intud_PassiveSkill_PDefAdd.Value = passiveSkill.PDefAdd;
            }
            else
            {
                decud_PassiveSkill_PDefMul.Text = "";
                intud_PassiveSkill_PDefAdd.Text = "";
            }

            // Apply the magical defense
            check_PassiveSkill_MDef.IsChecked = passiveSkill.CustomMDef;
            if (passiveSkill.CustomMDef)
            {
                decud_PassiveSkill_MDefMul.Value = passiveSkill.MDefMul;
                intud_PassiveSkill_MDefAdd.Value = passiveSkill.MDefAdd;
            }
            else
            {
                decud_PassiveSkill_MDefMul.Text = "";
                intud_PassiveSkill_MDefAdd.Text = "";
            }

            #endregion
        }

        #endregion

        #region Apply Empty Data

        public void applyEmptyPassiveSkill()
        {
            // Set the updating flag
            updating = true;

            // Apply the empty passive skill family name
            txt_PassiveSkill_Name.Text = "";

            #region In Family

            // In family
            passiveSkillInFamily.Clear();

            passiveSkillAvailable.Clear();
            if (cfg.PassiveSkillAvailable.Count > 0)
            {
                foreach (Skill skill in cfg.PassiveSkillAvailable)
                {
                    passiveSkillAvailable.Add(new Skill(skill.ID, skill.Name));
                }
                passiveSkillAvailable.Sort((x, y) => x.ID.CompareTo(y.ID));
            }

            #endregion

            #region Parameter

            // Apply the empty maximum HP
            check_PassiveSkill_MaxHP.IsChecked = false;
            decud_PassiveSkill_MaxHPMul.Text = "";
            intud_PassiveSkill_MaxHPAdd.Text = "";

            // Apply the empty maximum SP
            check_PassiveSkill_MaxSP.IsChecked = false;
            decud_PassiveSkill_MaxSPMul.Text = "";
            intud_PassiveSkill_MaxSPAdd.Text = "";

            // Apply the empty strengh
            check_PassiveSkill_Str.IsChecked = false;
            decud_PassiveSkill_StrMul.Text = "";
            intud_PassiveSkill_StrAdd.Text = "";

            // Apply the empty dexterity
            check_PassiveSkill_Dex.IsChecked = false;
            decud_PassiveSkill_DexMul.Text = "";
            intud_PassiveSkill_DexAdd.Text = "";

            // Apply the empty agility
            check_PassiveSkill_Agi.IsChecked = false;
            decud_PassiveSkill_AgiMul.Text = "";
            intud_PassiveSkill_AgiAdd.Text = "";

            // Apply the empty intelligence
            check_PassiveSkill_Int.IsChecked = false;
            decud_PassiveSkill_IntMul.Text = "";
            intud_PassiveSkill_IntAdd.Text = "";

            // Apply the empty guard rate
            check_PassiveSkill_GuardRate.IsChecked = false;
            decud_PassiveSkill_GuardRateMul.Text = "";
            intud_PassiveSkill_GuardRateAdd.Text = "";

            // Apply the empty evasion rate
            check_PassiveSkill_EvaRate.IsChecked = false;
            decud_PassiveSkill_EvaRateMul.Text = "";
            intud_PassiveSkill_EvaRateAdd.Text = "";

            #endregion

            #region Parameter Rate

            // Apply the empty strengh rate
            check_PassiveSkill_StrRate.IsChecked = false;
            decud_PassiveSkill_StrRateMul.Text = "";
            intud_PassiveSkill_StrRateAdd.Text = "";

            // Apply the empty dexterity rate
            check_PassiveSkill_DexRate.IsChecked = false;
            decud_PassiveSkill_DexRateMul.Text = "";
            intud_PassiveSkill_DexRateAdd.Text = "";

            // Apply the empty agility rate
            check_PassiveSkill_AgiRate.IsChecked = false;
            decud_PassiveSkill_AgiRateMul.Text = "";
            intud_PassiveSkill_AgiRateAdd.Text = "";

            // Apply the empty intelligence rate
            check_PassiveSkill_IntRate.IsChecked = false;
            decud_PassiveSkill_IntRateMul.Text = "";
            intud_PassiveSkill_IntRateAdd.Text = "";

            // Apply the empty physical defense rate
            check_PassiveSkill_PDefRate.IsChecked = false;
            decud_PassiveSkill_PDefRateMul.Text = "";
            intud_PassiveSkill_PDefRateAdd.Text = "";

            // Apply the empty magical defense rate
            check_PassiveSkill_MDefRate.IsChecked = false;
            decud_PassiveSkill_MDefRateMul.Text = "";
            intud_PassiveSkill_MDefRateAdd.Text = "";

            #endregion

            #region Defense against Attack Critical

            // Apply the defense against attack critical rate
            check_PassiveSkill_DefCritRate.IsChecked = false;
            decud_PassiveSkill_DefCritRateMul.Text = "";
            intud_PassiveSkill_DefCritRateAdd.Text = "";

            // Apply the defense against attack critical damage
            check_PassiveSkill_DefCritDamage.IsChecked = false;
            decud_PassiveSkill_DefCritDamageMul.Text = "";
            intud_PassiveSkill_DefCritDamageAdd.Text = "";

            // Apply the defense against attack critical rate
            check_PassiveSkill_DefSpCritRate.IsChecked = false;
            decud_PassiveSkill_DefSpCritRateMul.Text = "";
            intud_PassiveSkill_DefSpCritRateAdd.Text = "";

            // Apply the defense against attack critical damage
            check_PassiveSkill_DefSpCritDamage.IsChecked = false;
            decud_PassiveSkill_DefSpCritDamageMul.Text = "";
            intud_PassiveSkill_DefSpCritDamageAdd.Text = "";

            #endregion

            #region Defense against Skill Critical

            // Apply the defense against skill critical rate
            check_PassiveSkill_DefSkillCritRate.IsChecked = false;
            decud_PassiveSkill_DefSkillCritRateMul.Text = "";
            intud_PassiveSkill_DefSkillCritRateAdd.Text = "";

            // Apply the defense against attack critical damage
            check_PassiveSkill_DefSkillCritDamage.IsChecked = false;
            decud_PassiveSkill_DefSkillCritDamageMul.Text = "";
            intud_PassiveSkill_DefSkillCritDamageAdd.Text = "";

            // Apply the defense against attack critical rate
            check_PassiveSkill_DefSkillSpCritRate.IsChecked = false;
            decud_PassiveSkill_DefSkillSpCritRateMul.Text = "";
            intud_PassiveSkill_DefSkillSpCritRateAdd.Text = "";

            // Apply the defense against attack critical damage
            check_PassiveSkill_DefSkillSpCritDamage.IsChecked = false;
            decud_PassiveSkill_DefSkillSpCritDamageMul.Text = "";
            intud_PassiveSkill_DefSkillSpCritDamageAdd.Text = "";

            #endregion

            #region Attack

            // Apply the empty attack
            check_PassiveSkill_Atk.IsChecked = false;
            decud_PassiveSkill_AtkMul.Text = "";
            intud_PassiveSkill_AtkAdd.Text = "";

            // Apply the empty hit rate
            check_PassiveSkill_Hit.IsChecked = false;
            decud_PassiveSkill_HitMul.Text = "";
            intud_PassiveSkill_HitAdd.Text = "";

            #endregion

            #region Critical

            // Apply the critcal rate
            check_PassiveSkill_CritRate.IsChecked = false;
            decud_PassiveSkill_CritRateMul.Text = "";
            intud_PassiveSkill_CritRateAdd.Text = "";

            // Apply the critcal damage
            check_PassiveSkill_CritDamage.IsChecked = false;
            decud_PassiveSkill_CritDamageMul.Text = "";
            intud_PassiveSkill_CritDamageAdd.Text = "";

            // Apply the critcal guard rate reduction
            check_PassiveSkill_CritDefGuard.IsChecked = false;
            decud_PassiveSkill_CritDefGuardMul.Text = "";
            intud_PassiveSkill_CritDefGuardAdd.Text = "";

            // Apply the critcal evasion rate reduction
            check_PassiveSkill_CritDefEva.IsChecked = false;
            decud_PassiveSkill_CritDefEvaMul.Text = "";
            intud_PassiveSkill_CritDefEvaAdd.Text = "";

            #endregion

            #region Special Critical

            // Apply the special critcal rate
            check_PassiveSkill_SpCritRate.IsChecked = false;
            decud_PassiveSkill_SpCritRateMul.Text = "";
            intud_PassiveSkill_SpCritRateAdd.Text = "";

            // Apply the special critcal damage
            check_PassiveSkill_SpCritDamage.IsChecked = false;
            decud_PassiveSkill_SpCritDamageMul.Text = "";
            intud_PassiveSkill_SpCritDamageAdd.Text = "";

            // Apply the special critcal guard rate reduction
            check_PassiveSkill_SpCritDefGuard.IsChecked = false;
            decud_PassiveSkill_SpCritDefGuardMul.Text = "";
            intud_PassiveSkill_SpCritDefGuardAdd.Text = "";

            // Apply the special critcal evasion rate reduction
            check_PassiveSkill_SpCritDefEva.IsChecked = false;
            decud_PassiveSkill_SpCritDefEvaMul.Text = "";
            intud_PassiveSkill_SpCritDefEvaAdd.Text = "";

            #endregion

            #region Defense

            // Apply the empty physical defense
            check_PassiveSkill_PDef.IsChecked = false;
            decud_PassiveSkill_PDefMul.Text = "";
            intud_PassiveSkill_PDefAdd.Text = "";

            // Apply the empty magical defense
            check_PassiveSkill_MDef.IsChecked = false;
            decud_PassiveSkill_MDefMul.Text = "";
            intud_PassiveSkill_MDefAdd.Text = "";

            #endregion

            // Remove the updating Flag
            updating = false;
        }

        #endregion

        #region Apply Family Data

        public void applyPassiveSkillFamily(int index)
        {
            // Set the updating flag
            updating = true;

            // Load the new skill parameter
            if (tree_PassiveSkill_IsFamily() && index > 0 && index <= cfg.PassiveSkillFamily.Count && cfg.PassiveSkillFamily.Count > 0)
            {
                exp_PassiveSkill_Family.IsEnabled = true;
                setPassiveSkillData(cfg.PassiveSkillFamily[index - 1]);
            }
            else
            {
                // Empty the skill parameter
                applyEmptyPassiveSkill();
            }

            // Remove the updating Flag
            updating = false;
        }

        private void applyPassiveSkillFamily()
        {
            applyPassiveSkillFamily(tree_PassiveSkill_ID());
        }

        #endregion

        #region Apply Individual Data

        public void applyPassiveSkillID(int index)
        {
            // Set the updating flag
            updating = true;

            // Load the new skill parameter
            if (tree_PassiveSkill_IsIndividual() && index > 0 && index <= cfg.PassiveSkillID.Count && cfg.PassiveSkillID.Count > 0)
            {
                exp_PassiveSkill_Family.IsEnabled = false;
                exp_PassiveSkill_Family.IsExpanded = false;
                setPassiveSkillData(cfg.PassiveSkillID[index - 1]);
            }
            else
            {
                // Empty the skill parameter
                applyEmptyPassiveSkill();
            }

            // Remove the updating Flag
            updating = false;
        }

        private void applyPassiveSkillID()
        {
            applyPassiveSkillID(tree_PassiveSkill_ID());
        }

        #endregion

        #region Apply Default Data

        public void applyPassiveSkillDefault()
        {
            // Set the updating flag
            updating = true;

            // Load the new skill parameter
            if (tree_PassiveSkill_Default.IsSelected)
            {
                exp_PassiveSkill_Family.IsEnabled = false;
                exp_PassiveSkill_Family.IsExpanded = false;
                setPassiveSkillData(cfg.PassiveSkillDefault);
            }
            else
            {
                // Empty the skill parameter
                applyEmptyPassiveSkill();
            }

            // Remove the updating Flag
            updating = false;
        }

        #endregion

        #endregion

        #region Weapon

        #region Set Data

        public void setWeaponData(DataPackEquipment weapon)
        {
            // Apply the weapon family name
            txt_Weapon_Name.Text = weapon.Name;

            #region In Family

            // Create the list of weapon in the family
            weaponInFamily.Clear();
            if (weapon.WeaponFamily.Count > 0)
            {
                foreach (Weapon weapons in weapon.WeaponFamily)
                {
                    weaponInFamily.Add(new Weapon(weapons.ID, weapons.Name));
                }
                weaponInFamily.Sort((x, y) => x.ID.CompareTo(y.ID));
            }

            weaponAvailable.Clear();
            if (cfg.WeaponAvailable.Count > 0)
            {
                foreach (Weapon weapons in cfg.WeaponAvailable)
                {
                    weaponAvailable.Add(new Weapon(weapons.ID, weapons.Name));
                }
                weaponAvailable.Sort((x, y) => x.ID.CompareTo(y.ID));
            }

            #endregion

            #region Weapon

            // Apply the number of had
            check_Weapon_Hand.IsChecked = weapon.CustomHand;
            if (weapon.CustomHand)
            {
                intud_Weapon_Hand.Value = weapon.Hand;
            }
            else
            {
                intud_Weapon_Hand.Text = "";
            }

            // Apply the one slot only
            check_Weapon_ShieldOnly.IsChecked = weapon.ShieldOnly;
            check_Weapon_WeaponOnly.IsChecked = weapon.WeaponOnly;

            // Apply the cursed flag
            check_Weapon_Cursed.IsChecked = weapon.Cursed;

            // Apply the equiping switch id
            check_Weapon_SwitchID.IsChecked = weapon.CustomSwitchID;
            if (weapon.CustomSwitchID)
            {
                combo_Weapon_SwitchID.SelectedIndex = weapon.SwitchID;
            }
            else
            {
                combo_Weapon_SwitchID.SelectedIndex = 0;
            }

            #endregion

            #region Attack

            // Apply the attack
            check_Weapon_Atk.IsChecked = weapon.CustomAtk;
            if (weapon.CustomAtk)
            {
                intud_Weapon_AtkInitial.Value = weapon.AtkInitial;
            }
            else
            {
                intud_Weapon_AtkInitial.Text = "";
            }

            // Apply the hit rate
            check_Weapon_Hit.IsChecked = weapon.CustomHit;
            if (weapon.CustomHit)
            {
                decud_Weapon_HitInitial.Value = weapon.HitInitial;
            }
            else
            {
                decud_Weapon_HitInitial.Text = "";
            }

            // Apply the strengh force
            check_Weapon_ParamForce.IsChecked = weapon.CustomParamForce;
            if (weapon.CustomParamForce)
            {
                decud_Weapon_StrForce.Value = weapon.StrForce;
                decud_Weapon_DexForce.Value = weapon.DexForce;
                decud_Weapon_AgiForce.Value = weapon.AgiForce;
                decud_Weapon_IntForce.Value = weapon.IntForce;
            }
            else
            {
                decud_Weapon_StrForce.Text = "";
                decud_Weapon_DexForce.Text = "";
                decud_Weapon_AgiForce.Text = "";
                decud_Weapon_IntForce.Text = "";
            }

            // Apply the physical defense force
            check_Weapon_DefenseForce.IsChecked = weapon.CustomDefenseForce;
            if (weapon.CustomDefenseForce)
            {
                decud_Weapon_PDefForce.Value = weapon.PDefForce;
                decud_Weapon_MDefForce.Value = weapon.MDefForce;
            }
            else
            {
                decud_Weapon_PDefForce.Text = "";
                decud_Weapon_MDefForce.Text = "";
            }

            #endregion

            #region Critical

            // Apply the critcal rate
            check_Weapon_CritRate.IsChecked = weapon.CustomCritRate;
            if (weapon.CustomCritRate)
            {
                decud_Weapon_CritRateInitial.Value = weapon.CritRateInitial;
            }
            else
            {
                decud_Weapon_CritRateInitial.Text = "";
            }

            // Apply the critcal damage
            check_Weapon_CritDamage.IsChecked = weapon.CustomCritDamage;
            if (weapon.CustomCritDamage)
            {
                decud_Weapon_CritDamageInitial.Value = weapon.CritDamageInitial;
            }
            else
            {
                decud_Weapon_CritDamageInitial.Text = "";
            }

            // Apply the critcal guard rate reduction
            check_Weapon_CritDefGuard.IsChecked = weapon.CustomCritDefGuard;
            if (weapon.CustomCritDefGuard)
            {
                decud_Weapon_CritDefGuardInitial.Value = weapon.CritDefGuardInitial;
            }
            else
            {
                decud_Weapon_CritDefGuardInitial.Text = "";
            }

            // Apply the critcal evasion rate reduction
            check_Weapon_CritDefEva.IsChecked = weapon.CustomCritDefEva;
            if (weapon.CustomCritDefEva)
            {
                decud_Weapon_CritDefEvaInitial.Value = weapon.CritDefEvaInitial;
            }
            else
            {
                decud_Weapon_CritDefEvaInitial.Text = "";
            }

            #endregion

            #region Special Critical

            // Apply the special critcal rate
            check_Weapon_SpCritRate.IsChecked = weapon.CustomSpCritRate;
            if (weapon.CustomSpCritRate)
            {
                decud_Weapon_SpCritRateInitial.Value = weapon.SpCritRateInitial;
            }
            else
            {
                decud_Weapon_SpCritRateInitial.Text = "";
            }

            // Apply the special critcal damage
            check_Weapon_SpCritDamage.IsChecked = weapon.CustomSpCritDamage;
            if (weapon.CustomSpCritDamage)
            {
                decud_Weapon_SpCritDamageInitial.Value = weapon.SpCritDamageInitial;
            }
            else
            {
                decud_Weapon_SpCritDamageInitial.Text = "";
            }

            // Apply the special critcal guard rate reduction
            check_Weapon_SpCritDefGuard.IsChecked = weapon.CustomSpCritDefGuard;
            if (weapon.CustomSpCritDefGuard)
            {
                decud_Weapon_SpCritDefGuardInitial.Value = weapon.SpCritDefGuardInitial;
            }
            else
            {
                decud_Weapon_SpCritDefGuardInitial.Text = "";
            }

            // Apply the special critcal evasion rate reduction
            check_Weapon_SpCritDefEva.IsChecked = weapon.CustomSpCritDefEva;
            if (weapon.CustomSpCritDefEva)
            {
                decud_Weapon_SpCritDefEvaInitial.Value = weapon.SpCritDefEvaInitial;
            }
            else
            {
                decud_Weapon_SpCritDefEvaInitial.Text = "";
            }

            #endregion

            #region Parameter

            // Apply the maximum HP
            check_Weapon_MaxHP.IsChecked = weapon.CustomMaxHP;
            if (weapon.CustomMaxHP == true)
            {
                decud_Weapon_MaxHPMul.Value = weapon.MaxHPMul;
                intud_Weapon_MaxHPAdd.Value = weapon.MaxHPAdd;
            }
            else
            {
                decud_Weapon_MaxHPMul.Text = "";
                intud_Weapon_MaxHPAdd.Text = "";
            }

            // Apply the maximum SP
            check_Weapon_MaxSP.IsChecked = weapon.CustomMaxSP;
            if (weapon.CustomMaxSP == true)
            {
                decud_Weapon_MaxSPMul.Value = weapon.MaxSPMul;
                intud_Weapon_MaxSPAdd.Value = weapon.MaxSPAdd;
            }
            else
            {
                decud_Weapon_MaxSPMul.Text = "";
                intud_Weapon_MaxSPAdd.Text = "";
            }

            // Apply the strengh
            check_Weapon_Str.IsChecked = weapon.CustomStr;
            if (weapon.CustomStr)
            {
                decud_Weapon_StrMul.Value = weapon.StrMul;
                intud_Weapon_StrAdd.Value = weapon.StrAdd;
            }
            else
            {
                decud_Weapon_StrMul.Text = "";
                intud_Weapon_StrAdd.Text = "";
            }

            // Apply the dexterity
            check_Weapon_Dex.IsChecked = weapon.CustomDex;
            if (weapon.CustomDex)
            {
                decud_Weapon_DexMul.Value = weapon.DexMul;
                intud_Weapon_DexAdd.Value = weapon.DexAdd;
            }
            else
            {
                decud_Weapon_DexMul.Text = "";
                intud_Weapon_DexAdd.Text = "";
            }

            // Apply the agility
            check_Weapon_Agi.IsChecked = weapon.CustomAgi;
            if (weapon.CustomAgi)
            {
                decud_Weapon_AgiMul.Value = weapon.AgiMul;
                intud_Weapon_AgiAdd.Value = weapon.AgiAdd;
            }
            else
            {
                decud_Weapon_AgiMul.Text = "";
                intud_Weapon_AgiAdd.Text = "";
            }

            // Apply the intelligence
            check_Weapon_Int.IsChecked = weapon.CustomInt;
            if (weapon.CustomInt)
            {
                decud_Weapon_IntMul.Value = weapon.IntMul;
                intud_Weapon_IntAdd.Value = weapon.IntAdd;
            }
            else
            {
                decud_Weapon_IntMul.Text = "";
                intud_Weapon_IntAdd.Text = "";
            }

            #endregion

            #region Parameter Rate

            // Apply the strengh rate
            check_Weapon_StrRate.IsChecked = weapon.CustomStrRate;
            if (weapon.CustomStrRate)
            {
                decud_Weapon_StrRateMul.Value = weapon.StrRateMul;
                intud_Weapon_StrRateAdd.Value = weapon.StrRateAdd;
            }
            else
            {
                decud_Weapon_StrRateMul.Text = "";
                intud_Weapon_StrRateAdd.Text = "";
            }

            // Apply the dexterity rate
            check_Weapon_DexRate.IsChecked = weapon.CustomDexRate;
            if (weapon.CustomDexRate)
            {
                decud_Weapon_DexRateMul.Value = weapon.DexRateMul;
                intud_Weapon_DexRateAdd.Value = weapon.DexRateAdd;
            }
            else
            {
                decud_Weapon_DexRateMul.Text = "";
                intud_Weapon_DexRateAdd.Text = "";
            }

            // Apply the agility rate
            check_Weapon_AgiRate.IsChecked = weapon.CustomAgiRate;
            if (weapon.CustomAgiRate)
            {
                decud_Weapon_AgiRateMul.Value = weapon.AgiRateMul;
                intud_Weapon_AgiRateAdd.Value = weapon.AgiRateAdd;
            }
            else
            {
                decud_Weapon_AgiRateMul.Text = "";
                intud_Weapon_AgiRateAdd.Text = "";
            }

            // Apply the intelligence rate
            check_Weapon_IntRate.IsChecked = weapon.CustomIntRate;
            if (weapon.CustomIntRate)
            {
                decud_Weapon_IntRateMul.Value = weapon.IntRateMul;
                intud_Weapon_IntRateAdd.Value = weapon.IntRateAdd;
            }
            else
            {
                decud_Weapon_IntRateMul.Text = "";
                intud_Weapon_IntRateAdd.Text = "";
            }

            // Apply the physical defense rate
            check_Weapon_PDefRate.IsChecked = weapon.CustomPDefRate;
            if (weapon.CustomPDefRate)
            {
                decud_Weapon_PDefRateMul.Value = weapon.PDefRateMul;
                intud_Weapon_PDefRateAdd.Value = weapon.PDefRateAdd;
            }
            else
            {
                decud_Weapon_PDefRateMul.Text = "";
                intud_Weapon_PDefRateAdd.Text = "";
            }

            // Apply the magical defense rate
            check_Weapon_MDefRate.IsChecked = weapon.CustomMDefRate;
            if (weapon.CustomMDefRate)
            {
                decud_Weapon_MDefRateMul.Value = weapon.MDefRateMul;
                intud_Weapon_MDefRateAdd.Value = weapon.MDefRateAdd;
            }
            else
            {
                decud_Weapon_MDefRateMul.Text = "";
                intud_Weapon_MDefRateAdd.Text = "";
            }

            // Apply the guard rate
            check_Weapon_GuardRate.IsChecked = weapon.CustomGuardRate;
            if (weapon.CustomGuardRate)
            {
                decud_Weapon_GuardRateMul.Value = weapon.GuardRateMul;
                intud_Weapon_GuardRateAdd.Value = weapon.GuardRateAdd;
            }
            else
            {
                decud_Weapon_GuardRateMul.Text = "";
                intud_Weapon_GuardRateAdd.Text = "";
            }

            // Apply the evasion rate
            check_Weapon_EvaRate.IsChecked = weapon.CustomEvaRate;
            if (weapon.CustomEvaRate)
            {
                decud_Weapon_EvaRateMul.Value = weapon.EvaRateMul;
                intud_Weapon_EvaRateAdd.Value = weapon.EvaRateAdd;
            }
            else
            {
                decud_Weapon_EvaRateMul.Text = "";
                intud_Weapon_EvaRateAdd.Text = "";
            }

            #endregion

            #region Defense against Attack Critical

            // Apply the defense against attack critical rate
            check_Weapon_DefCritRate.IsChecked = weapon.CustomDefCritRate;
            if (weapon.CustomDefCritRate)
            {
                decud_Weapon_DefCritRateMul.Value = weapon.DefCritRateMul;
                intud_Weapon_DefCritRateAdd.Value = weapon.DefCritRateAdd;
            }
            else
            {
                decud_Weapon_DefCritRateMul.Text = "";
                intud_Weapon_DefCritRateAdd.Text = "";
            }

            // Apply the defense against attack critical damage
            check_Weapon_DefCritDamage.IsChecked = weapon.CustomDefCritDamage;
            if (weapon.CustomDefCritDamage)
            {
                decud_Weapon_DefCritDamageMul.Value = weapon.DefCritDamageMul;
                intud_Weapon_DefCritDamageAdd.Value = weapon.DefCritDamageAdd;
            }
            else
            {
                decud_Weapon_DefCritDamageMul.Text = "";
                intud_Weapon_DefCritDamageAdd.Text = "";
            }

            // Apply the defense against attack critical rate
            check_Weapon_DefSpCritRate.IsChecked = weapon.CustomDefSpCritRate;
            if (weapon.CustomDefSpCritRate)
            {
                decud_Weapon_DefSpCritRateMul.Value = weapon.DefSpCritRateMul;
                intud_Weapon_DefSpCritRateAdd.Value = weapon.DefSpCritRateAdd;
            }
            else
            {
                decud_Weapon_DefSpCritRateMul.Text = "";
                intud_Weapon_DefSpCritRateAdd.Text = "";
            }

            // Apply the defense against attack critical damage
            check_Weapon_DefSpCritDamage.IsChecked = weapon.CustomDefSpCritDamage;
            if (weapon.CustomDefSpCritDamage)
            {
                decud_Weapon_DefSpCritDamageMul.Value = weapon.DefSpCritDamageMul;
                intud_Weapon_DefSpCritDamageAdd.Value = weapon.DefSpCritDamageAdd;
            }
            else
            {
                decud_Weapon_DefSpCritDamageMul.Text = "";
                intud_Weapon_DefSpCritDamageAdd.Text = "";
            }

            #endregion

            #region Defense against Skill Critical

            // Apply the defense against skill critical rate
            check_Weapon_DefSkillCritRate.IsChecked = weapon.CustomDefSkillCritRate;
            if (weapon.CustomDefSkillCritRate)
            {
                decud_Weapon_DefSkillCritRateMul.Value = weapon.DefSkillCritRateMul;
                intud_Weapon_DefSkillCritRateAdd.Value = weapon.DefSkillCritRateAdd;
            }
            else
            {
                decud_Weapon_DefSkillCritRateMul.Text = "";
                intud_Weapon_DefSkillCritRateAdd.Text = "";
            }

            // Apply the defense against attack critical damage
            check_Weapon_DefSkillCritDamage.IsChecked = weapon.CustomDefSkillCritDamage;
            if (weapon.CustomDefSkillCritDamage)
            {
                decud_Weapon_DefSkillCritDamageMul.Value = weapon.DefSkillCritDamageMul;
                intud_Weapon_DefSkillCritDamageAdd.Value = weapon.DefSkillCritDamageAdd;
            }
            else
            {
                decud_Weapon_DefSkillCritDamageMul.Text = "";
                intud_Weapon_DefSkillCritDamageAdd.Text = "";
            }

            // Apply the defense against attack critical rate
            check_Weapon_DefSkillSpCritRate.IsChecked = weapon.CustomDefSkillSpCritRate;
            if (weapon.CustomDefSkillSpCritRate)
            {
                decud_Weapon_DefSkillSpCritRateMul.Value = weapon.DefSkillSpCritRateMul;
                intud_Weapon_DefSkillSpCritRateAdd.Value = weapon.DefSkillSpCritRateAdd;
            }
            else
            {
                decud_Weapon_DefSkillSpCritRateMul.Text = "";
                intud_Weapon_DefSkillSpCritRateAdd.Text = "";
            }

            // Apply the defense against attack critical damage
            check_Weapon_DefSkillSpCritDamage.IsChecked = weapon.CustomDefSkillSpCritDamage;
            if (weapon.CustomDefSkillSpCritDamage)
            {
                decud_Weapon_DefSkillSpCritDamageMul.Value = weapon.DefSkillSpCritDamageMul;
                intud_Weapon_DefSkillSpCritDamageAdd.Value = weapon.DefSkillSpCritDamageAdd;
            }
            else
            {
                decud_Weapon_DefSkillSpCritDamageMul.Text = "";
                intud_Weapon_DefSkillSpCritDamageAdd.Text = "";
            }

            #endregion

            #region Defense

            // Apply the physical defense
            check_Weapon_PDef.IsChecked = weapon.CustomPDef;
            if (weapon.CustomPDef)
            {
                decud_Weapon_PDefMul.Value = weapon.PDefMul;
                intud_Weapon_PDefAdd.Value = weapon.PDefAdd;
            }
            else
            {
                decud_Weapon_PDefMul.Text = "";
                intud_Weapon_PDefAdd.Text = "";
            }

            // Apply the magical defense
            check_Weapon_MDef.IsChecked = weapon.CustomMDef;
            if (weapon.CustomMDef)
            {
                decud_Weapon_MDefMul.Value = weapon.MDefMul;
                intud_Weapon_MDefAdd.Value = weapon.MDefAdd;
            }
            else
            {
                decud_Weapon_MDefMul.Text = "";
                intud_Weapon_MDefAdd.Text = "";
            }

            #endregion
        }

        #endregion

        #region Apply Empty Data

        public void applyEmptyWeapon()
        {
            // Set the updating flag
            updating = true;

            // Apply the empty weapon family name
            txt_Weapon_Name.Text = "";

            #region In Family

            weaponInFamily.Clear();

            weaponAvailable.Clear();
            if (cfg.WeaponAvailable.Count > 0)
            {
                foreach (Weapon weapon in cfg.WeaponAvailable)
                {
                    weaponAvailable.Add(new Weapon(weapon.ID, weapon.Name));
                }
                weaponAvailable.Sort((x, y) => x.ID.CompareTo(y.ID));
            }

            #endregion

            #region Weapon

            // Apply the number of had
            check_Weapon_Hand.IsChecked = false;
            intud_Weapon_Hand.Text = "";

            // Apply the one slot only
            check_Weapon_ShieldOnly.IsChecked = false;
            check_Weapon_WeaponOnly.IsChecked = false;

            // Apply the cursed flag
            check_Weapon_Cursed.IsChecked = false;

            // Apply the equiping switch id
            check_Weapon_SwitchID.IsChecked = false;
            combo_Weapon_SwitchID.SelectedIndex = 0;

            #endregion

            #region Attack

            // Apply the empty attack
            check_Weapon_Atk.IsChecked = false;
            intud_Weapon_AtkInitial.Text = "";

            // Apply the empty hit rate
            check_Weapon_Hit.IsChecked = false;
            decud_Weapon_HitInitial.Text = "";

            // Apply the parameter force
            check_Weapon_ParamForce.IsChecked = false;
            decud_Weapon_StrForce.Text = "";
            decud_Weapon_DexForce.Text = "";
            decud_Weapon_AgiForce.Text = "";
            decud_Weapon_IntForce.Text = "";

            // Apply the defense force
            check_Weapon_DefenseForce.IsChecked = false;
            decud_Weapon_PDefForce.Text = "";
            decud_Weapon_MDefForce.Text = "";

            #endregion

            #region Critical

            // Apply the critcal rate
            check_Weapon_CritRate.IsChecked = false;
            decud_Weapon_CritRateInitial.Text = "";

            // Apply the critcal damage
            check_Weapon_CritDamage.IsChecked = false;
            decud_Weapon_CritDamageInitial.Text = "";

            // Apply the critcal guard rate reduction
            check_Weapon_CritDefGuard.IsChecked = false;
            decud_Weapon_CritDefGuardInitial.Text = "";

            // Apply the critcal evasion rate reduction
            check_Weapon_CritDefEva.IsChecked = false;
            decud_Weapon_CritDefEvaInitial.Text = "";

            #endregion

            #region Special Critical

            // Apply the special critcal rate
            check_Weapon_SpCritRate.IsChecked = false;
            decud_Weapon_SpCritRateInitial.Text = "";

            // Apply the special critcal damage
            check_Weapon_SpCritDamage.IsChecked = false;
            decud_Weapon_SpCritDamageInitial.Text = "";

            // Apply the special critcal guard rate reduction
            check_Weapon_SpCritDefGuard.IsChecked = false;
            decud_Weapon_SpCritDefGuardInitial.Text = "";

            // Apply the special critcal evasion rate reduction
            check_Weapon_SpCritDefEva.IsChecked = false;
            decud_Weapon_SpCritDefEvaInitial.Text = "";

            #endregion

            #region Parameter

            // Apply the empty maximum HP
            check_Weapon_MaxHP.IsChecked = false;
            decud_Weapon_MaxHPMul.Text = "";
            intud_Weapon_MaxHPAdd.Text = "";

            // Apply the empty maximum SP
            check_Weapon_MaxSP.IsChecked = false;
            decud_Weapon_MaxSPMul.Text = "";
            intud_Weapon_MaxSPAdd.Text = "";

            // Apply the empty strengh
            check_Weapon_Str.IsChecked = false;
            decud_Weapon_StrMul.Text = "";
            intud_Weapon_StrAdd.Text = "";

            // Apply the empty dexterity
            check_Weapon_Dex.IsChecked = false;
            decud_Weapon_DexMul.Text = "";
            intud_Weapon_DexAdd.Text = "";

            // Apply the empty agility
            check_Weapon_Agi.IsChecked = false;
            decud_Weapon_AgiMul.Text = "";
            intud_Weapon_AgiAdd.Text = "";

            // Apply the empty intelligence
            check_Weapon_Int.IsChecked = false;
            decud_Weapon_IntMul.Text = "";
            intud_Weapon_IntAdd.Text = "";

            // Apply the empty guard rate
            check_Weapon_GuardRate.IsChecked = false;
            decud_Weapon_GuardRateMul.Text = "";
            intud_Weapon_GuardRateAdd.Text = "";

            // Apply the empty evasion rate
            check_Weapon_EvaRate.IsChecked = false;
            decud_Weapon_EvaRateMul.Text = "";
            intud_Weapon_EvaRateAdd.Text = "";

            #endregion

            #region Parameter Rate

            // Apply the empty strengh rate
            check_Weapon_StrRate.IsChecked = false;
            decud_Weapon_StrRateMul.Text = "";
            intud_Weapon_StrRateAdd.Text = "";

            // Apply the empty dexterity rate
            check_Weapon_DexRate.IsChecked = false;
            decud_Weapon_DexRateMul.Text = "";
            intud_Weapon_DexRateAdd.Text = "";

            // Apply the empty agility rate
            check_Weapon_AgiRate.IsChecked = false;
            decud_Weapon_AgiRateMul.Text = "";
            intud_Weapon_AgiRateAdd.Text = "";

            // Apply the empty intelligence rate
            check_Weapon_IntRate.IsChecked = false;
            decud_Weapon_IntRateMul.Text = "";
            intud_Weapon_IntRateAdd.Text = "";

            // Apply the empty physical defense rate
            check_Weapon_PDefRate.IsChecked = false;
            decud_Weapon_PDefRateMul.Text = "";
            intud_Weapon_PDefRateAdd.Text = "";

            // Apply the empty magical defense rate
            check_Weapon_MDefRate.IsChecked = false;
            decud_Weapon_MDefRateMul.Text = "";
            intud_Weapon_MDefRateAdd.Text = "";

            #endregion

            #region Defense against Attack Critical

            // Apply the defense against attack critical rate
            check_Weapon_DefCritRate.IsChecked = false;
            decud_Weapon_DefCritRateMul.Text = "";
            intud_Weapon_DefCritRateAdd.Text = "";

            // Apply the defense against attack critical damage
            check_Weapon_DefCritDamage.IsChecked = false;
            decud_Weapon_DefCritDamageMul.Text = "";
            intud_Weapon_DefCritDamageAdd.Text = "";

            // Apply the defense against attack critical rate
            check_Weapon_DefSpCritRate.IsChecked = false;
            decud_Weapon_DefSpCritRateMul.Text = "";
            intud_Weapon_DefSpCritRateAdd.Text = "";

            // Apply the defense against attack critical damage
            check_Weapon_DefSpCritDamage.IsChecked = false;
            decud_Weapon_DefSpCritDamageMul.Text = "";
            intud_Weapon_DefSpCritDamageAdd.Text = "";

            #endregion

            #region Defense against Skill Critical

            // Apply the defense against skill critical rate
            check_Weapon_DefSkillCritRate.IsChecked = false;
            decud_Weapon_DefSkillCritRateMul.Text = "";
            intud_Weapon_DefSkillCritRateAdd.Text = "";

            // Apply the defense against attack critical damage
            check_Weapon_DefSkillCritDamage.IsChecked = false;
            decud_Weapon_DefSkillCritDamageMul.Text = "";
            intud_Weapon_DefSkillCritDamageAdd.Text = "";

            // Apply the defense against attack critical rate
            check_Weapon_DefSkillSpCritRate.IsChecked = false;
            decud_Weapon_DefSkillSpCritRateMul.Text = "";
            intud_Weapon_DefSkillSpCritRateAdd.Text = "";

            // Apply the defense against attack critical damage
            check_Weapon_DefSkillSpCritDamage.IsChecked = false;
            decud_Weapon_DefSkillSpCritDamageMul.Text = "";
            intud_Weapon_DefSkillSpCritDamageAdd.Text = "";

            #endregion

            #region Defense

            // Apply the empty physical defense
            check_Weapon_PDef.IsChecked = false;
            decud_Weapon_PDefMul.Text = "";
            intud_Weapon_PDefAdd.Text = "";

            // Apply the empty magical defense
            check_Weapon_MDef.IsChecked = false;
            decud_Weapon_MDefMul.Text = "";
            intud_Weapon_MDefAdd.Text = "";

            #endregion

            // Remove the updating Flag
            updating = false;
        }

        #endregion

        #region Apply Family Data

        public void applyWeaponFamily(int index)
        {
            // Set the updating flag
            updating = true;

            // Load the new skill parameter
            if (tree_Weapon_IsFamily() && index > 0 && index <= cfg.WeaponFamily.Count && cfg.WeaponFamily.Count > 0)
            {
                exp_Weapon_Family.IsEnabled = true;
                setWeaponData(cfg.WeaponFamily[index - 1]);
            }
            else
            {
                // Empty the skill parameter
                applyEmptyWeapon();
            }

            // Remove the updating Flag
            updating = false;
        }

        private void applyWeaponFamily()
        {
            applyWeaponFamily(tree_Weapon_ID());
        }

        #endregion

        #region Apply Individual Data

        public void applyWeaponID(int index)
        {
            // Set the updating flag
            updating = true;

            // Load the new skill parameter
            if (tree_Weapon_IsIndividual() && index > 0 && index <= cfg.WeaponID.Count && cfg.WeaponID.Count > 0)
            {
                exp_Weapon_Family.IsEnabled = false;
                exp_Weapon_Family.IsExpanded = false;
                setWeaponData(cfg.WeaponID[index - 1]);
            }
            else
            {
                // Empty the skill parameter
                applyEmptyWeapon();
            }

            // Remove the updating Flag
            updating = false;
        }

        private void applyWeaponID()
        {
            applyWeaponID(tree_Weapon_ID());
        }

        #endregion

        #region Apply Default Data

        public void applyWeaponDefault()
        {
            // Set the updating flag
            updating = true;

            // Load the new skill parameter
            if (tree_Weapon_Default.IsSelected)
            {
                exp_Weapon_Family.IsEnabled = false;
                exp_Weapon_Family.IsExpanded = false;
                setWeaponData(cfg.WeaponDefault);
            }
            else
            {
                // Empty the skill parameter
                applyEmptyWeapon();
            }

            // Remove the updating Flag
            updating = false;
        }

        #endregion

        #endregion

        #region Armor

        #region Set Data

        public void setArmorData(DataPackEquipment armor)
        {
            // Apply the armor family name
            txt_Armor_Name.Text = armor.Name;

            #region In Family

            // Create the list of passive skill in the family
            armorInFamily.Clear();
            if (armor.ArmorFamily.Count > 0)
            {
                foreach (Armor armors in armor.ArmorFamily)
                {
                    armorInFamily.Add(new Armor(armors.ID, armors.Name));
                }
                armorInFamily.Sort((x, y) => x.ID.CompareTo(y.ID));
            }

            armorAvailable.Clear();
            if (cfg.PassiveSkillAvailable.Count > 0)
            {
                foreach (Armor armors in cfg.ArmorAvailable)
                {
                    armorAvailable.Add(new Armor(armors.ID, armors.Name));
                }
                armorAvailable.Sort((x, y) => x.ID.CompareTo(y.ID));
            }

            #endregion

            #region Armor
            // Apply the armor type
            if (armor.Type < 5 || armor.Type > cfg.Default.EquipType.Count - 1)
            {
                combo_Armor_Type.SelectedIndex = 0;
            }
            else
            {
                combo_Armor_Type.SelectedIndex = armor.Type - 4;
            }

            // Apply the cursed flag
            check_Armor_Cursed.IsChecked = armor.Cursed;

            // Apply the equiping switch id
            check_Armor_SwitchID.IsChecked = armor.CustomSwitchID;
            if (armor.CustomSwitchID)
            {
                combo_Armor_SwitchID.SelectedIndex = armor.SwitchID;
            }
            else
            {
                combo_Armor_SwitchID.SelectedIndex = 0;
            }

            #endregion

            #region Defense

            // Apply the physical defense
            check_Armor_PDef.IsChecked = armor.CustomPDef;
            if (armor.CustomPDef)
            {
                intud_Armor_PDefInitial.Value = armor.PDefInitial;
            }
            else
            {
                intud_Armor_PDefInitial.Text = "";
            }

            // Apply the magical defense
            check_Armor_MDef.IsChecked = armor.CustomMDef;
            if (armor.CustomMDef)
            {
                intud_Armor_MDefInitial.Value = armor.MDefInitial;
            }
            else
            {
                intud_Armor_MDefInitial.Text = "";
            }

            #endregion

            #region Parameter

            // Apply the maximum HP
            check_Armor_MaxHP.IsChecked = armor.CustomMaxHP;
            if (armor.CustomMaxHP == true)
            {
                decud_Armor_MaxHPMul.Value = armor.MaxHPMul;
                intud_Armor_MaxHPAdd.Value = armor.MaxHPAdd;
            }
            else
            {
                decud_Armor_MaxHPMul.Text = "";
                intud_Armor_MaxHPAdd.Text = "";
            }

            // Apply the maximum SP
            check_Armor_MaxSP.IsChecked = armor.CustomMaxSP;
            if (armor.CustomMaxSP == true)
            {
                decud_Armor_MaxSPMul.Value = armor.MaxSPMul;
                intud_Armor_MaxSPAdd.Value = armor.MaxSPAdd;
            }
            else
            {
                decud_Armor_MaxSPMul.Text = "";
                intud_Armor_MaxSPAdd.Text = "";
            }

            // Apply the strengh
            check_Armor_Str.IsChecked = armor.CustomStr;
            if (armor.CustomStr)
            {
                decud_Armor_StrMul.Value = armor.StrMul;
                intud_Armor_StrAdd.Value = armor.StrAdd;
            }
            else
            {
                decud_Armor_StrMul.Text = "";
                intud_Armor_StrAdd.Text = "";
            }

            // Apply the dexterity
            check_Armor_Dex.IsChecked = armor.CustomDex;
            if (armor.CustomDex)
            {
                decud_Armor_DexMul.Value = armor.DexMul;
                intud_Armor_DexAdd.Value = armor.DexAdd;
            }
            else
            {
                decud_Armor_DexMul.Text = "";
                intud_Armor_DexAdd.Text = "";
            }

            // Apply the agility
            check_Armor_Agi.IsChecked = armor.CustomAgi;
            if (armor.CustomAgi)
            {
                decud_Armor_AgiMul.Value = armor.AgiMul;
                intud_Armor_AgiAdd.Value = armor.AgiAdd;
            }
            else
            {
                decud_Armor_AgiMul.Text = "";
                intud_Armor_AgiAdd.Text = "";
            }

            // Apply the intelligence
            check_Armor_Int.IsChecked = armor.CustomInt;
            if (armor.CustomInt)
            {
                decud_Armor_IntMul.Value = armor.IntMul;
                intud_Armor_IntAdd.Value = armor.IntAdd;
            }
            else
            {
                decud_Armor_IntMul.Text = "";
                intud_Armor_IntAdd.Text = "";
            }

            #endregion

            #region Parameter Rate

            // Apply the strengh rate
            check_Armor_StrRate.IsChecked = armor.CustomStrRate;
            if (armor.CustomStrRate)
            {
                decud_Armor_StrRateMul.Value = armor.StrRateMul;
                intud_Armor_StrRateAdd.Value = armor.StrRateAdd;
            }
            else
            {
                decud_Armor_StrRateMul.Text = "";
                intud_Armor_StrRateAdd.Text = "";
            }

            // Apply the dexterity rate
            check_Armor_DexRate.IsChecked = armor.CustomDexRate;
            if (armor.CustomDexRate)
            {
                decud_Armor_DexRateMul.Value = armor.DexRateMul;
                intud_Armor_DexRateAdd.Value = armor.DexRateAdd;
            }
            else
            {
                decud_Armor_DexRateMul.Text = "";
                intud_Armor_DexRateAdd.Text = "";
            }

            // Apply the agility rate
            check_Armor_AgiRate.IsChecked = armor.CustomAgiRate;
            if (armor.CustomAgiRate)
            {
                decud_Armor_AgiRateMul.Value = armor.AgiRateMul;
                intud_Armor_AgiRateAdd.Value = armor.AgiRateAdd;
            }
            else
            {
                decud_Armor_AgiRateMul.Text = "";
                intud_Armor_AgiRateAdd.Text = "";
            }

            // Apply the intelligence rate
            check_Armor_IntRate.IsChecked = armor.CustomIntRate;
            if (armor.CustomIntRate)
            {
                decud_Armor_IntRateMul.Value = armor.IntRateMul;
                intud_Armor_IntRateAdd.Value = armor.IntRateAdd;
            }
            else
            {
                decud_Armor_IntRateMul.Text = "";
                intud_Armor_IntRateAdd.Text = "";
            }

            // Apply the physical defense rate
            check_Armor_PDefRate.IsChecked = armor.CustomPDefRate;
            if (armor.CustomPDefRate)
            {
                decud_Armor_PDefRateMul.Value = armor.PDefRateMul;
                intud_Armor_PDefRateAdd.Value = armor.PDefRateAdd;
            }
            else
            {
                decud_Armor_PDefRateMul.Text = "";
                intud_Armor_PDefRateAdd.Text = "";
            }

            // Apply the magical defense rate
            check_Armor_MDefRate.IsChecked = armor.CustomMDefRate;
            if (armor.CustomMDefRate)
            {
                decud_Armor_MDefRateMul.Value = armor.MDefRateMul;
                intud_Armor_MDefRateAdd.Value = armor.MDefRateAdd;
            }
            else
            {
                decud_Armor_MDefRateMul.Text = "";
                intud_Armor_MDefRateAdd.Text = "";
            }

            // Apply the guard rate
            check_Armor_GuardRate.IsChecked = armor.CustomGuardRate;
            if (armor.CustomGuardRate)
            {
                decud_Armor_GuardRateMul.Value = armor.GuardRateMul;
                intud_Armor_GuardRateAdd.Value = armor.GuardRateAdd;
            }
            else
            {
                decud_Armor_GuardRateMul.Text = "";
                intud_Armor_GuardRateAdd.Text = "";
            }

            // Apply the evasion rate
            check_Armor_EvaRate.IsChecked = armor.CustomEvaRate;
            if (armor.CustomEvaRate)
            {
                decud_Armor_EvaRateMul.Value = armor.EvaRateMul;
                intud_Armor_EvaRateAdd.Value = armor.EvaRateAdd;
            }
            else
            {
                decud_Armor_EvaRateMul.Text = "";
                intud_Armor_EvaRateAdd.Text = "";
            }

            #endregion

            #region Defense against Attack Critical

            // Apply the defense against attack critical rate
            check_Armor_DefCritRate.IsChecked = armor.CustomDefCritRate;
            if (armor.CustomDefCritRate)
            {
                decud_Armor_DefCritRateMul.Value = armor.DefCritRateMul;
                intud_Armor_DefCritRateAdd.Value = armor.DefCritRateAdd;
            }
            else
            {
                decud_Armor_DefCritRateMul.Text = "";
                intud_Armor_DefCritRateAdd.Text = "";
            }

            // Apply the defense against attack critical damage
            check_Armor_DefCritDamage.IsChecked = armor.CustomDefCritDamage;
            if (armor.CustomDefCritDamage)
            {
                decud_Armor_DefCritDamageMul.Value = armor.DefCritDamageMul;
                intud_Armor_DefCritDamageAdd.Value = armor.DefCritDamageAdd;
            }
            else
            {
                decud_Armor_DefCritDamageMul.Text = "";
                intud_Armor_DefCritDamageAdd.Text = "";
            }

            // Apply the defense against attack critical rate
            check_Armor_DefSpCritRate.IsChecked = armor.CustomDefSpCritRate;
            if (armor.CustomDefSpCritRate)
            {
                decud_Armor_DefSpCritRateMul.Value = armor.DefSpCritRateMul;
                intud_Armor_DefSpCritRateAdd.Value = armor.DefSpCritRateAdd;
            }
            else
            {
                decud_Armor_DefSpCritRateMul.Text = "";
                intud_Armor_DefSpCritRateAdd.Text = "";
            }

            // Apply the defense against attack critical damage
            check_Armor_DefSpCritDamage.IsChecked = armor.CustomDefSpCritDamage;
            if (armor.CustomDefSpCritDamage)
            {
                decud_Armor_DefSpCritDamageMul.Value = armor.DefSpCritDamageMul;
                intud_Armor_DefSpCritDamageAdd.Value = armor.DefSpCritDamageAdd;
            }
            else
            {
                decud_Armor_DefSpCritDamageMul.Text = "";
                intud_Armor_DefSpCritDamageAdd.Text = "";
            }

            #endregion

            #region Defense against Skill Critical

            // Apply the defense against skill critical rate
            check_Armor_DefSkillCritRate.IsChecked = armor.CustomDefSkillCritRate;
            if (armor.CustomDefSkillCritRate)
            {
                decud_Armor_DefSkillCritRateMul.Value = armor.DefSkillCritRateMul;
                intud_Armor_DefSkillCritRateAdd.Value = armor.DefSkillCritRateAdd;
            }
            else
            {
                decud_Armor_DefSkillCritRateMul.Text = "";
                intud_Armor_DefSkillCritRateAdd.Text = "";
            }

            // Apply the defense against attack critical damage
            check_Armor_DefSkillCritDamage.IsChecked = armor.CustomDefSkillCritDamage;
            if (armor.CustomDefSkillCritDamage)
            {
                decud_Armor_DefSkillCritDamageMul.Value = armor.DefSkillCritDamageMul;
                intud_Armor_DefSkillCritDamageAdd.Value = armor.DefSkillCritDamageAdd;
            }
            else
            {
                decud_Armor_DefSkillCritDamageMul.Text = "";
                intud_Armor_DefSkillCritDamageAdd.Text = "";
            }

            // Apply the defense against attack critical rate
            check_Armor_DefSkillSpCritRate.IsChecked = armor.CustomDefSkillSpCritRate;
            if (armor.CustomDefSkillSpCritRate)
            {
                decud_Armor_DefSkillSpCritRateMul.Value = armor.DefSkillSpCritRateMul;
                intud_Armor_DefSkillSpCritRateAdd.Value = armor.DefSkillSpCritRateAdd;
            }
            else
            {
                decud_Armor_DefSkillSpCritRateMul.Text = "";
                intud_Armor_DefSkillSpCritRateAdd.Text = "";
            }

            // Apply the defense against attack critical damage
            check_Armor_DefSkillSpCritDamage.IsChecked = armor.CustomDefSkillSpCritDamage;
            if (armor.CustomDefSkillSpCritDamage)
            {
                decud_Armor_DefSkillSpCritDamageMul.Value = armor.DefSkillSpCritDamageMul;
                intud_Armor_DefSkillSpCritDamageAdd.Value = armor.DefSkillSpCritDamageAdd;
            }
            else
            {
                decud_Armor_DefSkillSpCritDamageMul.Text = "";
                intud_Armor_DefSkillSpCritDamageAdd.Text = "";
            }

            #endregion

            #region Attack

            // Apply the attack
            check_Armor_Atk.IsChecked = armor.CustomAtk;
            if (armor.CustomAtk)
            {
                decud_Armor_AtkMul.Value = armor.AtkMul;
                intud_Armor_AtkAdd.Value = armor.AtkAdd;
            }
            else
            {
                decud_Armor_AtkMul.Text = "";
                intud_Armor_AtkAdd.Text = "";
            }

            // Apply the hit rate
            check_Armor_Hit.IsChecked = armor.CustomHit;
            if (armor.CustomHit)
            {
                decud_Armor_HitMul.Value = armor.HitMul;
                intud_Armor_HitAdd.Value = armor.HitAdd;
            }
            else
            {
                decud_Armor_HitMul.Text = "";
                intud_Armor_HitAdd.Text = "";
            }

            #endregion

            #region Critical

            // Apply the critcal rate
            check_Armor_CritRate.IsChecked = armor.CustomCritRate;
            if (armor.CustomCritRate)
            {
                decud_Armor_CritRateMul.Value = armor.CritRateMul;
                intud_Armor_CritRateAdd.Value = armor.CritRateAdd;
            }
            else
            {
                decud_Armor_CritRateMul.Text = "";
                intud_Armor_CritRateAdd.Text = "";
            }

            // Apply the critcal damage
            check_Armor_CritDamage.IsChecked = armor.CustomCritDamage;
            if (armor.CustomCritDamage)
            {
                decud_Armor_CritDamageMul.Value = armor.CritDamageMul;
                intud_Armor_CritDamageAdd.Value = armor.CritDamageAdd;
            }
            else
            {
                decud_Armor_CritDamageMul.Text = "";
                intud_Armor_CritDamageAdd.Text = "";
            }

            // Apply the critcal guard rate reduction
            check_Armor_CritDefGuard.IsChecked = armor.CustomCritDefGuard;
            if (armor.CustomCritDefGuard)
            {
                decud_Armor_CritDefGuardMul.Value = armor.CritDefGuardMul;
                intud_Armor_CritDefGuardAdd.Value = armor.CritDefGuardAdd;
            }
            else
            {
                decud_Armor_CritDefGuardMul.Text = "";
                intud_Armor_CritDefGuardAdd.Text = "";
            }

            // Apply the critcal evasion rate reduction
            check_Armor_CritDefEva.IsChecked = armor.CustomCritDefEva;
            if (armor.CustomCritDefEva)
            {
                decud_Armor_CritDefEvaMul.Value = armor.CritDefEvaMul;
                intud_Armor_CritDefEvaAdd.Value = armor.CritDefEvaAdd;
            }
            else
            {
                decud_Armor_CritDefEvaMul.Text = "";
                intud_Armor_CritDefEvaAdd.Text = "";
            }

            #endregion

            #region Special Critical

            // Apply the special critcal rate
            check_Armor_SpCritRate.IsChecked = armor.CustomSpCritRate;
            if (armor.CustomSpCritRate)
            {
                decud_Armor_SpCritRateMul.Value = armor.SpCritRateMul;
                intud_Armor_SpCritRateAdd.Value = armor.SpCritRateAdd;
            }
            else
            {
                decud_Armor_SpCritRateMul.Text = "";
                intud_Armor_SpCritRateAdd.Text = "";
            }

            // Apply the special critcal damage
            check_Armor_SpCritDamage.IsChecked = armor.CustomSpCritDamage;
            if (armor.CustomSpCritDamage)
            {
                decud_Armor_SpCritDamageMul.Value = armor.SpCritDamageMul;
                intud_Armor_SpCritDamageAdd.Value = armor.SpCritDamageAdd;
            }
            else
            {
                decud_Armor_SpCritDamageMul.Text = "";
                intud_Armor_SpCritDamageAdd.Text = "";
            }

            // Apply the special critcal guard rate reduction
            check_Armor_SpCritDefGuard.IsChecked = armor.CustomSpCritDefGuard;
            if (armor.CustomSpCritDefGuard)
            {
                decud_Armor_SpCritDefGuardMul.Value = armor.SpCritDefGuardMul;
                intud_Armor_SpCritDefGuardAdd.Value = armor.SpCritDefGuardAdd;
            }
            else
            {
                decud_Armor_SpCritDefGuardMul.Text = "";
                intud_Armor_SpCritDefGuardAdd.Text = "";
            }

            // Apply the special critcal evasion rate reduction
            check_Armor_SpCritDefEva.IsChecked = armor.CustomSpCritDefEva;
            if (armor.CustomSpCritDefEva)
            {
                decud_Armor_SpCritDefEvaMul.Value = armor.SpCritDefEvaMul;
                intud_Armor_SpCritDefEvaAdd.Value = armor.SpCritDefEvaAdd;
            }
            else
            {
                decud_Armor_SpCritDefEvaMul.Text = "";
                intud_Armor_SpCritDefEvaAdd.Text = "";
            }

            #endregion
        }

        #endregion

        #region Apply Empty Data

        public void applyEmptyArmor()
        {
            // Set the updating flag
            updating = true;

            // Apply the empty armor family name
            txt_Armor_Name.Text = "";

            #region In Family

            armorInFamily.Clear();

            armorAvailable.Clear();
            if (cfg.ArmorAvailable.Count > 0)
            {
                foreach (Armor armor in cfg.ArmorAvailable)
                {
                    armorAvailable.Add(new Armor(armor.ID, armor.Name));
                }
                armorAvailable.Sort((x, y) => x.ID.CompareTo(y.ID));
            }

            #endregion

            #region Armor

            // Apply the armor type
            combo_Armor_Type.SelectedIndex = 0;

            // Apply the cursed flag
            check_Armor_Cursed.IsChecked = false;

            // Apply the equiping switch id
            check_Armor_SwitchID.IsChecked = false;
            combo_Armor_SwitchID.SelectedIndex = 0;

            #endregion

            #region Defense

            // Apply the physical defense
            check_Armor_PDef.IsChecked = false;
            intud_Armor_PDefInitial.Text = "";

            // Apply the magical defense
            check_Armor_MDef.IsChecked = false;
            intud_Armor_MDefInitial.Text = "";

            #endregion

            #region Parameter

            // Apply the empty maximum HP
            check_Armor_MaxHP.IsChecked = false;
            decud_Armor_MaxHPMul.Text = "";
            intud_Armor_MaxHPAdd.Text = "";

            // Apply the empty maximum SP
            check_Armor_MaxSP.IsChecked = false;
            decud_Armor_MaxSPMul.Text = "";
            intud_Armor_MaxSPAdd.Text = "";

            // Apply the empty strengh
            check_Armor_Str.IsChecked = false;
            decud_Armor_StrMul.Text = "";
            intud_Armor_StrAdd.Text = "";

            // Apply the empty dexterity
            check_Armor_Dex.IsChecked = false;
            decud_Armor_DexMul.Text = "";
            intud_Armor_DexAdd.Text = "";

            // Apply the empty agility
            check_Armor_Agi.IsChecked = false;
            decud_Armor_AgiMul.Text = "";
            intud_Armor_AgiAdd.Text = "";

            // Apply the empty intelligence
            check_Armor_Int.IsChecked = false;
            decud_Armor_IntMul.Text = "";
            intud_Armor_IntAdd.Text = "";

            // Apply the empty guard rate
            check_Armor_GuardRate.IsChecked = false;
            decud_Armor_GuardRateMul.Text = "";
            intud_Armor_GuardRateAdd.Text = "";

            // Apply the empty evasion rate
            check_Armor_EvaRate.IsChecked = false;
            decud_Armor_EvaRateMul.Text = "";
            intud_Armor_EvaRateAdd.Text = "";

            #endregion

            #region Parameter Rate

            // Apply the empty strengh rate
            check_Armor_StrRate.IsChecked = false;
            decud_Armor_StrRateMul.Text = "";
            intud_Armor_StrRateAdd.Text = "";

            // Apply the empty dexterity rate
            check_Armor_DexRate.IsChecked = false;
            decud_Armor_DexRateMul.Text = "";
            intud_Armor_DexRateAdd.Text = "";

            // Apply the empty agility rate
            check_Armor_AgiRate.IsChecked = false;
            decud_Armor_AgiRateMul.Text = "";
            intud_Armor_AgiRateAdd.Text = "";

            // Apply the empty intelligence rate
            check_Armor_IntRate.IsChecked = false;
            decud_Armor_IntRateMul.Text = "";
            intud_Armor_IntRateAdd.Text = "";

            // Apply the empty physical defense rate
            check_Armor_PDefRate.IsChecked = false;
            decud_Armor_PDefRateMul.Text = "";
            intud_Armor_PDefRateAdd.Text = "";

            // Apply the empty magical defense rate
            check_Armor_MDefRate.IsChecked = false;
            decud_Armor_MDefRateMul.Text = "";
            intud_Armor_MDefRateAdd.Text = "";

            #endregion

            #region Defense against Attack Critical

            // Apply the defense against attack critical rate
            check_Armor_DefCritRate.IsChecked = false;
            decud_Armor_DefCritRateMul.Text = "";
            intud_Armor_DefCritRateAdd.Text = "";

            // Apply the defense against attack critical damage
            check_Armor_DefCritDamage.IsChecked = false;
            decud_Armor_DefCritDamageMul.Text = "";
            intud_Armor_DefCritDamageAdd.Text = "";

            // Apply the defense against attack critical rate
            check_Armor_DefSpCritRate.IsChecked = false;
            decud_Armor_DefSpCritRateMul.Text = "";
            intud_Armor_DefSpCritRateAdd.Text = "";

            // Apply the defense against attack critical damage
            check_Armor_DefSpCritDamage.IsChecked = false;
            decud_Armor_DefSpCritDamageMul.Text = "";
            intud_Armor_DefSpCritDamageAdd.Text = "";

            #endregion

            #region Defense against Skill Critical

            // Apply the defense against skill critical rate
            check_Armor_DefSkillCritRate.IsChecked = false;
            decud_Armor_DefSkillCritRateMul.Text = "";
            intud_Armor_DefSkillCritRateAdd.Text = "";

            // Apply the defense against attack critical damage
            check_Armor_DefSkillCritDamage.IsChecked = false;
            decud_Armor_DefSkillCritDamageMul.Text = "";
            intud_Armor_DefSkillCritDamageAdd.Text = "";

            // Apply the defense against attack critical rate
            check_Armor_DefSkillSpCritRate.IsChecked = false;
            decud_Armor_DefSkillSpCritRateMul.Text = "";
            intud_Armor_DefSkillSpCritRateAdd.Text = "";

            // Apply the defense against attack critical damage
            check_Armor_DefSkillSpCritDamage.IsChecked = false;
            decud_Armor_DefSkillSpCritDamageMul.Text = "";
            intud_Armor_DefSkillSpCritDamageAdd.Text = "";

            #endregion

            #region Attack

            // Apply the empty attack
            check_Armor_Atk.IsChecked = false;
            decud_Armor_AtkMul.Text = "";
            intud_Armor_AtkAdd.Text = "";

            // Apply the empty hit rate
            check_Armor_Hit.IsChecked = false;
            decud_Armor_HitMul.Text = "";
            intud_Armor_HitAdd.Text = "";

            #endregion

            #region Critical

            // Apply the critcal rate
            check_Armor_CritRate.IsChecked = false;
            decud_Armor_CritRateMul.Text = "";
            intud_Armor_CritRateAdd.Text = "";

            // Apply the critcal damage
            check_Armor_CritDamage.IsChecked = false;
            decud_Armor_CritDamageMul.Text = "";
            intud_Armor_CritDamageAdd.Text = "";

            // Apply the critcal guard rate reduction
            check_Armor_CritDefGuard.IsChecked = false;
            decud_Armor_CritDefGuardMul.Text = "";
            intud_Armor_CritDefGuardAdd.Text = "";

            // Apply the critcal evasion rate reduction
            check_Armor_CritDefEva.IsChecked = false;
            decud_Armor_CritDefEvaMul.Text = "";
            intud_Armor_CritDefEvaAdd.Text = "";

            #endregion

            #region Special Critical

            // Apply the special critcal rate
            check_Armor_SpCritRate.IsChecked = false;
            decud_Armor_SpCritRateMul.Text = "";
            intud_Armor_SpCritRateAdd.Text = "";

            // Apply the special critcal damage
            check_Armor_SpCritDamage.IsChecked = false;
            decud_Armor_SpCritDamageMul.Text = "";
            intud_Armor_SpCritDamageAdd.Text = "";

            // Apply the special critcal guard rate reduction
            check_Armor_SpCritDefGuard.IsChecked = false;
            decud_Armor_SpCritDefGuardMul.Text = "";
            intud_Armor_SpCritDefGuardAdd.Text = "";

            // Apply the special critcal evasion rate reduction
            check_Armor_SpCritDefEva.IsChecked = false;
            decud_Armor_SpCritDefEvaMul.Text = "";
            intud_Armor_SpCritDefEvaAdd.Text = "";

            #endregion

            // Remove the updating Flag
            updating = false;
        }

        #endregion

        #region Apply Family Data

        public void applyArmorFamily(int index)
        {
            // Set the updating flag
            updating = true;

            // Load the new skill parameter
            if (tree_Armor_IsFamily() && index > 0 && index <= cfg.ArmorFamily.Count && cfg.ArmorFamily.Count > 0)
            {
                exp_Armor_Family.IsEnabled = true;
                setArmorData(cfg.ArmorFamily[index - 1]);
            }
            else
            {
                // Empty the skill parameter
                applyEmptyArmor();
            }

            // Remove the updating Flag
            updating = false;
        }

        private void applyArmorFamily()
        {
            applyArmorFamily(tree_Armor_ID());
        }

        #endregion

        #region Apply Individual Data

        public void applyArmorID(int index)
        {
            // Set the updating flag
            updating = true;

            // Load the new skill parameter
            if (tree_Armor_IsIndividual() && index > 0 && index <= cfg.ArmorID.Count && cfg.ArmorID.Count > 0)
            {
                exp_Armor_Family.IsEnabled = false;
                exp_Armor_Family.IsExpanded = false;
                setArmorData(cfg.ArmorID[index - 1]);
            }
            else
            {
                // Empty the skill parameter
                applyEmptyArmor();
            }

            // Remove the updating Flag
            updating = false;
        }

        private void applyArmorID()
        {
            applyArmorID(tree_Armor_ID());
        }

        #endregion

        #region Apply Default Data

        public void applyArmorDefault()
        {
            // Set the updating flag
            updating = true;

            // Load the new skill parameter
            if (tree_Armor_Default.IsSelected)
            {
                exp_Armor_Family.IsEnabled = false;
                exp_Armor_Family.IsExpanded = false;
                setArmorData(cfg.ArmorDefault);
            }
            else
            {
                // Empty the skill parameter
                applyEmptyArmor();
            }

            // Remove the updating Flag
            updating = false;
        }

        #endregion

        #endregion

        #region Enemy

        #region Set Data

        public void setEnemyData(DataPackEnemy enemy)
        {
            // Apply the ef family name
            txt_Enemy_Name.Text = enemy.Name;

            #region In Family

            // Create the list of ef in the family
            enemyInFamily.Clear();
            if (enemy.EnemyFamily.Count > 0)
            {
                foreach (Enemy enemies in enemy.EnemyFamily)
                {
                    enemyInFamily.Add(new Enemy(enemies.ID, enemies.Name));
                }
                enemyInFamily.Sort((x, y) => x.ID.CompareTo(y.ID));
            }

            enemyAvailable.Clear();
            if (cfg.EnemyAvailable.Count > 0)
            {
                foreach (Enemy enemies in cfg.EnemyAvailable)
                {
                    enemyAvailable.Add(new Enemy(enemies.ID, enemies.Name));
                }
                enemyAvailable.Sort((x, y) => x.ID.CompareTo(y.ID));
            }

            #endregion

            #region Parameter

            // Apply the maximum HP
            check_Enemy_MaxHP.IsChecked = enemy.CustomMaxHP;
            if (enemy.CustomMaxHP == true)
            {
                intud_Enemy_MaxHPInitial.Value = enemy.MaxHPInitial;
            }
            else
            {
                intud_Enemy_MaxHPInitial.Text = "";
            }

            // Apply the maximum SP
            check_Enemy_MaxSP.IsChecked = enemy.CustomMaxSP;
            if (enemy.CustomMaxSP == true)
            {
                intud_Enemy_MaxSPInitial.Value = enemy.MaxSPInitial;
            }
            else
            {
                intud_Enemy_MaxSPInitial.Text = "";
            }

            // Apply the strengh
            check_Enemy_Str.IsChecked = enemy.CustomStr;
            if (enemy.CustomStr)
            {
                intud_Enemy_StrInitial.Value = enemy.StrInitial;
            }
            else
            {
                intud_Enemy_StrInitial.Text = "";
            }

            // Apply the dexterity
            check_Enemy_Dex.IsChecked = enemy.CustomDex;
            if (enemy.CustomDex)
            {
                intud_Enemy_DexInitial.Value = enemy.DexInitial;
            }
            else
            {
                intud_Enemy_DexInitial.Text = "";
            }

            // Apply the agility
            check_Enemy_Agi.IsChecked = enemy.CustomAgi;
            if (enemy.CustomAgi)
            {
                intud_Enemy_AgiInitial.Value = enemy.AgiInitial;
            }
            else
            {
                intud_Enemy_AgiInitial.Text = "";
            }

            // Apply the intelligence
            check_Enemy_Int.IsChecked = enemy.CustomInt;
            if (enemy.CustomInt)
            {
                intud_Enemy_IntInitial.Value = enemy.IntInitial;
            }
            else
            {
                intud_Enemy_IntInitial.Text = "";
            }

            #endregion

            #region Parameter Rate

            // Apply the strengh rate
            check_Enemy_StrRate.IsChecked = enemy.CustomStrRate;
            if (enemy.CustomStrRate)
            {
                decud_Enemy_StrRate.Value = enemy.StrRate;
            }
            else
            {
                decud_Enemy_StrRate.Text = "";
            }

            // Apply the dexterity rate
            check_Enemy_DexRate.IsChecked = enemy.CustomDexRate;
            if (enemy.CustomDexRate)
            {
                decud_Enemy_DexRate.Value = enemy.DexRate;
            }
            else
            {
                decud_Enemy_DexRate.Text = "";
            }

            // Apply the agility rate
            check_Enemy_AgiRate.IsChecked = enemy.CustomAgiRate;
            if (enemy.CustomAgiRate)
            {
                decud_Enemy_AgiRate.Value = enemy.AgiRate;
            }
            else
            {
                decud_Enemy_AgiRate.Text = "";
            }

            // Apply the intelligence rate
            check_Enemy_IntRate.IsChecked = enemy.CustomIntRate;
            if (enemy.CustomIntRate)
            {
                decud_Enemy_IntRate.Value = enemy.IntRate;
            }
            else
            {
                decud_Enemy_IntRate.Text = "";
            }

            // Apply the physical defense rate
            check_Enemy_PDefRate.IsChecked = enemy.CustomPDefRate;
            if (enemy.CustomPDefRate)
            {
                decud_Enemy_PDefRate.Value = enemy.PDefRate;
            }
            else
            {
                decud_Enemy_PDefRate.Text = "";
            }

            // Apply the magical defense rate
            check_Enemy_MDefRate.IsChecked = enemy.CustomMDefRate;
            if (enemy.CustomMDefRate)
            {
                decud_Enemy_MDefRate.Value = enemy.MDefRate;
            }
            else
            {
                decud_Enemy_MDefRate.Text = "";
            }

            // Apply the guard rate
            check_Enemy_GuardRate.IsChecked = enemy.CustomGuardRate;
            if (enemy.CustomGuardRate)
            {
                decud_Enemy_GuardRate.Value = enemy.GuardRate;
            }
            else
            {
                decud_Enemy_GuardRate.Text = "";
            }

            // Apply the evasion rate
            check_Enemy_EvaRate.IsChecked = enemy.CustomEvaRate;
            if (enemy.CustomEvaRate)
            {
                decud_Enemy_EvaRate.Value = enemy.EvaRate;
            }
            else
            {
                decud_Enemy_EvaRate.Text = "";
            }

            #endregion

            #region Defense against Attack Critical

            // Apply the defense against attack critical rate
            check_Enemy_DefCritRate.IsChecked = enemy.CustomDefCritRate;
            if (enemy.CustomDefCritRate)
            {
                decud_Enemy_DefCritRate.Value = enemy.DefCritRate;
            }
            else
            {
                decud_Enemy_DefCritRate.Text = "";
            }

            // Apply the defense against attack critical damage
            check_Enemy_DefCritDamage.IsChecked = enemy.CustomDefCritDamage;
            if (enemy.CustomDefCritDamage)
            {
                decud_Enemy_DefCritDamage.Value = enemy.DefCritDamage;
            }
            else
            {
                decud_Enemy_DefCritDamage.Text = "";
            }

            // Apply the defense against attack critical rate
            check_Enemy_DefSpCritRate.IsChecked = enemy.CustomDefSpCritRate;
            if (enemy.CustomDefSpCritRate)
            {
                decud_Enemy_DefSpCritRate.Value = enemy.DefSpCritRate;
            }
            else
            {
                decud_Enemy_DefSpCritRate.Text = "";
            }

            // Apply the defense against attack critical damage
            check_Enemy_DefSpCritDamage.IsChecked = enemy.CustomDefSpCritDamage;
            if (enemy.CustomDefSpCritDamage)
            {
                decud_Enemy_DefSpCritDamage.Value = enemy.DefSpCritDamage;
            }
            else
            {
                decud_Enemy_DefSpCritDamage.Text = "";
            }

            #endregion

            #region Defense against Skill Critical

            // Apply the defense against skill critical rate
            check_Enemy_DefSkillCritRate.IsChecked = enemy.CustomDefSkillCritRate;
            if (enemy.CustomDefSkillCritRate)
            {
                decud_Enemy_DefSkillCritRate.Value = enemy.DefSkillCritRate;
            }
            else
            {
                decud_Enemy_DefSkillCritRate.Text = "";
            }

            // Apply the defense against skill critical damage
            check_Enemy_DefSkillCritDamage.IsChecked = enemy.CustomDefSkillCritDamage;
            if (enemy.CustomDefSkillCritDamage)
            {
                decud_Enemy_DefSkillCritDamage.Value = enemy.DefSkillCritDamage;
            }
            else
            {
                decud_Enemy_DefSkillCritDamage.Text = "";
            }

            // Apply the defense against skill special critical rate
            check_Enemy_DefSkillSpCritRate.IsChecked = enemy.CustomDefSkillSpCritRate;
            if (enemy.CustomDefSkillSpCritRate)
            {
                decud_Enemy_DefSkillSpCritRate.Value = enemy.DefSkillSpCritRate;
            }
            else
            {
                decud_Enemy_DefSkillSpCritRate.Text = "";
            }

            // Apply the defense against skill special critical damage
            check_Enemy_DefSkillSpCritDamage.IsChecked = enemy.CustomDefSkillSpCritDamage;
            if (enemy.CustomDefSkillSpCritDamage)
            {
                decud_Enemy_DefSkillSpCritDamage.Value = enemy.DefSkillSpCritDamage;
            }
            else
            {
                decud_Enemy_DefSkillSpCritDamage.Text = "";
            }

            #endregion

            #region Attack

            // Apply the attack
            check_Enemy_Atk.IsChecked = enemy.CustomAtk;
            if (enemy.CustomAtk)
            {
                intud_Enemy_AtkInitial.Value = enemy.AtkInitial;
            }
            else
            {
                intud_Enemy_AtkInitial.Text = "";
            }

            // Apply the hit rate
            check_Enemy_Hit.IsChecked = enemy.CustomHit;
            if (enemy.CustomHit)
            {
                decud_Enemy_HitInitial.Value = enemy.HitInitial;
            }
            else
            {
                decud_Enemy_HitInitial.Text = "";
            }

            // Apply the strengh attack force
            check_Enemy_ParamForce.IsChecked = enemy.CustomParamForce;
            if (enemy.CustomParamForce)
            {
                decud_Enemy_StrForce.Value = enemy.StrForce;
                decud_Enemy_DexForce.Value = enemy.DexForce;
                decud_Enemy_AgiForce.Value = enemy.AgiForce;
                decud_Enemy_IntForce.Value = enemy.IntForce;
            }
            else
            {
                decud_Enemy_StrForce.Text = "";
                decud_Enemy_DexForce.Text = "";
                decud_Enemy_AgiForce.Text = "";
                decud_Enemy_IntForce.Text = "";
            }

            // Apply the defense attack force
            check_Enemy_DefenseForce.IsChecked = enemy.CustomDefenseForce;
            if (enemy.CustomDefenseForce)
            {
                decud_Enemy_PDefForce.Value = enemy.PDefForce;
                decud_Enemy_MDefForce.Value = enemy.MDefForce;
            }
            else
            {
                decud_Enemy_PDefForce.Text = "";
                decud_Enemy_MDefForce.Text = "";
            }

            #endregion

            #region Critical

            // Apply the critcal rate
            check_Enemy_CritRate.IsChecked = enemy.CustomCritRate;
            if (enemy.CustomCritRate)
            {
                decud_Enemy_CritRate.Value = enemy.CritRate;
            }
            else
            {
                decud_Enemy_CritRate.Text = "";
            }

            // Apply the critcal damage
            check_Enemy_CritDamage.IsChecked = enemy.CustomCritDamage;
            if (enemy.CustomCritDamage)
            {
                decud_Enemy_CritDamage.Value = enemy.CritDamage;
            }
            else
            {
                decud_Enemy_CritDamage.Text = "";
            }

            // Apply the critcal guard rate reduction
            check_Enemy_CritDefGuard.IsChecked = enemy.CustomCritDefGuard;
            if (enemy.CustomCritDefGuard)
            {
                decud_Enemy_CritDefGuard.Value = enemy.CritDefGuard;
            }
            else
            {
                decud_Enemy_CritDefGuard.Text = "";
            }

            // Apply the critcal evasion rate reduction
            check_Enemy_CritDefEva.IsChecked = enemy.CustomCritDefEva;
            if (enemy.CustomCritDefEva)
            {
                decud_Enemy_CritDefEva.Value = enemy.CritDefEva;
            }
            else
            {
                decud_Enemy_CritDefEva.Text = "";
            }

            #endregion

            #region Special Critical

            // Apply the special critcal rate
            check_Enemy_SpCritRate.IsChecked = enemy.CustomSpCritRate;
            if (enemy.CustomSpCritRate)
            {
                decud_Enemy_SpCritRate.Value = enemy.SpCritRate;
            }
            else
            {
                decud_Enemy_SpCritRate.Text = "";
            }

            // Apply the special critcal damage
            check_Enemy_SpCritDamage.IsChecked = enemy.CustomSpCritDamage;
            if (enemy.CustomSpCritDamage)
            {
                decud_Enemy_SpCritDamage.Value = enemy.SpCritDamage;
            }
            else
            {
                decud_Enemy_SpCritDamage.Text = "";
            }

            // Apply the special critcal guard rate reduction
            check_Enemy_SpCritDefGuard.IsChecked = enemy.CustomSpCritDefGuard;
            if (enemy.CustomSpCritDefGuard)
            {
                decud_Enemy_SpCritDefGuard.Value = enemy.SpCritDefGuard;
            }
            else
            {
                decud_Enemy_SpCritDefGuard.Text = "";
            }

            // Apply the special critcal evasion rate reduction
            check_Enemy_SpCritDefEva.IsChecked = enemy.CustomSpCritDefEva;
            if (enemy.CustomSpCritDefEva)
            {
                decud_Enemy_SpCritDefEva.Value = enemy.SpCritDefEva;
            }
            else
            {
                decud_Enemy_SpCritDefEva.Text = "";
            }

            #endregion

            #region Defense

            // Apply the physical defense
            check_Enemy_PDef.IsChecked = enemy.CustomPDef;
            if (enemy.CustomPDef)
            {
                intud_Enemy_PDefInitial.Value = enemy.PDefInitial;
            }
            else
            {
                intud_Enemy_PDefInitial.Text = "";
            }

            // Apply the magical defense
            check_Enemy_MDef.IsChecked = enemy.CustomMDef;
            if (enemy.CustomMDef)
            {
                intud_Enemy_MDefInitial.Value = enemy.MDefInitial;
            }
            else
            {
                intud_Enemy_MDefInitial.Text = "";
            }

            #endregion
        }

        #endregion

        #region Apply Empty Data

        public void applyEmptyEnemy()
        {
            // Set the updating flag
            updating = true;

            // Apply the empty ef family name
            txt_Enemy_Name.Text = "";

            #region In Family

            enemyInFamily.Clear();

            enemyAvailable.Clear();
            if (cfg.EnemyAvailable.Count > 0)
            {
                foreach (Enemy ef in cfg.EnemyAvailable)
                {
                    enemyAvailable.Add(new Enemy(ef.ID, ef.Name));
                }
                enemyAvailable.Sort((x, y) => x.ID.CompareTo(y.ID));
            }

            #endregion

            #region Parameter

            // Apply the empty maximum HP
            check_Enemy_MaxHP.IsChecked = false;
            intud_Enemy_MaxHPInitial.Text = "";

            // Apply the empty maximum SP
            check_Enemy_MaxSP.IsChecked = false;
            intud_Enemy_MaxSPInitial.Text = "";

            // Apply the empty strengh
            check_Enemy_Str.IsChecked = false;
            intud_Enemy_StrInitial.Text = "";

            // Apply the empty dexterity
            check_Enemy_Dex.IsChecked = false;
            intud_Enemy_DexInitial.Text = "";

            // Apply the empty agility
            check_Enemy_Agi.IsChecked = false;
            intud_Enemy_AgiInitial.Text = "";

            // Apply the empty intelligence
            check_Enemy_Int.IsChecked = false;
            intud_Enemy_IntInitial.Text = "";

            #endregion

            #region Parameter Rate

            // Apply the empty strengh rate
            check_Enemy_StrRate.IsChecked = false;
            decud_Enemy_StrRate.Text = "";

            // Apply the empty dexterity rate
            check_Enemy_DexRate.IsChecked = false;
            decud_Enemy_DexRate.Text = "";

            // Apply the empty agility rate
            check_Enemy_AgiRate.IsChecked = false;
            decud_Enemy_AgiRate.Text = "";

            // Apply the empty intelligence rate
            check_Enemy_IntRate.IsChecked = false;
            decud_Enemy_IntRate.Text = "";

            // Apply the empty physical defense rate
            check_Enemy_PDefRate.IsChecked = false;
            decud_Enemy_PDefRate.Text = "";

            // Apply the empty magical defense rate
            check_Enemy_MDefRate.IsChecked = false;
            decud_Enemy_MDefRate.Text = "";

            // Apply the empty guard rate
            check_Enemy_GuardRate.IsChecked = false;
            decud_Enemy_GuardRate.Text = "";

            // Apply the empty evasion rate
            check_Enemy_EvaRate.IsChecked = false;
            decud_Enemy_EvaRate.Text = "";

            #endregion

            #region Defense against Attack Critical

            // Apply the defense against attack critical rate
            check_Enemy_DefCritRate.IsChecked = false;
            decud_Enemy_DefCritRate.Text = "";

            // Apply the defense against attack critical damage
            check_Enemy_DefCritDamage.IsChecked = false;
            decud_Enemy_DefCritDamage.Text = "";

            // Apply the defense against attack critical rate
            check_Enemy_DefSpCritRate.IsChecked = false;
            decud_Enemy_DefSpCritRate.Text = "";

            // Apply the defense against attack critical damage
            check_Enemy_DefSpCritDamage.IsChecked = false;
            decud_Enemy_DefSpCritDamage.Text = "";

            #endregion

            #region Defense against Skill Critical

            // Apply the defense against skill critical rate
            check_Enemy_DefSkillCritRate.IsChecked = false;
            decud_Enemy_DefSkillCritRate.Text = "";

            // Apply the defense against attack critical damage
            check_Enemy_DefSkillCritDamage.IsChecked = false;
            decud_Enemy_DefSkillCritDamage.Text = "";

            // Apply the defense against attack critical rate
            check_Enemy_DefSkillSpCritRate.IsChecked = false;
            decud_Enemy_DefSpCritRate.Text = "";

            // Apply the defense against attack critical damage
            check_Enemy_DefSkillSpCritDamage.IsChecked = false;
            decud_Enemy_DefSkillSpCritDamage.Text = "";

            #endregion

            #region Attack

            // Apply the empty attack
            check_Enemy_Atk.IsChecked = false;
            intud_Enemy_AtkInitial.Text = "";

            // Apply the empty hit rate
            check_Enemy_Hit.IsChecked = false;
            decud_Enemy_HitInitial.Text = "";

            // Apply the empty parameter attack force
            check_Enemy_ParamForce.IsChecked = false;
            decud_Enemy_StrForce.Text = "";
            decud_Enemy_DexForce.Text = "";
            decud_Enemy_AgiForce.Text = "";
            decud_Enemy_IntForce.Text = "";

            // Apply the empty empty defense attack force
            check_Enemy_DefenseForce.IsChecked = false;
            decud_Enemy_PDefForce.Text = "";
            decud_Enemy_MDefForce.Text = "";

            #endregion

            #region Critical

            // Apply the empty critcal rate
            check_Enemy_CritRate.IsChecked = false;
            decud_Enemy_CritRate.Text = "";

            // Apply the empty critcal damage
            check_Enemy_CritDamage.IsChecked = false;
            decud_Enemy_CritDamage.Text = "";

            // Apply the empty critcal guard rate reduction
            check_Enemy_CritDefGuard.IsChecked = false;
            decud_Enemy_CritDefGuard.Text = "";

            // Apply the empty critcal evasion rate reduction
            check_Enemy_CritDefEva.IsChecked = false;
            decud_Enemy_CritDefEva.Text = "";

            #endregion

            #region Special Critical

            // Apply the empty special critcal rate
            check_Enemy_SpCritRate.IsChecked = false;
            decud_Enemy_SpCritRate.Text = "";

            // Apply the empty special critcal damage
            check_Enemy_SpCritDamage.IsChecked = false;
            decud_Enemy_SpCritDamage.Text = "";

            // Apply the empty special critcal guard rate reduction
            check_Enemy_SpCritDefGuard.IsChecked = false;
            decud_Enemy_SpCritDefGuard.Text = "";

            // Apply the empty special critcal evasion rate reduction
            check_Enemy_SpCritDefEva.IsChecked = false;
            decud_Enemy_SpCritDefEva.Text = "";

            #endregion

            #region Defense

            // Apply the empty physical defense
            check_Enemy_PDef.IsChecked = false;
            intud_Enemy_PDefInitial.Text = "";

            // Apply the empty magical defense
            check_Enemy_MDef.IsChecked = false;
            intud_Enemy_MDefInitial.Text = "";

            #endregion

            // Remove the updating Flag
            updating = false;
        }

        #endregion

        #region Apply Family Data

        public void applyEnemyFamily(int index)
        {
            // Set the updating flag
            updating = true;

            // Load the new skill parameter
            if (tree_Enemy_IsFamily() && index > 0 && index <= cfg.EnemyFamily.Count && cfg.EnemyFamily.Count > 0)
            {
                exp_Enemy_Family.IsEnabled = true;
                setEnemyData(cfg.EnemyFamily[index - 1]);
            }
            else
            {
                // Empty the skill parameter
                applyEmptyEnemy();
            }

            // Remove the updating Flag
            updating = false;
        }

        private void applyEnemyFamily()
        {
            applyEnemyFamily(tree_Enemy_ID());
        }

        #endregion

        #region Apply Individual Data

        public void applyEnemyID(int index)
        {
            // Set the updating flag
            updating = true;

            // Load the new skill parameter
            if (tree_Enemy_IsIndividual() && index > 0 && index <= cfg.EnemyID.Count && cfg.EnemyID.Count > 0)
            {
                exp_Enemy_Family.IsEnabled = false;
                exp_Enemy_Family.IsExpanded = false;
                setEnemyData(cfg.EnemyID[index - 1]);
            }
            else
            {
                // Empty the skill parameter
                applyEmptyEnemy();
            }

            // Remove the updating Flag
            updating = false;
        }

        private void applyEnemyID()
        {
            applyEnemyID(tree_Enemy_ID());
        }

        #endregion

        #region Apply Default Data

        public void applyEnemyDefault()
        {
            // Set the updating flag
            updating = true;

            // Load the new skill parameter
            if (tree_Enemy_Default.IsSelected)
            {
                exp_Enemy_Family.IsEnabled = false;
                exp_Enemy_Family.IsExpanded = false;
                setEnemyData(cfg.EnemyDefault);
            }
            else
            {
                // Empty the skill parameter
                applyEmptyEnemy();
            }

            // Remove the updating Flag
            updating = false;
        }

        #endregion

        #endregion

        #region State

        #region Set Data

        private void setStateData(DataPackState state)
        {
            // Apply the state family name
            txt_State_Name.Text = state.Name;

            #region In Family

            // Create the list of passive skill in the family
            stateInFamily.Clear();
            if (state.StateFamily.Count > 0)
            {
                foreach (State states in state.StateFamily)
                {
                    stateInFamily.Add(new State(states.ID, states.Name));
                }
                stateInFamily.Sort((x, y) => x.ID.CompareTo(y.ID));
            }

            stateAvailable.Clear();
            if (cfg.StateAvailable.Count > 0)
            {
                foreach (State states in cfg.StateAvailable)
                {
                    stateAvailable.Add(new State(states.ID, states.Name));
                }
                stateAvailable.Sort((x, y) => x.ID.CompareTo(y.ID));
            }

            #endregion

            #region Parameter

            // Apply the maximum HP
            check_State_MaxHP.IsChecked = state.CustomMaxHP;
            if (state.CustomMaxHP == true)
            {
                decud_State_MaxHPMul.Value = state.MaxHPMul;
                intud_State_MaxHPAdd.Value = state.MaxHPAdd;
            }
            else
            {
                decud_State_MaxHPMul.Text = "";
                intud_State_MaxHPAdd.Text = "";
            }

            // Apply the maximum SP
            check_State_MaxSP.IsChecked = state.CustomMaxSP;
            if (state.CustomMaxSP == true)
            {
                decud_State_MaxSPMul.Value = state.MaxSPMul;
                intud_State_MaxSPAdd.Value = state.MaxSPAdd;
            }
            else
            {
                decud_State_MaxSPMul.Text = "";
                intud_State_MaxSPAdd.Text = "";
            }

            // Apply the strengh
            check_State_Str.IsChecked = state.CustomStr;
            if (state.CustomStr)
            {
                decud_State_StrMul.Value = state.StrMul;
                intud_State_StrAdd.Value = state.StrAdd;
            }
            else
            {
                decud_State_StrMul.Text = "";
                intud_State_StrAdd.Text = "";
            }

            // Apply the dexterity
            check_State_Dex.IsChecked = state.CustomDex;
            if (state.CustomDex)
            {
                decud_State_DexMul.Value = state.DexMul;
                intud_State_DexAdd.Value = state.DexAdd;
            }
            else
            {
                decud_State_DexMul.Text = "";
                intud_State_DexAdd.Text = "";
            }

            // Apply the agility
            check_State_Agi.IsChecked = state.CustomAgi;
            if (state.CustomAgi)
            {
                decud_State_AgiMul.Value = state.AgiMul;
                intud_State_AgiAdd.Value = state.AgiAdd;
            }
            else
            {
                decud_State_AgiMul.Text = "";
                intud_State_AgiAdd.Text = "";
            }

            // Apply the intelligence
            check_State_Int.IsChecked = state.CustomInt;
            if (state.CustomInt)
            {
                decud_State_IntMul.Value = state.IntMul;
                intud_State_IntAdd.Value = state.IntAdd;
            }
            else
            {
                decud_State_IntMul.Text = "";
                intud_State_IntAdd.Text = "";
            }

            #endregion

            #region Parameter Rate

            // Apply the strengh rate
            check_State_StrRate.IsChecked = state.CustomStrRate;
            if (state.CustomStrRate)
            {
                decud_State_StrRateMul.Value = state.StrRateMul;
                intud_State_StrRateAdd.Value = state.StrRateAdd;
            }
            else
            {
                decud_State_StrRateMul.Text = "";
                intud_State_StrRateAdd.Text = "";
            }

            // Apply the dexterity rate
            check_State_DexRate.IsChecked = state.CustomDexRate;
            if (state.CustomDexRate)
            {
                decud_State_DexRateMul.Value = state.DexRateMul;
                intud_State_DexRateAdd.Value = state.DexRateAdd;
            }
            else
            {
                decud_State_DexRateMul.Text = "";
                intud_State_DexRateAdd.Text = "";
            }

            // Apply the agility rate
            check_State_AgiRate.IsChecked = state.CustomAgiRate;
            if (state.CustomAgiRate)
            {
                decud_State_AgiRateMul.Value = state.AgiRateMul;
                intud_State_AgiRateAdd.Value = state.AgiRateAdd;
            }
            else
            {
                decud_State_AgiRateMul.Text = "";
                intud_State_AgiRateAdd.Text = "";
            }

            // Apply the intelligence rate
            check_State_IntRate.IsChecked = state.CustomIntRate;
            if (state.CustomIntRate)
            {
                decud_State_IntRateMul.Value = state.IntRateMul;
                intud_State_IntRateAdd.Value = state.IntRateAdd;
            }
            else
            {
                decud_State_IntRateMul.Text = "";
                intud_State_IntRateAdd.Text = "";
            }

            // Apply the physical defense rate
            check_State_PDefRate.IsChecked = state.CustomPDefRate;
            if (state.CustomPDefRate)
            {
                decud_State_PDefRateMul.Value = state.PDefRateMul;
                intud_State_PDefRateAdd.Value = state.PDefRateAdd;
            }
            else
            {
                decud_State_PDefRateMul.Text = "";
                intud_State_PDefRateAdd.Text = "";
            }

            // Apply the magical defense rate
            check_State_MDefRate.IsChecked = state.CustomMDefRate;
            if (state.CustomMDefRate)
            {
                decud_State_MDefRateMul.Value = state.MDefRateMul;
                intud_State_MDefRateAdd.Value = state.MDefRateAdd;
            }
            else
            {
                decud_State_MDefRateMul.Text = "";
                intud_State_MDefRateAdd.Text = "";
            }

            // Apply the guard rate
            check_State_GuardRate.IsChecked = state.CustomGuardRate;
            if (state.CustomGuardRate)
            {
                decud_State_GuardRateMul.Value = state.GuardRateMul;
                intud_State_GuardRateAdd.Value = state.GuardRateAdd;
            }
            else
            {
                decud_State_GuardRateMul.Text = "";
                intud_State_GuardRateAdd.Text = "";
            }

            // Apply the evasion rate
            check_State_EvaRate.IsChecked = state.CustomEvaRate;
            if (state.CustomEvaRate)
            {
                decud_State_EvaRateMul.Value = state.EvaRateMul;
                intud_State_EvaRateAdd.Value = state.EvaRateAdd;
            }
            else
            {
                decud_State_EvaRateMul.Text = "";
                intud_State_EvaRateAdd.Text = "";
            }

            #endregion

            #region Defense against Attack Critical

            // Apply the defense against attack critical rate
            check_State_DefCritRate.IsChecked = state.CustomDefCritRate;
            if (state.CustomDefCritRate)
            {
                decud_State_DefCritRateMul.Value = state.DefCritRateMul;
                intud_State_DefCritRateAdd.Value = state.DefCritRateAdd;
            }
            else
            {
                decud_State_DefCritRateMul.Text = "";
                intud_State_DefCritRateAdd.Text = "";
            }

            // Apply the defense against attack critical damage
            check_State_DefCritDamage.IsChecked = state.CustomDefCritDamage;
            if (state.CustomDefCritDamage)
            {
                decud_State_DefCritDamageMul.Value = state.DefCritDamageMul;
                intud_State_DefCritDamageAdd.Value = state.DefCritDamageAdd;
            }
            else
            {
                decud_State_DefCritDamageMul.Text = "";
                intud_State_DefCritDamageAdd.Text = "";
            }

            // Apply the defense against attack critical rate
            check_State_DefSpCritRate.IsChecked = state.CustomDefSpCritRate;
            if (state.CustomDefSpCritRate)
            {
                decud_State_DefSpCritRateMul.Value = state.DefSpCritRateMul;
                intud_State_DefSpCritRateAdd.Value = state.DefSpCritRateAdd;
            }
            else
            {
                decud_State_DefSpCritRateMul.Text = "";
                intud_State_DefSpCritRateAdd.Text = "";
            }

            // Apply the defense against attack critical damage
            check_State_DefSpCritDamage.IsChecked = state.CustomDefSpCritDamage;
            if (state.CustomDefSpCritDamage)
            {
                decud_State_DefSpCritDamageMul.Value = state.DefSpCritDamageMul;
                intud_State_DefSpCritDamageAdd.Value = state.DefSpCritDamageAdd;
            }
            else
            {
                decud_State_DefSpCritDamageMul.Text = "";
                intud_State_DefSpCritDamageAdd.Text = "";
            }

            #endregion

            #region Defense against Skill Critical

            // Apply the defense against skill critical rate
            check_State_DefSkillCritRate.IsChecked = state.CustomDefSkillCritRate;
            if (state.CustomDefSkillCritRate)
            {
                decud_State_DefSkillCritRateMul.Value = state.DefSkillCritRateMul;
                intud_State_DefSkillCritRateAdd.Value = state.DefSkillCritRateAdd;
            }
            else
            {
                decud_State_DefSkillCritRateMul.Text = "";
                intud_State_DefSkillCritRateAdd.Text = "";
            }

            // Apply the defense against attack critical damage
            check_State_DefSkillCritDamage.IsChecked = state.CustomDefSkillCritDamage;
            if (state.CustomDefSkillCritDamage)
            {
                decud_State_DefSkillCritDamageMul.Value = state.DefSkillCritDamageMul;
                intud_State_DefSkillCritDamageAdd.Value = state.DefSkillCritDamageAdd;
            }
            else
            {
                decud_State_DefSkillCritDamageMul.Text = "";
                intud_State_DefSkillCritDamageAdd.Text = "";
            }

            // Apply the defense against attack critical rate
            check_State_DefSkillSpCritRate.IsChecked = state.CustomDefSkillSpCritRate;
            if (state.CustomDefSkillSpCritRate)
            {
                decud_State_DefSkillSpCritRateMul.Value = state.DefSkillSpCritRateMul;
                intud_State_DefSkillSpCritRateAdd.Value = state.DefSkillSpCritRateAdd;
            }
            else
            {
                decud_State_DefSkillSpCritRateMul.Text = "";
                intud_State_DefSkillSpCritRateAdd.Text = "";
            }

            // Apply the defense against attack critical damage
            check_State_DefSkillSpCritDamage.IsChecked = state.CustomDefSkillSpCritDamage;
            if (state.CustomDefSkillSpCritDamage)
            {
                decud_State_DefSkillSpCritDamageMul.Value = state.DefSkillSpCritDamageMul;
                intud_State_DefSkillSpCritDamageAdd.Value = state.DefSkillSpCritDamageAdd;
            }
            else
            {
                decud_State_DefSkillSpCritDamageMul.Text = "";
                intud_State_DefSkillSpCritDamageAdd.Text = "";
            }

            #endregion

            #region Attack

            // Apply the attack
            check_State_Atk.IsChecked = state.CustomAtk;
            if (state.CustomAtk)
            {
                decud_State_AtkMul.Value = state.AtkMul;
                intud_State_AtkAdd.Value = state.AtkAdd;
            }
            else
            {
                decud_State_AtkMul.Text = "";
                intud_State_AtkAdd.Text = "";
            }

            // Apply the hit rate
            check_State_Hit.IsChecked = state.CustomHit;
            if (state.CustomHit)
            {
                decud_State_HitMul.Value = state.HitMul;
                intud_State_HitAdd.Value = state.HitAdd;
            }
            else
            {
                decud_State_HitMul.Text = "";
                intud_State_HitAdd.Text = "";
            }

            #endregion

            #region Critical

            // Apply the critcal rate
            check_State_CritRate.IsChecked = state.CustomCritRate;
            if (state.CustomCritRate)
            {
                decud_State_CritRateMul.Value = state.CritRateMul;
                intud_State_CritRateAdd.Value = state.CritRateAdd;
            }
            else
            {
                decud_State_CritRateMul.Text = "";
                intud_State_CritRateAdd.Text = "";
            }

            // Apply the critcal damage
            check_State_CritDamage.IsChecked = state.CustomCritDamage;
            if (state.CustomCritDamage)
            {
                decud_State_CritDamageMul.Value = state.CritDamageMul;
                intud_State_CritDamageAdd.Value = state.CritDamageAdd;
            }
            else
            {
                decud_State_CritDamageMul.Text = "";
                intud_State_CritDamageAdd.Text = "";
            }

            // Apply the critcal guard rate reduction
            check_State_CritDefGuard.IsChecked = state.CustomCritDefGuard;
            if (state.CustomCritDefGuard)
            {
                decud_State_CritDefGuardMul.Value = state.CritDefGuardMul;
                intud_State_CritDefGuardAdd.Value = state.CritDefGuardAdd;
            }
            else
            {
                decud_State_CritDefGuardMul.Text = "";
                intud_State_CritDefGuardAdd.Text = "";
            }

            // Apply the critcal evasion rate reduction
            check_State_CritDefEva.IsChecked = state.CustomCritDefEva;
            if (state.CustomCritDefEva)
            {
                decud_State_CritDefEvaMul.Value = state.CritDefEvaMul;
                intud_State_CritDefEvaAdd.Value = state.CritDefEvaAdd;
            }
            else
            {
                decud_State_CritDefEvaMul.Text = "";
                intud_State_CritDefEvaAdd.Text = "";
            }

            #endregion

            #region Special Critical

            // Apply the special critcal rate
            check_State_SpCritRate.IsChecked = state.CustomSpCritRate;
            if (state.CustomSpCritRate)
            {
                decud_State_SpCritRateMul.Value = state.SpCritRateMul;
                intud_State_SpCritRateAdd.Value = state.SpCritRateAdd;
            }
            else
            {
                decud_State_SpCritRateMul.Text = "";
                intud_State_SpCritRateAdd.Text = "";
            }

            // Apply the special critcal damage
            check_State_SpCritDamage.IsChecked = state.CustomSpCritDamage;
            if (state.CustomSpCritDamage)
            {
                decud_State_SpCritDamageMul.Value = state.SpCritDamageMul;
                intud_State_SpCritDamageAdd.Value = state.SpCritDamageAdd;
            }
            else
            {
                decud_State_SpCritDamageMul.Text = "";
                intud_State_SpCritDamageAdd.Text = "";
            }

            // Apply the special critcal guard rate reduction
            check_State_SpCritDefGuard.IsChecked = state.CustomSpCritDefGuard;
            if (state.CustomSpCritDefGuard)
            {
                decud_State_SpCritDefGuardMul.Value = state.SpCritDefGuardMul;
                intud_State_SpCritDefGuardAdd.Value = state.SpCritDefGuardAdd;
            }
            else
            {
                decud_State_SpCritDefGuardMul.Text = "";
                intud_State_SpCritDefGuardAdd.Text = "";
            }

            // Apply the special critcal evasion rate reduction
            check_State_SpCritDefEva.IsChecked = state.CustomSpCritDefEva;
            if (state.CustomSpCritDefEva)
            {
                decud_State_SpCritDefEvaMul.Value = state.SpCritDefEvaMul;
                intud_State_SpCritDefEvaAdd.Value = state.SpCritDefEvaAdd;
            }
            else
            {
                decud_State_SpCritDefEvaMul.Text = "";
                intud_State_SpCritDefEvaAdd.Text = "";
            }

            #endregion

            #region Defense

            // Apply the physical defense
            check_State_PDef.IsChecked = state.CustomPDef;
            if (state.CustomPDef)
            {
                decud_State_PDefMul.Value = state.PDefMul;
                intud_State_PDefAdd.Value = state.PDefAdd;
            }
            else
            {
                decud_State_PDefMul.Text = "";
                intud_State_PDefAdd.Text = "";
            }

            // Apply the magical defense
            check_State_MDef.IsChecked = state.CustomMDef;
            if (state.CustomMDef)
            {
                decud_State_MDefMul.Value = state.MDefMul;
                intud_State_MDefAdd.Value = state.MDefAdd;
            }
            else
            {
                decud_State_MDefMul.Text = "";
                intud_State_MDefAdd.Text = "";
            }

            #endregion
        }

        #endregion

        #region Apply Empty Data

        public void applyEmptyState()
        {
            // Set the updating flag
            updating = true;

            // Apply the empty state family name
            txt_State_Name.Text = "";

            #region In Family

            stateInFamily.Clear();

            stateAvailable.Clear();
            if (cfg.StateAvailable.Count > 0)
            {
                foreach (State state in cfg.StateAvailable)
                {
                    stateAvailable.Add(new State(state.ID, state.Name));
                }
                stateAvailable.Sort((x, y) => x.ID.CompareTo(y.ID));
            }

            #endregion

            #region Parameter

            // Apply the empty maximum HP
            check_State_MaxHP.IsChecked = false;
            decud_State_MaxHPMul.Text = "";
            intud_State_MaxHPAdd.Text = "";

            // Apply the empty maximum SP
            check_State_MaxSP.IsChecked = false;
            decud_State_MaxSPMul.Text = "";
            intud_State_MaxSPAdd.Text = "";

            // Apply the empty strengh
            check_State_Str.IsChecked = false;
            decud_State_StrMul.Text = "";
            intud_State_StrAdd.Text = "";

            // Apply the empty dexterity
            check_State_Dex.IsChecked = false;
            decud_State_DexMul.Text = "";
            intud_State_DexAdd.Text = "";

            // Apply the empty agility
            check_State_Agi.IsChecked = false;
            decud_State_AgiMul.Text = "";
            intud_State_AgiAdd.Text = "";

            // Apply the empty intelligence
            check_State_Int.IsChecked = false;
            decud_State_IntMul.Text = "";
            intud_State_IntAdd.Text = "";

            // Apply the empty guard rate
            check_State_GuardRate.IsChecked = false;
            decud_State_GuardRateMul.Text = "";
            intud_State_GuardRateAdd.Text = "";

            // Apply the empty evasion rate
            check_State_EvaRate.IsChecked = false;
            decud_State_EvaRateMul.Text = "";
            intud_State_EvaRateAdd.Text = "";

            #endregion

            #region Parameter Rate

            // Apply the empty strengh rate
            check_State_StrRate.IsChecked = false;
            decud_State_StrRateMul.Text = "";
            intud_State_StrRateAdd.Text = "";

            // Apply the empty dexterity rate
            check_State_DexRate.IsChecked = false;
            decud_State_DexRateMul.Text = "";
            intud_State_DexRateAdd.Text = "";

            // Apply the empty agility rate
            check_State_AgiRate.IsChecked = false;
            decud_State_AgiRateMul.Text = "";
            intud_State_AgiRateAdd.Text = "";

            // Apply the empty intelligence rate
            check_State_IntRate.IsChecked = false;
            decud_State_IntRateMul.Text = "";
            intud_State_IntRateAdd.Text = "";

            // Apply the empty physical defense rate
            check_State_PDefRate.IsChecked = false;
            decud_State_PDefRateMul.Text = "";
            intud_State_PDefRateAdd.Text = "";

            // Apply the empty magical defense rate
            check_State_MDefRate.IsChecked = false;
            decud_State_MDefRateMul.Text = "";
            intud_State_MDefRateAdd.Text = "";

            #endregion

            #region Defense against Attack Critical

            // Apply the defense against attack critical rate
            check_State_DefCritRate.IsChecked = false;
            decud_State_DefCritRateMul.Text = "";
            intud_State_DefCritRateAdd.Text = "";

            // Apply the defense against attack critical damage
            check_State_DefCritDamage.IsChecked = false;
            decud_State_DefCritDamageMul.Text = "";
            intud_State_DefCritDamageAdd.Text = "";

            // Apply the defense against attack critical rate
            check_State_DefSpCritRate.IsChecked = false;
            decud_State_DefSpCritRateMul.Text = "";
            intud_State_DefSpCritRateAdd.Text = "";

            // Apply the defense against attack critical damage
            check_State_DefSpCritDamage.IsChecked = false;
            decud_State_DefSpCritDamageMul.Text = "";
            intud_State_DefSpCritDamageAdd.Text = "";

            #endregion

            #region Defense against Skill Critical

            // Apply the defense against skill critical rate
            check_State_DefSkillCritRate.IsChecked = false;
            decud_State_DefSkillCritRateMul.Text = "";
            intud_State_DefSkillCritRateAdd.Text = "";

            // Apply the defense against attack critical damage
            check_State_DefSkillCritDamage.IsChecked = false;
            decud_State_DefSkillCritDamageMul.Text = "";
            intud_State_DefSkillCritDamageAdd.Text = "";

            // Apply the defense against attack critical rate
            check_State_DefSkillSpCritRate.IsChecked = false;
            decud_State_DefSkillSpCritRateMul.Text = "";
            intud_State_DefSkillSpCritRateAdd.Text = "";

            // Apply the defense against attack critical damage
            check_State_DefSkillSpCritDamage.IsChecked = false;
            decud_State_DefSkillSpCritDamageMul.Text = "";
            intud_State_DefSkillSpCritDamageAdd.Text = "";

            #endregion

            #region Attack

            // Apply the empty attack
            check_State_Atk.IsChecked = false;
            decud_State_AtkMul.Text = "";
            intud_State_AtkAdd.Text = "";

            // Apply the empty hit rate
            check_State_Hit.IsChecked = false;
            decud_State_HitMul.Text = "";
            intud_State_HitAdd.Text = "";

            #endregion

            #region Critical

            // Apply the critcal rate
            check_State_CritRate.IsChecked = false;
            decud_State_CritRateMul.Text = "";
            intud_State_CritRateAdd.Text = "";

            // Apply the critcal damage
            check_State_CritDamage.IsChecked = false;
            decud_State_CritDamageMul.Text = "";
            intud_State_CritDamageAdd.Text = "";

            // Apply the critcal guard rate reduction
            check_State_CritDefGuard.IsChecked = false;
            decud_State_CritDefGuardMul.Text = "";
            intud_State_CritDefGuardAdd.Text = "";

            // Apply the critcal evasion rate reduction
            check_State_CritDefEva.IsChecked = false;
            decud_State_CritDefEvaMul.Text = "";
            intud_State_CritDefEvaAdd.Text = "";

            #endregion

            #region Special Critical

            // Apply the special critcal rate
            check_State_SpCritRate.IsChecked = false;
            decud_State_SpCritRateMul.Text = "";
            intud_State_SpCritRateAdd.Text = "";

            // Apply the special critcal damage
            check_State_SpCritDamage.IsChecked = false;
            decud_State_SpCritDamageMul.Text = "";
            intud_State_SpCritDamageAdd.Text = "";

            // Apply the special critcal guard rate reduction
            check_State_SpCritDefGuard.IsChecked = false;
            decud_State_SpCritDefGuardMul.Text = "";
            intud_State_SpCritDefGuardAdd.Text = "";

            // Apply the special critcal evasion rate reduction
            check_State_SpCritDefEva.IsChecked = false;
            decud_State_SpCritDefEvaMul.Text = "";
            intud_State_SpCritDefEvaAdd.Text = "";

            #endregion

            #region Defense

            // Apply the empty physical defense
            check_State_PDef.IsChecked = false;
            decud_State_PDefMul.Text = "";
            intud_State_PDefAdd.Text = "";

            // Apply the empty magical defense
            check_State_MDef.IsChecked = false;
            decud_State_MDefMul.Text = "";
            intud_State_MDefAdd.Text = "";

            #endregion

            // Remove the updating Flag
            updating = false;
        }

        #endregion

        #region Apply Family Data

        public void applyStateFamily(int index)
        {
            // Set the updating flag
            updating = true;

            // Load the new skill parameter
            if (tree_State_IsFamily() && index > 0 && index <= cfg.StateFamily.Count && cfg.StateFamily.Count > 0)
            {
                exp_State_Family.IsEnabled = true;
                setStateData(cfg.StateFamily[index - 1]);
            }
            else
            {
                // Empty the skill parameter
                applyEmptyState();
            }

            // Remove the updating Flag
            updating = false;
        }

        private void applyStateFamily()
        {
            applyStateFamily(tree_State_ID());
        }

        #endregion

        #region Apply Individual Data

        public void applyStateID(int index)
        {
            // Set the updating flag
            updating = true;

            // Load the new skill parameter
            if (tree_State_IsIndividual() && index > 0 && index <= cfg.StateID.Count && cfg.StateID.Count > 0)
            {
                exp_State_Family.IsEnabled = false;
                exp_State_Family.IsExpanded = false;
                setStateData(cfg.StateID[index - 1]);
            }
            else
            {
                // Empty the skill parameter
                applyEmptyState();
            }

            // Remove the updating Flag
            updating = false;
        }

        private void applyStateID()
        {
            applyStateID(tree_State_ID());
        }

        #endregion

        #region Apply Default Data

        public void applyStateDefault()
        {
            // Set the updating flag
            updating = true;

            // Load the new skill parameter
            if (tree_State_Default.IsSelected)
            {
                exp_State_Family.IsEnabled = false;
                exp_State_Family.IsExpanded = false;
                setStateData(cfg.StateDefault);
            }
            else
            {
                // Empty the skill parameter
                applyEmptyState();
            }

            // Remove the updating Flag
            updating = false;
        }

        #endregion

        #endregion

        #endregion

        #region Reset Configuration

        private void resetConfiguration()
        {
            #region General

            resetGeneral();

            #endregion

            #region Default

            resetDefault();

            #endregion

            #region Actor

            resetActorFamily();
            resetActorID();
            resetActorDefault();

            #endregion

            #region Class

            resetClassFamily();
            resetClassID();
            resetClassDefault();

            #endregion

            #region Skill

            resetSkillFamily();
            resetSkillID();
            resetSkillDefault();

            #endregion

            #region Passive Skill

            resetPassiveSkillFamily();
            resetPassiveSkillID();
            resetPassiveSkillDefault();

            #endregion

            #region Weapon

            resetWeaponFamily();
            resetWeaponID();
            resetWeaponDefault();

            #endregion

            #region Armor

            resetArmorFamily();
            resetArmorID();
            resetArmorDefault();

            #endregion

            #region Enemy

            resetEnemyFamily();
            resetEnemyID();
            resetEnemyDefault();

            #endregion

            #region State

            resetStateFamily();
            resetStateID();
            resetStateDefault();

            #endregion
        }

        #region General

        private void resetGeneral()
        {
            cfg.General.Reset();
            applyGeneral();
        }

        #endregion

        #region Default

        private void resetDefault()
        {
            cfg.Default.Reset();
            cfg.Default.SetupList();
            applyDefault();
        }

        #endregion

        #region Actor

        private void resetActorFamily()
        {
            cfg.ActorFamily.Clear();
            tree_Actor_Family.Items.Clear();
            actorAvailable.Clear();
            if (gameData.Actors.Count > 0)
            {
                foreach (Actor actor in gameData.Actors)
                {
                    actorAvailable.Add(new Actor(actor.ID, actor.Name));
                }
            }
        }

        private void resetActorID()
        {
            if (cfg.ActorID.Count > 0)
            {
                for (int i = 0; i < cfg.ActorID.Count; i++)
                {
                    cfg.ActorID[i].Reset();
                }
            }
            else
            {
                cfg.ActorID.Clear();
            }
        }

        private void resetActorDefault()
        {
            cfg.ActorDefault.Reset();
        }

        #endregion

        #region Class

        private void resetClassFamily()
        {
            cfg.ClassFamily.Clear();
            tree_Class_Family.Items.Clear();
            classAvailable.Clear();
            if (gameData.Classes.Count > 0)
            {
                foreach (Class classes in gameData.Classes)
                {
                    classAvailable.Add(new Class(classes.ID, classes.Name));
                }
            }
        }

        private void resetClassID()
        {
            if (cfg.ClassID.Count > 0)
            {
                for (int i = 0; i < cfg.ClassID.Count; i++)
                {
                    cfg.ClassID[i].Reset();
                }
            }
            else
            {
                cfg.ClassID.Clear();
            }
        }

        private void resetClassDefault()
        {
            cfg.ClassDefault.Reset();
        }

        #endregion

        #region Skill

        private void resetSkillFamily()
        {
            cfg.SkillFamily.Clear();
            tree_Skill_Family.Items.Clear();
            skillAvailable.Clear();
            if (gameData.Skills.Count > 0)
            {
                foreach (Skill skill in gameData.Skills)
                {
                    skillAvailable.Add(new Skill(skill.ID, skill.Name));
                }
            }
        }

        private void resetSkillID()
        {
            if (cfg.SkillID.Count > 0)
            {
                for (int i = 0; i < cfg.SkillID.Count; i++)
                {
                    cfg.SkillID[i].Reset();
                }
            }
            else
            {
                cfg.SkillID.Clear();
            }
        }

        private void resetSkillDefault()
        {
            cfg.SkillDefault.Reset();
        }

        #endregion

        #region Passive Skill

        private void resetPassiveSkillFamily()
        {
            cfg.PassiveSkillFamily.Clear();
            tree_PassiveSkill_Family.Items.Clear();
            passiveSkillAvailable.Clear();
            if (gameData.Skills.Count > 0)
            {
                foreach (Skill skill in gameData.Skills)
                {
                    passiveSkillAvailable.Add(new Skill(skill.ID, skill.Name));
                }
            }
        }

        private void resetPassiveSkillID()
        {
            if (cfg.PassiveSkillID.Count > 0)
            {
                for (int i = 0; i < cfg.PassiveSkillID.Count; i++)
                {
                    cfg.PassiveSkillID[i].Reset();
                }
            }
            else
            {
                cfg.PassiveSkillID.Clear();
            }
        }

        private void resetPassiveSkillDefault()
        {
            cfg.PassiveSkillDefault.Reset();
        }

        #endregion

        #region Weapon

        private void resetWeaponFamily()
        {
            cfg.WeaponFamily.Clear();
            tree_Weapon_Family.Items.Clear();
            weaponAvailable.Clear();
            if (gameData.Weapons.Count > 0)
            {
                foreach (Weapon weapon in gameData.Weapons)
                {
                    weaponAvailable.Add(new Weapon(weapon.ID, weapon.Name));
                }
            }
        }

        private void resetWeaponID()
        {
            if (cfg.WeaponID.Count > 0)
            {
                for (int i = 0; i < cfg.WeaponID.Count; i++)
                {
                    cfg.WeaponID[i].Reset();
                }
            }
            else
            {
                cfg.WeaponID.Clear();
            }
        }

        private void resetWeaponDefault()
        {
            cfg.WeaponDefault.Reset();
        }

        #endregion

        #region Armor

        private void resetArmorFamily()
        {
            cfg.ArmorFamily.Clear();
            tree_Armor_Family.Items.Clear();
            armorAvailable.Clear();
            if (gameData.Armors.Count > 0)
            {
                foreach (Armor armor in gameData.Armors)
                {
                    armorAvailable.Add(new Armor(armor.ID, armor.Name));
                }
            }
        }

        private void resetArmorID()
        {
            if (cfg.ArmorID.Count > 0)
            {
                for (int i = 0; i < cfg.ArmorID.Count; i++)
                {
                    cfg.ArmorID[i].Reset();
                }
            }
            else
            {
                cfg.ArmorID.Clear();
            }
        }

        private void resetArmorDefault()
        {
            cfg.ArmorDefault.Reset();
        }

        #endregion

        #region Enemy

        private void resetEnemyFamily()
        {
            cfg.EnemyFamily.Clear();
            tree_Enemy_Family.Items.Clear();
            enemyAvailable.Clear();
            if (gameData.Enemies.Count > 0)
            {
                foreach (Enemy enemies in gameData.Enemies)
                {
                    enemyAvailable.Add(new Enemy(enemies.ID, enemies.Name));
                }
            }
        }

        private void resetEnemyID()
        {
            if (cfg.EnemyID.Count > 0)
            {
                for (int i = 0; i < cfg.EnemyID.Count; i++)
                {
                    cfg.EnemyID[i].Reset();
                }
            }
            else
            {
                cfg.EnemyID.Clear();
            }
        }

        private void resetEnemyDefault()
        {
            cfg.EnemyDefault.Reset();
        }

        #endregion

        #region State

        private void resetStateFamily()
        {
            cfg.StateFamily.Clear();
            tree_State_Family.Items.Clear();
            stateAvailable.Clear();
            if (gameData.States.Count > 0)
            {
                foreach (State state in gameData.States)
                {
                    stateAvailable.Add(new State(state.ID, state.Name));
                }
            }
        }

        private void resetStateID()
        {
            if (cfg.StateID.Count > 0)
            {
                for (int i = 0; i < cfg.StateID.Count; i++)
                {
                    cfg.StateID[i].Reset();
                }
            }
            else
            {
                cfg.StateID.Clear();
            }
        }

        private void resetStateDefault()
        {
            cfg.StateDefault.Reset();
        }

        #endregion

        #endregion

        #region Store Configuration

        private void storeConfiguration()
        {
            storeGeneral();

            storeDefault();

            #region Actor

            if (tree_Actor_IsFamily())
            {
                storeActorFamily();
            }
            else if (tree_Actor_IsIndividual())
            {
                storeActorID();
            }
            else if (tree_Actor_Default.IsSelected)
            {
                storeActorDefault();
            }

            #endregion

            #region Class

            if (tree_Class_IsFamily())
            {
                storeClassFamily();
            }
            else if (tree_Class_IsIndividual())
            {
                storeClassID();
            }
            else if (tree_Class_Default.IsSelected)
            {
                storeClassDefault();
            }

            #endregion

            #region Skill

            if (tree_Skill_IsFamily())
            {
                storeSkillFamily();
            }
            else if (tree_Skill_IsIndividual())
            {
                storeSkillID();
            }
            else if (tree_Skill_Default.IsSelected)
            {
                storeSkillDefault();
            }

            #endregion

            #region Passive Skill

            if (tree_PassiveSkill_IsFamily())
            {
                storePassiveSkillFamily();
            }
            else if (tree_PassiveSkill_IsIndividual())
            {
                storePassiveSkillID();
            }
            else if (tree_PassiveSkill_Default.IsSelected)
            {
                storePassiveSkillDefault();
            }

            #endregion

            #region Weapon

            if (tree_Weapon_IsFamily())
            {
                storeWeaponFamily();
            }
            else if (tree_Weapon_IsIndividual())
            {
                storeWeaponID();
            }
            else if (tree_Weapon_Default.IsSelected)
            {
                storeWeaponDefault();
            }

            #endregion

            #region Armor

            if (tree_Armor_IsFamily())
            {
                storeArmorFamily();
            }
            else if (tree_Armor_IsIndividual())
            {
                storeArmorID();
            }
            else if (tree_Armor_Default.IsSelected)
            {
                storeArmorDefault();
            }

            #endregion

            #region Enemy

            if (tree_Enemy_IsFamily())
            {
                storeEnemyFamily();
            }
            else if (tree_Enemy_IsIndividual())
            {
                storeEnemyID();
            }
            else if (tree_Enemy_Default.IsSelected)
            {
                storeEnemyDefault();
            }

            #endregion

            #region State

            if (tree_State_IsFamily())
            {
                storeStateFamily();
            }
            else if (tree_State_IsIndividual())
            {
                storeStateID();
            }
            else if (tree_State_Default.IsSelected)
            {
                storeStateDefault();
            }

            #endregion
        }

        #region General

        private void storeGeneral(string checkBox)
        {
            // Stat Limit Bypass
            if (check_General_StatLimitBypass.IsChecked == true)
            {
                cfg.General.SetLimitBypass = true;
                if (checkBox != "check_General_StatLimitBypass")
                {
                    cfg.General.SetHPSPMaxLimit = intud_General_HPSPMax.Value ?? cfg.General.SetHPSPMaxLimit;
                    cfg.General.SetStatMaxLimit = intud_General_StatMax.Value ?? cfg.General.SetStatMaxLimit;
                }
            }
            else
            {
                cfg.General.SetLimitBypass = false;
                cfg.General.ResetStatMaximum();
            }

            // Attack Skill Rate Type
            cfg.General.SkillParamRateType = combo_General_ParamRateType.SelectedIndex;
            cfg.General.SkillDefenseRateType = combo_General_DefenseRateType.SelectedIndex;

            // Actor class behavior
            cfg.General.OrderEquipmentList = combo_General_OrderEquipmentList.SelectedIndex;
            cfg.General.OrderEquipmentMultiplier = combo_General_OrderEquipmentMultiplier.SelectedIndex;
            cfg.General.OrderEquipmentFlags = combo_General_OrderEquipmentFlags.SelectedIndex;
            cfg.General.OrderHandReduce = combo_General_OrderHandReduce.SelectedIndex;
            cfg.General.OrderUnarmedAttackForce = combo_General_OrderUnarmedAttackForce.SelectedIndex;

            // Cursed color
            cfg.General.CursedColorRed = color_General_Cursed.R;
            cfg.General.CursedColorGreen = color_General_Cursed.G;
            cfg.General.CursedColorBlue = color_General_Cursed.B;
            cfg.General.CursedColorAlpha = color_General_Cursed.A;

            // Cursed equipment Setting
            if (check_General_ShowCursed.IsChecked == true)
            {
                cfg.General.SetShowCursed = true;
                if (checkBox != "check_General_ShowCursed")
                {
                    if (check_General_BlockCursed.IsChecked == true)
                    {
                        cfg.General.SetBlockCursed = true;
                    }
                    else
                    {
                        cfg.General.SetBlockCursed = false;
                    }
                }
            }
            else
            {
                cfg.General.SetShowCursed = false;
                cfg.General.SetBlockCursed = false;
            }
        }

        private void storeGeneral()
        {
            storeGeneral("");
        }

        #endregion

        #region Default

        private void storeDefault()
        {
            #region Equipment

            // Clear the equip type and list
            cfg.Default.EquipType.Clear();
            cfg.Default.EquipList.Clear();

            // Store the equip type list
            if (defaultEquipType.Count > 0)
            {
                foreach (EquipType equip in defaultEquipType)
                {
                    cfg.Default.EquipType.Add(new EquipType(equip.ID, equip.Name));
                }

                if (list_Default_EquipType.SelectedIndex >= 5)
                {
                    cfg.Default.EquipType[list_Default_EquipType.SelectedIndex].Name = txt_Default_NameEquipType.Text;
                }
            }

            // Store the equip id list
            if (defaultEquipList.Count > 0)
            {
                foreach (EquipType equip in defaultEquipList)
                {
                    cfg.Default.EquipList.Add(new EquipType(equip.ID, equip.Name));
                }

                if (list_Default_EquipList.SelectedIndex >= 0)
                {
                    cfg.Default.EquipList[list_Default_EquipList.SelectedIndex].Name = txt_Default_NameEquipList.Text;
                }
            }

            // Store dual hold
            if (check_Default_DualHold.IsChecked == true)
            {
                cfg.Default.DualHold = true;
            }
            else
            {
                cfg.Default.DualHold = false;
            }

            // Store dual hold name
            cfg.Default.DualHoldNameWeapon = txt_Default_DualHoldNameWeapon.Text;
            cfg.Default.DualHoldNameShield = txt_Default_DualHoldNameShield.Text;

            // Store dual hold multiplier
            cfg.Default.DualHoldMulWeapon = decud_Default_DualHoldMulWeapon.Value ?? cfg.Default.DualHoldMulWeapon;
            cfg.Default.DualHoldMulShield = decud_Default_DualHoldMulShield.Value ?? cfg.Default.DualHoldMulShield;

            // Store the shield bypass
            if (check_Default_ShieldBypass.IsChecked == true)
            {
                cfg.Default.ShieldBypass = true;
            }
            else
            {
                cfg.Default.ShieldBypass = false;
            }
            cfg.Default.ShieldBypassMul = decud_Default_ShieldBypass.Value ?? cfg.Default.ShieldBypassMul;

            // Store the weapon bypass
            if (check_Default_WeaponBypass.IsChecked == true)
            {
                cfg.Default.WeaponBypass = true;
            }
            else
            {
                cfg.Default.WeaponBypass = false;
            }
            cfg.Default.WeaponBypassMul = decud_Default_WeaponBypass.Value ?? cfg.Default.WeaponBypassMul;

            // Store the reduce hand
            cfg.Default.ReduceHand = intud_Default_ReduceHand.Value ?? cfg.Default.ReduceHand;
            cfg.Default.ReduceHandMul = decud_Default_ReduceHandMul.Value ?? cfg.Default.ReduceHandMul;

            #endregion

            #region Parameter

            // Store maximum HP
            cfg.Default.MaxHPInitial = intud_Default_MaxHPInitial.Value ?? cfg.Default.MaxHPInitial;
            cfg.Default.MaxHPFinal = intud_Default_MaxHPFinal.Value ?? cfg.Default.MaxHPFinal;

            // Store maximum SP
            cfg.Default.MaxSPInitial = intud_Default_MaxSPInitial.Value ?? cfg.Default.MaxSPInitial;
            cfg.Default.MaxSPFinal = intud_Default_MaxSPFinal.Value ?? cfg.Default.MaxSPFinal;

            // Store strengh
            cfg.Default.StrInitial = intud_Default_StrInitial.Value ?? cfg.Default.StrInitial;
            cfg.Default.StrFinal = intud_Default_StrFinal.Value ?? cfg.Default.StrFinal;

            // Store dexterity
            cfg.Default.DexInitial = intud_Default_DexInitial.Value ?? cfg.Default.DexInitial;
            cfg.Default.DexFinal = intud_Default_DexFinal.Value ?? cfg.Default.DexFinal;

            // Store agility
            cfg.Default.AgiInitial = intud_Default_AgiInitial.Value ?? cfg.Default.AgiInitial;
            cfg.Default.AgiFinal = intud_Default_AgiFinal.Value ?? cfg.Default.AgiFinal;

            // Store intelligence
            cfg.Default.IntInitial = intud_Default_IntInitial.Value ?? cfg.Default.IntInitial;
            cfg.Default.IntFinal = intud_Default_IntFinal.Value ?? cfg.Default.IntFinal;

            #endregion

            #region Parameter Rate

            // Store strengh rate
            cfg.Default.StrRate = decud_Default_StrRate.Value ?? cfg.Default.StrRate;

            // Store dexterity rate
            cfg.Default.DexRate = decud_Default_DexRate.Value ?? cfg.Default.DexRate;

            // Store agility rate
            cfg.Default.AgiRate = decud_Default_AgiRate.Value ?? cfg.Default.AgiRate;

            // Store intelligence rate
            cfg.Default.IntRate = decud_Default_IntRate.Value ?? cfg.Default.IntRate;

            // Store physical defense rate
            cfg.Default.PDefRate = decud_Default_PDefRate.Value ?? cfg.Default.PDefRate;

            // Store magical defense rate
            cfg.Default.MDefRate = decud_Default_MDefRate.Value ?? cfg.Default.MDefRate;

            // Store guard rate
            cfg.Default.GuardRate = decud_Default_GuardRate.Value ?? cfg.Default.GuardRate;

            // Store evasion rate
            cfg.Default.EvaRate = decud_Default_EvaRate.Value ?? cfg.Default.EvaRate;

            #endregion

            #region Defense against attack critical

            // Store defense against attack critical rate
            cfg.Default.DefCritRate = decud_Default_DefCritRate.Value ?? cfg.Default.DefCritRate;

            // Store defense against attack critical damage
            cfg.Default.DefCritDamage = decud_Default_DefCritDamage.Value ?? cfg.Default.DefCritDamage;

            // Store defense against attack special critical rate
            cfg.Default.DefSpCritRate = decud_Default_DefSpCritRate.Value ?? cfg.Default.DefSpCritRate;

            // Store defense against attack special critical damage
            cfg.Default.DefSpCritDamage = decud_Default_DefSpCritDamage.Value ?? cfg.Default.DefSpCritDamage;

            #endregion

            #region Defense against Skill critical

            // Store defense against attack critical rate
            cfg.Default.DefSkillCritRate = decud_Default_DefSkillCritRate.Value ?? cfg.Default.DefSkillCritRate;

            // Store defense against attack critical damage
            cfg.Default.DefSkillCritDamage = decud_Default_DefSkillCritDamage.Value ?? cfg.Default.DefSkillCritDamage;

            // Store defense against attack special critical rate
            cfg.Default.DefSkillSpCritRate = decud_Default_DefSkillSpCritRate.Value ?? cfg.Default.DefSkillSpCritRate;

            // Store defense against attack special critical damage
            cfg.Default.DefSkillSpCritDamage = decud_Default_DefSkillSpCritDamage.Value ?? cfg.Default.DefSkillSpCritDamage;

            #endregion

            #region Attack

            // Store attack
            cfg.Default.AtkInitial = intud_Default_AtkInitial.Value ?? cfg.Default.AtkInitial;
            cfg.Default.AtkFinal = intud_Default_AtkFinal.Value ?? cfg.Default.AtkFinal;

            // Store hit rate
            cfg.Default.HitInitial = decud_Default_HitInitial.Value ?? cfg.Default.HitInitial;
            cfg.Default.HitFinal = decud_Default_HitFinal.Value ?? cfg.Default.HitFinal;

            // Store unarmed attack animation
            cfg.Default.AnimCaster = combo_Default_AnimCaster.SelectedIndex;
            cfg.Default.AnimTarget = combo_Default_AnimTarget.SelectedIndex;

            // Store parameter force
            cfg.Default.StrForce = decud_Default_StrForce.Value ?? cfg.Default.StrForce;
            cfg.Default.DexForce = decud_Default_DexForce.Value ?? cfg.Default.DexForce;
            cfg.Default.AgiForce = decud_Default_AgiForce.Value ?? cfg.Default.AgiForce;
            cfg.Default.IntForce = decud_Default_IntForce.Value ?? cfg.Default.IntForce;

            // Store defense force
            cfg.Default.PDefForce = decud_Default_PDefForce.Value ?? cfg.Default.PDefForce;
            cfg.Default.MDefForce = decud_Default_MDefForce.Value ?? cfg.Default.MDefForce;

            #endregion

            #region Critical

            // Store critical rate
            cfg.Default.CritRate = decud_Default_CritRate.Value ?? cfg.Default.CritRate;

            // Store critical damage
            cfg.Default.CritDamage = decud_Default_CritDamage.Value ?? cfg.Default.CritDamage;

            // Store critical guard reduction
            cfg.Default.CritDefGuard = decud_Default_CritDefGuard.Value ?? cfg.Default.CritDefGuard;

            // Store critical evasion reduction
            cfg.Default.CritDefEva = decud_Default_CritDefEva.Value ?? cfg.Default.CritDefEva;

            #endregion

            #region Special Critical

            // Store special critical rate
            cfg.Default.SpCritRate = decud_Default_SpCritRate.Value ?? cfg.Default.SpCritRate;

            // Store special critical damage
            cfg.Default.SpCritDamage = decud_Default_SpCritDamage.Value ?? cfg.Default.SpCritDamage;

            // Store critical guard reduction
            cfg.Default.SpCritDefGuard = decud_Default_SpCritDefGuard.Value ?? cfg.Default.SpCritDefGuard;

            // Store critical evasion reduction
            cfg.Default.SpCritDefEva = decud_Default_SpCritDefEva.Value ?? cfg.Default.SpCritDefEva;

            #endregion

            #region Defense

            // Store physical defense
            cfg.Default.PDefInitial = intud_Default_PDefInitial.Value ?? cfg.Default.PDefInitial;
            cfg.Default.PDefFinal = intud_Default_PDefFinal.Value ?? cfg.Default.PDefFinal;

            // Store magical defense
            cfg.Default.MDefInitial = intud_Default_MDefInitial.Value ?? cfg.Default.MDefInitial;
            cfg.Default.MDefFinal = intud_Default_MDefFinal.Value ?? cfg.Default.MDefFinal;

            #endregion
        }

        #endregion

        #region Actor

        #region Get Data

        private DataPackActor getActorData(int index, string checkBox, DataPackActor oldActorData)
        {
            DataPackActor actor = new DataPackActor();

            // Set the updating flag
            updating = true;

            // Set the id of the data
            actor.ID = index;

            // Set the name of the data
            actor.Name = txt_Actor_Name.Text;

            #region In family

            actor.ActorFamily.Clear();
            if (actorInFamily.Count > 0)
            {
                foreach (Actor actors in actorInFamily)
                {
                    actor.ActorFamily.Add(new Actor(actors.ID, actors.Name));
                }
            }

            cfg.ActorAvailable.Clear();
            if (actorAvailable.Count > 0)
            {
                foreach (Actor actors in actorAvailable)
                {
                    cfg.ActorAvailable.Add(new Actor(actors.ID, actors.Name));
                }
            }

            #endregion

            #region Equipment

            // Clear the equip type and list_Actor
            actor.EquipType.Clear();
            actor.EquipList.Clear();

            // Check if the custom equip is enable
            if (check_Actor_CustomEquip.IsChecked == true)
            {
                // Store the value of the custom equip checkbox
                actor.CustomEquip = true;

                // Store the equip type list
                if (actorEquipType.Count > 0)
                {
                    foreach (EquipType equip in actorEquipType)
                    {
                        actor.EquipType.Add(new EquipType(equip.ID, equip.Name));
                    }
                }

                // Store the equip id list
                if (actorEquipList.Count > 0)
                {
                    foreach (EquipType equip in actorEquipList)
                    {
                        actor.EquipList.Add(new EquipType(equip.ID, equip.Name));
                    }
                }
            }

            // Store dual hold
            if (check_Actor_DualHold.IsChecked == true)
            {
                actor.DualHold = true;

                // Store dual hold name
                if (check_Actor_DualHoldName.IsChecked == true)
                {
                    actor.CustomDualHoldName = true;
                    if (checkBox != "check_Actor_DualHoldName")
                    {
                        actor.DualHoldNameWeapon = txt_Actor_DualHoldNameWeapon.Text;
                        actor.DualHoldNameShield = txt_Actor_DualHoldNameShield.Text;
                    }
                    else
                    {
                        actor.DualHoldNameWeapon = cfg.Default.DualHoldNameWeapon;
                        actor.DualHoldNameShield = cfg.Default.DualHoldNameShield;
                    }
                }

                // Store dual hold multiplier
                if (check_Actor_DualHoldMul.IsChecked == true)
                {
                    actor.CustomDualHoldMul = true;
                    if (checkBox != "check_Actor_DualHoldMul")
                    {
                        actor.DualHoldMulWeapon = decud_Actor_DualHoldMulWeapon.Value ?? oldActorData.DualHoldMulWeapon;
                        actor.DualHoldMulShield = decud_Actor_DualHoldMulShield.Value ?? oldActorData.DualHoldMulShield;
                    }
                    else
                    {
                        actor.DualHoldMulWeapon = cfg.Default.DualHoldMulWeapon;
                        actor.DualHoldMulShield = cfg.Default.DualHoldMulShield;
                    }
                }

                // Store the shield bypass
                if (check_Actor_ShieldBypass.IsChecked == true)
                {
                    actor.ShieldBypass = true;
                    if (checkBox != "check_Actor_ShieldBypass")
                    {
                        actor.ShieldBypassMul = decud_Actor_ShieldBypass.Value ?? oldActorData.ShieldBypassMul;
                    }
                    else
                    {
                        actor.ShieldBypassMul = cfg.Default.ShieldBypassMul;
                    }
                }
            }

            // Store the weapon bypass
            if (check_Actor_WeaponBypass.IsChecked == true)
            {
                actor.WeaponBypass = true;
                if (checkBox != "check_Actor_WeaponBypass")
                {
                    actor.WeaponBypassMul = decud_Actor_WeaponBypass.Value ?? oldActorData.WeaponBypassMul;
                }
                else
                {
                    actor.WeaponBypassMul = cfg.Default.WeaponBypassMul;
                }
            }

            // Store the reduce hand
            if (check_Actor_ReduceHand.IsChecked == true)
            {
                actor.CustomReduceHand = true;
                if (checkBox != "check_Actor_ReduceHand")
                {
                    actor.ReduceHand = decud_Actor_ReduceHand.Value ?? oldActorData.ReduceHand;
                    actor.ReduceHandMul = decud_Actor_ReduceHandMul.Value ?? oldActorData.ReduceHandMul;
                }
                else
                {
                    actor.ReduceHand = cfg.Default.ReduceHand;
                    actor.ReduceHandMul = cfg.Default.ReduceHandMul;
                }
            }

            #endregion

            #region Parameter

            // Store maximum HP
            if (check_Actor_MaxHP.IsChecked == true)
            {
                actor.CustomMaxHP = true;
                if (checkBox != "check_Actor_MaxHP")
                {
                    actor.MaxHPInitial = intud_Actor_MaxHPInitial.Value ?? oldActorData.MaxHPInitial;
                    actor.MaxHPFinal = intud_Actor_MaxHPFinal.Value ?? oldActorData.MaxHPFinal;
                }
                else
                {
                    actor.MaxHPInitial = cfg.Default.MaxHPInitial;
                    actor.MaxHPFinal = cfg.Default.MaxHPFinal;
                }
            }

            // Store maximum SP
            if (check_Actor_MaxSP.IsChecked == true)
            {
                actor.CustomMaxSP = true;
                if (checkBox != "check_Actor_MaxSP")
                {
                    actor.MaxSPInitial = intud_Actor_MaxSPInitial.Value ?? oldActorData.MaxSPInitial;
                    actor.MaxSPFinal = intud_Actor_MaxSPFinal.Value ?? oldActorData.MaxSPFinal;
                }
                else
                {
                    actor.MaxSPInitial = cfg.Default.MaxSPInitial;
                    actor.MaxSPFinal = cfg.Default.MaxSPFinal;
                }
            }

            // Store strengh
            if (check_Actor_Str.IsChecked == true)
            {
                actor.CustomStr = true;
                if (checkBox != "check_Actor_Str")
                {
                    actor.StrInitial = intud_Actor_StrInitial.Value ?? oldActorData.StrInitial;
                    actor.StrFinal = intud_Actor_StrFinal.Value ?? oldActorData.StrFinal;
                }
                else
                {
                    actor.StrInitial = cfg.Default.StrInitial;
                    actor.StrFinal = cfg.Default.StrFinal;
                }
            }

            // Store dexterity
            if (check_Actor_Dex.IsChecked == true)
            {
                actor.CustomDex = true;
                if (checkBox != "check_Actor_Dex")
                {
                    actor.DexInitial = intud_Actor_DexInitial.Value ?? oldActorData.DexInitial;
                    actor.DexFinal = intud_Actor_DexFinal.Value ?? oldActorData.DexFinal;
                }
                else
                {
                    actor.DexInitial = cfg.Default.DexInitial;
                    actor.DexFinal = cfg.Default.DexFinal;
                }
            }

            // Store agility
            if (check_Actor_Agi.IsChecked == true)
            {
                actor.CustomAgi = true;
                if (checkBox != "check_Actor_Agi")
                {
                    actor.AgiInitial = intud_Actor_AgiInitial.Value ?? oldActorData.AgiInitial;
                    actor.AgiFinal = intud_Actor_AgiFinal.Value ?? oldActorData.AgiFinal;
                }
                else
                {
                    actor.AgiInitial = cfg.Default.AgiInitial;
                    actor.AgiFinal = cfg.Default.AgiFinal;
                }
            }

            // Store intelligence
            if (check_Actor_Int.IsChecked == true)
            {
                actor.CustomInt = true;
                if (checkBox != "check_Actor_Int")
                {
                    actor.IntInitial = intud_Actor_IntInitial.Value ?? oldActorData.IntInitial;
                    actor.IntFinal = intud_Actor_IntFinal.Value ?? oldActorData.IntFinal;
                }
                else
                {
                    actor.IntInitial = cfg.Default.IntInitial;
                    actor.IntFinal = cfg.Default.IntFinal;
                }
            }

            #endregion

            #region Parameter Rate

            // Store strengh rate
            if (check_Actor_StrRate.IsChecked == true)
            {
                actor.CustomStrRate = true;
                if (checkBox != "check_Actor_StrRate")
                {
                    actor.StrRate = decud_Actor_StrRate.Value ?? oldActorData.StrRate;
                }
                else
                {
                    actor.StrRate = cfg.Default.StrRate;
                }
            }

            // Store dexterity rate
            if (check_Actor_DexRate.IsChecked == true)
            {
                actor.CustomDexRate = true;
                if (checkBox != "check_Actor_DexRate")
                {
                    actor.DexRate = decud_Actor_DexRate.Value ?? oldActorData.DexRate;
                }
                else
                {
                    actor.DexRate = cfg.Default.DexRate;
                }
            }

            // Store agility rate
            if (check_Actor_AgiRate.IsChecked == true)
            {
                actor.CustomAgiRate = true;
                if (checkBox != "check_Actor_AgiRate")
                {
                    actor.AgiRate = decud_Actor_AgiRate.Value ?? oldActorData.AgiRate;
                }
                else
                {
                    actor.AgiRate = cfg.Default.AgiRate;
                }
            }

            // Store intelligence rate
            if (check_Actor_IntRate.IsChecked == true)
            {
                actor.CustomIntRate = true;
                if (checkBox != "check_Actor_IntRate")
                {
                    actor.IntRate = decud_Actor_IntRate.Value ?? oldActorData.IntRate;
                }
                else
                {
                    actor.IntRate = cfg.Default.IntRate;
                }
            }

            // Store physical defense rate
            if (check_Actor_PDefRate.IsChecked == true)
            {
                actor.CustomPDefRate = true;
                if (checkBox != "check_Actor_PDefRate")
                {
                    actor.PDefRate = decud_Actor_PDefRate.Value ?? oldActorData.PDefRate;
                }
                else
                {
                    actor.PDefRate = cfg.Default.PDefRate;
                }
            }

            // Store magical defense rate
            if (check_Actor_MDefRate.IsChecked == true)
            {
                actor.CustomMDefRate = true;
                if (checkBox != "check_Actor_MDefRate")
                {
                    actor.MDefRate = decud_Actor_MDefRate.Value ?? oldActorData.MDefRate;
                }
                else
                {
                    actor.MDefRate = cfg.Default.MDefRate;
                }
            }

            // Store guard rate
            if (check_Actor_GuardRate.IsChecked == true)
            {
                actor.CustomGuardRate = true;
                if (checkBox != "check_Actor_GuardRate")
                {
                    actor.GuardRate = decud_Actor_GuardRate.Value ?? oldActorData.GuardRate;
                }
                else
                {
                    actor.GuardRate = cfg.Default.GuardRate;
                }
            }

            // Store evasion rate
            if (check_Actor_EvaRate.IsChecked == true)
            {
                actor.CustomEvaRate = true;
                if (checkBox != "check_Actor_EvaRate")
                {
                    actor.EvaRate = decud_Actor_EvaRate.Value ?? oldActorData.EvaRate;
                }
                else
                {
                    actor.EvaRate = cfg.Default.EvaRate;
                }
            }

            #endregion

            #region Defense against attack critical

            // Store defense against attack critical rate
            if (check_Actor_DefCritRate.IsChecked == true)
            {
                actor.CustomDefCritRate = true;
                if (checkBox != "check_Actor_DefCritRate")
                {
                    actor.DefCritRate = decud_Actor_DefCritRate.Value ?? oldActorData.DefCritRate;
                }
                else
                {
                    actor.DefCritRate = cfg.Default.DefCritRate;
                }
            }

            // Store defense against attack critical damage
            if (check_Actor_DefCritDamage.IsChecked == true)
            {
                actor.CustomDefCritDamage = true;
                if (checkBox != "check_Actor_DefCritDamage")
                {
                    actor.DefCritDamage = decud_Actor_DefCritDamage.Value ?? oldActorData.DefCritDamage;
                }
                else
                {
                    actor.DefCritDamage = cfg.Default.DefCritDamage;
                }
            }

            // Store defense against attack special critical rate
            if (check_Actor_DefSpCritRate.IsChecked == true)
            {
                actor.CustomDefSpCritRate = true;
                if (checkBox != "check_Actor_DefSpCritRate")
                {
                    actor.DefSpCritRate = decud_Actor_DefSpCritRate.Value ?? oldActorData.DefSpCritRate;
                }
                else
                {
                    actor.DefSpCritRate = cfg.Default.DefSpCritRate;
                }
            }

            // Store defense against attack special critical damage
            if (check_Actor_DefSpCritDamage.IsChecked == true)
            {
                actor.CustomDefSpCritDamage = true;
                if (checkBox != "check_Actor_DefSpCritDamage")
                {
                    actor.DefSpCritDamage = decud_Actor_DefSpCritDamage.Value ?? oldActorData.DefSpCritDamage;
                }
                else
                {
                    actor.DefSpCritDamage = cfg.Default.DefSpCritDamage;
                }
            }

            #endregion

            #region Defense against Skill critical

            // Store defense against attack critical rate
            if (check_Actor_DefSkillCritRate.IsChecked == true)
            {
                actor.CustomDefSkillCritRate = true;
                if (checkBox != "check_Actor_DefSkillCritRate")
                {
                    actor.DefSkillCritRate = decud_Actor_DefSkillCritRate.Value ?? oldActorData.DefSkillCritRate;
                }
                else
                {
                    actor.DefSkillCritRate = cfg.Default.DefSkillCritRate;
                }
            }

            // Store defense against attack critical damage
            if (check_Actor_DefSkillCritDamage.IsChecked == true)
            {
                actor.CustomDefSkillCritDamage = true;
                if (checkBox != "check_Actor_DefSkillCritDamage")
                {
                    actor.DefSkillCritDamage = decud_Actor_DefSkillCritDamage.Value ?? oldActorData.DefSkillCritDamage;
                }
                else
                {
                    actor.DefSkillCritDamage = cfg.Default.DefSkillCritDamage;
                }
            }

            // Store defense against attack special critical rate
            if (check_Actor_DefSkillSpCritRate.IsChecked == true)
            {
                actor.CustomDefSkillSpCritRate = true;
                if (checkBox != "check_Actor_DefSkillSpCritRate")
                {
                    actor.DefSkillSpCritRate = decud_Actor_DefSkillSpCritRate.Value ?? oldActorData.DefSkillSpCritRate;
                }
                else
                {
                    actor.DefSkillSpCritRate = cfg.Default.DefSkillSpCritRate;
                }
            }

            // Store defense against attack special critical damage
            if (check_Actor_DefSkillSpCritDamage.IsChecked == true)
            {
                actor.CustomDefSkillSpCritDamage = true;
                if (checkBox != "check_Actor_DefSkillSpCritDamage")
                {
                    actor.DefSkillSpCritDamage = decud_Actor_DefSkillSpCritDamage.Value ?? oldActorData.DefSkillSpCritDamage;
                }
                else
                {
                    actor.DefSkillSpCritDamage = cfg.Default.DefSkillSpCritDamage;
                }
            }

            #endregion

            #region Unarmed Attack

            // Store unarmed attack
            if (check_Actor_Atk.IsChecked == true)
            {
                actor.CustomAtk = true;
                if (checkBox != "check_Actor_Atk")
                {
                    actor.AtkInitial = intud_Actor_AtkInitial.Value ?? oldActorData.AtkInitial;
                    actor.AtkFinal = intud_Actor_AtkFinal.Value ?? oldActorData.AtkFinal;
                }
                else
                {
                    actor.AtkInitial = cfg.Default.AtkInitial;
                    actor.AtkFinal = cfg.Default.AtkFinal;
                }
            }

            // Store unarmed hit rate
            if (check_Actor_Hit.IsChecked == true)
            {
                actor.CustomHit = true;
                if (checkBox != "check_Actor_Hit")
                {
                    actor.HitInitial = decud_Actor_HitInitial.Value ?? oldActorData.HitInitial;
                    actor.HitFinal = decud_Actor_HitFinal.Value ?? oldActorData.HitFinal;
                }
                else
                {
                    actor.HitInitial = cfg.Default.HitInitial;
                    actor.HitFinal = cfg.Default.HitFinal;
                }
            }

            // Store unarmed attack animation
            if (check_Actor_Anim.IsChecked == true)
            {
                actor.CustomAnim = true;
                if (checkBox != "check_Actor_Anim")
                {
                    actor.AnimCaster = combo_Actor_AnimCaster.SelectedIndex;
                    actor.AnimTarget = combo_Actor_AnimTarget.SelectedIndex;
                }
                else
                {
                    actor.AnimCaster = cfg.Default.AnimCaster;
                    actor.AnimTarget = cfg.Default.AnimTarget;
                }
            }

            // Store unarmed parameter attack force
            if (check_Actor_ParamForce.IsChecked == true)
            {
                actor.CustomParamForce = true;
                if (checkBox != "check_Actor_ParamForce")
                {
                    actor.StrForce = decud_Actor_StrForce.Value ?? oldActorData.StrForce;
                    actor.DexForce = decud_Actor_DexForce.Value ?? oldActorData.DexForce;
                    actor.AgiForce = decud_Actor_AgiForce.Value ?? oldActorData.AgiForce;
                    actor.IntForce = decud_Actor_IntForce.Value ?? oldActorData.IntForce;
                }
                else
                {
                    actor.StrForce = cfg.Default.StrForce;
                    actor.DexForce = cfg.Default.DexForce;
                    actor.AgiForce = cfg.Default.AgiForce;
                    actor.IntForce = cfg.Default.IntForce;
                }
            }

            // Store unarmed defense attack force
            if (check_Actor_DefenseForce.IsChecked == true)
            {
                actor.CustomDefenseForce = true;
                if (checkBox != "check_Actor_DefenseForce")
                {
                    actor.PDefForce = decud_Actor_PDefForce.Value ?? oldActorData.PDefForce;
                    actor.MDefForce = decud_Actor_MDefForce.Value ?? oldActorData.MDefForce;
                }
                else
                {
                    actor.PDefForce = cfg.Default.PDefForce;
                    actor.MDefForce = cfg.Default.MDefForce;
                }
            }

            #endregion

            #region Unarmed Critical

            // Store unarmed critical rate
            if (check_Actor_CritRate.IsChecked == true)
            {
                actor.CustomCritRate = true;
                if (checkBox != "check_Actor_CritRate")
                {
                    actor.CritRate = decud_Actor_CritRate.Value ?? oldActorData.CritRate;
                }
                else
                {
                    actor.CritRate = cfg.Default.CritRate;
                }
            }

            // Store unarmed critical damage
            if (check_Actor_CritDamage.IsChecked == true)
            {
                actor.CustomCritDamage = true;
                if (checkBox != "check_Actor_CritDamage")
                {
                    actor.CritDamage = decud_Actor_CritDamage.Value ?? oldActorData.CritDamage;
                }
                else
                {
                    actor.CritDamage = cfg.Default.CritDamage;
                }
            }

            // Store unarmed critical guard reduction
            if (check_Actor_CritDefGuard.IsChecked == true)
            {
                actor.CustomCritDefGuard = true;
                if (checkBox != "check_Actor_CritDefGuard")
                {
                    actor.CritDefGuard = decud_Actor_CritDefGuard.Value ?? oldActorData.CritDefGuard;
                }
                else
                {
                    actor.CritDefGuard = cfg.Default.CritDefGuard;
                }
            }

            // Store unarmed critical evasion reduction
            if (check_Actor_CritDefEva.IsChecked == true)
            {
                actor.CustomCritDefEva = true;
                if (checkBox != "check_Actor_CritDefEva")
                {
                    actor.CritDefEva = decud_Actor_CritDefEva.Value ?? oldActorData.CritDefEva;
                }
                else
                {
                    actor.CritDefEva = cfg.Default.CritDefEva;
                }
            }

            #endregion

            #region Unarmed Special Critical

            // Store unarmed special critical rate
            if (check_Actor_SpCritRate.IsChecked == true)
            {
                actor.CustomSpCritRate = true;
                if (checkBox != "check_Actor_SpCritRate")
                {
                    actor.SpCritRate = decud_Actor_SpCritRate.Value ?? oldActorData.SpCritRate;
                }
                else
                {
                    actor.SpCritRate = cfg.Default.SpCritRate;
                }
            }

            // Store unarmed special critical damage
            if (check_Actor_SpCritDamage.IsChecked == true)
            {
                actor.CustomSpCritDamage = true;
                if (checkBox != "check_Actor_SpCritDamage")
                {
                    actor.SpCritDamage = decud_Actor_SpCritDamage.Value ?? oldActorData.SpCritDamage;
                }
                else
                {
                    actor.SpCritDamage = cfg.Default.SpCritDamage;
                }
            }

            // Store unarmed critical guard reduction
            if (check_Actor_SpCritDefGuard.IsChecked == true)
            {
                actor.CustomSpCritDefGuard = true;
                if (checkBox != "check_Actor_SpCritDefGuard")
                {
                    actor.SpCritDefGuard = decud_Actor_SpCritDefGuard.Value ?? oldActorData.SpCritDefGuard;
                }
                else
                {
                    actor.SpCritDefGuard = cfg.Default.SpCritDefGuard;
                }
            }

            // Store unarmed critical evasion reduction
            if (check_Actor_SpCritDefEva.IsChecked == true)
            {
                actor.CustomSpCritDefEva = true;
                if (checkBox != "check_Actor_SpCritDefEva")
                {
                    actor.SpCritDefEva = decud_Actor_SpCritDefEva.Value ?? oldActorData.SpCritDefEva;
                }
                else
                {
                    actor.SpCritDefEva = cfg.Default.SpCritDefEva;
                }
            }

            #endregion

            #region Unarmoured Defense

            // Store unarmoured physical defense
            if (check_Actor_PDef.IsChecked == true)
            {
                actor.CustomPDef = true;
                if (checkBox != "check_Actor_PDef")
                {
                    actor.PDefInitial = intud_Actor_PDefInitial.Value ?? oldActorData.PDefInitial;
                    actor.PDefFinal = intud_Actor_PDefFinal.Value ?? oldActorData.PDefFinal;
                }
                else
                {
                    actor.PDefInitial = cfg.Default.PDefInitial;
                    actor.PDefFinal = cfg.Default.PDefFinal;
                }
            }

            // Store unarmoured magical defense
            if (check_Actor_MDef.IsChecked == true)
            {
                actor.CustomMDef = true;
                if (checkBox != "check_Actor_MDef")
                {
                    actor.MDefInitial = intud_Actor_MDefInitial.Value ?? oldActorData.MDefInitial;
                    actor.MDefFinal = intud_Actor_MDefFinal.Value ?? oldActorData.MDefFinal;
                }
                else
                {
                    actor.MDefInitial = cfg.Default.MDefInitial;
                    actor.MDefFinal = cfg.Default.MDefFinal;
                }
            }

            #endregion

            // Remove the updating flag
            updating = false;

            return actor;
        }

        #endregion

        #region Store Family

        private void storeActorFamily(int index, string checkBox)
        {
            if (tree_Actor_IsFamily() && index > 0 && index <= cfg.ActorFamily.Count && cfg.ActorFamily.Count > 0)
            {
                cfg.ActorFamily[index - 1].CloneFrom(getActorData(index, checkBox, cfg.ActorFamily[index - 1]));
            }
        }

        private void storeActorFamily(string checkBox)
        {
            storeActorFamily(tree_Actor_ID(), checkBox);
        }

        private void storeActorFamily(int index)
        {
            storeActorFamily(index, "");
        }

        private void storeActorFamily()
        {
            storeActorFamily(tree_Actor_ID(), "");
        }

        #endregion

        #region Store Individual

        private void storeActorID(int index, string checkBox)
        {
            if (tree_Actor_IsIndividual() && index > 0 && index <= cfg.ActorID.Count && cfg.ActorID.Count > 0)
            {
                cfg.ActorID[index - 1].CloneFrom(getActorData(index, checkBox, cfg.ActorID[index - 1]));
            }
        }

        private void storeActorID(string checkBox)
        {
            storeActorID(tree_Actor_ID(), checkBox);
        }

        private void storeActorID(int index)
        {
            storeActorID(index, "");
        }

        private void storeActorID()
        {
            storeActorID(tree_Actor_ID(), "");
        }

        #endregion

        #region Store Default

        private void storeActorDefault(string checkBox)
        {
            cfg.ActorDefault.CloneFrom(getActorData(0, checkBox, cfg.ActorDefault));
        }

        private void storeActorDefault()
        {
            storeActorDefault("");
        }

        #endregion

        #endregion

        #region Class

        #region Get Data

        private DataPackClass getClassData(int index, string checkBox, DataPackClass oldClassData)
        {
            DataPackClass classes = new DataPackClass();

            // Set the updating flag
            updating = true;

            // Set the id of the data
            classes.ID = index;

            // Set the name of the data
            classes.Name = txt_Class_Name.Text;

            #region In family

            classes.ClassFamily.Clear();
            if (classInFamily.Count > 0)
            {
                foreach (Class classF in classInFamily)
                {
                    classes.ClassFamily.Add(new Class(classF.ID, classF.Name));
                }
            }

            cfg.ClassAvailable.Clear();
            if (classAvailable.Count > 0)
            {
                foreach (Class classF in classAvailable)
                {
                    cfg.ClassAvailable.Add(new Class(classF.ID, classF.Name));
                }
            }

            #endregion

            #region Equipment

            // Clear the equip type and list_Class
            classes.EquipType.Clear();
            classes.EquipList.Clear();

            // Check if the custom equip is enable
            if (check_Class_CustomEquip.IsChecked == true)
            {
                // Store the value of the custom equip checkbox
                classes.CustomEquip = true;

                // Store the equip type list
                if (classEquipType.Count > 0)
                {
                    foreach (EquipType equip in classEquipType)
                    {
                        classes.EquipType.Add(new EquipType(equip.ID, equip.Name));
                    }
                }

                // Store the equip id list
                if (classEquipList.Count > 0)
                {
                    foreach (EquipType equip in classEquipList)
                    {
                        classes.EquipList.Add(new EquipType(equip.ID, equip.Name));
                    }
                }
            }

            // Store dual hold
            if (check_Class_DualHold.IsChecked == true)
            {
                classes.DualHold = true;

                // Store dual hold name
                if (check_Class_DualHoldName.IsChecked == true)
                {
                    classes.CustomDualHoldName = true;
                    if (checkBox != "check_Class_DualHoldName")
                    {
                        classes.DualHoldNameWeapon = txt_Class_DualHoldNameWeapon.Text;
                        classes.DualHoldNameShield = txt_Class_DualHoldNameShield.Text;
                    }
                    else
                    {
                        classes.DualHoldNameWeapon = cfg.Default.DualHoldNameWeapon;
                        classes.DualHoldNameShield = cfg.Default.DualHoldNameShield;
                    }
                }

                // Store dual hold multiplier
                if (check_Class_DualHoldMul.IsChecked == true)
                {
                    classes.CustomDualHoldMul = true;
                    if (checkBox != "check_Class_DualHoldMul")
                    {
                        classes.DualHoldMulWeapon = decud_Class_DualHoldMulWeapon.Value ?? oldClassData.DualHoldMulWeapon;
                        classes.DualHoldMulShield = decud_Class_DualHoldMulShield.Value ?? oldClassData.DualHoldMulShield;
                    }
                    else
                    {
                        classes.DualHoldMulWeapon = cfg.Default.DualHoldMulWeapon;
                        classes.DualHoldMulShield = cfg.Default.DualHoldMulShield;
                    }
                }

                // Store the shield bypass
                if (check_Class_ShieldBypass.IsChecked == true)
                {
                    classes.ShieldBypass = true;
                    if (checkBox != "check_Class_ShieldBypass")
                    {
                        classes.ShieldBypassMul = decud_Class_ShieldBypass.Value ?? oldClassData.ShieldBypassMul;
                    }
                    else
                    {
                        classes.ShieldBypassMul = cfg.Default.ShieldBypassMul;
                    }
                }
            }

            // Store the weapon bypass
            if (check_Class_WeaponBypass.IsChecked == true)
            {
                classes.WeaponBypass = true;
                if (checkBox != "check_Class_WeaponBypass")
                {
                    classes.WeaponBypassMul = decud_Class_WeaponBypass.Value ?? oldClassData.WeaponBypassMul;
                }
                else
                {
                    classes.WeaponBypassMul = cfg.Default.WeaponBypassMul;
                }
            }

            // Store the reduce hand
            if (check_Class_ReduceHand.IsChecked == true)
            {
                classes.CustomReduceHand = true;
                if (checkBox != "check_Class_ReduceHand")
                {
                    classes.ReduceHand = decud_Class_ReduceHand.Value ?? oldClassData.ReduceHand;
                    classes.ReduceHandMul = decud_Class_ReduceHandMul.Value ?? oldClassData.ReduceHandMul;
                }
                else
                {
                    classes.ReduceHand = cfg.Default.ReduceHand;
                    classes.ReduceHandMul = cfg.Default.ReduceHandMul;
                }
            }

            #endregion

            #region Parameter

            // Store maximum HP
            if (check_Class_MaxHP.IsChecked == true)
            {
                classes.CustomMaxHP = true;
                if (checkBox != "check_Class_MaxHP")
                {
                    classes.MaxHPMul = decud_Class_MaxHPMul.Value ?? oldClassData.MaxHPMul;
                    classes.MaxHPAdd = intud_Class_MaxHPAdd.Value ?? oldClassData.MaxHPAdd;
                }
            }

            // Store maximum SP
            if (check_Class_MaxSP.IsChecked == true)
            {
                classes.CustomMaxSP = true;
                if (checkBox != "check_Class_MaxSP")
                {
                    classes.MaxSPMul = decud_Class_MaxSPMul.Value ?? oldClassData.MaxSPMul;
                    classes.MaxSPAdd = intud_Class_MaxSPAdd.Value ?? oldClassData.MaxSPAdd;
                }
            }

            // Store strengh
            if (check_Class_Str.IsChecked == true)
            {
                classes.CustomStr = true;
                if (checkBox != "check_Class_Str")
                {
                    classes.StrMul = decud_Class_StrMul.Value ?? oldClassData.StrMul;
                    classes.StrAdd = intud_Class_StrAdd.Value ?? oldClassData.StrAdd;
                }
            }

            // Store dexterity
            if (check_Class_Dex.IsChecked == true)
            {
                classes.CustomDex = true;
                if (checkBox != "check_Class_Dex")
                {
                    classes.DexMul = decud_Class_DexMul.Value ?? oldClassData.DexMul;
                    classes.DexAdd = intud_Class_DexAdd.Value ?? oldClassData.DexAdd;
                }
            }

            // Store agility
            if (check_Class_Agi.IsChecked == true)
            {
                classes.CustomAgi = true;
                if (checkBox != "check_Class_Agi")
                {
                    classes.AgiMul = decud_Class_AgiMul.Value ?? oldClassData.AgiMul;
                    classes.AgiAdd = intud_Class_AgiAdd.Value ?? oldClassData.AgiAdd;
                }
            }

            // Store intelligence
            if (check_Class_Int.IsChecked == true)
            {
                classes.CustomInt = true;
                if (checkBox != "check_Class_Int")
                {
                    classes.IntMul = decud_Class_IntMul.Value ?? oldClassData.IntMul;
                    classes.IntAdd = intud_Class_IntAdd.Value ?? oldClassData.IntAdd;
                }
            }

            #endregion

            #region Parameter Rate

            // Store strengh rate
            if (check_Class_StrRate.IsChecked == true)
            {
                classes.CustomStrRate = true;
                if (checkBox != "check_Class_StrRate")
                {
                    classes.StrRateMul = decud_Class_StrRateMul.Value ?? oldClassData.StrRateMul;
                    classes.StrRateAdd = intud_Class_StrRateAdd.Value ?? oldClassData.StrRateAdd;
                }
            }

            // Store dexterity rate
            if (check_Class_DexRate.IsChecked == true)
            {
                classes.CustomDexRate = true;
                if (checkBox != "check_Class_DexRate")
                {
                    classes.DexRateMul = decud_Class_DexRateMul.Value ?? oldClassData.DexRateMul;
                    classes.DexRateAdd = intud_Class_DexRateAdd.Value ?? oldClassData.DexRateAdd;
                }
            }

            // Store agility rate
            if (check_Class_AgiRate.IsChecked == true)
            {
                classes.CustomAgiRate = true;
                if (checkBox != "check_Class_AgiRate")
                {
                    classes.AgiRateMul = decud_Class_AgiRateMul.Value ?? oldClassData.AgiRateMul;
                    classes.AgiRateAdd = intud_Class_AgiRateAdd.Value ?? oldClassData.AgiRateAdd;
                }
            }

            // Store intelligence rate
            if (check_Class_IntRate.IsChecked == true)
            {
                classes.CustomIntRate = true;
                if (checkBox != "check_Class_IntRate")
                {
                    classes.IntRateMul = decud_Class_IntRateMul.Value ?? oldClassData.IntRateMul;
                    classes.IntRateAdd = intud_Class_IntRateAdd.Value ?? oldClassData.IntRateAdd;
                }
            }

            // Store physical defense rate
            if (check_Class_PDefRate.IsChecked == true)
            {
                classes.CustomPDefRate = true;
                if (checkBox != "check_Class_PDefRate")
                {
                    classes.PDefRateMul = decud_Class_PDefRateMul.Value ?? oldClassData.PDefRateMul;
                    classes.PDefRateAdd = intud_Class_PDefRateAdd.Value ?? oldClassData.PDefRateAdd;
                }
            }

            // Store magical defense rate
            if (check_Class_MDefRate.IsChecked == true)
            {
                classes.CustomMDefRate = true;
                if (checkBox != "check_Class_MDefRate")
                {
                    classes.MDefRateMul = decud_Class_MDefRateMul.Value ?? oldClassData.MDefRateMul;
                    classes.MDefRateAdd = intud_Class_MDefRateAdd.Value ?? oldClassData.MDefRateAdd;
                }
            }

            // Store guard rate
            if (check_Class_GuardRate.IsChecked == true)
            {
                classes.CustomGuardRate = true;
                if (checkBox != "check_Class_GuardRate")
                {
                    classes.GuardRateMul = decud_Class_GuardRateMul.Value ?? oldClassData.GuardRateMul;
                    classes.GuardRateAdd = intud_Class_GuardRateAdd.Value ?? oldClassData.GuardRateAdd;
                }
            }

            // Store evasion rate
            if (check_Class_EvaRate.IsChecked == true)
            {
                classes.CustomEvaRate = true;
                if (checkBox != "check_Class_EvaRate")
                {
                    classes.EvaRateMul = decud_Class_EvaRateMul.Value ?? oldClassData.EvaRateMul;
                    classes.EvaRateAdd = intud_Class_EvaRateAdd.Value ?? oldClassData.EvaRateAdd;
                }
            }

            #endregion

            #region Defense against attack critical

            // Store defense against attack critical rate
            if (check_Class_DefCritRate.IsChecked == true)
            {
                classes.CustomDefCritRate = true;
                if (checkBox != "check_Class_DefCritRate")
                {
                    classes.DefCritRateMul = decud_Class_DefCritRateMul.Value ?? oldClassData.DefCritRateMul;
                    classes.DefCritRateAdd = intud_Class_DefCritRateAdd.Value ?? oldClassData.DefCritRateAdd;
                }
            }

            // Store defense against attack critical damage
            if (check_Class_DefCritDamage.IsChecked == true)
            {
                classes.CustomDefCritDamage = true;
                if (checkBox != "check_Class_DefCritDamage")
                {
                    classes.DefCritDamageMul = decud_Class_DefCritDamageMul.Value ?? oldClassData.DefCritDamageMul;
                    classes.DefCritDamageAdd = intud_Class_DefCritDamageAdd.Value ?? oldClassData.DefCritDamageAdd;
                }
            }

            // Store defense against attack special critical rate
            if (check_Class_DefSpCritRate.IsChecked == true)
            {
                classes.CustomDefSpCritRate = true;
                if (checkBox != "check_Class_DefSpCritRate")
                {
                    classes.DefSpCritRateMul = decud_Class_DefSpCritRateMul.Value ?? oldClassData.DefSpCritRateMul;
                    classes.DefSpCritRateAdd = intud_Class_DefSpCritRateAdd.Value ?? oldClassData.DefSpCritRateAdd;
                }
            }

            // Store defense against attack special critical damage
            if (check_Class_DefSpCritDamage.IsChecked == true)
            {
                classes.CustomDefSpCritDamage = true;
                if (checkBox != "check_Class_DefSpCritDamage")
                {
                    classes.DefSpCritDamageMul = decud_Class_DefSpCritDamageMul.Value ?? oldClassData.DefSpCritDamageMul;
                    classes.DefSpCritDamageAdd = intud_Class_DefSpCritDamageAdd.Value ?? oldClassData.DefSpCritDamageAdd;
                }
            }

            #endregion

            #region Defense against Skill critical

            // Store defense against skill critical rate
            if (check_Class_DefSkillCritRate.IsChecked == true)
            {
                classes.CustomDefSkillCritRate = true;
                if (checkBox != "check_Class_DefSkillCritRate")
                {
                    classes.DefSkillCritRateMul = decud_Class_DefSkillCritRateMul.Value ?? oldClassData.DefSkillCritRateMul;
                    classes.DefSkillCritRateAdd = intud_Class_DefSkillCritRateAdd.Value ?? oldClassData.DefSkillCritRateAdd;
                }
            }

            // Store defense against skill critical damage
            if (check_Class_DefSkillCritDamage.IsChecked == true)
            {
                classes.CustomDefSkillCritDamage = true;
                if (checkBox != "check_Class_DefSkillCritDamage")
                {
                    classes.DefSkillCritDamageMul = decud_Class_DefSkillCritDamageMul.Value ?? oldClassData.DefSkillCritDamageMul;
                    classes.DefSkillCritDamageAdd = intud_Class_DefSkillCritDamageAdd.Value ?? oldClassData.DefSkillCritDamageAdd;
                }
            }

            // Store defense against skill special critical rate
            if (check_Class_DefSkillSpCritRate.IsChecked == true)
            {
                classes.CustomDefSkillSpCritRate = true;
                if (checkBox != "check_Class_DefSkillSpCritRate")
                {
                    classes.DefSkillSpCritRateMul = decud_Class_DefSkillSpCritRateMul.Value ?? oldClassData.DefSkillSpCritRateMul;
                    classes.DefSkillSpCritRateAdd = intud_Class_DefSkillSpCritRateAdd.Value ?? oldClassData.DefSkillSpCritRateAdd;
                }
            }

            // Store defense against skill special critical damage
            if (check_Class_DefSkillSpCritDamage.IsChecked == true)
            {
                classes.CustomDefSkillSpCritDamage = true;
                if (checkBox != "check_Class_DefSkillSpCritDamage")
                {
                    classes.DefSkillSpCritDamageMul = decud_Class_DefSkillSpCritDamageMul.Value ?? oldClassData.DefSkillSpCritDamageMul;
                    classes.DefSkillSpCritDamageAdd = intud_Class_DefSkillSpCritDamageAdd.Value ?? oldClassData.DefSkillSpCritDamageAdd;
                }
            }


            #endregion

            #region Passive attack

            // Store attack
            if (check_Class_PassiveAtk.IsChecked == true)
            {
                classes.CustomPassiveAtk = true;
                if (checkBox != "check_Class_PassiveAtk")
                {
                    classes.AtkMul = decud_Class_AtkMul.Value ?? oldClassData.AtkMul;
                    classes.AtkAdd = intud_Class_AtkAdd.Value ?? oldClassData.AtkAdd;
                }
            }

            // Store hit rate
            if (check_Class_PassiveHit.IsChecked == true)
            {
                classes.CustomPassiveHit = true;
                if (checkBox != "check_Class_PassiveHit")
                {
                    classes.HitMul = decud_Class_HitMul.Value ?? oldClassData.HitMul;
                    classes.HitAdd = intud_Class_HitAdd.Value ?? oldClassData.HitAdd;
                }
            }

            #endregion

            #region Passive critical

            // Store critical rate
            if (check_Class_PassiveCritRate.IsChecked == true)
            {
                classes.CustomPassiveCritRate = true;
                if (checkBox != "check_Class_PassiveCritRate")
                {
                    classes.CritRateMul = decud_Class_CritRateMul.Value ?? oldClassData.CritRateMul;
                    classes.CritRateAdd = intud_Class_CritRateAdd.Value ?? oldClassData.CritRateAdd;
                }
            }

            // Store critical damage
            if (check_Class_PassiveCritDamage.IsChecked == true)
            {
                classes.CustomPassiveCritDamage = true;
                if (checkBox != "check_Class_PassiveCritDamage")
                {
                    classes.CritDamageMul = decud_Class_CritDamageMul.Value ?? oldClassData.CritDamageMul;
                    classes.CritDamageAdd = intud_Class_CritDamageAdd.Value ?? oldClassData.CritDamageAdd;
                }
            }

            // Store critical guard rate reduction
            if (check_Class_PassiveCritDefGuard.IsChecked == true)
            {
                classes.CustomPassiveCritDefGuard = true;
                if (checkBox != "check_Class_PassiveCritDefGuard")
                {
                    classes.CritDefGuardMul = decud_Class_CritDefGuardMul.Value ?? oldClassData.CritDefGuardMul;
                    classes.CritDefGuardAdd = intud_Class_CritDefGuardAdd.Value ?? oldClassData.CritDefGuardAdd;
                }
            }

            // Store critical evasion rate reduction
            if (check_Class_PassiveCritDefEva.IsChecked == true)
            {
                classes.CustomPassiveCritDefEva = true;
                if (checkBox != "check_Class_PassiveCritDefEva")
                {
                    classes.CritDefEvaMul = decud_Class_CritDefEvaMul.Value ?? oldClassData.CritDefEvaMul;
                    classes.CritDefEvaAdd = intud_Class_CritDefEvaAdd.Value ?? oldClassData.CritDefEvaAdd;
                }
            }

            #endregion

            #region Passive special Critical

            // Store special critical rate
            if (check_Class_PassiveSpCritRate.IsChecked == true)
            {
                classes.CustomPassiveSpCritRate = true;
                if (checkBox != "check_Class_PassiveSpCritRate")
                {
                    classes.SpCritRateMul = decud_Class_SpCritRateMul.Value ?? oldClassData.SpCritRateMul;
                    classes.SpCritRateAdd = intud_Class_SpCritRateAdd.Value ?? oldClassData.SpCritRateAdd;
                }
            }

            // Store special critical damage
            if (check_Class_PassiveSpCritDamage.IsChecked == true)
            {
                classes.CustomPassiveSpCritDamage = true;
                if (checkBox != "check_Class_PassiveSpCritDamage")
                {
                    classes.SpCritDamageMul = decud_Class_SpCritDamageMul.Value ?? oldClassData.SpCritDamageMul;
                    classes.SpCritDamageAdd = intud_Class_SpCritDamageAdd.Value ?? oldClassData.SpCritDamageAdd;
                }
            }

            // Store special critical guard rate reduction
            if (check_Class_PassiveSpCritDefGuard.IsChecked == true)
            {
                classes.CustomPassiveSpCritDefGuard = true;
                if (checkBox != "check_Class_PassiveSpCritDefGuard")
                {
                    classes.SpCritDefGuardMul = decud_Class_SpCritDefGuardMul.Value ?? oldClassData.SpCritDefGuardMul;
                    classes.SpCritDefGuardAdd = intud_Class_SpCritDefGuardAdd.Value ?? oldClassData.SpCritDefGuardAdd;
                }
            }

            // Store special critical evasion rate reduction
            if (check_Class_PassiveSpCritDefEva.IsChecked == true)
            {
                classes.CustomPassiveSpCritDefEva = true;
                if (checkBox != "check_Class_PassiveSpCritDefEva")
                {
                    classes.SpCritDefEvaMul = decud_Class_SpCritDefEvaMul.Value ?? oldClassData.SpCritDefEvaMul;
                    classes.SpCritDefEvaAdd = intud_Class_SpCritDefEvaAdd.Value ?? oldClassData.SpCritDefEvaAdd;
                }
            }

            #endregion

            #region Passive defense

            // Store physical defense
            if (check_Class_PassivePDef.IsChecked == true)
            {
                classes.CustomPassivePDef = true;
                if (checkBox != "check_Class_PassivePDef")
                {
                    classes.PDefMul = decud_Class_PDefMul.Value ?? oldClassData.PDefMul;
                    classes.PDefAdd = intud_Class_PDefAdd.Value ?? oldClassData.PDefAdd;
                }
            }

            // Store magical defense
            if (check_Class_PassiveMDef.IsChecked == true)
            {
                classes.CustomPassiveMDef = true;
                if (checkBox != "check_Class_PassiveMDef")
                {
                    classes.MDefMul = decud_Class_MDefMul.Value ?? oldClassData.MDefMul;
                    classes.MDefAdd = intud_Class_MDefAdd.Value ?? oldClassData.MDefAdd;
                }
            }

            #endregion

            #region Unarmed Attack

            // Store unarmed attack
            if (check_Class_UnarmedAtk.IsChecked == true)
            {
                classes.CustomUnarmedAtk = true;
                if (checkBox != "check_Class_UnarmedAtk")
                {
                    classes.AtkInitial = intud_Class_AtkInitial.Value ?? oldClassData.AtkInitial;
                    classes.AtkFinal = intud_Class_AtkFinal.Value ?? oldClassData.AtkFinal;
                }
                else
                {
                    classes.AtkInitial = cfg.Default.AtkInitial;
                    classes.AtkFinal = cfg.Default.AtkFinal;
                }
            }

            // Store unarmed hit rate
            if (check_Class_UnarmedHit.IsChecked == true)
            {
                classes.CustomUnarmedHit = true;
                if (checkBox != "check_Class_UnarmedHit")
                {
                    classes.HitInitial = decud_Class_HitInitial.Value ?? oldClassData.HitInitial;
                    classes.HitFinal = decud_Class_HitFinal.Value ?? oldClassData.HitFinal;
                }
                else
                {
                    classes.HitInitial = cfg.Default.HitInitial;
                    classes.HitFinal = cfg.Default.HitFinal;
                }
            }

            // Store unarmed attack animation
            if (check_Class_UnarmedAnim.IsChecked == true)
            {
                classes.CustomUnarmedAnim = true;
                if (checkBox != "check_Class_UnarmedAnim")
                {
                    classes.AnimCaster = combo_Class_AnimCaster.SelectedIndex;
                    classes.AnimTarget = combo_Class_AnimTarget.SelectedIndex;
                }
                else
                {
                    classes.AnimCaster = cfg.Default.AnimCaster;
                    classes.AnimTarget = cfg.Default.AnimTarget;
                }
            }

            // Store unarmed parameter attack force
            if (check_Class_UnarmedParamForce.IsChecked == true)
            {
                classes.CustomUnarmedParamForce = true;
                if (checkBox != "check_Class_UnarmedParamForce")
                {
                    classes.StrForce = decud_Class_StrForce.Value ?? oldClassData.StrForce;
                    classes.DexForce = decud_Class_DexForce.Value ?? oldClassData.DexForce;
                    classes.AgiForce = decud_Class_AgiForce.Value ?? oldClassData.AgiForce;
                    classes.IntForce = decud_Class_IntForce.Value ?? oldClassData.IntForce;
                }
                else
                {
                    classes.StrForce = cfg.Default.StrForce;
                    classes.DexForce = cfg.Default.DexForce;
                    classes.AgiForce = cfg.Default.AgiForce;
                    classes.IntForce = cfg.Default.IntForce;
                }
            }

            // Store unarmed defense attack force
            if (check_Class_UnarmedDefenseForce.IsChecked == true)
            {
                classes.CustomUnarmedDefenseForce = true;
                if (checkBox != "check_Class_UnarmedDefenseForce")
                {
                    classes.PDefForce = decud_Class_PDefForce.Value ?? oldClassData.PDefForce;
                    classes.MDefForce = decud_Class_MDefForce.Value ?? oldClassData.MDefForce;
                }
                else
                {
                    classes.PDefForce = cfg.Default.PDefForce;
                    classes.MDefForce = cfg.Default.MDefForce;
                }
            }

            #endregion

            #region Unarmed Critical

            // Store unarmed critical rate
            if (check_Class_UnarmedCritRate.IsChecked == true)
            {
                classes.CustomUnarmedCritRate = true;
                if (checkBox != "check_Class_UnarmedCritRate")
                {
                    classes.CritRate = decud_Class_CritRate.Value ?? oldClassData.CritRate;
                }
                else
                {
                    classes.CritRate = cfg.Default.CritRate;
                }
            }

            // Store unarmed critical damage
            if (check_Class_UnarmedCritDamage.IsChecked == true)
            {
                classes.CustomUnarmedCritDamage = true;
                if (checkBox != "check_Class_UnarmedCritDamage")
                {
                    classes.CritDamage = decud_Class_CritDamage.Value ?? oldClassData.CritDamage;
                }
                else
                {
                    classes.CritDamage = cfg.Default.CritDamage;
                }
            }

            // Store unarmed critical guard reduction
            if (check_Class_UnarmedCritDefGuard.IsChecked == true)
            {
                classes.CustomUnarmedCritDefGuard = true;
                if (checkBox != "check_Class_UnarmedCritDefGuard")
                {
                    classes.CritDefGuard = decud_Class_CritDefGuard.Value ?? oldClassData.CritDefGuard;
                }
                else
                {
                    classes.CritDefGuard = cfg.Default.CritDefGuard;
                }
            }

            // Store unarmed critical evasion reduction
            if (check_Class_UnarmedCritDefEva.IsChecked == true)
            {
                classes.CustomUnarmedCritDefEva = true;
                if (checkBox != "check_Class_UnarmedCritDefEva")
                {
                    classes.CritDefEva = decud_Class_CritDefEva.Value ?? oldClassData.CritDefEva;
                }
                else
                {
                    classes.CritDefEva = cfg.Default.CritDefEva;
                }
            }

            #endregion

            #region Unarmed Special Critical

            // Store unarmed special critical rate
            if (check_Class_UnarmedSpCritRate.IsChecked == true)
            {
                classes.CustomUnarmedSpCritRate = true;
                if (checkBox != "check_Class_UnarmedSpCritRate")
                {
                    classes.SpCritRate = decud_Class_SpCritRate.Value ?? oldClassData.SpCritRate;
                }
                else
                {
                    classes.SpCritRate = cfg.Default.SpCritRate;
                }
            }

            // Store unarmed special critical damage
            if (check_Class_UnarmedSpCritDamage.IsChecked == true)
            {
                classes.CustomUnarmedSpCritDamage = true;
                if (checkBox != "check_Class_UnarmedSpCritDamage")
                {
                    classes.SpCritDamage = decud_Class_SpCritDamage.Value ?? oldClassData.SpCritDamage;
                }
                else
                {
                    classes.SpCritDamage = cfg.Default.SpCritDamage;
                }
            }

            // Store unarmed critical guard reduction
            if (check_Class_UnarmedSpCritDefGuard.IsChecked == true)
            {
                classes.CustomUnarmedSpCritDefGuard = true;
                if (checkBox != "check_Class_UnarmedSpCritDefGuard")
                {
                    classes.SpCritDefGuard = decud_Class_SpCritDefGuard.Value ?? oldClassData.SpCritDefGuard;
                }
                else
                {
                    classes.SpCritDefGuard = cfg.Default.SpCritDefGuard;
                }
            }

            // Store unarmed critical evasion reduction
            if (check_Class_UnarmedSpCritDefEva.IsChecked == true)
            {
                classes.CustomUnarmedSpCritDefEva = true;
                if (checkBox != "check_Class_UnarmedSpCritDefEva")
                {
                    classes.SpCritDefEva = decud_Class_SpCritDefEva.Value ?? oldClassData.SpCritDefEva;
                }
                else
                {
                    classes.SpCritDefEva = cfg.Default.SpCritDefEva;
                }
            }

            #endregion

            #region Unarmoured Defense

            // Store unarmoured physical defense
            if (check_Class_UnarmouredPDef.IsChecked == true)
            {
                classes.CustomUnarmouredPDef = true;
                if (checkBox != "check_Class_UnarmouredPDef")
                {
                    classes.PDefInitial = intud_Class_PDefInitial.Value ?? oldClassData.PDefInitial;
                    classes.PDefFinal = intud_Class_PDefFinal.Value ?? oldClassData.PDefFinal;
                }
                else
                {
                    classes.PDefInitial = cfg.Default.PDefInitial;
                    classes.PDefFinal = cfg.Default.PDefFinal;
                }
            }

            // Store unarmoured magical defense
            if (check_Class_UnarmouredMDef.IsChecked == true)
            {
                classes.CustomUnarmouredMDef = true;
                if (checkBox != "check_Class_UnarmouredMDef")
                {
                    classes.MDefInitial = intud_Class_MDefInitial.Value ?? oldClassData.MDefInitial;
                    classes.MDefFinal = intud_Class_MDefFinal.Value ?? oldClassData.MDefFinal;
                }
                else
                {
                    classes.MDefInitial = cfg.Default.MDefInitial;
                    classes.MDefFinal = cfg.Default.MDefFinal;
                }
            }

            #endregion

            // Remove the updating flag
            updating = false;

            return classes;
        }

        #endregion

        #region Store Family

        private void storeClassFamily(int index, string checkBox)
        {
            if (tree_Class_IsFamily() && index > 0 && index <= cfg.ClassFamily.Count && cfg.ClassFamily.Count > 0)
            {
                cfg.ClassFamily[index - 1].CloneFrom(getClassData(index, checkBox, cfg.ClassFamily[index - 1]));
            }
        }

        private void storeClassFamily(string checkBox)
        {
            storeClassFamily(tree_Class_ID(), checkBox);
        }

        private void storeClassFamily(int index)
        {
            storeClassFamily(index, "");
        }

        private void storeClassFamily()
        {
            storeClassFamily(tree_Class_ID(), "");
        }

        #endregion

        #region Store Individual

        private void storeClassID(int index, string checkBox)
        {
            if (tree_Class_IsIndividual() && index > 0 && index <= cfg.ClassID.Count && cfg.ClassID.Count > 0)
            {
                cfg.ClassID[index - 1].CloneFrom(getClassData(index, checkBox, cfg.ClassID[index - 1]));
            }
        }

        private void storeClassID(string checkBox)
        {
            storeClassID(tree_Class_ID(), checkBox);
        }

        private void storeClassID(int index)
        {
            storeClassID(index, "");
        }

        private void storeClassID()
        {
            storeClassID(tree_Class_ID(), "");
        }

        #endregion

        #region Store Default

        private void storeClassDefault(string checkBox)
        {
            cfg.ClassDefault.CloneFrom(getClassData(0, checkBox, cfg.ClassDefault));
        }

        private void storeClassDefault()
        {
            storeClassDefault("");
        }

        #endregion

        #endregion

        #region Skill

        #region Get Data

        public DataPackSkill getSkillData(int index, string checkBox, DataPackSkill oldSkillData)
        {
            DataPackSkill skill = new DataPackSkill();

            // Set the updating flag
            updating = true;

            // Set the id of the data
            skill.ID = index;

            // Set the name of the data
            skill.Name = txt_Skill_Name.Text;

            #region In family

            skill.SkillFamily.Clear();
            if (skillInFamily.Count > 0)
            {
                foreach (Skill skills in skillInFamily)
                {
                    skill.SkillFamily.Add(new Skill(skills.ID, skills.Name));
                }
            }

            cfg.SkillAvailable.Clear();
            if (skillAvailable.Count > 0)
            {
                foreach (Skill skills in skillAvailable)
                {
                    cfg.SkillAvailable.Add(new Skill(skills.ID, skills.Name));
                }
            }

            #endregion

            #region Attack

            // Store attack
            if (check_Skill_Atk.IsChecked == true)
            {
                skill.CustomAtk = true;
                if (checkBox != "check_Skill_Atk")
                {
                    skill.AtkInitial = intud_Skill_AtkInitial.Value ?? oldSkillData.AtkInitial;
                }
                else
                {
                    skill.AtkInitial = cfg.Default.AtkInitial;
                }
            }

            // Store hit rate
            if (check_Skill_Hit.IsChecked == true)
            {
                skill.CustomHit = true;
                if (checkBox != "check_Skill_Hit")
                {
                    skill.HitInitial = decud_Skill_HitInitial.Value ?? oldSkillData.HitInitial;
                }
                else
                {
                    skill.HitInitial = cfg.Default.HitInitial;
                }
            }

            // Store parameter attack force
            if (check_Skill_ParamForce.IsChecked == true)
            {
                skill.CustomParamForce = true;
                if (checkBox != "check_Skill_ParamForce")
                {
                    skill.StrForce = decud_Skill_StrForce.Value ?? oldSkillData.StrForce;
                    skill.DexForce = decud_Skill_DexForce.Value ?? oldSkillData.DexForce;
                    skill.AgiForce = decud_Skill_AgiForce.Value ?? oldSkillData.AgiForce;
                    skill.IntForce = decud_Skill_IntForce.Value ?? oldSkillData.IntForce;
                }
                else
                {
                    skill.StrForce = cfg.Default.StrForce;
                    skill.DexForce = cfg.Default.DexForce;
                    skill.AgiForce = cfg.Default.AgiForce;
                    skill.IntForce = cfg.Default.IntForce;
                }
            }

            // Store defense attack force
            if (check_Skill_DefenseForce.IsChecked == true)
            {
                skill.CustomDefenseForce = true;
                if (checkBox != "check_Skill_DefenseForce")
                {
                    skill.PDefForce = decud_Skill_PDefForce.Value ?? oldSkillData.PDefForce;
                    skill.MDefForce = decud_Skill_MDefForce.Value ?? oldSkillData.MDefForce;
                }
                else
                {
                    skill.PDefForce = cfg.Default.PDefForce;
                    skill.MDefForce = cfg.Default.MDefForce;
                }
            }

            #endregion

            #region Critical

            // Store critical rate
            if (check_Skill_CritRate.IsChecked == true)
            {
                skill.CustomCritRate = true;
                if (checkBox != "check_Skill_CritRate")
                {
                    skill.CritRate = decud_Skill_CritRate.Value ?? oldSkillData.CritRate;
                }
                else
                {
                    skill.CritRate = cfg.Default.CritRate;
                }
            }

            // Store critical damage
            if (check_Skill_CritDamage.IsChecked == true)
            {
                skill.CustomCritDamage = true;
                if (checkBox != "check_Skill_CritDamage")
                {
                    skill.CritDamage = decud_Skill_CritDamage.Value ?? oldSkillData.CritDamage;
                }
                else
                {
                    skill.CritDamage = cfg.Default.CritDamage;
                }
            }

            // Store critical guard reduction
            if (check_Skill_CritDefGuard.IsChecked == true)
            {
                skill.CustomCritDefGuard = true;
                if (checkBox != "check_Skill_CritDefGuard")
                {
                    skill.CritDefGuard = decud_Skill_CritDefGuard.Value ?? oldSkillData.CritDefGuard;
                }
                else
                {
                    skill.CritDefGuard = cfg.Default.CritDefGuard;
                }
            }

            // Store critical evasion reduction
            if (check_Skill_CritDefEva.IsChecked == true)
            {
                skill.CustomCritDefEva = true;
                if (checkBox != "check_Skill_CritDefEva")
                {
                    skill.CritDefEva = decud_Skill_CritDefEva.Value ?? oldSkillData.CritDefEva;
                }
                else
                {
                    skill.CritDefEva = cfg.Default.CritDefEva;
                }
            }

            #endregion

            #region Special Critical

            // Store special critical rate
            if (check_Skill_SpCritRate.IsChecked == true)
            {
                skill.CustomSpCritRate = true;
                if (checkBox != "check_Skill_SpCritRate" && decud_Skill_SpCritRate.Value.HasValue)
                {
                    skill.SpCritRate = decud_Skill_SpCritRate.Value ?? oldSkillData.SpCritRate;
                }
                else
                {
                    skill.SpCritRate = cfg.Default.SpCritRate;
                }
            }

            // Store special critical damage
            if (check_Skill_SpCritDamage.IsChecked == true)
            {
                skill.CustomSpCritDamage = true;
                if (checkBox != "check_Skill_SpCritDamage")
                {
                    skill.SpCritDamage = decud_Skill_SpCritDamage.Value ?? oldSkillData.SpCritDamage;
                }
                else
                {
                    skill.SpCritDamage = cfg.Default.SpCritDamage;
                }
            }

            // Store critical guard reduction
            if (check_Skill_SpCritDefGuard.IsChecked == true)
            {
                skill.CustomSpCritDefGuard = true;
                if (checkBox != "check_Skill_SpCritDefGuard")
                {
                    skill.SpCritDefGuard = decud_Skill_SpCritDefGuard.Value ?? oldSkillData.SpCritDefGuard;
                }
                else
                {
                    skill.SpCritDefGuard = cfg.Default.SpCritDefGuard;
                }
            }

            // Store critical evasion reduction
            if (check_Skill_SpCritDefEva.IsChecked == true)
            {
                skill.CustomSpCritDefEva = true;
                if (checkBox != "check_Skill_SpCritDefEva")
                {
                    skill.SpCritDefEva = decud_Skill_SpCritDefEva.Value ?? oldSkillData.SpCritDefEva;
                }
                else
                {
                    skill.SpCritDefEva = cfg.Default.SpCritDefEva;
                }
            }

            #endregion

            // Remove the updating flag
            updating = false;

            return skill;
        }

        #endregion

        #region Store Family

        private void storeSkillFamily(int index, string checkBox)
        {
            if (tree_Skill_IsFamily() && index > 0 && index <= cfg.SkillFamily.Count && cfg.SkillFamily.Count > 0)
            {
                //textTxt.Text = "Test store";

                cfg.SkillFamily[index - 1].CloneFrom(getSkillData(index, checkBox, cfg.SkillFamily[index - 1]));
            }
        }

        private void storeSkillFamily(string checkBox)
        {
            storeSkillFamily(tree_Skill_ID(), checkBox);
        }

        private void storeSkillFamily(int index)
        {
            storeSkillFamily(index, "");
        }

        private void storeSkillFamily()
        {
            storeSkillFamily(tree_Skill_ID(), "");
        }

        #endregion

        #region Store Individual

        private void storeSkillID(int index, string checkBox)
        {
            if (tree_Skill_IsIndividual() && index > 0 && index <= cfg.SkillID.Count && cfg.SkillID.Count > 0)
            {
                cfg.SkillID[index - 1].CloneFrom(getSkillData(index, checkBox, cfg.SkillID[index - 1]));
            }
        }

        private void storeSkillID(string checkBox)
        {
            storeSkillID(tree_Skill_ID(), checkBox);
        }

        private void storeSkillID(int index)
        {
            storeSkillID(index, "");
        }

        private void storeSkillID()
        {
            storeSkillID(tree_Skill_ID(), "");
        }

        #endregion

        #region Store Default

        private void storeSkillDefault(string checkBox)
        {
            cfg.SkillDefault.CloneFrom(getSkillData(0, checkBox, cfg.SkillDefault));
        }

        private void storeSkillDefault()
        {
            storeSkillDefault("");
        }

        #endregion

        #endregion

        #region Passive Skill

        #region Get Data

        public DataPackPassiveSkill getPassiveSkillData(int index, string checkBox, DataPackPassiveSkill oldPassiveSkillData)
        {
            DataPackPassiveSkill passiveSkill = new DataPackPassiveSkill();

            // Set the updating flag
            updating = true;

            // Set the id of the data
            passiveSkill.ID = index;

            // Set the name of the data
            passiveSkill.Name = txt_PassiveSkill_Name.Text;

            #region In Family

            passiveSkill.PassiveSkillFamily.Clear();
            if (passiveSkillInFamily.Count > 0)
            {
                foreach (Skill skill in passiveSkillInFamily)
                {
                    passiveSkill.PassiveSkillFamily.Add(new Skill(skill.ID, skill.Name));
                }
            }

            cfg.PassiveSkillAvailable.Clear();
            if (passiveSkillAvailable.Count > 0)
            {
                foreach (Skill skill in passiveSkillAvailable)
                {
                    cfg.PassiveSkillAvailable.Add(new Skill(skill.ID, skill.Name));
                }
            }

            #endregion

            #region Parameter

            // Store maximum HP
            if (check_PassiveSkill_MaxHP.IsChecked == true)
            {
                passiveSkill.CustomMaxHP = true;
                if (checkBox != "check_PassiveSkill_MaxHP")
                {
                    passiveSkill.MaxHPMul = decud_PassiveSkill_MaxHPMul.Value ?? oldPassiveSkillData.MaxHPMul;
                    passiveSkill.MaxHPAdd = intud_PassiveSkill_MaxHPAdd.Value ?? oldPassiveSkillData.MaxHPAdd;
                }
            }

            // Store maximum SP
            if (check_PassiveSkill_MaxSP.IsChecked == true)
            {
                passiveSkill.CustomMaxSP = true;
                if (checkBox != "check_PassiveSkill_MaxSP")
                {
                    passiveSkill.MaxSPMul = decud_PassiveSkill_MaxSPMul.Value ?? oldPassiveSkillData.MaxSPMul;
                    passiveSkill.MaxSPAdd = intud_PassiveSkill_MaxSPAdd.Value ?? oldPassiveSkillData.MaxSPAdd;
                }
            }

            // Store strengh
            if (check_PassiveSkill_Str.IsChecked == true)
            {
                passiveSkill.CustomStr = true;
                if (checkBox != "check_PassiveSkill_Str")
                {
                    passiveSkill.StrMul = decud_PassiveSkill_StrMul.Value ?? oldPassiveSkillData.StrMul;
                    passiveSkill.StrAdd = intud_PassiveSkill_StrAdd.Value ?? oldPassiveSkillData.StrAdd;
                }
            }

            // Store dexterity
            if (check_PassiveSkill_Dex.IsChecked == true)
            {
                passiveSkill.CustomDex = true;
                if (checkBox != "check_PassiveSkill_Dex")
                {
                    passiveSkill.DexMul = decud_PassiveSkill_DexMul.Value ?? oldPassiveSkillData.DexMul;
                    passiveSkill.DexAdd = intud_PassiveSkill_DexAdd.Value ?? oldPassiveSkillData.DexAdd;
                }
            }

            // Store agility
            if (check_PassiveSkill_Agi.IsChecked == true)
            {
                passiveSkill.CustomAgi = true;
                if (checkBox != "check_PassiveSkill_Agi")
                {
                    passiveSkill.AgiMul = decud_PassiveSkill_AgiMul.Value ?? oldPassiveSkillData.AgiMul;
                    passiveSkill.AgiAdd = intud_PassiveSkill_AgiAdd.Value ?? oldPassiveSkillData.AgiAdd;
                }
            }

            // Store intelligence
            if (check_PassiveSkill_Int.IsChecked == true)
            {
                passiveSkill.CustomInt = true;
                if (checkBox != "check_PassiveSkill_Int")
                {
                    passiveSkill.IntMul = decud_PassiveSkill_IntMul.Value ?? oldPassiveSkillData.IntMul;
                    passiveSkill.IntAdd = intud_PassiveSkill_IntAdd.Value ?? oldPassiveSkillData.IntAdd;
                }
            }

            #endregion

            #region Parameter Rate

            // Store strengh rate
            if (check_PassiveSkill_StrRate.IsChecked == true)
            {
                passiveSkill.CustomStrRate = true;
                if (checkBox != "check_PassiveSkill_StrRate")
                {
                    passiveSkill.StrRateMul = decud_PassiveSkill_StrRateMul.Value ?? oldPassiveSkillData.StrRateMul;
                    passiveSkill.StrRateAdd = intud_PassiveSkill_StrRateAdd.Value ?? oldPassiveSkillData.StrRateAdd;
                }
            }

            // Store dexterity rate
            if (check_PassiveSkill_DexRate.IsChecked == true)
            {
                passiveSkill.CustomDexRate = true;
                if (checkBox != "check_PassiveSkill_DexRate")
                {
                    passiveSkill.DexRateMul = decud_PassiveSkill_DexRateMul.Value ?? oldPassiveSkillData.DexRateMul;
                    passiveSkill.DexRateAdd = intud_PassiveSkill_DexRateAdd.Value ?? oldPassiveSkillData.DexRateAdd;
                }
            }

            // Store agility rate
            if (check_PassiveSkill_AgiRate.IsChecked == true)
            {
                passiveSkill.CustomAgiRate = true;
                if (checkBox != "check_PassiveSkill_AgiRate")
                {
                    passiveSkill.AgiRateMul = decud_PassiveSkill_AgiRateMul.Value ?? oldPassiveSkillData.AgiRateMul;
                    passiveSkill.AgiRateAdd = intud_PassiveSkill_AgiRateAdd.Value ?? oldPassiveSkillData.AgiRateAdd;
                }
            }

            // Store intelligence rate
            if (check_PassiveSkill_IntRate.IsChecked == true)
            {
                passiveSkill.CustomIntRate = true;
                if (checkBox != "check_PassiveSkill_IntRate")
                {
                    passiveSkill.IntRateMul = decud_PassiveSkill_IntRateMul.Value ?? oldPassiveSkillData.IntRateMul;
                    passiveSkill.IntRateAdd = intud_PassiveSkill_IntRateAdd.Value ?? oldPassiveSkillData.IntRateAdd;
                }
            }

            // Store physical defense rate
            if (check_PassiveSkill_PDefRate.IsChecked == true)
            {
                passiveSkill.CustomPDefRate = true;
                if (checkBox != "check_PassiveSkill_PDefRate")
                {
                    passiveSkill.PDefRateMul = decud_PassiveSkill_PDefRateMul.Value ?? oldPassiveSkillData.PDefRateMul;
                    passiveSkill.PDefRateAdd = intud_PassiveSkill_PDefRateAdd.Value ?? oldPassiveSkillData.PDefRateAdd;
                }
            }

            // Store magical defense rate
            if (check_PassiveSkill_MDefRate.IsChecked == true)
            {
                passiveSkill.CustomMDefRate = true;
                if (checkBox != "check_PassiveSkill_MDefRate")
                {
                    passiveSkill.MDefRateMul = decud_PassiveSkill_MDefRateMul.Value ?? oldPassiveSkillData.MDefRateMul;
                    passiveSkill.MDefRateAdd = intud_PassiveSkill_MDefRateAdd.Value ?? oldPassiveSkillData.MDefRateAdd;
                }
            }

            // Store guard rate
            if (check_PassiveSkill_GuardRate.IsChecked == true)
            {
                passiveSkill.CustomGuardRate = true;
                if (checkBox != "check_PassiveSkill_GuardRate")
                {
                    passiveSkill.GuardRateMul = decud_PassiveSkill_GuardRateMul.Value ?? oldPassiveSkillData.GuardRateMul;
                    passiveSkill.GuardRateAdd = intud_PassiveSkill_GuardRateAdd.Value ?? oldPassiveSkillData.GuardRateAdd;
                }
            }

            // Store evasion rate
            if (check_PassiveSkill_EvaRate.IsChecked == true)
            {
                passiveSkill.CustomEvaRate = true;
                if (checkBox != "check_PassiveSkill_EvaRate")
                {
                    passiveSkill.EvaRateMul = decud_PassiveSkill_EvaRateMul.Value ?? oldPassiveSkillData.EvaRateMul;
                    passiveSkill.EvaRateAdd = intud_PassiveSkill_EvaRateAdd.Value ?? oldPassiveSkillData.EvaRateAdd;
                }
            }

            #endregion

            #region Defense against attack critical

            // Store defense against attack critical rate
            if (check_PassiveSkill_DefCritRate.IsChecked == true)
            {
                passiveSkill.CustomDefCritRate = true;
                if (checkBox != "check_PassiveSkill_DefCritRate")
                {
                    passiveSkill.DefCritRateMul = decud_PassiveSkill_DefCritRateMul.Value ?? oldPassiveSkillData.DefCritRateMul;
                    passiveSkill.DefCritRateAdd = intud_PassiveSkill_DefCritRateAdd.Value ?? oldPassiveSkillData.DefCritRateAdd;
                }
            }

            // Store defense against attack critical damage
            if (check_PassiveSkill_DefCritDamage.IsChecked == true)
            {
                passiveSkill.CustomDefCritDamage = true;
                if (checkBox != "check_PassiveSkill_DefCritDamage")
                {
                    passiveSkill.DefCritDamageMul = decud_PassiveSkill_DefCritDamageMul.Value ?? oldPassiveSkillData.DefCritDamageMul;
                    passiveSkill.DefCritDamageAdd = intud_PassiveSkill_DefCritDamageAdd.Value ?? oldPassiveSkillData.DefCritDamageAdd;
                }
            }

            // Store defense against attack special critical rate
            if (check_PassiveSkill_DefSpCritRate.IsChecked == true)
            {
                passiveSkill.CustomDefSpCritRate = true;
                if (checkBox != "check_PassiveSkill_DefSpCritRate")
                {
                    passiveSkill.DefSpCritRateMul = decud_PassiveSkill_DefSpCritRateMul.Value ?? oldPassiveSkillData.DefSpCritRateMul;
                    passiveSkill.DefSpCritRateAdd = intud_PassiveSkill_DefSpCritRateAdd.Value ?? oldPassiveSkillData.DefSpCritRateAdd;
                }
            }

            // Store defense against attack special critical damage
            if (check_PassiveSkill_DefSpCritDamage.IsChecked == true)
            {
                passiveSkill.CustomDefSpCritDamage = true;
                if (checkBox != "check_PassiveSkill_DefSpCritDamage")
                {
                    passiveSkill.DefSpCritDamageMul = decud_PassiveSkill_DefSpCritDamageMul.Value ?? oldPassiveSkillData.DefSpCritDamageMul;
                    passiveSkill.DefSpCritDamageAdd = intud_PassiveSkill_DefSpCritDamageAdd.Value ?? oldPassiveSkillData.DefSpCritDamageAdd;
                }
            }

            #endregion

            #region Defense against Skill critical

            // Store defense against skill critical rate
            if (check_PassiveSkill_DefSkillCritRate.IsChecked == true)
            {
                passiveSkill.CustomDefSkillCritRate = true;
                if (checkBox != "check_PassiveSkill_DefSkillCritRate")
                {
                    passiveSkill.DefSkillCritRateMul = decud_PassiveSkill_DefSkillCritRateMul.Value ?? oldPassiveSkillData.DefSkillCritRateMul;
                    passiveSkill.DefSkillCritRateAdd = intud_PassiveSkill_DefSkillCritRateAdd.Value ?? oldPassiveSkillData.DefSkillCritRateAdd;
                }
            }

            // Store defense against skill critical damage
            if (check_PassiveSkill_DefSkillCritDamage.IsChecked == true)
            {
                passiveSkill.CustomDefSkillCritDamage = true;
                if (checkBox != "check_PassiveSkill_DefSkillCritDamage")
                {
                    passiveSkill.DefSkillCritDamageMul = decud_PassiveSkill_DefSkillCritDamageMul.Value ?? oldPassiveSkillData.DefSkillCritDamageMul;
                    passiveSkill.DefSkillCritDamageAdd = intud_PassiveSkill_DefSkillCritDamageAdd.Value ?? oldPassiveSkillData.DefSkillCritDamageAdd;
                }
            }

            // Store defense against skill special critical rate
            if (check_PassiveSkill_DefSkillSpCritRate.IsChecked == true)
            {
                passiveSkill.CustomDefSkillSpCritRate = true;
                if (checkBox != "check_PassiveSkill_DefSkillSpCritRate")
                {
                    passiveSkill.DefSkillSpCritRateMul = decud_PassiveSkill_DefSkillSpCritRateMul.Value ?? oldPassiveSkillData.DefSkillSpCritRateMul;
                    passiveSkill.DefSkillSpCritRateAdd = intud_PassiveSkill_DefSkillSpCritRateAdd.Value ?? oldPassiveSkillData.DefSkillSpCritRateAdd;
                }
            }

            // Store defense against skill special critical damage
            if (check_PassiveSkill_DefSkillSpCritDamage.IsChecked == true)
            {
                passiveSkill.CustomDefSkillSpCritDamage = true;
                if (checkBox != "check_PassiveSkill_DefSkillSpCritDamage")
                {
                    passiveSkill.DefSkillSpCritDamageMul = decud_PassiveSkill_DefSkillSpCritDamageMul.Value ?? oldPassiveSkillData.DefSkillSpCritDamageMul;
                    passiveSkill.DefSkillSpCritDamageAdd = intud_PassiveSkill_DefSkillSpCritDamageAdd.Value ?? oldPassiveSkillData.DefSkillSpCritDamageAdd;
                }
            }


            #endregion

            #region Attack

            // Store attack
            if (check_PassiveSkill_Atk.IsChecked == true)
            {
                passiveSkill.CustomAtk = true;
                if (checkBox != "check_PassiveSkill_Atk")
                {
                    passiveSkill.AtkMul = decud_PassiveSkill_AtkMul.Value ?? oldPassiveSkillData.AtkMul;
                    passiveSkill.AtkAdd = intud_PassiveSkill_AtkAdd.Value ?? oldPassiveSkillData.AtkAdd;
                }
            }

            // Store hit rate
            if (check_PassiveSkill_Hit.IsChecked == true)
            {
                passiveSkill.CustomHit = true;
                if (checkBox != "check_PassiveSkill_Hit")
                {
                    passiveSkill.HitMul = decud_PassiveSkill_HitMul.Value ?? oldPassiveSkillData.HitMul;
                    passiveSkill.HitAdd = intud_PassiveSkill_HitAdd.Value ?? oldPassiveSkillData.HitAdd;
                }
            }

            #endregion

            #region Critical

            // Store critical rate
            if (check_PassiveSkill_CritRate.IsChecked == true)
            {
                passiveSkill.CustomCritRate = true;
                if (checkBox != "check_PassiveSkill_CritRate")
                {
                    passiveSkill.CritRateMul = decud_PassiveSkill_CritRateMul.Value ?? oldPassiveSkillData.CritRateMul;
                    passiveSkill.CritRateAdd = intud_PassiveSkill_CritRateAdd.Value ?? oldPassiveSkillData.CritRateAdd;
                }
            }

            // Store critical damage
            if (check_PassiveSkill_CritDamage.IsChecked == true)
            {
                passiveSkill.CustomCritDamage = true;
                if (checkBox != "check_PassiveSkill_CritDamage")
                {
                    passiveSkill.CritDamageMul = decud_PassiveSkill_CritDamageMul.Value ?? oldPassiveSkillData.CritDamageMul;
                    passiveSkill.CritDamageAdd = intud_PassiveSkill_CritDamageAdd.Value ?? oldPassiveSkillData.CritDamageAdd;
                }
            }

            // Store critical guard rate reduction
            if (check_PassiveSkill_CritDefGuard.IsChecked == true)
            {
                passiveSkill.CustomCritDefGuard = true;
                if (checkBox != "check_PassiveSkill_CritDefGuard")
                {
                    passiveSkill.CritDefGuardMul = decud_PassiveSkill_CritDefGuardMul.Value ?? oldPassiveSkillData.CritDefGuardMul;
                    passiveSkill.CritDefGuardAdd = intud_PassiveSkill_CritDefGuardAdd.Value ?? oldPassiveSkillData.CritDefGuardAdd;
                }
            }

            // Store critical evasion rate reduction
            if (check_PassiveSkill_CritDefEva.IsChecked == true)
            {
                passiveSkill.CustomCritDefEva = true;
                if (checkBox != "check_PassiveSkill_CritDefEva")
                {
                    passiveSkill.CritDefEvaMul = decud_PassiveSkill_CritDefEvaMul.Value ?? oldPassiveSkillData.CritDefEvaMul;
                    passiveSkill.CritDefEvaAdd = intud_PassiveSkill_CritDefEvaAdd.Value ?? oldPassiveSkillData.CritDefEvaAdd;
                }
            }

            #endregion

            #region Special Critical

            // Store special critical rate
            if (check_PassiveSkill_SpCritRate.IsChecked == true)
            {
                passiveSkill.CustomSpCritRate = true;
                if (checkBox != "check_PassiveSkill_SpCritRate")
                {
                    passiveSkill.SpCritRateMul = decud_PassiveSkill_SpCritRateMul.Value ?? oldPassiveSkillData.SpCritRateMul;
                    passiveSkill.SpCritRateAdd = intud_PassiveSkill_SpCritRateAdd.Value ?? oldPassiveSkillData.SpCritRateAdd;
                }
            }

            // Store special critical damage
            if (check_PassiveSkill_SpCritDamage.IsChecked == true)
            {
                passiveSkill.CustomSpCritDamage = true;
                if (checkBox != "check_PassiveSkill_SpCritDamage")
                {
                    passiveSkill.SpCritDamageMul = decud_PassiveSkill_SpCritDamageMul.Value ?? oldPassiveSkillData.SpCritDamageMul;
                    passiveSkill.SpCritDamageAdd = intud_PassiveSkill_SpCritDamageAdd.Value ?? oldPassiveSkillData.SpCritDamageAdd;
                }
            }

            // Store special critical guard rate reduction
            if (check_PassiveSkill_SpCritDefGuard.IsChecked == true)
            {
                passiveSkill.CustomSpCritDefGuard = true;
                if (checkBox != "check_PassiveSkill_SpCritDefGuard")
                {
                    passiveSkill.SpCritDefGuardMul = decud_PassiveSkill_SpCritDefGuardMul.Value ?? oldPassiveSkillData.SpCritDefGuardMul;
                    passiveSkill.SpCritDefGuardAdd = intud_PassiveSkill_SpCritDefGuardAdd.Value ?? oldPassiveSkillData.SpCritDefGuardAdd;
                }
            }

            // Store special critical evasion rate reduction
            if (check_PassiveSkill_SpCritDefEva.IsChecked == true)
            {
                passiveSkill.CustomSpCritDefEva = true;
                if (checkBox != "check_PassiveSkill_SpCritDefEva")
                {
                    passiveSkill.SpCritDefEvaMul = decud_PassiveSkill_SpCritDefEvaMul.Value ?? oldPassiveSkillData.SpCritDefEvaMul;
                    passiveSkill.SpCritDefEvaAdd = intud_PassiveSkill_SpCritDefEvaAdd.Value ?? oldPassiveSkillData.SpCritDefEvaAdd;
                }
            }

            #endregion

            #region Defense

            // Store physical defense
            if (check_PassiveSkill_PDef.IsChecked == true)
            {
                passiveSkill.CustomPDef = true;
                if (checkBox != "check_PassiveSkill_PDef")
                {
                    passiveSkill.PDefMul = decud_PassiveSkill_PDefMul.Value ?? oldPassiveSkillData.PDefMul;
                    passiveSkill.PDefAdd = intud_PassiveSkill_PDefAdd.Value ?? oldPassiveSkillData.PDefAdd;
                }
            }

            // Store magical defense
            if (check_PassiveSkill_MDef.IsChecked == true)
            {
                passiveSkill.CustomMDef = true;
                if (checkBox != "check_PassiveSkill_MDef")
                {
                    passiveSkill.MDefMul = decud_PassiveSkill_MDefMul.Value ?? oldPassiveSkillData.MDefMul;
                    passiveSkill.MDefAdd = intud_PassiveSkill_MDefAdd.Value ?? oldPassiveSkillData.MDefAdd;
                }
            }

            #endregion

            // Remove the updating flag
            updating = false;

            return passiveSkill;
        }

        #endregion

        #region Store Family

        private void storePassiveSkillFamily(int index, string checkBox)
        {
            if (tree_PassiveSkill_IsFamily() && index > 0 && index <= cfg.PassiveSkillFamily.Count && cfg.PassiveSkillFamily.Count > 0)
            {
                cfg.PassiveSkillFamily[index - 1].CloneFrom(getPassiveSkillData(index, checkBox, cfg.PassiveSkillFamily[index - 1]));
            }
        }

        private void storePassiveSkillFamily(string checkBox)
        {
            storePassiveSkillFamily(tree_PassiveSkill_ID(), checkBox);
        }

        private void storePassiveSkillFamily(int index)
        {
            storePassiveSkillFamily(index, "");
        }

        private void storePassiveSkillFamily()
        {
            storePassiveSkillFamily(tree_PassiveSkill_ID(), "");
        }

        #endregion

        #region Store Individual

        private void storePassiveSkillID(int index, string checkBox)
        {
            if (tree_PassiveSkill_IsIndividual() && index > 0 && index <= cfg.PassiveSkillID.Count && cfg.PassiveSkillID.Count > 0)
            {
                cfg.PassiveSkillID[index - 1].CloneFrom(getPassiveSkillData(index, checkBox, cfg.PassiveSkillID[index - 1]));
            }
        }

        private void storePassiveSkillID(string checkBox)
        {
            storePassiveSkillID(tree_PassiveSkill_ID(), checkBox);
        }

        private void storePassiveSkillID(int index)
        {
            storePassiveSkillID(index, "");
        }

        private void storePassiveSkillID()
        {
            storePassiveSkillID(tree_PassiveSkill_ID(), "");
        }

        #endregion

        #region Store Default

        private void storePassiveSkillDefault(string checkBox)
        {
            cfg.PassiveSkillDefault.CloneFrom(getPassiveSkillData(0, checkBox, cfg.PassiveSkillDefault));
        }

        private void storePassiveSkillDefault()
        {
            storePassiveSkillDefault("");
        }

        #endregion

        #endregion

        #region Weapon

        #region Get Data

        public DataPackEquipment getWeaponData(int index, string checkBox, DataPackEquipment oldWeaponData)
        {
            DataPackEquipment weapon = new DataPackEquipment();

            // Set the updating flag
            updating = true;

            // Set the id of the data
            weapon.ID = index;

            // Set the name of the data
            weapon.Name = txt_Weapon_Name.Text;

            #region In family

            weapon.WeaponFamily.Clear();
            if (weaponInFamily.Count > 0)
            {
                foreach (Weapon weapons in weaponInFamily)
                {
                    weapon.WeaponFamily.Add(new Weapon(weapons.ID, weapons.Name));
                }
            }

            cfg.WeaponAvailable.Clear();
            if (weaponAvailable.Count > 0)
            {
                foreach (Weapon weapons in weaponAvailable)
                {
                    cfg.WeaponAvailable.Add(new Weapon(weapons.ID, weapons.Name));
                }
            }

            #endregion

            #region Weapon

            // Store the number of hand
            if (check_Weapon_Hand.IsChecked == true)
            {
                weapon.CustomHand = true;
                if (checkBox != "check_Weapon_Hand")
                {
                    weapon.Hand = intud_Weapon_Hand.Value ?? oldWeaponData.Hand;
                }
            }

            // Store the shield slot only
            if (check_Weapon_ShieldOnly.IsChecked == true)
            {
                weapon.ShieldOnly = true;
            }

            // Store the weapon slot only
            if (check_Weapon_WeaponOnly.IsChecked == true)
            {
                weapon.WeaponOnly = true;
            }

            // Store the cursed flag
            if (check_Weapon_Cursed.IsChecked == true)
            {
                weapon.Cursed = true;
            }

            // Store the equiping switch id
            if (check_Weapon_SwitchID.IsChecked == true)
            {
                weapon.CustomSwitchID = true;
                if (checkBox != "check_Weapon_SwitchID")
                {
                    weapon.SwitchID = combo_Weapon_SwitchID.SelectedIndex;
                }
            }

            #endregion

            #region Attack

            // Store attack
            if (check_Weapon_Atk.IsChecked == true)
            {
                weapon.CustomAtk = true;
                if (checkBox != "check_Weapon_Atk")
                {
                    weapon.AtkInitial = intud_Weapon_AtkInitial.Value ?? oldWeaponData.AtkInitial;
                }
                else
                {
                    weapon.AtkInitial = cfg.Default.AtkInitial;
                }
            }

            // Store hit rate
            if (check_Weapon_Hit.IsChecked == true)
            {
                weapon.CustomHit = true;
                if (checkBox != "check_Weapon_Hit")
                {
                    weapon.HitInitial = decud_Weapon_HitInitial.Value ?? oldWeaponData.HitInitial;
                }
                else
                {
                    weapon.HitInitial = cfg.Default.HitInitial;
                }
            }

            // Store parameter force
            if (check_Weapon_ParamForce.IsChecked == true)
            {
                weapon.CustomParamForce = true;
                if (checkBox != "check_Weapon_ParamForce")
                {
                    weapon.StrForce = decud_Weapon_StrForce.Value ?? oldWeaponData.StrForce;
                    weapon.DexForce = decud_Weapon_DexForce.Value ?? oldWeaponData.DexForce;
                    weapon.AgiForce = decud_Weapon_AgiForce.Value ?? oldWeaponData.AgiForce;
                    weapon.IntForce = decud_Weapon_IntForce.Value ?? oldWeaponData.IntForce;
                }
                else
                {
                    weapon.StrForce = cfg.Default.StrForce;
                    weapon.DexForce = cfg.Default.DexForce;
                    weapon.AgiForce = cfg.Default.AgiForce;
                    weapon.IntForce = cfg.Default.IntForce;
                }
            }

            // Store defense force
            if (check_Weapon_DefenseForce.IsChecked == true)
            {
                weapon.CustomDefenseForce = true;
                if (checkBox != "check_Weapon_DefenseForce")
                {
                    weapon.PDefForce = decud_Weapon_PDefForce.Value ?? oldWeaponData.PDefForce;
                    weapon.MDefForce = decud_Weapon_MDefForce.Value ?? oldWeaponData.MDefForce;
                }
                else
                {
                    weapon.PDefForce = cfg.Default.PDefForce;
                    weapon.MDefForce = cfg.Default.MDefForce;
                }
            }

            #endregion

            #region Critical

            // Store critical rate
            if (check_Weapon_CritRate.IsChecked == true)
            {
                weapon.CustomCritRate = true;
                if (checkBox != "check_Weapon_CritRate")
                {
                    weapon.CritRateInitial = decud_Weapon_CritRateInitial.Value ?? oldWeaponData.CritRateInitial;
                }
                else
                {
                    weapon.CritRateInitial = cfg.Default.CritRate;
                }
            }

            // Store critical damage
            if (check_Weapon_CritDamage.IsChecked == true)
            {
                weapon.CustomCritDamage = true;
                if (checkBox != "check_Weapon_CritDamage")
                {
                    weapon.CritDamageInitial = decud_Weapon_CritDamageInitial.Value ?? oldWeaponData.CritDamageInitial;
                }
                else
                {
                    weapon.CritDamageInitial = cfg.Default.CritDamage;
                }
            }

            // Store critical guard reduction
            if (check_Weapon_CritDefGuard.IsChecked == true)
            {
                weapon.CustomCritDefGuard = true;
                if (checkBox != "check_Weapon_CritDefGuard")
                {
                    weapon.CritDefGuardInitial = decud_Weapon_CritDefGuardInitial.Value ?? oldWeaponData.CritDefGuardInitial;
                }
                else
                {
                    weapon.CritDefGuardInitial = cfg.Default.CritDefGuard;
                }
            }

            // Store critical evasion reduction
            if (check_Weapon_CritDefEva.IsChecked == true)
            {
                weapon.CustomCritDefEva = true;
                if (checkBox != "check_Weapon_CritDefEva")
                {
                    weapon.CritDefEvaInitial = decud_Weapon_CritDefEvaInitial.Value ?? oldWeaponData.CritDefEvaInitial;
                }
                else
                {
                    weapon.CritDefEvaInitial = cfg.Default.CritDefEva;
                }
            }

            #endregion

            #region Special Critical

            // Store special critical rate
            if (check_Weapon_SpCritRate.IsChecked == true)
            {
                weapon.CustomSpCritRate = true;
                if (checkBox != "check_Weapon_SpCritRate")
                {
                    weapon.SpCritRateInitial = decud_Weapon_SpCritRateInitial.Value ?? oldWeaponData.SpCritRateInitial;
                }
                else
                {
                    weapon.SpCritRateInitial = cfg.Default.SpCritRate;
                }
            }

            // Store special critical damage
            if (check_Weapon_SpCritDamage.IsChecked == true)
            {
                weapon.CustomSpCritDamage = true;
                if (checkBox != "check_Weapon_SpCritDamage")
                {
                    weapon.SpCritDamageInitial = decud_Weapon_SpCritDamageInitial.Value ?? oldWeaponData.SpCritDamageInitial;
                }
                else
                {
                    weapon.SpCritDamageInitial = cfg.Default.SpCritDamage;
                }
            }

            // Store critical guard reduction
            if (check_Weapon_SpCritDefGuard.IsChecked == true)
            {
                weapon.CustomSpCritDefGuard = true;
                if (checkBox != "check_Weapon_SpCritDefGuard")
                {
                    weapon.SpCritDefGuardInitial = decud_Weapon_SpCritDefGuardInitial.Value ?? oldWeaponData.SpCritDefGuardInitial;
                }
                else
                {
                    weapon.SpCritDefGuardInitial = cfg.Default.SpCritDefGuard;
                }
            }

            // Store critical evasion reduction
            if (check_Weapon_SpCritDefEva.IsChecked == true)
            {
                weapon.CustomSpCritDefEva = true;
                if (checkBox != "check_Weapon_SpCritDefEva")
                {
                    weapon.SpCritDefEvaInitial = decud_Weapon_SpCritDefEvaInitial.Value ?? oldWeaponData.SpCritDefEvaInitial;
                }
                else
                {
                    weapon.SpCritDefEvaInitial = cfg.Default.SpCritDefEva;
                }
            }

            #endregion

            #region Parameter

            // Store maximum HP
            if (check_Weapon_MaxHP.IsChecked == true)
            {
                weapon.CustomMaxHP = true;
                if (checkBox != "check_Weapon_MaxHP")
                {
                    weapon.MaxHPMul = decud_Weapon_MaxHPMul.Value ?? oldWeaponData.MaxHPMul;
                    weapon.MaxHPAdd = intud_Weapon_MaxHPAdd.Value ?? oldWeaponData.MaxHPAdd;
                }
            }

            // Store maximum SP
            if (check_Weapon_MaxSP.IsChecked == true)
            {
                weapon.CustomMaxSP = true;
                if (checkBox != "check_Weapon_MaxSP")
                {
                    weapon.MaxSPMul = decud_Weapon_MaxSPMul.Value ?? oldWeaponData.MaxSPMul;
                    weapon.MaxSPAdd = intud_Weapon_MaxSPAdd.Value ?? oldWeaponData.MaxSPAdd;
                }
            }

            // Store strengh
            if (check_Weapon_Str.IsChecked == true)
            {
                weapon.CustomStr = true;
                if (checkBox != "check_Weapon_Str")
                {
                    weapon.StrMul = decud_Weapon_StrMul.Value ?? oldWeaponData.StrMul;
                    weapon.StrAdd = intud_Weapon_StrAdd.Value ?? oldWeaponData.StrAdd;
                }
            }

            // Store dexterity
            if (check_Weapon_Dex.IsChecked == true)
            {
                weapon.CustomDex = true;
                if (checkBox != "check_Weapon_Dex")
                {
                    weapon.DexMul = decud_Weapon_DexMul.Value ?? oldWeaponData.DexMul;
                    weapon.DexAdd = intud_Weapon_DexAdd.Value ?? oldWeaponData.DexAdd;
                }
            }

            // Store agility
            if (check_Weapon_Agi.IsChecked == true)
            {
                weapon.CustomAgi = true;
                if (checkBox != "check_Weapon_Agi")
                {
                    weapon.AgiMul = decud_Weapon_AgiMul.Value ?? oldWeaponData.AgiMul;
                    weapon.AgiAdd = intud_Weapon_AgiAdd.Value ?? oldWeaponData.AgiAdd;
                }
            }

            // Store intelligence
            if (check_Weapon_Int.IsChecked == true)
            {
                weapon.CustomInt = true;
                if (checkBox != "check_Weapon_Int")
                {
                    weapon.IntMul = decud_Weapon_IntMul.Value ?? oldWeaponData.IntMul;
                    weapon.IntAdd = intud_Weapon_IntAdd.Value ?? oldWeaponData.IntAdd;
                }
            }

            #endregion

            #region Parameter Rate

            // Store strengh rate
            if (check_Weapon_StrRate.IsChecked == true)
            {
                weapon.CustomStrRate = true;
                if (checkBox != "check_Weapon_StrRate")
                {
                    weapon.StrRateMul = decud_Weapon_StrRateMul.Value ?? oldWeaponData.StrRateMul;
                    weapon.StrRateAdd = intud_Weapon_StrRateAdd.Value ?? oldWeaponData.StrRateAdd;
                }
            }

            // Store dexterity rate
            if (check_Weapon_DexRate.IsChecked == true)
            {
                weapon.CustomDexRate = true;
                if (checkBox != "check_Weapon_DexRate")
                {
                    weapon.DexRateMul = decud_Weapon_DexRateMul.Value ?? oldWeaponData.DexRateMul;
                    weapon.DexRateAdd = intud_Weapon_DexRateAdd.Value ?? oldWeaponData.DexRateAdd;
                }
            }

            // Store agility rate
            if (check_Weapon_AgiRate.IsChecked == true)
            {
                weapon.CustomAgiRate = true;
                if (checkBox != "check_Weapon_AgiRate")
                {
                    weapon.AgiRateMul = decud_Weapon_AgiRateMul.Value ?? oldWeaponData.AgiRateMul;
                    weapon.AgiRateAdd = intud_Weapon_AgiRateAdd.Value ?? oldWeaponData.AgiRateAdd;
                }
            }

            // Store intelligence rate
            if (check_Weapon_IntRate.IsChecked == true)
            {
                weapon.CustomIntRate = true;
                if (checkBox != "check_Weapon_IntRate")
                {
                    weapon.IntRateMul = decud_Weapon_IntRateMul.Value ?? oldWeaponData.IntRateMul;
                    weapon.IntRateAdd = intud_Weapon_IntRateAdd.Value ?? oldWeaponData.IntRateAdd;
                }
            }

            // Store physical defense rate
            if (check_Weapon_PDefRate.IsChecked == true)
            {
                weapon.CustomPDefRate = true;
                if (checkBox != "check_Weapon_PDefRate")
                {
                    weapon.PDefRateMul = decud_Weapon_PDefRateMul.Value ?? oldWeaponData.PDefRateMul;
                    weapon.PDefRateAdd = intud_Weapon_PDefRateAdd.Value ?? oldWeaponData.PDefRateAdd;
                }
            }

            // Store magical defense rate
            if (check_Weapon_MDefRate.IsChecked == true)
            {
                weapon.CustomMDefRate = true;
                if (checkBox != "check_Weapon_MDefRate")
                {
                    weapon.MDefRateMul = decud_Weapon_MDefRateMul.Value ?? oldWeaponData.MDefRateMul;
                    weapon.MDefRateAdd = intud_Weapon_MDefRateAdd.Value ?? oldWeaponData.MDefRateAdd;
                }
            }

            // Store guard rate
            if (check_Weapon_GuardRate.IsChecked == true)
            {
                weapon.CustomGuardRate = true;
                if (checkBox != "check_Weapon_GuardRate")
                {
                    weapon.GuardRateMul = decud_Weapon_GuardRateMul.Value ?? oldWeaponData.GuardRateMul;
                    weapon.GuardRateAdd = intud_Weapon_GuardRateAdd.Value ?? oldWeaponData.GuardRateAdd;
                }
            }

            // Store evasion rate
            if (check_Weapon_EvaRate.IsChecked == true)
            {
                weapon.CustomEvaRate = true;
                if (checkBox != "check_Weapon_EvaRate")
                {
                    weapon.EvaRateMul = decud_Weapon_EvaRateMul.Value ?? oldWeaponData.EvaRateMul;
                    weapon.EvaRateAdd = intud_Weapon_EvaRateAdd.Value ?? oldWeaponData.EvaRateAdd;
                }
            }

            #endregion

            #region Defense against attack critical

            // Store defense against attack critical rate
            if (check_Weapon_DefCritRate.IsChecked == true)
            {
                weapon.CustomDefCritRate = true;
                if (checkBox != "check_Weapon_DefCritRate")
                {
                    weapon.DefCritRateMul = decud_Weapon_DefCritRateMul.Value ?? oldWeaponData.DefCritRateMul;
                    weapon.DefCritRateAdd = intud_Weapon_DefCritRateAdd.Value ?? oldWeaponData.DefCritRateAdd;
                }
            }

            // Store defense against attack critical damage
            if (check_Weapon_DefCritDamage.IsChecked == true)
            {
                weapon.CustomDefCritDamage = true;
                if (checkBox != "check_Weapon_DefCritDamage")
                {
                    weapon.DefCritDamageMul = decud_Weapon_DefCritDamageMul.Value ?? oldWeaponData.DefCritDamageMul;
                    weapon.DefCritDamageAdd = intud_Weapon_DefCritDamageAdd.Value ?? oldWeaponData.DefCritDamageAdd;
                }
            }

            // Store defense against attack special critical rate
            if (check_Weapon_DefSpCritRate.IsChecked == true)
            {
                weapon.CustomDefSpCritRate = true;
                if (checkBox != "check_Weapon_DefSpCritRate")
                {
                    weapon.DefSpCritRateMul = decud_Weapon_DefSpCritRateMul.Value ?? oldWeaponData.DefSpCritRateMul;
                    weapon.DefSpCritRateAdd = intud_Weapon_DefSpCritRateAdd.Value ?? oldWeaponData.DefSpCritRateAdd;
                }
            }

            // Store defense against attack special critical damage
            if (check_Weapon_DefSpCritDamage.IsChecked == true)
            {
                weapon.CustomDefSpCritDamage = true;
                if (checkBox != "check_Weapon_DefSpCritDamage")
                {
                    weapon.DefSpCritDamageMul = decud_Weapon_DefSpCritDamageMul.Value ?? oldWeaponData.DefSpCritDamageMul;
                    weapon.DefSpCritDamageAdd = intud_Weapon_DefSpCritDamageAdd.Value ?? oldWeaponData.DefSpCritDamageAdd;
                }
            }

            #endregion

            #region Defense against Skill critical

            // Store defense against skill critical rate
            if (check_Weapon_DefSkillCritRate.IsChecked == true)
            {
                weapon.CustomDefSkillCritRate = true;
                if (checkBox != "check_Weapon_DefSkillCritRate")
                {
                    weapon.DefSkillCritRateMul = decud_Weapon_DefSkillCritRateMul.Value ?? oldWeaponData.DefSkillCritRateMul;
                    weapon.DefSkillCritRateAdd = intud_Weapon_DefSkillCritRateAdd.Value ?? oldWeaponData.DefSkillCritRateAdd;
                }
            }

            // Store defense against skill critical damage
            if (check_Weapon_DefSkillCritDamage.IsChecked == true)
            {
                weapon.CustomDefSkillCritDamage = true;
                if (checkBox != "check_Weapon_DefSkillCritDamage")
                {
                    weapon.DefSkillCritDamageMul = decud_Weapon_DefSkillCritDamageMul.Value ?? oldWeaponData.DefSkillCritDamageMul;
                    weapon.DefSkillCritDamageAdd = intud_Weapon_DefSkillCritDamageAdd.Value ?? oldWeaponData.DefSkillCritDamageAdd;
                }
            }

            // Store defense against skill special critical rate
            if (check_Weapon_DefSkillSpCritRate.IsChecked == true)
            {
                weapon.CustomDefSkillSpCritRate = true;
                if (checkBox != "check_Weapon_DefSkillSpCritRate")
                {
                    weapon.DefSkillSpCritRateMul = decud_Weapon_DefSkillSpCritRateMul.Value ?? oldWeaponData.DefSkillSpCritRateMul;
                    weapon.DefSkillSpCritRateAdd = intud_Weapon_DefSkillSpCritRateAdd.Value ?? oldWeaponData.DefSkillSpCritRateAdd;
                }
            }

            // Store defense against skill special critical damage
            if (check_Weapon_DefSkillSpCritDamage.IsChecked == true)
            {
                weapon.CustomDefSkillSpCritDamage = true;
                if (checkBox != "check_Weapon_DefSkillSpCritDamage")
                {
                    weapon.DefSkillSpCritDamageMul = decud_Weapon_DefSkillSpCritDamageMul.Value ?? oldWeaponData.DefSkillSpCritDamageMul;
                    weapon.DefSkillSpCritDamageAdd = intud_Weapon_DefSkillSpCritDamageAdd.Value ?? oldWeaponData.DefSkillSpCritDamageAdd;
                }
            }

            #endregion

            #region Defense

            // Store physical defense
            if (check_Weapon_PDef.IsChecked == true)
            {
                weapon.CustomPDef = true;
                if (checkBox != "check_Weapon_PDef")
                {
                    weapon.PDefMul = decud_Weapon_PDefMul.Value ?? oldWeaponData.PDefMul;
                    weapon.PDefAdd = intud_Weapon_PDefAdd.Value ?? oldWeaponData.PDefAdd;
                }
            }

            // Store magical defense
            if (check_Weapon_MDef.IsChecked == true)
            {
                weapon.CustomMDef = true;
                if (checkBox != "check_Weapon_MDef")
                {
                    weapon.MDefMul = decud_Weapon_MDefMul.Value ?? oldWeaponData.MDefMul;
                    weapon.MDefAdd = intud_Weapon_MDefAdd.Value ?? oldWeaponData.MDefAdd;
                }
            }

            #endregion

            // Remove the updating flag
            updating = false;

            return weapon;
        }

        #endregion

        #region Store Family

        private void storeWeaponFamily(int index, string checkBox)
        {
            if (tree_Weapon_IsFamily() && index > 0 && index <= cfg.WeaponFamily.Count && cfg.WeaponFamily.Count > 0)
            {
                //textTxt.Text = "Test store";

                cfg.WeaponFamily[index - 1].CloneFrom(getWeaponData(index, checkBox, cfg.WeaponFamily[index - 1]));
            }
        }

        private void storeWeaponFamily(string checkBox)
        {
            storeWeaponFamily(tree_Weapon_ID(), checkBox);
        }

        private void storeWeaponFamily(int index)
        {
            storeWeaponFamily(index, "");
        }

        private void storeWeaponFamily()
        {
            storeWeaponFamily(tree_Weapon_ID(), "");
        }

        #endregion

        #region Store Individual

        private void storeWeaponID(int index, string checkBox)
        {
            if (tree_Weapon_IsIndividual() && index > 0 && index <= cfg.WeaponID.Count && cfg.WeaponID.Count > 0)
            {
                cfg.WeaponID[index - 1].CloneFrom(getWeaponData(index, checkBox, cfg.WeaponID[index - 1]));
            }
        }

        private void storeWeaponID(string checkBox)
        {
            storeWeaponID(tree_Weapon_ID(), checkBox);
        }

        private void storeWeaponID(int index)
        {
            storeWeaponID(index, "");
        }

        private void storeWeaponID()
        {
            storeWeaponID(tree_Weapon_ID(), "");
        }

        #endregion

        #region Store Default

        private void storeWeaponDefault(string checkBox)
        {
            cfg.WeaponDefault.CloneFrom(getWeaponData(0, checkBox, cfg.WeaponDefault));
        }

        private void storeWeaponDefault()
        {
            storeWeaponDefault("");
        }

        #endregion

        #endregion

        #region Armor

        #region Get Data

        public DataPackEquipment getArmorData(int index, string checkBox, DataPackEquipment oldArmorData)
        {
            DataPackEquipment armor = new DataPackEquipment();

            // Set the updating flag
            updating = true;

            // Set the id of the data
            armor.ID = index;

            // Set the name of the data
            armor.Name = txt_Armor_Name.Text;

            #region In family

            armor.ArmorFamily.Clear();
            if (armorInFamily.Count > 0)
            {
                foreach (Armor armors in armorInFamily)
                {
                    armor.ArmorFamily.Add(new Armor(armors.ID, armors.Name));
                }
            }

            cfg.ArmorAvailable.Clear();
            if (armorAvailable.Count > 0)
            {
                foreach (Armor armors in armorAvailable)
                {
                    cfg.ArmorAvailable.Add(new Armor(armors.ID, armors.Name));
                }
            }

            #endregion

            #region Armor

            // Store the armor type
            if (combo_Armor_Type.SelectedIndex > 0)
            {
                armor.Type = combo_Armor_Type.SelectedIndex + 4;
            }

            // Store the cursed flag
            if (check_Armor_Cursed.IsChecked == true)
            {
                armor.Cursed = true;
            }
            else
            {
                armor.Cursed = false;
            }

            // Store the equiping switch id
            if (check_Armor_SwitchID.IsChecked == true)
            {
                armor.CustomSwitchID = true;
                if (checkBox != "check_Armor_SwitchID")
                {
                    armor.SwitchID = combo_Armor_SwitchID.SelectedIndex;
                }
            }
            else
            {
                armor.ResetSwitchID();
            }

            #endregion

            #region Defense

            // Store physical defense
            if (check_Armor_PDef.IsChecked == true)
            {
                armor.CustomPDef = true;
                if (checkBox != "check_Armor_PDef")
                {
                    armor.PDefInitial = intud_Armor_PDefInitial.Value ?? oldArmorData.PDefInitial;
                }
                else
                {
                    armor.PDefInitial = cfg.Default.PDefInitial;
                }
            }

            // Store magical defense
            if (check_Armor_MDef.IsChecked == true)
            {
                armor.CustomMDef = true;
                if (checkBox != "check_Armor_MDef")
                {
                    armor.MDefInitial = intud_Armor_MDefInitial.Value ?? oldArmorData.MDefInitial;
                }
                else
                {
                    armor.MDefInitial = cfg.Default.MDefInitial;
                }
            }

            #endregion

            #region Parameter

            // Store maximum HP
            if (check_Armor_MaxHP.IsChecked == true)
            {
                armor.CustomMaxHP = true;
                if (checkBox != "check_Armor_MaxHP")
                {
                    armor.MaxHPMul = decud_Armor_MaxHPMul.Value ?? oldArmorData.MaxHPMul;
                    armor.MaxHPAdd = intud_Armor_MaxHPAdd.Value ?? oldArmorData.MaxHPAdd;
                }
            }

            // Store maximum SP
            if (check_Armor_MaxSP.IsChecked == true)
            {
                armor.CustomMaxSP = true;
                if (checkBox != "check_Armor_MaxSP")
                {
                    armor.MaxSPMul = decud_Armor_MaxSPMul.Value ?? oldArmorData.MaxSPMul;
                    armor.MaxSPAdd = intud_Armor_MaxSPAdd.Value ?? oldArmorData.MaxSPAdd;
                }
            }

            // Store strengh
            if (check_Armor_Str.IsChecked == true)
            {
                armor.CustomStr = true;
                if (checkBox != "check_Armor_Str")
                {
                    armor.StrMul = decud_Armor_StrMul.Value ?? oldArmorData.StrMul;
                    armor.StrAdd = intud_Armor_StrAdd.Value ?? oldArmorData.StrAdd;
                }
            }

            // Store dexterity
            if (check_Armor_Dex.IsChecked == true)
            {
                armor.CustomDex = true;
                if (checkBox != "check_Armor_Dex")
                {
                    armor.DexMul = decud_Armor_DexMul.Value ?? oldArmorData.DexMul;
                    armor.DexAdd = intud_Armor_DexAdd.Value ?? oldArmorData.DexAdd;
                }
            }

            // Store agility
            if (check_Armor_Agi.IsChecked == true)
            {
                armor.CustomAgi = true;
                if (checkBox != "check_Armor_Agi")
                {
                    armor.AgiMul = decud_Armor_AgiMul.Value ?? oldArmorData.AgiMul;
                    armor.AgiAdd = intud_Armor_AgiAdd.Value ?? oldArmorData.AgiAdd;
                }
            }

            // Store intelligence
            if (check_Armor_Int.IsChecked == true)
            {
                armor.CustomInt = true;
                if (checkBox != "check_Armor_Int")
                {
                    armor.IntMul = decud_Armor_IntMul.Value ?? oldArmorData.IntMul;
                    armor.IntAdd = intud_Armor_IntAdd.Value ?? oldArmorData.IntAdd;
                }
            }

            #endregion

            #region Parameter Rate

            // Store strengh rate
            if (check_Armor_StrRate.IsChecked == true)
            {
                armor.CustomStrRate = true;
                if (checkBox != "check_Armor_StrRate")
                {
                    armor.StrRateMul = decud_Armor_StrRateMul.Value ?? oldArmorData.StrRateMul;
                    armor.StrRateAdd = intud_Armor_StrRateAdd.Value ?? oldArmorData.StrRateAdd;
                }
            }

            // Store dexterity rate
            if (check_Armor_DexRate.IsChecked == true)
            {
                armor.CustomDexRate = true;
                if (checkBox != "check_Armor_DexRate")
                {
                    armor.DexRateMul = decud_Armor_DexRateMul.Value ?? oldArmorData.DexRateMul;
                    armor.DexRateAdd = intud_Armor_DexRateAdd.Value ?? oldArmorData.DexRateAdd;
                }
            }

            // Store agility rate
            if (check_Armor_AgiRate.IsChecked == true)
            {
                armor.CustomAgiRate = true;
                if (checkBox != "check_Armor_AgiRate")
                {
                    armor.AgiRateMul = decud_Armor_AgiRateMul.Value ?? oldArmorData.AgiRateMul;
                    armor.AgiRateAdd = intud_Armor_AgiRateAdd.Value ?? oldArmorData.AgiRateAdd;
                }
            }

            // Store intelligence rate
            if (check_Armor_IntRate.IsChecked == true)
            {
                armor.CustomIntRate = true;
                if (checkBox != "check_Armor_IntRate")
                {
                    armor.IntRateMul = decud_Armor_IntRateMul.Value ?? oldArmorData.IntRateMul;
                    armor.IntRateAdd = intud_Armor_IntRateAdd.Value ?? oldArmorData.IntRateAdd;
                }
            }

            // Store physical defense rate
            if (check_Armor_PDefRate.IsChecked == true)
            {
                armor.CustomPDefRate = true;
                if (checkBox != "check_Armor_PDefRate")
                {
                    armor.PDefRateMul = decud_Armor_PDefRateMul.Value ?? oldArmorData.PDefRateMul;
                    armor.PDefRateAdd = intud_Armor_PDefRateAdd.Value ?? oldArmorData.PDefRateAdd;
                }
            }

            // Store magical defense rate
            if (check_Armor_MDefRate.IsChecked == true)
            {
                armor.CustomMDefRate = true;
                if (checkBox != "check_Armor_MDefRate")
                {
                    armor.MDefRateMul = decud_Armor_MDefRateMul.Value ?? oldArmorData.MDefRateMul;
                    armor.MDefRateAdd = intud_Armor_MDefRateAdd.Value ?? oldArmorData.MDefRateAdd;
                }
            }

            // Store guard rate
            if (check_Armor_GuardRate.IsChecked == true)
            {
                armor.CustomGuardRate = true;
                if (checkBox != "check_Armor_GuardRate")
                {
                    armor.GuardRateMul = decud_Armor_GuardRateMul.Value ?? oldArmorData.GuardRateMul;
                    armor.GuardRateAdd = intud_Armor_GuardRateAdd.Value ?? oldArmorData.GuardRateAdd;
                }
            }

            // Store evasion rate
            if (check_Armor_EvaRate.IsChecked == true)
            {
                armor.CustomEvaRate = true;
                if (checkBox != "check_Armor_EvaRate")
                {
                    armor.EvaRateMul = decud_Armor_EvaRateMul.Value ?? oldArmorData.EvaRateMul;
                    armor.EvaRateAdd = intud_Armor_EvaRateAdd.Value ?? oldArmorData.EvaRateAdd;
                }
            }

            #endregion

            #region Defense against attack critical

            // Store defense against attack critical rate
            if (check_Armor_DefCritRate.IsChecked == true)
            {
                armor.CustomDefCritRate = true;
                if (checkBox != "check_Armor_DefCritRate")
                {
                    armor.DefCritRateMul = decud_Armor_DefCritRateMul.Value ?? oldArmorData.DefCritRateMul;
                    armor.DefCritRateAdd = intud_Armor_DefCritRateAdd.Value ?? oldArmorData.DefCritRateAdd;
                }
            }

            // Store defense against attack critical damage
            if (check_Armor_DefCritDamage.IsChecked == true)
            {
                armor.CustomDefCritDamage = true;
                if (checkBox != "check_Armor_DefCritDamage")
                {
                    armor.DefCritDamageMul = decud_Armor_DefCritDamageMul.Value ?? oldArmorData.DefCritDamageMul;
                    armor.DefCritDamageAdd = intud_Armor_DefCritDamageAdd.Value ?? oldArmorData.DefCritDamageAdd;
                }
            }

            // Store defense against attack special critical rate
            if (check_Armor_DefSpCritRate.IsChecked == true)
            {
                armor.CustomDefSpCritRate = true;
                if (checkBox != "check_Armor_DefSpCritRate")
                {
                    armor.DefSpCritRateMul = decud_Armor_DefSpCritRateMul.Value ?? oldArmorData.DefSpCritRateMul;
                    armor.DefSpCritRateAdd = intud_Armor_DefSpCritRateAdd.Value ?? oldArmorData.DefSpCritRateAdd;
                }
            }

            // Store defense against attack special critical damage
            if (check_Armor_DefSpCritDamage.IsChecked == true)
            {
                armor.CustomDefSpCritDamage = true;
                if (checkBox != "check_Armor_DefSpCritDamage")
                {
                    armor.DefSpCritDamageMul = decud_Armor_DefSpCritDamageMul.Value ?? oldArmorData.DefSpCritDamageMul;
                    armor.DefSpCritDamageAdd = intud_Armor_DefSpCritDamageAdd.Value ?? oldArmorData.DefSpCritDamageAdd;
                }
            }

            #endregion

            #region Defense against Skill critical

            // Store defense against skill critical rate
            if (check_Armor_DefSkillCritRate.IsChecked == true)
            {
                armor.CustomDefSkillCritRate = true;
                if (checkBox != "check_Armor_DefSkillCritRate")
                {
                    armor.DefSkillCritRateMul = decud_Armor_DefSkillCritRateMul.Value ?? oldArmorData.DefSkillCritRateMul;
                    armor.DefSkillCritRateAdd = intud_Armor_DefSkillCritRateAdd.Value ?? oldArmorData.DefSkillCritRateAdd;
                }
            }

            // Store defense against skill critical damage
            if (check_Armor_DefSkillCritDamage.IsChecked == true)
            {
                armor.CustomDefSkillCritDamage = true;
                if (checkBox != "check_Armor_DefSkillCritDamage")
                {
                    armor.DefSkillCritDamageMul = decud_Armor_DefSkillCritDamageMul.Value ?? oldArmorData.DefSkillCritDamageMul;
                    armor.DefSkillCritDamageAdd = intud_Armor_DefSkillCritDamageAdd.Value ?? oldArmorData.DefSkillCritDamageAdd;
                }
            }

            // Store defense against skill special critical rate
            if (check_Armor_DefSkillSpCritRate.IsChecked == true)
            {
                armor.CustomDefSkillSpCritRate = true;
                if (checkBox != "check_Armor_DefSkillSpCritRate")
                {
                    armor.DefSkillSpCritRateMul = decud_Armor_DefSkillSpCritRateMul.Value ?? oldArmorData.DefSkillSpCritRateMul;
                    armor.DefSkillSpCritRateAdd = intud_Armor_DefSkillSpCritRateAdd.Value ?? oldArmorData.DefSkillSpCritRateAdd;
                }
            }

            // Store defense against skill special critical damage
            if (check_Armor_DefSkillSpCritDamage.IsChecked == true)
            {
                armor.CustomDefSkillSpCritDamage = true;
                if (checkBox != "check_Armor_DefSkillSpCritDamage")
                {
                    armor.DefSkillSpCritDamageMul = decud_Armor_DefSkillSpCritDamageMul.Value ?? oldArmorData.DefSkillSpCritDamageMul;
                    armor.DefSkillSpCritDamageAdd = intud_Armor_DefSkillSpCritDamageAdd.Value ?? oldArmorData.DefSkillSpCritDamageAdd;
                }
            }

            #endregion

            #region Attack

            // Store attack
            if (check_Armor_Atk.IsChecked == true)
            {
                armor.CustomAtk = true;
                if (checkBox != "check_Armor_Atk")
                {
                    armor.AtkMul = decud_Armor_AtkMul.Value ?? oldArmorData.AtkMul;
                    armor.AtkAdd = intud_Armor_AtkAdd.Value ?? oldArmorData.AtkAdd;
                }
            }

            // Store hit rate
            if (check_Armor_Hit.IsChecked == true)
            {
                armor.CustomHit = true;
                if (checkBox != "check_Armor_Hit")
                {
                    armor.HitMul = decud_Armor_HitMul.Value ?? oldArmorData.HitMul;
                    armor.HitAdd = intud_Armor_HitAdd.Value ?? oldArmorData.HitAdd;
                }
            }

            #endregion

            #region Critical

            // Store critical rate
            if (check_Armor_CritRate.IsChecked == true)
            {
                armor.CustomCritRate = true;
                if (checkBox != "check_Armor_CritRate")
                {
                    armor.CritRateMul = decud_Armor_CritRateMul.Value ?? oldArmorData.CritRateMul;
                    armor.CritRateAdd = intud_Armor_CritRateAdd.Value ?? oldArmorData.CritRateAdd;
                }
            }

            // Store critical damage
            if (check_Armor_CritDamage.IsChecked == true)
            {
                armor.CustomCritDamage = true;
                if (checkBox != "check_Armor_CritDamage")
                {
                    armor.CritDamageMul = decud_Armor_CritDamageMul.Value ?? oldArmorData.CritDamageMul;
                    armor.CritDamageAdd = intud_Armor_CritDamageAdd.Value ?? oldArmorData.CritDamageAdd;
                }
            }

            // Store critical guard rate reduction
            if (check_Armor_CritDefGuard.IsChecked == true)
            {
                armor.CustomCritDefGuard = true;
                if (checkBox != "check_Armor_CritDefGuard")
                {
                    armor.CritDefGuardMul = decud_Armor_CritDefGuardMul.Value ?? oldArmorData.CritDefGuardMul;
                    armor.CritDefGuardAdd = intud_Armor_CritDefGuardAdd.Value ?? oldArmorData.CritDefGuardAdd;
                }
            }

            // Store critical evasion rate reduction
            if (check_Armor_CritDefEva.IsChecked == true)
            {
                armor.CustomCritDefEva = true;
                if (checkBox != "check_Armor_CritDefEva")
                {
                    armor.CritDefEvaMul = decud_Armor_CritDefEvaMul.Value ?? oldArmorData.CritDefEvaMul;
                    armor.CritDefEvaAdd = intud_Armor_CritDefEvaAdd.Value ?? oldArmorData.CritDefEvaAdd;
                }
            }

            #endregion

            #region Special Critical

            // Store special critical rate
            if (check_Armor_SpCritRate.IsChecked == true)
            {
                armor.CustomSpCritRate = true;
                if (checkBox != "check_Armor_SpCritRate")
                {
                    armor.SpCritRateMul = decud_Armor_SpCritRateMul.Value ?? oldArmorData.SpCritRateMul;
                    armor.SpCritRateAdd = intud_Armor_SpCritRateAdd.Value ?? oldArmorData.SpCritRateAdd;
                }
            }

            // Store special critical damage
            if (check_Armor_SpCritDamage.IsChecked == true)
            {
                armor.CustomSpCritDamage = true;
                if (checkBox != "check_Armor_SpCritDamage")
                {
                    armor.SpCritDamageMul = decud_Armor_SpCritDamageMul.Value ?? oldArmorData.SpCritDamageMul;
                    armor.SpCritDamageAdd = intud_Armor_SpCritDamageAdd.Value ?? oldArmorData.SpCritDamageAdd;
                }
            }

            // Store special critical guard rate reduction
            if (check_Armor_SpCritDefGuard.IsChecked == true)
            {
                armor.CustomSpCritDefGuard = true;
                if (checkBox != "check_Armor_SpCritDefGuard")
                {
                    armor.SpCritDefGuardMul = decud_Armor_SpCritDefGuardMul.Value ?? oldArmorData.SpCritDefGuardMul;
                    armor.SpCritDefGuardAdd = intud_Armor_SpCritDefGuardAdd.Value ?? oldArmorData.SpCritDefGuardAdd;
                }
            }

            // Store special critical evasion rate reduction
            if (check_Armor_SpCritDefEva.IsChecked == true)
            {
                armor.CustomSpCritDefEva = true;
                if (checkBox != "check_Armor_SpCritDefEva")
                {
                    armor.SpCritDefEvaMul = decud_Armor_SpCritDefEvaMul.Value ?? oldArmorData.SpCritDefEvaMul;
                    armor.SpCritDefEvaAdd = intud_Armor_SpCritDefEvaAdd.Value ?? oldArmorData.SpCritDefEvaAdd;
                }
            }

            #endregion

            // Remove the updating flag
            updating = false;

            return armor;
        }

        #endregion

        #region Store Family

        private void storeArmorFamily(int index, string checkBox)
        {
            if (tree_Armor_IsFamily() && index > 0 && index <= cfg.ArmorFamily.Count && cfg.ArmorFamily.Count > 0)
            {
                cfg.ArmorFamily[index - 1].CloneFrom(getArmorData(index, checkBox, cfg.ArmorFamily[index - 1]));
            }
        }

        private void storeArmorFamily(string checkBox)
        {
            storeArmorFamily(tree_Armor_ID(), checkBox);
        }

        private void storeArmorFamily(int index)
        {
            storeArmorFamily(index, "");
        }

        private void storeArmorFamily()
        {
            storeArmorFamily(tree_Armor_ID(), "");
        }

        #endregion

        #region Store Individual

        private void storeArmorID(int index, string checkBox)
        {
            if (tree_Armor_IsIndividual() && index > 0 && index <= cfg.ArmorID.Count && cfg.ArmorID.Count > 0)
            {
                cfg.ArmorID[index - 1].CloneFrom(getArmorData(index, checkBox, cfg.ArmorID[index - 1]));
            }
        }

        private void storeArmorID(string checkBox)
        {
            storeArmorID(tree_Armor_ID(), checkBox);
        }

        private void storeArmorID(int index)
        {
            storeArmorID(index, "");
        }

        private void storeArmorID()
        {
            storeArmorID(tree_Armor_ID(), "");
        }

        #endregion

        #region Store Default

        private void storeArmorDefault(string checkBox)
        {
            cfg.ArmorDefault.CloneFrom(getArmorData(0, checkBox, cfg.ArmorDefault));
        }

        private void storeArmorDefault()
        {
            storeArmorDefault("");
        }

        #endregion

        #endregion

        #region Enemy

        #region Get Data

        public DataPackEnemy getEnemyData(int index, string checkBox, DataPackEnemy oldEnemyData)
        {
            DataPackEnemy enemy = new DataPackEnemy();

            // Set the updating flag
            updating = true;

            // Set the id of the data
            enemy.ID = index;

            // Set the name of the data
            enemy.Name = txt_Enemy_Name.Text;

            #region In family

            enemy.EnemyFamily.Clear();
            if (enemyInFamily.Count > 0)
            {
                foreach (Enemy enemies in enemyInFamily)
                {
                    enemy.EnemyFamily.Add(new Enemy(enemies.ID, enemies.Name));
                }
            }

            cfg.EnemyAvailable.Clear();
            if (enemyAvailable.Count > 0)
            {
                foreach (Enemy enemies in enemyAvailable)
                {
                    cfg.EnemyAvailable.Add(new Enemy(enemies.ID, enemies.Name));
                }
            }

            #endregion

            #region Parameter

            // Store maximum HP
            if (check_Enemy_MaxHP.IsChecked == true)
            {
                enemy.CustomMaxHP = true;
                if (checkBox != "check_Enemy_MaxHP")
                {
                    enemy.MaxHPInitial = intud_Enemy_MaxHPInitial.Value ?? oldEnemyData.MaxHPInitial;
                }
                else
                {
                    enemy.MaxHPInitial = cfg.Default.MaxHPInitial;
                }
            }

            // Store maximum SP
            if (check_Enemy_MaxSP.IsChecked == true)
            {
                enemy.CustomMaxSP = true;
                if (checkBox != "check_Enemy_MaxSP")
                {
                    enemy.MaxSPInitial = intud_Enemy_MaxSPInitial.Value ?? oldEnemyData.MaxSPInitial;
                }
                else
                {
                    enemy.MaxSPInitial = cfg.Default.MaxSPInitial;
                }
            }

            // Store strengh
            if (check_Enemy_Str.IsChecked == true)
            {
                enemy.CustomStr = true;
                if (checkBox != "check_Enemy_Str")
                {
                    enemy.StrInitial = intud_Enemy_StrInitial.Value ?? oldEnemyData.StrInitial;
                }
                else
                {
                    enemy.StrInitial = cfg.Default.StrInitial;
                }
            }

            // Store dexterity
            if (check_Enemy_Dex.IsChecked == true)
            {
                enemy.CustomDex = true;
                if (checkBox != "check_Enemy_Dex")
                {
                    enemy.DexInitial = intud_Enemy_DexInitial.Value ?? oldEnemyData.DexInitial;
                }
                else
                {
                    enemy.DexInitial = cfg.Default.DexInitial;
                }
            }

            // Store agility
            if (check_Enemy_Agi.IsChecked == true)
            {
                enemy.CustomAgi = true;
                if (checkBox != "check_Enemy_Agi")
                {
                    enemy.AgiInitial = intud_Enemy_AgiInitial.Value ?? oldEnemyData.AgiInitial;
                }
                else
                {
                    enemy.AgiInitial = cfg.Default.AgiInitial;
                }
            }

            // Store intelligence
            if (check_Enemy_Int.IsChecked == true)
            {
                enemy.CustomInt = true;
                if (checkBox != "check_Enemy_Int")
                {
                    enemy.IntInitial = intud_Enemy_IntInitial.Value ?? oldEnemyData.IntInitial;
                }
                else
                {
                    enemy.IntInitial = cfg.Default.IntInitial;
                }
            }

            #endregion

            #region Parameter Rate

            // Store strengh rate
            if (check_Enemy_StrRate.IsChecked == true)
            {
                enemy.CustomStrRate = true;
                if (checkBox != "check_Enemy_StrRate")
                {
                    enemy.StrRate = decud_Enemy_StrRate.Value ?? oldEnemyData.StrRate;
                }
                else
                {
                    enemy.StrRate = cfg.Default.StrRate;
                }
            }

            // Store dexterity rate
            if (check_Enemy_DexRate.IsChecked == true)
            {
                enemy.CustomDexRate = true;
                if (checkBox != "check_Enemy_DexRate")
                {
                    enemy.DexRate = decud_Enemy_DexRate.Value ?? oldEnemyData.DexRate;
                }
                else
                {
                    enemy.DexRate = cfg.Default.DexRate;
                }
            }

            // Store agility rate
            if (check_Enemy_AgiRate.IsChecked == true)
            {
                enemy.CustomAgiRate = true;
                if (checkBox != "check_Enemy_AgiRate")
                {
                    enemy.AgiRate = decud_Enemy_AgiRate.Value ?? oldEnemyData.AgiRate;
                }
                else
                {
                    enemy.AgiRate = cfg.Default.AgiRate;
                }
            }

            // Store intelligence rate
            if (check_Enemy_IntRate.IsChecked == true)
            {
                enemy.CustomIntRate = true;
                if (checkBox != "check_Enemy_IntRate")
                {
                    enemy.IntRate = decud_Enemy_IntRate.Value ?? oldEnemyData.IntRate;
                }
                else
                {
                    enemy.IntRate = cfg.Default.IntRate;
                }
            }

            // Store physical defense rate
            if (check_Enemy_PDefRate.IsChecked == true)
            {
                enemy.CustomPDefRate = true;
                if (checkBox != "check_Enemy_PDefRate")
                {
                    enemy.PDefRate = decud_Enemy_PDefRate.Value ?? oldEnemyData.PDefRate;
                }
                else
                {
                    enemy.PDefRate = cfg.Default.PDefRate;
                }
            }

            // Store magical defense rate
            if (check_Enemy_MDefRate.IsChecked == true)
            {
                enemy.CustomMDefRate = true;
                if (checkBox != "check_Enemy_MDefRate")
                {
                    enemy.MDefRate = decud_Enemy_MDefRate.Value ?? oldEnemyData.MDefRate;
                }
                else
                {
                    enemy.MDefRate = cfg.Default.MDefRate;
                }
            }

            // Store guard rate
            if (check_Enemy_GuardRate.IsChecked == true)
            {
                enemy.CustomGuardRate = true;
                if (checkBox != "check_Enemy_GuardRate")
                {
                    enemy.GuardRate = decud_Enemy_GuardRate.Value ?? oldEnemyData.GuardRate;
                }
                else
                {
                    enemy.GuardRate = cfg.Default.GuardRate;
                }
            }

            // Store evasion rate
            if (check_Enemy_EvaRate.IsChecked == true)
            {
                enemy.CustomEvaRate = true;
                if (checkBox != "check_Enemy_EvaRate")
                {
                    enemy.EvaRate = decud_Enemy_EvaRate.Value ?? oldEnemyData.EvaRate;
                }
                else
                {
                    enemy.EvaRate = cfg.Default.EvaRate;
                }
            }

            #endregion

            #region Defense against attack critical

            // Store defense against attack critical rate
            if (check_Enemy_DefCritRate.IsChecked == true)
            {
                enemy.CustomDefCritRate = true;
                if (checkBox != "check_Enemy_DefCritRate")
                {
                    enemy.DefCritRate = decud_Enemy_DefCritRate.Value ?? oldEnemyData.DefCritRate;
                }
                else
                {
                    enemy.DefCritRate = cfg.Default.DefCritRate;
                }
            }

            // Store defense against attack critical damage
            if (check_Enemy_DefCritDamage.IsChecked == true)
            {
                enemy.CustomDefCritDamage = true;
                if (checkBox != "check_Enemy_DefCritDamage")
                {
                    enemy.DefCritDamage = decud_Enemy_DefCritDamage.Value ?? oldEnemyData.DefCritDamage;
                }
                else
                {
                    enemy.DefCritDamage = cfg.Default.DefCritDamage;
                }
            }

            // Store defense against attack special critical rate
            if (check_Enemy_DefSpCritRate.IsChecked == true)
            {
                enemy.CustomDefSpCritRate = true;
                if (checkBox != "check_Enemy_DefSpCritRate")
                {
                    enemy.DefSpCritRate = decud_Enemy_DefSpCritRate.Value ?? oldEnemyData.DefSpCritRate;
                }
                else
                {
                    enemy.DefSpCritRate = cfg.Default.DefSpCritRate;
                }
            }

            // Store defense against attack special critical damage
            if (check_Enemy_DefSpCritDamage.IsChecked == true)
            {
                enemy.CustomDefSpCritDamage = true;
                if (checkBox != "check_Enemy_DefSpCritDamage")
                {
                    enemy.DefSpCritDamage = decud_Enemy_DefSpCritDamage.Value ?? oldEnemyData.DefSpCritDamage;
                }
                else
                {
                    enemy.DefSpCritDamage = cfg.Default.DefSpCritDamage;
                }
            }

            #endregion

            #region Defense against Skill critical

            // Store defense against attack critical rate
            if (check_Enemy_DefSkillCritRate.IsChecked == true)
            {
                enemy.CustomDefSkillCritRate = true;
                if (checkBox != "check_Enemy_DefSkillCritRate")
                {
                    enemy.DefSkillCritRate = decud_Enemy_DefSkillCritRate.Value ?? oldEnemyData.DefSkillCritRate;
                }
                else
                {
                    enemy.DefSkillCritRate = cfg.Default.DefSkillCritRate;
                }
            }

            // Store defense against attack critical damage
            if (check_Enemy_DefSkillCritDamage.IsChecked == true)
            {
                enemy.CustomDefSkillCritDamage = true;
                if (checkBox != "check_Enemy_DefSkillCritDamage")
                {
                    enemy.DefSkillCritDamage = decud_Enemy_DefSkillCritDamage.Value ?? oldEnemyData.DefSkillCritDamage;
                }
                else
                {
                    enemy.DefSkillCritDamage = cfg.Default.DefSkillCritDamage;
                }
            }

            // Store defense against attack special critical rate
            if (check_Enemy_DefSkillSpCritRate.IsChecked == true)
            {
                enemy.CustomDefSkillSpCritRate = true;
                if (checkBox != "check_Enemy_DefSkillSpCritRate")
                {
                    enemy.DefSkillSpCritRate = decud_Enemy_DefSkillSpCritRate.Value ?? oldEnemyData.DefSkillSpCritRate;
                }
                else
                {
                    enemy.DefSkillSpCritRate = cfg.Default.DefSkillSpCritRate;
                }
            }

            // Store defense against attack special critical damage
            if (check_Enemy_DefSkillSpCritDamage.IsChecked == true)
            {
                enemy.CustomDefSkillSpCritDamage = true;
                if (checkBox != "check_Enemy_DefSkillSpCritDamage")
                {
                    enemy.DefSkillSpCritDamage = decud_Enemy_DefSkillSpCritDamage.Value ?? oldEnemyData.DefSkillSpCritDamage;
                }
                else
                {
                    enemy.DefSkillSpCritDamage = cfg.Default.DefSkillSpCritDamage;
                }
            }

            #endregion

            #region Attack

            // Store attack
            if (check_Enemy_Atk.IsChecked == true)
            {
                enemy.CustomAtk = true;
                if (checkBox != "check_Enemy_Atk")
                {
                    enemy.AtkInitial = intud_Enemy_AtkInitial.Value ?? oldEnemyData.AtkInitial;
                }
                else
                {
                    enemy.AtkInitial = cfg.Default.AtkInitial;
                }
            }

            // Store hit rate
            if (check_Enemy_Hit.IsChecked == true)
            {
                enemy.CustomHit = true;
                if (checkBox != "check_Enemy_Hit")
                {
                    enemy.HitInitial = decud_Enemy_HitInitial.Value ?? oldEnemyData.HitInitial;
                }
                else
                {
                    enemy.HitInitial = cfg.Default.HitInitial;
                }
            }

            // Store parameter attack force
            if (check_Enemy_ParamForce.IsChecked == true)
            {
                enemy.CustomParamForce = true;
                if (checkBox != "check_Enemy_ParamForce")
                {
                    enemy.StrForce = decud_Enemy_StrForce.Value ?? oldEnemyData.StrForce;
                    enemy.DexForce = decud_Enemy_DexForce.Value ?? oldEnemyData.DexForce;
                    enemy.AgiForce = decud_Enemy_AgiForce.Value ?? oldEnemyData.AgiForce;
                    enemy.IntForce = decud_Enemy_IntForce.Value ?? oldEnemyData.IntForce;
                }
                else
                {
                    enemy.StrForce = cfg.Default.StrForce;
                    enemy.DexForce = cfg.Default.DexForce;
                    enemy.AgiForce = cfg.Default.AgiForce;
                    enemy.IntForce = cfg.Default.IntForce;
                }
            }

            // Store defense attack force
            if (check_Enemy_DefenseForce.IsChecked == true)
            {
                enemy.CustomDefenseForce = true;
                if (checkBox != "check_Enemy_DefenseForce")
                {
                    enemy.PDefForce = decud_Enemy_PDefForce.Value ?? oldEnemyData.PDefForce;
                    enemy.MDefForce = decud_Enemy_MDefForce.Value ?? oldEnemyData.MDefForce;
                }
                else
                {
                    enemy.PDefForce = cfg.Default.PDefForce;
                    enemy.MDefForce = cfg.Default.MDefForce;
                }
            }

            #endregion

            #region Critical

            // Store critical rate
            if (check_Enemy_CritRate.IsChecked == true)
            {
                enemy.CustomCritRate = true;
                if (checkBox != "check_Enemy_CritRate")
                {
                    enemy.CritRate = decud_Enemy_CritRate.Value ?? oldEnemyData.CritRate;
                }
                else
                {
                    enemy.CritRate = cfg.Default.CritRate;
                }
            }

            // Store critical damage
            if (check_Enemy_CritDamage.IsChecked == true)
            {
                enemy.CustomCritDamage = true;
                if (checkBox != "check_Enemy_CritDamage")
                {
                    enemy.CritDamage = decud_Enemy_CritDamage.Value ?? oldEnemyData.CritDamage;
                }
                else
                {
                    enemy.CritDamage = cfg.Default.CritDamage;
                }
            }

            // Store critical guard reduction
            if (check_Enemy_CritDefGuard.IsChecked == true)
            {
                enemy.CustomCritDefGuard = true;
                if (checkBox != "check_Enemy_CritDefGuard")
                {
                    enemy.CritDefGuard = decud_Enemy_CritDefGuard.Value ?? oldEnemyData.CritDefGuard;
                }
                else
                {
                    enemy.CritDefGuard = cfg.Default.CritDefGuard;
                }
            }

            // Store critical evasion reduction
            if (check_Enemy_CritDefEva.IsChecked == true)
            {
                enemy.CustomCritDefEva = true;
                if (checkBox != "check_Enemy_CritDefEva")
                {
                    enemy.CritDefEva = decud_Enemy_CritDefEva.Value ?? oldEnemyData.CritDefEva;
                }
                else
                {
                    enemy.CritDefEva = cfg.Default.CritDefEva;
                }
            }

            #endregion

            #region Special Critical

            // Store special critical rate
            if (check_Enemy_SpCritRate.IsChecked == true)
            {
                enemy.CustomSpCritRate = true;
                if (checkBox != "check_Enemy_SpCritRate")
                {
                    enemy.SpCritRate = decud_Enemy_SpCritRate.Value ?? oldEnemyData.SpCritRate;
                }
                else
                {
                    enemy.SpCritRate = cfg.Default.SpCritRate;
                }
            }

            // Store special critical damage
            if (check_Enemy_SpCritDamage.IsChecked == true)
            {
                enemy.CustomSpCritDamage = true;
                if (checkBox != "check_Enemy_SpCritDamage")
                {
                    enemy.SpCritDamage = decud_Enemy_SpCritDamage.Value ?? oldEnemyData.SpCritDamage;
                }
                else
                {
                    enemy.SpCritDamage = cfg.Default.SpCritDamage;
                }
            }

            // Store critical guard reduction
            if (check_Enemy_SpCritDefGuard.IsChecked == true)
            {
                enemy.CustomSpCritDefGuard = true;
                if (checkBox != "check_Enemy_SpCritDefGuard")
                {
                    enemy.SpCritDefGuard = decud_Enemy_SpCritDefGuard.Value ?? oldEnemyData.SpCritDefGuard;
                }
                else
                {
                    enemy.SpCritDefGuard = cfg.Default.SpCritDefGuard;
                }
            }

            // Store critical evasion reduction
            if (check_Enemy_SpCritDefEva.IsChecked == true)
            {
                enemy.CustomSpCritDefEva = true;
                if (checkBox != "check_Enemy_SpCritDefEva")
                {
                    enemy.SpCritDefEva = decud_Enemy_SpCritDefEva.Value ?? oldEnemyData.SpCritDefEva;
                }
                else
                {
                    enemy.SpCritDefEva = cfg.Default.SpCritDefEva;
                }
            }

            #endregion

            #region Defense

            // Store physical defense
            if (check_Enemy_PDef.IsChecked == true)
            {
                enemy.CustomPDef = true;
                if (checkBox != "check_Enemy_PDef")
                {
                    enemy.PDefInitial = intud_Enemy_PDefInitial.Value ?? oldEnemyData.PDefInitial;
                }
                else
                {
                    enemy.PDefInitial = cfg.Default.PDefInitial;
                }
            }

            // Store magical defense
            if (check_Enemy_MDef.IsChecked == true)
            {
                enemy.CustomMDef = true;
                if (checkBox != "check_Enemy_MDef")
                {
                    enemy.MDefInitial = intud_Enemy_MDefInitial.Value ?? oldEnemyData.MDefInitial;
                }
                else
                {
                    enemy.MDefInitial = cfg.Default.MDefInitial;
                }
            }

            #endregion

            // Remove the updating flag
            updating = false;

            return enemy;
        }

        #endregion

        #region Store Family

        private void storeEnemyFamily(int index, string checkBox)
        {
            if (tree_Enemy_IsFamily() && index > 0 && index <= cfg.EnemyFamily.Count && cfg.EnemyFamily.Count > 0)
            {
                cfg.EnemyFamily[index - 1].CloneFrom(getEnemyData(index, checkBox, cfg.EnemyFamily[index - 1]));
            }
        }

        private void storeEnemyFamily(string checkBox)
        {
            storeEnemyFamily(tree_Enemy_ID(), checkBox);
        }

        private void storeEnemyFamily(int index)
        {
            storeEnemyFamily(index, "");
        }

        private void storeEnemyFamily()
        {
            storeEnemyFamily(tree_Enemy_ID(), "");
        }

        #endregion

        #region Store Individual

        private void storeEnemyID(int index, string checkBox)
        {
            if (tree_Enemy_IsIndividual() && index > 0 && index <= cfg.EnemyID.Count && cfg.EnemyID.Count > 0)
            {
                cfg.EnemyID[index - 1].CloneFrom(getEnemyData(index, checkBox, cfg.EnemyID[index - 1]));
            }
        }

        private void storeEnemyID(string checkBox)
        {
            storeEnemyID(tree_Enemy_ID(), checkBox);
        }

        private void storeEnemyID(int index)
        {
            storeEnemyID(index, "");
        }

        private void storeEnemyID()
        {
            storeEnemyID(tree_Enemy_ID(), "");
        }

        #endregion

        #region Store Default

        private void storeEnemyDefault(string checkBox)
        {
            cfg.EnemyDefault.CloneFrom(getEnemyData(0, checkBox, cfg.EnemyDefault));
        }

        private void storeEnemyDefault()
        {
            storeEnemyDefault("");
        }

        #endregion

        #endregion

        #region State

        #region Get Data

        public DataPackState getStateData(int index, string checkBox, DataPackState oldStateData)
        {
            DataPackState state = new DataPackState();

            // Set the updating flag
            updating = true;

            // Set the id of the data
            state.ID = index;

            // Set the name of the data
            state.Name = txt_State_Name.Text;

            #region In family

            state.StateFamily.Clear();
            if (stateInFamily.Count > 0)
            {
                foreach (State states in stateInFamily)
                {
                    state.StateFamily.Add(new State(states.ID, states.Name));
                }
            }

            cfg.StateAvailable.Clear();
            if (stateAvailable.Count > 0)
            {
                foreach (State states in stateAvailable)
                {
                    cfg.StateAvailable.Add(new State(states.ID, states.Name));
                }
            }

            #endregion

            #region Parameter

            // Store maximum HP
            if (check_State_MaxHP.IsChecked == true)
            {
                state.CustomMaxHP = true;
                if (checkBox != "check_State_MaxHP")
                {
                    state.MaxHPMul = decud_State_MaxHPMul.Value ?? oldStateData.MaxHPMul;
                    state.MaxHPAdd = intud_State_MaxHPAdd.Value ?? oldStateData.MaxHPAdd;
                }
            }

            // Store maximum SP
            if (check_State_MaxSP.IsChecked == true)
            {
                state.CustomMaxSP = true;
                if (checkBox != "check_State_MaxSP")
                {
                    state.MaxSPMul = decud_State_MaxSPMul.Value ?? oldStateData.MaxSPMul;
                    state.MaxSPAdd = intud_State_MaxSPAdd.Value ?? oldStateData.MaxSPAdd;
                }
            }

            // Store strengh
            if (check_State_Str.IsChecked == true)
            {
                state.CustomStr = true;
                if (checkBox != "check_State_Str")
                {
                    state.StrMul = decud_State_StrMul.Value ?? oldStateData.StrMul;
                    state.StrAdd = intud_State_StrAdd.Value ?? oldStateData.StrAdd;
                }
            }

            // Store dexterity
            if (check_State_Dex.IsChecked == true)
            {
                state.CustomDex = true;
                if (checkBox != "check_State_Dex")
                {
                    state.DexMul = decud_State_DexMul.Value ?? oldStateData.DexMul;
                    state.DexAdd = intud_State_DexAdd.Value ?? oldStateData.DexAdd;
                }
            }

            // Store agility
            if (check_State_Agi.IsChecked == true)
            {
                state.CustomAgi = true;
                if (checkBox != "check_State_Agi")
                {
                    state.AgiMul = decud_State_AgiMul.Value ?? oldStateData.AgiMul;
                    state.AgiAdd = intud_State_AgiAdd.Value ?? oldStateData.AgiAdd;
                }
            }

            // Store intelligence
            if (check_State_Int.IsChecked == true)
            {
                state.CustomInt = true;
                if (checkBox != "check_State_Int")
                {
                    state.IntMul = decud_State_IntMul.Value ?? oldStateData.IntMul;
                    state.IntAdd = intud_State_IntAdd.Value ?? oldStateData.IntAdd;
                }
            }

            #endregion

            #region Parameter Rate

            // Store strengh rate
            if (check_State_StrRate.IsChecked == true)
            {
                state.CustomStrRate = true;
                if (checkBox != "check_State_StrRate")
                {
                    state.StrRateMul = decud_State_StrRateMul.Value ?? oldStateData.StrRateMul;
                    state.StrRateAdd = intud_State_StrRateAdd.Value ?? oldStateData.StrRateAdd;
                }
            }

            // Store dexterity rate
            if (check_State_DexRate.IsChecked == true)
            {
                state.CustomDexRate = true;
                if (checkBox != "check_State_DexRate")
                {
                    state.DexRateMul = decud_State_DexRateMul.Value ?? oldStateData.DexRateMul;
                    state.DexRateAdd = intud_State_DexRateAdd.Value ?? oldStateData.DexRateAdd;
                }
            }

            // Store agility rate
            if (check_State_AgiRate.IsChecked == true)
            {
                state.CustomAgiRate = true;
                if (checkBox != "check_State_AgiRate")
                {
                    state.AgiRateMul = decud_State_AgiRateMul.Value ?? oldStateData.AgiRateMul;
                    state.AgiRateAdd = intud_State_AgiRateAdd.Value ?? oldStateData.AgiRateAdd;
                }
            }

            // Store intelligence rate
            if (check_State_IntRate.IsChecked == true)
            {
                state.CustomIntRate = true;
                if (checkBox != "check_State_IntRate")
                {
                    state.IntRateMul = decud_State_IntRateMul.Value ?? oldStateData.IntRateMul;
                    state.IntRateAdd = intud_State_IntRateAdd.Value ?? oldStateData.IntRateAdd;
                }
            }

            // Store physical defense rate
            if (check_State_PDefRate.IsChecked == true)
            {
                state.CustomPDefRate = true;
                if (checkBox != "check_State_PDefRate")
                {
                    state.PDefRateMul = decud_State_PDefRateMul.Value ?? oldStateData.PDefRateMul;
                    state.PDefRateAdd = intud_State_PDefRateAdd.Value ?? oldStateData.PDefRateAdd;
                }
            }

            // Store magical defense rate
            if (check_State_MDefRate.IsChecked == true)
            {
                state.CustomMDefRate = true;
                if (checkBox != "check_State_MDefRate")
                {
                    state.MDefRateMul = decud_State_MDefRateMul.Value ?? oldStateData.MDefRateMul;
                    state.MDefRateAdd = intud_State_MDefRateAdd.Value ?? oldStateData.MDefRateAdd;
                }
            }

            // Store guard rate
            if (check_State_GuardRate.IsChecked == true)
            {
                state.CustomGuardRate = true;
                if (checkBox != "check_State_GuardRate")
                {
                    state.GuardRateMul = decud_State_GuardRateMul.Value ?? oldStateData.GuardRateMul;
                    state.GuardRateAdd = intud_State_GuardRateAdd.Value ?? oldStateData.GuardRateAdd;
                }
            }

            // Store evasion rate
            if (check_State_EvaRate.IsChecked == true)
            {
                state.CustomEvaRate = true;
                if (checkBox != "check_State_EvaRate")
                {
                    state.EvaRateMul = decud_State_EvaRateMul.Value ?? oldStateData.EvaRateMul;
                    state.EvaRateAdd = intud_State_EvaRateAdd.Value ?? oldStateData.EvaRateAdd;
                }
            }

            #endregion

            #region Defense against attack critical

            // Store defense against attack critical rate
            if (check_State_DefCritRate.IsChecked == true)
            {
                state.CustomDefCritRate = true;
                if (checkBox != "check_State_DefCritRate")
                {
                    state.DefCritRateMul = decud_State_DefCritRateMul.Value ?? oldStateData.DefCritRateMul;
                    state.DefCritRateAdd = intud_State_DefCritRateAdd.Value ?? oldStateData.DefCritRateAdd;
                }
            }

            // Store defense against attack critical damage
            if (check_State_DefCritDamage.IsChecked == true)
            {
                state.CustomDefCritDamage = true;
                if (checkBox != "check_State_DefCritDamage")
                {
                    state.DefCritDamageMul = decud_State_DefCritDamageMul.Value ?? oldStateData.DefCritDamageMul;
                    state.DefCritDamageAdd = intud_State_DefCritDamageAdd.Value ?? oldStateData.DefCritDamageAdd;
                }
            }

            // Store defense against attack special critical rate
            if (check_State_DefSpCritRate.IsChecked == true)
            {
                state.CustomDefSpCritRate = true;
                if (checkBox != "check_State_DefSpCritRate")
                {
                    state.DefSpCritRateMul = decud_State_DefSpCritRateMul.Value ?? oldStateData.DefSpCritRateMul;
                    state.DefSpCritRateAdd = intud_State_DefSpCritRateAdd.Value ?? oldStateData.DefSpCritRateAdd;
                }
            }

            // Store defense against attack special critical damage
            if (check_State_DefSpCritDamage.IsChecked == true)
            {
                state.CustomDefSpCritDamage = true;
                if (checkBox != "check_State_DefSpCritDamage")
                {
                    state.DefSpCritDamageMul = decud_State_DefSpCritDamageMul.Value ?? oldStateData.DefSpCritDamageMul;
                    state.DefSpCritDamageAdd = intud_State_DefSpCritDamageAdd.Value ?? oldStateData.DefSpCritDamageAdd;
                }
            }

            #endregion

            #region Defense against Skill critical

            // Store defense against skill critical rate
            if (check_State_DefSkillCritRate.IsChecked == true)
            {
                state.CustomDefSkillCritRate = true;
                if (checkBox != "check_State_DefSkillCritRate")
                {
                    state.DefSkillCritRateMul = decud_State_DefSkillCritRateMul.Value ?? oldStateData.DefSkillCritRateMul;
                    state.DefSkillCritRateAdd = intud_State_DefSkillCritRateAdd.Value ?? oldStateData.DefSkillCritRateAdd;
                }
            }

            // Store defense against skill critical damage
            if (check_State_DefSkillCritDamage.IsChecked == true)
            {
                state.CustomDefSkillCritDamage = true;
                if (checkBox != "check_State_DefSkillCritDamage")
                {
                    state.DefSkillCritDamageMul = decud_State_DefSkillCritDamageMul.Value ?? oldStateData.DefSkillCritDamageMul;
                    state.DefSkillCritDamageAdd = intud_State_DefSkillCritDamageAdd.Value ?? oldStateData.DefSkillCritDamageAdd;
                }
            }

            // Store defense against skill special critical rate
            if (check_State_DefSkillSpCritRate.IsChecked == true)
            {
                state.CustomDefSkillSpCritRate = true;
                if (checkBox != "check_State_DefSkillSpCritRate")
                {
                    state.DefSkillSpCritRateMul = decud_State_DefSkillSpCritRateMul.Value ?? oldStateData.DefSkillSpCritRateMul;
                    state.DefSkillSpCritRateAdd = intud_State_DefSkillSpCritRateAdd.Value ?? oldStateData.DefSkillSpCritRateAdd;
                }
            }

            // Store defense against skill special critical damage
            if (check_State_DefSkillSpCritDamage.IsChecked == true)
            {
                state.CustomDefSkillSpCritDamage = true;
                if (checkBox != "check_State_DefSkillSpCritDamage")
                {
                    state.DefSkillSpCritDamageMul = decud_State_DefSkillSpCritDamageMul.Value ?? oldStateData.DefSkillSpCritDamageMul;
                    state.DefSkillSpCritDamageAdd = intud_State_DefSkillSpCritDamageAdd.Value ?? oldStateData.DefSkillSpCritDamageAdd;
                }
            }

            #endregion

            #region Attack

            // Store attack
            if (check_State_Atk.IsChecked == true)
            {
                state.CustomAtk = true;
                if (checkBox != "check_State_Atk")
                {
                    state.AtkMul = decud_State_AtkMul.Value ?? oldStateData.AtkMul;
                    state.AtkAdd = intud_State_AtkAdd.Value ?? oldStateData.AtkAdd;
                }
            }

            // Store hit rate
            if (check_State_Hit.IsChecked == true)
            {
                state.CustomHit = true;
                if (checkBox != "check_State_Hit")
                {
                    state.HitMul = decud_State_HitMul.Value ?? oldStateData.HitMul;
                    state.HitAdd = intud_State_HitAdd.Value ?? oldStateData.HitAdd;
                }
            }

            #endregion

            #region Critical

            // Store critical rate
            if (check_State_CritRate.IsChecked == true)
            {
                state.CustomCritRate = true;
                if (checkBox != "check_State_CritRate")
                {
                    state.CritRateMul = decud_State_CritRateMul.Value ?? oldStateData.CritRateMul;
                    state.CritRateAdd = intud_State_CritRateAdd.Value ?? oldStateData.CritRateAdd;
                }
            }

            // Store critical damage
            if (check_State_CritDamage.IsChecked == true)
            {
                state.CustomCritDamage = true;
                if (checkBox != "check_State_CritDamage")
                {
                    state.CritDamageMul = decud_State_CritDamageMul.Value ?? oldStateData.CritDamageMul;
                    state.CritDamageAdd = intud_State_CritDamageAdd.Value ?? oldStateData.CritDamageAdd;
                }
            }

            // Store critical guard rate reduction
            if (check_State_CritDefGuard.IsChecked == true)
            {
                state.CustomCritDefGuard = true;
                if (checkBox != "check_State_CritDefGuard")
                {
                    state.CritDefGuardMul = decud_State_CritDefGuardMul.Value ?? oldStateData.CritDefGuardMul;
                    state.CritDefGuardAdd = intud_State_CritDefGuardAdd.Value ?? oldStateData.CritDefGuardAdd;
                }
            }

            // Store critical evasion rate reduction
            if (check_State_CritDefEva.IsChecked == true)
            {
                state.CustomCritDefEva = true;
                if (checkBox != "check_State_CritDefEva")
                {
                    state.CritDefEvaMul = decud_State_CritDefEvaMul.Value ?? oldStateData.CritDefEvaMul;
                    state.CritDefEvaAdd = intud_State_CritDefEvaAdd.Value ?? oldStateData.CritDefEvaAdd;
                }
            }

            #endregion

            #region Special Critical

            // Store special critical rate
            if (check_State_SpCritRate.IsChecked == true)
            {
                state.CustomSpCritRate = true;
                if (checkBox != "check_State_SpCritRate")
                {
                    state.SpCritRateMul = decud_State_SpCritRateMul.Value ?? oldStateData.SpCritRateMul;
                    state.SpCritRateAdd = intud_State_SpCritRateAdd.Value ?? oldStateData.SpCritRateAdd;
                }
            }

            // Store special critical damage
            if (check_State_SpCritDamage.IsChecked == true)
            {
                state.CustomSpCritDamage = true;
                if (checkBox != "check_State_SpCritDamage")
                {
                    state.SpCritDamageMul = decud_State_SpCritDamageMul.Value ?? oldStateData.SpCritDamageMul;
                    state.SpCritDamageAdd = intud_State_SpCritDamageAdd.Value ?? oldStateData.SpCritDamageAdd;
                }
            }

            // Store special critical guard rate reduction
            if (check_State_SpCritDefGuard.IsChecked == true)
            {
                state.CustomSpCritDefGuard = true;
                if (checkBox != "check_State_SpCritDefGuard")
                {
                    state.SpCritDefGuardMul = decud_State_SpCritDefGuardMul.Value ?? oldStateData.SpCritDefGuardMul;
                    state.SpCritDefGuardAdd = intud_State_SpCritDefGuardAdd.Value ?? oldStateData.SpCritDefGuardAdd;
                }
            }

            // Store special critical evasion rate reduction
            if (check_State_SpCritDefEva.IsChecked == true)
            {
                state.CustomSpCritDefEva = true;
                if (checkBox != "check_State_SpCritDefEva")
                {
                    state.SpCritDefEvaMul = decud_State_SpCritDefEvaMul.Value ?? oldStateData.SpCritDefEvaMul;
                    state.SpCritDefEvaAdd = intud_State_SpCritDefEvaAdd.Value ?? oldStateData.SpCritDefEvaAdd;
                }
            }

            #endregion

            #region Defense

            // Store physical defense
            if (check_State_PDef.IsChecked == true)
            {
                state.CustomPDef = true;
                if (checkBox != "check_State_PDef")
                {
                    state.PDefMul = decud_State_PDefMul.Value ?? oldStateData.PDefMul;
                    state.PDefAdd = intud_State_PDefAdd.Value ?? oldStateData.PDefAdd;
                }
            }

            // Store magical defense
            if (check_State_MDef.IsChecked == true)
            {
                state.CustomMDef = true;
                if (checkBox != "check_State_MDef")
                {
                    state.MDefMul = decud_State_MDefMul.Value ?? oldStateData.MDefMul;
                    state.MDefAdd = intud_State_MDefAdd.Value ?? oldStateData.MDefAdd;
                }
            }

            #endregion

            // Remove the updating flag
            updating = false;

            return state;
        }

        #endregion

        #region Store Family

        private void storeStateFamily(int index, string checkBox)
        {
            if (tree_State_IsFamily() && index > 0 && index <= cfg.StateFamily.Count && cfg.StateFamily.Count > 0)
            {
                cfg.StateFamily[index - 1].CloneFrom(getStateData(index, checkBox, cfg.StateFamily[index - 1]));
            }
        }

        private void storeStateFamily(string checkBox)
        {
            storeStateFamily(tree_State_ID(), checkBox);
        }

        private void storeStateFamily(int index)
        {
            storeStateFamily(index, "");
        }

        private void storeStateFamily()
        {
            storeStateFamily(tree_State_ID(), "");
        }

        #endregion

        #region Store Individual

        private void storeStateID(int index, string checkBox)
        {
            if (tree_State_IsIndividual() && index > 0 && index <= cfg.StateID.Count && cfg.StateID.Count > 0)
            {
                cfg.StateID[index - 1].CloneFrom(getStateData(index, checkBox, cfg.StateID[index - 1]));
            }
        }

        private void storeStateID(string checkBox)
        {
            storeStateID(tree_State_ID(), checkBox);
        }

        private void storeStateID(int index)
        {
            storeStateID(index, "");
        }

        private void storeStateID()
        {
            storeStateID(tree_State_ID(), "");
        }

        #endregion

        #region Store Default

        private void storeStateDefault(string checkBox)
        {
            cfg.StateDefault.CloneFrom(getStateData(0, checkBox, cfg.StateDefault));
        }

        private void storeStateDefault()
        {
            storeStateDefault("");
        }

        #endregion

        #endregion

        #endregion

        #region Load Configuration

        private void loadConfiguration(Configuration loadCfg)
        {
            #region General

            loadGeneral(loadCfg);

            #endregion

            #region Default

            loadDefault(loadCfg);

            #endregion

            #region Actor

            loadActorFamily(loadCfg);
            loadActorID(loadCfg);
            loadActorDefault(loadCfg);

            #endregion

            #region Class

            loadClassFamily(loadCfg);
            loadClassID(loadCfg);
            loadClassDefault(loadCfg);

            #endregion

            #region Skill

            loadSkillFamily(loadCfg);
            loadSkillID(loadCfg);
            loadSkillDefault(loadCfg);

            #endregion

            #region Passive Skill

            loadPassiveSkillFamily(loadCfg);
            loadPassiveSkillID(loadCfg);
            loadPassiveSkillDefault(loadCfg);

            #endregion

            #region Weapon

            loadWeaponFamily(loadCfg);
            loadWeaponID(loadCfg);
            loadWeaponDefault(loadCfg);

            #endregion

            #region Armor

            loadArmorFamily(loadCfg);
            loadArmorID(loadCfg);
            loadArmorDefault(loadCfg);

            #endregion

            #region Enemy

            loadEnemyFamily(loadCfg);
            loadEnemyID(loadCfg);
            loadEnemyDefault(loadCfg);

            #endregion

            #region State

            loadStateFamily(loadCfg);
            loadStateID(loadCfg);
            loadStateDefault(loadCfg);

            #endregion
        }

        #region General

        private void loadGeneral(Configuration loadCfg)
        {
            cfg.General.CloneFrom(loadCfg.General);
            applyGeneral();
        }

        #endregion

        #region Default

        private void loadDefault(Configuration loadCfg)
        {
            cfg.Default.CloneFrom(loadCfg.Default);
            applyDefault();
        }

        #endregion

        #region Actor

        #region Family

        private void loadActorFamily(Configuration loadCfg)
        {
            // Set the updating flag
            updating = true;

            // Clear available actor list
            actorAvailable.Clear();
            cfg.ActorAvailable.Clear();

            // Load available actor list
            if (loadCfg.ActorAvailable.Count > 0)
            {
                foreach (Actor actor in loadCfg.ActorAvailable)
                {
                    cfg.ActorAvailable.Add(new Actor(actor.ID, actor.Name));
                }
            }

            // Clear current actor family list
            cfg.ActorFamily.Clear();
            tree_Actor_Family.Items.Clear();
            if (loadCfg.ActorFamily.Count > 0)
            {
                foreach (DataPackActor family in loadCfg.ActorFamily)
                {
                    // Load actor in family
                    cfg.ActorFamily.Add(new DataPackActor(family.ID));
                    cfg.ActorFamily[cfg.ActorFamily.Count - 1].CloneFrom(family);
                    tree_Actor_Family.Items.Add(new TreeViewItem() { Header = family.Name, Tag = family.ID.ToString() });
                }
            }

            // Apply the empty configuration
            tree_Actor_Family.IsSelected = true;
            applyEmptyActor();

            // Remove the updating flag
            updating = false;
        }

        #endregion

        #region Individual

        private void loadActorID(Configuration loadCfg)
        {
            // Set the updating flag
            updating = true;

            // Clear current actor id list
            cfg.ActorID.Clear();
            tree_Actor_Individual.Items.Clear();
            if (loadCfg.ActorID.Count > 0)
            {
                if (gameData.Actors.Count > 0)
                {
                    foreach (DataPackActor actor in loadCfg.ActorID)
                    {
                        if (actor.ID <= gameData.Actors.Count)
                        {
                            // Load actor in id list
                            cfg.ActorID.Add(new DataPackActor(actor.ID));
                            cfg.ActorID[cfg.ActorID.Count - 1].CloneFrom(actor);
                            tree_Actor_Individual.Items.Add(new TreeViewItem() { Header = actor.Name, Tag = actor.ID.ToString() });
                        }
                    }
                    // Add additional actor if their's some missing from the loadCfg
                    if (cfg.ActorID.Count < gameData.Actors.Count)
                    {
                        for (int i = cfg.ActorID.Count; i < gameData.Actors.Count; i++)
                        {
                            cfg.ActorID.Add(new DataPackActor(i, gameData.Actors[i].Name));
                            tree_Actor_Individual.Items.Add(new TreeViewItem() { Header = cfg.ActorID[i].Name, Tag = i.ToString() });
                        }
                    }
                }

                // Apply the empty configuration
                tree_Actor_Family.IsSelected = true;
                applyEmptyActor();

                // Remove the updating flag
                updating = false;
            }
        }

        #endregion

        #region Default

        private void loadActorDefault(Configuration loadCfg)
        {
            // Set the updating flag
            updating = true;

            // Load the actor default
            cfg.ActorDefault.CloneFrom(loadCfg.ActorDefault);

            // Apply the empty configuration
            tree_Actor_Family.IsSelected = true;
            applyEmptyActor();

            // Remove the updating flag
            updating = false;
        }

        #endregion

        #endregion

        #region Class

        #region Family

        private void loadClassFamily(Configuration loadCfg)
        {
            // Set the updating flag
            updating = true;

            // Clear available class list
            classAvailable.Clear();
            cfg.ClassAvailable.Clear();

            // Load available class list
            if (loadCfg.ClassAvailable.Count > 0)
            {
                foreach (Class classes in loadCfg.ClassAvailable)
                {
                    cfg.ClassAvailable.Add(new Class(classes.ID, classes.Name));
                }
            }

            // Clear current class family list
            cfg.ClassFamily.Clear();
            tree_Class_Family.Items.Clear();
            if (loadCfg.ClassFamily.Count > 0)
            {
                foreach (DataPackClass family in loadCfg.ClassFamily)
                {
                    // Load class in family
                    cfg.ClassFamily.Add(new DataPackClass(family.ID));
                    cfg.ClassFamily[cfg.ClassFamily.Count - 1].CloneFrom(family);
                    tree_Class_Family.Items.Add(new TreeViewItem() { Header = family.Name, Tag = family.ID.ToString() });
                }
            }

            // Apply the empty configuration
            tree_Class_Family.IsSelected = true;
            applyEmptyClass();

            // Remove the updating flag
            updating = false;
        }

        #endregion

        #region Individual

        private void loadClassID(Configuration loadCfg)
        {
            // Set the updating flag
            updating = true;

            // Clear current class id list
            cfg.ClassID.Clear();
            tree_Class_Individual.Items.Clear();
            if (loadCfg.ClassID.Count > 0)
            {
                if (gameData.Classes.Count > 0)
                {
                    foreach (DataPackClass classes in loadCfg.ClassID)
                    {
                        if (classes.ID <= gameData.Classes.Count)
                        {
                            // Load class in id list
                            cfg.ClassID.Add(new DataPackClass(classes.ID));
                            cfg.ClassID[cfg.ClassID.Count - 1].CloneFrom(classes);
                            tree_Class_Individual.Items.Add(new TreeViewItem() { Header = classes.Name, Tag = classes.ID.ToString() });
                        }
                    }
                    // Add additional class if their's some missing from the loadCfg
                    if (cfg.ClassID.Count < gameData.Classes.Count)
                    {
                        for (int i = cfg.ClassID.Count; i < gameData.Classes.Count; i++)
                        {
                            cfg.ClassID.Add(new DataPackClass(i, gameData.Classes[i].Name));
                            tree_Class_Individual.Items.Add(new TreeViewItem() { Header = cfg.ClassID[i].Name, Tag = i.ToString() });
                        }
                    }
                }

                // Apply the empty configuration
                tree_Class_Family.IsSelected = true;
                applyEmptyClass();

                // Remove the updating flag
                updating = false;
            }
        }

        #endregion

        #region Default

        private void loadClassDefault(Configuration loadCfg)
        {
            // Set the updating flag
            updating = true;

            // Load the class default
            cfg.ClassDefault.CloneFrom(loadCfg.ClassDefault);

            // Apply the empty configuration
            tree_Class_Family.IsSelected = true;
            applyEmptyClass();

            // Remove the updating flag
            updating = false;
        }

        #endregion

        #endregion

        #region Skill

        #region Family

        private void loadSkillFamily(Configuration loadCfg)
        {
            // Set the updating flag
            updating = true;

            // Clear available class list
            classAvailable.Clear();
            cfg.SkillAvailable.Clear();

            // Load available class list
            if (loadCfg.SkillAvailable.Count > 0)
            {
                foreach (Skill skill in loadCfg.SkillAvailable)
                {
                    cfg.SkillAvailable.Add(new Skill(skill.ID, skill.Name));
                }
            }

            // Clear current class family list
            cfg.SkillFamily.Clear();
            tree_Skill_Family.Items.Clear();
            if (loadCfg.SkillFamily.Count > 0)
            {
                foreach (DataPackSkill family in loadCfg.SkillFamily)
                {
                    // Load class in family
                    cfg.SkillFamily.Add(new DataPackSkill(family.ID));
                    cfg.SkillFamily[cfg.SkillFamily.Count - 1].CloneFrom(family);
                    tree_Skill_Family.Items.Add(new TreeViewItem() { Header = family.Name, Tag = family.ID.ToString() });
                }
            }

            // Apply the empty configuration
            tree_Skill_Family.IsSelected = true;
            applyEmptySkill();

            // Remove the updating flag
            updating = false;
        }

        #endregion

        #region Individual

        private void loadSkillID(Configuration loadCfg)
        {
            // Set the updating flag
            updating = true;

            // Clear current class id list
            cfg.SkillID.Clear();
            tree_Skill_Individual.Items.Clear();
            if (loadCfg.SkillID.Count > 0)
            {
                if (gameData.Skills.Count > 0)
                {
                    foreach (DataPackSkill skill in loadCfg.SkillID)
                    {
                        if (skill.ID <= gameData.Skills.Count)
                        {
                            // Load class in id list
                            cfg.SkillID.Add(new DataPackSkill(skill.ID));
                            cfg.SkillID[cfg.SkillID.Count - 1].CloneFrom(skill);
                            tree_Skill_Individual.Items.Add(new TreeViewItem() { Header = skill.Name, Tag = skill.ID.ToString() });
                        }
                    }
                    // Add additional class if their's some missing from the loadCfg
                    if (cfg.SkillID.Count < gameData.Skills.Count)
                    {
                        for (int i = cfg.SkillID.Count; i < gameData.Skills.Count; i++)
                        {
                            cfg.SkillID.Add(new DataPackSkill(i, gameData.Skills[i].Name));
                            tree_Skill_Individual.Items.Add(new TreeViewItem() { Header = cfg.SkillID[i].Name, Tag = i.ToString() });
                        }
                    }
                }

                // Apply the empty configuration
                tree_Skill_Family.IsSelected = true;
                applyEmptySkill();

                // Remove the updating flag
                updating = false;
            }
        }

        #endregion

        #region Default

        private void loadSkillDefault(Configuration loadCfg)
        {
            // Set the updating flag
            updating = true;

            // Load the class default
            cfg.SkillDefault.CloneFrom(loadCfg.SkillDefault);

            // Apply the empty configuration
            tree_Skill_Family.IsSelected = true;
            applyEmptySkill();

            // Remove the updating flag
            updating = false;
        }

        #endregion

        #endregion

        #region Passive Skill

        #region Family

        private void loadPassiveSkillFamily(Configuration loadCfg)
        {
            // Set the updating flag
            updating = true;

            // Clear available class list
            classAvailable.Clear();
            cfg.PassiveSkillAvailable.Clear();

            // Load available class list
            if (loadCfg.PassiveSkillAvailable.Count > 0)
            {
                foreach (Skill skill in loadCfg.PassiveSkillAvailable)
                {
                    cfg.PassiveSkillAvailable.Add(new Skill(skill.ID, skill.Name));
                }
            }

            // Clear current class family list
            cfg.PassiveSkillFamily.Clear();
            tree_PassiveSkill_Family.Items.Clear();
            if (loadCfg.PassiveSkillFamily.Count > 0)
            {
                foreach (DataPackPassiveSkill family in loadCfg.PassiveSkillFamily)
                {
                    // Load class in family
                    cfg.PassiveSkillFamily.Add(new DataPackPassiveSkill(family.ID));
                    cfg.PassiveSkillFamily[cfg.PassiveSkillFamily.Count - 1].CloneFrom(family);
                    tree_PassiveSkill_Family.Items.Add(new TreeViewItem() { Header = family.Name, Tag = family.ID.ToString() });
                }
            }

            // Apply the empty configuration
            tree_PassiveSkill_Family.IsSelected = true;
            applyEmptyPassiveSkill();

            // Remove the updating flag
            updating = false;
        }

        #endregion

        #region Individual

        private void loadPassiveSkillID(Configuration loadCfg)
        {
            // Set the updating flag
            updating = true;

            // Clear current class id list
            cfg.PassiveSkillID.Clear();
            tree_PassiveSkill_Individual.Items.Clear();
            if (loadCfg.PassiveSkillID.Count > 0)
            {
                if (gameData.Skills.Count > 0)
                {
                    foreach (DataPackPassiveSkill skill in loadCfg.PassiveSkillID)
                    {
                        if (skill.ID <= gameData.Skills.Count)
                        {
                            // Load class in id list
                            cfg.PassiveSkillID.Add(new DataPackPassiveSkill(skill.ID));
                            cfg.PassiveSkillID[cfg.PassiveSkillID.Count - 1].CloneFrom(skill);
                            tree_PassiveSkill_Individual.Items.Add(new TreeViewItem() { Header = skill.Name, Tag = skill.ID.ToString() });
                        }
                    }
                    // Add additional class if their's some missing from the loadCfg
                    if (cfg.PassiveSkillID.Count < gameData.Skills.Count)
                    {
                        for (int i = cfg.PassiveSkillID.Count; i < gameData.Skills.Count; i++)
                        {
                            cfg.PassiveSkillID.Add(new DataPackPassiveSkill(i, gameData.Skills[i].Name));
                            tree_PassiveSkill_Individual.Items.Add(new TreeViewItem() { Header = cfg.PassiveSkillID[i].Name, Tag = i.ToString() });
                        }
                    }
                }

                // Apply the empty configuration
                tree_PassiveSkill_Family.IsSelected = true;
                applyEmptyPassiveSkill();

                // Remove the updating flag
                updating = false;
            }
        }

        #endregion

        #region Default

        private void loadPassiveSkillDefault(Configuration loadCfg)
        {
            // Set the updating flag
            updating = true;

            // Load the class default
            cfg.PassiveSkillDefault.CloneFrom(loadCfg.PassiveSkillDefault);

            // Apply the empty configuration
            tree_PassiveSkill_Family.IsSelected = true;
            applyEmptyPassiveSkill();

            // Remove the updating flag
            updating = false;
        }

        #endregion

        #endregion

        #region Weapon

        #region Family

        private void loadWeaponFamily(Configuration loadCfg)
        {
            // Set the updating flag
            updating = true;

            // Clear available class list
            classAvailable.Clear();
            cfg.WeaponAvailable.Clear();

            // Load available class list
            if (loadCfg.WeaponAvailable.Count > 0)
            {
                foreach (Weapon weapon in loadCfg.WeaponAvailable)
                {
                    cfg.WeaponAvailable.Add(new Weapon(weapon.ID, weapon.Name));
                }
            }

            // Clear current class family list
            cfg.WeaponFamily.Clear();
            tree_Weapon_Family.Items.Clear();
            if (loadCfg.WeaponFamily.Count > 0)
            {
                foreach (DataPackEquipment family in loadCfg.WeaponFamily)
                {
                    // Load class in family
                    cfg.WeaponFamily.Add(new DataPackEquipment(family.ID));
                    cfg.WeaponFamily[cfg.WeaponFamily.Count - 1].CloneFrom(family);
                    tree_Weapon_Family.Items.Add(new TreeViewItem() { Header = family.Name, Tag = family.ID.ToString() });
                }
            }

            // Apply the empty configuration
            tree_Weapon_Family.IsSelected = true;
            applyEmptyWeapon();

            // Remove the updating flag
            updating = false;
        }

        #endregion

        #region Individual

        private void loadWeaponID(Configuration loadCfg)
        {
            // Set the updating flag
            updating = true;

            // Clear current class id list
            cfg.WeaponID.Clear();
            tree_Weapon_Individual.Items.Clear();
            if (loadCfg.WeaponID.Count > 0)
            {
                if (gameData.Weapons.Count > 0)
                {
                    foreach (DataPackEquipment weapon in loadCfg.WeaponID)
                    {
                        if (weapon.ID <= gameData.Weapons.Count)
                        {
                            // Load class in id list
                            cfg.WeaponID.Add(new DataPackEquipment(weapon.ID));
                            cfg.WeaponID[cfg.WeaponID.Count - 1].CloneFrom(weapon);
                            tree_Weapon_Individual.Items.Add(new TreeViewItem() { Header = weapon.Name, Tag = weapon.ID.ToString() });
                        }
                    }
                    // Add additional class if their's some missing from the loadCfg
                    if (cfg.WeaponID.Count < gameData.Weapons.Count)
                    {
                        for (int i = cfg.WeaponID.Count; i < gameData.Weapons.Count; i++)
                        {
                            cfg.WeaponID.Add(new DataPackEquipment(i, gameData.Weapons[i].Name));
                            tree_Weapon_Individual.Items.Add(new TreeViewItem() { Header = cfg.WeaponID[i].Name, Tag = i.ToString() });
                        }
                    }
                }

                // Apply the empty configuration
                tree_Weapon_Family.IsSelected = true;
                applyEmptyWeapon();

                // Remove the updating flag
                updating = false;
            }
        }

        #endregion

        #region Default

        private void loadWeaponDefault(Configuration loadCfg)
        {
            // Set the updating flag
            updating = true;

            // Load the class default
            cfg.WeaponDefault.CloneFrom(loadCfg.WeaponDefault);

            // Apply the empty configuration
            tree_Weapon_Family.IsSelected = true;
            applyEmptyWeapon();

            // Remove the updating flag
            updating = false;
        }

        #endregion

        #endregion

        #region Armor

        #region Family

        private void loadArmorFamily(Configuration loadCfg)
        {
            // Set the updating flag
            updating = true;

            // Clear available class list
            classAvailable.Clear();
            cfg.ArmorAvailable.Clear();

            // Load available class list
            if (loadCfg.ArmorAvailable.Count > 0)
            {
                foreach (Armor armor in loadCfg.ArmorAvailable)
                {
                    cfg.ArmorAvailable.Add(new Armor(armor.ID, armor.Name));
                }
            }

            // Clear current class family list
            cfg.ArmorFamily.Clear();
            tree_Armor_Family.Items.Clear();
            if (loadCfg.ArmorFamily.Count > 0)
            {
                foreach (DataPackEquipment family in loadCfg.ArmorFamily)
                {
                    // Load class in family
                    cfg.ArmorFamily.Add(new DataPackEquipment(family.ID));
                    cfg.ArmorFamily[cfg.ArmorFamily.Count - 1].CloneFrom(family);
                    tree_Armor_Family.Items.Add(new TreeViewItem() { Header = family.Name, Tag = family.ID.ToString() });
                }
            }

            // Apply the empty configuration
            tree_Armor_Family.IsSelected = true;
            applyEmptyArmor();

            // Remove the updating flag
            updating = false;
        }

        #endregion

        #region Individual

        private void loadArmorID(Configuration loadCfg)
        {
            // Set the updating flag
            updating = true;

            // Clear current class id list
            cfg.ArmorID.Clear();
            tree_Armor_Individual.Items.Clear();
            if (loadCfg.ArmorID.Count > 0)
            {
                if (gameData.Armors.Count > 0)
                {
                    foreach (DataPackEquipment armor in loadCfg.ArmorID)
                    {
                        if (armor.ID <= gameData.Armors.Count)
                        {
                            // Load class in id list
                            cfg.ArmorID.Add(new DataPackEquipment(armor.ID));
                            cfg.ArmorID[cfg.ArmorID.Count - 1].CloneFrom(armor);
                            tree_Armor_Individual.Items.Add(new TreeViewItem() { Header = armor.Name, Tag = armor.ID.ToString() });
                        }
                    }
                    // Add additional class if their's some missing from the loadCfg
                    if (cfg.ArmorID.Count < gameData.Armors.Count)
                    {
                        for (int i = cfg.ArmorID.Count; i < gameData.Armors.Count; i++)
                        {
                            cfg.ArmorID.Add(new DataPackEquipment(i, gameData.Armors[i].Name));
                            tree_Armor_Individual.Items.Add(new TreeViewItem() { Header = cfg.ArmorID[i].Name, Tag = i.ToString() });
                        }
                    }
                }

                // Apply the empty configuration
                tree_Armor_Family.IsSelected = true;
                applyEmptyArmor();

                // Remove the updating flag
                updating = false;
            }
        }

        #endregion

        #region Default

        private void loadArmorDefault(Configuration loadCfg)
        {
            // Set the updating flag
            updating = true;

            // Load the class default
            cfg.ArmorDefault.CloneFrom(loadCfg.ArmorDefault);

            // Apply the empty configuration
            tree_Armor_Family.IsSelected = true;
            applyEmptyArmor();

            // Remove the updating flag
            updating = false;
        }

        #endregion

        #endregion

        #region Enemy

        #region Family

        private void loadEnemyFamily(Configuration loadCfg)
        {
            // Set the updating flag
            updating = true;

            // Clear available class list
            classAvailable.Clear();
            cfg.EnemyAvailable.Clear();

            // Load available class list
            if (loadCfg.EnemyAvailable.Count > 0)
            {
                foreach (Enemy enemies in loadCfg.EnemyAvailable)
                {
                    cfg.EnemyAvailable.Add(new Enemy(enemies.ID, enemies.Name));
                }
            }

            // Clear current class family list
            cfg.EnemyFamily.Clear();
            tree_Enemy_Family.Items.Clear();
            if (loadCfg.EnemyFamily.Count > 0)
            {
                foreach (DataPackEnemy family in loadCfg.EnemyFamily)
                {
                    // Load class in family
                    cfg.EnemyFamily.Add(new DataPackEnemy(family.ID));
                    cfg.EnemyFamily[cfg.EnemyFamily.Count - 1].CloneFrom(family);
                    tree_Enemy_Family.Items.Add(new TreeViewItem() { Header = family.Name, Tag = family.ID.ToString() });
                }
            }

            // Apply the empty configuration
            tree_Enemy_Family.IsSelected = true;
            applyEmptyEnemy();

            // Remove the updating flag
            updating = false;
        }

        #endregion

        #region Individual

        private void loadEnemyID(Configuration loadCfg)
        {
            // Set the updating flag
            updating = true;

            // Clear current class id list
            cfg.EnemyID.Clear();
            tree_Enemy_Individual.Items.Clear();
            if (loadCfg.EnemyID.Count > 0)
            {
                if (gameData.Enemies.Count > 0)
                {
                    foreach (DataPackEnemy enemies in loadCfg.EnemyID)
                    {
                        if (enemies.ID <= gameData.Enemies.Count)
                        {
                            // Load class in id list
                            cfg.EnemyID.Add(new DataPackEnemy(enemies.ID));
                            cfg.EnemyID[cfg.EnemyID.Count - 1].CloneFrom(enemies);
                            tree_Enemy_Individual.Items.Add(new TreeViewItem() { Header = enemies.Name, Tag = enemies.ID.ToString() });
                        }
                    }
                    // Add additional class if their's some missing from the loadCfg
                    if (cfg.EnemyID.Count < gameData.Enemies.Count)
                    {
                        for (int i = cfg.EnemyID.Count; i < gameData.Enemies.Count; i++)
                        {
                            cfg.EnemyID.Add(new DataPackEnemy(i, gameData.Enemies[i].Name));
                            tree_Enemy_Individual.Items.Add(new TreeViewItem() { Header = cfg.EnemyID[i].Name, Tag = i.ToString() });
                        }
                    }
                }

                // Apply the empty configuration
                tree_Enemy_Family.IsSelected = true;
                applyEmptyEnemy();

                // Remove the updating flag
                updating = false;
            }
        }

        #endregion

        #region Default

        private void loadEnemyDefault(Configuration loadCfg)
        {
            // Set the updating flag
            updating = true;

            // Load the class default
            cfg.EnemyDefault.CloneFrom(loadCfg.EnemyDefault);

            // Apply the empty configuration
            tree_Enemy_Family.IsSelected = true;
            applyEmptyEnemy();

            // Remove the updating flag
            updating = false;
        }

        #endregion

        #endregion

        #region State

        #region Family

        private void loadStateFamily(Configuration loadCfg)
        {
            // Set the updating flag
            updating = true;

            // Clear available class list
            classAvailable.Clear();
            cfg.StateAvailable.Clear();

            // Load available class list
            if (loadCfg.StateAvailable.Count > 0)
            {
                foreach (State armor in loadCfg.StateAvailable)
                {
                    cfg.StateAvailable.Add(new State(armor.ID, armor.Name));
                }
            }

            // Clear current class family list
            cfg.StateFamily.Clear();
            tree_State_Family.Items.Clear();
            if (loadCfg.StateFamily.Count > 0)
            {
                foreach (DataPackState family in loadCfg.StateFamily)
                {
                    // Load class in family
                    cfg.StateFamily.Add(new DataPackState(family.ID));
                    cfg.StateFamily[cfg.StateFamily.Count - 1].CloneFrom(family);
                    tree_State_Family.Items.Add(new TreeViewItem() { Header = family.Name, Tag = family.ID.ToString() });
                }
            }

            // Apply the empty configuration
            tree_State_Family.IsSelected = true;
            applyEmptyState();

            // Remove the updating flag
            updating = false;
        }

        #endregion

        #region Individual

        private void loadStateID(Configuration loadCfg)
        {
            // Set the updating flag
            updating = true;

            // Clear current class id list
            cfg.StateID.Clear();
            tree_State_Individual.Items.Clear();
            if (loadCfg.StateID.Count > 0)
            {
                if (gameData.States.Count > 0)
                {
                    foreach (DataPackState state in loadCfg.StateID)
                    {
                        if (state.ID <= gameData.States.Count)
                        {
                            // Load class in id list
                            cfg.StateID.Add(new DataPackState(state.ID));
                            cfg.StateID[cfg.StateID.Count - 1].CloneFrom(state);
                            tree_State_Individual.Items.Add(new TreeViewItem() { Header = state.Name, Tag = state.ID.ToString() });
                        }
                    }
                    // Add additional class if their's some missing from the loadCfg
                    if (cfg.StateID.Count < gameData.States.Count)
                    {
                        for (int i = cfg.StateID.Count; i < gameData.States.Count; i++)
                        {
                            cfg.StateID.Add(new DataPackState(i, gameData.States[i].Name));
                            tree_State_Individual.Items.Add(new TreeViewItem() { Header = cfg.StateID[i].Name, Tag = i.ToString() });
                        }
                    }
                }

                // Apply the empty configuration
                tree_State_Family.IsSelected = true;
                applyEmptyState();

                // Remove the updating flag
                updating = false;
            }
        }

        #endregion

        #region Default

        private void loadStateDefault(Configuration loadCfg)
        {
            // Set the updating flag
            updating = true;

            // Load the class default
            cfg.StateDefault.CloneFrom(loadCfg.StateDefault);

            // Apply the empty configuration
            tree_State_Family.IsSelected = true;
            applyEmptyState();

            // Remove the updating flag
            updating = false;
        }

        #endregion

        #endregion

        #endregion

        #endregion
    }
}
