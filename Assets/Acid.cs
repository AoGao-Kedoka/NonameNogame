using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : MonoBehaviour
{
    public bool overriden = false;
    public float pushbackForce = 100f;

    public void Override()
    {
        this.overriden = true;
        //animation?

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(overriden && other.gameObject.CompareTag("ChasingRobot"))
        {
            other.GetComponent<Rigidbody2D>().AddForce(new Vector2(-pushbackForce, 0f));

        }
        else if (!overriden && other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<CharacterController>().Die();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {

        if (overriden && other.gameObject.CompareTag("ChasingRobot"))
        {
            other.GetComponent<Rigidbody2D>().AddForce(new Vector2(-pushbackForce, 0f));

        }

    }
}
