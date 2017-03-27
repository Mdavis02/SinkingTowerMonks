using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.AnimatedValues;

[CustomEditor(typeof(UnityEngine.Object),true,isFallback = true)]
[CanEditMultipleObjects]
public class CustomEditorBase : Editor {
	private Dictionary<string,ReorderableListProperty> reorderableLists;

	protected virtual void OnEnable() {
		reorderableLists = new Dictionary<string,ReorderableListProperty>(10);
	}

	~CustomEditorBase() {
		reorderableLists.Clear();
		reorderableLists = null;
	}

	//private Color cachedGuiColor;
	public override void OnInspectorGUI() {
		//EditorGUILayout.LabelField("Custom Editor",EditorStyles.centeredGreyMiniLabel);
		Color cachedGuiColor = GUI.color;
		//cachedGuiColor = GUI.color;
		serializedObject.Update();
		var property = serializedObject.GetIterator();
		var next = property.NextVisible(true);
		if(next) {
			do {
				GUI.color = cachedGuiColor;
				HandleProperty(property);
			} while(property.NextVisible(false));
		}
		serializedObject.ApplyModifiedProperties();
		//Repaint();
	}

	protected void HandleProperty(SerializedProperty property) {
		//Debug.LogFormat("name: {0}, displayName: {1}, type: {2}, propertyType: {3}, path: {4}", property.name, property.displayName, property.type, property.propertyType, property.propertyPath);
		bool isdefaultScriptProperty = property.name.Equals("m_Script") && property.type.Equals("PPtr<MonoScript>") && property.propertyType == SerializedPropertyType.ObjectReference && property.propertyPath.Equals("m_Script");
		bool cachedGUIEnabled = GUI.enabled;
		if(isdefaultScriptProperty) GUI.enabled = false;
		//var attr = this.GetPropertyAttributes(property);
		if(property.isArray && property.propertyType != SerializedPropertyType.String) HandleArray(property);
		else EditorGUILayout.PropertyField(property,property.isExpanded);
		if(isdefaultScriptProperty) GUI.enabled = cachedGUIEnabled;
	}

	protected void HandleArray(SerializedProperty property) {
		var listData = GetReorderableList(property);
		listData.IsExpanded.target = property.isExpanded;
		//if((!listData.IsExpanded.value && !listData.IsExpanded.isAnimating) || (!listData.IsExpanded.value && listData.IsExpanded.isAnimating)) { //uh... what?
		if(!listData.IsExpanded.value) {
			EditorGUILayout.BeginHorizontal();
			//property.isExpanded = EditorGUILayout.ToggleLeft(string.Format("{0}[]",property.displayName),property.isExpanded,EditorStyles.boldLabel);
			bool toggle = EditorGUILayout.ToggleLeft(string.Format("{0}[]",property.displayName),property.isExpanded,EditorStyles.boldLabel);
			if(toggle != property.isExpanded) Repaint(); //force redraw of inspector when array is shown / hidden
			property.isExpanded = toggle;
			EditorGUILayout.LabelField(string.Format("size: {0}",property.arraySize));
			EditorGUILayout.EndHorizontal();
		} else {
			if(EditorGUILayout.BeginFadeGroup(listData.IsExpanded.faded)) listData.List.DoLayoutList();
			EditorGUILayout.EndFadeGroup();
		}
	}

	protected object[] GetPropertyAttributes(SerializedProperty property) {
		return GetPropertyAttributes<PropertyAttribute>(property);
	}

	protected object[] GetPropertyAttributes<T>(SerializedProperty property) where T : System.Attribute {
		BindingFlags bindingFlags = BindingFlags.GetField | BindingFlags.GetProperty | BindingFlags.IgnoreCase |
									BindingFlags.Instance | BindingFlags.NonPublic   | BindingFlags.Public;
		if(property.serializedObject.targetObject == null) return null;
		var targetType = property.serializedObject.targetObject.GetType();
		var field = targetType.GetField(property.name,bindingFlags);
		if(field != null) return field.GetCustomAttributes(typeof(T),true);
		return null;
	}

	private ReorderableListProperty GetReorderableList(SerializedProperty property) {
		ReorderableListProperty ret = null;
		if(reorderableLists.TryGetValue(property.name,out ret)) {
			ret.Property = property;
			return ret;
		}
		ret = new ReorderableListProperty(property);
		reorderableLists.Add(property.name,ret);
		return ret;
	}
	//private ReorderableListProperty GetReorderableList(SerializedProperty property) {
	//	ReorderableListProperty ret = null;
	//	if(reorderableLists.TryGetValue(property.name,out ret)) ret.Property = property;
	//	else {
	//		ret = new ReorderableListProperty(property);
	//		reorderableLists.Add(property.name,ret);
	//	}
	//	SerializedProperty depthprop = property.Copy(); int depth;
	//	if(depthprop.NextVisible(true) && (depth = depthprop.depth) > property.depth) {
	//		do {
	//			GUI.color = cachedGuiColor;
	//			HandleProperty(depthprop);
	//		} while(depthprop.NextVisible(false) && depthprop.depth == depth);
	//	}

	//	return ret;
	//}

	#region Inner-class ReorderableListProperty
	private class ReorderableListProperty {
		public AnimBool IsExpanded { get; private set; }

		/// <summary>
		/// ref http://va.lent.in/unity-make-your-lists-functional-with-reorderablelist/
		/// </summary>
		public ReorderableList List { get; private set; }

		private SerializedProperty _property;
		public SerializedProperty Property {
			get { return _property; }
			set { _property = value; List.serializedProperty = _property; }
		}

		public ReorderableListProperty(SerializedProperty property) {
			IsExpanded = new AnimBool(property.isExpanded);
			IsExpanded.speed = 1f;
			_property = property;
			CreateList();
		}

		~ReorderableListProperty() {
			_property = null;
			List = null;
		}

		private void CreateList() {
			bool dragable = true, header = true, add = true, remove = true;
			List = new ReorderableList(Property.serializedObject,Property,dragable,header,add,remove);
			List.drawHeaderCallback += rect => _property.isExpanded = EditorGUI.ToggleLeft(rect,_property.displayName,_property.isExpanded,EditorStyles.boldLabel);
			//List.onAddCallback += (ReorderableList list) => {
			//	int index = list.serializedProperty.arraySize;
			//	list.serializedProperty.arraySize++;
			//	list.index = index;

			//	new SerializedObject()
			//	SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index);
			//	element.serializedObject.
			//	element.FindPropertyRelative()  //need reflection somehow to always start with default values in monobehavior scripts
			//};
			//List.onCanRemoveCallback += (list) => { return List.count > 0; };
			List.onCanRemoveCallback += (list) => { return list.count > 0; };
			List.drawElementCallback += drawElement;
			List.elementHeightCallback += (idx) => { return Mathf.Max(EditorGUIUtility.singleLineHeight,EditorGUI.GetPropertyHeight(_property.GetArrayElementAtIndex(idx),GUIContent.none,true)) + 4.0f; };
		}

		private void drawElement(Rect rect,int index,bool active,bool focused) {
			if(_property.GetArrayElementAtIndex(index).propertyType == SerializedPropertyType.Generic) {
				//EditorGUI.LabelField(rect,"["+index+"] "+_property.GetArrayElementAtIndex(index).displayName);
				EditorGUI.LabelField(rect,string.Format("[{0}] {1}",index,_property.GetArrayElementAtIndex(index).displayName));
			}
			//rect.height = 16;
			rect.height = EditorGUI.GetPropertyHeight(_property.GetArrayElementAtIndex(index),GUIContent.none,true);
			rect.y += 1;
			EditorGUI.PropertyField(rect,_property.GetArrayElementAtIndex(index),GUIContent.none,true);
			//if(_property.isArray && _property.propertyType != SerializedPropertyType.String) {
			//	HandleArray(_property);
			//	//} else EditorGUILayout.PropertyField(property,property.isExpanded);
			//} else EditorGUI.PropertyField(rect,_property.GetArrayElementAtIndex(index),GUIContent.none,true);
			List.elementHeight = rect.height + 4.0f;
		}
	}
	#endregion
}