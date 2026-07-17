using UnityEngine;

public static class SaveData
{
    public static bool TutorialVisto
    {
        get => PlayerPrefs.GetInt("TUTORIAL_VISTO", 0) == 1;
        set
        {
            PlayerPrefs.SetInt("TUTORIAL_VISTO", value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public static bool Nivel2Desbloqueado
    {
        get => PlayerPrefs.GetInt("NIVEL2_DESBLOQUEADO", 0) == 1;
        set
        {
            PlayerPrefs.SetInt("NIVEL2_DESBLOQUEADO", value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
}