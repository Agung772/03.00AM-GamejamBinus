using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMaling : MonoBehaviour
{
    public GameObject followMaling, cameraUtama, penutupLantai;
    public Vector3 offset = new Vector3(0, 0, -10);

    private void Awake()
    {
        followMaling = GameObject.FindGameObjectWithTag("Maling");
        cameraUtama.SetActive(false);
        penutupLantai.GetComponent<Animator>().SetBool("Start", false);
        Destroy(gameObject, 5);
    }
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, followMaling.transform.position + offset, Time.deltaTime);
    }
    private void OnDisable()
    {
        cameraUtama.SetActive(true);
        penutupLantai.GetComponent<Animator>().SetBool("Start", true);
    }
}
