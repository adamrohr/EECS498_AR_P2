using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float speed = 5.0f;

    [Range(0.0f, 1.0f)] public float smoothFactor = 0.5f;

    public float xRotateSpeed = 8.0f;
    public float yRotateSpeed = 10.0f;

    private void Start()
    {
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            Quaternion xCamTurn = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * xRotateSpeed, Vector3.up);
            Quaternion yCamTurn = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * yRotateSpeed, transform.right);
            offset = xCamTurn * yCamTurn * offset;
        }
        Vector3 newPos = target.position + offset;
        transform.position = Vector3.Slerp(transform.position, newPos, smoothFactor);

        
        transform.LookAt(target);
    }
}
