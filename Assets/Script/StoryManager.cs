using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoryManager : MonoBehaviour
{
    public float timeChange, timeAnimasi;
    public string[] isiText;
    public Text textTMPro;
    public Animator animatortextTMPro;

    private void Awake()
    {
    }
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(timeChange);
        textTMPro.gameObject.SetActive(false);
        yield return new WaitForSeconds(timeAnimasi);

        GameManager.instance.allButton.SetActive(true);

    }
}
