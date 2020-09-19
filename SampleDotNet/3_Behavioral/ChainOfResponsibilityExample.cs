using System;

namespace SampleDotNet._3_Behavioral
{
    internal static class ChainOfResponsibilityExample
    {
        public static void Run()
        {
            var goblin = new Creature("Goblin", 2, 2);
            Console.WriteLine(goblin);

            var root = new CreatureModifier(goblin);

            root.Add(new NoBonusModifier(goblin));

            Console.WriteLine("Let's double the goblin's attack");
            root.Add(new DoubleAttackModifier(goblin));
            Console.WriteLine("Let's increase goblin's defense");
            root.Add(new IncreaseDefenseModifier(goblin));

            root.Handle();

            Console.WriteLine(goblin);
        }

        public class Creature
        {
            public string Name;
            public int Attack, Defense;

            public Creature(string name, int attack, int defense)
            {
                Name = name ?? throw new ArgumentNullException(nameof(name));
                Attack = attack;
                Defense = defense;
            }

            public override string ToString()
            {
                return $"{nameof(Name)}: {Name}, {nameof(Attack)}: {Attack}, {nameof(Defense)}: {Defense}";
            }
        }

        public class CreatureModifier
        {
            protected Creature creature;
            protected CreatureModifier next; // Linked list

            public CreatureModifier(Creature creature)
            {
                this.creature = creature ?? throw new ArgumentNullException(nameof(creature));
            }

            public void Add(CreatureModifier cm)
            {
                if (next != null) next.Add(cm);
                else next = cm;
            }

            public virtual void Handle() => next?.Handle();
        }

        public class DoubleAttackModifier : CreatureModifier
        {
            public DoubleAttackModifier(Creature creature) : base(creature)
            {
            }

            public override void Handle()
            {
                Console.WriteLine($"Doubling {creature.Name}'s attack");
                creature.Attack *= 2;
                base.Handle();
            }
        }

        public class IncreaseDefenseModifier : CreatureModifier
        {
            public IncreaseDefenseModifier(Creature creature) : base(creature)
            {
            }

            public override void Handle()
            {
                Console.WriteLine($"Increasing {creature.Name}'s defense");
                creature.Defense += 3;
                base.Handle();
            }
        }

        public class NoBonusModifier : CreatureModifier
        {
            public NoBonusModifier(Creature creature) : base(creature)
            {
            }

            public override void Handle()
            {
                // Nothing here - prevent from next
                Console.WriteLine("No bonuses for you!");
            }
        }
    }
}
