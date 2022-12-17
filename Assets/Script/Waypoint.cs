using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public bool sudahTrigger;
    public enum KemanaTujuanSelanjutnya
    {
        kanan, kiri, atas, bawah
    }

    public KemanaTujuanSelanjutnya kemanaTujuanSelanjutnya;
    public int nomorWaypoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<MalingController>() && !sudahTrigger && nomorWaypoint == collision.GetComponent<MalingController>().nomorWaypointSaatIni)
        {
            sudahTrigger = true;

            collision.GetComponent<MalingController>().ChangeWaypoint(nomorWaypoint);

            if (kemanaTujuanSelanjutnya == KemanaTujuanSelanjutnya.kanan)
            {
                collision.GetComponent<MalingController>().AnimasiMaling("Kanan");
            }
            else if (kemanaTujuanSelanjutnya == KemanaTujuanSelanjutnya.kiri)
            {
                collision.GetComponent<MalingController>().AnimasiMaling("Kiri");
            }
            else if (kemanaTujuanSelanjutnya == KemanaTujuanSelanjutnya.atas)
            {
                collision.GetComponent<MalingController>().AnimasiMaling("Atas");
            }
            else if (kemanaTujuanSelanjutnya == KemanaTujuanSelanjutnya.bawah)
            {
                collision.GetComponent<MalingController>().AnimasiMaling("Bawah");
            }
        }
    }
}
