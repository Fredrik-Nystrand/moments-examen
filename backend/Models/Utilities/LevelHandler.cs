namespace backend.Models.Utilities;

public class LevelHandler
{
    public int Level { get; set; }
    public int XpRemainder { get; set; }
    public bool LeveledUp { get; set; } = false;

    private int _requiredXp = 5000;


    public LevelHandler(int currentXp, int currentLevel)
    {
        checkIfLevelUp(currentXp, currentLevel);
    }
    public void checkIfLevelUp(int currentXp, int currentLevel)
    {
        if (currentXp >= _requiredXp)
        {
            LeveledUp = true;
            Level = currentLevel + 1;

            if (currentXp > _requiredXp)
            {
                XpRemainder = currentXp - _requiredXp;
            }
            else
            {
                XpRemainder = 0;
            }
        }
        else
        {
            LeveledUp = false;
            XpRemainder = currentXp;
            Level = currentLevel;

        }

    }
}
