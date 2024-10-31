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

        // ���ӸŴ����� enemy ����Ʈ���� ���� Ž��
        foreach (Enemy enemy in GameManager.Instance.enemies)
        {
            // enemy�� �������� �� null�̸� ���ܰ� �߻��ϹǷ� null�Ͻ� ������ �ǳʶ�
            if (enemy == null) { continue; }

            // enemy�� �÷��̾��� �Ÿ� ����
            float distance = Mathf.Abs(enemy.transform.position.x - playerTransform.position.x);

            // �� ����� �Ÿ��� ���� enemy ã��
            if (distance < targetDistance)
            {
                targetEnemy = enemy;
                targetDistance = distance;
                targetPosition = enemy.transform.position;
                Debug.Log($"���� ����� ���� ��ǥ : {targetPosition}");
            }
        }

        return targetPosition;
    }
}