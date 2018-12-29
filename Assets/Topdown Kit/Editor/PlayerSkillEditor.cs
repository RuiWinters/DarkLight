using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(PlayerSkill))]
public class PlayerSkillEditor : Editor {
	
	public bool showPassiveSkill = true,showActiveSkill = true,showBuffSkill = true;
	
	PlayerSkill playerSkill;
	
	public override void OnInspectorGUI(){
		
	 playerSkill = (PlayerSkill)target;
		
		//showPassiveSkill 
		showPassiveSkill = EditorGUILayout.Foldout(showPassiveSkill,"Passive Skill");
		EditorGUI.indentLevel++;
		if(showPassiveSkill)
		{
			playerSkill.sizePassiveSkill = EditorGUILayout.IntField("Passive Skill Size",playerSkill.sizePassiveSkill);
			
			while(playerSkill.sizePassiveSkill != playerSkill.passiveSkill.Count)
			{
				if(playerSkill.sizePassiveSkill > playerSkill.passiveSkill.Count)
				{
					playerSkill.passiveSkill.Add(new PlayerSkill.PassiveSkill());
					playerSkill.showPassiveSize.Add(true);
				}
				else
				{
					playerSkill.passiveSkill.RemoveAt(playerSkill.passiveSkill.Count-1);
					playerSkill.showPassiveSize.RemoveAt(playerSkill.showPassiveSize.Count-1);
				}
			}
			
			for(int i = 0;i<playerSkill.passiveSkill.Count;i++)
			{
				playerSkill.showPassiveSize[i] = EditorGUILayout.Foldout(playerSkill.showPassiveSize[i],playerSkill.passiveSkill[i].skillName);
				
				if(playerSkill.showPassiveSize[i])
				{
					playerSkill.passiveSkill[i].skillIcon = (Texture2D)EditorGUILayout.ObjectField("Skill Icon",playerSkill.passiveSkill[i].skillIcon,typeof(Texture2D),true);
					
					EditorGUILayout.LabelField("Skill ID",playerSkill.passiveSkill[i].skillID.ToString());
					playerSkill.passiveSkill[i].skillID = 1000 + (i+1);
					
					playerSkill.passiveSkill[i].skillName = EditorGUILayout.TextField("Skill Name",playerSkill.passiveSkill[i].skillName);
					playerSkill.passiveSkill[i].typeSkill = EditorGUILayout.TextField("Skill Type",playerSkill.passiveSkill[i].typeSkill);
					playerSkill.passiveSkill[i].unlockLevel = EditorGUILayout.IntField("Unlock At Level",playerSkill.passiveSkill[i].unlockLevel);
					playerSkill.passiveSkill[i].addAttribute = (PlayerSkill.AddAttribute)EditorGUILayout.EnumPopup("Add Attribute",playerSkill.passiveSkill[i].addAttribute);
					playerSkill.passiveSkill[i].addValue = EditorGUILayout.FloatField("Add Value",playerSkill.passiveSkill[i].addValue);
					GUIStyle style = new GUIStyle();
					if(!UnityEditorInternal.InternalEditorUtility.HasPro())
					style.normal.textColor = Color.black;
					else
					style.normal.textColor = Color.gray;
					
					EditorGUILayout.LabelField("Description", "");
					EditorGUI.indentLevel++;
					playerSkill.passiveSkill[i].description = EditorGUILayout.TextArea(playerSkill.passiveSkill[i].description,style);
					EditorGUI.indentLevel--;
				}
					
			}
			
		}
		EditorGUI.indentLevel--;
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		
		//showActiveSkill 
		showActiveSkill = EditorGUILayout.Foldout(showActiveSkill,"Active Skill");
		EditorGUI.indentLevel++;
		if(showActiveSkill)
		{
			playerSkill.sizeActiveAttack = EditorGUILayout.IntField("Active Skill Size",playerSkill.sizeActiveAttack);
			
			while(playerSkill.sizeActiveAttack != playerSkill.activeSkillAttack.Count)
			{
				if(playerSkill.sizeActiveAttack > playerSkill.activeSkillAttack.Count)
				{
					playerSkill.activeSkillAttack.Add(new PlayerSkill.ActiveSkillAttack());
					playerSkill.showActiveAttackSize.Add(true);
				}
				else
				{
					playerSkill.activeSkillAttack.RemoveAt(playerSkill.activeSkillAttack.Count-1);
					playerSkill.showActiveAttackSize.RemoveAt(playerSkill.showActiveAttackSize.Count-1);
				}
			}
			
			for(int i = 0;i<playerSkill.activeSkillAttack.Count;i++)
			{
				playerSkill.showActiveAttackSize[i] = EditorGUILayout.Foldout(playerSkill.showActiveAttackSize[i],playerSkill.activeSkillAttack[i].skillName);
				
				if(playerSkill.showActiveAttackSize[i])
				{
					playerSkill.activeSkillAttack[i].skillIcon = (Texture2D)EditorGUILayout.ObjectField("Skill Icon",playerSkill.activeSkillAttack[i].skillIcon,typeof(Texture2D),true);
					EditorGUILayout.LabelField("Skill ID",playerSkill.activeSkillAttack[i].skillID.ToString());
					playerSkill.activeSkillAttack[i].skillID = 2000 + (i+1);
					playerSkill.activeSkillAttack[i].skillName = EditorGUILayout.TextField("Skill Name",playerSkill.activeSkillAttack[i].skillName);
					playerSkill.activeSkillAttack[i].typeSkill = EditorGUILayout.TextField("Skill Type",playerSkill.activeSkillAttack[i].typeSkill);
					playerSkill.activeSkillAttack[i].unlockLevel = EditorGUILayout.IntField("Unlock At Level",playerSkill.activeSkillAttack[i].unlockLevel);
					playerSkill.activeSkillAttack[i].mpUse = EditorGUILayout.IntField("MP Use",playerSkill.activeSkillAttack[i].mpUse);
					playerSkill.activeSkillAttack[i].castTime = EditorGUILayout.FloatField("Cast Time",playerSkill.activeSkillAttack[i].castTime);
					playerSkill.activeSkillAttack[i].targetSkill = (PlayerSkill.TargetSkill)EditorGUILayout.EnumPopup("Target Skill",playerSkill.activeSkillAttack[i].targetSkill);
					
					if(playerSkill.activeSkillAttack[i].targetSkill == PlayerSkill.TargetSkill.MultipleTarget)
					{
						playerSkill.activeSkillAttack[i].skillArea = EditorGUILayout.FloatField("Skill Area",playerSkill.activeSkillAttack[i].skillArea);	
						playerSkill.activeSkillAttack[i].skillType = (PlayerSkill.SkillType)EditorGUILayout.EnumPopup("Type Skill",playerSkill.activeSkillAttack[i].skillType);
					}
					
					if(playerSkill.activeSkillAttack[i].skillType != PlayerSkill.SkillType.Instance)
					{
						playerSkill.activeSkillAttack[i].skillRange = EditorGUILayout.FloatField("Skill Range",playerSkill.activeSkillAttack[i].skillRange);
					}
					playerSkill.activeSkillAttack[i].animationAttack = (AnimationClip)EditorGUILayout.ObjectField("Animation Attack",playerSkill.activeSkillAttack[i].animationAttack,typeof(AnimationClip),true);
					playerSkill.activeSkillAttack[i].speedAnimation = EditorGUILayout.Slider("Speed Animation",playerSkill.activeSkillAttack[i].speedAnimation,0,3);
					playerSkill.activeSkillAttack[i].attackTimer = EditorGUILayout.Slider("Attack Timer",playerSkill.activeSkillAttack[i].attackTimer,0,0.99f);
					playerSkill.activeSkillAttack[i].multipleDamage = EditorGUILayout.Slider("Multiple Damage",playerSkill.activeSkillAttack[i].multipleDamage,0,10);
					playerSkill.activeSkillAttack[i].flichValue = EditorGUILayout.Slider("Flich Value",playerSkill.activeSkillAttack[i].flichValue,0,100);
					playerSkill.activeSkillAttack[i].skillAccurate = (int)EditorGUILayout.Slider("Skilll Accuarate",playerSkill.activeSkillAttack[i].skillAccurate,0,100);
					playerSkill.activeSkillAttack[i].speedTuning = EditorGUILayout.Toggle("Speed Tuning",playerSkill.activeSkillAttack[i].speedTuning);
					
					
					GUIStyle style = new GUIStyle();
					if(!UnityEditorInternal.InternalEditorUtility.HasPro())
					style.normal.textColor = Color.black;
					else
					style.normal.textColor = Color.gray;
					
					EditorGUILayout.LabelField("Description", "");
					EditorGUI.indentLevel++;
					playerSkill.activeSkillAttack[i].description = EditorGUILayout.TextArea(playerSkill.activeSkillAttack[i].description,style);
					EditorGUI.indentLevel--;
					if(playerSkill.activeSkillAttack[i].targetSkill == PlayerSkill.TargetSkill.MultipleTarget)
					{
						playerSkill.activeSkillAttack[i].skillFX = (GameObject)EditorGUILayout.ObjectField("Attack fx(Player)",playerSkill.activeSkillAttack[i].skillFX,typeof(GameObject),true);
						playerSkill.activeSkillAttack[i].soundFX = (AudioClip)EditorGUILayout.ObjectField("Sfx (Player)",playerSkill.activeSkillAttack[i].soundFX ,typeof(AudioClip),true);
					}
					
					playerSkill.activeSkillAttack[i].skillFxTarget = (GameObject)EditorGUILayout.ObjectField("Attack fx(Target)",playerSkill.activeSkillAttack[i].skillFxTarget,typeof(GameObject),true);
					playerSkill.activeSkillAttack[i].soundFxTarget = (AudioClip)EditorGUILayout.ObjectField("Sfx(Target)",playerSkill.activeSkillAttack[i].soundFxTarget ,typeof(AudioClip),true);
				}
					
			}
			
		}
		EditorGUI.indentLevel--;
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		
		//showSupportSkill
		showBuffSkill = EditorGUILayout.Foldout(showBuffSkill,"Support Skill");
		EditorGUI.indentLevel++;
		if(showBuffSkill)
		{
			playerSkill.sizeActiveSupport = EditorGUILayout.IntField("Support Skill Size",playerSkill.sizeActiveSupport);
			
			while(playerSkill.sizeActiveSupport != playerSkill.activeSkillSupport.Count)
			{
				if(playerSkill.sizeActiveSupport > playerSkill.activeSkillSupport.Count)
				{
					playerSkill.activeSkillSupport.Add(new PlayerSkill.ActiveSkillSupport());
					playerSkill.showActiveSupportSize.Add(true);
				}
				else
				{
					playerSkill.activeSkillSupport.RemoveAt(playerSkill.activeSkillSupport.Count-1);
					playerSkill.showActiveSupportSize.RemoveAt(playerSkill.showActiveSupportSize.Count-1);
				}
			}
			
			for(int i = 0;i<playerSkill.activeSkillSupport.Count;i++)
			{
				playerSkill.showActiveSupportSize[i] = EditorGUILayout.Foldout(playerSkill.showActiveSupportSize[i],playerSkill.activeSkillSupport[i].skillName);
				
				if(playerSkill.showActiveSupportSize[i])
				{
					playerSkill.activeSkillSupport[i].skillIcon = (Texture2D)EditorGUILayout.ObjectField("Skill Icon",playerSkill.activeSkillSupport[i].skillIcon,typeof(Texture2D),true);
					EditorGUILayout.LabelField("Skill ID",playerSkill.activeSkillSupport[i].skillID.ToString());
					playerSkill.activeSkillSupport[i].skillID = 3000 + (i+1);
					playerSkill.activeSkillSupport[i].skillName = EditorGUILayout.TextField("Skill Name",playerSkill.activeSkillSupport[i].skillName);
					
					playerSkill.activeSkillSupport[i].skillTypeSupport = (PlayerSkill.SkillTypeSupport)EditorGUILayout.EnumPopup("Type Skill",playerSkill.activeSkillSupport[i].skillTypeSupport);
					
					if(playerSkill.activeSkillSupport[i].skillTypeSupport == PlayerSkill.SkillTypeSupport.Buff)
					{
						playerSkill.activeSkillSupport[i].typeSkill = EditorGUILayout.TextField("Skill Type",playerSkill.activeSkillSupport[i].typeSkill);
						playerSkill.activeSkillSupport[i].unlockLevel = EditorGUILayout.IntField("Unlock At Level",playerSkill.activeSkillSupport[i].unlockLevel);
						playerSkill.activeSkillSupport[i].mpUse = EditorGUILayout.IntField("MP Use",playerSkill.activeSkillSupport[i].mpUse);
						playerSkill.activeSkillSupport[i].castTime = EditorGUILayout.FloatField("Cast Time",playerSkill.activeSkillSupport[i].castTime);
						playerSkill.activeSkillSupport[i].addAttribute = (PlayerSkill.AddAttribute)EditorGUILayout.EnumPopup("Add Attribute",playerSkill.activeSkillSupport[i].addAttribute);
						playerSkill.activeSkillSupport[i].addValue = EditorGUILayout.FloatField("Add Value",playerSkill.activeSkillSupport[i].addValue);
						playerSkill.activeSkillSupport[i].duration = EditorGUILayout.FloatField("Duration",playerSkill.activeSkillSupport[i].duration);
						playerSkill.activeSkillSupport[i].animationBuff = (AnimationClip)EditorGUILayout.ObjectField("Animation Buff",playerSkill.activeSkillSupport[i].animationBuff,typeof(AnimationClip),true);
						playerSkill.activeSkillSupport[i].speedAnimation = EditorGUILayout.Slider("Speed Animation",playerSkill.activeSkillSupport[i].speedAnimation,0,3);
						playerSkill.activeSkillSupport[i].activeTimer = EditorGUILayout.Slider("Active Timer",playerSkill.activeSkillSupport[i].activeTimer,0,0.99f);
					}else if(playerSkill.activeSkillSupport[i].skillTypeSupport == PlayerSkill.SkillTypeSupport.Heal)
					{
						playerSkill.activeSkillSupport[i].typeSkill = EditorGUILayout.TextField("Skill Type",playerSkill.activeSkillSupport[i].typeSkill);
						playerSkill.activeSkillSupport[i].unlockLevel = EditorGUILayout.IntField("Unlock At Level",playerSkill.activeSkillSupport[i].unlockLevel);
						playerSkill.activeSkillSupport[i].mpUse = EditorGUILayout.IntField("MP Use",playerSkill.activeSkillSupport[i].mpUse);
						playerSkill.activeSkillSupport[i].castTime = EditorGUILayout.FloatField("Cast Time",playerSkill.activeSkillSupport[i].castTime);
						playerSkill.activeSkillSupport[i].addValue = EditorGUILayout.FloatField("Add HP",playerSkill.activeSkillSupport[i].addValue);
						playerSkill.activeSkillSupport[i].animationBuff = (AnimationClip)EditorGUILayout.ObjectField("Animation Buff",playerSkill.activeSkillSupport[i].animationBuff,typeof(AnimationClip),true);
						playerSkill.activeSkillSupport[i].speedAnimation = EditorGUILayout.Slider("Speed Animation",playerSkill.activeSkillSupport[i].speedAnimation,0,3);
						playerSkill.activeSkillSupport[i].activeTimer = EditorGUILayout.Slider("Active Timer",playerSkill.activeSkillSupport[i].activeTimer,0,0.99f);
					}

					GUIStyle style = new GUIStyle();
					if(!UnityEditorInternal.InternalEditorUtility.HasPro())
					style.normal.textColor = Color.black;
					else
					style.normal.textColor = Color.gray;
					
					EditorGUILayout.LabelField("Description", "");
					EditorGUI.indentLevel++;
					playerSkill.activeSkillSupport[i].description = EditorGUILayout.TextArea(playerSkill.activeSkillSupport[i].description,style);
					EditorGUI.indentLevel--;

					if(playerSkill.activeSkillSupport[i].skillTypeSupport == PlayerSkill.SkillTypeSupport.Buff)
					{
						playerSkill.activeSkillSupport[i].buffFX = (GameObject)EditorGUILayout.ObjectField("Buff fx",playerSkill.activeSkillSupport[i].buffFX,typeof(GameObject),true);
						playerSkill.activeSkillSupport[i].soundFX = (AudioClip)EditorGUILayout.ObjectField("Sfx",playerSkill.activeSkillSupport[i].soundFX ,typeof(AudioClip),true);
					}else
					{
						playerSkill.activeSkillSupport[i].buffFX = (GameObject)EditorGUILayout.ObjectField("Heal fx",playerSkill.activeSkillSupport[i].buffFX,typeof(GameObject),true);
						playerSkill.activeSkillSupport[i].soundFX = (AudioClip)EditorGUILayout.ObjectField("Sfx",playerSkill.activeSkillSupport[i].soundFX ,typeof(AudioClip),true);
					}
				}
					
			}
			
		}
		
		if(GUI.changed)
			EditorUtility.SetDirty(playerSkill);
		
	}
}
