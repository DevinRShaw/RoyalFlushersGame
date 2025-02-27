using System.Collections;
using UnityEngine;

public class EnemyTrackingAbility : Ability
{
    public string enemyTag = "Enemy";
    public Transform closestEnemy = null;
    

    // Set cooldown to control how often tracking can be updated
    protected override float Cooldown => 0f;  // You can adjust this to your preferred cooldown time

    // Coroutine reference for stopping the tracking when the ability is deactivated
    private Coroutine trackingCoroutine;



    // Override Activate() to start the tracking coroutine
    protected override void Activate()
    {
        // Start tracking the closest enemy immediately when the ability is activated
        if (trackingCoroutine == null)
        {
            trackingCoroutine = StartCoroutine(TrackClosestEnemyCoroutine());
        }
    }

    // Override Deactivate() to stop tracking
    public void Deactivate()
    {
        if (trackingCoroutine != null)
        {
            StopCoroutine(trackingCoroutine);
            trackingCoroutine = null;
        }

    }

    // Coroutine for finding the closest enemy
    private IEnumerator TrackClosestEnemyCoroutine()
    {
        while (true)
        {
            // Find the closest enemy
            FindClosestEnemyToMouse();

            // Wait until the cooldown is finished before checking again
            yield return new WaitForSeconds(Cooldown);  // Cooldown is used here to space out checks
        }
    }

    private void FindClosestEnemyToMouse()
    {
        // Convert mouse position to world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Set the z to 0 to avoid affecting the comparison

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        Transform newClosestEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(mousePosition, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                newClosestEnemy = enemy.transform;
            }
        }

        if (newClosestEnemy != closestEnemy)
        {

            closestEnemy = newClosestEnemy;
        }
    }

}
