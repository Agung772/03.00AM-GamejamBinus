using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputNomor : MonoBehaviour
{
    public GameObject parentUI, berangkasGameobject, pistol;
    public bool berangkas;

    public Text nomorText;
    public string[] isiNomor = new string[4];
    public int[] codeNomor = new int[4];
    public string kodeBener;


    private void OnEnable()
    {
        GameManager.instance.nonAktifkanEsc = true;
    }
    private void OnDisable()
    {
        GameManager.instance.nonAktifkanEsc = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ESC();
        }
    }

    public void InputButton(int inputnomor)
    {

        if (isiNomor[0] == "")
        {
            isiNomor[0] = "" + inputnomor;
            codeNomor[0] = inputnomor;
        }
        else if (isiNomor[1] == "")
        {
            isiNomor[1] = "" + inputnomor;
            codeNomor[1] = inputnomor;
        }
        else if (isiNomor[2] == "")
        {
            isiNomor[2] = "" + inputnomor;
            codeNomor[2] = inputnomor;
        }
        else if (isiNomor[3] == "")
        {
            isiNomor[3] = "" + inputnomor;
            codeNomor[3] = inputnomor;

            StartCoroutine(start());
            IEnumerator start()
            {
                yield return new WaitForSeconds(0f);

                if (berangkas)
                {
                    if (codeNomor[0] == 9 && codeNomor[1] == 2 && codeNomor[2] == 5 && codeNomor[3] == 7)
                    {
                        NaratorManager.Instance.CustomNarator("Password benar", 3);

                        PlayerController.instance.playerPegangPistol = true;
                        pistol.SetActive(true);
                        berangkasGameobject.GetComponent<Animator>().SetBool("Open", true);
                        ESC();

                        yield return new WaitForSeconds(5);
                        NaratorManager.Instance.CustomNarator("Kamu mendapatkan pistol dari berangkas tersebut segera bunuh perampok", 5);
                    }
                    else
                    {
                        NaratorManager.Instance.CustomNarator("Password yang kamu masukkan salah", 5);

                        for (int i = 0; i < 4; i++)
                        {
                            codeNomor[i] = 0;
                            isiNomor[i] = "";
                        }
                        nomorText.text = "";
                    }
                }
                else
                {
                    if (codeNomor[0] == 9 && codeNomor[1] == 9 && codeNomor[2] == 2 && codeNomor[3] == 5)
                    {
                        NaratorManager.Instance.CustomNarator("Menelpon polisi", 3);

                        GameManager.instance.StartTransisiHitam();
                        yield return new WaitForSeconds(3.5f);
                        GameManager.instance.WinPolisi();

                        ESC();
                    }
                    else
                    {
                        NaratorManager.Instance.CustomNarator("Nomor yang kamu hubungi tidak tersedia", 5);

                        for (int i = 0; i < 4; i++)
                        {
                            codeNomor[i] = 0;
                            isiNomor[i] = "";
                        }
                        nomorText.text = "";
                    }
                }
            }




        }

        nomorText.text = "" + isiNomor[0] + isiNomor[1] + isiNomor[2] + isiNomor[3];
        setting.instance.sounds[1].source.Play();

    }

    public void ESC()
    {
        parentUI.SetActive(false);
        setting.instance.sounds[1].source.Play();
    }
}
