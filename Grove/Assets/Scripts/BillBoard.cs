using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    public Transform cam;//reference of a camera

    // Update is called once per frame
    void LateUpdate()
    {
        if (cam == null)
        {
            cam = Camera.main.transform;
        }
        transform.LookAt(transform.position + cam.forward);
    }
}
