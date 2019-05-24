using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class PartsPaletteWindow : EditorWindow
{
    [MenuItem("ステージ作成/パーツパレット", priority = 10001)]
    static void CreateWindow()
    {
        var win = GetWindow<PartsPaletteWindow>("Prefab Palette");
        win.titleContent = new GUIContent("パーツパレット");
        win.Show();
    }

    PrefabPalette palette;
    PrefabPalette prevPalette;
    GameObject selected;
    Vector2 prefabScroll;

    [SerializeField]
    [EnumElementUsage(typeof(PrefabPalette.StageType), "ステージの種類")]
    PrefabPalette.StageType stageType;

    List<PrefabPalette> palettes = new List<PrefabPalette>();
    string[] paletteNames;

    Vector2 mousePos;
    GameObject placingObj;
    Vector3 placePos;
    Vector3 placeNor;

    bool optionsToggle = true;

    bool dragMode = false;
    bool scaleMode = false;
    Vector2 lastMousePos;
    float currentScale;
    Vector3 lastScale;

    Transform parentTo;

    void OnEnable()
    {
        SceneView.onSceneGUIDelegate -= OnSceneGUI;
        SceneView.onSceneGUIDelegate += OnSceneGUI;

        Undo.undoRedoPerformed -= Repaint;
        Undo.undoRedoPerformed += Repaint;

        wantsMouseMove = true;
        wantsMouseEnterLeaveWindow = true;
        LoadPalettes();
    }

    void OnDisable()
    {
        SceneView.onSceneGUIDelegate -= OnSceneGUI;
        Undo.undoRedoPerformed -= Repaint;
    }

    void OnSelectionChange()
    {
        LoadPalettes();
    }

    void OnFocus()
    {
        LoadPalettes();
    }

    void LoadPalettes()
    {
        var selection = Selection.activeGameObject;
        if (selection != null)
        {
            var sheet = selection.GetComponentInParent<PartsSheet>();
            if (sheet != null)
            {
                EditorApplication.delayCall += () =>
                {
                    palette = sheet.palette;
                    parentTo = sheet.transform;
                    if (selected != null)
                        selected = sheet?.palette?.prefabs[0];
                    Repaint();
                };
            }
            else
            {
                Deselect();
            }
        }

        palettes.Clear();
        foreach (var guid in AssetDatabase.FindAssets("t:" + typeof(PrefabPalette).Name))
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var pal = AssetDatabase.LoadAssetAtPath<PrefabPalette>(path);
            if (pal != null)
                palettes.Add(pal);
        }

        paletteNames = new string[palettes.Count];
        for (int i = 0; i < palettes.Count; ++i)
            if (palettes[i].title != null && palettes[i].title != "")
                paletteNames[i] = palettes[i].title;
            else
                paletteNames[i] = palettes[i].name;

        if (palette != null && !palettes.Contains(palette))
            palette = null;

        if (palette == null && palettes.Count > 0)
            palette = palettes[0];
    }

    void Deselect()
    {
        EditorApplication.delayCall += () =>
        {
            selected = null;
            Repaint();
        };
    }

    void OnGUI()
    {
        EditorGUILayout.Space();

        int paletteIndex = palettes.IndexOf(palette);
        paletteIndex = EditorGUILayout.Popup("Palette", paletteIndex, paletteNames);
        palette = paletteIndex < 0 ? null : palettes[paletteIndex];

        if (palette == null)
            return;

        if (palette != prevPalette)
        {
            foreach (GameObject obj in FindObjectsOfType(typeof(GameObject)))
            {
                if (obj.activeInHierarchy)
                {
                    var sheet = obj.GetComponent<PartsSheet>();
                    if (sheet != null)
                        if (sheet.palette == palette)
                        {
                            parentTo = sheet.transform;
                            Selection.activeGameObject = parentTo == null ? null : parentTo.gameObject;
                        }
                }
            }
            stageType = palette.stageType;
        }
        prevPalette = palette;

        if (ev.isMouse)
        {
            mousePos = ev.mousePosition;
            Repaint();
        }

        optionsToggle = EditorGUILayout.Foldout(optionsToggle, "Options");
        if (optionsToggle)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            EditorGUILayout.BeginVertical();

            var par = EditorGUILayout.ObjectField("Parent To", parentTo, typeof(Transform), true) as Transform;
            if (par != parentTo)
                if (par == null || (PrefabUtility.GetCorrespondingObjectFromSource(par) == null && PrefabUtility.GetPrefabInstanceHandle(par) == null))
                    parentTo = par;

            var serializedObject = new SerializedObject(this);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("stageType"));
            serializedObject.ApplyModifiedProperties();

            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.Space();

        var header = EditorGUILayout.GetControlRect();
        GUI.Label(header, "Prefabs", EditorStyles.boldLabel);
        header.y += header.height - 1f;
        header.height = 1f;
        EditorGUI.DrawRect(header, EditorStyles.label.normal.textColor);

        GUILayout.Space(2f);

        GUI.enabled = selected != null;
        if (GUILayout.Button("設置を終了", EditorStyles.miniButton))
            Deselect();
        GUI.enabled = true;
        if (ev.type == EventType.KeyDown && ev.keyCode == KeyCode.Escape)
            Deselect();

        var buttonHeight = EditorGUIUtility.singleLineHeight * 2f;
        var heightStyle = GUILayout.Height(buttonHeight);

        var lastRect = GUILayoutUtility.GetLastRect();
        var scrollMouse = mousePos;
        scrollMouse.x -= lastRect.xMin - prefabScroll.x;
        scrollMouse.y -= lastRect.yMax - prefabScroll.y;

        prefabScroll = EditorGUILayout.BeginScrollView(prefabScroll);

        if (palette.prefabs != null)
        {
            foreach (var prefab in palette.prefabs)
            {
                if (prefab == null)
                    continue;

                var rect = EditorGUILayout.GetControlRect(heightStyle);

                var bgRect = rect;
                bgRect.x -= 1f;
                bgRect.y -= 1f;
                bgRect.width += 2f;
                bgRect.height += 2f;
                if (prefab == selected)
                {
                    EditorGUI.DrawRect(bgRect, new Color32(0x42, 0x80, 0xe4, 0xff));
                }
                {
                    EditorGUIUtility.AddCursorRect(bgRect, MouseCursor.Link);

                    if (bgRect.Contains(scrollMouse))
                    {
                        EditorGUI.DrawRect(bgRect, new Color32(0x42, 0x80, 0xe4, 0x40));
                        if (ev.type == EventType.MouseDown)
                        {
                            EditorApplication.delayCall += () =>
                            {
                                if (selected != prefab)
                                {
                                    Tools.current = Tool.None;
                                    selected = prefab;
                                    Selection.activeGameObject = parentTo == null ? null : parentTo.gameObject;
                                }
                                else
                                {
                                    selected = null;
                                }
                                SceneView.RepaintAll();
                            };
                        }
                    }
                }

                var iconRect = new Rect(rect.x, rect.y, rect.height, rect.height);

                var icon = AssetPreview.GetAssetPreview(prefab);
                if (icon != null)
                    GUI.DrawTexture(iconRect, icon, ScaleMode.ScaleToFit, true, 1f, Color.white, Vector4.zero, Vector4.one * 4f);
                else
                    EditorGUI.DrawRect(iconRect, EditorStyles.label.normal.textColor * 0.25f);

                var labelRect = rect;
                labelRect.x += iconRect.width + 4f;
                labelRect.width -= iconRect.width + 4f;
                labelRect.height = EditorGUIUtility.singleLineHeight;
                labelRect.y += (buttonHeight - labelRect.height) * 0.5f;
                var labelStyle = prefab == selected ? EditorStyles.whiteBoldLabel : EditorStyles.label;
                GUI.Label(labelRect, prefab.name, labelStyle);
            }
        }

        EditorGUILayout.EndScrollView();

        if (AssetPreview.IsLoadingAssetPreviews())
            Repaint();
    }

    void OnSceneGUI(SceneView view)
    {
        view.wantsMouseMove = true;
        view.wantsMouseEnterLeaveWindow = true;

        if (selected == null)
        {
            ClearPlacingObj();
            return;
        }

        int control = GUIUtility.GetControlID(FocusType.Passive);

        HandleUtility.Repaint();

        if (ev.type == EventType.KeyDown && ev.keyCode == KeyCode.Escape)
            Deselect();

        if (ev.isMouse)
            mousePos = ev.mousePosition;

        if (ev.type == EventType.MouseLeaveWindow)
            ClearPlacingObj();
        else if (ev.isMouse || ev.type == EventType.MouseEnterWindow)
            UpdatePlacingObj();

        switch (ev.type)
        {
            case EventType.Layout:
                HandleUtility.AddDefaultControl(control);
                break;
            case EventType.MouseDown:
                if (ev.button == 0)
                {
                    Tools.current = Tool.None;
                    ev.Use();

                    lastMousePos = mousePos;
                    currentScale = 1f;
                    scaleMode = false;
                    dragMode = true;

                    lastScale = placingObj.transform.localScale;
                }
                break;
            case EventType.MouseDrag:
                if (ev.button == 0)
                {
                    if ((mousePos - lastMousePos).magnitude > .2f)
                        scaleMode = true;
                    if (scaleMode)
                    {
                        currentScale = Mathf.Pow(2f, -(mousePos - lastMousePos).y / 40f);
                        placingObj.transform.localScale = lastScale * currentScale;
                    }
                }
                break;
            case EventType.MouseUp:
                if (ev.button == 0)
                {
                    Tools.current = Tool.None;
                    ev.Use();
                    PlaceObj();

                    currentScale = 1f;
                    dragMode = false;
                }
                break;
        }

        if (placingObj != null)
            Handles.RectangleHandleCap(control, placePos, Quaternion.FromToRotation(Vector3.down, placeNor), 0.45f * currentScale, EventType.Repaint);

        Handles.BeginGUI();
        GUILayout.BeginArea(new Rect(4f, 4f, 300f, EditorGUIUtility.singleLineHeight * 3f));
        var r = GUILayoutUtility.GetRect(300f, EditorGUIUtility.singleLineHeight);
        GUI.Label(r, "X: " + placePos.x.ToString("0.00"), EditorStyles.whiteBoldLabel);
        r = GUILayoutUtility.GetRect(300f, EditorGUIUtility.singleLineHeight);
        r.y -= 4f;
        GUI.Label(r, "Y: " + placePos.y.ToString("0.00"), EditorStyles.whiteBoldLabel);
        r = GUILayoutUtility.GetRect(300f, EditorGUIUtility.singleLineHeight);
        r.y -= 8f;
        GUI.Label(r, "Z: " + placePos.z.ToString("0.00"), EditorStyles.whiteBoldLabel);
        GUILayout.EndArea();
        Handles.EndGUI();
    }

    void ClearPlacingObj()
    {
        if (placingObj != null)
        {
            DestroyImmediate(placingObj);
            placingObj = null;
        }
    }

    void UpdatePlacingObj()
    {
        if (placingObj != null)
        {
            var prefab = (GameObject)PrefabUtility.GetCorrespondingObjectFromSource(placingObj);
            if (selected != prefab)
                ClearPlacingObj();
        }

        if (placingObj == null && selected != null)
        {
            placingObj = (GameObject)PrefabUtility.InstantiatePrefab(selected, SceneManager.GetActiveScene());
            placingObj.hideFlags = HideFlags.HideAndDontSave | HideFlags.NotEditable;
        }

        if (placingObj == null)
            return;

        var ray = HandleUtility.GUIPointToWorldRay(mousePos);

        float myZ = (parentTo?.position.z).GetValueOrDefault(0);
        Vector3 myForward = Vector3.forward;
        Vector3 myUp = Vector3.up;
        if (stageType == PrefabPalette.StageType.WORLD_MAP)
        {
            myZ = (-parentTo?.position.y).GetValueOrDefault(0);
            myForward = Vector3.down;
            myUp = Vector3.forward;
        }

        float z = 0;
        if (parentTo != null)
            z = myZ;
        var plane = new Plane(myForward, -z);
        float enter;
        if (plane.Raycast(ray, out enter))
        {
            if (!dragMode)
                placePos = ray.GetPoint(enter);
            placeNor = myUp;
        }

        placingObj.transform.localPosition = placePos;
    }

    void PlaceObj()
    {
        if (placingObj == null)
            return;

        var t = placingObj.transform;
        placingObj.hideFlags = HideFlags.None;

        Undo.RegisterCreatedObjectUndo(placingObj, "place object");

        placingObj = null;
        UpdatePlacingObj();

        if (parentTo != null)
        {
            var pos = t.localPosition;
            var rot = t.localRotation;
            t.parent = parentTo;
            t.position = pos;
            t.rotation = rot;
        }
    }

    public Event ev
    {
        get { return Event.current; }
    }
}
