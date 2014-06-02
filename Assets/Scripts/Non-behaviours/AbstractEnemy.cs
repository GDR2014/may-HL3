using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Non_behaviours
{
    public abstract class AbstractEnemy : MonoBehaviour
    {
        public int Health { get; protected set; }

        public void TakeDamage(int dmgAmount)
        {
            Health -= dmgAmount;
            Hurt(dmgAmount);
            CheckDeath();
        }

        protected void CheckDeath()
        {
            if (Health <= 0) Die();
        }

        abstract protected void Hurt(int dmgAmount);
        abstract protected void Die();
    }
}
