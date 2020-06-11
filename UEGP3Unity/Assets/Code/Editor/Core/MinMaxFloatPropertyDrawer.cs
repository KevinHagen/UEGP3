using UEGP3.Core;
using UnityEditor;
using UnityEngine;

namespace UEGP3.Code.Editor.Core
{
	[CustomPropertyDrawer(typeof(MinMaxFloat), true)]
	public class MinMaxFloatPropertyDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			label = EditorGUI.BeginProperty(position, label, property);
			position = EditorGUI.PrefixLabel(position, label);

			// fetch properties
			SerializedProperty minProp = property.FindPropertyRelative("MinValue");
			SerializedProperty maxProp = property.FindPropertyRelative("MaxValue");

			// determine min/max value from properties
			float minValue = minProp.floatValue;
			float maxValue = maxProp.floatValue;

			// determine min/max values used
			float rangeMin = 0;
			float rangeMax = 1;

			// Look for attribute MinMaxFloatAttribute above field, using reflections
			var ranges = (MinMaxFloatAttribute[]) fieldInfo.GetCustomAttributes(typeof(MinMaxFloatAttribute), true);
			if (ranges.Length > 0)
			{
				rangeMin = ranges[0].Min;
				rangeMax = ranges[0].Max;
			}

			// width for labels
			const float rangeBoundsLabelWidth = 40f;

			// create label for min value
			var rangeBoundsLabel1Rect = new Rect(position);
			rangeBoundsLabel1Rect.width = rangeBoundsLabelWidth;
			GUI.Label(rangeBoundsLabel1Rect, new GUIContent(minValue.ToString("F2")));
			position.xMin += rangeBoundsLabelWidth;

			// create label for max value
			var rangeBoundsLabel2Rect = new Rect(position);
			rangeBoundsLabel2Rect.xMin = rangeBoundsLabel2Rect.xMax - rangeBoundsLabelWidth;
			GUI.Label(rangeBoundsLabel2Rect, new GUIContent(maxValue.ToString("F2")));
			position.xMax -= rangeBoundsLabelWidth;

			// Check whether the value was changed
			EditorGUI.BeginChangeCheck();
			EditorGUI.MinMaxSlider(position, ref minValue, ref maxValue, rangeMin, rangeMax);
			if (EditorGUI.EndChangeCheck())
			{
				// apply changes if they happened
				minProp.floatValue = minValue;
				maxProp.floatValue = maxValue;
			}

			EditorGUI.EndProperty();
		}
	}
}