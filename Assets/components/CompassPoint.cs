using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassPoint : MonoBehaviour
{
    [SerializeField] GameObject camera;
    // Update is called once per frame
    void Update()
    {
        Vector3 vec = new Vector3();
        vec.z = camera.transform.eulerAngles.y;
        transform.localEulerAngles = vec;
    }
}
