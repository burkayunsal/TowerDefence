using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : Singleton<PathManager>
{
    [SerializeField] private List<Node> lsNodes = new List<Node>();

    public Node GetStartNode() => lsNodes[0];
    private void Start()
    {
        if (lsNodes.Count == 0) return;
        lsNodes[^1].isLastNode = true;
    }

}
