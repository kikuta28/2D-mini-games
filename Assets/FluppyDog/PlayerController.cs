using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ジャンプの加速度
    public float jumpVelocity = 1000;

    Rigidbody2D rb;

    // 画面外の判定
    Vector2 screenTop, screenBottom;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 画面の座標を取得
        screenTop = Camera.main.ViewportToWorldPoint(Vector2.one);
        screenBottom = Camera.main.ViewportToWorldPoint(Vector2.zero);
    }

    // Update is called once per frame
    void Update()
    {
        // ジャンプ
        if (Input.GetKeyUp(KeyCode.Space))
        {
            jump();
        }
    }

    // 物理挙動を行う場合はFixedUpdate() 内に記述する
    private void FixedUpdate()
    {
        // 画面外の判定
        Vector3 pos = transform.position;

        // 上
        if (pos.y > screenTop.y)
        {
            // 速度リセット
            rb.velocity = Vector2.zero;
            pos.y = screenTop.y;
        }

        // 下
        if (pos.y < screenBottom.y)
        {
            jump();
            pos.y = screenBottom.y;
        }

        // 更新
        transform.position = pos;
    }

    void jump()
    {
        // 落下速度リセット
        rb.velocity = Vector2.zero;

        // ジャンプ
        rb.AddForce(new Vector2(0, jumpVelocity));
    }

    // 衝突判定
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

}

