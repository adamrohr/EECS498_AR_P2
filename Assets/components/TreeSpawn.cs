using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Factories;
using Mapbox.Unity.Utilities;
using System.Collections.Generic;
using Mapbox.Unity.Location;

public class TreeSpawn : MonoBehaviour
{
    [SerializeField] private EditorLocationProviderLocationLog locationLog;
    [SerializeField] private DeviceLocationProvider deviceLocProvider;
    [SerializeField]
    private AbstractMap map;
    [SerializeField] private ItemTracker inv;
    [SerializeField] private Currency currency;
    [SerializeField] private ARObjectSpawner objSpawner;
    [SerializeField] private GameObject dataPersistance;
    [SerializeField] private SceneTransition transition;

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
    [SerializeField] private GameObject squirrelPrefab;

    private GameObject[] treePrefabs = new GameObject[10];

    private static int[] _treesSpawned = new int[]{0, 0, 0, 0, 0, 0 ,0 ,0 ,0, 0};
    private static List<int> _treesOrder = new List<int>();
    private static List<int> _squirrelTrees = new List<int>();
    private static List<GameObject> _treeObjects = new List<GameObject>();

    [SerializeField] float squirrelScale = .05f;

    private static Vector2d playerLocation;
    private float timer = 0.0f;
    private bool spawnedTrees = false;

    private static List<Vector2d> treeLocations = new List<Vector2d>();
    private static List<int> plantOrder = new List<int>();
    private static List<int> _treeGrowth = new List<int>();
    
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
        DontDestroyOnLoad(dataPersistance);        
        // CreateTrees();
    }

    private void Map_OnInitialized()
    {
        if (spawnedTrees) return;
        CreateTrees();
        spawnedTrees = true;
    }
    // Update is called once per frame
    void Update()
    {
        UpdatePlayerLocation();
        timer += Time.deltaTime;
        int seconds = (int)timer % 60;
        
        //map.OnInitialized += Map_OnInitialized;
        if(map.InitialZoom != 0 && !spawnedTrees) {
            print("Spawning trees");
            CreateTrees();
            print("TreesOrder Count: " + _treesOrder.Count);
            spawnedTrees = true;
        }

        
        List<Vector2d> locations = objSpawner.getTreeLocations();
        int squirrelSpawnTree = Random.Range(0, 2000 * locations.Count);
        
        // Check if squirrelSpawn is a valid index in locations
        if(squirrelSpawnTree < _treesOrder.Count && _treeGrowth[squirrelSpawnTree] == 5) {
            List<int> planted = inv.GetPlanted();
            float randX = Random.Range(-1, 1);
            float randZ = Random.Range(-1, 1);
            var squirrel = Instantiate(squirrelPrefab);
            squirrel.transform.parent = _treeObjects[squirrelSpawnTree].transform;
            squirrel.transform.localScale = new Vector3(squirrelScale, squirrelScale, squirrelScale);
            squirrel.transform.localPosition += new Vector3(randX, 0, randZ);
            _squirrelTrees.Add(_treesOrder[squirrelSpawnTree]);
            // DontDestroyOnLoad(squirrel);
        }

        if (seconds >= 10)
        {
            for (int i = 0; i < _treeObjects.Count; ++i)
            {
                if (_treeGrowth[i] == 5)
                {
                    float currencyAdd = _treesSpawned[i] * (i+1);
                    if(_squirrelTrees.Contains(i)) {
                        currencyAdd *= 0.1f;
                    }
                    currency.AddCurrency(currencyAdd);
                    timer = 0.0f;
                }
            }
        }

        for (int i = 0; i < _treeObjects.Count; ++i)
        {
            if (_treeGrowth[i] < 5 && locationLog.GetMovement() >= 0.001)
            {
                _treeGrowth[i] += 1;
                _treeObjects[i].transform.localScale = new Vector3(_treeGrowth[i], _treeGrowth[i], _treeGrowth[i]);
                locationLog.ClearMovement();
            }
        }
    }

    public void AddTreeLocation(Vector2d location)
    {
        treeLocations.Add(location);
    }

    public void CreateTrees()
    {
        for (int i = 0; i < treeLocations.Count; ++i)
        {
            _treeObjects[i].transform.localPosition = map.GeoToWorldPosition(treeLocations[i]);
            _treeObjects[i].transform.localScale = new Vector3(_treeGrowth[i], _treeGrowth[i], _treeGrowth[i]);
        }
        
        List<int> planted = inv.GetPlanted();
        List<Vector2d> locations = objSpawner.getTreeLocations();
        for (int i = 0; i < planted.Count; ++i)
        {
            var instance = Instantiate(treePrefabs[planted[i]-1]);
            //Vector2d pos = locations[i];
            Vector2d treeOffset = new Vector2d(Random.Range(-0.0001f, 0.0001f), Random.Range(-0.0001f, 0.0001f));
            instance.transform.localPosition = map.GeoToWorldPosition(playerLocation + treeOffset, true);
            int _initTreeGrowth = 1;
            
            instance.transform.localScale = new Vector3(_initTreeGrowth, _initTreeGrowth, _initTreeGrowth);
            instance.tag = "Tree";
            treeLocations.Add(playerLocation + treeOffset);
            plantOrder.Add(planted[i]-1);
            _treesSpawned[planted[i]-1] += 1;
            _treesOrder.Add(planted[i] - 1);
            _treeObjects.Add(instance);
            DontDestroyOnLoad(instance);
            _treeGrowth.Add(_initTreeGrowth);
            transition.UpdateTrees(_treeObjects, _treeGrowth);
        }
        inv.ClearPlanted();
    }

    public void UpdatePlayerLocation()
    {
    #if UNITY_EDITOR
        playerLocation = locationLog.CurrentLocation.LatitudeLongitude;
    #else
        playerLocation = deviceLocProvider.CurrentLocation.LatitudeLongitude;
    #endif
    }

    public Vector2d GetPlayerLocation()
    {
        return playerLocation;
    }
}
