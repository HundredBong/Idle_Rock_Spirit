using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyUtility
{
    public static Vector3 SearchTargetPosition(Transform playerTransform, out Enemy targetEnemy)
    {
        targetEnemy = null;
        float targetDistance = float.MaxValue;
        Vector3 targetPosition = Vector3.zero;

        // 게임매니저의 enemy 리스트에서 적을 탐색
        foreach (Enemy enemy in GameManager.Instance.enemies)
        {
            // enemy에 접근했을 때 null이면 예외가 발생하므로 null일시 루프를 건너뜀
            if (enemy == null) { continue; }

            // enemy와 플레이어의 거리 측정
            float distance = Mathf.Abs(enemy.transform.position.x - playerTransform.position.x);

            // 더 가까운 거리를 가진 enemy 찾기
            if (distance < targetDistance)
            {
                targetEnemy = enemy;
                targetDistance = distance;
                targetPosition = enemy.transform.position;
                Debug.Log($"가장 가까운 적의 좌표 : {targetPosition}");
            }
        }

        return targetPosition;
    }
}