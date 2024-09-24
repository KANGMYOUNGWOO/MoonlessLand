using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Febucci.UI;
using System.IO;
using TMPro;


public class TextManager : MonoBehaviour,IManager
{

    public GameManager gameManager { get { return GameManager.gameManager; } }
    private ResourceManager resourceManager;
    private UIManager uiManager;

    private string RawText;

    // ���� �ؽ�Ʈ
    string CurrentText;

    // ���� #���� �и��� �ؽ�Ʈ
    public string[] CurrentTexts;
   


    private string Name;
    private string Trait;
 

    
    private TextAnimatorPlayer textAnimatorPlayer;
    [SerializeField] private Text NameText;
    [SerializeField] private Text TraitText;


    //[SerializeField] private RectTransform textrect;
    private int rectNum = 0;

    private void Awake()
    {
        resourceManager = GameManager.GetManagerClass<ResourceManager>();
        uiManager = GameManager.GetManagerClass<UIManager>();
        
    }

    public void initializeTextManager(TextAnimatorPlayer animatorPlayer , Text Name , Text Trait)
    {
        textAnimatorPlayer = animatorPlayer;
        NameText = Name;
        TraitText = Trait;
        textAnimatorPlayer.textAnimator.onEvent += TextPlayerEffect;

    }

    
    private void tmpReader()
    {
        //rectNum = 0;
       // textrect.anchoredPosition = new Vector2(-426f, 416.15f);
        textAnimatorPlayer.ShowText(" ");
        //Debug.Log(CurrentText);
        textAnimatorPlayer.ShowText(CurrentText);
        
    }
    
    public void ReadReadTextAsset(int code)
    {
     

       if( resourceManager.AD_Current.ContainsKey(code))
        {
            RawText = resourceManager.AD_Current[code];
            CurrentTexts = RawText.Split('#');
            Name = CurrentTexts[0];
            Trait = CurrentTexts[1];
            CurrentText = CurrentTexts[2];
           

            NameText.text = Name;
            TraitText.text = Trait;
                      
            tmpReader();
        }
       else
        {
           
           CurrentText = "���� Ư���� �� ����.";
            
            tmpReader();
        }
    }

    public void ReadMonsterAsset(int code,string name)
    {
        
        CurrentText = resourceManager.AD_Monsters[name][code];
        Name = name;
        TraitText.text = "";
        NameText.text = Name;
        tmpReader();
    }


    #region ���� ����Ʈ

    public void TextPlayerEffect(string message)
    {
        switch(message)
        {
            case "book":
                uiManager.Prologues("DcitionaryEnd");
                break;
            case "cameraShake":
                uiManager.Prologues("cameraShake");
                break;
            case "combine":
                uiManager.Prologues("ItemCombine");
                break;
            case "NimrodGlitch1":
                uiManager.Prologues("NimrodGlitch1");
                break;

            case "rect":
                AddTextRect();
                break;
        }
    }

     private void AddTextRect()
    {
        rectNum += 1;
       // textrect.anchoredPosition = new Vector2(-426f, 416f + rectNum * 49.21f);
    }

    public void Clear()
    {
        textAnimatorPlayer.ShowText(" ");
    }


    #endregion
    #region ������ ���� ������ ��
    public void AreaEnd()
    {
        NameText.text = "";
        TraitText.text = "";        
    }


    #endregion



}
