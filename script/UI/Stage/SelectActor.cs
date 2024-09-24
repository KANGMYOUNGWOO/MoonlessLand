using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SelectActor : MonoBehaviour
{
    private ResourceManager resourceManager;
    private UIManager uiManager;
    private LogicManager logicManager;

    #region FiledLines
    [SerializeField]
    private List<temptlinedraw> subLine = new List<temptlinedraw>();

    [SerializeField]
    private List<temptlinedraw> midLine = new List<temptlinedraw>();

    [SerializeField]
    private List<temptlinedraw> proLine = new List<temptlinedraw>();

    #endregion
    #region FieldIcons
    [SerializeField]
    private List<StageSelectButton> subList = new List<StageSelectButton>();
    [SerializeField]
    private List<StageSelectButton> midList = new List<StageSelectButton>();
    [SerializeField]
    private List<StageSelectButton> proList = new List<StageSelectButton>();
    #endregion

    [SerializeField] private GameObject BeginigPoint;
    [SerializeField] private GameObject SelectArea;


    // Start is called before the first frame update
    void Start()
    {
        resourceManager = GameManager.GetManagerClass<ResourceManager>();
        uiManager = GameManager.GetManagerClass<UIManager>();
        logicManager = GameManager.GetManagerClass<LogicManager>();
        logicManager.selectActor = this;
        uiManager.selectActor = this;

    }

    #region �������� ��ư�� ��ġ ����
    public void positioning(int _index, int level, int _subConnection, int _midConnection, int _proConnection)
    {
        int pos_x = 0;
        int pos_y = 0;

        for (int i = 1; i < _index + 1; i++)
        {
            pos_x = (375 - (_index - 1) * 125) - 445 + 250 * (i - 1);
            pos_y = -550 + (level + 1) * 432;
            switch (level)
            {
                case 0:
                    subList[i - 1].SetRectTransform(pos_x, pos_y, i);
                    subList[i - 1].gameObject.SetActive(true);
                    break;
                case 1:
                    midList[i - 1].SetRectTransform(pos_x, pos_y, i + _subConnection);
                    midList[i - 1].gameObject.SetActive(true);
                    break;
                case 2:
                    proList[i - 1].SetRectTransform(pos_x, pos_y, i + _subConnection + _midConnection);
                    proList[i - 1].gameObject.SetActive(true);
                    break;
            }
        }
    }

    #endregion
    #region ���� �׸���
    public void SetLine(int index, int outdex, int level, int _subConnection, int _midConnection, int _proConnection)
    {
        int quotient = 0;
        int peet = 0;



        for (int i = 0; i < index; i++)
        {

            quotient = (logicManager.connection[4 * (level - 1) + i]) / 10;

            for (int j = 5; j > 1; j--)
            {

                if (quotient % j == 0)
                {
                    quotient = quotient / j;

                    //Debug.Log((i+1).ToString() + " " +(j-1).ToString());
                    switch (level)
                    {
                        case 1:
                            midLine[peet].SetLine(i + 1, j - 1, level, index, outdex, _subConnection + _midConnection + _proConnection);
                            midLine[peet].gameObject.SetActive(true);
                            break;
                        case 2:
                            proLine[peet].SetLine(i + 1, j - 1, level, index, outdex, _subConnection + _midConnection + _proConnection);
                            proLine[peet].gameObject.SetActive(true);
                            break;
                    }

                    peet += 1;
                }

            }

        }

    }
    public void SetSubLine(int outdex, int level, int _subConnection, int _midConnection, int _proConnection)
    {

        for (int i = 0; i < _subConnection; i++)
        {
            subLine[i].gameObject.SetActive(true);
            subLine[i].SetLine(1, i + 1, level, 1, outdex, _subConnection + _midConnection + _proConnection);
        }
    }

    #endregion
    #region ���̺� �����
    public void SelectTableOut()
    {

        for (int i = 0; i < subLine.Count; i++)
        {
            subLine[i].Reset();
        }

        for (int i = 0; i < midLine.Count; i++)
        {
            midLine[i].Reset();
        }

        for (int i = 0; i < proLine.Count; i++)
        {
            proLine[i].Reset();
        }

    }
    #endregion
    #region �ܰ迡 ���� ��ư Ȱ��ȭ
    public void ReShowSelectPage(int _subconnection, int _midconnection, int _proconnection)
    {
        for (int i = 0; i < _subconnection; i++)
        {
            subList[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < _midconnection; i++)
        {
            midList[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < _proconnection; i++)
        {
            proList[i].gameObject.SetActive(true);
        }

        for (int i = 0; i < 4; i++)
        {
            subLine[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < midLine.Count; i++)
        {
            midLine[i].gameObject.SetActive(true);
        }

        if (_proconnection > 0)
        {
            for (int i = 0; i < proLine.Count; i++)
            {
                proLine[i].gameObject.SetActive(true);
            }
        }
    }
    #endregion
    #region �� ȿ��
    public void FireOut(int num, int _subconnection, int _midconnection, int _proconnection)
    {
        switch (num)
        {
            case 1:
                for (int i = 0; i < _proconnection; i++)
                {
                    proList[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < proLine.Count; i++)
                {
                    proLine[i].gameObject.SetActive(false);
                }
                break;
            case 2:
                for (int i = 0; i < _midconnection; i++)
                {
                    midList[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < midLine.Count; i++)
                {
                    midLine[i].gameObject.SetActive(false);
                }
                break;
            case 3:
                for (int i = 0; i < _subconnection; i++)
                {

                    subList[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < subLine.Count; i++)
                {
                    subLine[i].gameObject.SetActive(false);
                }

                break;

            case 4:
                SelectArea.SetActive(false);
                // ActionCardcon.gameObject.SetActive(true);

                break;


        }
    }

    #endregion
    #region �ɼ� ���� ���� �����
    public void SelectDesolve()
    {
        for (int i = 0; i < 4; i++)
        {
            subList[i].gameObject.SetActive(false);
            midList[i].gameObject.SetActive(false);
            proList[i].gameObject.SetActive(false);
            subList[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < midList.Count; i++)
        {
            midList[i].gameObject.SetActive(false);
            proList[i].gameObject.SetActive(false);
        }
    }
    #endregion
    #region ��ư �̹��� ����
    public void SetImage(int index, int level,int AreaLevel)
    {
        Sprite tempSprite = null;
        string TempString = "��";
        for (int i = 0; i < index; i++)
        {

            if (logicManager.code[i + level] < 1000)
            {
                tempSprite = resourceManager.I_IconDictionary[AreaLevel];
               // tempSprite = resourceManager.GetIconImage(AreaLevel);
               
                switch (level)
                {
                    case 0:
                        subList[i].ChangeImage(tempSprite);
                        subList[i].SetText("��");
                        break;
                    case 4:
                        midList[i].ChangeImage(tempSprite);
                        midList[i].SetText("��");
                        break;
                    case 8:
                        proList[i].ChangeImage(tempSprite);
                        proList[i].SetText("��");
                        break;
                }

            }
            else
            {
                switch (logicManager.connection[i + level] % 10)
                {
                    case 1:
                       tempSprite = resourceManager.I_IconDictionary[1000];                                
                        TempString = "���";
                        break;
                    case 2:
                        tempSprite = resourceManager.I_IconDictionary[2000];                      
                        TempString = "�ٶ�";
                        break;
                    case 3:
                        tempSprite = resourceManager.I_IconDictionary[3000];                        
                        TempString = "��ǳ";
                        break;
                    case 4:
                        tempSprite = resourceManager.I_IconDictionary[4000];
                      
                        TempString = "����";
                        break;
                    case 5:
                        tempSprite = resourceManager.I_IconDictionary[5000];                       
                        TempString = "����";
                        break;
                }
                switch (level)
                {
                    case 0:
                        subList[i].SetText(TempString);
                        subList[i].ChangeImage(tempSprite);
                        break;
                    case 4:
                        midList[i].SetText(TempString);
                        midList[i].ChangeImage(tempSprite);
                        break;
                    case 8:
                        proList[i].SetText(TempString);
                        proList[i].ChangeImage(tempSprite);
                        break;
                }
            }
        }

    }
    #endregion
    #region ��ư �츮�� 
    public void LiveButton(bool bisAlive , int index , int level)
    {
        switch(level)
        {
            case 0:
                subList[index].LiveButton(bisAlive);
                break;
            case 1:
                midList[index].LiveButton(bisAlive);
                break;
            case 2:
                proList[index].LiveButton(bisAlive);
                break;
        }
    }
    public void ActiveButton(bool bisActive, int index, int level)
    {
        switch (level)
        {
            case 0:
                subList[index].gameObject.SetActive(bisActive);
                break;
            case 1:
                midList[index].gameObject.SetActive(bisActive);
                break;
            case 2:
                proList[index].gameObject.SetActive(bisActive);
                break;
        }
    }
    public void SelectButton(int index, int level)
    {
        switch (level)
        {
            case 0:
                subList[index].Selected();
                break;
            case 1:
                midList[index].Selected();
                break;
            case 2:
                proList[index].Selected();
                break;
        }
    }
    public void StopSequence(int index, int level)
    {
        switch (level)
        {
            case 0:
                subList[index].StopSequence();
                break;
            case 1:
                midList[index].StopSequence();
                break;
            case 2:
                proList[index].StopSequence();
                break;
        }
    }
    public void ActiveLine(bool bisActive, int index, int level)
    {
        switch (level)
        {
            case 0:
                subLine[index].gameObject.SetActive(bisActive);
                break;
            case 1:
                midLine[index].gameObject.SetActive(bisActive);
                break;
            case 2:
                proLine[index].gameObject.SetActive(bisActive);
                break;
        }
    }
    public void ResetLine(int index, int level)
    { 
        switch (level)
        {
            case 0:
                subLine[index].Reset();
                break;
            case 1:
                midLine[index].Reset();
                break;
            case 2:
                proLine[index].Reset();
                break;
        }

    }
    public void ActiveBeginingPoint(bool bisActive)
    {
        BeginigPoint.gameObject.SetActive(bisActive);
    }
    public void SelectSceneActive(bool bisActive)
    {
        SelectArea.gameObject.SetActive(bisActive);
    }

    #endregion
}
