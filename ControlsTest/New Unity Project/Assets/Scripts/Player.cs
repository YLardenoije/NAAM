using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public UnityEvent OnPlayerDeath;

    [SerializeField] GlobalData GlobalData;

    private LivingThing livingThing;
    // Start is called before the first frame update
    void Start()
    {
        GlobalData.SetPlayer(this);
        livingThing = gameObject.GetComponent<LivingThing>();
        livingThing.OnDeathEvent.AddListener(OnDeath);
        OnPlayerDeath = new UnityEvent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDeath()
    {
        OnPlayerDeath.Invoke();
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        GlobalData.RemovePlayer();
    }
}
