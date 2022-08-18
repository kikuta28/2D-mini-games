using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public float moveSpeed = -100;

    Rigidbody2D rb;

    Vector2 screenMin;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(moveSpeed, 0));
        screenMin = Camera.main.ViewportToWorldPoint(Vector2.zero);
    }

    // Update is called once per frame
    void Update()
    {
        // 画面外の処理(左側)
        if (transform.position.x + transform.localScale.x < screenMin.x)
        {
            Destroy(gameObject);
        }
    }
}

