using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManger : MonoBehaviour
{

    [SerializeField] public GameObject player = null;

    [SerializeField] GameObject enemy1 = null;
    [SerializeField] GameObject enemy2 = null;


    [SerializeField] GameObject summonArea;

    [SerializeField] float creatRate = 3f;
    [SerializeField] int numOfEnemy1 = 3;

    public WavePack waves;

    // Start is called before the first frame update
    void Start()
    {
        //反覆的呼叫"CreatEnemy"一秒後開始,反覆時間為一秒
        //InvokeRepeating("CreatEnemy", 0f, 5f);
        //InvokeRepeating("CreatEnemy", 0f, creatRate);

        //Invoke("Wave1", 5f);
        //Invoke("Wave2", 12f);
        //InvokeRepeating("Wave1", 5f, 20f);
        GenerateWaves();
    }

    void GenerateWaves()
    {
        foreach (var wave in waves.waves)
        {
            StartCoroutine(GenerateWave(wave));
        }
    }

    IEnumerator GenerateWave(WaveSO wave)
    {
        yield return new WaitForSeconds(wave.startTime);
        wave.Activate(this);
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

    void Wave1()
    {
        //StartCoroutine(CreatObject(enemy1, transform.position));
        //StartCoroutine(CreatObject(enemy1, player.transform.position));
        InvokeRepeating("addCorroutine1", 0f, 0.5f);
        StartCoroutine(EndInvoke1(2f));
    }

    void Wave2()
    {
        //StartCoroutine(CreatObject(enemy1, transform.position));
        //StartCoroutine(CreatObject(enemy1, player.transform.position));
        InvokeRepeating("addCorroutine2", 0f, 0.5f);
        StartCoroutine(EndInvoke2(2f));
    }

    void addCorroutine2()
    {
        StartCoroutine(CreatObject(enemy2, player.transform.position));
    }

    void addCorroutine1()
    {
        StartCoroutine(CreatObject(enemy1, player.transform.position));
    }

    IEnumerator CreatObject(GameObject gameObject, Vector3 pos)
    {
        GameObject area = Instantiate(summonArea, pos, Quaternion.identity);
        area.transform.Translate(0, -pos.y, 0);
        area.GetComponent<SummonArea>().SetCreature(gameObject);
        yield return new WaitForSeconds(1f);
        //Instantiate(gameObject, pos, Quaternion.identity);
    }


    IEnumerator EndInvoke1(float t)
    {
        yield return new WaitForSeconds(t);
        CancelInvoke("addCorroutine1");
    }

    IEnumerator EndInvoke2(float t)
    {
        yield return new WaitForSeconds(t);
        CancelInvoke("addCorroutine2");
    }
}
