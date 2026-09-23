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

    void EnableHitBox(GameObject hitBox)
    {
        if(hitBox != null)
        {
            hitBox.SetActive(true);
            Hitbox hitboxComponent = hitBox.GetComponent<Hitbox>();
            if (hitboxComponent != null)
            {
                hitboxComponent.hitsRemaining = 1;
            }
        }
    }

    void DisableHitBox(GameObject hitBox)
    {
        if (hitBox != null)
        {
            hitBox.SetActive(false);
        }
    }

    void EnableHitBox_LeftHand()
    {
        EnableHitBox(hitBox_LeftHand);
    }

    void DisableHitBox_LeftHand()
    {
        DisableHitBox(hitBox_LeftHand);
    }

    void EnableHitBox_RightHand()
    {
        EnableHitBox(hitBox_RightHand);
    }

    void DisableHitBox_RightHand()
    {
        DisableHitBox(hitBox_RightHand);
    }
}
