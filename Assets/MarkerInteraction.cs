using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using UnityEngine;

public class MarkerInteraction : MonoBehaviour
{

    public Dictionary<string, Vector2d> markers = new Dictionary<string, Vector2d>()
    {
        {"Angell Hall", new Vector2d(42.276743, -83.740304)}, 
        {"The Dude", new Vector2d(42.291609, -83.715868)}, 
        {"The BBB", new Vector2d(42.292728, -83.716728)}, 
        {"The UGLI", new Vector2d(42.276062, -83.737242)}, 
        {"Rackham", new Vector2d(42.280536, -83.738271)}, 
        {"EECS Building", new Vector2d(42.292391, -83.714851)}, 
        {"STAMPS Auditorium", new Vector2d(42.291708, -83.717068)}, 
        {"Hatcher Graduate Library", new Vector2d(42.276357, -83.737747)}, 
        {"Orion Statue - UMMA", new Vector2d(42.275541, -83.740404)},
        {"Ford Robotics Building", new Vector2d(42.294468, -83.709906)}
    };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
