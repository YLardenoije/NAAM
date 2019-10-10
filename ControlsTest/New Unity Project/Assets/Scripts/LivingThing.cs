using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LivingThing : MonoBehaviour
{
    public UnityEvent OnDeathEvent = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        //call the OnDeath function of the parent here.
        OnDeathEvent.Invoke();
    }
}
