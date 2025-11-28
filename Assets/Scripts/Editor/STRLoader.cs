using UnityEditor;
using UnityEngine;

namespace Editor
{
	class STRLoader : EditorWindow
	{
		string loadSTRButton = "Load STR";
		string filePath = "";

		[MenuItem("Window/Simpsons/STR")]
		public static void ShowWindow()
		{
			EditorWindow.GetWindow(typeof(STRLoader));
		}

		void OnGUI()
		{
			filePath = GUILayout.TextField(filePath);

			EditorGUILayout.BeginHorizontal();
			if (GUILayout.Button(loadSTRButton))
			{
				if (filePath != "")
				{
					SDBMHash.PrecomputeHashes();
					EAStreamFile.LoadSTRFile(filePath);
				}
			}
			EditorGUILayout.EndHorizontal();
		}
	}
}