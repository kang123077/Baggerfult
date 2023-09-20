using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CannonFieldController : MonoBehaviour
{
    // TODO : Bullet들을 저장하는 방법 및 계층 구현 리베이스 테스트
    public GameObject protoBulletPrefab;
    public LineRenderer BulletTrail;
    // TODO : isDragging Stack으로 옮길 수 있는지(애초에 그게 좋은 방법인지) 나중에 검토
    private bool isDragging;
    private Vector2 curMousePos;

    void Update()
    {
        // TODO : 지금은 항상 감지하지만 나중에는 시퀀스에 따라서 코루틴 실행 > 코루틴에서 마우스 감지를 통해 Update 사용 없앨수있음
        if (Input.GetMouseButtonDown(0))
        {
            // 로직 상 필요로 예외 체크를 위로 옮김.
            // 어차피 이런 식으로 Ray를 쓸거라면, 이후 GameManager와 같은 곳에서 게임플레이(클릭, 터치 등) 을
            // 통째로 관리하는 것도 고려할 수 있을듯.
            Vector2 startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(startPoint, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                StartCoroutine(Shooting(startPoint));
            }
        }
    }

    // TODO : 시퀀스 관리도 포함되어야 함

    IEnumerator Shooting(Vector2 startPoint)
    {
        isDragging = true;
        BulletTrail.enabled = true;
        BulletTrail.SetPosition(0, startPoint);

        while (isDragging)
        {
            // TODO : 경로 보이기 관련 함수 기본(현재)
            // 이후 Ray등의 방식을 통해서 어느 정도 경로를 예상 할 수 있도록 SetPosition을 추가해야 함
            curMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            BulletTrail.SetPosition(1, curMousePos);

            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                BulletTrail.enabled = false;
                Vector2 endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                // hit 전에 마우스 위치를 확인, 너무 가까우면 정지
                // TODO : 이 부분에 문제가 있을 경우
                // 발사 버튼을 추가해야 할 수 있음 ex ) 실수 방지
                if (Vector2.Distance(startPoint, endPoint) < 0.1f) yield break;

                RaycastHit2D hit = Physics2D.Raycast(endPoint, Vector2.zero);
                // 무언가 hit했고, Block필드면
                if (hit.collider != null &&
                    (hit.collider.gameObject.layer == StaticLayerMask.ObstacleBlock ||
                    hit.collider.gameObject.layer == StaticLayerMask.ActionBlock ||
                    hit.collider.gameObject.layer == StaticLayerMask.BlockField))
                {
                    // TODO : 오브젝트 풀 요청시 Bullet 종류에 따랄 제네릭 활용 여부 고려
                    ObjectPool.inst.GetObject<ProtoBullet>(protoBulletPrefab, transform)
                        .GetComponent<ProtoBullet>().Shoot(startPoint, endPoint);
                }
                else
                {
                    // TODO : 드래그 종료에 따른 발사 취소 관련 로직이 필요해질 수 있음
                }
            }
            yield return null;
        }
    }
}
