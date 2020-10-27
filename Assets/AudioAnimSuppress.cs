using UnityEngine;

public class AudioAnimSuppress : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        animator.fireEvents = false;
    }

}
