using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] GlobalData GlobalData;

    public LivingThing livingThing;
    // Start is called before the first frame update
    void Start()
    {
        livingThing = gameObject.GetComponent<LivingThing>();
        livingThing.OnDeathEvent.AddListener(OnDeath);
        GlobalData.SetPlayer(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDeath()
    {
        Debug.Log("OnDeathEvent was invoked.");
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        GlobalData.RemovePlayer();
    }
}
