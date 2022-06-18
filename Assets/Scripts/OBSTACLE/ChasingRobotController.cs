using System;
using System.Collections;
using UnityEngine;

public class ChasingRobotController : MonoBehaviour
{
    [SerializeField] private float chasingSpeed;
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
        if (Math.Abs(transform.position.x - player.transform.position.x) >= 300f)
        {
            var instance = Instantiate(this, new Vector3((player.transform.position.x - distanceToPlayer), transform.position.y, transform.position.z),
                Quaternion.identity);
            // wait for 2 seconds till the collider activate again
            StartCoroutine(instance.DisableCollider());
            // reset the layer collision between obstacle and chasing robot to disabled
            Physics2D.IgnoreLayerCollision(8, this.gameObject.layer, false);
            Destroy(this);
        }
    }

    IEnumerator DisableCollider()
    {
        var collider = this.GetComponent<Collider2D>();
        collider.enabled = false;
        var renderer = this.GetComponent<Renderer>();
        //blinking 
        for (int i = 0; i < 10; i++)
        {
            renderer.enabled = false;
            yield return new WaitForSeconds(0.2f);
            renderer.enabled = true;
        }
        collider.enabled = true; 
    }


}
