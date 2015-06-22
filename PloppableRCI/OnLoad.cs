using ColossalFramework;
using ColossalFramework.Globalization;
using ColossalFramework.Math;
using ColossalFramework.Plugins;
using System;
using UnityEngine;
using ICities;
using ColossalFramework.UI;

namespace PloppableAI
{
	
	public class MakeInfos : LoadingExtensionBase
	{
		public PloppableTool PloppableTool;
		public override void OnCreated(ILoading loading)
		{
			GenerateInfos();
		}


		public override void OnLevelLoaded(LoadMode mode)
		{ 	
			base.OnLevelLoaded (mode);

			if (mode == LoadMode.LoadGame || mode == LoadMode.NewGame) 
			{
				GenerateInfos ();

				PloppableTool = GameObject.FindObjectOfType<PloppableTool>();
				if(PloppableTool == null)
				{
					GameObject gameController = GameObject.FindWithTag("GameController");
					PloppableTool = gameController.AddComponent<PloppableTool>();
				}
				PloppableTool.InitGui();
				PloppableTool.enabled = false;
			}
		}

		
		public override void OnLevelUnloading ()
		{
			base.OnLevelUnloading ();
		}


		public void SetThings(BuildingInfo original, BuildingInfo newone){

			//This changes some settings in the newly instanceated BuildingInfos. 

			newone.m_buildingAI = original.m_buildingAI;
			newone.m_AssetEditorTemplate = false;
			newone.m_prefabInitialized = false;
			newone.m_instanceChanged = true;
			newone.m_autoRemove = false;
		}
			

		public void GenerateInfos()
		{
			//The BuildingInfos are generated fresh at every scene load from the list of installed assets
			//This Looks though all BuildingInfos, and finds ones that have the Custom AI's assigned. 
			//When It finds one, it makes 5 instances and adds the level to the name. 
			//We need 5 new BuildingInfo objects so we can change the levels on each one. 
			//We want to leave the original one alone. 
			//It then initalizes the new BuildingInfos so they can be assigned to Building objects via the custom AI simulation steps. 

			int num3 = PrefabCollection<BuildingInfo>.LoadedCount(); //grab the number of BuidingInfos

			for (int num = 1; num <= num3; num++) { //loop though them all


				string namer = PrefabCollection<BuildingInfo>.PrefabName ((uint)num);
				BuildingInfo Holder = PrefabCollection<BuildingInfo>.FindLoaded (namer);

				if (Holder.m_buildingAI is PloppableResidential)
					//|| Holder.m_buildingAI is PloppableOffice) { //If one has a custom AI assigned, then make the insances
				{
					BuildingInfo Level1 = BuildingInfo.Instantiate (Holder);
					Level1.name = Holder.name + "_Level1";
					this.SetThings (Holder, Level1);

					BuildingInfo Level2 = BuildingInfo.Instantiate (Holder);
					Level2.name = Holder.name + "_Level2";
					this.SetThings (Holder, Level2);

					BuildingInfo Level3 = BuildingInfo.Instantiate (Holder);
					Level3.name = Holder.name + "_Level3";
					this.SetThings (Holder, Level3);

					BuildingInfo Level4 = BuildingInfo.Instantiate (Holder);
					Level4.name = Holder.name + "_Level4";
					this.SetThings (Holder, Level4);

					BuildingInfo Level5 = BuildingInfo.Instantiate (Holder);
					Level5.name = Holder.name + "_Level5";
					this.SetThings (Holder, Level5);

					BuildingInfo[] bray = new BuildingInfo[] { Level1, Level2, Level3, Level4, Level5 };
					string[] stra = new string[] { Level1.name, Level2.name, Level3.name, Level4.name, Level5.name };

					PrefabCollection<BuildingInfo>.InitializePrefabs ("BuildingInfo", bray, stra); //initlaize the instances so they can be referenced by the Buliding objects. 
					PrefabCollection<BuildingInfo>.BindPrefabs ();

				}
			}
		}
	}
}