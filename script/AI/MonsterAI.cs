using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonsterBase
{
    public override void Action(int num = 0, int result = 0)
    {
        Debug.Log(string.Format("���� �ѹ� :{0}", currentBehaviour));
        switch (currentBehaviour)
        {
            case 0:

                //currentBehaviour = 1;
                battleManager.SetEnmeyCard(0,1,8,"������ Ÿ��", "Ÿ��", "");
                battleManager.SetEnmeyCard(1,1,8,"������ Ÿ��", "Ÿ��", "");
                battleManager.SetEnmeyCard(2,1,8,"������ Ÿ��", "Ÿ��", "");

                break;
            case 1:
                //battleManager.DamageToPlayer(15, "sting");
                battleManager.SetEnmeyCard(0, 1, 8, "������ Ÿ��", "Ÿ��", "");
                battleManager.SetEnmeyCard(1, 1, 8, "������ Ÿ��", "Ÿ��", "");
                battleManager.SetEnmeyCard(2, 1, 8, "������ Ÿ��", "Ÿ��", "");
                break;


            case 2:
               
                bisOption = true;
               
                currentBehaviour = 3;
                break;

            case 3:

                bisOption = false;
                if (num == 0)
                {
                    if (result >= 1)
                    {
                        logicManager.ButtonActive(0, 3, "����!", "���Ƴ´�");
                        currentBehaviour = 4;
                    }
                    else
                    {
                        logicManager.ButtonActive(0, 3, "����!", "������");
                        battleManager.DamageToPlayer(15, "sting");
                        currentBehaviour = 5;
                    }
                }
                else if (num == 1)
                {
                    if (result >= 1)
                    {
                        logicManager.ButtonActive(0, 3, "����!", "���ߴ�");
                        currentBehaviour = 4;
                    }
                    else
                    {
                        logicManager.ButtonActive(0, 3, "����!", "������");
                        battleManager.DamageToPlayer(15, "sting");
                        currentBehaviour = 5;
                    }
                }
                break;

            case 4:
               
                break;
            case 5:

               
                break;

            case 6:
              
                break;




        }



    }
}
