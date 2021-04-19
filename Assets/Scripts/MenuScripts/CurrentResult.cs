using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentResult : MonoBehaviour
{
    /// <summary>
    /// Поле отображения пройденной дистанции на экране
    /// </summary>
    public Text distanceText;
    /// <summary>
    /// Поле отображения количества очков на экране
    /// </summary>
    public Text scoreText;

    void Update()
    {
        int distance = (WorldController.platformCounter - 4) * 5;
        distanceText.text = $"Пройденная дистанция: {distance} м";
        int score = BonusDissapear.score * 10;
        scoreText.text = $"Очков набрано: {score}";
    }
}
