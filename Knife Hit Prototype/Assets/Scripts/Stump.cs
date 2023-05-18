using System;
using UnityEngine;

public class Stump : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;  

    void Update()
    {
        transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime);
    }
}
