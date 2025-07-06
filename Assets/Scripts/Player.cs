using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // プレイヤーの移動量
    [SerializeField]
    private float moveSpeed;
    private float dashMultiPlier = 1.5f;
    private float dashDuration = 0.5f;
    private float dashCoolDown = 3f;
    private bool isDash = false;
    private bool canDash = true;
    private float dashCoolDownTimer = 0f;

    private float keyIn;

    private Rigidbody2D rb;

    [SerializeField]
    private Image dashCoolDownUI;

    void Start(){
        rb = this.GetComponent<Rigidbody2D>();
        if (dashCoolDownUI != null)
            dashCoolDownUI.fillAmount = 0f;
    }

    void Update(){
        // 横方向のキー入力を受け付け
        keyIn = Input.GetAxis("Horizontal");
        if (!canDash){
            dashCoolDownTimer -= Time.deltaTime;
            if (dashCoolDownUI != null)
                dashCoolDownUI.fillAmount = 1 - (dashCoolDownTimer / dashCoolDown);

            if (dashCoolDownTimer <= 0f){
                canDash = true;
                if (dashCoolDownUI != null)
                    dashCoolDownUI.fillAmount = 0f;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && canDash)
            StartCoroutine(Dash());
    }

    void FixedUpdate(){
        float currentSpped = isDash ? moveSpeed * dashMultiPlier : moveSpeed;
        rb.velocity = new Vector2(keyIn * currentSpped, rb.velocity.y);
    }

    private IEnumerator Dash(){
        isDash = true;
        canDash = false;
        dashCoolDownTimer = dashCoolDown;

        if (dashCoolDownUI != null)
            dashCoolDownUI.fillAmount = 1f;

        yield return new WaitForSeconds(dashDuration);

        isDash = false;
        if (dashCoolDownUI != null)
            dashCoolDownUI.fillAmount = 0f;
    }
}