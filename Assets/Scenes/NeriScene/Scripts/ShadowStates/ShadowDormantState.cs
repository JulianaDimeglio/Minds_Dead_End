using Game.Enemies.States;
using UnityEngine;
using UnityEngine.AI;

public class ShadowDormantState : IEnemyState
{
    private float _timer = 0f; // this counts how much time Shadow stays hidden

    public void EnterState(BaseEnemy enemy)
    {
        if (enemy is not ShadowEnemy shadow) return;

        // turn off NavMeshAgent so he doesn’t move
        shadow.Agent.enabled = false;

        // move him far away so we don’t see him
        shadow.transform.position = new Vector3(5000f, 5000f, 5000f);

        Debug.Log("[ShadowDormantState] Moved far away and disabled NavMeshAgent.");
    }

    public void UpdateState(BaseEnemy enemy)
    {
        _timer += Time.deltaTime; // count time

        if (enemy is not ShadowEnemy shadow) return;

        // when time is up, change to appear state
        if (_timer >= shadow.waitTimeBeforeAppear)
        {
            Debug.Log("[ShadowDormantState] Switching to AppearState.");
            enemy.SwitchState(new ShadowAppearState());
        }
    }

    public void ExitState(BaseEnemy enemy)
    {
        if (enemy is not ShadowEnemy shadow) return;

        // get position close to player
        Vector3 playerPos = shadow.Target.position;
        Vector3 spawnPosition = GetValidNavMeshPositionNear(playerPos, 5f, 20f);

        // turn on NavMeshAgent again
        shadow.Agent.enabled = true;

        if (spawnPosition != Vector3.zero)
        {
            // move using Warp so he connects with NavMesh
            shadow.Agent.Warp(spawnPosition);
        }
        else
        {
            // if no good place found, go to player position
            Debug.LogWarning("[ShadowDormantState] No valid NavMesh position found near player. Using fallback.");
            shadow.Agent.Warp(playerPos);
        }

        Debug.Log("[ShadowDormantState] Exiting DORMANT STATE.");
    }

    private Vector3 GetValidNavMeshPositionNear(Vector3 center, float minRadius, float maxRadius)
    {
        for (int i = 0; i < 10; i++)
        {
            // random position near player
            Vector3 randomDirection = Random.insideUnitSphere * maxRadius;
            randomDirection.y = 0;
            Vector3 candidate = center + randomDirection;

            // skip if too close
            if (Vector3.Distance(candidate, center) < minRadius) continue;

            // check if it’s on the NavMesh
            if (NavMesh.SamplePosition(candidate, out NavMeshHit hit, 2f, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }

        // return zero if nothing found
        return Vector3.zero;
    }

    public void OnSeenByPlayer(BaseEnemy enemy)
    {
        // do nothing while Shadow is hidden
    }
}