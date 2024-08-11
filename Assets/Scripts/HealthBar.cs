using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour

{
    private Image _image;

    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();

    }

    public void UpdateHealthBar(float startHealth, float currentHealth)
    {
        print("ITS WORKING");
        print(currentHealth);
        _image.fillAmount = currentHealth / startHealth;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
