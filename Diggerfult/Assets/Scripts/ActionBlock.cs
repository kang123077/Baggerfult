using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBlock : MonoBehaviour
{
    // TODO : 현재 드래그 앤 드랍 형태이나 표 형태를 통해 관리하고 싶음..
    // Enum으로 설정한 후 게임이 시작 될 때 해당 정보를 찾아오는 방법도 있으나 좀 비효율적일 것 같아 고민 됨
    public BlockInfo blockInfo;
    private int _hp;
    public int hp
    {
        get { return _hp; }
        set
        {
            _hp = value;
            BlockHpCheck();
        }
    }

    public void BlockAction(int dmg)
    {
        blockInfo.BlockEffect();
    }

    private void BlockHpCheck()
    {
        if (hp <= 0)
        {
            blockInfo.BlockDestroy();
        }
    }
}
