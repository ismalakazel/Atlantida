using UnityEngine;
using System.Collections;

public static class Manager {
	
	// Player
	public static Vector3 playerPos;
	public static Quaternion playerRot;
	public static float camPivot;
	//
	
	// Atronomical Calendar
	public static bool isCalendarActive;
	//
	
	// Collider
	public static bool isEnergySource;
	public static bool isSideButton;
	public static bool isWires;
	public static bool isKeyPad;
	//
	
	// Rock Throw
	public static bool isRockInHand;
	//
	
	public enum GameState
	{
		Menu,
		Playing,
		Pause,
		Credits
	}
	
}
