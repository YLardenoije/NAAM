using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;
using FMODUnity;

public class FMODParameterEditor : MonoBehaviour
{
    [SerializeField] GlobalData GD;

    FMOD.Studio.EventInstance FmodEv;
    // Start is called before the first frame update
    void Start()
    {
        FmodEv = GetComponent<StudioEventEmitter>().EventInstance;
        //FmodEv.getParameterByName("ThreatLevel", out threat);
        GD.IntensityChanged.AddListener(OnIntensityChange);
    }

    // Update is called once per frame
    public void OnIntensityChange()
    {
        FmodEv.setParameterByName("ThreatLevel", GD.Intensity);
    }
    
}
