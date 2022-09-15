using System;

namespace MemoryExampleCS
{
    struct GameObject_s
    {
        public int x;
        public int y;
    }

    class GameObject_c
    {
        public int x;
        public int y;
    }

    class Program
    {
        static void Main(string[] args)
        {
            GameObject_c enemy = new GameObject_c();
            enemy.x = 4;
            enemy.y = 5;
            DoSomething(enemy);
            Console.WriteLine($"(x: {enemy.x}, y: {enemy.y})");


            GameObject_s hero;
            hero.x = 4;
            hero.y = 5;
            DoSomething(hero);
            Console.WriteLine($"(x: {hero.x}, y: {hero.y})");
        }

        static void DoSomething(GameObject_c obj)
        {
            obj.x = 42;
            obj.y = 25;
        }

        static void DoSomething(GameObject_s obj)
        {
            obj.x = 42;
            obj.y = 25;
        }
    }
}