using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    //적 유닛 시작시 이동속도
    public float startSpeed = 10f;

    [HideInInspector]
    public float speed;
    //적 유닛 체력
    public float startHealth = 100;
    private float health;

    //죽을때 주는 금액
    public int worth = 50;
    //죽을때 이펙트
    public GameObject deathEffect;
    //체력바 이미지
    [Header("Unity Stuff")]
    public Image healthBar;

 void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }

    //데미지를 입을때 다른곳에서 데미지값 가져오기
    public void TakeDamaage(float amount)
    {
        health -= amount;
        healthBar.fillAmount = health / startHealth;

        if(health <= 0)
        {
            Die();
        }
    } 

    //슬로우
    public void Slow(float pct)
    {
        speed = startSpeed * (1f - pct);
    }

    void Die()
    {
        PlayerStats.Money += worth;
        //죽으면 살아있는 적의 수 줄이기
        
        Destroy(gameObject);
        WaveSpawner.EnemiesAlive--;
    }   
}
