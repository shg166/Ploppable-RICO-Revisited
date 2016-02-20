using ColossalFramework.UI;
using ICities;
using System;
using System.Collections.Generic;
using System.Linq;
using ColossalFramework;
using ColossalFramework.Globalization;
using ColossalFramework.Math;
using ColossalFramework.Plugins;
using UnityEngine;

namespace PloppableRICO
{
	public class Loading : LoadingExtensionBase
	{
		private PloppableTool _ploppableTool;
		private bool _initialized = false;


		public override void OnLevelLoaded (LoadMode mode)
		{
			base.OnLevelLoaded (mode);

			// Only load if it's a game
			if (mode != LoadMode.LoadGame && mode != LoadMode.NewGame)
				return;

			// Load XML settings and apply them to prefabs. Based on the Sub-Buildings Enabler mod.
			var prefabImporter = new PrefabImporter ();
			var prefabToCategoryMap = prefabImporter.Run ();



			// Load the RICO panel. This is based on the Terraform Tool mod. 

			try {
				if (_ploppableTool == null) {
					GameObject gameController = GameObject.FindWithTag ("GameController");
					_ploppableTool = gameController.AddComponent<PloppableTool> ();
					_ploppableTool.name = "PloppableTool";
					_ploppableTool.DrawPloppableTool ();
					_ploppableTool.PopulateAssets(prefabToCategoryMap);
					_ploppableTool.enabled = false;
					GameObject.FindObjectOfType<ToolController> ().Tools [0].enabled = true;
				}
			} catch (Exception e) {
				Debug.Log (e.ToString ());
			}

			Detour.BuildingToolDetour.Deploy ();


			_initialized = true;

			MonumentsPanel[] monpan = GameObject.FindObjectsOfType<MonumentsPanel> ();

			foreach (MonumentsPanel panel in monpan) {
				//panel.CleanPanel ();
				panel.RefreshPanel();
				Debug.Log (panel.name + " Updated");
			}
		
		}

		public override void OnLevelUnloading ()
		{
			base.OnLevelUnloading ();

			//if (!_initialized)
			//	return;

			// RICO ploppables need a non private item class assigned to pass though the game reload. 
			for (uint i = 0; i < PrefabCollection<BuildingInfo>.LoadedCount (); i++) {
				
				var prefab = PrefabCollection<BuildingInfo>.GetLoaded (i);

				if (prefab.m_buildingAI is PloppableRICO.PloppableExtractor
				    || prefab.m_buildingAI  is PloppableResidential
				    || prefab.m_buildingAI  is PloppableOffice ||
				    prefab.m_buildingAI is PloppableCommercial ||
				    prefab.m_buildingAI  is PloppableIndustrial) {

					// Just assign any RICO prefab a ploppable ItemClass so it will reload. It gets set back once the mod loads. 
					prefab.m_class = ItemClassCollection.FindClass ("Beautification Item"); 

					prefab.InitializePrefab ();
				}
			}
		}

		public override void OnReleased ()
		{
			base.OnReleased ();

			Detour.BuildingToolDetour.Revert ();

		}
	}
}
