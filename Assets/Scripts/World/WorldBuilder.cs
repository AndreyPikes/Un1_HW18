using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс создает платформы в platformContainer
/// </summary>
public class WorldBuilder : MonoBehaviour
{
    public GameObject[] emptyPlatforms; //массивы откуда берем 
    public GameObject[] obstaclePlatforms;
    public GameObject[] bonuses;
    /// <summary>
    /// Переманная типа transform, хранящая платформы
    /// </summary>
    public Transform platformContainer; //здесь будут храниться все платформы для их совместного передвижения
    private Transform lastPlatform = null; //последняя сгенерированная платформа

    void Start()
    {
        QualitySettings.vSyncCount = 1;
        Init();
    }    
    
    public void Init()
    {
        for (int i = 0; i < 3; i++)
        {
            CreateEmptyPlatform();
        }
        for (int i = 0; i < 4; i++)
        {
            CreatePlatform();            
        }
    }
    /// <summary>
    /// Создает рандомно платформы пустые и с препятствиями
    /// </summary>
    public void CreatePlatform()
    {
        int a = Random.Range(1, 10);
        if (WorldController.platformCounter < 30)
        {
            if (a < 7)
            {
                CreateEmptyPlatform();
            }
            if (a >= 7)
            {
                CreateObstaclePlatform();
            }
            CreateBonus();
        }
        else
        {
            if (a < 3)
            {
                CreateHardPlatform();
            }
            if (a >= 3)
            {
                CreateObstaclePlatform();
            }
            CreateBonus();
        }        
        WorldController.platformCounter++;
    }

    private void CreateEmptyPlatform()
    {
        Vector3 position = (lastPlatform == null) ?
            platformContainer.position : //если это первая платформа, то ставим координаты контейнера
            lastPlatform.GetComponent<PlatformController>().endPoint.position; //если не первая, то ставим координаты
        //обращаемся к endPoint типа трансформ последней платформы

        //lastPlatform.Find("End");  поиск объекта внутри иерархии по названию
        int index = Random.Range(0, emptyPlatforms.Length); //первое включительно, второе не включительно
        //как только мы создали платформу, нам нужно сделать ее последней платформой
        GameObject result = Instantiate(emptyPlatforms[index], position, Quaternion.identity, platformContainer);
        lastPlatform = result.transform;
    }
    private void CreateObstaclePlatform()
    {
        Vector3 position = (lastPlatform == null) ?
            platformContainer.position : 
            lastPlatform.GetComponent<PlatformController>().endPoint.position; 
        int index = Random.Range(0, 5); 
        GameObject result = Instantiate(obstaclePlatforms[index], position, Quaternion.identity, platformContainer);
        lastPlatform = result.transform;
    }
    private void CreateHardPlatform()
    {
        Vector3 position = (lastPlatform == null) ?
            platformContainer.position :
            lastPlatform.GetComponent<PlatformController>().endPoint.position;
        int index = Random.Range(5, obstaclePlatforms.Length);
        GameObject result = Instantiate(obstaclePlatforms[index], position, Quaternion.identity, platformContainer);
        lastPlatform = result.transform;
    }
    private void CreateBonus()
    {
        if (lastPlatform != null)
        {
            Vector3 position = lastPlatform.GetComponent<PlatformController>().endPoint.position;
            int index = Random.Range(0, 3);
            GameObject result = Instantiate(bonuses[index], position, Quaternion.identity, lastPlatform);            
        }    
    }
}
