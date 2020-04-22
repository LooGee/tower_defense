using System.Collections;

using UnityEngine;

[System.Serializable]
public class TurretBlueprint
{
    //생성할 터렛
    public GameObject prefab;
    //터렛 생성 비용

    public int cost = 100;
  
  
    //터렛 판매
    public int GetSellAmount()
    {
        return cost / 2;
    }

}
