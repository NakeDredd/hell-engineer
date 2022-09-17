using Pathfinding;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAIBehavior : MonoBehaviour, IDamagable
{
    /// <summary>
    /// Это класс, из которого берутся методы, с которыми работает Bolt State Machine.
    /// Все методы в этом классе публичные, чтобы с ними мог работать State Machine.
    /// </summary>

    #region BoltCustomEventsConstants
    public const string ON_INIT = "OnInit";
    public const string ON_TARGET_IN_ATTACK_RANGE = "OnTargetInAttackRange";
    public const string ON_PREPARING_ATTACK_END = "OnPreparingAttackEnd";
    public const string ON_ATTACK_END = "OnAttackEnd";
    public const string ON_TARGET_IS_STILL_IN_RANGE = "OnTargetIsStillInRange";
    public const string ON_TARGET_IS_NOT_STILL_IN_RANGE = "OnTargetIsNotStillInRange";
    public const string ON_DEATH = "OnDeath";
    #endregion

    [Header("Pathfinding")]
    [SerializeField] private float nextWaypointDistance;

    private Seeker seeker;
    private Path path;
    private Transform target;

    private IDisposable disposable;

    private int currentWaypoint;

    [Header("Health")]
    [SerializeField] private int health;

    private int currentHealth;

    [Header("Combat")]
    [SerializeField] private int damage;
    [SerializeField] private float attackRangeDistance;
    [SerializeField] private float attackPrepareTime;
    [SerializeField] private float attackCooldownTime;

    public float AttackPrepareTime { get => attackPrepareTime; set => attackPrepareTime = value; }
    public float AttackCooldownTime { get => attackCooldownTime; set => attackCooldownTime = value; }

    [Header("Movement")]
    [SerializeField] private float speed;

    private Rigidbody2D rb;

    private float currentSpeed;

    [Header("Other")]
    [SerializeField] private GameObject coinPrefab;


    [Button] private void UpdateParams()
    {
        InitVariables();
    }

    #region Init
    //Аналог метода Start()
    public void InitBehavior()
    {
        InitReferences();
        InitVariables();
    }

    private void InitReferences()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        target = GlobalAI.Instance.Player.transform;

    }

    private void InitVariables()
    {
        currentWaypoint = 0;
        currentHealth = health;
        currentSpeed = speed;
    }
    #endregion

    #region Pathfinding
    public void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(transform.position, target.position, OnGeneratePathComplete);
        }
    }

    public void TurnOnPathUpdating()
    {
        disposable = Observable.Interval(TimeSpan.FromSeconds(0.5f)).Subscribe(_ =>
        {
            UpdatePath();
        });
    }

    public void TurnOffPathUpdating()
    {
        disposable.Dispose();
    }

    public bool IsHavePath()
    {
        return path != null;
    }

    private void OnGeneratePathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    public void NextWaypoint()
    {
        currentWaypoint++;
    }

    public bool IsReachedWaypoint()
    {
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        return nextWaypointDistance > distance;
    }

    public bool IsReachedPath()
    {
        return currentWaypoint >= path.vectorPath.Count;
    }
    #endregion

    #region Movement
    public void MoveToPlayer()
    {
        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * currentSpeed * Time.fixedDeltaTime;

        rb.AddForce(force);
    }

    public void Flip()
    {
        if (rb.velocity.x >= 0.01f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (rb.velocity.x <= 0.01f)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    #endregion

    #region Combat

    public bool IsTagetInAttackRange()
    {
        float distanceToTarget = Vector2.Distance(rb.position, target.position);

        return distanceToTarget <= attackRangeDistance;
    }

    public void Attack()
    {
        GlobalAI.Instance.Player.GetComponent<PlayerHealth>().GetDamage(damage);
    }

    #endregion

    #region HP
    public void GetDamage(int damage)
    {
        if (damage >= currentHealth)
        {
            currentHealth = 0;
            CustomEvent.Trigger(gameObject, ON_DEATH);
        }
        else
        {
            currentHealth -= damage;
        }
    }

    public void Death()
    {
        EnemySpawner.Instance.DeleateEnemy(this);
        Destroy(gameObject);
    }

    public void DropCoin()
    {
        Instantiate(coinPrefab, transform.position, Quaternion.identity);
    }
    #endregion
}
