using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] private Node currentSearchNode;
    Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };

    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid;

    private void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager)
        {
            grid = gridManager.Grid;
        }
        SearchForNeighbors();
    }

    private void SearchForNeighbors()
    {
        List<Vector2Int> neighbords = new List<Vector2Int>();
        
        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbordCoordinates = currentSearchNode.coordinates + direction;
            if (grid.ContainsKey(neighbordCoordinates))
            {
                neighbords.Add(neighbordCoordinates);

                // Remove this
                grid[neighbordCoordinates].isExplored = true;
                grid[currentSearchNode.coordinates].isPath = true;
            }
        }
    }
}
