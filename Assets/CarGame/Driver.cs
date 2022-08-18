using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    [SerializeField] float steerSpeed = 150f;
    [SerializeField] float moveSpeed = 5f;

    void Start()
    {

    }

    void Update()
    {
        float steerDirection = -Input.GetAxis("Horizontal");
        float steer = steerDirection * steerSpeed * Time.deltaTime;

        float moveDirection = Input.GetAxis("Vertical");
        float move = moveDirection * moveSpeed * Time.deltaTime;

        transform.Rotate(0, 0, steer);
        transform.Translate(0, move, 0);
    }
}
