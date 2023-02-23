using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum Scene
    {
        Choice, Gameplay, MainMenu
    }

    public string A, B, C;

    public static GameManager instance;
    public string choiceSaatIni;
    public GameObject triggerNaratorA, triggerNaratorB, triggerNaratorC;

    public Scene scene;
    public PlayerController playerController;
    public GameObject choiceUI, allButton, gameplayUI, gameoverUI, pauseUI, optionUI, creditUI, mainMenuUI, teleponUI, berangkasUI, komputerUI;
    public Vector2 savePosisiPlayer;
    public Animator animatorTransisiHitam, animatorTV;
    public MalingController malingController;
    public GameObject cameraKamar1;

    public bool nonAktifkanEsc;
    public Button buttonA, buttonB;

    private void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;

        choiceSaatIni = PlayerPrefs.GetString("Choice");

        A = PlayerPrefs.GetString("A");
        B = PlayerPrefs.GetString("B");
        C = PlayerPrefs.GetString("C");


    }
    private void Start()
    {
        if (playerController != null)
        {
            savePosisiPlayer = playerController.transform.position;
        }

        //Scene Choice-----------------------------------------------
        if (scene == Scene.Choice)
        {
            choiceUI.SetActive(true);
            gameplayUI.SetActive(false);
            mainMenuUI.SetActive(false);
            allButton.SetActive(false);

            if (A == "SELESAI" && B == "SELESAI")
            {
                print("Mulai C");
                PlayerPrefs.SetString("Choice", "C");
                SceneManager.LoadScene("Gameplay");

            }
            else if (A == "SELESAI")
            {
                buttonA.interactable = false;
            }
            else if (B == "SELESAI")
            {
                buttonB.interactable = false;
            }

        }

        //Scene Gameplay--------------------------------------------
        else if (scene == Scene.Gameplay)
        {
            choiceUI.SetActive(false);
            gameplayUI.SetActive(true);

            if (choiceSaatIni == "A")
            {
                triggerNaratorA.SetActive(true);
                triggerNaratorB.SetActive(false);
                triggerNaratorC.SetActive(false);

                animatorTV.SetTrigger("AB");
                print("Ini A");
            }
            else if (choiceSaatIni == "B")
            {
                triggerNaratorA.SetActive(false);
                triggerNaratorB.SetActive(true);
                triggerNaratorC.SetActive(false);

                animatorTV.SetTrigger("AB");
                print("Ini B");
            }
            else if (choiceSaatIni == "C")
            {
                triggerNaratorA.SetActive(false);
                triggerNaratorB.SetActive(false);
                triggerNaratorC.SetActive(true);

                animatorTV.SetTrigger("C");
                print("Ini C");
            }

            NaratorManager.Instance.StartNarator();
        }

        //Scene Mainmenu--------------------------------------------
        else if (scene == Scene.MainMenu)
        {
            choiceUI.SetActive(false);
            gameplayUI.SetActive(false);
            mainMenuUI.SetActive(true);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            setting.instance.sounds[1].source.Play();
            PlayerPrefs.DeleteAll();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            Gameover();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            PlayerPrefs.SetString("Choice", "C");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            PlayerPrefs.SetString("A", "SELESAI");
            PlayerPrefs.SetString("Choice", "A");
            EndA();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            PlayerPrefs.SetString("B", "SELESAI");
            PlayerPrefs.SetString("Choice", "B");
            EndB();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            PlayerPrefs.SetString("C", "SELESAI");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!nonAktifkanEsc)
            {
                PauseUI();

                if (UIlainnyaBool)
                {
                    UIlainnyaBool = false;
                    pauseUIBool = false;
                    pauseUI.SetActive(false);
                    optionUI.SetActive(false);
                    creditUI.SetActive(false);

                    Time.timeScale = 1;
                }
            }

        }
    }
    public void ChoiceButton(string choiceParameter)
    {
        PlayerPrefs.SetString("Choice", choiceParameter);
        if (choiceParameter == "A")
        {
            SceneManager.LoadScene("Gameplay");
        }
        else if (choiceParameter == "B")
        {
            SceneManager.LoadScene("Gameplay");
        }
        else if (choiceParameter == "C")
        {
            SceneManager.LoadScene("Gameplay");
        }

        setting.instance.sounds[1].source.Play();
    }

    bool pauseUIBool, UIlainnyaBool;
    public void Back()
    {
        pauseUI.SetActive(false);
        optionUI.SetActive(false);
        creditUI.SetActive(false);

        UIlainnyaBool = false;
        pauseUIBool = true;

        Time.timeScale = 1;

        if (scene == Scene.MainMenu)
        {
            mainMenuUI.SetActive(true);
        }

        setting.instance.sounds[1].source.Play();
    }

    void PauseUI()
    {
        if (!pauseUIBool)
        {
            pauseUIBool = true;
            pauseUI.SetActive(true);
            Time.timeScale = 0;
        }
        else if (pauseUIBool)
        {
            pauseUIBool = false;
            pauseUI.SetActive(false);
            Time.timeScale = 1;
        }

        setting.instance.sounds[1].source.Play();
    }
    public void Options()
    {
        UIlainnyaBool = true;

        pauseUI.SetActive(false);
        optionUI.SetActive(true);
        creditUI.SetActive(false);
        mainMenuUI.SetActive(false);
        pauseUIBool = false;
        setting.instance.sounds[1].source.Play();
    }
    public void StartGame()
    {
        pauseUI.SetActive(false);
        optionUI.SetActive(false);
        creditUI.SetActive(false);
        pauseUIBool = false;
        Time.timeScale = 1;

        setting.instance.sounds[1].source.Play();
    }
    public void Credit()
    {
        UIlainnyaBool = true;
        pauseUI.SetActive(false);
        pauseUIBool = false;
        optionUI.SetActive(false);
        creditUI.SetActive(true);
        mainMenuUI.SetActive(false);

        setting.instance.sounds[1].source.Play();
    }

    public Text textBerangkas;
    int[] isiTextBerangkas;
    public void ButtonBerangkas(int input)
    {
        textBerangkas.text = "" + isiTextBerangkas;

        setting.instance.sounds[1].source.Play();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Mainmenu");

        setting.instance.sounds[1].source.Play();
    }

    public void ExitGame()
    {
        Application.Quit();

        setting.instance.sounds[1].source.Play();
    }

    public void StartChoice()
    {
        SceneManager.LoadScene("Choice");

        setting.instance.sounds[1].source.Play();
    }

    public GameObject[] isiGameoverUI;
    bool gameOver;
    public void Gameover()
    {
        StartCoroutine(GameoverCoroutine());
        IEnumerator GameoverCoroutine()
        {
            if (!gameOver)
            {
                gameOver = true;
                gameoverUI.SetActive(true);

                isiGameoverUI = new GameObject[gameoverUI.transform.childCount];
                for (int i = 0; i < isiGameoverUI.Length; i++)
                {
                    isiGameoverUI[i] = gameoverUI.transform.GetChild(i).gameObject;

                    if (i == 0)
                    {
                        isiGameoverUI[i].SetActive(true);
                        isiGameoverUI[i].GetComponent<Animator>().SetBool("Start", true);
                        isiGameoverUI[i].GetComponent<Animator>().SetBool("Exit", false);
                    }
                    else if (i != 0)
                    {
                        isiGameoverUI[i].SetActive(false);
                    }
                }
                yield return new WaitForSeconds(0.5f);
                for (int i = 0; i < isiGameoverUI.Length; i++)
                {
                    if (i != 0)
                    {
                        isiGameoverUI[i].SetActive(true);
                    }

                }
            }
            setting.instance.sounds[8].source.Play();
        }
    }

    public void RestartGame()
    {
        StartCoroutine(RestratGameCoroutine());
        IEnumerator RestratGameCoroutine()
        {

            if (A == "SELESAI" && B == "SELESAI")
            {
                SceneManager.LoadScene("Choice");
            }
            else if (A == "SELESAI" && choiceSaatIni == "A")
            {
                SceneManager.LoadScene("Choice");
            }
            else if (B == "SELESAI" && choiceSaatIni == "B")
            {
                SceneManager.LoadScene("Choice");
            }
            else
            {
                SceneManager.LoadScene("Gameplay");
            }
            yield return new WaitForSeconds(0f);

        }

        setting.instance.sounds[1].source.Play();
    }

    public void TransisiHitam()
    {
        StartCoroutine(StartGameCoroutine());
        IEnumerator StartGameCoroutine()
        {
            StartTransisiHitam();
            yield return new WaitForSeconds(1f);
            ExitTransisiHitam();
        }
    }

    public void StartTransisiHitam()
    {
        StartCoroutine(StartGameCoroutine());
        IEnumerator StartGameCoroutine()
        {
            yield return new WaitForSeconds(0);
            if (!gameoverUI.activeInHierarchy)
            {
                animatorTransisiHitam.gameObject.SetActive(true);
                animatorTransisiHitam.SetBool("Start", true);
                animatorTransisiHitam.SetBool("Exit", false);
            }
        }
    }

    public void ExitTransisiHitam()
    {
        StartCoroutine(ExitCoroutine());
        IEnumerator ExitCoroutine()
        {
            animatorTransisiHitam.SetBool("Start", false);
            animatorTransisiHitam.SetBool("Exit", true);
            yield return new WaitForSeconds(1);
            animatorTransisiHitam.gameObject.SetActive(false);
        }
    }
    void ResetGame()
    {
        NaratorManager.Instance.A = 1;
        NaratorManager.Instance.B = 1;
        NaratorManager.Instance.C = 1;

        gameOver = false;
        playerController.MatiinSemuaCamera();
        cameraKamar1.SetActive(true);
        if (choiceSaatIni == "A")
        {
            malingController.ResetMaling();

            GameObject[] triggerA = new GameObject[triggerNaratorA.transform.childCount];

            for (int i = 0; i < triggerA.Length; i++)
            {
                triggerA[i] = triggerNaratorA.transform.GetChild(i).gameObject;
                triggerA[i].GetComponent<TriggerNarator>().sudahTrigger = false;
            }

            triggerNaratorA.SetActive(true);
            triggerNaratorB.SetActive(false);
            triggerNaratorC.SetActive(false);
        }
        else if (choiceSaatIni == "B")
        {
            malingController.ResetMaling();

            GameObject[] triggerB = new GameObject[triggerNaratorB.transform.childCount];

            for (int i = 0; i < triggerB.Length; i++)
            {
                triggerB[i] = triggerNaratorB.transform.GetChild(i).gameObject;
                triggerB[i].GetComponent<TriggerNarator>().sudahTrigger = false;
            }

            triggerNaratorA.SetActive(false);
            triggerNaratorB.SetActive(true);
            triggerNaratorC.SetActive(false);
        }
        else if (choiceSaatIni == "C")
        {
            malingController.ResetMaling();

            GameObject[] triggerC = new GameObject[triggerNaratorC.transform.childCount];

            for (int i = 0; i < triggerC.Length; i++)
            {
                triggerC[i] = triggerNaratorC.transform.GetChild(i).gameObject;
                triggerC[i].GetComponent<TriggerNarator>().sudahTrigger = false;
            }

            triggerNaratorA.SetActive(false);
            triggerNaratorB.SetActive(false);
            triggerNaratorC.SetActive(true);
        }
    }
    public Animator animatorLemari;

    public void Esc()
    {
        komputerUI.SetActive(false);
        setting.instance.sounds[1].source.Play();
    }
    public void KomputerUI(int urutanUI)
    {
        GameObject[] chield = new GameObject[komputerUI.transform.childCount];
        for (int i = 0; i < chield.Length; i++)
        {
            chield[i] = komputerUI.transform.GetChild(i).gameObject;
            chield[i].SetActive(false);
        }

        if (urutanUI == 0)
        {
            chield[0].SetActive(true);
        }
        else if (urutanUI == 1)
        {
            chield[0].SetActive(false);
            chield[1].SetActive(true);
        }
        else if(urutanUI == 2)
        {
            chield[1].SetActive(false);
            chield[2].SetActive(true);
        }
        else if (urutanUI == 11)
        {
            NaratorManager.Instance.CustomNarator("Pilihan kamu salah, coba lagi setelah 10 detik", 5);
            komputerUI.SetActive(false);
            playerController.timeKomputerUI = 10f;
        }
        else if (urutanUI == 12)
        {
            NaratorManager.Instance.CustomNarator("Pilihan kamu salah, coba lagi setelah 10 detik", 5);
            komputerUI.SetActive(false);
            playerController.timeKomputerUI = 10f;
        }
        else if (urutanUI == 13)
        {
            NaratorManager.Instance.CustomNarator("Pilihan kamu salah, coba lagi setelah 10 detik", 5);
            komputerUI.SetActive(false);
            playerController.timeKomputerUI = 10f;
        }
        else if (urutanUI == 14)
        {
            NaratorManager.Instance.CustomNarator("Pilihan kamu benar", 5);
            animatorLemari.SetBool("Open", true);
            komputerUI.SetActive(false);

            setting.instance.sounds[11].source.Play();
        }
        setting.instance.sounds[1].source.Play();
    }

    //Ending-------------------------------------------------------------------------------

    public void WinPistol()
    {
        NaratorManager.Instance.CustomNarator("Tembak perampok itu!!!", 5);
        setting.instance.sounds[3].source.Play();
        StartTransisiHitam();
        Gameover();
    }

    public void WinPisau()
    {
        NaratorManager.Instance.CustomNarator("Segera bunuh perampok itu!!!", 5);
        setting.instance.sounds[8].source.Play();
        StartTransisiHitam();
        Gameover();
    }
    public void WinPolisi()
    {
        NaratorManager.Instance.CustomNarator("Sekarang kamu aman, polisi akan datang secepatnya", 5);
        Gameover();
    }


    public void EndA()
    {
        PlayerPrefs.SetString("A", "SELESAI");
        A = PlayerPrefs.GetString("A");
        malingController.patrol = false;
        playerController.endA = true;
        StartCoroutine(RestratGameCoroutine());
        IEnumerator RestratGameCoroutine()
        {
            yield return new WaitForSeconds(5);
        }
    }
    public void EndB()
    {
        PlayerPrefs.SetString("B", "SELESAI");
        B = PlayerPrefs.GetString("B");
        playerController.endB = true;
        StartCoroutine(RestratGameCoroutine());
        IEnumerator RestratGameCoroutine()
        {
            NaratorManager.Instance.textNarative.gameObject.SetActive(true);
            NaratorManager.Instance.CustomNarative("Setelah Cecil Memberi minyak ke engsel jendela, tiba tiba perampok masuk ke kamar itu", 10);
            yield return new WaitForSeconds(10);
            NaratorManager.Instance.CustomNarative("karena cecil ketakutan ia langsung membuka jendela dan lompat. Tanpa disangka ternyata Cecil mati karena jatuh dari ketinggian.", 10);
            yield return new WaitForSeconds(10);
            NaratorManager.Instance.CustomNarative("Lagi-Lagi bukan ending yang di harapkan. Bagaimana kalau kita coba satu kali lagi", 10);
            yield return new WaitForSeconds(5);
            NaratorManager.Instance.CustomNarative("apakah gamenya terlalu susah?", 10);
            yield return new WaitForSeconds(5);
            NaratorManager.Instance.CustomNarative("Bagaimana kalau kami beri tambahan 5 menit untuk membuat persiapan, Kali ini, Coba lakukan dengan jalan mu sendiri :)”", 10);
            yield return new WaitForSeconds(10);

            NaratorManager.Instance.textNarative.gameObject.SetActive(false);
            Gameover();

        }
    }

    public void Sample()
    {
        StartCoroutine(RestratGameCoroutine());
        IEnumerator RestratGameCoroutine()
        {
            yield return new WaitForSeconds(1);

        }
    }

    public void ResetSave()
    {
        setting.instance.sounds[1].source.Play();
        PlayerPrefs.DeleteAll();
    }
}
