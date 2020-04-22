using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBuildIndex : MonoBehaviour
{
    public TurretBlueprint[] NomalTurret;
    public TurretBlueprint[] RareTurret;
    public TurretBlueprint[] EpicTurret;
    public TurretBlueprint[] LegendTurret;

    BuildManager buildManager;

    int TurretIndex;

    void Start()
    {
        buildManager = BuildManager.instance;
    }
    
    // Button >> Buildmanager

    public void RandomTurretBuildNomal()
    {
        TurretIndex = Random.Range(0, 3);
        TurretBlueprint turretBlueprint = NomalTurret[TurretIndex];
        buildManager.SelectTurretToBuild(turretBlueprint);
    }

    public void RandomTurretBuildRare()
    {
        TurretIndex = Random.Range(0, 3);
        TurretBlueprint turretBlueprint = RareTurret[TurretIndex];
        buildManager.SelectTurretToBuild(turretBlueprint);
    }

    public void RandomTurretBuildEpic()
    {
        TurretIndex = Random.Range(0, 3);
        TurretBlueprint turretBlueprint = EpicTurret[TurretIndex];
        buildManager.SelectTurretToBuild(turretBlueprint);
    }

    public void RandomTurretBuildLegend()
    {
        TurretIndex = Random.Range(0, 3);
        TurretBlueprint turretBlueprint = LegendTurret[TurretIndex];
        buildManager.SelectTurretToBuild(turretBlueprint);
    }
       
   
    
}


