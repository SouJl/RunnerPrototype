using Runner.Features.Tweens;
using UnityEditor;
using UnityEditor.UI;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Runner.Features.Tween.Editor
{
    [CustomEditor(typeof(ButtonTweenInheritance))]
    public class CustomButtonEditor :ButtonEditor
    {
        private SerializedProperty m_InteractableProperty;
        private SerializedProperty m_NavigationProperty;
        private SerializedProperty m_OnClickProperty;


        protected override void OnEnable()
        {
            m_InteractableProperty = serializedObject.FindProperty("m_Interactable");
            m_NavigationProperty = serializedObject.FindProperty("m_Navigation");
            m_OnClickProperty = serializedObject.FindProperty("m_OnClick");
        }

        // Новый способ редактирования представления инскпектора
        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();

            var animationType = new PropertyField(serializedObject.FindProperty(ButtonTweenInheritance.AnimationTypeName));
            var curveEase = new PropertyField(serializedObject.FindProperty(ButtonTweenInheritance.CurveEaseName));
            var duration = new PropertyField(serializedObject.FindProperty(ButtonTweenInheritance.DurationName));

            var tweenLabel = new Label("Settings Tween");

            root.Add(tweenLabel);
            root.Add(animationType);
            root.Add(curveEase);
            root.Add(duration);
 
            root.Add(new IMGUIContainer(OnInspectorGUI));

            return root;
        }

        // Старый способ представления инскпектора
        public override void OnInspectorGUI()
        {
            serializedObject.Update();          
            EditorGUILayout.PropertyField(m_InteractableProperty);
            EditorGUILayout.PropertyField(m_NavigationProperty);
            EditorGUILayout.PropertyField(m_OnClickProperty);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
