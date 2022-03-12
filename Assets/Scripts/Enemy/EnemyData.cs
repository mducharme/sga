using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy/Enemyr", order = 0)]
    public class EnemyData : ScriptableObject
    {

        static public string RESOURCE_FOLDER = "Data/Enemies/";

        public List<Combat.AttackModifier> attackModifiers = new();
        public List<Combat.DefenseModifier> defenseModifiers = new();

        public int xp;
        public List<Loot> loots = new();

        //public SoundEffectsData stepsSoundEffects;
        //public SoundEffectsData hitSoundEffects;
        //public SoundEffectsData deathSoundEffects;

        public ParticleSystem hitParticles;
        public ParticleSystem deathParticles;

        static public EnemyData LoadFromResourceName(string name)
        {
            return (EnemyData)Resources.Load(RESOURCE_FOLDER + name);
        }
    }
}