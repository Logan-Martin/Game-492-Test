using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        if(animator != null)
        {
            animator.SetBool("spawning", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
