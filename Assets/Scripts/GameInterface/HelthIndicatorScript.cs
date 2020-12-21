using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HelthIndicatorScript : MonoBehaviour
{
    [SerializeField] private Image _helthImage;
    [SerializeField] private Text _helthCount;

    public void Refresh(int helth)
    {
        if (helth < 0)
            helth = 0;
        if (helth > 100)
            helth = 100;

        Vector2 imageSize = _helthImage.rectTransform.sizeDelta;
        _helthImage.rectTransform.sizeDelta = new Vector2(helth, imageSize.y);
        _helthCount.text = helth.ToString();
    }
    
}
