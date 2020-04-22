using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{

    public GameObject ui;

    public Text upgradeCost;
    public Button upgradeButton;
    public GameObject upgradeTextUi;
    public Text upgradeText;

    public Text sellAmount;

    private Node target;

   
    public void SetTarget(Node _target)
    {
        target = _target;

        transform.position = target.GetBuildPosition();

        if (!target.isUpgrade)
        {
            
            upgradeButton.interactable = true;
        }
        if(target.isUpgradeText == 2)
        {
            upgradeCost.text = "DONE";
            upgradeButton.interactable = false;
        }

        sellAmount.text = "$" + target.turretBlueprint.GetSellAmount();

        ui.SetActive(true);
    }
   
    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        
        target.UpgradeTurretCheck();

        if (target.isUpgradeText == 1)
        {
            upgradeText.gameObject.SetActive(false);
            upgradeText.gameObject.SetActive(true);
            upgradeText.text = "같은 터렛이 없습니다.";

        }
        else if (target.isUpgradeText == 2)
        {
            upgradeText.gameObject.SetActive(false);
            upgradeText.gameObject.SetActive(true);
            upgradeText.text = "터렛이 최고등급 입니다.";
        }
        else
        {
            upgradeText.gameObject.SetActive(false);
        }

        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }

}
