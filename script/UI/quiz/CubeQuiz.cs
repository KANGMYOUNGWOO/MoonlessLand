using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeQuiz : MonoBehaviour
{
    // Start is called before the first frame update

    private LogicManager logicManager;

    #region List
    private List<Cube> BlueList;
    private List<Cube> GreenList;
    private List<Cube> RedList;
    #endregion

    #region Variables

    private int blueCount = 0;
    private int greenCount = 0;
    private int redCount = 0; 




    #endregion

    void Start()
    {
        logicManager = GameManager.GetManagerClass<LogicManager>();
    }

    public void ChangeCount(int blue, int green, int red)
    {
        
        /*
           cube.setcolor();
           인수 0 비활성화  
           인수 1 활성화 시키기
           인수 2 갯수 빼기
        */
        for (int i = blueCount; i < 6; i++)
        {
            BlueList[i].SetColor(1);
        }

        for(int i = greenCount; i < 6; i++)
        {
            GreenList[i].SetColor(1);
        }

        for(int i = redCount; i < 6; i++)
        {
            RedList[i].SetColor(1);
        }

        blueCount += blue;
        greenCount += green;
        redCount += red;

        redCount   = Mathf.Clamp(redCount, 0, 6);
        blueCount  = Mathf.Clamp(blueCount, 0, 6);
        greenCount = Mathf.Clamp(greenCount, 0, 6);

        
      
    }


    
}
