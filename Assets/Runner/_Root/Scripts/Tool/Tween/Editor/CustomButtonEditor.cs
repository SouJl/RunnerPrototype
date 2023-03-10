using Runner.Tool.Tweens;
using UnityEditor;
using UnityEditor.UI;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Runner.Features.Tween.Editor
{
    [CustomEditor(typeof(ButtonTween_Obsolete))]
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

        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();

            var animationType = new PropertyField(serializedObject.FindProperty(ButtonTween_Obsolete.AnimationTypeName));
            var curveEase = new PropertyField(serializedObject.FindProperty(ButtonTween_Obsolete.CurveEaseName));
            var duration = new PropertyField(serializedObject.FindProperty(ButtonTween_Obsolete.DurationName));
            var vibrato = new PropertyField(serializedObject.FindProperty(ButtonTween_Obsolete.Vibrato));
            var isIndependentUpdate = new PropertyField(serializedObject.FindProperty(ButtonTween_Obsolete.IsIndependentUpdate));

            var tweenLabel = new Label("Settings Tween");

            root.Add(tweenLabel);
            root.Add(animationType);
            root.Add(curveEase);
            root.Add(duration);
            root.Add(vibrato);
            root.Add(isIndependentUpdate);

            root.Add(new IMGUIContainer(OnInspectorGUI));

            return root;
        }

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
