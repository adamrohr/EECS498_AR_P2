using System;
using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity;
using TMPro;
using UnityEngine;

public class Currency : MonoBehaviour
{
    // Start is called before the first frame update
    private static float currentCurrency = 0;
    [SerializeField] private TextMeshProUGUI _myCurrencyElement;
    private void Start()
    {
        UpdateCurrencyText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateCurrencyText()
    {
        _myCurrencyElement.text = "$" + currentCurrency;
    }

    public void AddCurrency(float currency)
    {
        currentCurrency = Mathf.Min(currentCurrency + currency, 999);
        currentCurrency = (float)Math.Round(currentCurrency, 2);
        UpdateCurrencyText();
    }

    public float GetCurrency()
    {
        return currentCurrency;
    }

    public void SubtractCurrency(float currency)
    {
        currentCurrency = Math.Max(currentCurrency - currency, 0);
        UpdateCurrencyText();
    }
}
