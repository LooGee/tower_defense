using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    //목표물
    private Transform target;
    private Enemy targetEnemy;

    [Header("General")]
    //감지범위
    public float range = 15f;


    [Header("Use Bullets (defalult)")] //총알 오브젝트, 발사대기, 발사시간
    public GameObject bulletPrefeb;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Use Laser")] // 레이저 확인
    public bool useLaser = false;
    public int damageOverTime = 30;
    public float slowAmount = .5f;

    //레이저 이펙트
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;

    [Header("Unity Setup Fields")]
    //적의 테그값, 터렛회전값, 회전할 물체, 총알발사위치
    public string enemyTag = "Enemy";
    public float turnSpeed = 10f;
    public Transform partToRotate;
    public Transform firePoint;

    [Header("TurretClass")]
    public int TurretTypeNum;
    public int TurretRank;


    void Start()
    {
        //0.5초 마다 실행하기
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        //타겟이 없다면 리턴
        if (target == null)
        {
            //레이저 터렛이라면
            if (useLaser)
            {
                //레이저가 켜져있다면
                if (lineRenderer.enabled)
                {
                    // 레이저 끄고 이펙트 멈추기
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                }
            }
            return;
        }

        //타겟이 있다면
        LockOnTarget();
        //레이저 터렛이라면        
        if (useLaser)
        {
            Laser();
        }
        else
        {            //발사대기
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
            fireCountdown -= Time.deltaTime;
        }
    }

    //타겟 바라보기
    void LockOnTarget()
    {        //현재거리, 바라보는 방향, 터렛 회전방법, 터렛방향
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    //레이저 터렛
    void Laser()
    {   //레이저 데미지, 슬로우
        targetEnemy.TakeDamaage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowAmount);
        //레이저가 꺼져있다면 다시 켜기
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
        }
        //라인 렌더러의 시작 포지션과 끝 포지션
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);
        //이펙트 포현 위치,방향
        Vector3 dir = firePoint.position - target.position;
        impactEffect.transform.position = target.position + dir.normalized * .5f;
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);


    }

    // 발사
    void Shoot()
    {   //오브젝트를 발사위치 및 방향에 맞춰서 복사
        GameObject bulletgo = (GameObject)Instantiate(bulletPrefeb, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletgo.GetComponent<Bullet>();
        //총알이 있다면        
        if (bullet != null)
        {   //총알 스크립트에 타겟 보내기
            bullet.Seek(target);
        }
    }

    //타겟 확인
    void UpdateTarget()
    {
        //적      
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //최단거리 = 무한대
        float shortestDistance = Mathf.Infinity;
        //가장 가까운적
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {   //적과의 현재 거리
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            //무한대 보다 적과의 현재거리가 작다면
            if (distanceToEnemy < shortestDistance)
            {
                //현재적과의 거리
                shortestDistance = distanceToEnemy;
                //적은 가장 가까운 적
                nearestEnemy = enemy;
            }
        }
        //가장 가까운적이 있고 && 최단거리가 내 반경보다 작다면
        if (nearestEnemy != null && shortestDistance <= range)
        {
            Debug.Log("FindEnemy");
            //목표는 가장가까운 적의 정보값
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }
    //타워 범위 보여주기
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
