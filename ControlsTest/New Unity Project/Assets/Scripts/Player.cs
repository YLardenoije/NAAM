using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public UnityEvent OnDeathEvent;

    [SerializeField] GlobalData GlobalData;

    private LivingThing livingThing;
    // Start is called before the first frame update
    void Start()
    {
        GlobalData.SetPlayer(this);
        livingThing = gameObject.GetComponent<LivingThing>();
        livingThing.OnDeathEvent.AddListener(OnDeath);
        OnDeathEvent = new UnityEvent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDeath()
    {
        OnDeathEvent.Invoke();
        Debug.Log("OnDeathEvent was invoked.");
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        GlobalData.RemovePlayer();
    }
}
