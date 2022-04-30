using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedSlider : MonoBehaviour
{
    Slider speedSlider;
    private float speedScaleFactor = 1.5f;
    [SerializeField] private Movement _movement;

    void Start()
    {
        speedSlider = transform.GetComponent<Slider>();
        speedSlider.onValueChanged.AddListener(delegate { ValueChanged(); });
     
    }

    private void ValueChanged()
    {
        _movement.Speed = _movement.Speed * (speedScaleFactor * speedSlider.value);
    }
}
