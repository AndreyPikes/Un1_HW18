using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class MainMenu : MonoBehaviour
{
    public static bool firstStart = true; //для отслеживания первого запуска приложения. если первый - то загружаем настройки из файла
    public GameObject mainMenu; //главное меню
    public GameObject settingsMenu; //меню настроек
    public AudioMixer audioMixer; //Audiomixer для считывания и установки значений громкости
    public Slider volumeSlider;
    private float volume;
    public Slider effectsSlider;
    private float effects;

    private void Start()
    {
        if (firstStart)
        {
            LoadGame();
            firstStart = false;
        }

        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);

        audioMixer.GetFloat("ParamMusic", out volume);
        volumeSlider.value = volume;

        audioMixer.GetFloat("ParamEffects", out effects);
        effectsSlider.value = effects;
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        Time.timeScale = 1;        
    }
    public void OpenSettings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }
    public void ExitGame()
    {
        SaveGame();
        Application.Quit();
    }
    /// <summary>
    /// для слайдера настройки уровня громкости музыки
    /// </summary>
    /// <param name="value"></param>
    public void MusicVolume(float value)
    {
        audioMixer.SetFloat("ParamMusic", value);
    }
    /// <summary>
    /// для слайдера настройки уровня громкости эффектов
    /// </summary>
    /// <param name="value"></param>
    public void EffectsVolume(float value)
    {
        audioMixer.SetFloat("ParamEffects", value);
    }
    /// <summary>
    /// для кнопки выхода в главное меню
    /// </summary>
    public void SaveButtonPressed()
    {        
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    [Serializable]    
    class SaveData //сериализуемый класс для упаковки сохраняемых параметров
    {        
        public float savedMusicVolumeValue;
        public float savedEffectsVolumeValue;
    }
    /// <summary>
    /// сохранение настроек игры при закрытии
    /// </summary>
    void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create("/MySaveData.dat");
        SaveData data = new SaveData();
        audioMixer.GetFloat("ParamMusic", out volume);
        data.savedMusicVolumeValue = volume;
        audioMixer.GetFloat("ParamEffects", out effects);
        data.savedEffectsVolumeValue = effects;
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved!");
    }
    /// <summary>
    /// загрузка настроек игры при первом запуске
    /// </summary>
    void LoadGame()
    {
        if (File.Exists("/MySaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
              File.Open("/MySaveData.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            audioMixer.SetFloat("ParamMusic", data.savedMusicVolumeValue);
            audioMixer.SetFloat("ParamEffects", data.savedEffectsVolumeValue);            
            Debug.Log("Game data loaded!");
        }
        else
            Debug.LogError("There is no save data!");
    }
}
