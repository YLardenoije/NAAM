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
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject LeaveUI;
    public float MovementSpeed = 10;
    public Projectile GrapplePrefab, FireBallPrefab, ScatterPrefab;
    public Projectile CurrentGrapple;

    private Transform tf;
    private Rigidbody2D rb;    
    private bool Grappling = false;
    private Vector2 grapplepoint;
    private Vector2[] FirstTouchPositions, lastTouchPositions;
    private float AttackCooldown;
    private float TimeSinceLastAttack = 0;
    
    
    
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

        if( GlobalData.SelectedCombatItem == GlobalData.CombatItems.Scatter )
        {
            AttackCooldown = 1.5f;
        }
        else if( GlobalData.SelectedCombatItem == GlobalData.CombatItems.FireBall )
        {
            AttackCooldown = 0.4f;
        }

    }
    // Update is called once per frame
    void Update()
    {
        float tilt = Input.acceleration.x * MovementSpeed;
        if (tilt > 0.8)
        {
            animator.SetInteger("Dir", 1);
        }
        else if (tilt < -0.8)
        {
            animator.SetInteger("Dir", -1);
        }
        else
        {
            animator.SetInteger("Dir", 0); 

        }
        TimeSinceLastAttack += Time.deltaTime;
        rb.AddForce(new Vector2(tilt, 0)); //tilting should be working like this? // i guess it does C:
        HandleTouches();
        if( Input.GetKeyDown(KeyCode.Escape) )
        {
            LeaveUI.SetActive(!LeaveUI.activeSelf);
        }
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
            FireProjectile(_EndPoint);
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
                    if( TimeSinceLastAttack > AttackCooldown )
                    {
                        Fire(Endpoint, IsCombatItem: true );
                        TimeSinceLastAttack = 0;
                    }
                   
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
                if (FireBallPrefab != null )
                {
                    Projectile FB = Instantiate(FireBallPrefab, transform.position, transform.rotation);
                    FB.SetValues(endpoint, player.gameObject);
                }
                break;
            case GlobalData.CombatItems.Scatter:
                if (ScatterPrefab != null )
                {
                    Projectile Sc = Instantiate(ScatterPrefab, transform.position, transform.rotation);
                    Sc.GetComponent<Scatter>().Source = player.gameObject;
                    Sc.GetComponent<Rigidbody2D>().AddRelativeForce(endpoint.normalized, ForceMode2D.Force);
                }
                break;
        }
    }
}
