using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Febucci.UI;

public class BattleCard : MonoBehaviour, IEndDragHandler , IBeginDragHandler , IDragHandler
{
    float prev_X;
    float following_X;
   
    private enum direction {left, center, right};
    private direction curDirection = direction.center;

    [SerializeField] private List<EnemyCard>  EnemyCards  = new List<EnemyCard>();
    [SerializeField] private List<PlayerCard> PlayerCards = new List<PlayerCard>();
    private BattleManager battleManager;
    private UIManager uiManager;
    private Canvas can;

    [SerializeField] GameObject LeftSign;
    [SerializeField] GameObject RightSign;
    [SerializeField] GameObject Switch;


    private bool bisDuel = false;
    private int DuelNum = 0;

    private int[] matchResult  = new int[3];
    private int[] playerDamage = new int[3];
    private int[] enemyDamage  = new int[3];


    private void Start()
    {
        battleManager = GameManager.GetManagerClass<BattleManager>();
        uiManager = GameManager.GetManagerClass<UIManager>();
        uiManager.battleCard = this;
        can = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

   


    public void OnDrag(PointerEventData data)
    {

    }

    public void OnEndDrag(PointerEventData data)
    {
        following_X = data.position.x;
       
        DragObject();
    }

    public void OnBeginDrag(PointerEventData data)
    {
        prev_X = data.position.x;
       
    }

    
    public int SetPlayerCard(Sprite Card, Sprite DamageType, Sprite TestSype, Sprite Grade , string Name, string Explain, string tag)
    {
        int i_direction = 0;

        switch(curDirection)
        {
            case direction.left:
                i_direction = 0;
                PlayerCards[0].SetPlayerCard(Card, DamageType, TestSype, Grade, Name, Explain);
                break;
            case direction.center:
                i_direction = 1;
                PlayerCards[1].SetPlayerCard(Card, DamageType, TestSype, Grade, Name, Explain);
                break;
            case direction.right:
                i_direction = 2;
                PlayerCards[2].SetPlayerCard(Card, DamageType, TestSype, Grade, Name, Explain);
                break;
        }

        return i_direction;
    }

    public void UnSetPlayerCard(Sprite image, int index)
    {
        PlayerCards[index].UnSetPlayerCard(image);
    }
    

    public void SetEnemyCard(int index , Sprite damageType, string name, string explain)
    {
        EnemyCards[index].SetEnemyCard(damageType,name,explain);
    }


    public void DuelSequence()
    {
        bool bisShot = true;
        bool bisRound = false;
        DuelNum += 1;
        battleManager.DataSend(out matchResult[0], out matchResult[1], out matchResult[2] , out playerDamage[1] , out playerDamage[2] , out playerDamage[2] , out enemyDamage[1] , out enemyDamage[2] , out enemyDamage[2]);

        IEnumerator waitFor(int number , int result)
        {
            WaitForSeconds wait = new WaitForSeconds(0.8f);

            yield return wait;

            PlayerCards[number].transform.parent = can.transform;
            EnemyCards[number].transform.parent = can.transform;

            EnemyCards[number].SetPosition();
            PlayerCards[number].SetPosition();


            PlayerCards[number].ActionMove(matchResult[number],playerDamage[number]);
            EnemyCards[number].ActionMove(matchResult[number],enemyDamage[number]);



        }


        IEnumerator waitSpin()
        {
            WaitForSeconds wait = new WaitForSeconds(0.2f);
            yield return wait; 
        }

        switch (DuelNum)
        {
            


            case 1:



                Switch.gameObject.SetActive(false);
                bisDuel = true;

                switch (curDirection)
                {
                    case direction.left:


                        break;

                    case direction.center:

                        EnemyCards[0].ChangeState(2, bisRound);
                        EnemyCards[1].ChangeState(3, bisRound);
                        EnemyCards[2].ChangeState(0, bisRound);


                        PlayerCards[0].ChangeState(2, bisRound);
                        PlayerCards[1].ChangeState(3, bisRound);
                        PlayerCards[2].ChangeState(0, bisRound);

                        LeftSign.gameObject.SetActive(false);
                        RightSign.gameObject.SetActive(true);
                        break;

                    case direction.right:

                        EnemyCards[0].ChangeState(-1, bisRound);
                        EnemyCards[1].ChangeState(2, bisRound);
                        EnemyCards[2].ChangeState(3, bisRound);


                        PlayerCards[0].ChangeState(-1, bisRound);
                        PlayerCards[1].ChangeState(2, bisRound);
                        PlayerCards[2].ChangeState(3, bisRound);


                        LeftSign.gameObject.SetActive(true);
                        RightSign.gameObject.SetActive(true);

                        waitSpin();

                        EnemyCards[0].ChangeState(2, bisRound);
                        EnemyCards[1].ChangeState(3, bisRound);
                        EnemyCards[2].ChangeState(0, bisRound);


                        PlayerCards[0].ChangeState(2, bisRound);
                        PlayerCards[1].ChangeState(3, bisRound);
                        PlayerCards[2].ChangeState(0, bisRound);

                        LeftSign.gameObject.SetActive(false);
                        RightSign.gameObject.SetActive(true);

                        break;
                }

                


                

                StartCoroutine(waitFor(0, 0));



                break;

            case 2:

                PlayerCards[0].transform.parent = this.transform;
                EnemyCards[0].transform.parent = this.transform;
            


                EnemyCards[0].ChangeState(1, bisShot);
                EnemyCards[1].ChangeState(2, bisShot);
                EnemyCards[2].ChangeState(4, bisShot);


                PlayerCards[0].ChangeState(1, bisShot);
                PlayerCards[1].ChangeState(2, bisShot);
                PlayerCards[2].ChangeState(4, bisShot);

                LeftSign.gameObject.SetActive(true);
                RightSign.gameObject.SetActive(true);

                


               
                StartCoroutine(waitFor(1, 0));
                break;
            case 3:

                PlayerCards[1].transform.parent = this.transform;
                EnemyCards[1].transform.parent = this.transform;



                EnemyCards[0].ChangeState(0, bisShot);
                EnemyCards[1].ChangeState(1, bisShot);
                EnemyCards[2].ChangeState(2, bisShot);

                PlayerCards[0].ChangeState(0, bisShot);
                PlayerCards[1].ChangeState(1, bisShot);
                PlayerCards[2].ChangeState(2, bisShot);


                LeftSign.gameObject.SetActive(true);
                RightSign.gameObject.SetActive(false);



              

                StartCoroutine(waitFor(2, 0));
                break;

            case 4:
                PlayerCards[2].transform.parent = this.transform;
                EnemyCards[2].transform.parent = this.transform;
                //PlayerCards[2].resetPos();
                //EnemyCards[2].ResetPosition();


                EnemyCards[0].ChangeState(-1, bisDuel);
                EnemyCards[1].ChangeState(2, bisDuel);
                EnemyCards[2].ChangeState(3, bisDuel);


                PlayerCards[0].ChangeState(-1, bisDuel);
                PlayerCards[1].ChangeState(2, bisDuel);
                PlayerCards[2].ChangeState(3, bisDuel);

                LeftSign.gameObject.SetActive(true);
                RightSign.gameObject.SetActive(true);

                battleManager.ResetSettedCard();
                Switch.gameObject.SetActive(true);

                DuelNum = 0;
                bisDuel = false;
                curDirection = direction.center;
                break;

        }

      
    }

    




    private void DragObject()
    {
        if (bisDuel) return;
        

        if(prev_X > following_X )
        {
            switch(curDirection)
            {
                case direction.center:
                    curDirection = direction.right;
                 

                    EnemyCards[0].ChangeState(0, bisDuel);
                    EnemyCards[1].ChangeState(1, bisDuel);
                    EnemyCards[2].ChangeState(2, bisDuel);

                    PlayerCards[0].ChangeState(0, bisDuel);
                    PlayerCards[1].ChangeState(1, bisDuel);
                    PlayerCards[2].ChangeState(2, bisDuel);

                    RightSign.gameObject.SetActive(false);
                    break;
                    

                case direction.left:
                   
                    curDirection = direction.center;
                  
                    EnemyCards[0].ChangeState(1, bisDuel);
                    EnemyCards[1].ChangeState(2, bisDuel);
                    EnemyCards[2].ChangeState(4, bisDuel);


                    PlayerCards[0].ChangeState(1, bisDuel);
                    PlayerCards[1].ChangeState(2, bisDuel);
                    PlayerCards[2].ChangeState(4, bisDuel);

                    LeftSign.gameObject.SetActive(true);
                    RightSign.gameObject.SetActive(true);
                    break;

            }

        }
        else if(prev_X < following_X)
        {
            switch (curDirection)
            {
                case direction.center:
                   
                    curDirection = direction.left;
                   
                    EnemyCards[0].ChangeState(2, bisDuel);
                    EnemyCards[1].ChangeState(3, bisDuel);
                    EnemyCards[2].ChangeState(0, bisDuel);


                    PlayerCards[0].ChangeState(2, bisDuel);
                    PlayerCards[1].ChangeState(3, bisDuel);
                    PlayerCards[2].ChangeState(0, bisDuel);

                    LeftSign.gameObject.SetActive(false);
                    break;

                case direction.right:
                  
                    curDirection = direction.center;
                  
                    EnemyCards[0].ChangeState(-1, bisDuel);
                    EnemyCards[1].ChangeState(2, bisDuel);
                    EnemyCards[2].ChangeState(3, bisDuel);


                    PlayerCards[0].ChangeState(-1, bisDuel);
                    PlayerCards[1].ChangeState(2, bisDuel);
                    PlayerCards[2].ChangeState(3, bisDuel);


                    LeftSign.gameObject.SetActive(true);
                    RightSign.gameObject.SetActive(true);

                    

                    break;

            }
        }
    }

}
