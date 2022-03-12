using System.Collections.Generic;

namespace Combat
{
    [System.Serializable]
    public class Result
    {
        public readonly Attack attack;
        public readonly Defense defense;

        public List<Damage> damages = new();

        public bool isCritical;
        public float knockbackForce;
        public float disableDuration;

        public Result(Attack attack, Defense defense)
        {
            this.attack = attack;
            this.defense = defense;
        }

        public void AddDamage(DamageCategory type, int amount)
        {
            AddDamage(new Damage(type, amount));
        }

        public void AddDamage(Damage damage)
        {
            Damage exist = damages.Find(d => d.type == damage.type);
            if (exist != null)
            {
                exist.amount += damage.amount;
            }
            else
            {
                damages.Add(damage);
            }
        }

        public int GetTotalDamage()
        {
            float amount = 0;
            foreach (Damage d in damages)
            {
                amount += d.amount;
            }
            return (int)amount;
        }
    }
}