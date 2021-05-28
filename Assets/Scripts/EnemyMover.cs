using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<WayPoint> path = new List<WayPoint>();
    [SerializeField] [Range(0f, 10f)] float speed = 1f;

    Lives lives;

    public float Speed { get { return speed; } set { speed = value; } }

    private void OnEnable()
    {
        lives = FindObjectOfType<Lives>();
        StartCoroutine(FollowPath());
    }

    private IEnumerator FollowPath()
    {
        path.Clear();
        Transform pathParent = GameObject.FindGameObjectWithTag("Path").transform;
        
        foreach(Transform pathChild in pathParent)
        {
            path.Add(pathChild.GetComponent<WayPoint>());
        }

        transform.position = path[0].transform.position;

        foreach (WayPoint wayPoint in path)
        {
            if (transform.position != wayPoint.transform.position)
            {
                Vector3 startPos = transform.position;
                Vector3 endPos = wayPoint.transform.position;
                float travelPercent = 0;

                transform.LookAt(endPos);

                while (travelPercent < 1f)
                {
                    travelPercent += Time.deltaTime * speed;
                    transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
                    yield return new WaitForEndOfFrame();
                }
            }
        }
        lives.TotalLives--;
        CheckLives();
        gameObject.SetActive(false);
    }

    private void CheckLives()
    {
        if (lives.TotalLives <= 0)
        {
            lives.PrepareToLose();
        }
    }
}
