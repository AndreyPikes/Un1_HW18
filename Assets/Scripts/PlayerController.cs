using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Получаем данные сенсоров
    [SerializeField] private MoveSensorScript leftSensor;
    [SerializeField] private MoveSensorScript rightSensor;
    [SerializeField] private FrontSensorScript jumpSensor;
    [SerializeField] private DeathSensorScript deathSensor;

    //Внутренние переменные игрока
    private Animator animator;
    private CharacterController cc;

    //Флажки для запрета новых действий в момент выполнения старых
    //private bool isInMovement = false;
    private bool isJumping = false;
    [HideInInspector]public bool dead = false;   //передаем в worldController для остановки платформ
    
    //Перемещение вбок
    [SerializeField] private float rowIndent; //отступ крайних рядов от центра
    private float stepX;   //перемещение за один кадр
    private float dir; //направление движения вбок
    private Vector3 target; //целевая координата при перемещении вбок
    private float currentX; //текущая координата по оси Х
    
    //Прыжок вверх
    [SerializeField] private float speedY_O; //начальная скорость вверх
    private float G = -9.8f; //ускорение свободного падения
    private float speedY;   //начальная скорость по оси У

    public ParticleSystem psDust;
    public ParticleSystem psSteam;

    void Start()
    {
        QualitySettings.vSyncCount = 1;
        speedY = speedY_O; 
        animator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>(); 
        var animInfo = animator.GetCurrentAnimatorStateInfo(0);
        float time = animInfo.length;
        Debug.Log($"time - {Time.deltaTime}");
        stepX = rowIndent / time * Time.deltaTime;
        psDust.Play();
        psSteam.Play();
        StartCoroutine("IMoving");
    }
   
    void Update()
    {    
        if (!dead && deathSensor.dead)
        {
            Death();            
        }
        //Вертикальная ось        
        bool jumpKeyPressed = Input.GetKeyDown(KeyCode.Space);
        if (!dead &&!isJumping && jumpKeyPressed && (jumpSensor.permissionToJump))
        {//обеспечиваем анимацию и готовим и щелкаем триггер на прыжок
            isJumping = true;
            JumpSoundScript.SoundJump();
            psDust.Stop();
            animator.SetTrigger("Jump");
        }
        if (isJumping)
        {//реализуем прыжок

            Jump();
        }
    }
    IEnumerator IMoving()
    {
        while (!dead)
        {
            dir = Input.GetAxisRaw("Horizontal");
            if (dir != 0) 
            {
                if (((dir > 0) && (!rightSensor.permissionToMove))
                    || ((dir < 0) && (!leftSensor.permissionToMove)))
                {
                    yield return new WaitForEndOfFrame();
                }
                else
                {
                    if (Mathf.Abs(transform.position.x) <= 0.2)
                    {
                        target = new Vector3(dir * rowIndent, 0, 0);
                    }
                    else
                    {
                        target = new Vector3(0, 0, 0);
                    }

                    if ((dir > 0) && (transform.position.x < 1))
                    {
                        animator.SetTrigger("Right");
                        yield return StartCoroutine("IMoveToTarget");                        

                    }
                    if ((dir < 0) && (transform.position.x > -1))
                    {
                        animator.SetTrigger("Left");
                        yield return StartCoroutine("IMoveToTarget");                                      
                    }
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator IMoveToTarget()
    {
        while (this.transform.position != target)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, target, stepX);
            yield return null;
        }
    }

    private void Jump()
    {
        speedY += G * Time.deltaTime;
        cc.Move(new Vector3(0.0f, speedY * Time.deltaTime, 0.0f));
        if (cc.isGrounded && cc.transform.position.y <= 0.1)
        {
            isJumping = false;
            speedY = speedY_O;
            psDust.Play();
        }
    }
    
    private void Death()
    {
        cc.enabled = false;
        dead = true;        
        animator.enabled = false;
        psDust.Stop();
        psSteam.Stop();
        DeadSoundScript.SoundDead();
    }

    public void OnDestroy()
    {
        isJumping = false;
        dead = false;
    }
}
