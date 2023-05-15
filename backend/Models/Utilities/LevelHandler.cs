namespace backend.Models.Utilities;

public class LevelHandler
{
    public int Level;
    public int XpRemainder;

    private int _requiredXp = 5000;


    public LevelHandler(int currentXp, int currentLevel)
    {
        checkIfLevelUp(currentXp, currentLevel);
    }
    public void checkIfLevelUp(int currentXp, int currentLevel)
    {
        if (currentXp >= _requiredXp)
        {
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
            XpRemainder = currentXp;
            Level = currentLevel;

        }

    }
}
