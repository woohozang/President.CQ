using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prac_Order : MonoBehaviour
{
    [SerializeField] Renderer[] backRenderers;
    [SerializeField] Renderer[] middleRenderers;
    [SerializeField] string sortingLayerName;

    private void Start(){
        SetOrder(0);
    }

    public void SetOrder(int order){
        int mulOrder = order * 10;

        foreach(var renderer in backRenderers){
            renderer.sortingLayerName = sortingLayerName;
            renderer.sortingOrder = mulOrder;
        }
        foreach(var renderer in middleRenderers){
            renderer.sortingLayerName = sortingLayerName;
            renderer.sortingOrder = mulOrder+1;
        }
    }
}
