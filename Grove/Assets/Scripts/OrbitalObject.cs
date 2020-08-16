using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalObject : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float rotateSpeed;
    [SerializeField] Vector3 rotation;
    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(target.position, rotation, rotateSpeed * Time.deltaTime);
    }
}
