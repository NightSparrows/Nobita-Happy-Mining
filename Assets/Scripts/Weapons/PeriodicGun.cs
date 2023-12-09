using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CoolDown))]
public abstract class PeriodicGun : Gun
{
    public float coolDown { get; set; }

    //private Coroutine _shootCoroutine;

    private CoolDown _coolDown;

    protected virtual void Awake()
    {
        if (_coolDown == null)
            _coolDown = GetComponent<CoolDown>();
    }

    private void Start()
    {
        //_shootCoroutine = StartCoroutine(ShootPeriodically());
        StartCoroutine(ShootPeriodically());
    }

    private IEnumerator ShootPeriodically()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(_coolDown.value);
        }
    }
}
