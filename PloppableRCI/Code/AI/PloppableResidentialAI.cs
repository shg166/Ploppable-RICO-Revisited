// <copyright file="PloppableResidentialAI.cs" company="algernon (K. Algernon A. Sheppard)">
// Copyright (c) algernon (K. Algernon A. Sheppard). All rights reserved.
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace PloppableRICO
{
    using ColossalFramework;

    /// <summary>
    /// Replacement for Residential AI for ploppable RICO buildings.
    /// </summary>
    public class PloppableResidentialAI : GrowableResidentialAI
    {
        /// <summary>
        /// Gets the construction cost of the building.
        /// </summary>
        /// <returns>Construction cost.</returns>
        public override int GetConstructionCost()
        {
            int baseCost;

            if (ModSettings.overrideCost)
            {
                // Cost is multiplied by 100 before feeding into the EconomyManager.
                int costMultiplier = 100 + (ModSettings.costMultResLevel * (int)this.m_info.m_class.m_level);
                baseCost = ModSettings.costPerHousehold * CalculateHomeCount(this.m_info.m_class.m_level, default, this.m_info.GetWidth(), this.m_info.GetLength()) * costMultiplier;
            }
            else
            {
                 baseCost = m_constructionCost * 100;
            }

            Singleton<EconomyManager>.instance.m_EconomyWrapper.OnGetConstructionCost(ref baseCost, this.m_info.m_class.m_service, this.m_info.m_class.m_subService, this.m_info.m_class.m_level);
            return baseCost;
        }

        /// <summary>
        /// Returns the acceptable width for this class of building AI.
        /// For ploppable RICO buildings  minimum is always 1 and maximum is always 16.
        /// </summary>
        /// <param name="minWidth">Minimum building width (always 1).</param>
        /// <param name="maxWidth">Maximum building width (always 16).</param>
        public override void GetWidthRange(out int minWidth, out int maxWidth)
        {
            minWidth = 1;
            maxWidth = 16;
        }

        /// <summary>
        /// Returns the acceptable length for this class of building AI.
        /// For ploppable RICO buildings  minimum is always 1 and maximum is always 16.
        /// </summary>
        /// <param name="minLength">Minimum building length (always 1).</param>
        /// <param name="maxLength">Maximum building width (always 16).</param>
        public override void GetLengthRange(out int minLength, out int maxLength)
        {
            minLength = 1;
            maxLength = 16;
        }

        /// <summary>
        /// Returns the name for the building.
        /// For ploppable RICO buildings this is always the base name of the prefab (no autogenerated names).
        /// </summary>
        /// <param name="buildingID">Instance ID of the building (unused).</param>
        /// <param name="caller">Calling instance (unused).</param>
        /// <returns>Bulding name.</returns>
        public override string GenerateName(ushort buildingID, InstanceID caller)
        {
            return m_info.GetUncheckedLocalizedTitle();
        }

        /// <summary>
        /// Returns whether or not the building clears any zoning it's placed on.
        /// For ploppable RICO buildings this is always true.
        /// </summary>
        /// <returns>Whether this building clears away zoning (always true).</returns>
        public override bool ClearOccupiedZoning()
        {
            return true;
        }

        /// <summary>
        /// Determines what building (if any) this will upgrade to.
        /// For ploppable RICO buildings, this is always null.
        /// That causes a check to fail in CheckBuildingLevel and prevents the building from upgrading.
        /// </summary>
        /// <param name="buildingID">Instance ID of the original building.</param>
        /// <param name="data">Building data struct.</param>
        /// <returns>The BuildingInfo record of the building to upgrade to (always null).</returns>
        public override BuildingInfo GetUpgradeInfo(ushort buildingID, ref Building data)
        {
            return null;
        }

        /// <summary>
        /// Calculations performed on each simulation step.
        /// For a ploppable RICO building we want to force certain building flags to be set before and after each step.
        /// </summary>
        /// <param name="buildingID">Instance ID of the building.</param>
        /// <param name="buildingData">Building data struct.</param>
        /// <param name="frameData">Frame data.</param>
        public override void SimulationStep(ushort buildingID, ref Building buildingData, ref Building.Frame frameData)
        {
            // Apply flags.
            AIUtils.SetBuildingFlags(ref buildingData);

            // Execute base method.
            base.SimulationStep(buildingID, ref buildingData, ref frameData);

            // Ensure flags are still applied.
            AIUtils.SetBuildingFlags(ref buildingData);
        }

        /// <summary>
        /// Returns the construction time of the building.
        /// For ploppable RICO buildings this is always zero.
        /// </summary>
        /// <returns>Construction time (always 0).</returns>
        protected override int GetConstructionTime()
        {
            return 0;
        }

        /// <summary>
        /// Calculations performed on each simulation step.
        /// For a ploppable RICO building we want to force certain building flags to be set before and after each step.
        /// </summary>
        /// <param name="buildingID">Instance ID of the building.</param>
        /// <param name="buildingData">Building data struct.</param>
        /// <param name="frameData">Frame data.</param>
        protected override void SimulationStepActive(ushort buildingID, ref Building buildingData, ref Building.Frame frameData)
        {
            // Apply flags.
            AIUtils.SetBuildingFlags(ref buildingData);

            // Execute base method.
            base.SimulationStepActive(buildingID, ref buildingData, ref frameData);

            // Ensure flags are still applied.
            AIUtils.SetBuildingFlags(ref buildingData);
        }
    }
}