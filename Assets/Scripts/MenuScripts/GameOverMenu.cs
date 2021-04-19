using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOver; //всплывающая надпись GAMEOVER
    public GameObject menuExit; //панель меню после смерти
    public GameObject player; //смотрим на игроке флаг dead
    private bool alreadyDead = false; //флаг, позволяющий вызывать корутину смерти только один раз

    private void Start()
    {
        menuExit.SetActive(false);
        gameOver.SetActive(false);
    }

    void Update()
    {
        if (!alreadyDead && player.GetComponent<PlayerController>().dead)
        {            
            StartCoroutine("GameOver");
            alreadyDead = true;
        }
    }
    /// <summary>
    /// Для кнопки запуска новой игры
    /// </summary>
    public void NewGame()
    {
        menuExit.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
    /// <summary>
    /// Для кнопки возврата в главное меню
    /// </summary>
    public void ReturnToMainMenu()
    {
        menuExit.SetActive(false);
        SceneManager.LoadScene(0);
    }
    /// <summary>
    /// Корутина смерти для более красивого вывода надписи GAMEOVER и меню в два этапа
    /// </summary>
    /// <returns></returns>
    IEnumerator GameOver()
    {
        gameOver.SetActive(true);
        yield return new WaitForSeconds(2f);
        menuExit.SetActive(true);
        yield return null;
    }
}
