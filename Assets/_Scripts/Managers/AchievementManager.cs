using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Can be moved to the real Achievement Manager @Zaky
public enum AchievementType
{
    KillFish,       // Number of killed fish
    ReachingLevel,  // Reaching some levels
    DoDamage,       // Give damage to fish
}
public enum AchievementState
{
    OnGoing,       // Achievement still on completing process
    Completed,     // Achievement is completed but reward not claimed yet
    Claimed        // Achievement reward is claimed
}
