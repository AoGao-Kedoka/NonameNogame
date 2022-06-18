using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public bool overriden = false;
    public void SetOverride()
    {
        overriden = true;
        Debug.Log("override");
    }
}
