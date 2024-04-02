using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] Vector3 vector3;
    void Update()
    {
        transform.Rotate(vector3*Time.deltaTime);

    }
}
