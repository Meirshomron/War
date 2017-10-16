
using UnityEngine;

public class Shop : MonoBehaviour {

    public TurrentBlueprint standardTurrent;
    public TurrentBlueprint missileTurrent;
    public TurrentBlueprint laserBeamer;

    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectStandardTurrent()
    {
        buildManager.SelectTurrentToBuild(standardTurrent);
    }

    public void SelectMissileLauncherTurrent()
    {
        buildManager.SelectTurrentToBuild(missileTurrent);
    }

    public void SelectLaserBeamer()
    {
        buildManager.SelectTurrentToBuild(laserBeamer);
    }

}
