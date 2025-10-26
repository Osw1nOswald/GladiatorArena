using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    [Header("��������� �����")]
    public float damage = 25f;
    private bool canDamage = false; // ������� �� ����

    private void OnTriggerEnter(Collider other)
    {
        if (!canDamage) return;

        // ���������, ���� �� ���
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Debug.Log($"��� ����� �� {other.name}, �������� {damage} �����!");
            Debug.Log($"Trigger �������� �� {other.name}");
        }
    }

    // ��� ������ ���������� ���������
    public void EnableDamage()
    {
        canDamage = true;
    }

    public void DisableDamage()
    {
        canDamage = false;
    }
}
