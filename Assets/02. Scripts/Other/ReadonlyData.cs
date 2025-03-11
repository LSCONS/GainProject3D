using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ReadonlyData
{
    public static readonly string playerLayer = "Player";
    public static readonly string interactionLayer = "Interaction";
    public static readonly string groundLayer = "Ground";
    public static readonly string defaultLayer = "Default";
    public static readonly string jumpPlatformLayer = "JumpPlatform";

    public static readonly LayerMask playerLayerMask = 1 << LayerMask.NameToLayer(playerLayer);
    public static readonly LayerMask interactionLayerMask = 1 << LayerMask.NameToLayer(interactionLayer);
    public static readonly LayerMask groundLayerMask = 1 << LayerMask.NameToLayer(groundLayer);
    public static readonly LayerMask defaultLayerMask = 1 << LayerMask.NameToLayer(defaultLayer);
    public static readonly LayerMask jumpPlatformLayerMask = 1 << LayerMask.NameToLayer(jumpPlatformLayer);
}
