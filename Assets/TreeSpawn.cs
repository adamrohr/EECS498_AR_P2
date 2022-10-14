using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Factories;
using Mapbox.Unity.Utilities;
using System.Collections.Generic;

public class TreeSpawn : MonoBehaviour
{
    
    [SerializeField]
    private AbstractMap map;

    [SerializeField] private ItemTracker inv;
    
    [SerializeField] private GameObject tree0;
    [SerializeField] private GameObject tree1;
    [SerializeField] private GameObject tree2;
    [SerializeField] private GameObject tree3;
    [SerializeField] private GameObject tree4;
    [SerializeField] private GameObject tree5;
    [SerializeField] private GameObject tree6;
    [SerializeField] private GameObject tree7;
    [SerializeField] private GameObject tree8;
    [SerializeField] private GameObject tree9;

    private GameObject[] treePrefabs = new GameObject[10];
    
    [SerializeField]
    float spawnScale = .2f;

    private static Vector2d playerLocation;
    
    // Start is called before the first frame update
    void Start()
    {
        treePrefabs[0] = tree0;
        treePrefabs[1] = tree1;
        treePrefabs[2] = tree2;
        treePrefabs[3] = tree3;
        treePrefabs[4] = tree4;
        treePrefabs[5] = tree5;
        treePrefabs[6] = tree6;
        treePrefabs[7] = tree7;
        treePrefabs[8] = tree8;
        treePrefabs[9] = tree9;
        
        CreateTrees();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void CreateTrees()
    {
        List<int> planted = inv.GetPlanted();
        print(planted.Count);
        for (int i = 0; i < planted.Count; ++i)
        {
            var instance = Instantiate(treePrefabs[planted[i]]);
            instance.transform.localPosition = map.GeoToWorldPosition(playerLocation, true);
            instance.transform.localScale = new Vector3(spawnScale, spawnScale, spawnScale);
            DontDestroyOnLoad(instance);
        }
        inv.ClearPlanted();
    }

    public void SetPlayerLocation(Vector2d location)
    {
        playerLocation = location;
    }
}
