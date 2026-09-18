using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartEasy() => StartWithMode(20);
    public void StartNormal() => StartWithMode(55);

    private void StartWithMode(int mode)
    {
        PlayerPrefs.SetInt("DeckMode", mode);
        SceneManager.LoadScene("Game");
    }
}