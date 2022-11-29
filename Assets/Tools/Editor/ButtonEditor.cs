using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Tools
{
    [CustomEditor(typeof(Object), true, isFallback = false)]
    [CanEditMultipleObjects]
    public class ButtonEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            
           // GUIContent btnTxt = new GUIContent("OK");
            //var rt = GUILayoutUtility.GetRect(btnTxt, GUI.skin.button, GUILayout.ExpandWidth(false));
           
            //rt.center = new Vector2(EditorGUIUtility.currentViewWidth / 2, rt.center.y);
            //GUI.Button(rt, btnTxt, GUI.skin.button);
            
            //GUI.Label(rt, "Сontrol Patrol Position Enemy");
            

            foreach (var target in targets)
            {
                var mis = target.GetType().GetMethods().Where(m =>
                    m.GetCustomAttributes().Any(a => a.GetType() == typeof(EditorButtonAttribute)));
                
                if (mis != null)
                {
                    foreach (var mi in mis)
                    {
                        if (mi != null)
                        {
                            var attribute = (EditorButtonAttribute) mi.GetCustomAttribute(typeof(EditorButtonAttribute));
                            if (GUILayout.Button(attribute.name))
                            {
                                mi.Invoke(target, null);
                            }
                        }
                    }
                }
                
                var create= target.GetType().GetMethods().Where(m =>
                    m.GetCustomAttributes().Any(a => a.GetType() == typeof(EditorButtonCreateAttribute)));
                
                var remove= target.GetType().GetMethods().Where(m =>
                    m.GetCustomAttributes().Any(a => a.GetType() == typeof(EditorButtonRemoveAttribute)));
                if (create != null && remove != null)
                {
                    for (int i = 0; i < create.Count(); i++)
                    {
                        GUILayout.Space(25);
                        
                        GUIStyle style = new GUIStyle();
                        style.fontStyle = FontStyle.Bold;
                        style.normal.textColor = Color.white;
                        
                        GUIContent label = new GUIContent("Control Patrol Position Enemy");
                        var rt = GUILayoutUtility.GetRect(label, GUI.skin.button, GUILayout.ExpandWidth(false));
                        rt.center = new Vector2(EditorGUIUtility.currentViewWidth / 2, rt.center.y);
                        GUI.Label(rt, label, style);
                        
                        GUILayout.BeginHorizontal();
                        
                        var attribute = (EditorButtonCreateAttribute) create.ElementAt(i).GetCustomAttribute(typeof(EditorButtonCreateAttribute));
                        var attribute2 = (EditorButtonRemoveAttribute) remove.ElementAt(i).GetCustomAttribute(typeof(EditorButtonRemoveAttribute));
                        if (GUILayout.Button(attribute.name))
                        {
                            create.ElementAt(i).Invoke(target, null);
                        }
                        if (GUILayout.Button(attribute2.name))
                        {
                            remove.ElementAt(i).Invoke(target, null);
                        }
                        
                        GUILayout.EndHorizontal();
                    }
                }
            }
        }
    }
}