using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FronkonGames.SpritesMojo;
using Outline = FronkonGames.SpritesMojo.Outline;


public class testoutline : MonoBehaviour
{
    static public int timeToUpdate = 1;
    private float randomRange = 0.25f;
    public Material material;
    private float time = 1.8f;

    // Start is called before the first frame update
    void Start()
    {
        //material = this.GetComponent<SpriteRenderer>().material;
        material = this.GetComponent<Image>().material;
        Outline.Mode.Set(material, 1);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;


       
        if (time > timeToUpdate)
        {
            Outline.Size.Set(material, 30, timeToUpdate);
           // Outline.Mode.Set(material, (OutlineMode)Random.Range(0, 3));
            float hue = Random.Range(0.0f, 1.0f);
            Outline.Color0.Set(material, Color.HSVToRGB(hue, 1.0f, 1.0f), timeToUpdate);
            Outline.Color1.Set(material, Color.HSVToRGB(1.0f - hue, 1.0f, 1.0f), timeToUpdate);
            Outline.TextureVelocity.Set(material, new Vector2(Random.Range(-randomRange, randomRange), Random.Range(-randomRange, randomRange)) * 0.01f, timeToUpdate);
            Outline.Vertical.Set(material, Random.Range(0.0f, 1.0f) >= 0.5f);
            time = 0.0f;
        }
    }
}
