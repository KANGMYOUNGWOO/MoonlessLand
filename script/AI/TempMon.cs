using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempMon : MonsterBase
{ 
    
    public override void Action(int num =0 , int result =0)
    {
        Debug.Log(string.Format("현재 넘버 :{0}",currentBehaviour));
        switch(currentBehaviour)
        {
            case 0:
                
                currentBehaviour = 1;
                break;
            case 1:
                //battleManager.DamageToPlayer(15, "sting");
                logicManager.GuardFunction();
                currentBehaviour = 2;
                break;

                         
            case 2:
                Debug.Log("case 2");
                bisOption = true;
                
                currentBehaviour = 3;
                break;
              
            case 3:

                bisOption = false;
                if (num == 0)
                {
                    if (result >= 1)
                    {
                        logicManager.ButtonActive(0, 3, "성공!", "막아냈다");
                        currentBehaviour = 4;
                    }
                    else
                    {
                        logicManager.ButtonActive(0, 3, "실패!", "아프다");
                        battleManager.DamageToPlayer(15, "sting");
                        currentBehaviour = 5;
                    }
                }
                else if (num == 1)
                {
                    if (result >= 1)
                    {
                        logicManager.ButtonActive(0, 3, "성공!", "피했다");
                        currentBehaviour = 4;
                    }
                    else
                    {
                        logicManager.ButtonActive(0, 3, "실패!", "아프다");
                        battleManager.DamageToPlayer(15, "sting");
                        currentBehaviour = 5;
                    }
                }
                break;
               
            case 4:
                currentBehaviour = 6;
                Action();
                break;
            case 5:

                currentBehaviour = 6;
                Action();
                break;

            case 6:
                currentBehaviour = 1;
                logicManager.BattleFunction2();
                break;




        }

        

    }

}
