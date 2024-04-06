using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 rotateSpeed;

    private void FixedUpdate()
    {
        transform.Rotate(rotateSpeed * Time.fixedDeltaTime);
    }
}
