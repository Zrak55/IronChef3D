using UnityEngine;
using System.Collections;
using System.ComponentModel;
using Steamworks;

// This is a port of StatsAndAchievements.cpp from SpaceWar, the official Steamworks Example.
class AchievementManager : MonoBehaviour
{
	bool AchievesInit = false;

	private enum Achievement : int
	{
		ACH_TUTORIAL,
		ACH_CH1_BREAKFAST,
		ACH_CH1_LUNCH,
		ACH_CH1_DINNER,
		ACH_CH1_DESSERT,
		ACH_CH1,
		ACH_CH1_BREAKFAST_PERFECT,
		ACH_CH1_LUNCH_PERFECT,
		ACH_CH1_DINNER_PERFECT,
		ACH_CH1_DESSERT_PERFECT,
		ACH_CH1_PERFECT,
		ACH_HIDDEN_POWER,
		ACH_300_DAMAGE,
		ACH_BENEDICT_UNTOUCHED,
		ACH_BEEFCAKE_UNTOUCHED,
		ACH_ITALERNEAN_UNTOUCHED,
		ACH_NECCREAMMANCER_UNTOUCHED
	};

	private Achievement_t[] m_Achievements = new Achievement_t[] {
		new Achievement_t(Achievement.ACH_TUTORIAL, "Ready for Adventure!", "Complete the tutorial."),
		new Achievement_t(Achievement.ACH_CH1_BREAKFAST, "Sunrise Dining", "Complete Breakfast in Chapter 1."),
		new Achievement_t(Achievement.ACH_CH1_LUNCH, "Midday Mayhem", "Complete Lunch in Chapter 1."),
		new Achievement_t(Achievement.ACH_CH1_DINNER, "Going Out to Eat", "Complete Dinner in Chapter 1."),
		new Achievement_t(Achievement.ACH_CH1_DESSERT, "Sweet Tooth", "Complete Dessert in Chapter 1."),
		new Achievement_t(Achievement.ACH_CH1, "The Start of a Long Journey", "Complete all of Chapter 1."),
		new Achievement_t(Achievement.ACH_CH1_BREAKFAST_PERFECT, "Most Important Meal of the Day", "Complete breakfast in Chapter 1 with a perfect score."),
		new Achievement_t(Achievement.ACH_CH1_LUNCH_PERFECT, "Sacked Lunch", "Complete Lunch in Chapter 1 with a perfect score."),
		new Achievement_t(Achievement.ACH_CH1_DINNER_PERFECT, "5 Star Restaurant", "Complete Dinner in Chapter 1 with a perfect score."),
		new Achievement_t(Achievement.ACH_CH1_DESSERT_PERFECT, "Sugary Delight", "Complete Dessert in Chapter 1 with a perfect score."),
		new Achievement_t(Achievement.ACH_CH1_PERFECT, "Chapter 1 Champion", "Complete all of Chapter 1 with a perfect score."),
		new Achievement_t(Achievement.ACH_HIDDEN_POWER, "Little Friend", "Unlock a hidden power."),
		new Achievement_t(Achievement.ACH_300_DAMAGE, "Brutal Eggs and Ham", "Deal 300 damage is a single blow."),
		new Achievement_t(Achievement.ACH_BENEDICT_UNTOUCHED, "Over Easy", "Defeat Benedict without taking any damage."),
		new Achievement_t(Achievement.ACH_BEEFCAKE_UNTOUCHED, "Burger Well-Done", "Defeat Beefcake without taking any damage."),
		new Achievement_t(Achievement.ACH_ITALERNEAN_UNTOUCHED, "Give Me the Formuoli", "Defeat the Hydravioli without taking any damage."),
		new Achievement_t(Achievement.ACH_NECCREAMMANCER_UNTOUCHED, "Let it Spoil Dinner", "Defeat the Neccreammancer without taking any damage."),
	};

	// Our GameID
	private CGameID m_GameID;

	// Did we get the stats from Steam?
	private bool m_bRequestedStats;
	private bool m_bStatsValid;

	// Should we store stats this frame?
	private bool m_bStoreStats;

	

	protected Callback<UserStatsReceived_t> m_UserStatsReceived;
	protected Callback<UserStatsStored_t> m_UserStatsStored;
	protected Callback<UserAchievementStored_t> m_UserAchievementStored;

	void OnEnable()
	{
		
	}




	private void Update()
	{
		if (!AchievesInit)
        {
			if (!SteamManager.Initialized)
				return;

			// Cache the GameID for use in the Callbacks
			m_GameID = new CGameID(SteamUtils.GetAppID());

			m_UserStatsReceived = Callback<UserStatsReceived_t>.Create(OnUserStatsReceived);
			m_UserStatsStored = Callback<UserStatsStored_t>.Create(OnUserStatsStored);
			m_UserAchievementStored = Callback<UserAchievementStored_t>.Create(OnAchievementStored);

			// These need to be reset to get the stats upon an Assembly reload in the Editor.
			m_bRequestedStats = false;
			m_bStatsValid = false;

			AchievesInit = true;

			GetComponent<FBPPInitialLoad>().allInitialized = true;
		}
		
	}


	public static void UnlockAchievement(string achieveName)
	{
		Debug.Log("Unlocking " + achieveName);

		var am = FindObjectOfType<AchievementManager>();
		if (am == null) return;

		foreach (var achievement in am.m_Achievements)
		{
			if (achievement.m_strName == achieveName)
			{
				am.UnlockAchievement(achievement);
				break;
			}
		}
	}

	//-----------------------------------------------------------------------------
	// Purpose: Unlock this achievement
	//-----------------------------------------------------------------------------
	private void UnlockAchievement(Achievement_t achievement)
	{
		if(!achievement.m_bAchieved)
        {
			achievement.m_bAchieved = true;

			// the icon may change once it's unlocked
			//achievement.m_iIconImage = 0;

			// mark it down
			SteamUserStats.SetAchievement(achievement.m_eAchievementID.ToString());

			// Store stats end of frame
			m_bStoreStats = true;

		}
		
	}

	//-----------------------------------------------------------------------------
	// Purpose: We have stats data from Steam. It is authoritative, so update
	//			our data with those results now.
	//-----------------------------------------------------------------------------
	private void OnUserStatsReceived(UserStatsReceived_t pCallback)
	{
		if (!SteamManager.Initialized)
			return;

		// we may get callbacks for other games' stats arriving, ignore them
		if ((ulong)m_GameID == pCallback.m_nGameID)
		{
			if (EResult.k_EResultOK == pCallback.m_eResult)
			{
				Debug.Log("Received stats and achievements from Steam\n");

				m_bStatsValid = true;

				// load achievements
				foreach (Achievement_t ach in m_Achievements)
				{
					bool ret = SteamUserStats.GetAchievement(ach.m_eAchievementID.ToString(), out ach.m_bAchieved);
					if (ret)
					{
						ach.m_strName = SteamUserStats.GetAchievementDisplayAttribute(ach.m_eAchievementID.ToString(), "name");
						ach.m_strDescription = SteamUserStats.GetAchievementDisplayAttribute(ach.m_eAchievementID.ToString(), "desc");
					}
					else
					{
						Debug.LogWarning("SteamUserStats.GetAchievement failed for Achievement " + ach.m_eAchievementID + "\nIs it registered in the Steam Partner site?");
					}
				}

				// load stats
				//SteamUserStats.GetStat("NumGames", out m_nTotalGamesPlayed);
				//SteamUserStats.GetStat("NumWins", out m_nTotalNumWins);
				//SteamUserStats.GetStat("NumLosses", out m_nTotalNumLosses);
				//SteamUserStats.GetStat("FeetTraveled", out m_flTotalFeetTraveled);
				//SteamUserStats.GetStat("MaxFeetTraveled", out m_flMaxFeetTraveled);
				//SteamUserStats.GetStat("AverageSpeed", out m_flAverageSpeed);
			}
			else
			{
				Debug.Log("RequestStats - failed, " + pCallback.m_eResult);
			}
		}
	}

	//-----------------------------------------------------------------------------
	// Purpose: Our stats data was stored!
	//-----------------------------------------------------------------------------
	private void OnUserStatsStored(UserStatsStored_t pCallback)
	{
		// we may get callbacks for other games' stats arriving, ignore them
		if ((ulong)m_GameID == pCallback.m_nGameID)
		{
			if (EResult.k_EResultOK == pCallback.m_eResult)
			{
				Debug.Log("StoreStats - success");
			}
			else if (EResult.k_EResultInvalidParam == pCallback.m_eResult)
			{
				// One or more stats we set broke a constraint. They've been reverted,
				// and we should re-iterate the values now to keep in sync.
				Debug.Log("StoreStats - some failed to validate");
				// Fake up a callback here so that we re-load the values.
				UserStatsReceived_t callback = new UserStatsReceived_t();
				callback.m_eResult = EResult.k_EResultOK;
				callback.m_nGameID = (ulong)m_GameID;
				OnUserStatsReceived(callback);
			}
			else
			{
				Debug.Log("StoreStats - failed, " + pCallback.m_eResult);
			}
		}
	}

	//-----------------------------------------------------------------------------
	// Purpose: An achievement was stored
	//-----------------------------------------------------------------------------
	private void OnAchievementStored(UserAchievementStored_t pCallback)
	{
		// We may get callbacks for other games' stats arriving, ignore them
		if ((ulong)m_GameID == pCallback.m_nGameID)
		{
			if (0 == pCallback.m_nMaxProgress)
			{
				Debug.Log("Achievement '" + pCallback.m_rgchAchievementName + "' unlocked!");
			}
			else
			{
				Debug.Log("Achievement '" + pCallback.m_rgchAchievementName + "' progress callback, (" + pCallback.m_nCurProgress + "," + pCallback.m_nMaxProgress + ")");
			}
		}
	}

	private class Achievement_t
	{
		public Achievement m_eAchievementID;
		public string m_strName;
		public string m_strDescription;
		public bool m_bAchieved;

		/// <summary>
		/// Creates an Achievement. You must also mirror the data provided here in https://partner.steamgames.com/apps/achievements/yourappid
		/// </summary>
		/// <param name="achievement">The "API Name Progress Stat" used to uniquely identify the achievement.</param>
		/// <param name="name">The "Display Name" that will be shown to players in game and on the Steam Community.</param>
		/// <param name="desc">The "Description" that will be shown to players in game and on the Steam Community.</param>
		public Achievement_t(Achievement achievementID, string name, string desc)
		{
			m_eAchievementID = achievementID;
			m_strName = name;
			m_strDescription = desc;
			m_bAchieved = false;
		}
	}
}