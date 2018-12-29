using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(Quest_Data))]
public class QuestDataEditor : Editor {
	
	public bool showQuest;
	
	public override void OnInspectorGUI(){
		
		Quest_Data questData = (Quest_Data)target;
		
		showQuest = EditorGUILayout.Foldout(showQuest,"Quest Setup");
			EditorGUI.indentLevel++;
			if(showQuest)
			{
				questData.sizeQuest = EditorGUILayout.IntField("Quest List",questData.sizeQuest);
				
				while(questData.sizeQuest != questData.questSetting.Count)
				{
					if(questData.sizeQuest > questData.questSetting.Count)
					{
						questData.questSetting.Add(new Quest_Data.QuestSetting());
						questData.showSizeQuest.Add(true);
					}
					else
					{
						questData.questSetting.RemoveAt(questData.questSetting.Count-1);
						questData.showSizeQuest.RemoveAt(questData.showSizeQuest.Count-1);
					}
				}
			
				
				for(int i = 0;i<questData.questSetting.Count;i++)
				{
					questData.showSizeQuest[i] = EditorGUILayout.Foldout(questData.showSizeQuest[i],"Quest " + (i+1).ToString());
					
					if(questData.showSizeQuest[i])
					{
						EditorGUILayout.LabelField("Quest ID",questData.questSetting[i].questID.ToString());
						questData.questSetting[i].questID = 1000 + (i+1);
						
						questData.questSetting[i].questName = EditorGUILayout.TextField("Quest Name",questData.questSetting[i].questName);
					
						EditorGUILayout.LabelField("Quest Detail", "");
						EditorGUI.indentLevel++;
						questData.questSetting[i].questDetail = EditorGUILayout.TextArea(questData.questSetting[i].questDetail);
						EditorGUI.indentLevel--;
						
						EditorGUILayout.Space();
						questData.questSetting[i].questCondition = (Quest_Data.QuestCondition)EditorGUILayout.EnumPopup("Quest Condition",questData.questSetting[i].questCondition);
					
						if(questData.questSetting[i].questCondition == Quest_Data.QuestCondition.Hunting)
						{
							questData.questSetting[i].idCondition.x = EditorGUILayout.IntField("Enemy ID",(int)questData.questSetting[i].idCondition.x);
							questData.questSetting[i].idCondition.y = EditorGUILayout.IntField("Amount",(int)questData.questSetting[i].idCondition.y);
						}else
						{
							questData.questSetting[i].idCondition.x = EditorGUILayout.IntField("Item ID",(int)questData.questSetting[i].idCondition.x);
							questData.questSetting[i].idCondition.y = EditorGUILayout.IntField("Amount",(int)questData.questSetting[i].idCondition.y);
						}
						
						EditorGUILayout.Space();
						EditorGUILayout.LabelField("Item Reward", "");
						questData.questSetting[i].itemIDReward.x = EditorGUILayout.IntField("Item ID",(int)questData.questSetting[i].itemIDReward.x);
						questData.questSetting[i].itemIDReward.y = EditorGUILayout.IntField("Amount",(int)questData.questSetting[i].itemIDReward.y);
					
						EditorGUILayout.Space();
						questData.questSetting[i].repeatQuest = EditorGUILayout.Toggle("Can Repeat",questData.questSetting[i].repeatQuest);
						
						EditorGUILayout.Space();
						EditorGUILayout.Space();
					
					}
						
				}
		}
		
		if(GUI.changed)
			EditorUtility.SetDirty(questData);
 
	    
	}
}

