using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBuff : MonoBehaviour
{
    protected void AddTo(BuffRecipient recipient)
    {
        recipient.RecieveBuff(this);
    }
}
