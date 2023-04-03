using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BaseScene), true)]     //자식 클래서 에서도  하기 위해서 사요
public class BaseSceneInspector : Editor        //에디터 상속 
{

    BaseScene _editor;
    // Start is called before the first frame update
    void OnEnable()
    {
        _editor = target as BaseScene;
    }

    // Update is called once per frame
    public override void OnInspectorGUI()
    {
        _editor.sceneIndex = EditorGUILayout.Popup(_editor.sceneIndex, new string[] { "데이터 0", "데이터 1" });
        _editor.sceneDataShow = EditorGUILayout.Toggle(new GUIContent("데이터 보기"), _editor.sceneDataShow);

        switch(_editor.sceneIndex)
        {
            case 0:
                _editor.sceneData0 = EditorGUILayout.TextField(new GUIContent("데이터 0"), _editor.sceneData0);
                if(_editor.sceneDataShow)
                {
                    _editor.sceneIndex = EditorGUILayout.IntField(new GUIContent("SceneIndex"), _editor.sceneIndex);
                }
                break;

            case 1:
                _editor.sceneData1 = EditorGUILayout.TextField(new GUIContent("데이터 1"), _editor.sceneData1);
                break;
        }
    }
}
