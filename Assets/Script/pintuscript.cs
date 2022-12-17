using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pintuscript : MonoBehaviour
{
    public string namaPintu;
    public GameObject[] pintu;
    public bool isopen = false, pintuGudang, pintuKamar;
    public Animator animatorPintugudang;
    // Start is called before the first frame update
    void Start()
    {
        pintu = new GameObject[2];
        for (int i = 0; i < pintu.Length; i++)
        {
            pintu[i] = this.transform.GetChild(i).gameObject;
        }
    }

    private void Update()
    {
        if(isopen)
        {
            pintu[1].SetActive(true);
            pintu[0].SetActive(false);

            if (pintuGudang)
            {
                pintuGudang = false;
                setting.instance.sounds[5].source.Play();
                animatorPintugudang.SetBool("Open", true);
            }
            if (pintuKamar)
            {
                pintuKamar = false;
                setting.instance.sounds[5].source.Play();
            }

        }
        else
        {
            pintu[0].SetActive(true);
            pintu[1].SetActive(false);

        }
    }
}
