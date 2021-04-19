using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodBuilder : MonoBehaviour
{
    public GameObject[] trees; //массивы откуда берем 
    
    /// <summary>
    /// Переменная типа transform, хранящая платформы
    /// </summary>
    public Transform treeContainer; //здесь будут храниться все платформы для их совместного передвижения
    private Transform lastTree = null; //последняя сгенерированная платформа

    void Start()
    {
        Init();
    }
    
    public void Init()
    {
        
        for (int i = 0; i < 20; i++)
        {
            CreateTree();
        }
    }   
    
    public void CreateTree()
    {
        Vector3 position = (lastTree == null) ?
            treeContainer.position : //если это первая платформа, то ставим координаты контейнера
            lastTree.GetComponent<TreesController>().endPoint.position; //если не первая, то ставим координаты
        //обращаемся к endPoint типа трансформ последней платформы

        //lastPlatform.Find("End");  поиск объекта внутри иерархии по названию
        int index = Random.Range(0, trees.Length); //первое включительно, второе не включительно
        //как только мы создали платформу, нам нужно сделать ее последней платформой
        GameObject result = Instantiate(trees[index], position, Quaternion.identity, treeContainer);
        lastTree = result.transform;
    }
}
