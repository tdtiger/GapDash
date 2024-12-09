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

    private Vector3 startPos;
    private Rigidbody2D rb;

    private Transform tf;

    // 落下速度
    private float wallSpeed = 2f;

    public float wallspeed{
        get {
            return wallSpeed;
        }
        set {
            wallSpeed = value;
        }
    }

    private bool isMoving = false;
    public bool IsMoving{
        get{
            return isMoving;
        }
        set{
            isMoving = value;
        }
    }

    // 突起を格納する配列．現時点では最大20個に設定
    private GameObject[] protrusions = new GameObject[30];
    // 現在のレベル
    private int level = 1;

    private int cnt = 0;

    // Start is called before the first frame update
    void Start()
    {
        startPos = this.transform.position;
        rb = this.GetComponent<Rigidbody2D>();
        tf = this.GetComponent<Transform>();
        //SpawnProtrusion();
    }

    // Update is called once per frame
    void Update()
    {
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
            float randomX = Random.Range(-8.4f, 8.4f);
            protrusions[i] = Instantiate(protrusionPrefab, this.transform.position - new Vector3(randomX, 1.75f, 0f), Quaternion.identity);
        }
        Move(-wallSpeed);
    }

    private void DestroyProtrusion(){
        for(int i = 0; i < 10 + level; ++i){
            Destroy(protrusions[i].gameObject);
        }
    }

    private void Move(float wallSpeed){
        rb.velocity = new Vector3(0, wallSpeed, 0);
    }

    private void Stop(){
        rb.velocity = new Vector3(0, 0, 0);
    }

    private void UpdateLevel(){
        cnt += 1;
        if(cnt == 2){
            level += cnt;
            wallSpeed += 0.15f;
            cnt = 0;
        }
    }
}