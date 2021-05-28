using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    public Transform cam;

    private void Start()
    {
        if (!cam)
        {
            cam = Camera.main.transform;
        }
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
