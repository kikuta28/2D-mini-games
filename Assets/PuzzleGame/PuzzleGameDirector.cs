using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleGameDirector : MonoBehaviour
{
    // タイル関連
    List<GameObject> tiles;
    List<Vector2> startPositions;
    public int shuffleCount = 2;

    // クリアフラグ
    bool isClear;
    GameObject txtInfo;

    // Start is called before the first frame update
    void Start()
    {
        txtInfo = GameObject.Find("Text");
        txtInfo.SetActive(false);

        // タイル関連
        tiles = new List<GameObject>();
        startPositions = new List<Vector2>();

        for (int i = 0; i < 16; i++)
        {
            GameObject obj = GameObject.Find("" + i);
            tiles.Add(obj);

            // 正解のポジション（初期状態）
            startPositions.Add(obj.transform.position);
        }

        // シャッフルの処理
        for (int i = 0; i < shuffleCount; i++)
        {
            // 動かせるオブジェクト
            List<GameObject> moves = new List<GameObject>();

            // 全部のオブジェクト
            foreach (GameObject obj in tiles)
            {
                // 0に隣接しているオブジェクト
                if (null != getExTile(obj))
                {
                    moves.Add(obj);
                }
            }

            if (0 < moves.Count)
            {
                // ランダムで1つ動かす
                int rnd = Random.Range(0, moves.Count);
                GameObject tile0 = getExTile(moves[rnd]);

                changeTile(moves[rnd], tile0);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (isClear) return;

        // クリア判定
        isClear = true;
        for (int i = 0; i < tiles.Count; i++)
        {
            Vector2 pos = tiles[i].transform.position;
            if (startPositions[i] != pos)
            {
                isClear = false;
            }
        }

        if (isClear)
        {
            txtInfo.SetActive(true);

            // 弾ける処理
            for (int i = 0; i < tiles.Count; i++)
            {
                float x = Random.Range(-30, 30);
                float y = Random.Range(-30, 30);
                tiles[i].AddComponent<Rigidbody2D>().velocity = new Vector2(x, y);
            }
        }

        // タッチ処理
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 10);

            if (hit.collider)
            {
                GameObject hitobj = hit.collider.gameObject;
                GameObject target = getExTile(hitobj);

                changeTile(hitobj, target);
            }
        }
    }

    // 入替可能なタイルを見つけた場合、それを返す
    GameObject getExTile(GameObject tile)
    {
        GameObject ret = null;

        Vector2 posa = tile.transform.position;

        foreach (GameObject obj in tiles)
        {
            Vector2 posb = obj.transform.position;

            float dist = Vector2.Distance(posa, posb);

            if (1 == dist && obj.name.Equals("0"))
            {
                ret = obj;
            }
        }

        return ret;
    }

    // 場所を入れ替える
    void changeTile(GameObject tilea, GameObject tileb)
    {
        if (null == tilea || null == tileb) return;

        // ポジション更新
        Vector2 tmp = tilea.transform.position;
        tilea.transform.position = tileb.transform.position;
        tileb.transform.position = tmp;
    }

    // シーン再読み込み
    public void Retry()
    {
        SceneManager.LoadScene("PuzzleGameScene");
    }
}

