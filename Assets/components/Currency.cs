using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Currency : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int currentCurrency;
    [SerializeField] private TextMeshProUGUI _myCurrencyElement;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateCurrencyText()
    {
        _myCurrencyElement.text = "$" + currentCurrency;
    }

    public void AddCurrency(int currency)
    {
        currentCurrency = Mathf.Min(currentCurrency + currency, 9999);
        UpdateCurrencyText();
    }

    public void SubtractCurrency(int currency)
    {
        currentCurrency = Math.Max(currentCurrency - currency, 0);
        UpdateCurrencyText();
    }
}
