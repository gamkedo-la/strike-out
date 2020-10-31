using UnityEngine;

public class TurnObjectOn : MonoBehaviour
{
    public GameObject toTurnOn;
    public AudioTailObject ShatterBallSound;
    // Start is called before the first frame update
    void Start()
    {
        toTurnOn.SetActive(true);

        if (ShatterBallSound != null)
            ShatterBallSound.PlaySoundWithTail();
    }
}
