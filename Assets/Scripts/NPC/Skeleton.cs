using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Skeleton : MonoBehaviour
{
    [SerializeField] private string monsterName; // 몬스터 이름
    [SerializeField] private int hp; // 몬스터 체력
    [SerializeField] private float walkSpeed; // 걷기 스피드

    private Vector3 direction; // 방향

    private bool isAction; // 행동 판별
    private bool isWalking; // 걷기 판별

    [SerializeField] private float walkTime; // 걷기 시간
    [SerializeField] private float waitTime; // 대기 시간
    private float currentTime;
    
    // 필요한 컴포넌트
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody rigid;
    [SerializeField] private BoxCollider boxCol;
    
    // Start is called before the first frame update
    void Start()
    {
        currentTime = waitTime;
        isAction = true;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotation();
        ElapseTime();
    }

    private void Move()
    {
        if (isWalking)
            rigid.MovePosition(transform.position + (transform.forward * walkSpeed * Time.deltaTime));
    }

    private void Rotation()
    {
        if (isWalking)
        {
            Vector3 rotation = Vector3.Lerp(transform.eulerAngles, direction, 0.01f);
            rigid.MoveRotation(Quaternion.Euler(rotation));
        }
    }

    private void ElapseTime()
    {
        if (isAction)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
                ReSet();
        }
    }

    private void ReSet()
    {
        isWalking = false;
        isAction = true;
        
        anim.SetBool("Walking", isWalking);
        direction.Set(0f, Random.Range(0f, 360f), 0f);
        RandomAction();
    }

    private void RandomAction()
    {
        int random = Random.Range(0, 3);

        if (random == 0)
            Wait();
        else if (random == 1)
            TryWalk();
        else if (random == 2)
            Stand();
    }

    private void Wait()
    {
        currentTime = waitTime;
        Debug.Log("대기");

    }

    private void Stand()
    {
        currentTime = waitTime;
        anim.SetTrigger("Stand");
        Debug.Log("서 있기");

    }
    
    private void TryWalk()
    {
        isWalking = true;
        anim.SetBool("Walking", isWalking);
        currentTime = walkTime;
        Debug.Log("걷기");

    }
}
