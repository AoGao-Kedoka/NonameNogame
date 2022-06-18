using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ChasingRobotController : MonoBehaviour
{
    public float chasingSpeed;
    [SerializeField] private Transform player;
    private bool pushingBack = false;

    // Update is called once per frame
    void Update()
    {
        if (!pushingBack)
        {
            this.transform.position += new Vector3(chasingSpeed, 0, 0) * Time.deltaTime;
        }
    }

    public void PushedBack()
    {
        if (pushingBack == false)
        {
            pushingBack = true;
            this.transform.DOMoveX(transform.position.x - 100, 2).OnComplete(() =>
            {
                this.transform.DOMoveX(transform.position.x + 100, 2).OnComplete(() =>
                {
                    pushingBack = false;
                });
            });
        }
    }
}
