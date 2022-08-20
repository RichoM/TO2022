using System;
using System.Collections.Generic;
using System.Linq;

namespace Polymorphism
{
    class Program
    {
        static void Main(string[] args)
        {
            var hero = CreateHero();
            while (true)
            {
                Console.Clear();
                DisplayHero(hero);
                var item = ChooseItem(hero);
                if (item == null) return;
                hero.Use(item);
            }
        }

        static Hero CreateHero()
        {
            var hero = new Hero();
            for (var i = 0; i < 15; i++)
            {
                hero.Items.Add(new Poison());
            }
            for (var i = 0; i < 10; i++)
            {
                hero.Items.Add(new Elixir());
            }
            for (var i = 0; i < 5; i++)
            {
                hero.Items.Add(new Revive());
            }
            return hero;
        }

        static void DisplayHero(Hero hero)
        {
            Console.WriteLine($"HP: {hero.HP}");
            Console.WriteLine($"MP: {hero.MP}");
            Console.WriteLine();
            if (hero.IsDead)
            {
                Console.WriteLine("¡Estás muerto!");
                Console.WriteLine();
            }
        }

        static dynamic ChooseItem(Hero hero)
        {
            try
            {
                var groups = hero.Items.GroupBy(item => item.GetType()).ToArray();

                int index = 1;
                foreach (var group in groups)
                {
                    Console.WriteLine($"{index} - {group.Key.Name} (x{group.Count()})");
                    index++;
                }
                Console.WriteLine();

                var selectedIndex = int.Parse(Console.ReadLine());
                var selectedGroup = groups[selectedIndex - 1];

                return selectedGroup.FirstOrDefault();
            }
            catch
            {
                Console.WriteLine("Elección inválida. Intente de nuevo...");
                Console.WriteLine("");
                return ChooseItem(hero);
            }
        } 
    }

    class Hero
    {
        private float hp = 100;
        private float mp = 100;

        public float HP
        {
            get { return hp; }
            set { hp = Math.Min(Math.Max(value, 0), 100); }
        }

        public float MP
        {
            get { return mp; }
            set { mp = Math.Min(Math.Max(value, 0), 100); }
        }

        public List<dynamic> Items { get; } = new List<dynamic>();

        public bool IsAlive { get { return HP > 0; } }
        public bool IsDead { get { return !IsAlive; } }

        public void Use(dynamic item)
        {
            if (item.ApplyOn(this))
            {
                Items.Remove(item);
            }
        }
    }

    class Poison
    {
        public bool ApplyOn(Hero hero)
        {
            if (hero.IsDead) return false;
            hero.HP -= 10;
            return true;
        }
    }

    class Elixir
    {
        public bool ApplyOn(Hero hero)
        {
            if (hero.IsDead) return false;
            hero.HP += 20;
            hero.MP += 20;
            return true;
        }
    }

    class Revive
    {
        public bool ApplyOn(Hero hero)
        {
            if (hero.IsAlive) return false;
            hero.HP = hero.MP = 100;
            return true;
        }
    }
}
