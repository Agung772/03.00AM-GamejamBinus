using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class NaratorManager : MonoBehaviour
{
    public static NaratorManager Instance;

    public string[] isiTextA;
    public float[] timeTextA;

    public string[] isiTextB;
    public float[] timeTextB;

    public string[] isiTextC;
    public float[] timeTextC;

    public string choiceNarator;
    public Text textNarator, textNarative;

    public int A, B, C;
    private void Awake()
    {
        Instance = this;
        choiceNarator = PlayerPrefs.GetString("Choice");
    }
    public void StartNarator()
    {
        if (choiceNarator == "A")
        {
            MulaiNarator("A", 0);
        }
        else if (choiceNarator == "B")
        {
            MulaiNarator("B", 0);
        }
        else if (choiceNarator == "C")
        {
            StartCoroutine(ChoiceC());
            IEnumerator ChoiceC()
            {

                MulaiNarator("C", 0);
                yield return new WaitForSeconds(10);
                print("afafasa");
                MulaiNarator("C", 1);
                yield return new WaitForSeconds(7);
                MulaiNarator("C", 2);
                yield return new WaitForSeconds(7);
                MulaiNarator("C", 3);
                yield return new WaitForSeconds(7);
                MulaiNarator("C", 4);
                yield return new WaitForSeconds(7);
                MulaiNarator("C", 5);
                yield return new WaitForSeconds(7);
                MulaiNarator("C", 6);
                yield return new WaitForSeconds(7);
                MulaiNarator("C", 7);
                yield return new WaitForSeconds(7);
                MulaiNarator("C", 8);
                yield return new WaitForSeconds(7);
                MulaiNarator("C", 9);
                yield return new WaitForSeconds(7);
                MulaiNarator("C", 10);
                yield return new WaitForSeconds(7);


            }
        }
    }

    public void MulaiNarator(string choice, int urutanText)
    {
        StartCoroutine(MulaiCoroutine());
        IEnumerator MulaiCoroutine()
        {
            yield return new WaitForSeconds(0);
            if (choice == "A")
            {
                A++;
                textNarator.text = isiTextA[urutanText];
                yield return new WaitForSeconds(timeTextA[urutanText]);


            }
            else if (choice == "B")
            {
                B++;
                textNarator.text = isiTextB[urutanText];
                yield return new WaitForSeconds(timeTextB[urutanText]);

            }
            else if (choice == "C")
            {
                C++;
                textNarator.text = isiTextC[urutanText];
                yield return new WaitForSeconds(timeTextC[urutanText]);

            }

        }

    }


    public void CustomNarator(string isiText, float detik)
    {
        StartCoroutine(MulaiCoroutine());
        IEnumerator MulaiCoroutine()
        {
            textNarator.text = isiText;
            yield return new WaitForSeconds(detik);


        }

    }
    public void CustomNarative(string isiText, float detik)
    {
        textNarative.gameObject.SetActive(true);
        textNarator.text = "";
        StartCoroutine(MulaiCoroutine());
        IEnumerator MulaiCoroutine()
        {
            textNarative.text = isiText;
            yield return new WaitForSeconds(detik);


        }

    }
}
