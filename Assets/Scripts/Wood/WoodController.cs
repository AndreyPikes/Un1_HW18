using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodController : MonoBehaviour
{    
    public GameObject player;
    public WoodBuilder woodBuilder;
    public float minZ = -20;

    public delegate void tryDeleteAddTree();
    /// <summary>
    /// Событие движения платформ
    /// </summary>
    public event tryDeleteAddTree OnTreeMoving;
    /// <summary>
    /// сиглтон 
    /// </summary>
    public static WoodController instanceTree;
    // Start is called before the first frame update

    private void Awake() //как только компонент загрузится
    {
        if (WoodController.instanceTree != null) //если этот объект уже есть, то не надо создавать новый
        {
            Destroy(this); //gameObject того объекта, который случайно создался
            return;
        }
        WoodController.instanceTree = this;
        //DontDestroyOnLoad(this); //чтобы при переходе с одной сцены на другую
    }

    private void OnDestroy()
    {
        WoodController.instanceTree = null; //при удалении
    }

    void Start()
    {
        StartCoroutine(OnTreeMovementCorutine());
    }

    // Update is called once per frame
    void Update()
    {        
        //движение platformContainer
        if (player.GetComponent<PlayerController>().dead != true)
        {
            transform.position -= Vector3.forward * Speed.value * Time.deltaTime;
        }
    }

    IEnumerator OnTreeMovementCorutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (OnTreeMoving != null) //в случае, если никто еще не подписан на это событие
            {
                OnTreeMoving();
            }
        }
    }
}
