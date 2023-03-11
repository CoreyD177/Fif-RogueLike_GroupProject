using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.position -= new Vector3(39f,0f,0f);
    }
}
