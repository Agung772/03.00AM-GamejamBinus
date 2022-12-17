using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class scalefrommicrophone : MonoBehaviour
{
    public Slider volumedetec;
    public voicerecognize detector;
    public float loudnessSensibility = 10 * 50;
    [HideInInspector] public float threshold = 0.1f;
    public  float loudness;
    string alert;
    public Image fill;
    public Text narativeText;
    bool kunig, merah, ketahuan;
    public MalingController malingController;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        if (!ketahuan)
        {
            loudness = detector.Getloudnessfrommic() * loudnessSensibility;
        }


        if (loudness < threshold)
            loudness = 0;
        volumedetec.value = loudness;

        if (loudness < .5 * 50)
        {
            fill.color = Color.white;
            kunig = false;
            merah = false;
        }


        else if (loudness > .6 * 40 && loudness < 1.5 * 40)
        {
            alert = "peringatan!";
            fill.color = Color.yellow;
            InputText("Jangan Berisik !!", true);
        }
        else if (loudness > 2.7 * 40)
        {
            alert = "JANGAN BERISIK";

            fill.color = Color.red;
            if (!merah)
            {
                merah = true;
                InputText("Kamu ketahuan perampok!", false);
                ketahuan = true;
                malingController.patrol = false;
            }
        }
        else if (loudness > 1.4 * 40)
        {
            alert = "JANGAN BERISIK";

            fill.color = Color.red;
            if (!kunig)
            {
                kunig = true;
                InputText("Kecilkan suara mu !!", true);
            }
        }

        else if (loudness >= 1 * 40)
        {
            //do something

        }

    }

    void InputText(string text, bool hilangkan)
    {


        StartCoroutine(RestratGameCoroutine());
        IEnumerator RestratGameCoroutine()
        {
            narativeText.gameObject.SetActive(true);
            narativeText.text = text;

            if (hilangkan)
            {
                yield return new WaitForSeconds(3);
                narativeText.text = "";
                narativeText.gameObject.SetActive(false);
            }


        }
    }
}
