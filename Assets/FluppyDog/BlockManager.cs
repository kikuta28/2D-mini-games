using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public GameObject prefabBlock;

    // オブジェクト生成タイマー
    float waitTimer;

    // 経過時間
    public float totalTime;

    // 最初は停止状態
    public bool isStop;

    // Start is called before the first frame update
    void Start()
    {
        isStop = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStop) return;

        waitTimer -= Time.deltaTime;
        totalTime += Time.deltaTime;

        // ブロック生成
        if (waitTimer < 0)
        {
            Vector3 pos = transform.position;

            // ランダムな位置に出現させる
            pos.y = Random.Range(-5, 5);

            // プレハブ生成
            GameObject obj = Instantiate(prefabBlock, pos, Quaternion.identity);
            BlockController ctrl = obj.GetComponent<BlockController>();

            // 経過時間によって動く速度を変える
            ctrl.moveSpeed = -(100 + (totalTime * 1.5f));

            // 次の生成時間(ランダム)
            float min = 2 - (totalTime / 100);
            float max = min * 2;

            if (0.01f > min) min = 0.01f;

            waitTimer = Random.Range(min, max);
        }
    }
}

