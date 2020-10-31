using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScene : MonoBehaviour
{
    public GameObject Dialogue;

    public void TriggerDialogue()
    {
        Dialogue.SetActive(true);
    }
}
