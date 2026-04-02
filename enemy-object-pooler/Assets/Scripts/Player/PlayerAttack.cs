using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("Input")]
    private PlayerInput playerInput;
    private InputAction attackAction;
    
    [Header("Attack Settings")]
    [SerializeField] private Vector3 boxSize = new Vector3(1f, 1f, 1f);
    [SerializeField] private float damage = 25f;
    [SerializeField] public LayerMask enemyLayer;

    public Transform attackPoint;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        attackAction = playerInput.actions.FindAction("Attack");
    }

    // Update is called once per frame
    void Update()
    {
        if (attackAction.triggered)
        {
            Attack();
        }
    }

    private void Attack()
    {
        Debug.Log("Attack triggered!");
        // Detect enemies in range of attack
        Collider[] hitEnemies = Physics.OverlapBox(
            attackPoint.position,
            boxSize / 2,
            Quaternion.identity,
            enemyLayer
        );

        // Damage them
        foreach (Collider enemy in hitEnemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPoint.position, boxSize);
    }
}
