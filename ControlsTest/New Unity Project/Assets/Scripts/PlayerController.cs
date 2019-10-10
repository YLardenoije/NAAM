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
                CurrentGrapple.Source = player.gameObject;
            }
            else
            {
                Destroy(CurrentGrapple.gameObject);

                CurrentGrapple = null;
            }
        }
    }
}
