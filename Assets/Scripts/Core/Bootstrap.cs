using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    private void Start()
    {
        int mode = PlayerPrefs.GetInt("DeckMode", 55);
        GameManager.Instance.StartGame(mode);
    }
}