using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMovement : MonoBehaviour
{
    public bool isRedOpen, isRedClosed, isGreenOpen, isGreenClosed;
    Vector3 isClosed = new Vector3(0, -10, 0);
    Vector3 isOpen = new Vector3(0, +10, 0);
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
                this.transform.position = new Vector3(this.transform.position.x, -13, this.transform.position.z);
            }
        }

        if (!HOEGameManager.redToggle)
        {
            if (isRedOpen)
            {
                this.transform.position = new Vector3(this.transform.position.x, -13, this.transform.position.z);
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
    }
}
