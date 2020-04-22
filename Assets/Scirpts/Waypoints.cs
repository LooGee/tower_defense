using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static Transform[] points;
    
    void Awake()
    {
        //자식 오브젝트 카운트
        points = new Transform[transform.childCount];
        
        //자식 오브젝트 전체 갯수 확인하기
        for (int i =0;i<points.Length;i++)
        {
            points[i] = transform.GetChild(i);
        }
    }

}
