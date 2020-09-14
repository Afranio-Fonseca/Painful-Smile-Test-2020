using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Transform target;
    [SerializeField]
    float smoothSpeed = 1;
    [SerializeField]
    float frontalOffset = 1;

    // Update is called once per frame
    void LateUpdate()
    {
        if(target != null)
            transform.position = Vector3.Lerp(transform.position, target.position + target.up * frontalOffset - target.forward, smoothSpeed * Time.deltaTime);
    }
}
