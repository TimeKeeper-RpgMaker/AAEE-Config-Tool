using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AAEE.Data;

namespace AAEE.Utility
{
    public class GameData
    {
        #region Fields

        private List<Actor> actors = new List<Actor>();
        private List<Class> classes = new List<Class>();
        private List<Skill> skills = new List<Skill>();
        private List<Item> items = new List<Item>();
        private List<Weapon> weapons = new List<Weapon>();
        private List<Armor> armors = new List<Armor>();
        private List<Enemy> enemies = new List<Enemy>();
        private List<State> states = new List<State>();
        private List<Animation> animations = new List<Animation>();

        #endregion

        #region Properties

        /// <summary>Gets the Actors used in the game.</summary>
        public List<Actor> Actors
        {
            get { return actors; }
        }
        /// <summary>Gets the Classes used in the game.</summary>
        public List<Class> Classes
        {
            get { return classes; }
        }
        /// <summary>Gets the Skills used in the game.</summary>
        public List<Skill> Skills
        {
            get { return skills; }
        }
        /// <summary>Gets the Weapons used in the game.</summary>
        public List<Weapon> Weapons
        {
            get { return weapons; }
        }
        /// <summary>Gets the Armors used in the game.</summary>
        public List<Armor> Armors
        {
            get { return armors; }
        }
        /// <summary>Gets the Items used in the game.</summary>
        public List<Item> Items
        {
            get { return items; }
        }
        /// <summary>Gets the Enemies used in the game.</summary>
        public List<Enemy> Enemies
        {
            get { return enemies; }
        }
        /// <summary>Gets the States used in the game.</summary>
        public List<State> States
        {
            get { return states; }
        }
        /// <summary>Gets the States used in the game.</summary>
        public List<Animation> Animations
        {
            get { return animations; }
        }
        #endregion

        #region Behavior

        /// <summary>Adds an Actor to the data.</summary>
        /// <param name="id">ID of the Actor.</param>
        /// <param name="name">Name of the Actor.</param>
        public void AddActor(int id, string name)
        {
            actors.Add(new Actor(id, name));
        }
        /// <summary>Adds a Class to the data.</summary>
        /// <param name="id">ID of the Class.</param>
        /// <param name="name">Name of the Class.</param>
        public void AddClass(int id, string name)
        {
            classes.Add(new Class(id, name));
        }
        /// <summary>Adds a Skill to the data.</summary>
        /// <param name="id">ID of the Skill.</param>
        /// <param name="name">Name of the Skill.</param>
        public void AddSkill(int id, string name)
        {
            skills.Add(new Skill(id, name));
        }
        /// <summary>Adds an Item to the data.</summary>
        /// <param name="id">ID of the Item.</param>
        /// <param name="name">Name of the Item.</param>
        public void AddItem(int id, string name)
        {
            items.Add(new Item(id, name));
        }
        /// <summary>Adds a Weapon to the data.</summary>
        /// <param name="id">ID of the Weapon.</param>
        /// <param name="name">Name of the Weapon.</param>
        public void AddWeapon(int id, string name)
        {
            weapons.Add(new Weapon(id, name));
        }
        /// <summary>Adds an Armor to the data.</summary>
        /// <param name="id">ID of the Weapon.</param>
        /// <param name="name">Name of the Weapon.</param>
        public void AddArmor(int id, string name)
        {
            armors.Add(new Armor(id, name));
        }
        /// <summary>Adds an Enemy to the data.</summary>
        /// <param name="id">ID of the Enemy.</param>
        /// <param name="name">Name of the Enemy.</param>
        public void AddEnemy(int id, string name)
        {
            enemies.Add(new Enemy(id, name));
        }
        /// <summary>Adds a State to the data.</summary>
        /// <param name="id">ID of the Enemy.</param>
        /// <param name="name">Name of the Enemy.</param>
        public void AddState(int id, string name)
        {
            states.Add(new State(id, name));
        }
        /// <summary>Adds an Animation to the data.</summary>
        /// <param name="id">ID of the Enemy.</param>
        /// <param name="name">Name of the Enemy.</param>
        public void AddAnimation(int id, string name)
        {
            animations.Add(new Animation(id, name));
        }
        /// <summary>Clears all loaded game data.</summary>
        public void Clear()
        {
            actors.Clear();
            classes.Clear();
            skills.Clear();
            items.Clear();
            weapons.Clear();
            armors.Clear();
            enemies.Clear();
            states.Clear();
            animations.Clear();
        }

        #endregion
    }
}
