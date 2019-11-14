using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;
    [SerializeField] private int MaxWanderDistance;
    [SerializeField] private float StickToDirectionChance, WaitChance;
    [SerializeField] private Vector3 StartPosition;
    [SerializeField] private Vector3 WanderStart;
    [SerializeField] private Vector3 WanderGoal;
    [SerializeField] private bool GoingLeft;
    [SerializeField] private float waitTime = 0, waitedTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartPosition = gameObject.transform.position;
        WanderGoal = StartPosition;
        WanderStart = StartPosition;
    }

    // Update is called once per frame
    void Update()
    {
        waitedTime += Time.deltaTime;
        if (transform.position == WanderGoal) //If we are at the most recent goal, get a new goal
        {
            if (waitedTime > waitTime) // Our wait is over.
            {
                waitedTime = 0;
                waitTime = ConsiderWait(); // Possibly wait more.
            }
            if ( waitTime == 0 )
            {
                if (GoingLeft)
                {
                    if (Physics.Raycast(new Vector3(transform.position.x - 1, transform.position.y - 1, 1), Vector3.forward, 2)) // Found a piece of ground to walk on, proceed.
                    {
                        if (Physics.Raycast(new Vector3(transform.position.x - 1, transform.position.y, 1), Vector3.forward, 2)) // But if a wall obstructs it, never mind.
                        {
                            GoingLeft = !GoingLeft; // Change direction
                        }
                        else
                        {
                            ConsiderTurn(); // Maybe change direction
                        }
                    }
                    else // There is no more ground to walk on
                    {
                        GoingLeft = !GoingLeft;
                    }
                }
                else
                {
                    if (Physics.Raycast(new Vector3(transform.position.x + 1, transform.position.y - 1, 1), Vector3.forward, 2)) // Found a piece of ground to walk on, proceed.
                    {
                        if (Physics.Raycast(new Vector3(transform.position.x + 1, transform.position.y, 1), Vector3.forward, 2)) // But if a wall obstructs it, never mind.
                        {
                            GoingLeft = !GoingLeft; // Change direction
                        }
                        else
                        {
                            ConsiderTurn(); // Maybe change direction
                        }
                    }
                    else // There is no more ground to walk on
                    {
                        GoingLeft = !GoingLeft;
                    }
                }
                // As of now we know what direction to go to. Now define the next place to walk to.
                if (GoingLeft)
                {
                    WanderGoal = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
                }
                else
                {
                    WanderGoal = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
                }
            }
        }
        else // We arent there yet, keep on moving
        {
            transform.position = Vector3.MoveTowards(transform.position, WanderGoal, MoveSpeed * Time.deltaTime); // Move towards the goal

            // In case we passed it by accident
            if (transform.position.x > WanderGoal.x && !GoingLeft)
            {
                //transform.position = WanderGoal;
            }
            else if (transform.position.x < WanderGoal.x && GoingLeft)
            {
                //transform.position = WanderGoal;
            }
        }
    }

    private void ConsiderTurn()
    {
        if( Random.Range(0f, 1f) < StickToDirectionChance )
        {
            GoingLeft = !GoingLeft;
        }
    }

    private float ConsiderWait()
    {
        if( Random.Range( 0f, 1f) < WaitChance )
        {
            return Random.Range(0f, 4f);
        }
        return 0;
    }
}
