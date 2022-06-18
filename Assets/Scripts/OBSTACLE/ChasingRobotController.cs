using System;
using System.Collections;
using UnityEngine;

public class ChasingRobotController : MonoBehaviour
{
    public float chasingSpeed;
    [SerializeField] private Transform player;

    private float distanceToPlayer;

    private void Start()
    {
        distanceToPlayer = player.position.x - this.transform.position.x;
    }
    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(chasingSpeed, 0, 0) * Time.deltaTime;
    }
}
