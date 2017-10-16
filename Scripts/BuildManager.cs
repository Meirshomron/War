using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    private TurrentBlueprint turrentToBuild;
    private Node selectedNode;
    public GameObject buildEffect;

    //public NodeUI nodeUI;
    public bool CanBuild { get { return turrentToBuild!=null && PlayerStats.Money >= turrentToBuild.cost; } }

    void Awake()
    {
        instance = this;
    }

    public void SelectTurrentToBuild(TurrentBlueprint turrent)
    {
        turrentToBuild = turrent;
        DeselectNode();
    }

    public void SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }
        selectedNode = node;
        turrentToBuild = null;
        //nodeUI.setTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        //nodeUI.Hide();
    }

    public TurrentBlueprint getTurrentToBuild()
    {
        return turrentToBuild;
    }
}
