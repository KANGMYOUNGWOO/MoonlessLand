using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FronkonGames.SpritesMojo;


public class TokenUI : MonoBehaviour
{    
    private ResourceManager resourceManager;
    private CharacterManager characterManager;
    private UIManager uiManager;
    public float changetime;
    [SerializeField] private List<Image> Tokens = new List<Image>();
    private Material[] t_Materials = new Material[7];
    

    private void Awake()
    {
        for(int i=0; i<t_Materials.Length;i++)
        {
            t_Materials[i] = Retro.CreateMaterial();
            Retro.Emulation.Set(t_Materials[i], Retro.Emulations.NES);
            Retro.Pixelation.Set(t_Materials[i],1);
            Tokens[i].material = t_Materials[i];
        }

      
    }

    private void Start()
    {
        resourceManager = GameManager.GetManagerClass<ResourceManager>();
        characterManager = GameManager.GetManagerClass<CharacterManager>();
        uiManager = GameManager.GetManagerClass<UIManager>();
        uiManager.tokenUI = this;
        characterManager.tokenUI = this;

        transform.SetParent(GameObject.Find("Canvas").transform);
        transform.localScale = new Vector3(1,1,1);
        transform.localPosition = new Vector3(243,163,0);
    }

    public void SetToken(int index, bool activation , int color = 0)
    {
       
        if (!activation) StartCoroutine(TokenDissolve(index, color));
        else StartCoroutine(TokenActive(index,color));
    }

    private IEnumerator TokenDissolve(int index , int color)
    {
        WaitForSeconds wait = new WaitForSeconds(changetime);
        Tokens[index].sprite = resourceManager.I_TokenDictionary[color];
        SpriteMojo.Amount.Set(t_Materials[index], 1);
        int retroInt = 0;
        Retro.Pixelation.Set(t_Materials[index], retroInt);
        while (retroInt < 9)
        {
            retroInt += 1;
            //Retro.Luminance.Set(t_Materials[index],retroInt);
            Retro.Pixelation.Set(t_Materials[index],retroInt);
            yield return wait;
        }
        SpriteMojo.Amount.Set(t_Materials[index],0);
        
        
    }

    private IEnumerator TokenActive(int index, int color)
    {
        WaitForSeconds wait = new WaitForSeconds(changetime);
        Tokens[index].sprite = resourceManager.I_TokenDictionary[color];
        SpriteMojo.Amount.Set(t_Materials[index], 1);
        int retroInt = 9;
        Retro.Pixelation.Set(t_Materials[index], retroInt);
        while (retroInt > 1)
        {
            retroInt -= 1;
            Retro.Pixelation.Set(t_Materials[index], retroInt);
            yield return wait;
        }
        SpriteMojo.Amount.Set(t_Materials[index], 0);
        
    }

}
