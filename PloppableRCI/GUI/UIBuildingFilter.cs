﻿using UnityEngine;
using ColossalFramework.UI;

namespace PloppableRICO
{
    public class UIBuildingFilter : UIPanel
    {
        private const int NumOfCategories = 10;
        public UICheckBox[] zoningToggles;
        public UIButton allZones;
        public UIButton noZones;
        public UITextField nameFilter;
        public UICheckBox[] settingsFilter;

        public bool IsZoneSelected(Category zone)
        {
            return zoningToggles[(int)zone].isChecked;
        }

        public bool IsAllZoneSelected()
        {
            return zoningToggles[(int)Category.Monument].isChecked &&
                zoningToggles[(int)Category.Beautification].isChecked &&
                zoningToggles[(int)Category.Education].isChecked &&
                zoningToggles[(int)Category.Power].isChecked &&
                zoningToggles[(int)Category.Water].isChecked &&
                zoningToggles[(int)Category.Health].isChecked&&
                zoningToggles[(int)Category.Residential].isChecked &&
                zoningToggles[(int)Category.Commercial].isChecked &&
                zoningToggles[(int)Category.Office].isChecked &&
                zoningToggles[(int)Category.Industrial].isChecked;
        }


        public string buildingName
        {
            get { return nameFilter.text.Trim(); }
        }

        public event PropertyChangedEventHandler<int> eventFilteringChanged;

        public override void Start()
        {
            base.Start();

            // Zoning
            zoningToggles = new UICheckBox[NumOfCategories];
            for (int i = 0; i < NumOfCategories; i++)
            {
                zoningToggles[i] = UIUtils.CreateIconToggle(this, CategoryIcons.atlases[i], CategoryIcons.spriteNames[i], CategoryIcons.spriteNames[i] + "Disabled");
                zoningToggles[i].tooltip = CategoryIcons.tooltips[i];
                zoningToggles[i].relativePosition = new Vector3(40 * i, 0);
                zoningToggles[i].isChecked = true;
                zoningToggles[i].readOnly = true;
                zoningToggles[i].checkedBoxObject.isInteractive = false; // Don't eat my double click event please

                zoningToggles[i].eventClick += (c, p) =>
                {
                    ((UICheckBox)c).isChecked = !((UICheckBox)c).isChecked;
                    eventFilteringChanged(this, 0);
                };

                zoningToggles[i].eventDoubleClick += (c, p) =>
                {
                    for (int j = 0; j < NumOfCategories; j++)
                        zoningToggles[j].isChecked = false;
                    ((UICheckBox)c).isChecked = true;

                    eventFilteringChanged(this, 0);
                };
            }

            allZones = UIUtils.CreateButton(this);
            allZones.width = 55;
            allZones.text = Translations.GetTranslation("All");
            allZones.relativePosition = new Vector3(405, 5);

            allZones.eventClick += (c, p) =>
            {
                for (int i = 0; i < NumOfCategories; i++)
                {
                    zoningToggles[i].isChecked = true;
                }
                eventFilteringChanged(this, 0);
            };

            noZones = UIUtils.CreateButton(this);
            noZones.width = 55;
            noZones.text = Translations.GetTranslation("None");
            noZones.relativePosition = new Vector3(465, 5);

            noZones.eventClick += (c, p) =>
            {
                for (int i = 0; i < NumOfCategories; i++)
                {
                    zoningToggles[i].isChecked = false;
                }
                eventFilteringChanged(this, 0);
            };

            // Name filter
            UILabel nameLabel = AddUIComponent<UILabel>();
            nameLabel.textScale = 0.8f;
            nameLabel.padding = new RectOffset(0, 0, 8, 0);
            nameLabel.relativePosition = new Vector3(width - 250, 0);
            nameLabel.text = Translations.GetTranslation("Name") + ": ";

            nameFilter = UIUtils.CreateTextField(this);
            nameFilter.width = 200;
            nameFilter.height = 30;
            nameFilter.padding = new RectOffset(6, 6, 6, 6);
            nameFilter.relativePosition = new Vector3(width - nameFilter.width, 0);

            nameFilter.eventTextChanged += (c, s) => eventFilteringChanged(this, 5);
            nameFilter.eventTextSubmitted += (c, s) => eventFilteringChanged(this, 5);

            // Create settings filters.
            UILabel filterLabel = this.AddUIComponent<UILabel>();
            filterLabel.textScale = 0.8f;
            filterLabel.text = Translations.GetTranslation("Settings filter: Mod/Author/Local/Any");
            filterLabel.relativePosition = new Vector3(10, 50, 0);

            // Setting filter checkboxes.
            settingsFilter = new UICheckBox[4];
            for (int i = 0; i < 4; i++)
            {
                settingsFilter[i] = this.AddUIComponent<UICheckBox>();

                settingsFilter[i].width = 20f;
                settingsFilter[i].height = 20f;
                settingsFilter[i].clipChildren = true;
                settingsFilter[i].relativePosition = new Vector3(280 + (30 * i), 45f);

                UISprite sprite = settingsFilter[i].AddUIComponent<UISprite>();
                sprite.spriteName = "ToggleBase";
                sprite.size = new Vector2(20f, 20f);
                sprite.relativePosition = Vector3.zero;

                settingsFilter[i].checkedBoxObject = sprite.AddUIComponent<UISprite>();
                ((UISprite)settingsFilter[i].checkedBoxObject).spriteName = "ToggleBaseFocused";
                settingsFilter[i].checkedBoxObject.size = new Vector2(20f, 20f);
                settingsFilter[i].checkedBoxObject.relativePosition = Vector3.zero;

                // Special event handling for 'any' checkbox.
                if (i == 3)
                {
                    settingsFilter[i].eventCheckChanged += (c, state) =>
                    {
                        if (state)
                        {
                            // Unselect all other checkboxes if 'any' is checked.
                            settingsFilter[0].isChecked = false;
                            settingsFilter[1].isChecked = false;
                            settingsFilter[2].isChecked = false;
                        }
                    };
                }
                else
                {
                    // Non-'any' checkboxes.
                    // Unselect 'any' checkbox if any other is checked.
                    settingsFilter[i].eventCheckChanged += (c, state) =>
                    {
                        if (state) settingsFilter[3].isChecked = false;
                    };
                }

                // Trigger filtering changed event if any checkbox is changed.
                settingsFilter[i].eventCheckChanged += (c, state) => { eventFilteringChanged(this, 0); };
            }

            // Settings filter tooltips.
            settingsFilter[0].tooltip = Translations.GetTranslation("Mod settings");
            settingsFilter[1].tooltip = Translations.GetTranslation("Author settings");
            settingsFilter[2].tooltip = Translations.GetTranslation("Local settings");
            settingsFilter[3].tooltip = Translations.GetTranslation("Any settings");
        }
    }
}