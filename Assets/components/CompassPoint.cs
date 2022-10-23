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
        // transform.position = new Vector3(-1, 0, 0);
        // transform.forward = new Vector3(camera.transform.rotation.x, camera.transform.rotation.y, camera.transform.rotation.z);
        // transform.rotation = Quaternion.identity;
        // transform.RotateAround(transform.position, Vector3.forward, 5);
    }
}
