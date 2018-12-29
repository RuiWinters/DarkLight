using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(NpcSetup))]
public class NpcSetupEditor : Editor {
	
	public bool showDialogSetup,showDialogSetupQuest,showDialogSetupQuestInProgress,showDialogSetupQuestComplete,showDialogSetupQuestAfterComplete;
	NpcSetup npcSetup;

	public override void OnInspectorGUI(){
		
		npcSetup = (NpcSetup)target;
		
		npcSetup.npcFace = (Texture2D)EditorGUILayout.ObjectField("Npc Image",npcSetup.npcFace,typeof(Texture2D),true);
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		npcSetup.npcType = (NpcSetup.NpcType)EditorGUILayout.EnumPopup("Npc Type",npcSetup.npcType);
		npcSetup.npcName = EditorGUILayout.TextField("Npc Name",npcSetup.npcName);
		
		
		//Show Dialog
		if(npcSetup.npcType != NpcSetup.NpcType.QuestNpc)
		{
			showDialogSetup = EditorGUILayout.Foldout(showDialogSetup,"Dialog Setup");
			EditorGUI.indentLevel++;
			if(showDialogSetup)
			{
				npcSetup.sizeDialog = EditorGUILayout.IntField("Dialog List",npcSetup.sizeDialog);
				
				while(npcSetup.sizeDialog != npcSetup.dialogSetting.Count)
				{
					if(npcSetup.sizeDialog > npcSetup.dialogSetting.Count)
					{
						npcSetup.dialogSetting.Add(new NpcSetup.MessageBoxSetting());
						npcSetup.showSizeDialog.Add(true);
					}
					else
					{
						npcSetup.dialogSetting.RemoveAt(npcSetup.dialogSetting.Count-1);
						npcSetup.showSizeDialog.RemoveAt(npcSetup.showSizeDialog.Count-1);
					}
				}
				
				for(int i = 0;i<npcSetup.dialogSetting.Count;i++)
				{
					npcSetup.showSizeDialog[i] = EditorGUILayout.Foldout(npcSetup.showSizeDialog[i],"Dialog " + (i+1).ToString());
					
					if(npcSetup.showSizeDialog[i])
					{
						npcSetup.dialogSetting[i].enableFace = EditorGUILayout.Toggle("Enable Face",npcSetup.dialogSetting[i].enableFace);
						if(npcSetup.dialogSetting[i].enableFace)
						npcSetup.dialogSetting[i].faceSetup = (NpcSetup.FaceSetup)EditorGUILayout.EnumPopup("Face Setup",npcSetup.dialogSetting[i].faceSetup);
						if(npcSetup.dialogSetting[i].faceSetup == NpcSetup.FaceSetup.Other)
						npcSetup.dialogSetting[i].otherFace = (Texture2D)EditorGUILayout.ObjectField("Other Face",npcSetup.dialogSetting[i].otherFace,typeof(Texture2D),true);
							
						npcSetup.dialogSetting[i].enableNameTag = EditorGUILayout.Toggle("Enable Name Tag",npcSetup.dialogSetting[i].enableNameTag);
						if(npcSetup.dialogSetting[i].enableNameTag)
						npcSetup.dialogSetting[i].nameTagSetup = (NpcSetup.NameTagSetup)EditorGUILayout.EnumPopup("Name Tag Setup",npcSetup.dialogSetting[i].nameTagSetup);
						if(npcSetup.dialogSetting[i].faceSetup == NpcSetup.FaceSetup.Other)
						npcSetup.dialogSetting[i].otherNameTag = EditorGUILayout.TextField("Other Name Tag",npcSetup.dialogSetting[i].otherNameTag);
						
						GUIStyle style = new GUIStyle();
						if(!UnityEditorInternal.InternalEditorUtility.HasPro())
						style.normal.textColor = Color.black;
						else
						style.normal.textColor = Color.gray;
						
						EditorGUILayout.LabelField("Message", "");
						EditorGUI.indentLevel++;
						npcSetup.dialogSetting[i].message = EditorGUILayout.TextArea(npcSetup.dialogSetting[i].message);
						EditorGUI.indentLevel--;
					}
						
				}
				
			}
		}else
		{
			npcSetup.questID = EditorGUILayout.IntField("Quest ID",npcSetup.questID);
			
			showDialogSetupQuest = EditorGUILayout.Foldout(showDialogSetupQuest,"Dialog Quest Setup");
			EditorGUI.indentLevel++;
			if(showDialogSetupQuest)
			{
				npcSetup.sizeDialogQuest = EditorGUILayout.IntField("Dialog Quest List",npcSetup.sizeDialogQuest);
				
				while(npcSetup.sizeDialogQuest != npcSetup.dialogQuest.Count)
				{
					if(npcSetup.sizeDialogQuest > npcSetup.dialogQuest.Count)
					{
						npcSetup.dialogQuest.Add(new NpcSetup.MessageBoxSetting());
						npcSetup.showSizeDialogQuest.Add(true);
					}
					else
					{
						npcSetup.dialogQuest.RemoveAt(npcSetup.dialogQuest.Count-1);
						npcSetup.showSizeDialogQuest.RemoveAt(npcSetup.showSizeDialogQuest.Count-1);
					}
				}
				
				for(int i = 0;i<npcSetup.dialogQuest.Count;i++)
				{
					npcSetup.showSizeDialogQuest[i] = EditorGUILayout.Foldout(npcSetup.showSizeDialogQuest[i],"Dialog Quest " + (i+1).ToString());
					
					if(npcSetup.showSizeDialogQuest[i])
					{
						npcSetup.dialogQuest[i].enableFace = EditorGUILayout.Toggle("Enable Face",npcSetup.dialogQuest[i].enableFace);
						if(npcSetup.dialogQuest[i].enableFace)
						npcSetup.dialogQuest[i].faceSetup = (NpcSetup.FaceSetup)EditorGUILayout.EnumPopup("Face Setup",npcSetup.dialogQuest[i].faceSetup);
						if(npcSetup.dialogQuest[i].faceSetup == NpcSetup.FaceSetup.Other)
						npcSetup.dialogQuest[i].otherFace = (Texture2D)EditorGUILayout.ObjectField("Other Face",npcSetup.dialogQuest[i].otherFace,typeof(Texture2D),true);
							
						npcSetup.dialogQuest[i].enableNameTag = EditorGUILayout.Toggle("Enable Name Tag",npcSetup.dialogQuest[i].enableNameTag);
						if(npcSetup.dialogQuest[i].enableNameTag)
						npcSetup.dialogQuest[i].nameTagSetup = (NpcSetup.NameTagSetup)EditorGUILayout.EnumPopup("Name Tag Setup",npcSetup.dialogQuest[i].nameTagSetup);
						if(npcSetup.dialogQuest[i].faceSetup == NpcSetup.FaceSetup.Other)
						npcSetup.dialogQuest[i].otherNameTag = EditorGUILayout.TextField("Other Name Tag",npcSetup.dialogQuest[i].otherNameTag);
						
						GUIStyle style = new GUIStyle();
						if(!UnityEditorInternal.InternalEditorUtility.HasPro())
						style.normal.textColor = Color.black;
						else
						style.normal.textColor = Color.gray;
						
						EditorGUILayout.LabelField("Message", "");
						EditorGUI.indentLevel++;
						npcSetup.dialogQuest[i].message = EditorGUILayout.TextArea(npcSetup.dialogQuest[i].message);
						EditorGUI.indentLevel--;
					}
						
				}
				
			}
			
			EditorGUI.indentLevel--;
			EditorGUI.indentLevel--;
			showDialogSetupQuestInProgress = EditorGUILayout.Foldout(showDialogSetupQuestInProgress,"Dialog Quest In-Progress Setup");
			EditorGUI.indentLevel++;
			if(showDialogSetupQuestInProgress)
			{
				npcSetup.sizeDialogQuestInProgress = EditorGUILayout.IntField("Dialog Quest In-Progress List",npcSetup.sizeDialogQuestInProgress);
				
				while(npcSetup.sizeDialogQuestInProgress != npcSetup.dialogQuestInProgress.Count)
				{
					if(npcSetup.sizeDialogQuestInProgress > npcSetup.dialogQuestInProgress.Count)
					{
						npcSetup.dialogQuestInProgress.Add(new NpcSetup.MessageBoxSetting());
						npcSetup.showSizeDialogQuestInProgress.Add(true);
					}
					else
					{
						npcSetup.dialogQuestInProgress.RemoveAt(npcSetup.dialogQuestInProgress.Count-1);
						npcSetup.showSizeDialogQuestInProgress.RemoveAt(npcSetup.showSizeDialogQuestInProgress.Count-1);
					}
				}
				
				for(int i = 0;i<npcSetup.dialogQuestInProgress.Count;i++)
				{
					npcSetup.showSizeDialogQuestInProgress[i] = EditorGUILayout.Foldout(npcSetup.showSizeDialogQuestInProgress[i],"Dialog In-Progress" + (i+1).ToString());
					
					if(npcSetup.showSizeDialogQuestInProgress[i])
					{
						npcSetup.dialogQuestInProgress[i].enableFace = EditorGUILayout.Toggle("Enable Face",npcSetup.dialogQuestInProgress[i].enableFace);
						if(npcSetup.dialogQuestInProgress[i].enableFace)
						npcSetup.dialogQuestInProgress[i].faceSetup = (NpcSetup.FaceSetup)EditorGUILayout.EnumPopup("Face Setup",npcSetup.dialogQuestInProgress[i].faceSetup);
						if(npcSetup.dialogQuestInProgress[i].faceSetup == NpcSetup.FaceSetup.Other)
						npcSetup.dialogQuestInProgress[i].otherFace = (Texture2D)EditorGUILayout.ObjectField("Other Face",npcSetup.dialogQuestInProgress[i].otherFace,typeof(Texture2D),true);
							
						npcSetup.dialogQuestInProgress[i].enableNameTag = EditorGUILayout.Toggle("Enable Name Tag",npcSetup.dialogQuestInProgress[i].enableNameTag);
						if(npcSetup.dialogQuestInProgress[i].enableNameTag)
						npcSetup.dialogQuestInProgress[i].nameTagSetup = (NpcSetup.NameTagSetup)EditorGUILayout.EnumPopup("Name Tag Setup",npcSetup.dialogQuestInProgress[i].nameTagSetup);
						if(npcSetup.dialogQuestInProgress[i].faceSetup == NpcSetup.FaceSetup.Other)
						npcSetup.dialogQuestInProgress[i].otherNameTag = EditorGUILayout.TextField("Other Name Tag",npcSetup.dialogQuestInProgress[i].otherNameTag);
						
						GUIStyle style = new GUIStyle();
						if(!UnityEditorInternal.InternalEditorUtility.HasPro())
						style.normal.textColor = Color.black;
						else
						style.normal.textColor = Color.gray;
						
						EditorGUILayout.LabelField("Message", "");
						EditorGUI.indentLevel++;
						npcSetup.dialogQuestInProgress[i].message = EditorGUILayout.TextArea(npcSetup.dialogQuestInProgress[i].message);
						EditorGUI.indentLevel--;
					}
						
				}
				
			}
			
			EditorGUI.indentLevel--;
			EditorGUI.indentLevel--;
			showDialogSetupQuestComplete = EditorGUILayout.Foldout(showDialogSetupQuestComplete,"Dialog Quest Complete Setup");
			EditorGUI.indentLevel++;
			if(showDialogSetupQuestComplete)
			{
				npcSetup.sizeDialogQuestComplete = EditorGUILayout.IntField("Dialog Quest Complete List",npcSetup.sizeDialogQuestComplete);
				
				while(npcSetup.sizeDialogQuestComplete != npcSetup.dialogQuestComplete.Count)
				{
					if(npcSetup.sizeDialogQuestComplete > npcSetup.dialogQuestComplete.Count)
					{
						npcSetup.dialogQuestComplete.Add(new NpcSetup.MessageBoxSetting());
						npcSetup.showSizeDialogQuestComplete.Add(true);
					}
					else
					{
						npcSetup.dialogQuestComplete.RemoveAt(npcSetup.dialogQuestComplete.Count-1);
						npcSetup.showSizeDialogQuestComplete.RemoveAt(npcSetup.showSizeDialogQuestComplete.Count-1);
					}
				}
				
				for(int i = 0;i<npcSetup.dialogQuestComplete.Count;i++)
				{
					npcSetup.showSizeDialogQuestComplete[i] = EditorGUILayout.Foldout(npcSetup.showSizeDialogQuestComplete[i],"Dialog Complete" + (i+1).ToString());
					
					if(npcSetup.showSizeDialogQuestComplete[i])
					{
						npcSetup.dialogQuestComplete[i].enableFace = EditorGUILayout.Toggle("Enable Face",npcSetup.dialogQuestComplete[i].enableFace);
						if(npcSetup.dialogQuestComplete[i].enableFace)
						npcSetup.dialogQuestComplete[i].faceSetup = (NpcSetup.FaceSetup)EditorGUILayout.EnumPopup("Face Setup",npcSetup.dialogQuestComplete[i].faceSetup);
						if(npcSetup.dialogQuestComplete[i].faceSetup == NpcSetup.FaceSetup.Other)
						npcSetup.dialogQuestComplete[i].otherFace = (Texture2D)EditorGUILayout.ObjectField("Other Face",npcSetup.dialogQuestComplete[i].otherFace,typeof(Texture2D),true);
							
						npcSetup.dialogQuestComplete[i].enableNameTag = EditorGUILayout.Toggle("Enable Name Tag",npcSetup.dialogQuestComplete[i].enableNameTag);
						if(npcSetup.dialogQuestComplete[i].enableNameTag)
						npcSetup.dialogQuestComplete[i].nameTagSetup = (NpcSetup.NameTagSetup)EditorGUILayout.EnumPopup("Name Tag Setup",npcSetup.dialogQuestComplete[i].nameTagSetup);
						if(npcSetup.dialogQuestComplete[i].faceSetup == NpcSetup.FaceSetup.Other)
						npcSetup.dialogQuestComplete[i].otherNameTag = EditorGUILayout.TextField("Other Name Tag",npcSetup.dialogQuestComplete[i].otherNameTag);
						
						GUIStyle style = new GUIStyle();
						if(!UnityEditorInternal.InternalEditorUtility.HasPro())
						style.normal.textColor = Color.black;
						else
						style.normal.textColor = Color.gray;
						
						EditorGUILayout.LabelField("Message", "");
						EditorGUI.indentLevel++;
						npcSetup.dialogQuestComplete[i].message = EditorGUILayout.TextArea(npcSetup.dialogQuestComplete[i].message);
						EditorGUI.indentLevel--;
					}
						
				}
				
			}
			
			EditorGUI.indentLevel--;
			EditorGUI.indentLevel--;
			showDialogSetupQuestAfterComplete = EditorGUILayout.Foldout(showDialogSetupQuestAfterComplete,"Dialog Quest After Complete Setup");
			EditorGUI.indentLevel++;
			if(showDialogSetupQuestAfterComplete)
			{
				npcSetup.sizeDialogQuestAfterComplete = EditorGUILayout.IntField("Dialog Quest After Complete List",npcSetup.sizeDialogQuestAfterComplete);
				
				while(npcSetup.sizeDialogQuestAfterComplete != npcSetup.dialogQuestAfterComplete.Count)
				{
					if(npcSetup.sizeDialogQuestAfterComplete > npcSetup.dialogQuestAfterComplete.Count)
					{
						npcSetup.dialogQuestAfterComplete.Add(new NpcSetup.MessageBoxSetting());
						npcSetup.showSizeDialogQuestAfterComplete.Add(true);
					}
					else
					{
						npcSetup.dialogQuestAfterComplete.RemoveAt(npcSetup.dialogQuestAfterComplete.Count-1);
						npcSetup.showSizeDialogQuestAfterComplete.RemoveAt(npcSetup.showSizeDialogQuestAfterComplete.Count-1);
					}
				}
				
				for(int i = 0;i<npcSetup.dialogQuestAfterComplete.Count;i++)
				{
					npcSetup.showSizeDialogQuestAfterComplete[i] = EditorGUILayout.Foldout(npcSetup.showSizeDialogQuestAfterComplete[i],"Dialog After Complete" + (i+1).ToString());
					
					if(npcSetup.showSizeDialogQuestAfterComplete[i])
					{
						npcSetup.dialogQuestAfterComplete[i].enableFace = EditorGUILayout.Toggle("Enable Face",npcSetup.dialogQuestAfterComplete[i].enableFace);
						if(npcSetup.dialogQuestAfterComplete[i].enableFace)
						npcSetup.dialogQuestAfterComplete[i].faceSetup = (NpcSetup.FaceSetup)EditorGUILayout.EnumPopup("Face Setup",npcSetup.dialogQuestAfterComplete[i].faceSetup);
						if(npcSetup.dialogQuestAfterComplete[i].faceSetup == NpcSetup.FaceSetup.Other)
						npcSetup.dialogQuestAfterComplete[i].otherFace = (Texture2D)EditorGUILayout.ObjectField("Other Face",npcSetup.dialogQuestAfterComplete[i].otherFace,typeof(Texture2D),true);
							
						npcSetup.dialogQuestAfterComplete[i].enableNameTag = EditorGUILayout.Toggle("Enable Name Tag",npcSetup.dialogQuestAfterComplete[i].enableNameTag);
						if(npcSetup.dialogQuestAfterComplete[i].enableNameTag)
						npcSetup.dialogQuestAfterComplete[i].nameTagSetup = (NpcSetup.NameTagSetup)EditorGUILayout.EnumPopup("Name Tag Setup",npcSetup.dialogQuestAfterComplete[i].nameTagSetup);
						if(npcSetup.dialogQuestAfterComplete[i].faceSetup == NpcSetup.FaceSetup.Other)
						npcSetup.dialogQuestAfterComplete[i].otherNameTag = EditorGUILayout.TextField("Other Name Tag",npcSetup.dialogQuestAfterComplete[i].otherNameTag);
						
						GUIStyle style = new GUIStyle();
						if(!UnityEditorInternal.InternalEditorUtility.HasPro())
						style.normal.textColor = Color.black;
						else
						style.normal.textColor = Color.gray;
						
						EditorGUILayout.LabelField("Message", "");
						EditorGUI.indentLevel++;
						npcSetup.dialogQuestAfterComplete[i].message = EditorGUILayout.TextArea(npcSetup.dialogQuestAfterComplete[i].message);
						EditorGUI.indentLevel--;
					}
						
				}
				
			}
		}
		
		
		if(GUI.changed)
			EditorUtility.SetDirty(npcSetup);
	}
}
