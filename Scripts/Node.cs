using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

    public Vector3 positionOffset;

    private Color hoverColor = new Color();
    private Color notEnoughMoneyColor = new Color();
    private Color startColor;
    private Renderer rend;

    [HideInInspector]
    public GameObject turrent;
    [HideInInspector]
    public TurrentBlueprint turrentBluePrint;
    [HideInInspector]
    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        ColorUtility.TryParseHtmlString("#0F4B29FF", out hoverColor);
        ColorUtility.TryParseHtmlString("#9E9E9EFF", out notEnoughMoneyColor);
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
            
        if (buildManager.CanBuild)
            rend.material.color = hoverColor;
        else
            rend.material.color = notEnoughMoneyColor;
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
       
        BuildTurrent(buildManager.getTurrentToBuild());
    }

    void BuildTurrent(TurrentBlueprint bluePrint)
    {
        if (PlayerStats.Money < bluePrint.cost)
            return;
        
        PlayerStats.Money -= bluePrint.cost;
        GameObject _turrent = (GameObject)Instantiate(bluePrint.prefab, GetBuildPosition(), Quaternion.identity);
        turrent = _turrent;
        turrent.transform.rotation = Quaternion.Euler(0, 180, 0);
        turrentBluePrint = bluePrint;
        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }
}
