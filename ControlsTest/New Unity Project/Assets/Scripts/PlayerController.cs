using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

// TODO: REFACTOR

[RequireComponent(typeof (Transform))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] EventSystem ES;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GlobalData GlobalData;
    [SerializeField] int simultaneousTouches = 5;
    [SerializeField] private Player player;
    [SerializeField] private float minDragDistance = Screen.width * 0.1f;

    public float MovementSpeed = 10;
    public Projectile GrapplePrefab, FireBallPrefab;
    public Projectile CurrentGrapple;

    private Transform tf;
    private Rigidbody2D rb;    
    private bool Grappling = false;
    private Vector2 grapplepoint;
    private Vector2[] FirstTouchPositions, lastTouchPositions;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        FirstTouchPositions = new Vector2[simultaneousTouches];
        lastTouchPositions = new Vector2[simultaneousTouches];
        tf = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        if (mainCamera == null )
        {
            mainCamera = FindObjectOfType<Camera>();
        }
        if( ES == null )
        {
            ES = FindObjectOfType<EventSystem>();
        }

    }
    // Update is called once per frame
    void Update()
    {
        rb.AddForce(new Vector2(Input.acceleration.x*MovementSpeed, 0)); //tilting should be working like this? // i guess it does C:
        HandleTouches();
    }
    void Fire( Vector2 _EndPoint, bool IsCombatItem)
    {   
        if(CurrentGrapple!=null)
        {
            Destroy(CurrentGrapple.gameObject);
            CurrentGrapple = null;
        }
        if (IsCombatItem)
        {
            if (FireBallPrefab != null )
            {
                FireProjectile(_EndPoint);
            }
        }
        else
        {
            if( GrapplePrefab != null )
            {
                FireGrapple(_EndPoint);
            }
        }         
    }
    void HandleTouches() //huehuehue
    {
        int i = 0;
        foreach (Touch t in Input.touches)
        {

            if (t.phase == TouchPhase.Began)
            {
              
                if (!EventSystem.current.IsPointerOverGameObject(t.fingerId))
                {
                    FirstTouchPositions[i] = t.position;
                    lastTouchPositions[i] = t.position;
                }
            }
            else if (t.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lastTouchPositions[i] = t.position;
            }
            else if (t.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lastTouchPositions[i] = t.position;  //last t position. Ommitted if you use list

                Vector2 Endpoint = mainCamera.ScreenToWorldPoint(t.position);
                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs((lastTouchPositions[i]-FirstTouchPositions[i]).magnitude) > minDragDistance)
                {       
                    //It's a drag
                    Fire(Endpoint, IsCombatItem: false);
                }
                else
                {
                    Fire(Endpoint, IsCombatItem: true );
                }
            }
                i++;
            
        }
    }
    void FireGrapple(Vector2 endpoint)
    {
        switch (GlobalData.SelectedMovementItem)
        {
            case GlobalData.MovementItems.Grapple:
                if (CurrentGrapple == null)
                {
                    CurrentGrapple = Instantiate(GrapplePrefab, transform.position, transform.rotation);
                    CurrentGrapple.SetValues(endpoint, player.gameObject);
                }
               
                break;
        }

    }
    void FireProjectile(Vector2 endpoint)
    {
        switch (GlobalData.SelectedCombatItem)
        {
            case GlobalData.CombatItems.FireBall:
                Projectile FB = Instantiate(FireBallPrefab, transform.position, transform.rotation);
                FB.SetValues(endpoint, player.gameObject);
                break;
        }
    }
}
