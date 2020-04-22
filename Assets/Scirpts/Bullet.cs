using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //타겟
    private Transform target;
    //총알 속도
    public float speed = 70f;

    public int damage = 50;
    public float explosionRadius = 0f;

    //이펙트
    public GameObject impactEffect;

    public void Start()
    {

    }

    // Turret의 타겟을 가져오기
    public void Seek(Transform _target)
    {
        target = _target;

    }

    // Update is called once per frame
    void Update()
    {
        //타겟이 없다면
        if(target == null)
        {
            //총알파괴
            Destroy(gameObject);
            return;
        }
        //거리
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        //거리의 크기? 보다 현재위치????보다 작다면
        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        //월드 좌표를 기준으로 타겟방향으로 이동
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    //타겟명중
    void HitTarget()
    {
        //이펙트를 현재오브젝트의 위치및 방향에 복사, 2초후 이펙트 파괴, 타겟 파괴
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 3f);     

        //폭발 범위 확인하기
        if(explosionRadius>0f)
        {
            SoundManager.instance.PlayMissile();
            Explode();
        }
        else
        {
            SoundManager.instance.PlayStadard();
            Damage(target);
        }
        Destroy(gameObject);
        Debug.Log("Hit");
    }

    //미사일 효과 범위안에 데미지 주기
    void Explode()
    {
        Collider[]  colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider collider in colliders)
        {
            if(collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    //데미지
    void Damage(Transform enemy)
    {
        // Enemy 스크립트가 적용되어 있다면 데미지 주기
        Enemy e = enemy.GetComponent<Enemy>();

        if(e != null)
        {
            e.TakeDamaage(damage);

        }       
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
