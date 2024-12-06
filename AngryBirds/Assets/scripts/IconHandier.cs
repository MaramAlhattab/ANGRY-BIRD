using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconHandier : MonoBehaviour
{
    [SerializeField] private Image[] icons;
    [SerializeField] private Color usedColor;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void UseShot1(int ShotNamber)
    {
        for (int i = 0; i < icons.Length; i++) 
        {
            if (ShotNamber == i + 1)
            {
                icons[i].color = usedColor;
                return;
            }
        }
    }
}
