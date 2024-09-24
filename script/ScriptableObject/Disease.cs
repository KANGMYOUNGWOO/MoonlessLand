using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Disease : ScriptableObject
{
    public string DiseaseName;

    public string red_DiseaseExplain;
    public string yellow_DiseaseExplain;
    public string green_DiseaseExplain;
    public string purple_DiseaseExplain;
    public string blue_DiseaseExplain;
    public string black_DiseaseExplain;

    public int    DiseaseCode;

    public int    t_PenaltyValue;
    public int    e_PenaltyValue; 
    public int    s_PenaltyValue1;
    public int    s_PenaltyValue2;

    public int PenaltyTurn;
   
    public bool isTurnDm;
    public bool isEndDm;
    public bool isStat1;
    public bool isStat2;
    public bool idEndMulti;

    public string statType1;
    public string statType2;




    public enum DiseaseType { Black, Red, Blue, Green, purple, Yellow };
    public DiseaseType diseaseType;


}
