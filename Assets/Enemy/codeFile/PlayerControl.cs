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
        //rig���ƭȱqunity��Rigidbody���o(�e���K�[��Rigidbody)
        rig = GetComponent<Rigidbody>();
        //���Ъ��I�s"Attack"�@���}�l,���Юɶ����@��
        //InvokeRepeating("Attack", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        // horizontalMove���ȱqunity�]�w�������ʪ��ƭ�
        //( Input.GetAxisRaw("Horizontal")�����UAD�Υ��k�ǤJ-1~1���ƭȥX��)
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");
    }

    //�C�Ӫ��z�V����@��
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
        //�ͦ�����(����W��,�����m,���󨤫�)
        Instantiate(bullet1, transform.position, Quaternion.identity);
    }

}
