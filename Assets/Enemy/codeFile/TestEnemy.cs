using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    [SerializeField] GameObject attackArea;
    GameObject player;
    Vector3 dir;

    private Rigidbody rig;

    Attack attack;

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();

        attack = GetComponent<Attack>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.player;
        SetDirection();
        StartCoroutine(DashAttack());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetDirection()
    {
        Vector3 p1 = transform.position;
        Vector3 p2 = player.transform.position;

        dir = (p2 - p1).normalized;
    }

    IEnumerator DashAttack()
    {
        //yield return new WaitForSeconds(1f);
        GameObject area = Instantiate(attackArea, transform.position, Quaternion.identity);
        area.transform.Translate(0, -transform.position.y, 0);
        Vector3 pos = player.transform.position;
        pos.y = transform.position.y;
        transform.LookAt(pos);
        pos.y = 0;
        area.transform.LookAt(pos);
        area.transform.localScale += new Vector3(0, 0, 2);
        yield return new WaitForSeconds(1f);
        Destroy(area);
        // TODO:MOVE
        GetComponent<ForwardMovement>().enabled = true;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
