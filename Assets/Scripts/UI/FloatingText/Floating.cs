/*
 * Refrence: https://www.youtube.com/watch?v=KOt85IoD__4
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Floating : MonoBehaviour
{
    public AnimationCurve opacityCurve;
    public AnimationCurve moveSpeedCurve;

    public float moveDistance = 1f;

    public float lifetime
    {
        get { return 1f / timeMul; }
        set { timeMul = 1f / lifetime; }
    }

    [SerializeField] private TextMeshProUGUI text;

    private float timeMul = 1f;
    private float time = 0f;

    private Vector3 orgPosition;
    private Vector3 endPosition;

    private void Start()
    {
        Vector3 moveDir = Random.insideUnitSphere.normalized;

        orgPosition = transform.position;
        endPosition = orgPosition + moveDistance * moveDir;
    }

    private void Update()
    {
        Color color = text.color;
        color.a = opacityCurve.Evaluate(timeMul * time);
        text.color = color;

        float fspeed = moveSpeedCurve.Evaluate(timeMul * time);
        transform.position = (1 - fspeed) * orgPosition + fspeed * endPosition;

        time += Time.deltaTime;
    }
}
