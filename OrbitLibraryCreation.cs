using UnityEngine;
using UnityEditor;
using System.Collections;

public class OrbitLibraryCreation : EditorWindow {
	
	private string newNetworkName = "MyNewSkillLibrary";
	
	public OrbitLibraryCreation()
	{
        titleContent = new GUIContent("Orbit Skill Library Setup");
		
		position = new Rect(200f,200f,300f,75f);
		maxSize = new Vector2(300f,75f);
		minSize = new Vector2(300f,75f);			
	}	
	
	void OnGUI()
	{		
		EditorGUILayout.BeginVertical();
		{
			EditorGUILayout.BeginHorizontal();
			{
				EditorGUILayout.PrefixLabel("Library Name:");
				newNetworkName = EditorGUILayout.TextField(newNetworkName);
			}
			EditorGUILayout.EndHorizontal();
			
			GUILayout.FlexibleSpace();
			
			EditorGUILayout.BeginHorizontal();
			{
				GUILayout.FlexibleSpace();
				
				if(GUILayout.Button("Create New Skill Library",GUILayout.ExpandWidth(false)) || Event.current.keyCode == KeyCode.Return || Event.current.keyCode == KeyCode.KeypadEnter)
				{
					CreateNewAsset();
				}
			}
			EditorGUILayout.EndHorizontal();
		}
		EditorGUILayout.EndVertical();	
	}	
	
	public void CreateNewAsset()
	{
		var asset = ScriptableObject.CreateInstance<OrbitAsset>();

		newNetworkName = newNetworkName.Replace(" ", string.Empty);

		string nameCopy = newNetworkName.Clone().ToString();
			
		int index = 0;

		if(System.IO.File.Exists(Application.dataPath + "/coAdjoint/Orbit/Libraries/" + newNetworkName + ".asset"))
		{
			++index;
			
			while(System.IO.File.Exists(Application.dataPath + "/coAdjoint/Orbit/Libraries/" + newNetworkName + index + ".asset"))
			{
				++index;
			}
			
			nameCopy += index.ToString();
		}
		
		asset.libraryData = (new OrbitLibrary()).GetData();		
		
		AssetDatabase.CreateAsset(asset,"Assets/coAdjoint/Orbit/Libraries/" + nameCopy + ".asset");
		
		Selection.activeObject = asset;
		
		OrbitMenu.EditLibraryAsset();
		
		this.Close();
	}
}


