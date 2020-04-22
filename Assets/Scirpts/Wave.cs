using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//시스템화
[System.Serializable]
public class Wave
{
    //생성할 적, 갯수, 대기시간
    public GameObject enemy;
    public int count;
    public float rate;
}
