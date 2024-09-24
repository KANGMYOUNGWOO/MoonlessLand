using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MedicineTab : MonoBehaviour
{
    public GameManager gameManager { get { return GameManager.gameManager; } }

    [SerializeField] private List<MedicineSlot> Slots = new List<MedicineSlot>();
    [SerializeField] private Image ItemImage;
    [SerializeField] private TextMeshProUGUI ItemName;
    [SerializeField] private TextMeshProUGUI ItemCompnay;
    [SerializeField] private TextMeshProUGUI ItemExplain;
    [SerializeField] private MedicineRejection _rejection;

    private PlayerInfo playerInfo;
    private ResourceManager resourceManager;
    private UIManager uiManager;
    private CharacterManager characterManager;
    private int ItemCode;
    private string ItemCompany;
    private bool bisCancle;
    private bool bisselcted;
    private bool bisProduction = false;

    private void Awake()
    {
        
    }

    private void Start()
    {
        bisselcted = false;
        resourceManager = GameManager.GetManagerClass<ResourceManager>();
        uiManager = GameManager.GetManagerClass<UIManager>();
        characterManager = GameManager.GetManagerClass<CharacterManager>();
        uiManager.medicineTab = this;
        for(int i=0;i<Slots.Count;i++)
        {
            Slots[i].medicineTab = this;
        }
        resourceManager.GetPlayerInfo(out playerInfo);
        transform.SetParent(GameObject.Find("Canvas").transform);
        transform.localScale = new Vector3(1,1,1);
        transform.localPosition = new Vector3(-109.5f,141,0);
        gameObject.SetActive(false);
        
    }

    


    public void InitializeSlot()
    {
        string t_diseaseName = "";
        string t_diseaseExplain = "";
        Sprite t_diseaseSprite = null;
        string strindex = "green";
        Disease disease = null;
        int code = 0;

        for (int i = 0; i < playerInfo.DiseaseArray.Length; i++)
        {
            t_diseaseName = "";
            t_diseaseExplain = "";
            t_diseaseSprite = null;
            strindex = "green";
            disease = null;
            code = 0;

            if (playerInfo.DiseaseArray[i].DiseaseCode != 0)
            {
                disease = playerInfo.DiseaseArray[i].disease;
                code = playerInfo.DiseaseArray[i].DiseaseCode;

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

                t_diseaseName = disease.DiseaseName;
                t_diseaseSprite = resourceManager.I_DiseaseDictionary[strindex];
                t_diseaseExplain = ActiveExplain(i);

            }
           
            Slots[i].InitializeSlot(t_diseaseSprite, t_diseaseName, t_diseaseExplain);
        }
       
    }


    private string ActiveExplain(int index)
    {
        int value = 0;
        int turn = 0;
        string diseaseLine = "";
        string name;
        string explain = "";
        string stat = "";
        bisselcted = false;

        Disease disease = playerInfo.DiseaseArray[index].disease;
        name = disease.DiseaseName;

        if (playerInfo.DiseaseArray[index].disease.isTurnDm)
        {
            turn = playerInfo.DiseaseArray[index].DiseaseTurn;
            value = playerInfo.DiseaseArray[index].t_DiseaseValue;
            explain = disease.red_DiseaseExplain;
            diseaseLine = string.Format("<color=yellow>{0}</color> 턴 동안 매 턴 마다 {1} <color=red>{2}</color> 피해", turn, explain, value);
        }

        if (playerInfo.DiseaseArray[index].disease.isEndDm)
        {
            turn = playerInfo.DiseaseArray[index].DiseaseTurn;
            value = playerInfo.DiseaseArray[index].e_DiseaseValue;
            explain = disease.yellow_DiseaseExplain;
            diseaseLine = diseaseLine == "" ? string.Format("<color=yellow>{0}</color> 턴 후 {1} <color=red>{2}</color> 피해", turn, explain, value)
                : diseaseLine + string.Format("\n<color=yellow>{0}</color> 턴 후 {1} <color=red>{2}</color> 피해", turn, explain, value);

        }
        if (playerInfo.DiseaseArray[index].disease.isStat1)
        {
            turn = playerInfo.DiseaseArray[index].DiseaseTurn;
            value = disease.s_PenaltyValue1;
            stat = disease.statType1;

            if (turn > 0)
                diseaseLine = diseaseLine == "" ? string.Format("<color=yellow>{0}</color> 턴 동안  <color=green>{1}</color> 이 <color=red>{2}</color> 만큼 감소", turn, stat, value)
               : diseaseLine + string.Format("\n<color=yellow>{0}</color> 턴 동안  <color=green>{1}</color> 이 <color=red>{2}</color> 만큼 감소", turn, stat, value);

            else
                diseaseLine = diseaseLine == "" ? string.Format("<color=green>{0}</color> 이  <color=red>{1}</color> 만큼 감소", stat, value)
               : diseaseLine + string.Format("\n<color=green>{0}</color> 이  <color=red>{1}</color> 만큼 감소", stat, value);

        }

        if (playerInfo.DiseaseArray[index].disease.isStat2)
        {
            turn = playerInfo.DiseaseArray[index].DiseaseTurn;
            value = disease.s_PenaltyValue2;
            stat = disease.statType2;

            if (turn > 0)
                diseaseLine = diseaseLine == "" ? string.Format("<color=yellow>{0}</color> 턴 동안  <color=green>{1}</color> 이 <color=red>{2}</color> 만큼 감소", turn, stat, value)
               : diseaseLine + string.Format("\n<color=yellow>{0}</color> 턴 동안  <color=green>{1}</color> 이 <color=red>{2}</color> 만큼 감소", turn, stat, value);

            else
                diseaseLine = diseaseLine == "" ? string.Format("<color=green>{0}</color> 이  <color=red>{1}</color> 만큼 감소", stat, value)
               : diseaseLine + string.Format("\n<color=green>{0}</color> 이  <color=red>{1}</color> 만큼 감소", stat, value);
        }

        return diseaseLine;
    }

    public void ActiveMedTab(Sprite item, string itemName, string itemCompnay , int itemCode)
   {
        string ExplainText = "";

        ItemImage.sprite = item;
        ItemName.text    = itemName;
        ItemCompnay.text = itemCompnay;
        ItemCode = itemCode;

        switch(itemCompnay)
        {
            case "안신":
                ExplainText = "치료한 <color=red>질병</color>에 <color=yellow>2</color>턴 면역";
                break;
            case "리겔":
                ExplainText = "<color=green>약</color>의 등급이 <color=red>질병</color>의 등급보다 높으면\n추가 효과";
                break;
            case "해울":
                ExplainText = "두 개의 <color=red>질병</color>을 치료";
                break;
            case "미르":
                ExplainText = "<color=green>약</color>의 등급이 <color=red>질병</color>의 등급보다 낮아도\n<color=blue>소소한 효과</color>";
                break;
            case "길모어":
                ExplainText = "<color=cyan>정신력</color>을 회복한다";
                break;                
        }
       
      InitializeSlot();
   }

    public void SelectSlot(int index)
    {
        if (bisselcted) return;
       
        if (playerInfo.DiseaseArray[index].DiseaseCode == 0) return;
        if(playerInfo.DiseaseArray[index].DiseaseCode / 1000 <= ItemCode / 1000)
        {
            
            int diseasetype = (playerInfo.DiseaseArray[index].DiseaseCode % 1000) / 100;
            int medicinetype = (ItemCode % 1000) / 100;

            if (diseasetype == medicinetype)
            {
                bisselcted = true;
                switch (ItemCompany)
                {
                    case "안신":
                        Debug.Log("안신");
                        break;
                    case "리겔":
                        if (playerInfo.DiseaseArray[index].DiseaseCode / 1000 < ItemCode / 1000)
                        {

                        }
                        break;
                    case "해울":
                        Debug.Log("해울");
                        break;
                    case "길모어":
                        Debug.Log("길모어");
                        break;

                }

                Slots[index].CureDisease();
                characterManager.EndDisease(index);
                characterManager.RemoveItem(ItemCode,1);
                bisCancle = true;
               
            }
            else
            {
                _rejection.Apear("이 <color=red>병</color>에 들지 않는 <color=green>약</color>이다.<?>MRDisapear ");
            }

        }
        else
        {
            if(ItemCompany != "미르") 
{
                _rejection.Apear("<color=green>약</color>이 이 <color=red>병</color>을 치료하기엔 약하다<?>MRDisapear ");
                
            }
            else
            {
                Debug.Log("미르");
                bisselcted = true;
            }
        }
    }


    public void EndProduction()
    {
        if (bisProduction)
        {
            bisProduction = false;
            uiManager.Prologues("ItemTutorial4");
        }
        bisCancle = false;
        gameObject.SetActive(false);
    }

    public void tutorial()
    {
        bisProduction = true;
    }

}