using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class TileLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color pathColor = new Color(1f, 0.5f, 0f);

    [SerializeField] bool prefabMode = false;

    GridManager gridManager;
    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    Dictionary<Vector2Int, Node> grid;

    private void Awake()
    {
        label = GetComponentInChildren<TextMeshPro>();
        if (label)
        {
            label.enabled = false;
        }
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager)
        {
            grid = gridManager.Grid;
        }
        DisplayCoordinates();
    }

    private void Update()
    {
        if (!Application.isPlaying && !prefabMode)
        {
            DisplayCoordinates();
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            if (label)
            {
                label.enabled = true;
            }
        }
        LabelColor();
        ToggleLabel();
    }

    private void LabelColor()
    {
        if (gridManager)
        {
            if (label)
            {
                Node node = gridManager.GetNode(coordinates);
                if (node != null)
                {
                    if (!node.isWalkable)
                    {
                        label.color = blockedColor;
                    }
                    else if (node.isPath)
                    {
                        label.color = pathColor;
                    }
                    else if (node.isExplored)
                    {
                        label.color = exploredColor;
                    }
                    else
                    {
                        label.color = defaultColor;
                    }
                }
            }
        }
    }

    private void ToggleLabel()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (label)
            {
                label.enabled = !label.IsActive();
            }
        }
    }

    private void DisplayCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.position.z / UnityEditor.EditorSnapSettings.move.z);
        name = $"Tile ({coordinates.x}, {coordinates.y})";
        if (label)
        {
            label.text = $"{coordinates.x}, {coordinates.y}";
        }
    }
}
