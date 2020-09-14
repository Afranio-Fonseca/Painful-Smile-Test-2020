using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    [Header("Scene Elements")]
    [SerializeField]
    Slider sessionTimeSlider = null;
    [SerializeField]
    Slider spawnTimeSlider = null;
    [SerializeField]
    InputField sessionTimeInput = null;
    [SerializeField]
    InputField spawnTimeInput = null;


    // Start is called before the first frame update
    void Start()
    {
        SetSessionTime(PlayerPrefs.GetInt("sessionTime", 60));
        SetSpawnTime(PlayerPrefs.GetInt("spawnTime", 10));
    }

    public void loadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    void SetSessionTime(int value)
    {
        if (value > 180) value = 180;
        if (value < 60) value = 60;
        sessionTimeSlider.value = value;
        sessionTimeInput.SetTextWithoutNotify(value.ToString());
        PlayerPrefs.SetInt("sessionTime", value);
    }

    void SetSpawnTime(int value)
    {
        if (value > 60) value = 60;
        if (value < 1) value = 1;
        spawnTimeSlider.value = value;
        spawnTimeInput.SetTextWithoutNotify(value.ToString());
        PlayerPrefs.SetInt("spawnTime", value);
    }

    public void SetSessionTimeSlider()
    {
        SetSessionTime(Mathf.CeilToInt(sessionTimeSlider.value));
    }

    public void SetSpawnTimeSlider()
    {
        SetSpawnTime(Mathf.CeilToInt(spawnTimeSlider.value));
    }

    public void SetSessionTimeInput()
    {
        if(sessionTimeInput.textComponent.text != "")
        SetSessionTime(int.Parse(sessionTimeInput.textComponent.text));
    }

    public void SetSpawnTimeInput()
    {
        if (spawnTimeInput.textComponent.text != "")
            SetSpawnTime(int.Parse(spawnTimeInput.textComponent.text));
    }


}
