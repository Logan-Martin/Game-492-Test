using UnityEngine;

public class EnemySpawnerGizmoScript : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.darkRed;
        Gizmos.DrawCube(this.transform.position, Vector3.one * 0.5f);
    }
}
