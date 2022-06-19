using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class Magnet : Obstacle
{
    public int multiplier;
    public float pushbackForce = 100f;
    public float maxRangeLeft = 100f;
    public float maxRangeRight = 30f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PullEnemies();
    }

    [ContextMenu("Interact Pressed")]
    public override void SetOverride()
    {
        overriden = true;
        this.transform.localScale *= -1;

    }

    private void PullEnemies()
    {
        multiplier = 1;
        if (this.overriden == true)
        {
            multiplier = -1;
        }
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("ChasingRobot");
        for (int i = 0; i < enemies.Length; i++)
        {
            var magnetX = this.transform.position.x;
            var enemyX = enemies[i].transform.position.x;
            var distance = Math.Abs(magnetX - enemyX);
            if (magnetX - enemyX > 0) // robot to the left of magnet
            {
                if (distance <= this.maxRangeLeft)
                {
                    enemies[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(multiplier * pushbackForce, 0f));
                }
            }
            else //robot to the right of magnet
            {
                if (distance <= this.maxRangeRight)
                {
                    enemies[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(multiplier * pushbackForce, 0f));
                }
            }  
        }

    }
}
