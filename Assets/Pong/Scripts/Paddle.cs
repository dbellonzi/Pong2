using UnityEngine;

public class Paddle : MonoBehaviour
{
    public AudioClip hitSound;
    public AudioSource speaker;



    //-----------------------------------------------------------------------------
    public void MadeContact(float speed)
    {
        speaker.pitch = speed/2;
        speaker.PlayOneShot(hitSound);
    }
}
