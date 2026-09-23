using UnityEngine;

public class HitBoxEvents : MonoBehaviour
{
    public GameObject hitBox_LeftHand;
    public GameObject hitBox_RightHand;

    void Start()
    {
        if (hitBox_LeftHand != null)
        {
            hitBox_LeftHand.SetActive(false);
        }

        if (hitBox_RightHand != null)
        {
            hitBox_RightHand.SetActive(false);
        }
    }

    void EnableHitBox_LeftHand()
    {
        if (hitBox_LeftHand != null)
        {
            hitBox_LeftHand.SetActive(true);
        }
    }

    void DisableHitBox_LeftHand()
    {
        if (hitBox_LeftHand != null)
        {
            hitBox_LeftHand.SetActive(false);
        }
    }

    void EnableHitBox_RightHand()
    {
        if (hitBox_RightHand != null)
        {
            hitBox_RightHand.SetActive(true);
        }
    }

    void DisableHitBox_RightHand()
    {
        if (hitBox_RightHand != null)
        {
            hitBox_RightHand.SetActive(false);
        }
    }
}
