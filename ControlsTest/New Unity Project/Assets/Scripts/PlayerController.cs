using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

[RequireComponent(typeof (Transform))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] EventSystem ES;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GlobalData GlobalData;
    private Transform tf;
    private Rigidbody2D rb;
    public float MovementSpeed = 10;
    private bool Grappling = false;
    private Vector2 grapplepoint;

    [SerializeField] private Player player;
    [SerializeField] private Grapple GrapplePrefab, CurrentGrapple;
    [SerializeField] private FireBall FireBallPrefab;
    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        if (mainCamera == null)
        {
            mainCamera = FindObjectOfType<Camera>();
        }
        if (ES == null)
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
    void Fire( Vector2 _EndPoint)
    {
        Vector2 EndPoint = _EndPoint;
        switch (GlobalData.SelectedItemType)
        {
            case GlobalData.ItemTypes.MovementItem:
                switch (GlobalData.SelectedMovementItem)
                {
                    case GlobalData.MovementItems.Grapple:
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
                        break;
                }
                break;
                case GlobalData.ItemTypes.CombatItem:
                    switch( GlobalData.SelectedCombatItem )
                    {
                        case GlobalData.CombatItems.FireBall:
                            FireBall FB = Instantiate(FireBallPrefab, transform.position, transform.rotation);
                            FB.Target = EndPoint;
                            FB.Source = player.gameObject;
                        break;
                }
                break;
        }
    }
    void HandleTouches()
    {
        foreach (Touch t in Input.touches)
        {
            if (t.phase == TouchPhase.Began)
            {
                Vector2 Endpoint = mainCamera.ScreenToWorldPoint(t.position);
                if (!EventSystem.current.IsPointerOverGameObject(t.fingerId))
                {
                    Fire(Endpoint);
                    
                }
            }
        }
    }

}
