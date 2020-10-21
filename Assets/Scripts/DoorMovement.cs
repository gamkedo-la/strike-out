using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMovement : MonoBehaviour
{
    public bool isRedOpen, isRedClosed, isGreenOpen, isGreenClosed;
    Vector3 isClosed = new Vector3(0, -10, 0);
    Vector3 isOpen = new Vector3(0, +10, 0);

    public AudioData gateOpenAudio;
    public AudioData gateCloseAudio;
    public AudioSourceController audioController;
    private Vector3 storedPos;

    // Start is called before the first frame update
    void Start()
    {

        if (isRedOpen)
        {
            this.transform.position += isClosed;
        }

        if (isGreenOpen)
        {
            this.transform.position += isClosed;
        }

        if(gateOpenAudio == null)
            gateOpenAudio = FindObjectOfType<AudioButtonAction>().data.GateOpen;

        if (gateCloseAudio == null)
            gateCloseAudio = FindObjectOfType<AudioButtonAction>().data.GateClose;

        if (audioController == null)
            audioController = gameObject.AddComponent<AudioSourceController>();

        
        storedPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (HOEGameManager.redToggle)
        {
            if (isRedOpen)
            {
                this.transform.position = new Vector3(this.transform.position.x, 3, this.transform.position.z);
            }
            if (isRedClosed)
            {
                this.transform.position = new Vector3(this.transform.position.x, -3.5f, this.transform.position.z);
            }
        }

        if (!HOEGameManager.redToggle)
        {
            if (isRedOpen)
            {
                this.transform.position = new Vector3(this.transform.position.x, -3.5f, this.transform.position.z);
            }
            if (isRedClosed)
            {
                this.transform.position = new Vector3(this.transform.position.x, 3, this.transform.position.z);
            }
        }

        if (HOEGameManager.greenToggle)
        {
            if (isGreenOpen)
            {
                this.transform.position = new Vector3(this.transform.position.x, 3, this.transform.position.z);
            }
            if (isGreenClosed)
            {
                this.transform.position = new Vector3(this.transform.position.x, -13, this.transform.position.z);
            }
        }

        if (!HOEGameManager.greenToggle)
        {
            if (isGreenOpen)
            {
                this.transform.position = new Vector3(this.transform.position.x, -13, this.transform.position.z);
            }
            if (isGreenClosed)
            {
                this.transform.position = new Vector3(this.transform.position.x, 3, this.transform.position.z);
            }
        }

        GateOpenCloseAudio();
    }

    void GateOpenCloseAudio()
    {
        if (this.transform.position != storedPos)
        {
            if (this.transform.position.y == storedPos.y - 3.5f || this.transform.position.y == storedPos.y - 13f)
            {
                if(audioController != null)
                    audioController.PlayRandom(gateOpenAudio);
            }
            else
            {
                if (audioController != null)
                    audioController.PlayRandom(gateCloseAudio);
            }
        }

        StoreNewPos(this.transform.position);
    }

    void StoreNewPos(Vector3 newPosition)
    {
        storedPos = newPosition;
    }
}
