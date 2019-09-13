using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Transform))]
public class Projectile : MonoBehaviour
{
    public bool IsFired = true;
    private Transform tf;

    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
    }
    
    //public void Fire(Vector2 From, Vector2 EndPoint, bool EndAtEndPoint)
    //{


    //}
    public void MovementThisFrame(Vector2 EndPoint, bool EndAtEndPoint, int TicksPerUnit = 1)//fires from current location
    {

        Vector2 vec  = (EndPoint - new Vector2(tf.position.x, tf.position.y)) ;

    }
    // Update is called once per frame
    void Update()
    {
        if (IsFired)
        {
           
        }
    }
}
