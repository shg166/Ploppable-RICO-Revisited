using ColossalFramework.UI;
using ICities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ColossalFramework.IO;
using ColossalFramework.Packaging;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;

namespace PloppableRICO
{
	public class ExtendedLoading : LoadingExtensionBase
	{
		public PloppableTool PloppableTool;
		System.Collections.Generic.List<string[]> BNames = new System.Collections.Generic.List<string[]>{ };
		static GameObject buildingWindowGameObject;
		BuildingInfoWindow5 buildingWindow;
		ServiceInfoWindow serviceWindow;
		Sub_BuildingsEnabler sub = new Sub_BuildingsEnabler ();
		private LoadMode _mode;

		public override void OnLevelLoaded (LoadMode mode)
		{
			base.OnLevelLoaded (mode);

			if (mode == LoadMode.NewAsset || mode == LoadMode.LoadAsset)
				return;

			sub.Run (BNames); // Boformers Sub-Building Enabler


			/////////////////////////////This code is the foundation of EMF's Extended Building Information Mod
	
			if (mode != LoadMode.LoadGame && mode != LoadMode.NewGame)
				return;
			_mode = mode;

			buildingWindowGameObject = new GameObject ("buildingWindowObject");

			var buildingInfo = UIView.Find<UIPanel> ("(Library) ZonedBuildingWorldInfoPanel");
			this.buildingWindow = buildingWindowGameObject.AddComponent<BuildingInfoWindow5> ();
			this.buildingWindow.transform.parent = buildingInfo.transform;
			this.buildingWindow.size = new Vector3 (buildingInfo.size.x, buildingInfo.size.y);
			this.buildingWindow.baseBuildingWindow = buildingInfo.gameObject.transform.GetComponentInChildren<ZonedBuildingWorldInfoPanel> ();
			this.buildingWindow.position = new Vector3 (0, 12);
			buildingInfo.eventVisibilityChanged += buildingInfo_eventVisibilityChanged;


			var serviceBuildingInfo = UIView.Find<UIPanel> ("(Library) CityServiceWorldInfoPanel");
			serviceWindow = buildingWindowGameObject.AddComponent<ServiceInfoWindow> (); 
			serviceWindow.servicePanel = serviceBuildingInfo.gameObject.transform.GetComponentInChildren<CityServiceWorldInfoPanel> ();
	
			serviceBuildingInfo.eventVisibilityChanged += serviceBuildingInfo_eventVisibilityChanged;

			///////////////////////////END

			///////////////This IS the TOOL
			//PloppableTool = GameObject.FindObjectOfType<PloppableTool>();

			try {
				if (PloppableTool == null) {
					GameObject gameController = GameObject.FindWithTag ("GameController");
					PloppableTool = gameController.AddComponent<PloppableTool> ();
					PloppableTool.name = "PloppableTool";
					PloppableTool.InitGui (BNames);
					PloppableTool.enabled = false;
					GameObject.FindObjectOfType<ToolController> ().Tools [0].enabled = true;
				}
					
			} catch (Exception e) {
				Debug.Log (e.ToString ());
			}
		}

	
		private void serviceBuildingInfo_eventVisibilityChanged (UIComponent component, bool value)
		{
			serviceWindow.Update ();
		}

		void buildingInfo_eventVisibilityChanged (UIComponent component, bool value)
		{
			this.buildingWindow.isEnabled = value;
			if (value) {
				this.buildingWindow.Show ();
			} else {
				this.buildingWindow.Hide ();
			}
		}

		public override void OnLevelUnloading ()
		{
			if (_mode != LoadMode.LoadGame && _mode != LoadMode.NewGame)
				return;

			if (buildingWindowGameObject != null) {
				GameObject.Destroy (buildingWindowGameObject);
			}
		}
			
	}
}
