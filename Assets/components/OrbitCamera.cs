using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float speed = 5.0f;

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetMouseButton(0)) {
            float translation = 0.0f;
            if(Input.GetAxis("Mouse X") < 0) {
                translation = -1;
            } else if(Input.GetAxis("Mouse X") > 0) {
                translation = 1;
            }
            // float zTranslation = Mathf.Sin(10 * Input.GetAxis("Mouse X"));
            // float xTranslation = Mathf.Cos(10 * Input.GetAxis("Mouse X"));
            float yTranslation = Input.GetAxis("Mouse Y") * speed;
            if(transform.position.y > 0 || yTranslation > 0) {
                transform.Translate(0, yTranslation, 0);
            }
            // float yTranslation = Input.GetAxis("Mouse Y") * speed * Time.deltaTime;
            transform.Translate(translation, 0, 0);
            
        }
        else
        {
            Vector3 desiredPosition = target.position + offset;
            transform.position = desiredPosition;
        }
        transform.LookAt(target);
    }
}
