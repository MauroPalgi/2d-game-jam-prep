using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;

    public void SetMaxValue(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    public void SetSlider(int health)
    {
        slider.value = health;
    }
}
