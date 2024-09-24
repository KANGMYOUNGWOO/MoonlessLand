using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMap : MonoBehaviour
{
    [SerializeField] private List<temptlinedraw> Lines = new List<temptlinedraw>();
    [SerializeField] private List<StageSelectButton> Beatbuttons = new List<StageSelectButton>();

    private UIManager uiManager;
    private LogicManager logicManager;

    private void Start()
    {
        uiManager    = GameManager.GetManagerClass<UIManager>();
        uiManager.worldMap = this;
        logicManager = GameManager.GetManagerClass<LogicManager>();
        transform.SetParent(GameObject.Find("Canvas").transform);
        transform.localScale = new Vector3(1,1,1);
        transform.localPosition = new Vector3(-51.6f,257,0);

        gameObject.SetActive(false);
    }

    public void MapSelect(int index)
    {
        logicManager.SetWorldMapToSelectScene(index);

    }
    public void MapStart(int level)
    {
        int adjustlevel = level;

        for(int i=0; i<Beatbuttons.Count;i++)
        {
            Beatbuttons[i].gameObject.SetActive(true);
            Beatbuttons[i].SizeSet();
            //
        }


        

        switch (level)
        {
          
            case 4:
                adjustlevel = 3;
                break;
            case 5:
                adjustlevel = 3;
                break;
            case 6:
                adjustlevel = 4;
                break;
            case 7:
                adjustlevel = 5;
                break;
        }

        if (level == 3)
        {
            Beatbuttons[level - 1].SetRectTransform(1);
           
            for (int i = 3; i <= 5; i++)
            {
                Beatbuttons[i].SetRectTransform(2);
                Beatbuttons[i].LiveButton(true);
            }

            for (int i = 0; i < Beatbuttons.Count; i++)
            {
                if (i == 3) continue;
                if (i == 4) continue;
                if (i == 5) continue;

                Beatbuttons[i].LiveButton(false);
            }


        }
        else
        {
            for (int i = 0; i < Beatbuttons.Count; i++)
            {
                if (i == level) continue;

                Beatbuttons[i].LiveButton(false);
            }

            Beatbuttons[level - 1].SetRectTransform(1);
            Beatbuttons[level].SetRectTransform(2);
           

            Beatbuttons[level - 1].LiveButton(true);
            Beatbuttons[level].LiveButton(true);

        }
        
        for(int i=0;i<Lines.Count;i++)
        {
            Lines[i].MapSetLine(adjustlevel);
        }
      
    }

}
