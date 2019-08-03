using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsControl : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    [SerializeField]
    private float masterVolume = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
       // var listener = GameObject.FindObjectOfType<AudioListener>();
    }

    // Update is called once per frame
    void Update()
    {
        AudioListener.volume = masterVolume;
    }

    public void SetQuality(int qualityIndex)
    {

        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
