using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MalingController : MonoBehaviour
{
    public PlayerController playerController;
    public NavMeshAgent agent;
    public Animator animator;
    public Transform[] waypoint;
    public float speedMaling;
    public int nomorWaypoint;
    public bool patrol;
    public GameObject senterMaling;
    Vector3 savePosisi;
    public GameObject parentWaypoint;

    bool keAtas;
    public Vector3 jarak;
    public int nomorWaypointSaatIni;

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Start()
    {
        savePosisi = transform.position;


        agent.updateRotation = false;
        agent.updateUpAxis = false;

        animator.SetBool("WalkingDepan", false);
        animator.SetBool("WalkingBelakang", true);
        animator.SetBool("WalkingKanan", false);
    }
    private void Update()
    {

        if (patrol)
        {
            agent.SetDestination(waypoint[nomorWaypoint].position);
            agent.speed = speedMaling;
        }
        else
        {
            KejarPlayer();
        }
    }
    GameObject[] semuaWaypoint;
    public void ChangeWaypoint(int parameter)
    {
        if (parameter == waypoint.Length - 1)
        {
            nomorWaypoint = 0;
            nomorWaypointSaatIni = 0;
            semuaWaypoint = new GameObject[parentWaypoint.transform.childCount];

            for (int i = 0; i < semuaWaypoint.Length; i++)
            {
                semuaWaypoint[i] = parentWaypoint.transform.GetChild(i).gameObject;
                semuaWaypoint[i].GetComponent<Waypoint>().sudahTrigger = false;
            }
        } 
        else if (parameter != waypoint.Length - 1)
        {
            nomorWaypoint = parameter + 1;
            nomorWaypointSaatIni++;
        }

    }

    public void AnimasiMaling(string parameter)
    {
        if (animasiBaru)
        {
            if (agent.velocity.z > .5f)
            {
                print("kanan");
            }
            else if (agent.velocity.z < -.5f)
            {
                print("kiri");
            }
            else if (agent.velocity.x < .5f)
            {
                print("atas");
            }
            else if (agent.velocity.x < -.5f)
            {
                print("bawah");
            }
        }
        else
        {
            if (parameter == "Kanan")
            {
                animator.SetBool("WalkingDepan", false);
                animator.SetBool("WalkingBelakang", false);
                animator.SetBool("WalkingKanan", true);

                senterMaling.transform.localEulerAngles = new Vector3(0, 0, -90);
                transform.eulerAngles = Vector3.zero;
            }
            else if (parameter == "Kiri")
            {
                animator.SetBool("WalkingDepan", false);
                animator.SetBool("WalkingBelakang", false);
                animator.SetBool("WalkingKanan", true);


                senterMaling.transform.localEulerAngles = new Vector3(0, 0, 180 + 90);
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (parameter == "Atas")
            {
                animator.SetBool("WalkingDepan", false);
                animator.SetBool("WalkingBelakang", true);
                animator.SetBool("WalkingKanan", false);


                senterMaling.transform.localEulerAngles = new Vector3(0, 0, 0);
            }
            else if (parameter == "Bawah")
            {
                animator.SetBool("WalkingDepan", true);
                animator.SetBool("WalkingBelakang", false);
                animator.SetBool("WalkingKanan", false);


                senterMaling.transform.localEulerAngles = new Vector3(0, 0, 180);
            }
        }


    }
    public bool animasiBaru;
    void KejarPlayer()
    {
        agent.SetDestination(playerController.transform.position);
        agent.speed = 15;

        //Animasi
        if (animasiBaru)
        {
            if (agent.velocity.z > .5f)
            {
                print("kanan");
            }
            else if (agent.velocity.z < -.5f)
            {
                print("kiri");
            }
            else if (agent.velocity.x < .5f)
            {
                print("atas");
            }
            else if (agent.velocity.x < -.5f)
            {
                print("bawah");
            }
        }
        else
        {
            if (keAtas)
            {
                if (transform.position.y > playerController.transform.position.y)
                {
                    animator.SetBool("WalkingDepan", false);
                    animator.SetBool("WalkingBelakang", true);
                    animator.SetBool("WalkingKanan", false);
                }
                else if (transform.position.y < playerController.transform.position.y)
                {
                    animator.SetBool("WalkingDepan", true);
                    animator.SetBool("WalkingBelakang", false);
                    animator.SetBool("WalkingKanan", false);
                }
            }
            else if (!keAtas)
            {
                if (transform.position.x < playerController.transform.position.x)
                {
                    animator.SetBool("WalkingDepan", false);
                    animator.SetBool("WalkingBelakang", false);
                    animator.SetBool("WalkingKanan", true);

                    transform.eulerAngles = Vector3.zero;
                }
                else if (transform.position.x > playerController.transform.position.x)
                {
                    animator.SetBool("WalkingDepan", false);
                    animator.SetBool("WalkingBelakang", false);
                    animator.SetBool("WalkingKanan", true);

                    transform.eulerAngles = new Vector3(0, 180, 0);
                }
            }



        }

    }

    public void JatuhkaneSenter()
    {

    }

    public void ResetMaling()
    {
        agent.speed = speedMaling;
        transform.position = savePosisi;
        senterMaling.SetActive(true);
        patrol = true;

        nomorWaypoint = 0;
    }
}
