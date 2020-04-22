using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    //웨이브의 살아있는 적
    public static int EnemiesAlive = 0;

    //웨이브 크기
    public Wave[] waves;
    //스폰 위치
    public Transform spawnPoint;

    //다음 웨이브까지의 시간
    public float timeBetwweenWaves = 5f;
    //게임 시작 카운트다운
    private float countdown = 2f;

    //웨이브 시간 출력
    public Text waveCountdown;

    public Text WaveCount;

    //웨이브
    private int waveIndex = 0;

    void Update()
    {

        //적이 살아있다면
        if (EnemiesAlive > 0)
        {
            return;
        }
        //게임시작 시간이 된다면
        if (countdown <= 0f)
        {
            //적 유닛 스폰 시작
            StartCoroutine(SpawnWave());
            //게임 카운트다운 초기화
            countdown = timeBetwweenWaves;
        }

        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        //Mathf.Round 정수 한 정수로 반올림 한 / f /를 반환합니다.
        // 숫자가 .5로 끝나서 두 정수 사이의 중간에있는 경우, 하나는 짝수이고 다른 홀수는 짝수입니다.
        waveCountdown.text = "Time" + " " + string.Format("{0:0.0}", countdown);
        WaveCount.text = "Wave" + " " + PlayerStats.Rounds.ToString();
    }

    //적 유닛 웨이브
    IEnumerator SpawnWave()
    {
        //현재 라운드
        PlayerStats.Rounds++;

        //웨이브 정보
        Wave wave = waves[waveIndex];

        //현재 웨이브의 살아있는 적 갯수
        EnemiesAlive = wave.count;

        //웨이브의 적 갯수
        for (int i = 0; i < wave.count; i++)
        {
            //현재 웨이브의 적 생성
            spawnEnemy(wave.enemy);
            //0.5초 기다렸다 다시 실행
            yield return new WaitForSeconds(wave.rate);
        }

        //다음 웨이브
        waveIndex++;

        Debug.Log("Wave Incomming!");
        Debug.Log(waveIndex);

        //웨이브 정보는 웨이브 크기랑 같음 
        if (waveIndex == waves.Length)
        {
            Debug.Log("Level won");
            this.enabled = false;
        }
    }

    //적 유닛 소환
    void spawnEnemy(GameObject enemy)
    {
        //적 유닛을 시작위치,방향 맞춰서 복사생성하기
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);

    }

}
