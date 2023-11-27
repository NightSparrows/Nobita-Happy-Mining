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
        //反覆的呼叫"CreatEnemy"一秒後開始,反覆時間為一秒
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
            //生成物件(物件名稱,物件位置,物件角度)
            GameObject enemy = Instantiate(enemy1, transform.position, Quaternion.identity);
            //enemy.GetComponent<Enemy>().SetPlayer(player);
        }
    }
}
