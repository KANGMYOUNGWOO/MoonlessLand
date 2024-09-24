using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterBase : MonoBehaviour
{
    [SerializeField]protected string Monster_Name;
    protected BookData data;

    [SerializeField]protected int behaviourCount;
    protected int currentBehaviour;

    [SerializeField]protected int first;
    [SerializeField]protected int last;
    protected BattleManager battleManager;
    protected LogicManager logicManager;
    public bool bisOption { get; set; }
    

    public abstract void Action(int num =0 , int result =0);
    

    public void SetManager(BattleManager bm, LogicManager lm)
    {
        battleManager = bm;
        logicManager = lm;
    }

    public void SetData(BookData b)
    {
        data = b;
    }

    public string getName()
    {
        return Monster_Name;
    }

    public int GetCurrentBehaviour()
    {
        return currentBehaviour;
    }


    public int SelectFirstBehaviour()
    {
        int a = 0;
        if (last == 1) a = 1;
        else a = UnityEngine.Random.Range(first, last);
        currentBehaviour = a;
        return a;
    }

    private bool GetBonusData(int bonus)
    {
        bool benefit = false;

        switch(bonus)
        {
            case 1:
                benefit = data.vegetalDict[Monster_Name].bisBonus1 == 1 ? true :false;
                break;
            case 2:
                benefit = data.vegetalDict[Monster_Name].bisBonus2 == 1 ? true : false;
                break;
            case 3:
                benefit = data.vegetalDict[Monster_Name].bisBonus3 == 1 ? true : false;
                break;
        }

        return benefit;

    }



}