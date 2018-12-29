using UnityEngine;
using System.Collections;

public class QuestWindow : MonoBehaviour {

	[System.Serializable]
	public class GUISetting
	{
		public Vector2 position;
		public Vector2 size;
		public Texture2D texture;
	}
	
	[System.Serializable]
	public class ButtonSetting
	{
		public Vector2 position;
		public Vector2 size;
		public GUIStyle buttonStlye;
	}
	
	[System.Serializable]
	public class LabelSetting
	{
		public Vector2 position;
		public GUIStyle style;
		public bool enableStroke;
		public Color strokeColor;
	}
	
	private Vector2 defaultScreenRes; //Screen Resolution
	
	public GUISetting questWindow; //window setting
	public ButtonSetting buttonAccept,buttonCancel; //button setting
	
	public LabelSetting titleFont,detailFont,rewardLabel,itemNameFont,targetLabel,targetFont;
	
	[HideInInspector]
	public HeroController controller; //script controller
	
	public static bool enableWindow; //check enable/disable window
	public static bool enableButtonAccept; // check enable/disable accept button
	
	private Quest_Data questData;
	private Item_Data itemData;
	private Monster_Data monsterData;
	
	[Multiline]
	public static string detailQuest;
	public static string titleQuest;
	public static Vector2 condition;
	public static Vector2 reward;
	
	private int questID;
	private string itemRewardName;
	private string conditionName;
	
	// Use this for initialization
	void Start () {
		
		enableWindow = false;
		defaultScreenRes.x = 1920; //declare max screen ratio
		defaultScreenRes.y = 1080; //declare max screen ratio
		
		GameObject go = GameObject.FindGameObjectWithTag("Player");  //Find player
		controller = go.GetComponent<HeroController>();
		
		questData = GameObject.Find("QuestData").GetComponent<Quest_Data>();
		itemData = GameObject.Find("Item_Data").GetComponent<Item_Data>();
		monsterData = GameObject.Find("MonsterData").GetComponent<Monster_Data>();
	}
	
	void OnGUI()
	{
		// Resize GUI Matrix according to screen size
        ResizeGUIMatrix();
		
		if(enableWindow)
		{
			if(!controller.dontMove)
				controller.dontMove = true;
			
			//Draw window
			GUI.DrawTexture(new Rect(questWindow.position.x,questWindow.position.y,questWindow.size.x,questWindow.size.y),questWindow.texture);
		
			if(enableButtonAccept)
			{
				//Draw accept button
				if(GUI.Button(new Rect(buttonAccept.position.x,buttonAccept.position.y,buttonAccept.size.x,buttonAccept.size.y),"",buttonAccept.buttonStlye))
				{
					SoundManager.instance.PlayingSound("Accept_Quest");
					NpcSetup.resetMessageBox = true;
					NpcSetup.disableNext = false;
					MessageBox.showMessageBox = false;
					MessageBox.showNameTag = false;
					MessageBox.showFace = false;
					questData.StartQuest(questID);
					controller.dontMove = false;
					controller.dontClick = false;
					enableButtonAccept = false;
					enableWindow = false;
					
				}
			
				//Draw cancel button
				if(GUI.Button(new Rect(buttonCancel.position.x,buttonCancel.position.y,buttonCancel.size.x,buttonCancel.size.y),"",buttonCancel.buttonStlye))
				{
					NpcSetup.resetMessageBox = true;
					MessageBox.showMessageBox = false;
					MessageBox.showNameTag = false;
					MessageBox.showFace = false;
					NpcSetup.disableNext = false;
					controller.dontMove = false;
					enableButtonAccept = false;
					enableWindow = false;
				}
			}
			
			
			
			//Title Quest
			if(titleFont.enableStroke)
			TextFilter.DrawOutline(new Rect(titleFont.position.x ,titleFont.position.y, 1000 , 1000)
				,titleQuest,titleFont.style,titleFont.strokeColor,titleFont.style.normal.textColor,2f);
			else
				GUI.Label(new Rect(titleFont.position.x ,titleFont.position.y, 1000 , 1000),titleQuest,titleFont.style);
			
			//Detail Quest
			if(detailFont.enableStroke)
			TextFilter.DrawOutline(new Rect(detailFont.position.x ,detailFont.position.y, 1000 , 1000)
				,detailQuest,detailFont.style,detailFont.strokeColor,detailFont.style.normal.textColor,2f);
			else
				GUI.Label(new Rect(detailFont.position.x ,detailFont.position.y, 1000 , 1000),detailQuest,detailFont.style);
			
			//Target Label
			if(targetLabel.enableStroke)
			TextFilter.DrawOutline(new Rect(targetLabel.position.x ,targetLabel.position.y, 1000 , 1000)
				,"Target",targetLabel.style,targetLabel.strokeColor,targetLabel.style.normal.textColor,2f);
			else
				GUI.Label(new Rect(targetLabel.position.x ,targetLabel.position.y, 1000 , 1000),"Target",targetLabel.style);
			
			//Target
			if(targetFont.enableStroke)
			TextFilter.DrawOutline(new Rect(targetFont.position.x ,targetFont.position.y, 1000 , 1000)
				,condition.y.ToString() + " x " + conditionName,targetFont.style,targetFont.strokeColor,targetFont.style.normal.textColor,2f);
			else
				GUI.Label(new Rect(targetFont.position.x ,targetFont.position.y, 1000 , 1000),condition.y.ToString() + " x " + conditionName,targetFont.style);
			
			
			//Reward Label
			if(rewardLabel.enableStroke)
			TextFilter.DrawOutline(new Rect(rewardLabel.position.x ,rewardLabel.position.y, 1000 , 1000)
				,"Reward",rewardLabel.style,rewardLabel.strokeColor,rewardLabel.style.normal.textColor,2f);
			else
				GUI.Label(new Rect(rewardLabel.position.x ,rewardLabel.position.y, 1000 , 1000),"Reward",rewardLabel.style);
			
			//Reward
			if(itemNameFont.enableStroke)
			TextFilter.DrawOutline(new Rect(itemNameFont.position.x ,itemNameFont.position.y, 1000 , 1000)
				,reward.y.ToString() + " x " + itemRewardName,itemNameFont.style,itemNameFont.strokeColor,itemNameFont.style.normal.textColor,2f);
			else
				GUI.Label(new Rect(itemNameFont.position.x ,itemNameFont.position.y, 1000 , 1000),reward.y.ToString() + " x " + itemRewardName,itemNameFont.style);
			
		}
		
		// Reset matrix after finish
	        GUI.matrix = Matrix4x4.identity;
	}
	
	void ResizeGUIMatrix()
    {
       // Set matrix
       Vector2 ratio = new Vector2(Screen.width/defaultScreenRes.x , Screen.height/defaultScreenRes.y );
       Matrix4x4 guiMatrix = Matrix4x4.identity;
       guiMatrix.SetTRS(new Vector3(1, 1, 1), Quaternion.identity, new Vector3(ratio.x, ratio.y, 1));
       GUI.matrix = guiMatrix;
    }
	
	public void SetupQuestWindow(int id)
	{
		for(int i=0;i<questData.questSetting.Count;i++)
		{
			if(id == questData.questSetting[i].questID)
			{
				titleQuest = questData.questSetting[i].questName;
				detailQuest = questData.questSetting[i].questDetail;
				condition.y = questData.questSetting[i].idCondition.y;
				condition.x = questData.questSetting[i].idCondition.x;
				reward.y = questData.questSetting[i].itemIDReward.y;
				reward.x = questData.questSetting[i].itemIDReward.x;
				
				
				if(questData.questSetting[i].questCondition == Quest_Data.QuestCondition.Collect)
					ConvertItemIDToName((int)condition.x,"Condition");
				else ConvertEnemyIDToName((int)condition.x);
				
				ConvertItemIDToName((int)reward.x,"Reward");
				
				questID = id;
				
			}
		}
	}
	
	void ConvertEnemyIDToName(int enemyID)
	{
		//Check Monster Id
		for(int i=0;i < monsterData.monsterList.Count;i++)
		{
			if(enemyID == monsterData.monsterList[i].enemyID)
			{
				conditionName = monsterData.monsterList[i].enemyStatus.enemyName;
				
				break;
			}
		}
	}
	
	void ConvertItemIDToName(int itemID,string type)
	{
		//Check Usable Item
		for(int i=0;i < itemData.item_usable_set.Count;i++)
		{
			if(itemID == itemData.item_usable_set[i].item_ID)
			{
				if(type == "Condition")
				{
					conditionName = itemData.item_usable_set[i].item_Name;
				}	
				else if(type == "Reward")
				{
					itemRewardName = itemData.item_usable_set[i].item_Name;
				}
				
				break;
			}
		}
		
		//Check Weapon
		for(int i=0;i < itemData.item_equipment_set.Count;i++)
		{
			if(itemID == itemData.item_equipment_set[i].item_ID)
			{
				if(type == "Condition")
				{
					conditionName = itemData.item_equipment_set[i].item_Name;
				}	
				else if(type == "Reward")
				{
					itemRewardName = itemData.item_equipment_set[i].item_Name;
				}
				
				break;
			}
		}
		
		//Check Etc Item
		
		for(int i=0;i < itemData.item_etc_set.Count;i++)
		{
			if(itemID == itemData.item_etc_set[i].item_ID)
			{
				if(type == "Condition")
				{
					conditionName = itemData.item_etc_set[i].item_Name;
				}	
				else if(type == "Reward")
				{
					itemRewardName = itemData.item_etc_set[i].item_Name;
				}
				
				break;
			}
		}
		
		//Check Gold
		
		for(int i=0;i < itemData.item_gold.Length;i++)
		{
			if(itemID == itemData.item_gold[i].item_ID)
			{
				if(type == "Condition")
				{
					conditionName = itemData.item_gold[i].item_Name;
				}	
				else if(type == "Reward")
				{
					itemRewardName = itemData.item_gold[i].item_Name;
				}
				
				break;
			}
		}
	}
	
}
