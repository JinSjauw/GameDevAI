using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageState
{
    ALIVE = 0,
    ATTACKED = 1,
    DEAD = 2,
}

public interface IDamageable
{
    void TakeDamage(Transform attacker, int damage);

    DamageState state { get; set; }
}
    
