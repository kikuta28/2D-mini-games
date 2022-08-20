using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class PuzzleGameDirector : MonoBehaviour
{
    // タイル関連
    List<GameObject> tiles;
    List<Vector2> startPositions;
    List<Vector2> tileHistory;
    public int shuffleCount = 2;
    public Text answerText;
    public GameObject NextAnswerPosition;


    // クリアフラグ
    bool isClear;
    bool isShownAnswer = false;
    GameObject textInfo;

    // Start is called before the first frame update
    void Start()
    {
        answerText.text = "";
        NextAnswerPosition.SetActive(false);

        textInfo = GameObject.Find("Text");
        textInfo.SetActive(false);

        // タイル関連
        tiles = new List<GameObject>();
        startPositions = new List<Vector2>();
        tileHistory = new List<Vector2>();

        for (int i = 0; i < 16; i++)
        {
            GameObject gameObject = GameObject.Find("" + i);
            tiles.Add(gameObject);

            // 正解のポジション（初期状態）
            startPositions.Add(gameObject.transform.position);
        }

        // シャッフルの処理
        for (int i = 0; i < shuffleCount; i++)
        {
            // 動かせるオブジェクト
            List<GameObject> moves = new List<GameObject>();


            // 全部のオブジェクト
            foreach (GameObject gameObject in tiles)
            {
                // 0に隣接しているオブジェクト
                if (null != getReplaceableTile(gameObject))
                {
                    moves.Add(gameObject);
                }
            }

            if (0 < moves.Count)
            {
                // ランダムで1つ動かす
                int random = Random.Range(0, moves.Count);
                GameObject tile0 = getReplaceableTile(moves[random]);

                changeTile(moves[random], tile0);

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
            Vector2 position = tiles[i].transform.position;
            if (startPositions[i] != position)
            {
                isClear = false;
            }
        }

        if (isClear)
        {
            textInfo.SetActive(true);

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
                GameObject hitObject = hit.collider.gameObject;
                GameObject target = getReplaceableTile(hitObject);

                changeTile(hitObject, target);
            }
        }
    }

    // 入替可能なタイルを見つけた場合、それを返す
    GameObject getReplaceableTile(GameObject tile)
    {
        GameObject result = null;

        Vector2 positionA = tile.transform.position;

        foreach (GameObject gameObject in tiles)
        {
            Vector2 positionB = gameObject.transform.position;

            float dist = Vector2.Distance(positionA, positionB);

            if (1 == dist && gameObject.name.Equals("0"))
            {
                result = gameObject;
            }
        }

        return result;
    }

    // 場所を入れ替える
    void changeTile(GameObject tileA, GameObject tileB)
    {
        if (null == tileA || null == tileB) return;

        // ポジション更新
        Vector2 tileAPosition = tileA.transform.position;
        tileA.transform.position = tileB.transform.position;
        tileB.transform.position = tileAPosition;
        if (!isShownAnswer)
        {
            tileHistory.Add(tileAPosition);

            Debug.Log(string.Join(",", tileHistory.Select(t => " x:" + t.x + " y: " + t.y + "\n")));
        }
    }

    // シーン再読み込み
    public void Retry()
    {
        SceneManager.LoadScene("PuzzleGameScene");
    }
    int answerCount = 1;
    public void OutputAnswer()
    {
        int lastIndex = tileHistory.Count();
        Vector2 answer = tileHistory[lastIndex - answerCount];
        answerCount++;
        NextAnswerPosition.transform.position = answer;
        NextAnswerPosition.SetActive(true);
        Debug.Log(string.Join(",", tileHistory.Select(t => " x:" + t.x + " y: " + t.y + "\n")));
        answerText.text = answerCount + "番目の座標" + answer;
        isShownAnswer = true;
    }

}
