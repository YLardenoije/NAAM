using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GlobalData GlobalData;
    // Start is called before the first frame update
    void Start()
    {
        GlobalData.player = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDeath()
    {

    }

    private void OnDestroy()
    {
        GlobalData.player = null;
    }
}
