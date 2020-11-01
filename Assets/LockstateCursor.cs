using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockstateCursor : MonoBehaviour
{
    bool isLocked;
    // Start is called before the first frame update
    void Start()
    {
        isLocked = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isLocked = !isLocked;
        }

        if (isLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (!isLocked)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
