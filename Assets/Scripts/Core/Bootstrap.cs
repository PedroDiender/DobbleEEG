using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    private void Start()
    {
        int mode = 4;
        GameManager.Instance.StartGame(mode);
    }
}