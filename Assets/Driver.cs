using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    [SerializeField] float steerSpeed = 0.5f;
    [SerializeField] float moveSpeed = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float steerDirection = Input.GetAxis("Horizontal");
        transform.Rotate(0, 0, steerDirection * steerSpeed);
        transform.Translate(0, moveSpeed, 0);
    }


}
