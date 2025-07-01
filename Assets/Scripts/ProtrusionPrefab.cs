using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtrusionPrefab : MonoBehaviour
{
    private GameManager gameManager;

    // 衝突の検出
    private void OnCollisionEnter2D(Collision2D collision){
        // Playerという名前のオブジェクトに衝突した際，ゲームオーバーとする
        if(collision.gameObject.name == "Player"){
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            gameManager.GameOver();
        }
    }
}