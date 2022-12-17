using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public bool gerakan;
    public Vector2 savePosisiPlayer;
    public Animator animator;
    public new Rigidbody2D rigidbody2D;
    public float speedPlayer;
    float inputX, inputY;
    public string condisiAnimasi;
    Vector2 movement;

    public MalingController malingController;
    public GameObject CameraUtama;


    public GameObject textBox, penutupLantaiatas;

    public bool playerPegangPistol, playerPegangPisau;

    public GameObject nongolinBoltCutter, nongolinMinyak, nongolinKunciEmas;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        penutupLantaiatas.SetActive(true);
        savePosisiPlayer = transform.position;
    }

    private void FixedUpdate()
    {
        if (gerakan)
        {
            MovePlayer();
        }

        AnimasiPlayer();
        Sembunyi();
        BukaPintu();
        BukaLemari();
        PindahScene();
        TeleponUI();
        BerangkasUI();
        CooldownKomputerUI();

    }

    void MovePlayer()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        rigidbody2D.MovePosition(rigidbody2D.position + movement * speedPlayer * Time.fixedDeltaTime);
    }

    void AnimasiPlayer()
    {
        if (inputX == 0 && inputY == 0)
        {
            animator.SetBool("IdleDepan", false);
            animator.SetBool("IdleBelakang", false);
            animator.SetBool("IdleKanan", false);
            animator.SetBool("WalkingDepan", false);
            animator.SetBool("WalkingBelakang", false);
            animator.SetBool("WalkingKanan", false);

            if (condisiAnimasi == "Kanan")
            {
                animator.SetBool("IdleDepan", false);
                animator.SetBool("IdleBelakang", false);
                animator.SetBool("IdleKanan", true);
                animator.SetBool("WalkingDepan", false);
                animator.SetBool("WalkingBelakang", false);
                animator.SetBool("WalkingKanan", false);
            }
            else if (condisiAnimasi == "Kiri")
            {
                animator.SetBool("IdleDepan", false);
                animator.SetBool("IdleBelakang", false);
                animator.SetBool("IdleKanan", true);
                animator.SetBool("WalkingDepan", false);
                animator.SetBool("WalkingBelakang", false);
                animator.SetBool("WalkingKanan", false);
            }
            else if (condisiAnimasi == "Depan")
            {
                animator.SetBool("IdleDepan", true);
                animator.SetBool("IdleBelakang", false);
                animator.SetBool("IdleKanan", false);
                animator.SetBool("WalkingDepan", false);
                animator.SetBool("WalkingBelakang", false);
                animator.SetBool("WalkingKanan", false);
            }
            else if (condisiAnimasi == "Belakang")
            {
                animator.SetBool("IdleDepan", false);
                animator.SetBool("IdleBelakang", true);
                animator.SetBool("IdleKanan", false);
                animator.SetBool("WalkingDepan", false);
                animator.SetBool("WalkingBelakang", false);
                animator.SetBool("WalkingKanan", false);
            }

        }
        if (inputX > 0)
        {
            condisiAnimasi = "Kanan";

            animator.SetBool("IdleDepan", false);
            animator.SetBool("IdleBelakang", false);
            animator.SetBool("IdleKanan", false);
            animator.SetBool("WalkingDepan", false);
            animator.SetBool("WalkingBelakang", false);
            animator.SetBool("WalkingKanan", true);

            transform.eulerAngles = Vector3.zero;
            textBox.GetComponent<RectTransform>().localPosition = new Vector2(1.5f, 1);
            textBox.transform.localEulerAngles = Vector2.zero;
        }
        else if (inputX < 0)
        {
            condisiAnimasi = "Kiri";
            animator.SetBool("IdleDepan", false);
            animator.SetBool("IdleBelakang", false);
            animator.SetBool("IdleKanan", false);
            animator.SetBool("WalkingDepan", false);
            animator.SetBool("WalkingBelakang", false);
            animator.SetBool("WalkingKanan", true);

            transform.eulerAngles = new Vector3(0, 180, 0);
            textBox.GetComponent<RectTransform>().localPosition = new Vector2(-1.5f, 1);
            textBox.transform.localEulerAngles = new Vector2(0, 180);
        }

        if (inputY < 0)
        {
            condisiAnimasi = "Depan";

            animator.SetBool("IdleDepan", false);
            animator.SetBool("IdleBelakang", false);
            animator.SetBool("IdleKanan", false);
            animator.SetBool("WalkingDepan", true);
            animator.SetBool("WalkingBelakang", false);
            animator.SetBool("WalkingKanan", false);
        }
        else if (inputY > 0)
        {
            condisiAnimasi = "Belakang";

            animator.SetBool("IdleDepan", false);
            animator.SetBool("IdleBelakang", false);
            animator.SetBool("IdleKanan", false);
            animator.SetBool("WalkingDepan", false);
            animator.SetBool("WalkingBelakang", true);
            animator.SetBool("WalkingKanan", false);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Maling"))
        {

        }

        if (collision.collider.CompareTag("Sembunyi"))
        {
            sembunyi = true;
            sembunyiTransform = collision.transform.position;
            textBox.SetActive(true);
            textBox.GetComponent<TextMeshPro>().text = "F untuk bersembunyi";
        }

        if (collision.collider.CompareTag("Pintu"))
        {
            bukaPintu = true;
            animatorCol = collision.collider.GetComponent<Animator>();
            boxCollider2D = collision.collider.GetComponent<BoxCollider2D>();

            textBox.SetActive(true);
            textBox.GetComponent<TextMeshPro>().text = "F untuk membuka pintu";
        }

        //Maling
        if (collision.collider.GetComponent<MalingController>())
        {
            if (!maling)
            {
                maling = true;
                NaratorManager.Instance.CustomNarator("Kamu ditangkap perampok", 5);
                GameManager.instance.Gameover();

                if (endA)
                {
                    NaratorManager.Instance.MulaiNarator("A", 9);
                }
            }

        }
    }
    public bool endA, endB;
    bool maling;
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Sembunyi"))
        {
            sembunyi = false;
            sembunyiTransform = Vector2.zero;
            textBox.SetActive(false);
            gameObject.GetComponent<CapsuleCollider2D>().isTrigger = false;
        }

        if (collision.collider.CompareTag("Pintu"))
        {
            textBox.SetActive(false);
            bukaPintu = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Lemari"))
        {
            bukaLemari = true;
            animatorCol = collision.GetComponent<Animator>();
            boxCollider2D = collision.GetComponent<BoxCollider2D>();

            textBox.SetActive(true);
            textBox.GetComponent<TextMeshPro>().text = "F untuk membuka lemari";
        }
        if (collision.CompareTag("Berangkas"))
        {
            berangkas = true;


            textBox.SetActive(true);
            textBox.GetComponent<TextMeshPro>().text = "F untuk membuka berangkas";
        }
        if (collision.CompareTag("Telepon"))
        {
            telepon = true;
            textBox.SetActive(true);
            textBox.GetComponent<TextMeshPro>().text = "F untuk menelpon";
        }
        if (collision.CompareTag("Komputer"))
        {
            komputer = true;
            textBox.SetActive(true);
            textBox.GetComponent<TextMeshPro>().text = "F untuk buka komputer";
        }

        if (collision.GetComponent<TriggerLantaiatas>())
        {
            penutupLantaiatas.GetComponent<Animator>().SetBool("Start", true);
        }

        //Scene
        if (collision.CompareTag("PindahScene"))
        {
  
            pindahScene = true;
            lokasiTarget = collision.GetComponent<PindahScene>().lokasiTarget;
            pintuSekarang = collision.GetComponent<PindahScene>();
            print(collision.GetComponent<PindahScene>().namaLokasi);

            textBox.SetActive(true);
            textBox.GetComponent<TextMeshPro>().text = "F untuk " + collision.GetComponent<PindahScene>().namaLokasi;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Lemari"))
        {
            textBox.SetActive(false);
            bukaLemari = false;
        }

        if (collision.GetComponent<TriggerLantaiatas>())
        {
            penutupLantaiatas.GetComponent<Animator>().SetBool("Start", false);
        }
        if (collision.CompareTag("Berangkas"))
        {
            berangkas = false;
            textBox.SetActive(false);
        }
        if (collision.CompareTag("Telepon"))
        {
            telepon = false;
            textBox.SetActive(false);
        }
        if (collision.CompareTag("Komputer"))
        {
            komputer = false;
            textBox.SetActive(false);
        }

        //Scene
        if (collision.CompareTag("PindahScene"))
        {

            pindahScene = false;
            lokasiTarget = null;
            pintuSekarang = null;

            textBox.SetActive(false);
        }


    }
    bool komputer;
    public float timeKomputerUI;
    void CooldownKomputerUI()
    {
        timeKomputerUI -= Time.deltaTime;
        timeKomputerUI = Mathf.Clamp(timeKomputerUI, 0f, 10f);

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (timeKomputerUI <= 0f && komputer)
            {
                komputer = false;
                GameManager.instance.komputerUI.SetActive(true);
                GameManager.instance.KomputerUI(0);
            }
            else if (timeKomputerUI > 0f && komputer)
            {
                NaratorManager.Instance.CustomNarator("Masih cooldown " + (int)timeKomputerUI + " detik", 3);
            }
        }

    }

    bool telepon;
    void TeleponUI()
    {
        if (Input.GetKeyDown(KeyCode.F) && telepon)
        {
            GameManager.instance.teleponUI.SetActive(true);
        }
    }

    bool berangkas;
    void BerangkasUI()
    {
        if (Input.GetKeyDown(KeyCode.F) && berangkas)
        {
            GameManager.instance.berangkasUI.SetActive(true);
        }
    }

    bool pindahScene, cooldownPindahscene;
    public Transform lokasiTarget;
    public PindahScene pintuSekarang;
    public void PindahScene()
    {
        if (Input.GetKeyDown(KeyCode.F) && pindahScene && !cooldownPindahscene)
        {
            setting.instance.sounds[9].source.Play();
            cooldownPindahscene = true;

            StartCoroutine(DelayCoroutine());
            IEnumerator DelayCoroutine()
            {
                if (pintuSekarang.endB)
                {
                    GameManager.instance.EndB();
                    GameManager.instance.StartTransisiHitam();
                }
                else if (!pintuSekarang.endB)
                {
                    GameManager.instance.TransisiHitam();
                    yield return new WaitForSeconds(0.5f);

                    MatiinSemuaCamera();

                    if (pintuSekarang.keluarRuang)
                    {
                        CameraUtama.SetActive(true);
                    }
                    else if (!pintuSekarang.keluarRuang)
                    {
                        lokasiTarget.transform.GetChild(0).gameObject.SetActive(true);
                    }


                    transform.position = lokasiTarget.position;

                    textBox.SetActive(false);
                    pintuSekarang = null;
                    pindahScene = false;
                    lokasiTarget = null;
                    yield return new WaitForSeconds(1f);
                    cooldownPindahscene = false;
                }


            }
        }
    }

    public void MatiinSemuaCamera()
    {
        GameObject[] semuaCamera = GameObject.FindGameObjectsWithTag("MainCamera");

        for (int i = 0; i < semuaCamera.Length; i++)
        {
            semuaCamera[i].SetActive(false);
        }
    }

    bool bukaPintu, bukaLemari;
    Animator animatorCol;
    BoxCollider2D boxCollider2D;

    void BukaLemari()
    {
        if (Input.GetKeyDown(KeyCode.F) && bukaLemari)
        {
            bukaLemari = false;

            textBox.SetActive(false);
            animatorCol.SetBool("open", true);
            if (boxCollider2D.GetComponent<Lemari>().nomorLemari == 1)
            {
                nongolinBoltCutter.SetActive(true);
            }
            else if (boxCollider2D.GetComponent<Lemari>().nomorLemari == 2)
            {
                nongolinMinyak.SetActive(true);
            }
            else if (boxCollider2D.GetComponent<Lemari>().nomorLemari == 3)
            {
                
            }

            boxCollider2D.enabled = false;
            animatorCol = null;
            boxCollider2D = null;

            setting.instance.sounds[6].source.Play();
        }
    }
    void BukaPintu()
    {
        if (Input.GetKeyDown(KeyCode.F) && bukaPintu)
        {
            bukaPintu = false;

            textBox.SetActive(false);
            animatorCol.SetBool("open", true);
            boxCollider2D.isTrigger = true;
            animatorCol = null;
            boxCollider2D = null;
            setting.instance.sounds[9].source.Play();
        }
    }
    public bool sedangSembunyi;
    public bool sembunyi, keadaanSembunyi;
    Vector2 sembunyiTransform;
    void Sembunyi()
    {
        if (sembunyi)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                textBox.SetActive(false);
                sedangSembunyi = true;
                keadaanSembunyi = true;
                sembunyi = false;
                savePosisiPlayer = transform.position;
                transform.position = sembunyiTransform;
                gerakan = false;
                gameObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
                setting.instance.sounds[7].source.Play();   
            }

        }
        else if (Input.GetKeyDown(KeyCode.F) && !sembunyi && keadaanSembunyi)
        {
            sedangSembunyi = false;
            keadaanSembunyi = false;
            transform.position = savePosisiPlayer;
            gerakan = true;
            gameObject.GetComponent<CapsuleCollider2D>().isTrigger = false;
            setting.instance.sounds[7].source.Play();
        }
    }
}
