using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ConnectorController))]
public class DrawLineEditor : Editor
{
    GameObject selectionPrev;
    GameObject selectionNext;

    void OnSceneGUI()
    {
        var e = Event.current;

        ConnectorController connect = target as ConnectorController;

        // override default control
        Tools.current = Tool.None;
        HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));

        if (e.type == EventType.MouseDown)
        {
            GameObject selection = GetSelectionAt(e.mousePosition);
            if (selection != null)
            {
                selectionPrev = selection;
                selectionNext = null;
            }
        }

        if (e.type == EventType.MouseDrag || e.type == EventType.MouseUp)
        {
            GameObject selection = GetSelectionAt(e.mousePosition);
            if (selection != null)
            {
                if (selectionPrev != selection)
                    selectionNext = selection;
            }
        }

        if (e.type == EventType.MouseUp)
        {
            Undo.RecordObject(connect, "Point Connection");
            if (selectionPrev != null && selectionNext != null)
            {
                Undo.SetTransformParent(connect.transform, selectionPrev.transform, "Point Connection");
                Undo.RecordObject(connect.transform, "Point Connection");
                connect.transform.localPosition = Vector3.zero;
                Undo.RecordObject(connect, "Point Connection");
                connect.connectionA = selectionPrev;
                connect.connectionB = selectionNext;
            }
            else
            {
                selectionPrev = connect.connectionA;
                selectionNext = connect.connectionB;
            }
        }

        Tools.current = Tool.None;

        if (selectionPrev != null && selectionNext != null)
            Handles.DrawLine(selectionPrev.transform.position, selectionNext.transform.position);

        Selection.activeGameObject = connect.gameObject;
    }

    private GameObject GetSelectionAt(Vector2 mousePosition)
    {
        var hit = HandleUtility.PickGameObject(mousePosition, true);
        var other = hit?.GetComponentInParent<PointController>();
        if (other != null)
        {
            return other.gameObject;
        }

        return null;
    }

}