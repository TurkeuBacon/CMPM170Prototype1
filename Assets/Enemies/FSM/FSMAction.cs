using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSMAction : MonoBehaviour
{
    public abstract void Act();

    public abstract void Update();
}
