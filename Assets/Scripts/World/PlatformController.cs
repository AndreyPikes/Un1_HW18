using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс конкретной платформы, хранящий endPoint. 
/// </summary>
public class PlatformController : MonoBehaviour //висит на платформе
{
    /// <summary>
    /// Переменная типа transform хранящая координату конца последней платформы
    /// </summary>
    public Transform endPoint;
    /// <summary>
    /// Функция создания и удаления платформы
    /// </summary>
    private void TryDellAddPlatform()
    {
        if (transform.position.z < WorldController.instancePlatform.minZ)
        {
            WorldController.instancePlatform.worldBuilder.CreatePlatform();
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        WorldController.instancePlatform.OnPlatformMoving += TryDellAddPlatform;
    }


    private void OnDestroy()
    {
        if (WorldController.instancePlatform != null)
        {
            WorldController.instancePlatform.OnPlatformMoving -= TryDellAddPlatform;
        }
    }
}
