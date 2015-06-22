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
	public class PloppableExperiment : IUserMod 
	{

		public string Name 
		{
			get { return "PloppableRCI"; }
		}

		public string Description 
		{
			get { return "Adds Ploppable RCI"; }
		}
	}

	//This is my current attempts at making the UI button with some code copied from various mods. Still learning how it all works. 

	public class PloppableTool : ToolBase
	{
		public UIButton mainButton;
		public UIScrollablePanel BuildingPanel;
		public UITabstrip RCITabs;

		private bool m_active;

		protected override void Awake()
		{
			//this.m_dataLock = new object();
			m_active = false;
			base.Awake();
		}

		public void InitGui () {

			var bulldozeButton = UIView.GetAView().FindUIComponent<UIMultiStateButton>("RCIButton");

			mainButton = bulldozeButton.parent.AddUIComponent<UIButton>();
			mainButton.name = "Ploppable RCI";
			mainButton.size = new Vector2(36, 36);
			mainButton.relativePosition = new Vector2
				(
					bulldozeButton.relativePosition.x + bulldozeButton.width / 2.0f - mainButton.width - bulldozeButton.width,
					bulldozeButton.relativePosition.y + bulldozeButton.height / 2.0f - mainButton.height / 2.0f
				);
			mainButton.normalBgSprite = "ZoningOptionMarquee";
			mainButton.focusedFgSprite = "ToolbarIconGroup6Focused";
			mainButton.hoveredFgSprite = "ToolbarIconGroup6Hovered";
			mainButton.eventClick += buttonClicked;

			BuildingPanel = UIView.GetAView().FindUIComponent("TSBar").AddUIComponent<UIScrollablePanel>();
			//RCITabs = UIView.GetView().FindUIComponent<UITabstrip>("m

			BuildingPanel.backgroundSprite = "SubcategoriesPanel";
			BuildingPanel.isVisible = false;
			BuildingPanel.name = "PloppableBuildingsList";
			BuildingPanel.size = new Vector2(150, 400);

			BuildingPanel.relativePosition = new Vector2
				(
					bulldozeButton.relativePosition.x + bulldozeButton.width / 2.0f -BuildingPanel.width ,
					bulldozeButton.relativePosition.y - BuildingPanel.height 
				);
			//= new Vector3(2.0f, 2.0f, 0.0f);
			BuildingPanel.isVisible = false;

			UIButton BuildingB = BuildingPanel.AddUIComponent<UIButton> ();

			BuildingInfo b = PrefabCollection<BuildingInfo>.FindLoaded ("Pittsfield Building_Data");

			BuildingB.name = "Pittsfield";
			BuildingB.text = name;

			UILabel l = BuildingPanel.AddUIComponent<UILabel> ();
			l.text = "HEY";

			BuildingB.size = new Vector2(36, 36);
			//BuildingB.normalBgSprite = "PolicyBarBack";
			//BuildingB.atlas = b.m_Atlas;

			//BuildingB.relativePosition = new Vector3(2f, 2f, 0f);
			BuildingB.foregroundSpriteMode = UIForegroundSpriteMode.Fill;
			BuildingB.normalFgSprite = "ThumbnailBuildingDefault";
			BuildingB.focusedFgSprite = "ThumbnailBuildingDefault" + "Focused";
			BuildingB.hoveredFgSprite = "ThumbnailBuildingDefault" + "Hovered";
			BuildingB.pressedFgSprite = "ThumbnailBuildingDefault" + "Pressed";
			BuildingB.disabledFgSprite = "ThumbnailBuildingDefault" + "Disabled";
		}

		void buttonClicked(UIComponent component, UIMouseEventParameter eventParam)
		{
			this.enabled = true;
			BuildingPanel.isVisible = true;
		}
		protected override void OnDisable()
		{
			BuildingPanel.isVisible = false;
			if (BuildingPanel != null)
				BuildingPanel.isVisible = false;
			base.OnDisable();
		}
		protected override void OnEnable()
		{
			UIView.GetAView().FindUIComponent<UITabstrip>("MainToolstrip").selectedIndex = -1;
			base.OnEnable();
		}


	}
}

 
 u s i n g   U n i t y E n g i n e ; 
 
 u s i n g   I C i t i e s ; 
 
 u s i n g   C o l o s s a l F r a m e w o r k . U I ; 
 
 
 
 n a m e s p a c e   P l o p p a b l e A I 
 
 { 
 
 	 p u b l i c   c l a s s   P l o p p a b l e E x p e r i m e n t   :   I U s e r M o d   
 
 	 { 
 
 	 	 
 
 	 	 p u b l i c   s t r i n g   N a m e   
 
 	 	 { 
 
 	 	 	 g e t   {   r e t u r n   " P l o p p a b l e R C I " ;   } 
 
 	 	 } 
 
 
 
 	 	 p u b l i c   s t r i n g   D e s c r i p t i o n   
 
 	 	 { 
 
 	 	 	 g e t   {   r e t u r n   " A d d s   P l o p p a b l e   R C I " ;   } 
 
 	 	 } 
 
 	 } 
 
 
 
 	 / / T h i s   i s   m y   c u r r e n t   a t t e m p t s   a t   m a k i n g   t h e   U I   b u t t o n   w i t h   s o m e   c o d e   c o p i e d   f r o m   v a r i o u s   m o d s .   S t i l l   l e a r n i n g   h o w   i t   a l l   w o r k s .   
 
 
 
 	 p u b l i c   c l a s s   P l o p p a b l e T o o l   :   T o o l B a s e 
 
 	 { 
 
 	 	 p u b l i c   U I B u t t o n   m a i n B u t t o n ; 
 
 	 	 p u b l i c   U I S c r o l l a b l e P a n e l   B u i l d i n g P a n e l ; 
 
 	 	 p u b l i c   U I T a b s t r i p   R C I T a b s ; 
 
 
 
 	 	 p r i v a t e   b o o l   m _ a c t i v e ; 
 
 
 
 	 	 p r o t e c t e d   o v e r r i d e   v o i d   A w a k e ( ) 
 
 	 	 { 
 
 	 	 	 / / t h i s . m _ d a t a L o c k   =   n e w   o b j e c t ( ) ; 
 
 	 	 	 m _ a c t i v e   =   f a l s e ; 
 
 	 	 	 b a s e . A w a k e ( ) ; 
 
 	 	 } 
 
 
 
 	 	 p u b l i c   v o i d   I n i t G u i   ( )   { 
 
 	 	 	 
 
 	 	 	 v a r   b u l l d o z e B u t t o n   =   U I V i e w . G e t A V i e w ( ) . F i n d U I C o m p o n e n t < U I M u l t i S t a t e B u t t o n > ( " R C I B u t t o n " ) ; 
 
 
 
 	 	 	 m a i n B u t t o n   =   b u l l d o z e B u t t o n . p a r e n t . A d d U I C o m p o n e n t < U I B u t t o n > ( ) ; 
 
 	 	 	 m a i n B u t t o n . n a m e   =   " P l o p p a b l e   R C I " ; 
 
 	 	 	 m a i n B u t t o n . s i z e   =   n e w   V e c t o r 2 ( 3 6 ,   3 6 ) ; 
 
 	 	 	 m a i n B u t t o n . r e l a t i v e P o s i t i o n   =   n e w   V e c t o r 2 
 
 	 	 	 	 ( 
 
 	 	 	 	 	 b u l l d o z e B u t t o n . r e l a t i v e P o s i t i o n . x   +   b u l l d o z e B u t t o n . w i d t h   /   2 . 0 f   -   m a i n B u t t o n . w i d t h   -   b u l l d o z e B u t t o n . w i d t h , 
 
 	 	 	 	 	 b u l l d o z e B u t t o n . r e l a t i v e P o s i t i o n . y   +   b u l l d o z e B u t t o n . h e i g h t   /   2 . 0 f   -   m a i n B u t t o n . h e i g h t   /   2 . 0 f 
 
 	 	 	 	 ) ; 
 
 	 	 	 m a i n B u t t o n . n o r m a l B g S p r i t e   =   " Z o n i n g O p t i o n M a r q u e e " ; 
 
 	 	 	 m a i n B u t t o n . f o c u s e d F g S p r i t e   =   " T o o l b a r I c o n G r o u p 6 F o c u s e d " ; 
 
 	 	 	 m a i n B u t t o n . h o v e r e d F g S p r i t e   =   " T o o l b a r I c o n G r o u p 6 H o v e r e d " ; 
 
 	 	 	 m a i n B u t t o n . e v e n t C l i c k   + =   b u t t o n C l i c k e d ; 
 
 
 
 	 	 	 B u i l d i n g P a n e l   =   U I V i e w . G e t A V i e w ( ) . F i n d U I C o m p o n e n t ( " T S B a r " ) . A d d U I C o m p o n e n t < U I S c r o l l a b l e P a n e l > ( ) ; 
 
 	 	 	 / / R C I T a b s   =   U I V i e w . G e t V i e w ( ) . F i n d U I C o m p o n e n t < U I T a b s t r i p > ( " m 
 
 
 
 	 	 	 B u i l d i n g P a n e l . b a c k g r o u n d S p r i t e   =   " S u b c a t e g o r i e s P a n e l " ; 
 
 	 	 	 B u i l d i n g P a n e l . i s V i s i b l e   =   f a l s e ; 
 
 	 	 	 B u i l d i n g P a n e l . n a m e   =   " P l o p p a b l e B u i l d i n g s L i s t " ; 
 
 	 	 	 B u i l d i n g P a n e l . s i z e   =   n e w   V e c t o r 2 ( 1 5 0 ,   4 0 0 ) ; 
 
 
 
 	 	 	 B u i l d i n g P a n e l . r e l a t i v e P o s i t i o n   =   n e w   V e c t o r 2 
 
 	 	 	 	 ( 
 
 	 	 	 	 	 b u l l d o z e B u t t o n . r e l a t i v e P o s i t i o n . x   +   b u l l d o z e B u t t o n . w i d t h   /   2 . 0 f   - B u i l d i n g P a n e l . w i d t h   , 
 
 	 	 	 	 	 b u l l d o z e B u t t o n . r e l a t i v e P o s i t i o n . y   -   B u i l d i n g P a n e l . h e i g h t   
 
 	 	 	 	 ) ; 
 
 	 	 	 / / =   n e w   V e c t o r 3 ( 2 . 0 f ,   2 . 0 f ,   0 . 0 f ) ; 
 
 	 	 	 B u i l d i n g P a n e l . i s V i s i b l e   =   f a l s e ; 
 
 
 
 	 	 	 U I B u t t o n   B u i l d i n g B   =   B u i l d i n g P a n e l . A d d U I C o m p o n e n t < U I B u t t o n >   ( ) ; 
 
 
 
 	 	 	 B u i l d i n g I n f o   b   =   P r e f a b C o l l e c t i o n < B u i l d i n g I n f o > . F i n d L o a d e d   ( " P i t t s f i e l d   B u i l d i n g _ D a t a " ) ; 
 
 
 
 	 	 	 B u i l d i n g B . n a m e   =   " P i t t s f i e l d " ; 
 
 	 	 	 B u i l d i n g B . t e x t   =   n a m e ; 
 
 
 
 	 	 	 U I L a b e l   l   =   B u i l d i n g P a n e l . A d d U I C o m p o n e n t < U I L a b e l >   ( ) ; 
 
 	 	 	 l . t e x t   =   " H E Y " ; 
 
 
 
 	 	 	 B u i l d i n g B . s i z e   =   n e w   V e c t o r 2 ( 3 6 ,   3 6 ) ; 
 
 	 	 	 / / B u i l d i n g B . n o r m a l B g S p r i t e   =   " P o l i c y B a r B a c k " ; 
 
 	 	 	 / / B u i l d i n g B . a t l a s   =   b . m _ A t l a s ; 
 
 
 
 	 	 	 / / B u i l d i n g B . r e l a t i v e P o s i t i o n   =   n e w   V e c t o r 3 ( 2 f ,   2 f ,   0 f ) ; 
 
 	 	 	 B u i l d i n g B . f o r e g r o u n d S p r i t e M o d e   =   U I F o r e g r o u n d S p r i t e M o d e . F i l l ; 
 
 	 	 	 B u i l d i n g B . n o r m a l F g S p r i t e   =   " T h u m b n a i l B u i l d i n g D e f a u l t " ; 
 
 	 	 	 B u i l d i n g B . f o c u s e d F g S p r i t e   =   " T h u m b n a i l B u i l d i n g D e f a u l t "   +   " F o c u s e d " ; 
 
 	 	 	 B u i l d i n g B . h o v e r e d F g S p r i t e   =   " T h u m b n a i l B u i l d i n g D e f a u l t "   +   " H o v e r e d " ; 
 
 	 	 	 B u i l d i n g B . p r e s s e d F g S p r i t e   =   " T h u m b n a i l B u i l d i n g D e f a u l t "   +   " P r e s s e d " ; 
 
 	 	 	 B u i l d i n g B . d i s a b l e d F g S p r i t e   =   " T h u m b n a i l B u i l d i n g D e f a u l t "   +   " D i s a b l e d " ; 
 
 	 	 } 
 
 
 
 	 	 v o i d   b u t t o n C l i c k e d ( U I C o m p o n e n t   c o m p o n e n t ,   U I M o u s e E v e n t P a r a m e t e r   e v e n t P a r a m ) 
 
 	 	 { 
 
 	 	 	 t h i s . e n a b l e d   =   t r u e ; 
 
 	 	 	 	 B u i l d i n g P a n e l . i s V i s i b l e   =   t r u e ; 
 
 	 	 } 
 
 	 	 p r o t e c t e d   o v e r r i d e   v o i d   O n D i s a b l e ( ) 
 
 	 	 { 
 
 	 	 	 B u i l d i n g P a n e l . i s V i s i b l e   =   f a l s e ; 
 
 	 	 	 i f   ( B u i l d i n g P a n e l   ! =   n u l l ) 
 
 	 	 	 	 B u i l d i n g P a n e l . i s V i s i b l e   =   f a l s e ; 
 
 	 	 	 b a s e . O n D i s a b l e ( ) ; 
 
 	 	 } 
 
 	 	 p r o t e c t e d   o v e r r i d e   v o i d   O n E n a b l e ( ) 
 
 	 	 { 
 
 	 	 	 U I V i e w . G e t A V i e w ( ) . F i n d U I C o m p o n e n t < U I T a b s t r i p > ( " M a i n T o o l s t r i p " ) . s e l e c t e d I n d e x   =   - 1 ; 
 
 	 	 	 b a s e . O n E n a b l e ( ) ; 
 
 	 	 } 
 
 	 	 	 
 
 	 	 
 
 	 } 
 
 } 
 
 
 
 
 
 	 
 
 	 	 