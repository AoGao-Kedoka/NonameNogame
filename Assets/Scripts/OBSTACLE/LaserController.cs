using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
// Initially aimed at right side, continously kill player if he goes to the right
// after override shoot one beam to the left

public class LaserController : Obstacle
{
    [SerializeField] private Vector3 defDistanceRay = new Vector3 (100f, 0, 0);

    public Transform laserFirePoint;
    public LineRenderer lineRenderer;
    Transform _transform;
    public GameObject bulletPrefab;

    // Start is called before the first frame update
    private void Awake()
    {
        this._transform = GetComponent<Transform>();
        lineRenderer.widthMultiplier = 10f;
    }

    // Update is called once per frame
    void Update()
    {
            if (!this.overriden)
            {
                KillPlayer();
            }
    }

    [ContextMenu("Interact Pressed")]
    public override void SetOverride()
    {
        overriden = true;
        KillEnemies();
        Destroy(this.GetComponent<LineRenderer>());
    }

    private void KillPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(this._transform.position, transform.right, 100f);
        bool found = false;
        if (hit.collider.gameObject.CompareTag("Player"))
        {
            found = true;
        }
        if(found)
        {
            Draw2DRay(laserFirePoint.position, hit.point);
            hit.collider.gameObject.GetComponent<CharacterController>().Die();
        }
        else
        {
            Draw2DRay(laserFirePoint.position, laserFirePoint.position + this.defDistanceRay);
        }
    }


    

    private void KillEnemies()
    {
        ///his.transform.parent.RotateAround(transform.position, transform.parent.up, 180f);
        this.transform.parent.localScale *= -1;//(transform.position, transform.parent.up, 180f);
        //Vector3 position = this.GetComponent<Renderer>().bounds.center;

       // this.transform.RotateAround(position, new Vector3(0f, 180f,0f), 10 * Time.deltaTime);
        Instantiate(this.bulletPrefab, this.laserFirePoint.position, laserFirePoint.rotation);
    }

    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        this.lineRenderer.SetPosition(0, startPos);
        this.lineRenderer.SetPosition(1, endPos);
        
    }
}
