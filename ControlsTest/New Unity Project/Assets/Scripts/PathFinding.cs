using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;
    [SerializeField] private int MaxWanderDistance;
    [SerializeField] private float StickToDirectionChance;
    [SerializeField] private Vector3 StartPosition;
    [SerializeField] private Vector3 WanderGoal;
    [SerializeField] private bool GoingLeft;
    [SerializeField] private float waitTime = 0, waitedTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartPosition = gameObject.transform.position;
        WanderGoal = StartPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if( StartPosition == WanderGoal  && waitedTime >= waitTime )
        {
            Vector3 floorPos = new Vector3(transform.position.x + (GoingLeft ? -1 : 1), transform.position.y - 1, transform.position.z);
            Vector3 WallPos = new Vector3(transform.position.x + (GoingLeft ? -1 : 1), transform.position.y, transform.position.z);
            if (Physics.CheckSphere( floorPos , 0.2f) == false || Physics.CheckSphere( WallPos, 0.2f ) == true )
            {
                GoingLeft = !GoingLeft;
            }
            else
            {
                ConsiderTurn();
            }
            WanderGoal = new Vector3(transform.position.x + (GoingLeft ? -1 : 1), transform.position.y, transform.position.z);
        }
        else
        {
            transform.Translate( WanderGoal.normalized * Time.deltaTime * MoveSpeed, Space.World );
            if( (WanderGoal - transform.position).magnitude < 1 && (WanderGoal - transform.position).magnitude > -1 )
            {
                transform.position = WanderGoal;
            }
        }

        waitedTime += Time.deltaTime;
        waitTime = ConsiderWait();
        if (waitTime > 0)
        {
            waitedTime = 0;
            return;
        }
    }

    private void ConsiderTurn()
    {
        if( Random.Range(0, 1) > StickToDirectionChance )
        {
            GoingLeft = !GoingLeft;
        }
    }

    private float ConsiderWait()
    {
        if( Random.Range( 0, 1) < 0.3f )
        {
            return Random.Range(0f, 4f);
        }
        return 0;
    }
}
