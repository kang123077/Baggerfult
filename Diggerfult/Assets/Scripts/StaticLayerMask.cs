using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticLayerMask
{
    public static LayerMask BlockField = LayerMask.NameToLayer("BlockField");
    public static LayerMask ExitField = LayerMask.NameToLayer("ExitField");
    public static LayerMask ActionBlock = LayerMask.NameToLayer("ActionBlock");
    public static LayerMask ObstacleBlock = LayerMask.NameToLayer("ObstacleBlock");
    public static LayerMask Bullet = LayerMask.NameToLayer("Bullet");
    public static LayerMask[] LayerHittable = new LayerMask[]
    {
        BlockField, ActionBlock, ObstacleBlock
    };
}
