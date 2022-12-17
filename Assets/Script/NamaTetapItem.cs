using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamaTetapItem : MonoBehaviour
{
    public string namaItem;
    public bool bisalangsungdiambil;
    private void Start()
    {
        transform.name = namaItem;
    }
}
