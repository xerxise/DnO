using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public Slider HealthBar;
    public float Damage;
    private void Update()
    {
        if(Input.GetKey(KeyCode.H))
        {
            HealthBar.value -= Damage;
        }
    }
}

