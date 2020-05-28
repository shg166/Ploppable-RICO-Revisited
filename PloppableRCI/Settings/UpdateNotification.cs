﻿using System;
using UnityEngine;
using ColossalFramework.UI;


namespace PloppableRICO
{
    /// <summary>
    /// Base class of the update notification panel.
    /// </summary>
    public class UpdateNotification : UIPanel
    {
        // Constants.
        private const float panelWidth = 600;
        private const float panelHeight = 300;
        private const float spacing = 10;

        // Instance references.
        private static GameObject uiGameObject;
        private static UpdateNotification _instance;
        public static UpdateNotification instance { get { return _instance; } }


        /// <summary>
        /// Creates the panel object in-game.
        /// </summary>
        public void Create()
        {
            try
            {
                // Destroy existing (if any) instances.
                uiGameObject = GameObject.Find("PloppableRICOUpgradeNotification");
                if (uiGameObject != null)
                {
                    UnityEngine.Debug.Log("RICO Revisited: found existing upgrade notification instance.");
                    GameObject.Destroy(uiGameObject);
                }

                // Create new instance.
                // Give it a unique name for easy finding with ModTools.
                uiGameObject = new GameObject("PloppableRICOUpgradeNotification");
                uiGameObject.transform.parent = UIView.GetAView().transform;
                _instance = uiGameObject.AddComponent<UpdateNotification>();
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogException(e);
            }
        }


        /// <summary>
        /// Create the update notification panel; called by Unity just before any of the Update methods is called for the first time.
        /// </summary>
        public override void Start()
        {
            base.Start();

            try
            {
                // Basic setup.
                isVisible = true;
                canFocus = true;
                isInteractive = true;
                width = panelWidth;
                height = panelHeight;
                relativePosition = new Vector3(Mathf.Floor((GetUIView().fixedWidth - width) / 2), Mathf.Floor((GetUIView().fixedHeight - height) / 2));
                backgroundSprite = "UnlockingPanel2";

                // Put behind other things.
                zOrder = 2;

                // Title.
                UILabel title = this.AddUIComponent<UILabel>();
                title.relativePosition = new Vector3(0, spacing);
                title.textAlignment = UIHorizontalAlignment.Center;
                title.text = "Plopplable RICO Revisited 2.1 update";
                title.textScale = 1.0f;
                title.autoSize = false;
                title.width = this.width;

                // Note 1.
                UILabel note1 = this.AddUIComponent<UILabel>();
                note1.relativePosition = new Vector3(spacing, 40);
                note1.textAlignment = UIHorizontalAlignment.Left;
                note1.text = Translations.GetTranslation("Ploppable RICO Revisited has been updated to version 2.1.  Some key features of this update are:");
                note1.textScale = 0.8f;
                note1.autoSize = false;
                note1.autoHeight = true;
                note1.width = this.width - (spacing * 2);
                note1.wordWrap = true;

                // Note 2.
                UILabel note2 = this.AddUIComponent<UILabel>();
                note2.relativePosition = new Vector3(spacing *2, 40 + note1.height + spacing);
                note2.textAlignment = UIHorizontalAlignment.Left;
                note2.text = Translations.GetTranslation("New mod options panel (accessed via game options), including option for plain thumbnail backgrounds.\r\n\r\nAdjusted lighting of thumnail image renders to help users with over-saturated map themes.\r\n\r\nLocal RICO settings created from existing growable buildings will be growable by default and inherit the default household/workplace counts of the original.\r\n\r\nAdditional failsafes to reduce risk of residential building household counts being reduced on game load if your city is close to hitting internal game limits.");
                note2.textScale = 0.8f;
                note2.autoSize = false;
                note2.autoHeight = true;
                note2.width = this.width - (spacing * 4);
                note2.wordWrap = true;

                // Auto resize panel to accomodate note.
                this.height = 80 + note1.height + note2.height + (spacing * 3);

                // Close button.
                UIButton closeButton = UIUtils.CreateButton(this);
                closeButton.width = 200;
                closeButton.relativePosition = new Vector3(spacing, this.height - closeButton.height - spacing);
                closeButton.text = Translations.GetTranslation("Close");
                closeButton.Enable();

                // Event handler.
                closeButton.eventClick += (c, p) =>
                {
                    // Just hide this panel and destroy the game object - nothing more to do this load.
                    this.Hide();
                    GameObject.Destroy(uiGameObject);
                };

                // "Don't show again" button.
                UIButton noShowButton = UIUtils.CreateButton(this);
                noShowButton.width = 200;
                noShowButton.relativePosition = new Vector3(this.width - noShowButton.width - spacing, this.height - closeButton.height - spacing);
                noShowButton.text = Translations.GetTranslation("Don't show again");
                noShowButton.Enable();

                // Event handler.
                noShowButton.eventClick += (c, p) =>
                {
                    // Update and save settings file.
                    Loading.settingsFile.NotificationVersion = 2;
                    Configuration<SettingsFile>.Save();

                    // Just hide this panel and destroy the game object - nothing more to do.
                    this.Hide();
                    GameObject.Destroy(uiGameObject);
                };
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogException(e);
            }
        }
    }
}