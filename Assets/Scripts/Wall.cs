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

    private Transform tf;

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

    // 現在のレベル
    private int level = 1;

    // レベル更新制御用
    private int cnt = 0;

    // Start is called before the first frame update
    void Start()
    {
        // 各種変数の取得
        startPos = this.transform.position;
        rb = this.GetComponent<Rigidbody2D>();
        tf = this.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // 位置を逐次チェックし，移動方向を制御
        if(this.transform.position.y <= -1.5f)
            Move(wallSpeed);
        else if(this.transform.position.y > startPos.y){
            Move(-wallSpeed);
            DestroyProtrusion();
            UpdateLevel();
            SpawnProtrusion();
        }
    }

    public void SpawnProtrusion(){
        // 10 + 現在のレベル の数だけ突起を生成
        for(int i = 0; i < 10 + level; ++i){
            if(i > protrusions.length){
                break;
            }

            float randomX = Random.Range(-8.4f, 8.4f);
            protrusions[i] = Instantiate(protrusionPrefab, this.transform.position - new Vector3(randomX, 1.75f, 0f), Quaternion.identity);
        }
        Move(-wallSpeed);
    }

    private void DestroyProtrusion(){
        // 凸部分を全て削除
        for(int i = 0; i < 10 + level; ++i){
            if(i > protrusions.length){
                break;
            }

            Destroy(protrusions[i].gameObject);
        }
    }

    private void Move(float wallSpeed){
        // 速度をwallSpeedにする
        rb.velocity = new Vector3(0, wallSpeed, 0);
    }

/*
    private void Stop(){
        rb.velocity = new Vector3(0, 0, 0);
    }
*/

    private void UpdateLevel(){
        2回に1回，レベルと壁の速度が上がる
        cnt += 1;
        if(cnt == 2){
            level += cnt;
            wallSpeed += 0.15f;
            cnt = 0;
        }
    }
}