using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;





public class CharacterManager : MonoBehaviour, IManager
{
    public GameManager gameManager { get { return GameManager.gameManager; } }



    public delegate int Damage(int dam);
    public delegate int condition(int a, int b);

    public delegate bool trait(int hp = 0, int value = 0, string compareType = "", string type = "");


    #region 특성 대리자 모음

    public trait hpMore;
    public trait hpLess;
    public trait invenMore;

    /*
           대리자 사용 방법

           먼저 pc의 keyword에 따라 역할이 분류됩니다.

           if(pc.keyword == statBonus) -> calculateBonus
           if(pc.keyword == turnDamage) -> AdjustTurnDamage

           그 후 pc의 calcultype에 따라 계산 방식(calculdel)을 선택합니다

           ex) hpMore, hpLess, invenMore...
           calculdel =  CalculDictionary[pc.calcultype];

           calculdel은 pc.type (질병이면 질병코드 스탯이면 스탯코드)과 받은 string을 대조해
           참이거나 pc.type == all 이면 계산을 실행합니다.

           if(pc.type == "red")
           if(pc.type == "strength")


          */


    #endregion





    public PlayerInfo characterInfo;

    private List<int> traitList = new List<int>();
    public List<string> bonusExplainList = new List<string>();

    private Dictionary<string, trait> CalculDict = new Dictionary<string, trait>();
    private Dictionary<int, int> CoolTimes = new Dictionary<int, int>();

    public DIseaseTab diseaseTab { get; set; }
    private ResourceManager resourceManager;
    private UIManager uiManager;
    public Inventory inventory { get; set; }
    public TokenUI tokenUI { get; set; }

    public int[,] a_cooltimeArray = new int[,] { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } };
    public int[,] a_cooltimeCodeArray = new int[,] { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } };
    public int[,] g_cooltimeArray = new int[,] { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } };
    public int[,] g_cooltimeCodeArray = new int[,] { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } };



    #region 임시변수


    public int penaltyTurn { get; set; }
    public int turn_damage { get; set; }
    public int end_damage { get; set; }

    public int attackDamage { get; set; }

    private int eventBonus = 0;
    private string eventStat = "";


    #endregion
    #region 조건 체크 대리자
    #region 인벤토리 체크

    private int Calcul_dmInven(int damage)
    { int i;
        int count = 0;
        for (i = 0; i < characterInfo.ItemArray.Length; i++)
        {
            if (characterInfo.ItemArray[i].item.itemcode != 0) count += 1;
        }

        return count;
    }

    #endregion
    #region  YellowDamageDelegator




    #endregion
    #region 토큰체크
    private bool More_green(int token = 0, int value = 0, string compareType = "", string type = "")
    {
        if (compareType == "") return false;
        if (compareType != type)
            if (compareType != "all")
                return false;

        int tok = 0;
        for (int i = 0; i < characterInfo.tokenArray.Length; i++)
        {
            if (characterInfo.tokenArray[i] == 2) tok += 1;
        }

        return token <= tok;
    }

    private bool More_red(int token = 0, int value = 0, string compareType = "", string type = "")
    {
        if (compareType == "") return false;
        if (compareType != type)
            if (compareType != "all")
                return false;

        int tok = 0;
        for (int i = 0; i < characterInfo.tokenArray.Length; i++)
        {
            if (characterInfo.tokenArray[i] == 1) tok += 1;
        }

        return token <= tok;
    }


    #endregion
    #region hp체크
    private bool More_hp(int hp = 0, int value = 0, string compareType = "", string type = "")
    {
        if (compareType == "") return false;
        if (compareType != type)
            if (compareType != "all")
                return false;


        return characterInfo.hp >= hp;

    }


    private bool less_hp(int hp = 0, int value = 0, string compareType = "", string type = "")
    {
        if (compareType == "") return false;
        if (compareType != type)
            if (compareType != "all")
                return false;

        return characterInfo.hp < hp;
    }

    #endregion

    #region 정신력 체크

    private int more_mp(int mp, int result)
    {
        if (characterInfo.mp >= mp)
            return result;

        return 0;
    }

    #endregion
    #region 쿨타임이 돌아올때마다
    private bool getCoolOn(int code = 0, int value = 0, string compareType = "", string type = "")
    {
        if (compareType == "") return false;
        if (compareType != type)
            if (compareType != "all")
                return false;


        return true;

    }


    #endregion

    #endregion

    #region 질병


    #region GetPureDamage
    private int GetPureDamage(int code, int value)
    {
        int result = value;

        switch (code)
        {
            case 3101:
                result = Calcul_dmInven(value);
                break;

            default:

                break;
        }

        return result;
    }

    #endregion
    #region 질병걸리기
    public void AddDisease(int code)
    {
        Disease disease;
        Sprite sprite;
        resourceManager.DiseaseDictionary.TryGetValue(code, out disease);
        bool bisend = false;
        int index = 0;
        string strindex = "green";

        while (index < 5)
        {
            if (characterInfo.DiseaseArray[index].DiseaseCode == 0)
            {
                penaltyTurn = disease.PenaltyTurn;
                turn_damage = disease.t_PenaltyValue;
                end_damage = disease.e_PenaltyValue;
                switch (disease.diseaseType)
                {
                    case Disease.DiseaseType.Red:
                        strindex = "red";
                        break;
                    case Disease.DiseaseType.Blue:
                        strindex = "blue";
                        break;
                    case Disease.DiseaseType.Green:
                        strindex = "green";
                        break;
                    case Disease.DiseaseType.Yellow:
                        strindex = "yellow";
                        break;
                    case Disease.DiseaseType.purple:
                        strindex = "purple";
                        break;
                    case Disease.DiseaseType.Black:
                        strindex = "black";
                        break;
                }

                strindex = strindex + (code / 1000).ToString();


                sprite = resourceManager.I_DiseaseDictionary[strindex];

                if (disease.isTurnDm) turn_damage = GetPureDamage(disease.DiseaseCode, disease.t_PenaltyValue);

                if (disease.isEndDm) end_damage = GetPureDamage(disease.DiseaseCode, disease.e_PenaltyValue);

                if (disease.isTurnDm) AdjustTurnDamage(strindex);
                else turn_damage = 0;

                characterInfo.DiseaseArray[index].DiseaseCode = disease.DiseaseCode;
                characterInfo.DiseaseArray[index].DiseaseTurn = penaltyTurn;
                characterInfo.DiseaseArray[index].t_DiseaseValue = turn_damage;
                characterInfo.DiseaseArray[index].e_DiseaseValue = end_damage;

                characterInfo.DiseaseArray[index].disease = disease;

                diseaseTab.GetDiseaseUI(index, sprite);
                uiManager.diseaseEffect.gameObject.SetActive(true);
                uiManager.diseaseEffect.GetDiseaseUI(index,sprite);

                bisend = true;
                break;
            }

            index += 1;
        }

    }

    public void ReCalculateDiseaseDamage()
    {
        Disease current_disease;
        for (int i = 0; i < characterInfo.DiseaseArray.Length; i++)
        {
            if (characterInfo.DiseaseArray[i].DiseaseCode != 0)
            {
                current_disease = characterInfo.DiseaseArray[i].disease;

                if (current_disease.isTurnDm)
                {
                    turn_damage = GetPureDamage(current_disease.DiseaseCode, current_disease.t_PenaltyValue);
                    AdjustTurnDamage(current_disease.diseaseType.ToString());
                    characterInfo.DiseaseArray[i].t_DiseaseValue = turn_damage;

                }
                else turn_damage = 0;

            }


        }


    }


    public void AdjustDiseaseSlot()
    {
        string strindex;
        Disease disease;
        for (int i = 0; i < characterInfo.DiseaseArray.Length; i++)
        {
            if (characterInfo.DiseaseArray[i].DiseaseCode != 0)
            {
                disease = resourceManager.DiseaseDictionary[characterInfo.DiseaseArray[i].DiseaseCode];

                strindex = "red";
                switch (disease.diseaseType)
                {
                    case Disease.DiseaseType.Red:
                        strindex = "red";
                        break;
                    case Disease.DiseaseType.Blue:
                        strindex = "blue";
                        break;
                    case Disease.DiseaseType.Green:
                        strindex = "green";
                        break;
                    case Disease.DiseaseType.Yellow:
                        strindex = "yellow";
                        break;
                    case Disease.DiseaseType.purple:
                        strindex = "purple";
                        break;
                    case Disease.DiseaseType.Black:
                        strindex = "black";
                        break;
                }

                strindex = strindex + (disease.DiseaseCode / 1000).ToString();


                diseaseTab.AdjustDiseaseUI(i, resourceManager.I_DiseaseDictionary[strindex]);
            }
        }
    }

    #endregion
    #region 질병제거
    public void EndDisease(int index)
    {
        characterInfo.DiseaseArray[index].DiseaseCode = 0;
        characterInfo.DiseaseArray[index].disease = resourceManager.DiseaseDictionary[0];
        characterInfo.DiseaseArray[index].e_DiseaseValue = 0;
        characterInfo.DiseaseArray[index].t_DiseaseValue = 0;
        diseaseTab.EndDiseaseUI(index, null);
    }

    #endregion

    #region 질병걸렸는지 확인
    public bool checkDisease(int code)
    {
        for (int i = 0; i < characterInfo.DiseaseArray.Length; i++)
        {
            if (characterInfo.DiseaseArray[i].DiseaseCode == code)
                return true;
        }
        return false;
    }

    #endregion

    public void EndDisease(int index, int situation)
    {
        if (characterInfo.DiseaseArray[index].disease.isEndDm)
            playerGetDamage(characterInfo.DiseaseArray[index].e_DiseaseValue);
    }


    #region AdjustTurnDamage

    private void AdjustTurnDamage(string disease)
    {
        trait calculdel;
        passiveCard pc;

        for (int i = 0; i < traitList.Count; i++)
        {
            pc = resourceManager.PassiveDictionary[traitList[i]];
            if (pc.calculType == "turnDM")
            {
                // 연산 대리자에게 알맞는 함수를 할당해줍니다. 
                calculdel = CalculDict[pc.calculType];
                // bool 함수 결과에 따라 turn_damage 값을 pc.value 만큼 깎습니다.
                turn_damage = calculdel(pc.condition, pc.value, disease, pc.type) ? turn_damage - pc.value : turn_damage;
                // turn_damage 값이 0미만이라면 0으로 고정시켜줍니다.
                turn_damage = turn_damage < 0 ? 0 : turn_damage;

            }
        }
    }

    #endregion

    #endregion

    #region 아이템

    #region 아이템 추가

    public void AddItem(int code, int count, int smell = 0, int decay = 0)
    {

        bool bisExist = false;
        bool bisEseat = false;
        for (int i = 0; i < characterInfo.ItemArray.Length; i++)
        {
            if (characterInfo.ItemArray[i].item.itemcode == code)
            {
                Item tempitem = resourceManager.ItemDictionary[code];
                characterInfo.ItemArray[i].ItemCount += count;
                bisExist = true;
                inventory.AdjustSlot(i, tempitem, characterInfo.ItemArray[i].ItemCount);
                break;
            }
        }
        if (!bisExist)
        {
            for (int i = 0; i < characterInfo.ItemArray.Length; i++)
            {
                if (characterInfo.ItemArray[i].ItemCount == 0)
                {
                    Item tempitem = resourceManager.ItemDictionary[code];
                    characterInfo.ItemArray[i].item = resourceManager.ItemDictionary[code];
                    // characterInfo.ItemArray[i].ItemSmell = (smell == 0) ? tempitem.ItemSmell : smell; 
                    characterInfo.ItemArray[i].ItemDecay = (decay == 0) ? tempitem.ItemDecay : decay;
                    characterInfo.ItemArray[i].ItemCount += count;

                    bisEseat = true;

                    inventory.AdjustSlot(i, tempitem, count);

                    break;
                }
            }

        }
        // if (!bisEseat) Debug.Log("ㅠㅠ");


    }
    #endregion

    #region 아이템 제거

    public Item RemoveItem(int code, int count)
    {
        bool bisExist = false;
        bool bisEseat = false;
        Item tempitem = null;
        for (int i = 0; i < characterInfo.ItemArray.Length; i++)
        {
            if (characterInfo.ItemArray[i].item.itemcode == code)
            {
                //characterInfo.ItemArray[i].ItemCount = Mathf.Clamp(characterInfo.ItemArray[i].ItemCount - count, 0, 99);
                characterInfo.ItemArray[i].ItemCount -= count;
                if (characterInfo.ItemArray[i].ItemCount < 0) characterInfo.ItemArray[i].ItemCount = 0;
                bisEseat = true;

                if (characterInfo.ItemArray[i].ItemCount == 0)
                {
                    tempitem = resourceManager.ItemDictionary[0];


                    characterInfo.ItemArray[i].ItemDecay = 0;
                    characterInfo.ItemArray[i].ItemSmell = 0;
                }

                else
                {
                    tempitem = resourceManager.ItemDictionary[code];
                }
                characterInfo.ItemArray[i].item = tempitem;
                inventory.AdjustSlot(i, tempitem, characterInfo.ItemArray[i].ItemCount);
                bisExist = true;
                break;
            }

        }

        tempitem = resourceManager.ItemDictionary[code];
        return tempitem;

    }




    #endregion

    #region 아이템합성
    public int MedLevel(int code1, int code2, int code3)
    {       
        int[] levels = new int[3];

        levels[0] = code1 / 1000;
        levels[1] = code2 / 1000;
        levels[2] = code3 / 1000;

        int max = 1;
      
        for (int i = 0; i < 3; i++)
        {
            max = levels[i] > max ? levels[i] : max;
        }
        
        return max;
    }




    public int CombineItem(int code1, int code2, int code3 ,  int option , int detail)
    {
        Item.ItemType itempType = resourceManager.ItemDictionary[code1].type;
        int result = 1111;
        int type = 1;
        int company = 1;


        int[] levels = new int[3];

        levels[0] = code1 / 1000;
        levels[1] = code2 / 1000;
        levels[2] = code3 / 1000;

        int max = 1;

        int sum = 0;
        int mul = 0;

        int rand = 0;
       

        int perc = 0;
        int level = 1;


        for (int i = 0; i < 3; i++)
        {
            max = levels[i] > max ? levels[i] : max;
        }

        switch (max)
        {
            case 1:
                level = 1;
                break;
            #region 2
            case 2:
                if (levels[0] == max)
                {
                    sum = levels[1] + levels[2] - max * 2;
                    mul = (levels[1] - max) + (levels[2] - max);
                }
                else if (levels[1] == max)
                {
                    sum = levels[0] + levels[2] - max * 2;
                    mul = (levels[0] - max) + (levels[2] - max);
                }
                else if (levels[2] == max)
                {
                    sum = levels[0] + levels[1] - max * 2;
                    mul = (levels[0] - max) + (levels[1] - max);
                }

                rand = UnityEngine.Random.Range(1, 11);
                if (sum - mul == -3) perc = 3;
                else if (sum - mul == -1) perc = 7;
                else if (sum - mul == 0) perc = 10;

                level = rand <= perc ? 2 : 1;

                break;

            #endregion

            #region 3

            case 3:
                if (levels[0] == max)
                {
                    sum = levels[1] + levels[2] - max * 2;
                    mul = (levels[1] - max) + (levels[2] - max);
                }
                else if (levels[1] == max)
                {
                    sum = levels[0] + levels[2] - max * 2;
                    mul = (levels[0] - max) + (levels[2] - max);
                }
                else if (levels[2] == max)
                {
                    sum = levels[0] + levels[1] - max * 2;
                    mul = (levels[0] - max) + (levels[1] - max);
                }

                rand = UnityEngine.Random.Range(1, 11);
                if (sum - mul == -8) perc = 5;
                else if (sum - mul == -5) perc = 6;
                else if (sum - mul == -3) perc = 7;
                else if (sum - mul == -2) perc = 8;
                else if (sum - mul == -1) perc = 9;
                else if (sum - mul == 0) perc = 10;

                level = rand <= perc ? 3 : 2;
                break;
            #endregion

            #region 4
            case 4:
                if (levels[0] == max)
                {
                    sum = levels[1] + levels[2] - max * 2;
                    mul = (levels[1] - max) + (levels[2] - max);
                }
                else if (levels[1] == max)
                {
                    sum = levels[0] + levels[2] - max * 2;
                    mul = (levels[0] - max) + (levels[2] - max);
                }
                else if (levels[2] == max)
                {
                    sum = levels[0] + levels[1] - max * 2;
                    mul = (levels[0] - max) + (levels[1] - max);
                }

                rand = UnityEngine.Random.Range(1, 11);
                if (sum - mul == -15) perc = 2;
                else if (sum - mul == -11) perc = 3;
                else if (sum - mul == -8) perc = 4;
                else if (sum - mul == -7) perc = 5;
                else if (sum - mul == -5) perc = 6;
                else if (sum - mul == -3) perc = 7;
                else if (sum - mul == -2) perc = 8;
                else if (sum - mul == -1) perc = 9;
                else if (sum - mul == 0) perc = 10;

                level = rand <= perc ? 4 : 3;

                break;


                #endregion
        }
         switch (itempType)
        {
            
            case Item.ItemType.Medicine:
                CombineMedicine();
                break;
          
            case Item.ItemType.Consumable:
                CombineConsumable();
                break;
           
            case Item.ItemType.Essence:
                CombineEssence();
                break;
        }

       

         #region 자연물 합성
    void CombineConsumable()
        {

      
            switch(option)
            {
                #region 제조사를 골랐을 때
                case 0:
                    company = detail;

                    switch (detail)
                    {
                        case 1:
                            type = UnityEngine.Random.Range(1, 5);
                            if (type == 3) type = 5;
                            break;
                        case 2:
                            type = UnityEngine.Random.Range(1, 4);
                            if (type == 3) type = 5;
                            break;
                        case 3:
                            type = UnityEngine.Random.Range(1, 5);
                            if (type == 3) type = 5;
                            break;
                        case 4:
                            type = UnityEngine.Random.Range(3, 6);
                            if (type == 3) type = 1;
                            break;
                        case 5:
                            type = 6;
                            break;
                    }
                break;
                #endregion
                #region 색을 골랐을 때
                case 1:
                    type = detail;
                    switch(detail)
                    {
                        case 1:
                            company = UnityEngine.Random.Range(1, 5);
                            break;
                        case 2:
                            company = UnityEngine.Random.Range(1, 4);
                            break;
                        case 4:
                            company = UnityEngine.Random.Range(1, 4);
                            if (company == 2) company = 4;
                            break;
                        case 5:
                            company = UnityEngine.Random.Range(1, 5);
                            break;

                    }
                    #endregion
                    break;
                case 2:
                    level = max;
                    company = UnityEngine.Random.Range(1, 6);
                    switch (company)
                    {
                        case 1:
                            type = UnityEngine.Random.Range(1, 5);
                            if (type == 3) type = 5;
                            break;
                        case 2:
                            type = UnityEngine.Random.Range(1, 4);
                            if (type == 3) type = 5;
                            break;
                        case 3:
                            type = UnityEngine.Random.Range(1, 5);
                            if (type == 3) type = 5;
                            break;
                        case 4:
                            type = UnityEngine.Random.Range(3, 6);
                            if (type == 3) type = 1;
                            break;
                        case 5:
                            type = 6;
                            break;
                    }

                    break;
            }
            

                    /*
                     switch(company)
                     {
                         case 1:
                             type = UnityEngine.Random.Range(1,5);
                             if (type == 3) type = 5;
                             break;
                         case 2:
                             type = UnityEngine.Random.Range(1, 4);
                             if (type == 3) type = 5;
                             break;
                         case 3:
                             type = UnityEngine.Random.Range(1, 5);
                             if (type == 3) type = 5;
                             break;
                         case 4:
                             type = UnityEngine.Random.Range(3, 6);
                             if (type == 3) type = 1;
                             break;
                         case 5:
                             type = 6;
                             break;


                     }

                     result = level * 1000 + type * 100 + company * 10 + 1;
                     */

                    result = level * 1000 + type * 100 + company * 10 + 1;

        }
        #endregion

        #endregion

        #region 약물 합성
        void CombineMedicine()
        {
            int[] levels    = new int[3];
            int[] companies = new int[3];
            int[] types     = new int[3];

            levels[0] = code1 / 1000;
            levels[1] = code2 / 1000;
            levels[2] = code3 / 1000;

            types[0] = (code1 / 100) % 10;
            types[1] = (code2 / 100) % 10;
            types[2] = (code3 / 100) % 10;

            companies[0] = (code1 / 10) % 10;
            companies[1] = (code2 / 10) % 10;
            companies[2] = (code3 / 10) % 10;

            int max = 1;
            int levelcount = 0;
            int random = 0;

            int company = 0;
            int type = 0;

            
            int level = 1;

            for (int i = 0; i < 3; i++)
            {
                max = levels[i] > max ? levels[i] : max;
            }

            for(int i=0; i<3; i++)
            {
                if (levels[i] == max) levelcount += 1; 
            }

            level = levelcount > 1 ? max + 1 : max;
            if (level > 4) level = 4;

            random = UnityEngine.Random.Range(0,3);
            type = types[random];
            random = UnityEngine.Random.Range(0, 3);
            company = companies[random];

            result = level * 1000 + type * 100 + company * 10 + 1;
            if (companies[0] == companies[1] && companies[0] == companies[2])
            {
                random = UnityEngine.Random.Range(1, 6);
                inventory.Bonus(resourceManager.I_CardBackDictionary[levels[0] * 100 + 10 +random]);
                AddTestCard(levels[0] * 100 + 10 + random);
                //inventory.Bonus();
            }
        }

        #endregion


        #region 모듈합성
        void CombineEssence()
        {
            
            result = 0;
            AddModule(code1%100);
        }

        #endregion

        Debug.Log(result);
        return result;
    }


    #endregion




    #region 피해받기와 정신력 소모하기(반드시 이것만 쓸것 characterinfo.stat 직접 건드리면 죽빵 개 쎄게 때림)

    public void playerGetArmor(int armor)
    {
        if (characterInfo.armor == 0)
        {
            characterInfo.armor += armor;
          
            uiManager.ArmorApear(armor,characterInfo.hp,true);
        }
        else
        {
            characterInfo.armor += armor;
            
            uiManager.ArmorUI(characterInfo.armor,characterInfo.hp);
        }
    }


    public void playerGetDamage(int hp)
    { 
        int over;
       
        if(characterInfo.armor ==0)
        {
            characterInfo.hp -= hp;
            if (characterInfo.hp <= 0) hp = 0;
            if (characterInfo.hp <= 0)
            {
                characterInfo.hp = 0;
                uiManager.Gameover();
            }
            if (characterInfo.hp >= 200) hp = 200;
        }
        else
        {
            characterInfo.armor -= hp;
            if (characterInfo.armor <= 0)
            {
                over = Mathf.Abs(characterInfo.armor);
                characterInfo.armor = 0;
                uiManager.ArmorApear(0,0,false);
                characterInfo.hp -= over;
                if (characterInfo.hp <= 0) hp = 0;
                if (characterInfo.hp <= 0)
                {
                    characterInfo.hp = 0;
                    uiManager.Gameover();
                }
                if (characterInfo.hp >= 200) hp = 200;
            }
            else
            {
                uiManager.ArmorUI(characterInfo.armor,characterInfo.hp);
            }
        }
       
        
        uiManager.HPUI(characterInfo.hp, characterInfo.mp);
        /*
        characterInfo.hp -= hp;
        if (characterInfo.hp <= 0)
        {
            characterInfo.hp = 0;
            uiManager.Gameover();
        }
        if (characterInfo.hp >= 200) characterInfo.hp = 200;
        uiManager.HPUI(characterInfo.hp,characterInfo.mp);
       */
    }
    
    public void playerUseMana(int mp)
    {
        characterInfo.mp -= mp;
        if (characterInfo.mp <= 0) characterInfo.mp = 0;
        if (characterInfo.mp >= 200) characterInfo.mp = 200;
        uiManager.HPUI(characterInfo.hp, characterInfo.mp);
    }

    public void UseArmor(int ap)
    {
        if (characterInfo.armor == 0)
        {           
          
        }
        else
        {
            characterInfo.armor -= ap;
            if(characterInfo.armor > 0) uiManager.ArmorUI(characterInfo.armor, characterInfo.hp);            
            else  uiManager.ArmorApear(0,0,false);
            if (characterInfo.armor < 0) characterInfo.armor = 0;
        }              
    }


    #endregion


    #region  토큰

    #region 토큰 더하기

    public void AddToken(int color)
    {

        int tokenIndex = 0;
        for (tokenIndex = 0; tokenIndex < characterInfo.tokenArray.Length; tokenIndex++)
        {
            if (characterInfo.tokenArray[tokenIndex] == 0)
            {

                characterInfo.tokenArray[tokenIndex] = color;
                tokenUI.SetToken(tokenIndex, true, color);
                break;
            }
        }

        if (tokenIndex > 6)
        {
            for (tokenIndex = 0; tokenIndex < characterInfo.tokenArray.Length; tokenIndex++)
            {
                if (characterInfo.tokenArray[tokenIndex] == 1 || characterInfo.tokenArray[tokenIndex] == 3)
                {
                    tokenUI.SetToken(tokenIndex, true, color);
                    break;
                }
            }
        }

    }

    #region 토큰 빼기 
    public void RemoveToken(int color)
    {
       for(int i=0; i<characterInfo.tokenArray.Length;i++)
        {
            if(characterInfo.tokenArray[i] == color)
            {
                characterInfo.tokenArray[i] = 0;
                tokenUI.SetToken(i, false);
                break;
            }

        }
    }




    #endregion



    #endregion



    #endregion

    #region TestCard


    public void AddTestCard(int cardNum)
    {
        characterInfo.TestCardList.Add(cardNum);
    }




    #endregion
    #region
    public void AdjustTurn()
    {     
        TurnDamage();
        Cooling();
    }
    #endregion


    #region  대미지 받기

    private void TurnDamage()
    {
        for(int i=0; i<5;i++)
        {
            if (int.Equals(characterInfo.DiseaseArray[i].DiseaseCode,0)) continue;
            if (characterInfo.DiseaseArray[i].disease.isTurnDm) {  playerGetDamage(characterInfo.DiseaseArray[i].t_DiseaseValue); }
            if (characterInfo.DiseaseArray[i].disease.PenaltyTurn > 0)
            {
                characterInfo.DiseaseArray[i].DiseaseTurn -= 1;
               // if(characterInfo.DiseaseArray[i].DiseaseTurn == 0)
                    
            }           
        }                
    }


    #endregion


    #region 쿨타임 

    #region 쿨타임 걸기

    public void A_Cooltimed(int index , int code, int cool)
    {
        if (CoolTimes.ContainsKey(code)) CoolTimes[code] += cool;
        else CoolTimes.Add(code, cool);

        if (characterInfo.CurrentModule == characterInfo.CardModule1)      { a_cooltimeArray[0, index] = cool; a_cooltimeCodeArray[0, index] = code;}
        else if (characterInfo.CurrentModule == characterInfo.CardModule2) { a_cooltimeArray[1, index] = cool; a_cooltimeCodeArray[1, index] = code; }
        else if (characterInfo.CurrentModule == characterInfo.CardModule3) { a_cooltimeArray[2, index] = cool; a_cooltimeCodeArray[2, index] = code; }
        else if (characterInfo.CurrentModule == characterInfo.CardModule4) { a_cooltimeArray[3, index] = cool; a_cooltimeCodeArray[3, index] = code; }
        else if (characterInfo.CurrentModule == characterInfo.CardModule5) { a_cooltimeArray[4, index] = cool; a_cooltimeCodeArray[4, index] = code; }
        else if (characterInfo.CurrentModule == characterInfo.CardModule6) { a_cooltimeArray[5, index] = cool; a_cooltimeCodeArray[5, index] = code; }

    }

    public void G_Cooltimed(int index, int code, int cool)
    {
        if (CoolTimes.ContainsKey(code)) CoolTimes[code] += cool;
        else CoolTimes.Add(code, cool);

        if      (characterInfo.CurrentModule == characterInfo.CardModule1) { g_cooltimeArray[0, index] = cool; g_cooltimeCodeArray[0, index] = code; }
        else if (characterInfo.CurrentModule == characterInfo.CardModule2) { g_cooltimeArray[1, index] = cool; g_cooltimeCodeArray[1, index] = code; }
        else if (characterInfo.CurrentModule == characterInfo.CardModule3) { g_cooltimeArray[2, index] = cool; g_cooltimeCodeArray[2, index] = code; }
        else if (characterInfo.CurrentModule == characterInfo.CardModule4) { g_cooltimeArray[3, index] = cool; g_cooltimeCodeArray[3, index] = code; }
        else if (characterInfo.CurrentModule == characterInfo.CardModule5) { g_cooltimeArray[4, index] = cool; g_cooltimeCodeArray[4, index] = code; }
        else if (characterInfo.CurrentModule == characterInfo.CardModule6) { g_cooltimeArray[5, index] = cool; g_cooltimeCodeArray[5, index] = code; }

    }






    public void CooltimeStopper(int index)
    {
        for(int i=0;i<5;i++)
        {
            a_cooltimeCodeArray[index, i] = 0;
            g_cooltimeCodeArray[index, i] = 0;
                a_cooltimeArray[index, i] = 0;
                g_cooltimeArray[index, i] = 0;
              

        }
    }

    public void A_CooltimeReset(int modulenum, int index , int code)
    {
        if(CoolTimes.ContainsKey(code))
            if(CoolTimes[code] > 0)
            {
                a_cooltimeCodeArray[modulenum, index] = code;
                a_cooltimeArray[modulenum, index] = CoolTimes[code];
            }
    }

    #endregion

    #region 쿨타임 흐름
    public void Cooling()
    {
        for(int i=0; i<6; i++)
        {
            for(int j = 0; j<5; j++)
            {
                if (a_cooltimeCodeArray[i, j] != 0)
                {
                    a_cooltimeArray[i, j] -= 1;
                    CoolTimes[a_cooltimeCodeArray[i, j]] -= 1;
                    if(a_cooltimeArray[i,j] <= 0)
                    {
                        CoolTimes.Remove(a_cooltimeCodeArray[i,j]);
                        a_cooltimeCodeArray[i, j] = 0;
                        a_cooltimeArray[i, j] = 0;

                    }

                }
                if(g_cooltimeCodeArray[i,j] != 0)
                {
                    g_cooltimeArray[i, j] -= 1;
                    CoolTimes[g_cooltimeCodeArray[i, j]] -= 1;
                    if(g_cooltimeArray[i,j] <= 0)
                    {
                        CoolTimes.Remove(g_cooltimeCodeArray[i, j]);
                        g_cooltimeCodeArray[i, j] = 0;
                        g_cooltimeArray[i, j] = 0;
                    }


                }



            }



        }


    }



    #endregion

    #region 조건에 따라 쿨타임 제거
    public void CoolTimeDecrease(int code, int cool)
    {
        if (CoolTimes.ContainsKey(code))
        {
            CoolTimes[code] -= cool;
            if (CoolTimes[code] <= 0) CoolTimes.Remove(code);
        }
    }

    #endregion

    #region 쿨타임 걸렸는지 확인
    public bool GetIsCoolTime(int code)
    {
        return CoolTimes.ContainsKey(code);
    }

    public int GetCoolTime(int code)
    {
        if (CoolTimes.ContainsKey(code))
            return CoolTimes[code];
        else
            return 0;
    }


    #endregion


    #endregion


    #region

    public void ActiveTrait( int key)
    {
        traitList.Add(key);
    }

    public void DisActiveTrait( int key)
    {
        traitList.Remove(key);
    }

    public void InitializeTrait()
    {
        nimrod currentModule = resourceManager.NimrodDictionary[characterInfo.CurrentModule.module];
        for (int i = 0; i < currentModule.EssentialPassive.Count; i++)
        {
            traitList.Add(currentModule.EssentialPassive[i]);
           
        }
        for (int i = currentModule.EssentialPassive.Count; i < currentModule.maxPassiveTrait; i++)
        {
            traitList.Add(characterInfo.CurrentModule.PassiveTrait[i]);
        }

    }




    #endregion

    #region Module

    #region ChangeModule
    /*
    private void ModuleChange(int moduleindex, int index)
    {
        int code = 0;
        string Str_name;


        PlayerInfo.CardModule selectedModule;

        selectedModule = characterInfo.CardModule1;
        nimrod nim;

        switch (moduleindex)
        {
            case 1:
                selectedModule = characterInfo.CardModule1;
                break;
            case 2:
                selectedModule = characterInfo.CardModule2;
                break;
            case 3:
                selectedModule = characterInfo.CardModule3;
                break;
            case 4:
                selectedModule = characterInfo.CardModule4;
                break;
            case 5:
                selectedModule = characterInfo.CardModule5;
                break;
            case 6:
                selectedModule = characterInfo.CardModule6;
                break;
        }

        selectedModule.module = characterInfo.ModuleArray[index];
        nim = resourceManager.NimrodDictionary[selectedModule.module];




        for(int i=0; i<nim.EssentialAttack.Count; i++)
        {
            code = nim.EssentialAttack[i];
        }

        for(int i=0;i<nim.EssentialGuard.Count; i++)
        {
            code = nim.EssentialGuard[i];
        }


        for (int i = 0; i <nim.EssentialPassive.Count; i++)
        {
            code = nim.EssentialPassive[i];
            Str_name = resourceManager.PassiveDictionary[code].keyword;
            ActiveTrait(Str_name, code);
        }

        for(int i=0; i<selectedModule.PassiveTrait.Length; i++)
        {
            if(   code != 0)
            {
                code = selectedModule.PassiveTrait[i];
                Str_name = resourceManager.PassiveDictionary[code].keyword;
                ActiveTrait(Str_name, code);
            }
        }


    }
    */
    #endregion


    #region AddModule
    public void AddModule(int moduleNum)
    {

        for (int i = 0; i < characterInfo.ModuleArray.Length; i++)
        {
            if (int.Equals(characterInfo.ModuleArray[i], 0))
            {
                characterInfo.ModuleArray[i] = moduleNum;
                break;
            }
        }
    }



    #endregion



    #endregion
    #region ActionCard


    #region Attack

    private void CalculateAttackValue(int value)
    {
        attackDamage = value;
    }


    #endregion



    #endregion


    public PlayerInfo GetPlayerInfo()
    {
        return this.characterInfo;
    }

    public int CalculateResult(string stat)
    {
        trait calculdel;
        passiveCard pc;
        int bonus = 0;        

        for (int i = 0; i < traitList.Count; i++)
        {
            pc = resourceManager.PassiveDictionary[traitList[i]];
            if (pc.keyword != "statBonus") continue;

            calculdel = CalculDict[pc.calculType];
            
            if(calculdel(pc.condition, pc.value, stat, pc.type))
            {
                bonus += pc.value;
                bonusExplainList.Add(pc.Explain);
            }

        }
        if(eventStat == stat)
        {
            bonus += eventBonus;
        }
              
        eventBonus = 0;

        return bonus;
    }

    public void EventBonus(string bonus,string stat ,int value)
    {
        bonusExplainList.Add(bonus);
        eventBonus += value;
        eventStat = stat;
    }
    

    private void Awake()
    {
        hpMore = More_hp;
        hpLess = less_hp;
        CalculDict.Add("hpMore", hpMore);
        CalculDict.Add("hpLess", hpLess);
    }

    private void Start()
    {
        //TraitDictionary.Add("hmorep_1", false);

        resourceManager = GameManager.GetManagerClass<ResourceManager>();
        uiManager = GameManager.GetManagerClass<UIManager>();

    }










}

