using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour

{
    [SerializeField] private float _timeToDrain = 0.25f;
    [SerializeField] private Gradient _healthBarGradient;
    private Image _image;

    private float _targetFillAmount = 1;

    private Color _newHealthBarColor;

    private Coroutine drainHealthBarCoroutine;


    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();

        // set the starting health bar color
        _image.color = _healthBarGradient.Evaluate(_targetFillAmount);

        CheckHealthBarGradientAmount();

    }

    public void UpdateHealthBar(float startHealth, float currentHealth)
    {
        //print("ITS WORKING");
        //print(currentHealth);
        _targetFillAmount = currentHealth / startHealth;

        drainHealthBarCoroutine = StartCoroutine(DrainHealthBar());

        CheckHealthBarGradientAmount();

    }

    private IEnumerator DrainHealthBar()
    {
        float fillAmount = _image.fillAmount;
        Color currentColor = _image.color;

        float elapsedTime = 0f;
        while (elapsedTime < _timeToDrain)
        {
            elapsedTime += Time.deltaTime;

            // Lerp the fill amount
            _image.fillAmount = Mathf.Lerp(fillAmount, _targetFillAmount, (elapsedTime / _timeToDrain));

            // Lerp the fill color
            _image.color = Color.Lerp(currentColor, _newHealthBarColor, (elapsedTime / _timeToDrain));


            yield return null;
        }

    }

    private void CheckHealthBarGradientAmount()
    {
        _newHealthBarColor = _healthBarGradient.Evaluate(_targetFillAmount);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
