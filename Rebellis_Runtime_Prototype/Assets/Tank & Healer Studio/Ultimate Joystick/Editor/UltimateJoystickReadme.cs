﻿/* UltimateJoystickReadme.cs */
/* Written by Kaz Crowe */
using UnityEngine;
using System.Collections.Generic;

namespace TankAndHealerStudioAssets
{
	public class UltimateJoystickReadme : ScriptableObject
	{
		public Texture2D icon;
		public Texture2D settings;
		public Texture2D scriptReference;

		// GIZMO COLORS //
		[HideInInspector]
		public Color colorDefault = new Color( 1.0f, 1.0f, 1.0f, 0.5f );
		[HideInInspector]
		public Color colorValueChanged = new Color( 0.0f, 1.0f, 0.0f, 1.0f );

		// VERSION CHANGES //
		public static int ImportantChange = 5;
		public class VersionHistory
		{
			public string versionNumber = "";
			public string[] changes;
		}
		public VersionHistory[] versionHistory = new VersionHistory[]
		{
		// VERSION 4.0.0 //
		new VersionHistory ()
		{
			versionNumber = "4.0.0",
			changes = new string[]
			{
				// GENERAL CHANGES //
				"Reordered and simplified the inspector to improve workflow and ease of use",
				"Improved editor performance",
				"Improved existing scene gizmos and added new useful ones",
				"Serialized and made private all internal fields to clean up the script and reference",
				"Added internal documentation and range values for fields inside the main Ultimate Joystick script",
				"Updated the Custom Activation Range option to respect the Left or Right anchor option set by the user",
				"Removed the Scaling Axis option and improved default screen positioning calculations",
				"Removed the Boundary option",
				"Improved the Dead Zone option",
				"Added additional checks within the editor script to disable Raycast Target on child images",
				"Removed the UltimateJoystickScreenSizeUpdater script",
				"Removed the Spaceship Example files",
				"Added new example scenes to help show how to use the many features of the Ultimate Joystick",
				"Improved the Joystick Prefab Display scene to show each individual joystick",
				"Added link to the README file for the online documentation",
				// NEW FEATURES //
				"Added the ability to reposition and resize the joystick at runtime using the new Player Position Override option. See the New Functions section below for the list of associated functions",
				"Added the ability to click and drag the joystick graphic in the editor to set it's position on the screen",
				"Added the ability to open sections on the Ultimate Joystick inspector when dragging an object in the inspector window",
				"Added two new options to the Anchor property: Relative To Transform and Orbit Transform",
				"Added a new option that allows input on the joystick to be transmitted to any graphics that might be below it",
				"Added a new setting to determine how the input is handled on the joystick, whether using the EventSystem or directly using touch input on the screen",
				// DEPRECIATED FUNCTIONS //
				"Depreciated the GetTapCount functions. The new tap count callbacks (OnTapAchieved or OnTapReleased) should now be used instead",
				"Depreciated both the public and static GetJoystickState functions and replaced it with a new function: GetInputActive",
				"Depreciated the UpdateHighlightColor function. The new HighlightColor property can be used to update the highlight color at runtime",
				"Depreciated the UpdateTensionColors function. The new TensionColorNone and TensionColorFull properties can be used to update the tension colors at runtime",
				"Depreciated the GetUltimateJoystick static function. The new ReturnComponent function should now be used instead",
				"Depreciated both the public and static DisableJoystick functions and replaced them with the new Disable functions",
				"Depreciated both the public and static EnableJoystick functions and replaced them with the new Enable functions",
				"Removed the public function: UpdateParentCanvas",
				// NEW FUNCTIONS //
				"Added a new public function for returning the current angle that the joystick is in relation to the center. The function is named: GetAngle",
				"Added 5 new functions for custom player defined positioning: StartOverridePositioning, StopOverridePositioning, SetOverridePosition, SetOverrideSize, and ResetOverridePositioning",
				"Added a new override to the UpdatePositioning function that allows the user to specify new position values to apply to the joystick",
				// FIXES //
				"Fixed a debug that would sometimes be sent to the console while dragging an Ultimate Joystick prefab into a scene",
				"Fixed warnings in Unity 2023+ dealing with obsolete FindObjectOfType references",
			},
		},
		// VERSION 3.2.1 //
		new VersionHistory ()
		{
			versionNumber = "3.2.1",
			changes = new string[]
			{
				// GENERAL CHANGES //
				"Implemented a small check to ensure the Awake() function will always be called even if the user has the Enter Play Mode Settings enabled without checking the Reload Scene option",
			},
		},
		// VERSION 3.2.0 //
		new VersionHistory ()
		{
			versionNumber = "3.2.0",
			changes = new string[]
			{
				// GENERAL CHANGES //
				"Changed the folder structure to be inside of one root folder: Tank & Healer Studio",
				"Removed the option for exclusively using Unity's touch input instead of the EventSystem. The option was not working consistently across all versions of Unity",
				"Improved calculations for making the Ultimate Joystick completely compatible with the new Unity Input System",
				"Added new function: InputInRange. This function is designed to allow users to check to see if any input is within the activation range of the Ultimate Joystick before executing some of their custom code",
				"Removed all of the old depreciated code that was left in on version 3.0.0",
			},
		},
		// VERSION 3.1.2 //
		new VersionHistory ()
		{
			versionNumber = "3.1.2",
			changes = new string[]
			{
				// GENERAL CHANGES //
				"Minor update to the README file",
			},
		},
		// VERSION 3.1.1 //
		new VersionHistory ()
		{
			versionNumber = "3.1.1",
			changes = new string[]
			{
				// BUG FIX //
				"Fixed the joystick to not request a new canvas if the existing canvas didn't use the Constant Pixel Size option",
				"Fixed activation calculation that could be slightly incorrect when using certain canvas options",
			},
		},
		// VERSION 3.1.0 //
		new VersionHistory ()
		{
			versionNumber = "3.1.0",
			changes = new string[]
			{
				// GENERAL CHANGES //
				"Updated prefabs to have consistent starting position values",
				"Improved calculations inside the Ultimate Joystick to allow for use inside of all different Canvas Scaler options",
				"Overall improvements to the Ultimate Joystick editor script",
				"Updated the README file to be able to stay on the same page, even after compiling scripts",
				// BUG FIX //
				"Fixed a small error that would sometimes occur when adding a prefab into a new scene while in the Scene window",
			},
		},
		// VERSION 3.0.0 // Important Change = 5
		new VersionHistory ()
		{
			versionNumber = "3.0.0",
			changes = new string[]
			{
				// GENERAL CHANGES //
				"Removed the Touch Size option and replaced it with a slider for Activation Range. There is still an option for a custom touch area size by using the new Custom Activation Range option located under the Activation Range variable on the inspector",
				"Improved calculations for initiating the touch on the joystick",
				"Removed the use of the Joystick Size Folder",
				"Reorganized the inspector to improve workflow",
				"Added new scene gizmos to help understand what an option is affecting, such as the on the Radius variable so that the user can see a visual representation of the radius of the joystick",
				"Added new option for exclusively using Unity's touch input instead of the EventSystem. This has been requested to combat some potential issues when using different versions of Unity",
				"Removed the option for playing an animation when interacting with the joystick and replaced it with an option for scaling the joystick when being interacted with",
				"Condensed the options for displaying transitions when interacting with the joystick into one option: Input Transition",
				"Added a button into the Tension Accent option to rotate the tension images. This will be useful for when a user wants to use a single tension image for all directions",
				"Added a Base Color option into the inspector which can be used to change the color of the joystick and joystick base images. This variable is only available from the inspector so as not to clutter up the main Ultimate Joystick script",
				"Overall improved the Ultimate Joystick inspector and workflow",
				"Added an option for developers that want to expand on the Ultimate Joystick code. The option is located in the Settings of the README window. To access it, select the README file and click the gear icon in the top right. There will be an option at the bottom for Enable Development Mode. Now the Ultimate Joystick inspector will have a new section: Development Inspector",
				"Simplified calculations within the Ultimate Joystick script to improve performance",
				"Overall cleanup of the Ultimate Joystick script",
				"Revamped the tension display options and functionality to allow for more diversity",
				"Added new versions of the existing joystick textures and removed the old versions",
				"Updated joystick prefabs to use the new textures",
			},
		},
		// VERSION 2.6.1
		new VersionHistory ()
		{
			versionNumber = "2.6.1",
			changes = new string[]
			{
				"Fixed an issue with the README editor",
				"Fixed an issue where the Ultimate Joystick would return incorrect values when using the DisableJoystick function",
			},
		},
		// VERSION 2.6.0
		new VersionHistory ()
		{
			versionNumber = "2.6.0",
			changes = new string[]
			{
				"Improved the Ultimate Joystick textures",
				"Simplified calculations for the horizontal and vertical values of the joystick",
				"Simplified inspector and separated joystick functionality from the visual options",
				"Removed AnimBool functionality from the inspector to avoid errors with Unity 2019+",
				"Improved the Dead Zone calculations to be more consistent and fluid",
				"Renamed Throwable option to Gravity and simplified the option",
				"Renamed Draggable option to Extend Radius to help describe what it does",
				"Added new script: UltimateJoystickReadme.cs",
				"Added new script: UltimateJoystickReadmeEditor.cs",
				"Added new file at the Ultimate Joystick root folder: README. This file has all the documentation and how to information",
				"Removed the UltimateJoystickWindow.cs file. All of that information is now located in the README file",
				"Removed the old README text file. All of that information is now located in the README file",
			},
		},
		// VERSION 2.5.1
		new VersionHistory ()
		{
			versionNumber = "2.5.1",
			changes = new string[]
			{
				"Improved the calculation of the joystick center",
				"Uploaded with Unity 2018 to show compatibility with all versions of Unity",
			},
		},
		// VERSION 2.5.0
		new VersionHistory ()
		{
			versionNumber = "2.5.0",
			changes = new string[]
			{
				"Reordered folders ( again ) to better conform to Unity's new standard for folder structure. This may cause some errors if you already had the Ultimate Joystick inside of your project. Please <b>completely remove</b> the Ultimate Joystick from your project and re-import the Ultimate Joystick after",
				"Removed the ability to create an Ultimate Joystick from the Create menu because of the new folder structure. In order to create an Ultimate Joystick in your scene, use the method explained in the How To section of this window",
				"Fixed a small problem with the Animator for the joysticks in the Space Shooter example scene",
				"Major improvements to the Ultimate Joystick Editor",
				"Completely revamped the current Dead Zone option to be more consistent with Unity's default input system",
				"Updated support for game making plugins by exposing two get values: HoriztonalAxis and VerticalAxis",
				"Added a new script to handle updating with screen size. The script is named UltimateJoystickScreenSizeUpdater",
				"Renamed the GetJoystick function to be GetUltimateJoystick",
				"Added two new functions to use in accord with the new Dead Zone option. These new functions work very similarly to Unity's GetAxisRaw function.\n     • GetHorizontalAxisRaw\n     • GetVerticalAxisRaw",
				"Added an official way to disable and enable the Ultimate Joystick through code.\n     • DisableJoystick\n     • EnableJoystick",
				"Removed the Vector2 GetPosition function. All input values should be obtained through the GetHorizotalAxis and GetVerticalAxis functions",
			},
		},
		};

		[HideInInspector]
		public List<int> pageHistory = new List<int>();
		[HideInInspector]
		public Vector2 scrollValue = new Vector2();
	}
}