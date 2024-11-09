using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // プレイヤーの移動量
    [SerializeField]
    private int moveSpeed;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // 横方向のキー入力を受け付け
        float horizontal = Input.GetAxis("Horizontal");
        // プレイヤーに速度を与え，動かす
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
    }
}