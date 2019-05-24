using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ConnectorBase), true)]
public class ConnectorEditor : Editor
{
    GameObject selectionPrev;
    GameObject selectionNext;

    void OnSceneGUI()
    {
        var e = Event.current;

        ConnectorBase connect = target as ConnectorBase;

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
                var p1 = selectionPrev.GetComponent<IConnectorPoint>();
                var p2 = selectionNext.GetComponent<IConnectorPoint>();
                Undo.RecordObject((MonoBehaviour)p1, "Point Connection");
                Undo.RecordObject((MonoBehaviour)p2, "Point Connection");
                if (p1 is PointController)
                    ((PointController)p1).important = true;
                if (p2 is PointController)
                    ((PointController)p2).important = true;
                EditorUtility.SetDirty((MonoBehaviour)p1);
                EditorUtility.SetDirty((MonoBehaviour)p2);
                Undo.RecordObject(connect, "Point Connection");
                connect.connectionA = selectionPrev;
                connect.connectionB = selectionNext;
                EditorUtility.SetDirty(connect);
            }
            else
            {
                selectionPrev = connect.connectionA;
                selectionNext = connect.connectionB;
            }
        }

        if (selectionPrev != null && selectionNext != null)
            Handles.DrawLine(selectionPrev.transform.position, selectionNext.transform.position);

        Tools.current = Tool.None;

        Selection.activeGameObject = connect.gameObject;
    }

    private GameObject GetSelectionAt(Vector2 mousePosition)
    {
        var hit = HandleUtility.PickGameObject(mousePosition, true);
        var other = hit?.GetComponentInParent<IConnectorPoint>();
        if (other != null)
        {
            return other.gameObject;
        }

        return null;
    }

}