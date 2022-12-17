using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class voicerecognize : MonoBehaviour
{
    public int samplewindow = 64;
    [HideInInspector] public AudioClip microphoneClip;
    public bool micON;
    string michrophoneName;

#if !UNITY_WEBGL
    // Start is called before the first frame update
    void Start()
    {
        michrophoneToAudio();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void onofmic()
    {
        if (!micON)
        {
            micON = true;
            michrophoneToAudio();
        }
        else
        {
            micON = false;
            if (Microphone.IsRecording(michrophoneName))
            {
                Microphone.End(michrophoneName);
            }
        }
    }
    public void michrophoneToAudio()
    {
        for (int i = 0; i < Microphone.devices.Length; i++)
        {
            if (Microphone.devices[i] != null)
            {
                michrophoneName = Microphone.devices[0];
                microphoneClip = Microphone.Start(michrophoneName, true, 20, AudioSettings.outputSampleRate);
                break;
            }
            else
                continue;
        }
        Debug.Log(Microphone.devices.Length);
    }
    public float Getloudnessfrommic()
    {
        return getloudness(Microphone.GetPosition(Microphone.devices[0]), microphoneClip);
    }
    public float getloudness(int clipposition, AudioClip clip)
    {
        int startposition = clipposition - samplewindow;

        if (startposition < 0)
            return 0;

        float[] wavedata = new float[samplewindow];
        clip.GetData(wavedata, startposition);
        float totalloudness = 0;

        for (int i = 0; i < samplewindow; i++)
        {
            totalloudness += Mathf.Abs(wavedata[i]);
        }

        return totalloudness / samplewindow;
    }
#endif
}
