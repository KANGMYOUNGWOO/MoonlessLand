using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class nimrod : ScriptableObject
{

    public int code;
 

    public int strength;
    public int agility;
    public int examine;
    public int stealth;

    public int maxAttackTrait;
    public int maxGuardTrait;
    public int maxPassiveTrait;

    public string Name;
  
    public List<int> EssentialAttack = new List<int>();
    public List<int> EssentialGuard = new List<int>();
    public List<int> EssentialPassive = new List<int>();

}
