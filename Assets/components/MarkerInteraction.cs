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
    private int _itemUnlock;
    private float dist;
    [SerializeField] private ItemTracker inv;
    [SerializeField] private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dist = Mathf.Abs(Vector3.Distance(transform.position, player.transform.position));
    }

    public void SetMarkerName(string newName)
    {
        _name = newName;
    }

    public void SetInfoObject(GameObject info)
    {
        _info = info;
    }
    
    public void SetItemUnlock(int num)
    {
        _itemUnlock = num;
    }

    private void OnMouseDown()
    {
        if(dist < 10) {
            _info.SetActive(true);
            inv.UnlockSeed(_itemUnlock);
        }
    }
}
