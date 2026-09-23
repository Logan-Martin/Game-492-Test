using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public enum CombatTeam
    {
        Player,
        Enemy
    }

    public CombatTeam team;
    public int hitsRemaining;
    
}
