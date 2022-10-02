using System;

[Serializable]
public class GameData
{
    public SerialiizableDictionary<string, bool[]> starsCollectedByLevels;
    public SerialiizableDictionary<string, float> bestTimeByLevels;

    public GameData()
    {
        starsCollectedByLevels = new SerialiizableDictionary<string, bool[]>();
        bestTimeByLevels = new SerialiizableDictionary<string, float>();
    }
}
