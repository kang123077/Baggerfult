using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBlockInfo", menuName = "BlockInfo")]
public class BlockInfo : ScriptableObject
{
    public BlockType type;
    public int hp;

    public virtual void BlockEffect()
    {
        // TODO : VirtualEffect
    }

    public virtual void BlockDestroy()
    {
        // TODO : VirtualDestroy
    }
}
