using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockFieldController : MonoBehaviour
{
    // TODO : Bullet들을 저장하는 방법 및 계층 구현
    public GameObject protoBulletPrefab;
    private Vector2 startPoint;
    private Vector2 endPoint;
    private bool isDragging;

    void Update()
    {
        ShootingCheck();
    }

    public void ShootingCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == this.gameObject)
            {
                startPoint = mousePos;
                isDragging = true;
            }
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = false;
            // TODO : 오브젝트 풀 요청시 Bullet 종류에 따랄 제네릭 활용 여부 고려
            ObjectPool.inst.GetObject<ProtoBullet>(protoBulletPrefab, transform)
                .GetComponent<ProtoBullet>().Shoot(startPoint, endPoint);
        }
    }
}
