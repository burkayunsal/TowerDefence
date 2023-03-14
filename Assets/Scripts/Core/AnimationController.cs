using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class AnimationController : MonoBehaviour
{

    [SerializeField] private Image img;
    private Color initialColor;
    
    private void Start()
    {
        initialColor = img.color;
    }

    public void PunchScale(float sizePercentage, float duration)
    {
        transform.DOPunchScale(transform.localScale * sizePercentage, duration, 1, 0);
    }

    public void DoColorFlash(Color color, float duration)
    {
        img.DOColor(color, duration/2f).OnComplete(() =>
        {
            img.DOColor(initialColor, duration/2f);
        });
    }
    
    
}
