using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{

    public static BuildManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this;
    }
    // 설치 이펙트
    public GameObject buildEffect;
    // 판매 이펙트
    public GameObject sellEffect;
    //

    private TurretBlueprint turretToBuild;
    private Node selectedNode;

    public NodeUI nodeUI;
    public TurretBuildIndex tbi;

    public GameObject hoverEffect;
    public GameObject MoneyEffect;    


    //설치할 수 있는 터렛을 선택하지 않았다면 true 반환
    public bool CanBuild { get { return turretToBuild != null; } }
    //플레이어가 가지고 있는 돈이 터렛 설치 비용보다 많다면
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    
    // 노드를 선택했을때 행동
    public void SelectNode(Node node)
    {
        //똑같은 노드를 클릭했다면
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }
       //새로운 노드를 클릭 했다면
        selectedNode = node;
       
        turretToBuild = null;
        //터렛 업그레이드 창 띄우기
        nodeUI.SetTarget(node);
        Node_Effect_Hide();
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    //터렛 선택하기  Shop >> BuildManager
    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        selectedNode = null;

        DeselectNode();
    }

    // turretToBuild = turret >> Node.OnMouseDown
    public TurretBlueprint GetTurretBuild()
    {
        return turretToBuild;
    }

    public void Node_On(Node node)
    {
        hoverEffect.transform.position = node.transform.position;
        hoverEffect.SetActive(true);
    }
    public void Node_Money(Node node)
    {
        MoneyEffect.transform.position = node.transform.position;
        MoneyEffect.SetActive(true);
    }

    public void Node_Effect_Hide()
    {

        hoverEffect.SetActive(false);
        MoneyEffect.SetActive(false);
    }


}
