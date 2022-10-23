using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] Slider volumeSlider;

    public void GoToMenu()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        Debug.Log("Menu");
    }
    public void GoToOptions()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
        Debug.Log("Option");
    }
    public void OnPlayClick()
    {
        SceneManager.LoadScene("Gameplay");
        Debug.Log("NewGame");
    }
    public void Quit()
    {
        // #if UNITY_EDITOR
        //         UnityEditor.EdiorApplication.isPlaying = false;
        // #else
        //         Application.Quit();
        // #endif
        Application.Quit();
        Debug.Log("Game Keluar :" + Application.productName);
    }

    List<int> widths = new List<int>() { 1024, 1280, 1920 };
    List<int> heights = new List<int>() { 768, 720, 1080 };

    public void SetScreenSize(int index)
    {
        int width = widths[index];
        int height = heights[index];
        bool fullscreen = Screen.fullScreen;
        Screen.SetResolution(width, height, fullscreen);
        Debug.Log("Resolution :" + width + "x" + height);
    }
    public void SetFullScreen(bool _fullscreen)
    {
        Screen.fullScreen = _fullscreen;
        Debug.Log("Resolution Fullscreen :" + _fullscreen);
    }
    public void Awake()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            SetVolume(PlayerPrefs.GetFloat("Volume"));
            volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        }
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
        Debug.Log("Volume :" + volume);
    }
    public void SetMute(bool muted)
    {
        if (muted)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = 1;
        }
        Debug.Log("Volume" + muted);
    }

}
