using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtrusionPrefab : MonoBehaviour
{
    private GameManager gameManager;

    private Wall wall;

    private Rigidbody2D rb;

    // 移動速度
    private float wallSpeed;

    // Start is called before the first frame update
    void Start()
    {
        // 各種変数を取得後，移動開始
        wall = GameObject.Find("Wall").GetComponent<Wall>();
        wallSpeed = wall.WallSpeed;
        rb = this.GetComponent<Rigidbody2D>();
        Move(-wallSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        // 地面に達したとき，引き返す
        if(this.transform.position.y <= -3.23f)
            Move(wallSpeed);
    }

    private void Move(float wallSpeed){
        // 速度をwallSpeedに
        rb.velocity = new Vector3(0, wallSpeed, 0);
    }

/*
    private void Stop(){
        rb.velocity = new Vector3(0, 0, 0);
    }
*/

    // 衝突の検出
    private void OnCollisionEnter2D(Collision2D collision){
        // Playerという名前のオブジェクトに衝突した際，ゲームオーバーとする
        if(collision.gameObject.name == "Player"){
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            gameManager.GameOver();
        }
    }
}