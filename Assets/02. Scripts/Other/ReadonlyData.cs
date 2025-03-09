using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ReadonlyData
{
    public static readonly string playerLayer = "Player";
    public static readonly string interactionLayer = "Interaction";

    public static readonly LayerMask playerLayerMask = 1 << LayerMask.NameToLayer(playerLayer);
    public static readonly LayerMask interactionLayerMask = 1 << LayerMask.NameToLayer(interactionLayer);
}
