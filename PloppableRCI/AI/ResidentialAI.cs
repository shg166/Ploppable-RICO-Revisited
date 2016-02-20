using ColossalFramework;
using ColossalFramework.Globalization;
using ColossalFramework.Math;
using ColossalFramework.Plugins;
using System;
using UnityEngine;
using ICities;

namespace PloppableRICO
{

	public class PloppableResidential : ResidentialBuildingAI
	{
		public int m_constructionCost = 1;
		public int m_homeCount = 1;
		public int m_levelmin = 1;
		public int m_levelmax = 1;

		public override void GetWidthRange (out int minWidth, out int maxWidth)
		{
			minWidth = 1;
			maxWidth = 16;
		}

		public override void GetLengthRange (out int minLength, out int maxLength)
		{

			minLength = 1;
			maxLength = 16;
		}

		public override string GenerateName(ushort buildingID, InstanceID caller)
        { 
			return base.m_info.GetUncheckedLocalizedTitle();
		}
			
		public override bool ClearOccupiedZoning (){
			return true;
		}

		public override int GetConstructionCost()
		{
			int result = (m_constructionCost * 100);
			Singleton<EconomyManager>.instance.m_EconomyWrapper.OnGetConstructionCost(ref result, this.m_info.m_class.m_service, this.m_info.m_class.m_subService, this.m_info.m_class.m_level);
			return result;
		}

		public override int CalculateHomeCount (Randomizer r, int width, int length)
		{
			width = m_homeCount;
			length = 1;
			int num = 100;
			return Mathf.Max(100, width * length * num + r.Int32(100u)) / 100;
		}

		public override void SimulationStep (ushort buildingID, ref Building buildingData)
		{
			buildingData.m_levelUpProgress = 0;
			buildingData.m_flags &= ~Building.Flags.ZonesUpdated;
			buildingData.m_flags &= ~Building.Flags.Abandoned;
			buildingData.m_flags &= ~Building.Flags.Demolishing;

			buildingData.m_garbageBuffer = 0;
			buildingData.m_fireHazard = 0;
			buildingData.m_fireIntensity = 0;
			buildingData.m_majorProblemTimer = 0;

			base.SimulationStep(buildingID, ref buildingData);

			buildingData.m_flags &= ~Building.Flags.ZonesUpdated;
			buildingData.m_flags &= ~Building.Flags.Abandoned;
			buildingData.m_flags &= ~Building.Flags.Demolishing;
			buildingData.m_levelUpProgress = 0;
			buildingData.m_majorProblemTimer = 0;
		}

		protected override void SimulationStepActive(ushort buildingID, ref Building buildingData, ref Building.Frame frameData){

			buildingData.m_flags &= ~Building.Flags.ZonesUpdated;
			buildingData.m_flags &= ~Building.Flags.Abandoned;
			buildingData.m_flags &= ~Building.Flags.Demolishing;
			buildingData.m_levelUpProgress = 0;
			buildingData.m_majorProblemTimer = 0;

			base.SimulationStepActive(buildingID, ref buildingData, ref frameData);

			buildingData.m_levelUpProgress = 0;
			buildingData.m_flags &= ~Building.Flags.ZonesUpdated;
			buildingData.m_flags &= ~Building.Flags.Abandoned;
			buildingData.m_flags &= ~Building.Flags.Demolishing;
			buildingData.m_majorProblemTimer = 0;

			}

		public override BuildingInfo GetUpgradeInfo(ushort buildingID, ref Building data){
			
			return null; //this will cause a check to fail in CheckBuildingLevel, and prevent the building form leveling. 
		}
	}
}