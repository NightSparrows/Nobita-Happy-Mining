using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] GameObject bullet1 = null;

    private Rigidbody rig;
    
    [SerializeField] float speed = 5f;
    [SerializeField] float hp = 100f;
    public float atk = 1f;
    int level = 1;
    float exp = 1f;

    Health health;
    Attack attack;
    Defense defense;


    float horizontalMove;
    float verticalMove;

    private void Awake()
    {
        health = GetComponent<Health>();
        attack = GetComponent<Attack>();
        defense = GetComponent<Defense>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //rig的數值從unity的Rigidbody取得(前面添加的Rigidbody)
        rig = GetComponent<Rigidbody>();
        //反覆的呼叫"Attack"一秒後開始,反覆時間為一秒
        //InvokeRepeating("Attack", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        // horizontalMove的值從unity設定水平移動的數值
        //( Input.GetAxisRaw("Horizontal")為按下AD或左右傳入-1~1的數值出來)
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");
    }

    //每個物理幀執行一次
    private void FixedUpdate()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        rig.velocity =
           //new Vector2(horizontalMove * speed, rig.velocity.y);
           //new Vector3(horizontalMove * speed, rig.velocity.y, verticalMove * speed);
           //new Vector3(horizontalMove * speed, 0, verticalMove * speed);
           new Vector3(horizontalMove, 0, verticalMove).normalized * speed;

        Vector3 dir = transform.position + rig.velocity;
        //transform.LookAt(dir);
    }

    void Attack()
    {
        //生成物件(物件名稱,物件位置,物件角度)
        Instantiate(bullet1, transform.position, Quaternion.identity);
    }

}
