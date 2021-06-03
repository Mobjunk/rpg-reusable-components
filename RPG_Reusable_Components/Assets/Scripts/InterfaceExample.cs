using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceExample : MonoBehaviour
{

    public Attack attack;

    void Start()
    {
        attack = new AttackOne();
        attack = new AttackTwo();
        
        attack.Attack();
    }

}

public class AttackOne : Attack
{
    public void Attack()
    {
        throw new System.NotImplementedException();
    }

    public int GetAttackSpeed()
    {
        throw new System.NotImplementedException();
    }
}

public class AttackTwo : Attack
{
    public void Attack()
    {
        throw new System.NotImplementedException();
    }

    public int GetAttackSpeed()
    {
        throw new System.NotImplementedException();
    }
}

public interface Attack
{
    void Attack();

    int GetAttackSpeed();
}