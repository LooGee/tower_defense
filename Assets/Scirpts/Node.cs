
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    //색 변경   
    public Vector3 positionOffset;
    //private GameObject upgradeTurret;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public Turret GetTurret;
    [HideInInspector]
    public Turret NewTurret;

    private Renderer rend;
    private Color startColor;

    private NodeUI nodeUi;

    public bool isUpgrade =false;

    [HideInInspector]
    public int isUpgradeText = 1;

    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
        nodeUi = buildManager.nodeUI;

    }
    //터렛을 지을 포지션 위치값
    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    void OnMouseDown()
    {
        // https://docs.unity3d.com/2018.2/Documentation/ScriptReference/EventSystems.EventSystem.IsPointerOverGameObject.html
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        //터렛이 있다면
        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        //터렛을 선택하지 않았다면
        if (!buildManager.CanBuild)
            return;

        //터렛이 없고 설치할 터렛을 선택했다면
        buildManager.tbi.RandomTurretBuildNomal();
        BuildTurret(buildManager.GetTurretBuild());
    }

    void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("돈이부족해");
            return;
        }
        buildManager.Node_Effect_Hide();

        PlayerStats.Money -= blueprint.cost;

        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        turretBlueprint = blueprint;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
      
        buildManager.tbi.RandomTurretBuildNomal();

        Debug.Log("돈으로 타워를 샀어");
    }

    void UpgradeTurret(TurretBlueprint blueprint)
    {
        buildManager.Node_Effect_Hide();
        
        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        turretBlueprint = blueprint;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);          

        Debug.Log("타워를 업그레이드 했어");
    }

    //업그레이드 확인
    public void UpgradeTurretCheck()
    {
        //노드에 있는 터렛의 정보
        GetTurret = turret.GetComponent<Turret>();
        // 맵 전체 터렛      
        GameObject[] _UpgradeTurret = GameObject.FindGameObjectsWithTag("Turret");        
        //최단거리 = 무한대
        float shortestDistance = Mathf.Infinity;
        //가장 가까운터렛
        GameObject nearestTurret = null; 

        foreach (GameObject UpgradeTurrets in _UpgradeTurret)
        {   //터렛과의 현재 거리
            float distanceToTurret = Vector3.Distance(transform.position, UpgradeTurrets.transform.position);
            //무한대 보다 터렛과의 현재거리가 작다면        
            NewTurret = UpgradeTurrets.GetComponent<Turret>();
            if (GetTurret.TurretTypeNum == NewTurret.TurretTypeNum && GetTurret.TurretRank == NewTurret.TurretRank)
            {               
                if (distanceToTurret < shortestDistance && GetTurret.transform.position != UpgradeTurrets.transform.position)// 거리가 가깝고 같은 랭크에 같은 터렛이라면
                {                  
                    shortestDistance = distanceToTurret;
                    nearestTurret = UpgradeTurrets;
                }
            }
        }

        if (GetTurret.TurretRank == 4)
        {
            isUpgradeText = 2;
            Debug.Log(isUpgradeText);
            isUpgrade = true;
            return;
        }
        else
        {
            isUpgradeText = 3;
            isUpgrade = false;
        }
        //같은 종류의 터렛이 없다면 업그레이드 불가
        if (nearestTurret == null)
        {
            isUpgradeText = 1;
           
            return;
        } //터렛의 등급이 마지막 등급이라면

      

        switch (GetTurret.TurretRank)
        {
            case 1:
                buildManager.tbi.RandomTurretBuildRare();
                break;
            case 2:
                buildManager.tbi.RandomTurretBuildEpic();
                break;
            case 3:
                buildManager.tbi.RandomTurretBuildLegend();
                break;
        }      

        Destroy(turret);
        Destroy(nearestTurret);

        UpgradeTurret(buildManager.GetTurretBuild());
    }   

    public void SellTurret()
    {
        PlayerStats.Money += turretBlueprint.GetSellAmount();

        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
        Debug.Log(PlayerStats.Money);

        Destroy(turret);
        turretBlueprint = null;

    }

    //마우스가 객체 위에 머무르면 색상변경
    void OnMouseEnter()
    {

        if (EventSystem.current.IsPointerOverGameObject())
            return;
        //터렛을 지을수 없는 상태라면
        if (!buildManager.CanBuild)
            return;
        if (!buildManager.HasMoney && turret == null)
        {            
            buildManager.Node_Money(this);
            return;
        }
        if (buildManager.HasMoney && turret == null)
        {
            buildManager.Node_On(this);
            return;
        }


    }

    //마우스가 객체 위에 없다면 원래색
    void OnMouseExit()
    {
        buildManager.Node_Effect_Hide();
    }
}
