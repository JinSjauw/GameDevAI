using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class AttackTask : BTNode
{
    private Transform _agent;
    private Transform target;
    private string _targetKey;
    private Animator _animator;
    private int _attackDamage;
    private float _range;
    private IDamageable attackTarget;
    
    private float _waitTime = 1f; // in seconds
    private float _waitCounter = 0f;
    private bool _waiting = false;
    
    public AttackTask(Transform agent, string targetKey, float range, int attackDamage)
    {
        _agent = agent;
        _targetKey = targetKey;
        _range = range;
        _attackDamage = attackDamage;
        _animator = agent.GetComponentInChildren<Animator>();
    }

    public override NodeState Evaluate()
    {
        if (_waiting)
        {
            attackTarget.state = DamageState.ALIVE;
            _waitCounter += Time.deltaTime;
            if (_waitCounter >= _waitTime)
            {
                _waitCounter = 0f;
                _waiting = false;
            }
        }
        else
        {
            target = (Transform)GetData(_targetKey);
        
            if (target == null)
            {
                _state = NodeState.FAILURE;
                return _state;
            }
        
            //Attack
            attackTarget = target.GetComponent<IDamageable>();
        
            if (attackTarget == null)
            {
                _state = NodeState.FAILURE;
                return _state;
            }
        
            AnimationClip[] anims = _animator.runtimeAnimatorController.animationClips;
            foreach (AnimationClip anim in anims)
            {
                switch (anim.name)
                {
                    case "Attack":
                        _waitTime = anim.length;
                        break;
                }
            }
        
            if (Vector3.Distance(_agent.position, target.position) < _range && attackTarget.state != DamageState.ATTACKED)
            {
                _waiting = true;
                _animator.SetTrigger("Attack");
                attackTarget.TakeDamage(_agent, _attackDamage);
                
                _state = NodeState.SUCCESS;
                return _state;
            }
        }
        _state = NodeState.FAILURE;
        return _state;
    }
}
