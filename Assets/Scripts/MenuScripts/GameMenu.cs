using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public GameObject menu; //панель игрового меню
    public AudioMixer audioMixer; //AudioMixer для считывания и установки значений громкости
    public GameObject player; //смотрим флаг dead в игроке
    public Slider volumeSlider;
    private float volume;
    public Slider effectsSlider;
    private float effects;

    private void Start()
    {
        QualitySettings.vSyncCount = 1;
        menu.SetActive(false);
        audioMixer.GetFloat("ParamMusic", out volume); //считываем текущее значение громкости в переменную
        volumeSlider.value = volume;
        audioMixer.GetFloat("ParamEffects", out effects); //считываем текущее значение громкости в переменную
        effectsSlider.value = effects;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !player.GetComponent<PlayerController>().dead)
        {
            if (menu.activeInHierarchy)
            {
                Time.timeScale = 1;
                menu.SetActive(false);
            }
            else            
            {
                Time.timeScale = 0;
                menu.SetActive(true);
            }
        }
    }
    /// <summary>
    /// Для кнопки возврата в игру из меню
    /// </summary>
    public void ReturnToGame()
    {
        Time.timeScale = 1;
        menu.SetActive(false);
    }
    /// <summary>
    /// Для кнопки выхода в главное меню
    /// </summary>
    public void ReturnToMainMenu()
    {        
        SceneManager.LoadScene(0);        
    }
    /// <summary>
    /// Для слайдера громкости музыки
    /// </summary>
    /// <param name="value"></param>
    public void MusicVolume(float value)
    {
        audioMixer.SetFloat("ParamMusic", value);
    }
    /// <summary>
    /// Для слайдера громкости эффектов
    /// </summary>
    /// <param name="value"></param>
    public void EffectsVolume(float value)
    {
        audioMixer.SetFloat("ParamEffects", value);
    }
}
