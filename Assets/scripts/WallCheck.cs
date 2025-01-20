using UnityEngine;

public class WallCheck : MonoBehaviour
{
    public static WallCheck Instance;
    public bool isTouchingWall { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("ground"))
        {
            isTouchingWall = true;
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ground"))
        {
            isTouchingWall = false;
        }

    }
}
