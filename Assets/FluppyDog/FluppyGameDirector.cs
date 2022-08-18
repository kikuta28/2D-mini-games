using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FluppyDogGameDirector : MonoBehaviour
{
    public GameObject player;
    public GameObject txtScore;
    public GameObject txtInfo;
    public GameObject btnRestart;

    public BlockManager blockMng;

    float startTimer;

    enum MODE
    {
        NONE,
        READY,
        MAIN,
        RESULT,
    }

    MODE nowMode, nextMode;

    // Start is called before the first frame update
    void Start()
    {
        btnRestart.SetActive(false);
        startTimer = 3.9f;

        player.GetComponent<Rigidbody2D>().simulated = false;

        nowMode = MODE.READY;
        nextMode = MODE.NONE;
    }

    // Update is called once per frame
    void Update()
    {
        // 開始前演出
        if (MODE.READY == nowMode)
        {
            // 開始前カウントダウン
            startTimer -= Time.deltaTime;

            setText(txtInfo, "" + Mathf.Floor(startTimer));

            if (1 > startTimer)
            {
                setText(txtInfo, "START!!");
            }

            // 開始
            if (0 > startTimer)
            {
                // 物理挙動オン
                player.GetComponent<Rigidbody2D>().simulated = true;
                // ブロック生成開始
                blockMng.isStop = false;
                txtInfo.SetActive(false);

                nextMode = MODE.MAIN;
            }
        }
        // ゲームモード
        else if (MODE.MAIN == nowMode)
        {
            // 死亡処理(当たり判定があった場合、プレイヤーのオブジェクトは
            // Destoryによりnullになるので
            if (null == player)
            {
                setText(txtInfo, "GAME OVER");

                btnRestart.SetActive(true);
                txtInfo.SetActive(true);

                nextMode = MODE.RESULT;
            }

            // スコア更新
            setText(txtScore, "" + Math.Floor(blockMng.totalTime));
        }

        // モードの切り替え
        if (MODE.NONE != nextMode)
        {
            nowMode = nextMode;
            nextMode = MODE.NONE;
        }
    }

    // 指定のオブジェクトのTextコンポーネントに文字列をセット
    // たくさん出てくるので関数化
    void setText(GameObject obj, string str)
    {
        if (null == obj || null == obj.GetComponent<Text>())
        {
            return;
        }

        obj.GetComponent<Text>().text = str;
    }

    public void Retry()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
