using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class TileLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;

    [SerializeField] bool prefabMode = false;

    TextMeshPro label;
    WayPoint wayPoint;
    Vector2Int coordinates = new Vector2Int();

    private void Awake()
    {
        label = GetComponentInChildren<TextMeshPro>();
        wayPoint = GetComponent<WayPoint>();
        label.enabled = false;
        DisplayCoordinates();
    }

    private void Update()
    {
        if (!Application.isPlaying && !prefabMode)
        {
            DisplayCoordinates();
            SetAsChild();
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            label.enabled = true;
        }
        LabelColor();
        ToggleLabel();
    }

    private void LabelColor()
    {
        if (wayPoint)
        {
            if (wayPoint.IsPlaceable)
            {
                label.color = defaultColor;
            }
            else
            {
                label.color = blockedColor;
            }
        }
    }

    private void ToggleLabel()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
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

    private void SetAsChild()
    {
        if (!wayPoint.IsPath)
        {
            Transform parent = GameObject.Find("Tiles").transform;
            if (parent)
            {
                transform.parent = parent;
            }
        }
        else
        {
            Transform parent = GameObject.Find("Path").transform;
            if (parent)
            {
                transform.parent = parent;
            }
        }
    }
}
