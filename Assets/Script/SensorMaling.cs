using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorMaling : MonoBehaviour
{
    public MalingController malingController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !PlayerController.instance.sedangSembunyi)
        {
            malingController.patrol = false;


            if (PlayerController.instance.playerPegangPistol)
            {
                GameManager.instance.WinPistol();
                malingController.agent.speed = 0;
                malingController.enabled = false;
                print("afsa");
            }
            if (PlayerController.instance.playerPegangPisau)
            {
                GameManager.instance.WinPisau();
                malingController.agent.speed = 0;
                malingController.enabled = false;
            }
        }
    }


}
