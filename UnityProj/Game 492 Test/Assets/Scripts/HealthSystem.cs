//using System;
//using TMPro;
using System.Collections;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private float health;
    public float maxHealth = 100f;
    //public TextMeshProUGUI healthTextComponent;
    //public TeleportSystem teleportSystemScript;
    public GameObject playerRef;
    //public DestroyAllEnemiesScript destroyAllEnemiesScript;
    public Animator animator;

    //public event EventHandler OnPlayerDeath;
    //https://www.youtube.com/watch?v=OUDBGiAiOqA


    // Flash Red when taking Damage //
    // taken from my Shadow Adventure game code
    // Wont work on Chars bc they have a bunch of Meshes instead of being a single mesh
    private Renderer objectRenderer;
    private Color originalColor;
    public Color damageColor = Color.red;
    public float flashDuration = 0.15f;
    //
    private IEnumerator FlashRoutine()
    {
        // Change to damage color
        objectRenderer.material.SetColor("_BaseColor", damageColor);
        yield return new WaitForSeconds(flashDuration);
        // Revert to original color
        objectRenderer.material.SetColor("_BaseColor", originalColor);
    }
    private void DoRedFlashWhenTakingDamage()
    {
        StartCoroutine(FlashRoutine());
    }
    // -------------------------- //

    private void Start()
    {
        health = maxHealth;
        //healthTextComponent.text = "Health: " + health + " / " + maxHealth;
        animator = GetComponent<Animator>();
        //
        //objectRenderer = this.gameObject.GetComponent<Renderer>();
        //originalColor = objectRenderer.material.GetColor("_BaseColor");
    }

    public float GetHealth()
    {
        return health;
    }

    public void AddOrSubToHealth(float amount)
    {
        float tempCheck = health + amount;
        if (tempCheck > maxHealth)
        {
            tempCheck = maxHealth;
        }
        if (tempCheck < 0)
        {
            tempCheck = 0;
            animator.SetTrigger("PlayCharDeathAnim");
            ResetPlayer();
            return;
        }
        health = tempCheck;
        print("took damage!");
        animator.SetTrigger("PlayCharTakingPunchAnim");
        //healthTextComponent.text = "Health: " + health + " / " + maxHealth;
    }

    private void ResetPlayer()
    {
        health = maxHealth;
        print("Reset!");
        //healthTextComponent.text = "Health: " + health + " / " + maxHealth;

        //teleportSystemScript.TeleportPlayerToLastSpawn();
        //destroyAllEnemiesScript.StartCoroutine("DestroyAllEnemies");

        // waits till above
        //OnPlayerDeath?.Invoke(this, EventArgs.Empty);
        // ? for checking null ref. like if I have this invoke but nobody listens
        // if I didn't have that ?, it'd throw an error for that case
    }
}