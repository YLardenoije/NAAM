using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[RequireComponent(typeof (Transform))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private Transform tf;
    private Rigidbody2D rb;
    public float MovementSpeed = 10;
    private bool Grappling = false;
    private Vector2 grapplepoint;

    [SerializeField] private Player player;
    [SerializeField] private Grapple GrapplePrefab, CurrentGrapple;
    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        if (mainCamera == null)
        {
            mainCamera = FindObjectOfType<Camera>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(new Vector2(Input.acceleration.x*MovementSpeed, 0)); //tilting should be working like this? // i guess it does C:

        grappleStart();

    

    }
    //just test touches and raycasts, should be changed to add force in this direction because we want that rubber band effect.
    void grappleStart()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 EndPoint = mainCamera.ScreenToWorldPoint(touch.position);
            if (CurrentGrapple == null)
            {
                CurrentGrapple = Instantiate(GrapplePrefab, transform.position, transform.rotation);
                CurrentGrapple.Target = EndPoint;
                CurrentGrapple.Source = player;
            }
            else
            {
                Destroy(CurrentGrapple.gameObject);

                CurrentGrapple = null;
            }

            //This code should be useless now. If not, i fucked up. 
            /*
            float distance = (float)Math.Sqrt(Math.Pow(EndPoint.x - tf.position.x, 2.0) + Math.Pow(EndPoint.y - tf.position.y, 2.0));

            int layerMask = 0;
            layerMask = 1 << LayerMask.NameToLayer("Ground");
            RaycastHit2D hit = Physics2D.Raycast(tf.position, EndPoint - new Vector2(tf.position.x,tf.position.y), distance, layerMask);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider);
                grapplepoint = hit.point;
                Vector2 pos2D = new Vector2(tf.position.x, tf.position.y);
                rb.AddForce((hit.point-pos2D)*GrapplePower, ForceMode2D.Force);
                Debug.DrawLine(tf.position, new Vector3(EndPoint.x, EndPoint.y,0),Color.red,4, false);
                Debug.DrawLine(tf.position, new Vector3(hit.point.x,hit.point.y, 0), Color.green, 4, false);
                Debug.Log("hit a wall at " + hit.point);
                Grappling = true;
                
            }
            else 
            {
                Debug.Log("raycast did not hit a wall");
            }
            */
        }
    }

    void duringGrapple()
    {
        /*
        if(Input.touchCount <= 0 || Input.GetTouch(0).phase != TouchPhase.Began)
        {
           // rb.AddForce( (grapplepoint-new Vector2(tf.position.x,tf.position.y))* GrapplePower, ForceMode2D.Impulse);
        }
        else
        {
            Grappling = false;
        }
        */
    }

}
