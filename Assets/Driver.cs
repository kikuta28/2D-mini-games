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
        float steer = steerDirection * steerSpeed * Time.deltaTime;
        float moveDirection = Input.GetAxis("Vertical");
        float move = moveDirection * moveSpeed * Time.deltaTime;

        transform.Rotate(0, 0, steer);
        transform.Translate(0, move, 0);
    }


}
