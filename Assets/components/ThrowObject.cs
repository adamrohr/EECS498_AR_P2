using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Mapbox.Unity;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class ThrowObject : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    [SerializeField] private Transform cursor;
    private float throwForce = 5f;
    private float upForce = 0.5f;

    public AudioClip spawn_sound_effect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Throw()
    {
        Vector3 direction = (cursor.position - Camera.main.transform.position).normalized;
        var instance = Instantiate(obj, Camera.main.transform.position, Camera.main.transform.rotation);

        Vector3 forceDirection = (cursor.position - Camera.main.transform.position).normalized;
        var acorn = instance.GetComponent<Rigidbody>();
        Vector3 force = forceDirection * throwForce + transform.up * upForce;
        acorn.AddForce(force, ForceMode.Impulse);
        
        AudioSource.PlayClipAtPoint(spawn_sound_effect, Camera.main.transform.position);
    }
}
