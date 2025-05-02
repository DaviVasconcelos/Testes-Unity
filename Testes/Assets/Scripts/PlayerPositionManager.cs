using UnityEngine;

public class PlayerPositionManager : MonoBehaviour
{
    private static Vector3 savedPosition;

    public static void SavePlayerPosition()
    {
        GameObject player = Player.Instance.gameObject;
        savedPosition = player.transform.position;
    }

    public static void RestorePlayerPosition()
    {
        GameObject player = Player.Instance.gameObject;
        player.transform.position = savedPosition;
    }
}