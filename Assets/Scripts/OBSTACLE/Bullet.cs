using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public float speed = 80f;
    [SerializeField] public Rigidbody2D rb;
     private float pushbackForce = 70f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.transform.localScale = new Vector3(20.0f, 10.0f, 10.0f);
        rb.velocity = -transform.right * speed; 
        this.rb.gravityScale = 0;
        rb.isKinematic = true;
        //rb.detectCollisions = false;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {

        if (hitInfo.gameObject.CompareTag("ChasingRobot"))
        {
            hitInfo.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-pushbackForce, 0f));

            Destroy(gameObject, 2f);
        }
    }

}
