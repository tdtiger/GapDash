using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    // 壁の突起部分
    [SerializeField]
    GameObject protrusionPrefab;

    [SerializeField]
    GameObject gameManager;

    // 移動前の位置記録用
    private Vector3 startPos;

    private Rigidbody2D rb;

    // 移動速度
    private float wallSpeed = 2f;
    public float WallSpeed{
        get {
            return wallSpeed;
        }
        set {
            wallSpeed = value;
        }
    }

    // 動作中かを表すフラッグ
    private bool isMoving = false;
    public bool IsMoving{
        get{
            return isMoving;
        }
        set{
            isMoving = value;
        }
    }

    // 突起を格納する配列．現時点では最大30個に設定
    private GameObject[] protrusions = new GameObject[30];

    // 突起の生成位置を格納する配列
    private float[] spawnXpositions;

    // 現在のレベル
    private int level = 1;

    // レベル更新制御用
    private int cnt = 0;

    void Start(){
        // 各種変数の取得
        startPos = this.transform.position;
        rb = this.GetComponent<Rigidbody2D>();

        InitializeSpawnPositions();
    }

    void Update(){
        // 位置を逐次チェックし，移動方向を制御
        if(this.transform.position.y <= -1.5f)
            Move(wallSpeed);
        else if(this.transform.position.y > startPos.y + 1.5f){
            Move(-wallSpeed);
            DestroyProtrusion();
            UpdateLevel();
            SpawnProtrusion();
        }
    }

    private void InitializeSpawnPositions(){
        spawnXpositions = new float[20];
        float startX = -7.6f;
        float step = 0.8f;

        for (int i = 0; i < 20; i++)
            spawnXpositions[i] = startX + i * step;
    }
    public void SpawnProtrusion(){
        // 生成する突起の数
        int spawnCount = Mathf.Min(5 + level, spawnXpositions.Length - 1);

        // 重複防止のためのインデックス保存配列
        List<int> usedIndices = new List<int>();

        for (int i = 0; i < spawnCount; ++i){
            int index;
            int tryCount = 0;
            // まだ突起が存在しない領域が見つかるまで繰り返し(最大100回)
            do{
                index = Random.Range(0, spawnXpositions.Length);
                tryCount++;
            } while (usedIndices.Contains(index) && tryCount < 100);

            usedIndices.Add(index);
            float x = spawnXpositions[index];

            Vector3 spawnPosition = new Vector3(x, this.transform.position.y - 1.5f, 0f);

            // 突起を生成
            GameObject obj = Instantiate(protrusionPrefab, spawnPosition, Quaternion.identity, this.transform);
            // 削除用タグを付加
            obj.tag = "Protrusion";
            // スケールの調整
            obj.transform.localScale = new Vector3(0.045f, 1f, 1f);
        }
        Move(-wallSpeed);
    }

    private void DestroyProtrusion(){
        // 子オブジェクトとして存在する突起を全て削除
        foreach (Transform child in transform){
            if (child.CompareTag("Protrusion"))
                Destroy(child.gameObject);
        }
    }

    private void Move(float wallSpeed){
        // 速度をwallSpeedにする
        rb.velocity = new Vector3(0, wallSpeed, 0);
    }

    private void UpdateLevel(){
        // 2回に1回，レベルと壁の速度が上がる
        cnt += 1;
        if(cnt == 2){
            level += cnt;
            wallSpeed += 0.2f;
            cnt = 0;
        }
    }
}