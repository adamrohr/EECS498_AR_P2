using System;
using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using UnityEngine;

public class MarkerInteraction : MonoBehaviour
{
    private string _name = new string("");
    private GameObject _info;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMarkerName(string newName)
    {
        _name = newName;
    }

    public void SetInfoObject(GameObject info)
    {
        _info = info;
    }

    private void OnMouseDown()
    {
        _info.SetActive(true);
    }
}
