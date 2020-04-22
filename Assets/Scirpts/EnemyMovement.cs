using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    
    //이동방향
    private int wavepointIndex = 0;

    private Enemy enemy;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        //목표 위치는 웨이포인트
        target = Waypoints.points[0];
    }

    void Update()
    {
        //목표와 유닛의 위치를 뺸값 
        Vector3 dir = target.position - transform.position;
        //월드좌표 기준으로 오브젝트의 위치값을 dir.normalized * speed * Time.deltaTime 속도로 이동하기
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);
        //유닛과 목표의 거리가 0.3f 보다 작거나 같다면
        if (Vector3.Distance(transform.position, target.position) <= 0.3f)
        {
            //다음 웨이포인트로 변경
            GetNextWaypoint();
        }

        enemy.speed = enemy.startSpeed;

    }

    void GetNextWaypoint()
    {
        //현재 웨이포인트가 마지막 웨이포인트면 유닛삭제
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }
        //다음 웨이포인트
        wavepointIndex++;
        //다음 웨이포인트가 목표
        target = Waypoints.points[wavepointIndex];
    }

    void EndPath()
    {
        PlayerStats.Lives--;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}
