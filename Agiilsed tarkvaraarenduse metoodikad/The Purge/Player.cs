using System;

namespace The_Purge
{
    public class Player
    {
        public int Health { get; private set; }
        public int Food { get; private set; }

        private const int MaxHealth = 100;
        private const int MaxFood = 10;

        public Player(int initialHealth, int initialFood)
        {
            Health = Math.Clamp(initialHealth, 0, MaxHealth);
            Food = Math.Clamp(initialFood, 0, MaxFood);
        }

        public void TakeDamage(int amount)
        {
            if (amount <= 0) return;

            int actualDamage = Math.Min(amount, Health);
            Health -= actualDamage;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"OUCH! You took {actualDamage} damage. Remaining Health: {Health}");
            Console.ResetColor();
        }

        public bool ConsumeFood(int amount = 1)
        {
            if (amount <= 0) return false;

            if (Food >= amount)
            {
                Food -= amount;
                return true;
            }
            return false;
        }

        public void AddFood(int amount)
        {
            if (amount <= 0) return;

            int spaceAvailable = MaxFood - Food;
            int added = Math.Min(amount, spaceAvailable);

            if (added > 0)
            {
                Food += added;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Found {added} food ration(s)! Current Food: {Food}");
                Console.ResetColor();
            }
        }

        public void Heal(int amount)
        {
            if (amount <= 0) return;

            int potentialHeal = MaxHealth - Health;
            int healed = Math.Min(amount, potentialHeal);

            if (healed > 0)
            {
                Health += healed;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Restored {healed} health. Current Health: {Health}");
                Console.ResetColor();
            }
        }

        public bool IsAlive => Health > 0;

        public string GetStatus()
        {
            return $"Health: {Health}/{MaxHealth} | Food: {Food}/{MaxFood}";
        }
    }
}