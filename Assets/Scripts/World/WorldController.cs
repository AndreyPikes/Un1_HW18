using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс для движения platformContainer
/// </summary>
public class WorldController : MonoBehaviour 
{
    public GameObject player;
    public WorldBuilder worldBuilder;
    public float minZ = -20;
    public static int platformCounter = 0;

    public delegate void tryDeleteAddPlatform();
    /// <summary>
    /// Событие движения платформ
    /// </summary>
    public event tryDeleteAddPlatform OnPlatformMoving;
    /// <summary>
    /// сиглтон 
    /// </summary>
    public static WorldController instancePlatform;
    // Start is called before the first frame update

    private void Awake() //как только компонент загрузится
    {
        if (WorldController.instancePlatform != null) //если этот объект уже есть, то не надо создавать новый
        {
            Destroy(this); //gameObject того объекта, который случайно создался
            return;
        }
        WorldController.instancePlatform = this;
        //DontDestroyOnLoad(this); //чтобы при переходе с одной сцены на другую
    }

    private void OnDestroy()
    {
        WorldController.instancePlatform = null; //при удалении
        platformCounter = 0;
        BonusDissapear.score = 0;
    }

    void Start()
    {
        StartCoroutine(OnPlatformMovementCorutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (platformCounter > 40) Speed.value = 6.0f;
        //движение platformContainer
        if (player.GetComponent<PlayerController>().dead != true)
        {
            transform.position -= Vector3.forward * Speed.value * Time.deltaTime;
        }        
    }

    IEnumerator OnPlatformMovementCorutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (OnPlatformMoving != null) //в случае, если никто еще не подписан на это событие
            {
                OnPlatformMoving();
            }
        }
    }
}
