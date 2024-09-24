using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scroll : MonoBehaviour
{

    [SerializeField] RectTransform content;
    [SerializeField] RectTransform text;

    
    // Update is called once per frame
    private void Update()
    {
        
        content.sizeDelta = new Vector2(content.rect.width, text.rect.height);

    }
}
