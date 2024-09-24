using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
public class DIseaseTab : MonoBehaviour
{
    public GameManager gameManager { get { return GameManager.gameManager; } }

    private List<DiseaseSlot> Slots = new List<DiseaseSlot>();
    [SerializeField] private DiseaseExplain diseaseExplain;
    [SerializeField] private DiseaseExplain EffectExplain;
    public CharacterManager characterManager { get; set; }
    private ResourceManager resourceManager { get; set; }
    private UIManager uiManager;

    private PlayerInfo characterInfo;

    private Sprite TempSprite;
    private int tempIndex;

   // [SerializeField] private Sprite s;


    


    public void ActiveExplain(int index)
    {
        int value = 0;
        int turn = 0;
        string diseaseLine = "";
        string name;
        string explain = "";
        string stat = "";

        Disease disease = characterInfo.DiseaseArray[index].disease;
        name = disease.DiseaseName;

        if (characterInfo.DiseaseArray[index].DiseaseCode == 0) return;


        if(characterInfo.DiseaseArray[index].disease.isTurnDm)
        {
            turn = characterInfo.DiseaseArray[index].DiseaseTurn;
            value   = characterInfo.DiseaseArray[index].t_DiseaseValue;
            explain = disease.red_DiseaseExplain;
            diseaseLine = string.Format("<color=yellow>{0}</color> 턴 동안 매 턴 마다 {1} <color=red>{2}</color> 피해", turn, explain, value);
        }

        if(characterInfo.DiseaseArray[index].disease.isEndDm)
        {
            turn = characterInfo.DiseaseArray[index].DiseaseTurn;
            value = characterInfo.DiseaseArray[index].e_DiseaseValue;
            explain = disease.yellow_DiseaseExplain;
            diseaseLine = diseaseLine == "" ? string.Format("<color=yellow>{0}</color> 턴 후 {1} <color=red>{2}</color> 피해", turn, explain, value)
                : diseaseLine + string.Format("\n<color=yellow>{0}</color> 턴 후 {1} <color=red>{2}</color> 피해", turn, explain, value);

        }
        if(characterInfo.DiseaseArray[index].disease.isStat1)
        {
            turn  = characterInfo.DiseaseArray[index].DiseaseTurn;
            value = disease.s_PenaltyValue1;
            stat  = disease.statType1;
                   
            
            if (turn > 0)
                diseaseLine = diseaseLine == "" ? string.Format("<color=yellow>{0}</color> 턴 동안  <color=green>{1}</color> 이 <color=red>{2}</color> 만큼 감소", turn,stat, value)
               : diseaseLine + string.Format("\n<color=yellow>{0}</color> 턴 동안  <color=green>{1}</color> 이 <color=red>{2}</color> 만큼 감소", turn, stat, value);


            else
                diseaseLine = diseaseLine == "" ? string.Format("<color=green>{0}</color> 이  <color=red>{1}</color> 만큼 감소", stat, value)
               : diseaseLine + string.Format("\n<color=green>{0}</color> 이  <color=red>{1}</color> 만큼 감소", stat, value);




        }

        if(characterInfo.DiseaseArray[index].disease.isStat2)
        {
            turn  = characterInfo.DiseaseArray[index].DiseaseTurn;
            value = disease.s_PenaltyValue2;
            stat  = disease.statType2;


            if (turn > 0)
                diseaseLine = diseaseLine == "" ? string.Format("<color=yellow>{0}</color> 턴 동안  <color=green>{1}</color> 이 <color=red>{2}</color> 만큼 감소", turn, stat, value)
               : diseaseLine + string.Format("\n<color=yellow>{0}</color> 턴 동안  <color=green>{1}</color> 이 <color=red>{2}</color> 만큼 감소", turn, stat, value);


            else
                diseaseLine = diseaseLine == "" ? string.Format("<color=green>{0}</color> 이  <color=red>{1}</color> 만큼 감소", stat, value)
               : diseaseLine + string.Format("\n<color=green>{0}</color> 이  <color=red>{1}</color> 만큼 감소", stat, value);
        }




        diseaseExplain.gameObject.SetActive(true);
        diseaseExplain.transform.position = new Vector3(Slots[index].transform.position.x + 0.2f, Slots[index].transform.position.y- 0.7f, Slots[index].transform.position.z);        
        diseaseExplain.ActiveExplain(name,diseaseLine);
       
    }

   

    public void DeactiveExplain()
    {
        diseaseExplain.gameObject.SetActive(false);
    }

    

    private void Awake()
    {
        Transform SlotGroup = GameObject.Find("slotlist").transform;
        
        foreach (Transform slot in SlotGroup)
        {
            if (slot.tag == "DiseaseSlot")
            {
               
                DiseaseSlot s = slot.GetComponent<DiseaseSlot>();
                
                Slots.Add(s);
                s.diseaseTab = this;
            }
        }

       

        
    }


   

    private void Start()
    {
        characterManager = GameManager.GetManagerClass<CharacterManager>();
        characterManager.diseaseTab = this;
        uiManager = GameManager.GetManagerClass<UIManager>();
        uiManager.diseaseTab = this;
        resourceManager = GameManager.GetManagerClass<ResourceManager>();
        //characterInfo = characterManager.characterInfo;
        transform.SetParent(GameObject.Find("Canvas").transform);
        transform.localScale = new Vector3(1,1,1);
        transform.localPosition = new Vector3(-736,303,0);

        resourceManager.GetPlayerInfo(out characterInfo);
        diseaseExplain.gameObject.SetActive(false);
        

    }


    public void GetDiseaseUI(int index , Sprite sprite)
    {
        Slots[index].DiseaseSprite = sprite;
        TempSprite = sprite;
        Slots[index].Infected();      
        tempIndex = index;
    }

    public void EndDiseaseUI(int index, Sprite sprite)
    {
        Slots[index].DiseaseSprite = sprite;
        Slots[index].SetImage();
    }


  
    public void AdjustDiseaseUI(int index, Sprite sprite)
    {
        Slots[index].SetImage(sprite);
    }


    
}
