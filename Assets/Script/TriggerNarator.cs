using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNarator : MonoBehaviour
{
    public string iniChoiceApa;
    public int urutanText;
    public bool sudahTrigger, cameraMalingAktif, endA, endB;
    public GameObject cameraMaling;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !sudahTrigger && NaratorManager.Instance.A == urutanText)
        {
            sudahTrigger = true;
            NaratorManager.Instance.MulaiNarator(iniChoiceApa, urutanText);

            if (cameraMalingAktif)
            {
                cameraMaling.gameObject.SetActive(true);
            }
            if (endA)
            {
                GameManager.instance.EndA();
            }



        }

        else if (collision.CompareTag("Player") && !sudahTrigger && NaratorManager.Instance.B == urutanText)
        {
            sudahTrigger = true;
            NaratorManager.Instance.MulaiNarator(iniChoiceApa, urutanText);

            if (cameraMalingAktif)
            {
                cameraMaling.gameObject.SetActive(true);
            }
            if (endA)
            {
                GameManager.instance.EndA();
            }


        }
        else if (collision.CompareTag("Player") && !sudahTrigger && NaratorManager.Instance.C == urutanText)
        {
            sudahTrigger = true;
            NaratorManager.Instance.MulaiNarator(iniChoiceApa, urutanText);

            if (cameraMalingAktif)
            {
                cameraMaling.gameObject.SetActive(true);
            }
            if (endA)
            {
                GameManager.instance.EndA();
            }


        }
    }
}
