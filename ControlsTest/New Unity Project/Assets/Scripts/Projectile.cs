using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    public bool IsFired = true;
    private Transform tf;
    public Player Source; // the player who created the projectile
    public Vector2 Target;
    RaycastHit2D hit;
    public bool AtTarget = false;
    private Collider2D MyColl;
    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        int magicNumber = 100;
        int layermask = 1 << LayerMask.NameToLayer("Player");
        layermask = ~layermask;
        hit = Physics2D.Raycast(transform.position,
            Target - new Vector2(transform.position.x, transform.position.y),
            magicNumber, layermask);
    }
    

    public void MovementThisFrame(Vector2 EndPoint, bool EndAtEndPoint, int TicksPerUnit = 1)//fires from current location
    {

        Vector2 vec  = (EndPoint - new Vector2(tf.position.x, tf.position.y)) ;

    }
    // Update is called once per frame
    void Update()
    {
        if (! AtTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, hit.point, 10 * Time.deltaTime);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        
    }

}
