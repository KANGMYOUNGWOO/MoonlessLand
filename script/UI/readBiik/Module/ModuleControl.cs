using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModuleControl : MonoBehaviour
{
    #region Component

    [SerializeField] private GameObject moduleSlotParent;
    [SerializeField] private GameObject cardSlotParent;
    [SerializeField] private List<ModuleSet> moduleSets = new List<ModuleSet>();

    #endregion

   
    private ResourceManager resourceManager;
    private CharacterManager characterManager;


    private PlayerInfo playerinfo;

   
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
        resourceManager = GameManager.GetManagerClass<ResourceManager>();       
        characterManager = GameManager.GetManagerClass<CharacterManager>();
        resourceManager.GetPlayerInfo(out playerinfo);
    }


    private void GetPlayerData(PlayerInfo playerinfo)
    {
        this.playerinfo = playerinfo;
    }

    private void InitializeModule(PlayerInfo.CardModule mod, int a)
    {
        int str = mod.Strength;
        int agi = mod.Agility;
        int exa = mod.Examine;
        int ste = mod.Stealth;
        int nim = mod.module;


        Sprite s_str = resourceManager.I_CardBackDictionary[str];
        Sprite s_agi = resourceManager.I_CardBackDictionary[agi];
        Sprite s_exa = resourceManager.I_CardBackDictionary[exa];
        Sprite s_ste = resourceManager.I_CardBackDictionary[ste];
        Sprite icon  = resourceManager.I_CardBackDictionary[nim];

        string nameTxt = resourceManager.NimrodDictionary[nim].Name;

        moduleSets[a].SetImage(s_str,s_agi,s_exa,s_ste,icon);
        moduleSets[a].SetName(nameTxt);
    }


    private void Initialization()
    {
        InitializeModule(playerinfo.CardModule1, 0);
        InitializeModule(playerinfo.CardModule2, 1);
        InitializeModule(playerinfo.CardModule3, 2);
        InitializeModule(playerinfo.CardModule4, 3);
        InitializeModule(playerinfo.CardModule5, 4);
        InitializeModule(playerinfo.CardModule6, 5);


    }




}
