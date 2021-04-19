using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreesController : MonoBehaviour
{
    /// <summary>
    /// Переменная типа transform хранящая координату конца последней платформы
    /// </summary>
    public Transform endPoint;
    /// <summary>
    /// Функция создания и удаления платформы
    /// </summary>
    private void TryDellAddTree()
    {
        if (transform.position.z < WoodController.instanceTree.minZ)
        {
            WoodController.instanceTree.woodBuilder.CreateTree();
            Destroy(this);
        }
    }

    void Start()
    {
        WoodController.instanceTree.OnTreeMoving += TryDellAddTree;       
    }
    
    private void OnDestroy()
    {
        if (WoodController.instanceTree != null)
        {
            WoodController.instanceTree.OnTreeMoving -= TryDellAddTree;
        }       
    }
}
