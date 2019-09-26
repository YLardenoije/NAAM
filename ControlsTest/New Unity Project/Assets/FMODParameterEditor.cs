using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;
using FMODUnity;

public class FMODParameterEditor : MonoBehaviour
{
    public int Intensity=0;
    private float threat;
    FMOD.Studio.EventInstance FmodEv;
    // Start is called before the first frame update
    void Start()
    {
        FmodEv = GetComponent<StudioEventEmitter>().EventInstance;
        FmodEv.getParameterByName("ThreatLevel", out threat);
    }

    // Update is called once per frame
    void Update()
    {
        FmodEv.setParameterByName("ThreatLevel",(float)Intensity);
    }
}
