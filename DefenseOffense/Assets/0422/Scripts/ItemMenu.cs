using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMenu : MonoBehaviour
{
    public RectTransform panel;
    public RectTransform center;
    public RectTransform[] buttons;
    float[] distance;
    int buttonNumber;
    int minButtonCount;
    void Start()
    {
        int count = buttons.Length; 
        distance = new float[count];
        buttonNumber  = (int)Mathf.Abs(buttons[1].anchoredPosition.x - buttons[0].anchoredPosition.x);
        Debug.Log(buttonNumber);
        
    }

    
    void Update()
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            distance[i] = Mathf.Abs(center.position.x - buttons[i].position.x);
        }
        float distaceMin = Mathf.Min(distance);
        //Debug.Log(distaceMin);
        for(int i = 0; i < buttons.Length; i++)
        {
            if(distaceMin == distance[i])
            {
                Debug.Log(i);

            }

        }
    }
}
