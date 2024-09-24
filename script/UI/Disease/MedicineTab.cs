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
            diseaseLine = string.Format("<color=yellow>{0}</color> �� ���� �� �� ���� {1} <color=red>{2}</color> ����", turn, explain, value);
        }

        if (playerInfo.DiseaseArray[index].disease.isEndDm)
        {
            turn = playerInfo.DiseaseArray[index].DiseaseTurn;
            value = playerInfo.DiseaseArray[index].e_DiseaseValue;
            explain = disease.yellow_DiseaseExplain;
            diseaseLine = diseaseLine == "" ? string.Format("<color=yellow>{0}</color> �� �� {1} <color=red>{2}</color> ����", turn, explain, value)
                : diseaseLine + string.Format("\n<color=yellow>{0}</color> �� �� {1} <color=red>{2}</color> ����", turn, explain, value);

        }
        if (playerInfo.DiseaseArray[index].disease.isStat1)
        {
            turn = playerInfo.DiseaseArray[index].DiseaseTurn;
            value = disease.s_PenaltyValue1;
            stat = disease.statType1;

            if (turn > 0)
                diseaseLine = diseaseLine == "" ? string.Format("<color=yellow>{0}</color> �� ����  <color=green>{1}</color> �� <color=red>{2}</color> ��ŭ ����", turn, stat, value)
               : diseaseLine + string.Format("\n<color=yellow>{0}</color> �� ����  <color=green>{1}</color> �� <color=red>{2}</color> ��ŭ ����", turn, stat, value);

            else
                diseaseLine = diseaseLine == "" ? string.Format("<color=green>{0}</color> ��  <color=red>{1}</color> ��ŭ ����", stat, value)
               : diseaseLine + string.Format("\n<color=green>{0}</color> ��  <color=red>{1}</color> ��ŭ ����", stat, value);

        }

        if (playerInfo.DiseaseArray[index].disease.isStat2)
        {
            turn = playerInfo.DiseaseArray[index].DiseaseTurn;
            value = disease.s_PenaltyValue2;
            stat = disease.statType2;

            if (turn > 0)
                diseaseLine = diseaseLine == "" ? string.Format("<color=yellow>{0}</color> �� ����  <color=green>{1}</color> �� <color=red>{2}</color> ��ŭ ����", turn, stat, value)
               : diseaseLine + string.Format("\n<color=yellow>{0}</color> �� ����  <color=green>{1}</color> �� <color=red>{2}</color> ��ŭ ����", turn, stat, value);

            else
                diseaseLine = diseaseLine == "" ? string.Format("<color=green>{0}</color> ��  <color=red>{1}</color> ��ŭ ����", stat, value)
               : diseaseLine + string.Format("\n<color=green>{0}</color> ��  <color=red>{1}</color> ��ŭ ����", stat, value);
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
            case "�Ƚ�":
                ExplainText = "ġ���� <color=red>����</color>�� <color=yellow>2</color>�� �鿪";
                break;
            case "����":
                ExplainText = "<color=green>��</color>�� ����� <color=red>����</color>�� ��޺��� ������\n�߰� ȿ��";
                break;
            case "�ؿ�":
                ExplainText = "�� ���� <color=red>����</color>�� ġ��";
                break;
            case "�̸�":
                ExplainText = "<color=green>��</color>�� ����� <color=red>����</color>�� ��޺��� ���Ƶ�\n<color=blue>�Ҽ��� ȿ��</color>";
                break;
            case "����":
                ExplainText = "<color=cyan>���ŷ�</color>�� ȸ���Ѵ�";
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
                    case "�Ƚ�":
                        Debug.Log("�Ƚ�");
                        break;
                    case "����":
                        if (playerInfo.DiseaseArray[index].DiseaseCode / 1000 < ItemCode / 1000)
                        {

                        }
                        break;
                    case "�ؿ�":
                        Debug.Log("�ؿ�");
                        break;
                    case "����":
                        Debug.Log("����");
                        break;

                }

                Slots[index].CureDisease();
                characterManager.EndDisease(index);
                characterManager.RemoveItem(ItemCode,1);
                bisCancle = true;
               
            }
            else
            {
                _rejection.Apear("�� <color=red>��</color>�� ���� �ʴ� <color=green>��</color>�̴�.<?>MRDisapear ");
            }

        }
        else
        {
            if(ItemCompany != "�̸�") 
{
                _rejection.Apear("<color=green>��</color>�� �� <color=red>��</color>�� ġ���ϱ⿣ ���ϴ�<?>MRDisapear ");
                
            }
            else
            {
                Debug.Log("�̸�");
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