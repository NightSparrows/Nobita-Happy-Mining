using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManger : MonoBehaviour
{

    [SerializeField] GameObject player = null;
    [SerializeField] GameObject enemy1 = null;
    
    [SerializeField] float creatRate = 3f;
    [SerializeField] int numOfEnemy1 = 3;

    // Start is called before the first frame update
    void Start()
    {
        //���Ъ��I�s"CreatEnemy"�@���}�l,���Юɶ����@��
        //InvokeRepeating("CreatEnemy", 0f, 5f);
        InvokeRepeating("CreatEnemy", 0f, creatRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreatEnemy()
    {
        // (has bug now)
        if (numOfEnemy1 > 0)
        {
            --numOfEnemy1;
            //�ͦ�����(����W��,�����m,���󨤫�)
            GameObject enemy = Instantiate(enemy1, transform.position, Quaternion.identity);
            //enemy.GetComponent<Enemy>().SetPlayer(player);
        }
    }
}
