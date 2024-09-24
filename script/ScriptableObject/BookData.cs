using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BookData : ScriptableObject
{
    #region Numbers

    public List<string> AreaNames = new List<string>();
    public List<int> StartList = new List<int>();
    public List<int> EndList   = new List<int>();

    #endregion

    public struct ResearchData
    {
        public string name;
        public string subname;
        public string explain;
        public string knowledge1;
        public string knowledge2;
        public string knowledge3;
        public string bonus1;
        public string bonus2;
        public string bonus3;

        public int bisFirst;
        public int bisBonus1;
        public int bisBonus2;
        public int bisBonus3;

    }


    public Dictionary<string, ResearchData> vegetalDict = new Dictionary<string, ResearchData>();
    public List<string> vegetalNameList = new List<string>();
    public List<string> vegetalSubNameList = new List<string>();

    public void ClearDicitonary()
    {
        AreaNames.Clear();
        StartList.Clear();
        EndList.Clear();
        vegetalNameList.Clear();
    }


    public void StartDictionary(string name, string subname, string explain, string knowledge1, string knowledge2, string knowledge3, 
        string bonus1, string bonus2, string bonus3, int first, int isbonus1, int isbonus2 , int isbonus3)
    {
        ResearchData      data;
        data.name       = name;
        data.subname    = subname;
        data.explain    = explain;
        data.knowledge1 = knowledge1;
        data.knowledge2 = knowledge2;
        data.knowledge3 = knowledge3;
        data.bonus1     = bonus1;
        data.bonus2     = bonus2;
        data.bonus3     = bonus3;

        data.bisFirst      = first;
        data.bisBonus1     = isbonus1;
        data.bisBonus2     = isbonus2;
        data.bisBonus3     = isbonus3;

        vegetalDict.Add(name,data);
        vegetalNameList.Add(name);
        
    }

    public void GetDictData(int index , out string name)
    {        
        string str_index =  vegetalNameList[index];
        ResearchData data;
        name = " ";
        if(vegetalDict.TryGetValue(str_index,out data))
        {
            name = str_index;
        }
    }

    public void SetDictData(string name, int index)
    {
        ResearchData data;
        if (vegetalDict.TryGetValue(name,out data))
        {
            switch(index)
            {
                case 0:
                    data.bisFirst = 1;
                    break;
                case 1:
                    data.bisBonus1 = 1;
                    break;
                case 2:
                    data.bisBonus2 = 1;
                    break;
                case 3:
                    data.bisBonus3 = 1;
                    break;
            }
        }
        vegetalDict[name] = data;

    }

    public void GetDictData(int index, out string name,out string subname ,out string explain , out string knowledge1, out string knowledge2,out string knowledge3, out string bonus1, out string bonus2, out string bonus3)
    {
        string str_index = vegetalNameList[index];
        ResearchData data;
        name       = " ";
        subname    = " "; 
        explain    = " ";
        knowledge1 = " ";
        knowledge2 = " ";
        knowledge3 = " ";
        bonus1     = " ";
        bonus2     = " ";
        bonus3     = " ";
        if (vegetalDict.TryGetValue(str_index, out data))
        {
            name       = str_index;
            subname    = data.subname;
            explain    = data.explain;
            knowledge1 = data.knowledge1;
            knowledge2 = data.knowledge2;
            knowledge3 = data.knowledge3;
            bonus1     = data.bonus1;
            bonus2     = data.bonus2;
            bonus3     = data.bonus3;

        }



    }


    


}
