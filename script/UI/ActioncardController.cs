using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;




public class ActioncardController : MonoBehaviour
{
    
    private List<ActionCard> ActionCardList = new List<ActionCard>();
    private List<QuizSlot> quizSlotList = new List<QuizSlot>(); 
    private LogicManager logicManager;
    private ResourceManager resourceManager;

    [SerializeField] private ActionCard Sample;


    
   
    [SerializeField] private QuizSlot   quizSample;

    [SerializeField] private GameObject Content;
  

     private Canvas can;

   

    private ObjectPool<ActionCard> _cardPool = null;
    private ObjectPool<QuizSlot> _quizPool = null;

    #region 카드 생성

    public void AddCard(int situation, int index, string nameText, string explainText , int option = 1)
    {
         ActionCard newCard = _cardPool.GetRecyclableObject() ??
          _cardPool.RegisterRecyclableObject(Instantiate(Sample));
                                       
        newCard.transform.parent = Content.transform;
        newCard.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        newCard.index = index;
        newCard.logicManager = this.logicManager;
        newCard.actionCardCon = this;
        Sprite curBackSprite = resourceManager.I_ActionCardDictionary["SelectCard"];
       
        if (newCard.gameObject.activeSelf)
            newCard.isActive = true;
        else
            newCard.gameObject.SetActive(newCard.isActive = true);

        newCard.canvas = can;

        newCard.InitializeCard(curBackSprite,null,null,null,nameText,explainText,situation,option);       
        ActionCardList.Add(newCard);
    }

  
    #endregion,option:option
  
    public void AddCard(int index, string nameText ,string grade , string damageType, string testType, string Explain, int option =1)
    {
        ActionCard newCard = _cardPool.GetRecyclableObject() ??
            _cardPool.RegisterRecyclableObject(Instantiate(Sample));

        newCard.transform.parent = Content.transform;
        newCard.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        newCard.index = index;
        newCard.logicManager = this.logicManager;
        newCard.actionCardCon = this;
        string damage = "";
        switch (damageType)
        {
            case "절단":
                damage = "Cut";
                break;
            case "관통":
                damage = "Pierece";
                break;
            case "타격":
                damage = "Hit";
                break;
        }


        Sprite curBackSprite = resourceManager.I_ActionCardDictionary["AttackCard"];
        Sprite curDamageType = resourceManager.I_ActionCardDictionary[damage];
        Sprite curTestIcon   = resourceManager.I_ActionCardDictionary[testType];
        Sprite curGrade       = resourceManager.I_ActionCardDictionary[grade];

        if (newCard.gameObject.activeSelf)
            newCard.isActive = true;
        else
            newCard.gameObject.SetActive(newCard.isActive = true);

        newCard.canvas = can;

        newCard.InitializeCard(curBackSprite, curGrade, curTestIcon, curDamageType, nameText, Explain, 1, option);
        ActionCardList.Add(newCard);

    }


    #region 퀴즈 슬롯 생성

    public void AddQuizSlot(int index, Item item)
    {


       
        QuizSlot newQuiz = _quizPool.GetRecyclableObject() ??
            _quizPool.RegisterRecyclableObject(Instantiate(quizSample));

    

        newQuiz.transform.parent = Content.transform;
        newQuiz.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        newQuiz.Index = index;
        newQuiz.logicManager = this.logicManager;
        newQuiz.actionCardCon = this;
      
        if (newQuiz.gameObject.activeSelf)
            newQuiz.isActive = true;
        else
            newQuiz.gameObject.SetActive(newQuiz.isActive = true);

        newQuiz.canvas = can;
       
        newQuiz.InitializeCard(index, item);
        quizSlotList.Add(newQuiz);

    }

    #endregion

    #region  모두 제거

    public void RemoveAll()
    {
        for(int i=0; i<ActionCardList.Count; i++)
        {
            ActionCardList[i].gameObject.SetActive(false);
        }

        //ActionCardList.RemoveRange(0, ActionCardList.Count);
        ActionCardList.Clear();
    }


    public void RemoveQuizAll()
    {
        for(int i=0;i<quizSlotList.Count;i++)
        {
            quizSlotList[i].gameObject.SetActive(false);
        }

    }

    public void ActiveQuizAll()
    {
        for (int i = 0; i < quizSlotList.Count; i++)
        {
            quizSlotList[i].gameObject.SetActive(true);
        }
    }

    #endregion

    #region 동시에 못쓰게 하기위해 하나가 움직이는 동안 나머지 멈춤 
    public void stunAll(int index)
    {
        for(int i=0; i < ActionCardList.Count;i++)
        {
            if (i == index) continue;
            ActionCardList[i].Stun();
            
        }
    }

    public void EndStunAll()
    {
        for (int i = 0; i < ActionCardList.Count; i++) ActionCardList[i].EndStun();        
    }


    #endregion


    public void SetCoolTime(int index)
    {
       // Debug.Log(string.Format("아니 분명 {0}번째 카드라고 명령을 줬다니까????",index));
        ActionCardList[index].CoolTimed();
    }


    public void CoolingUI(int index ,int cool, int maxCool = 1)
    {
        ActionCardList[index].Cooling(cool, maxcool:maxCool);
    }

    

    public void RemoveCard(int index)
    {
        int ind = 0;
        while(ind < ActionCardList.Count)
        {
            if (ActionCardList[ind].index == index && ActionCardList[ind].gameObject.activeSelf == true)
            {
                ActionCardList[ind].gameObject.SetActive(false);
                break;
            }          
            ind += 1;
        }

       // ActionCardList[index].gameObject.SetActive(false);
        for (int i = 0; i < ActionCardList.Count; i++) ActionCardList[i].AdjustIndex(index);               
    }


    // Start is called before the first frame update
    void Start()
    {

        logicManager = GameManager.GetManagerClass<LogicManager>();
        logicManager.ActionCardcon = this;
        resourceManager = GameManager.GetManagerClass<ResourceManager>();
        
        _cardPool = new ObjectPool<ActionCard>();
        _quizPool = new ObjectPool<QuizSlot>();
        can = GameObject.Find("Canvas").GetComponent<Canvas>();
     
        transform.SetParent(can.transform);
        transform.localScale = new Vector3(1,1,1);
        transform.localPosition = new Vector3(-52,-679,0);
        
      //  gameObject.SetActive(false);
    }

    
}
