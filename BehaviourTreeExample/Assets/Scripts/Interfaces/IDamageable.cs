using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(Transform attacker, int damage);
    bool isAttacked { get; set; }
    bool isDead { get; set; }
}
    
