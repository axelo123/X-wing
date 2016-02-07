using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class OutilsEnumeration
{
    /// <summary>
    /// Génrère une énumération de valeurs entières comprises entre deux bornes, et en utilisant la valeur d'incrément spécifiée
    /// </summary>
    /// <param name="ValeurInitiale">Valeur initiale</param>
    /// <param name="ValeurFinale">Valeur finale</param>
    /// <param name="Increment">Valeur d'incrémentation positive ou négative de telle façon à pouvoir atteindre, ou dépasser, après une ou plusieurs étapes, la valeur finale en partant de la valeur initiale</param>
    /// <returns>Valeurs de l'énumération souhaitée</returns>
    public static IEnumerable<int> Enumerer(int ValeurInitiale, int ValeurFinale, int Increment = 1)
    {
        if (ValeurInitiale <= ValeurFinale)
        {
            if (Increment <= 0) yield break;
            for (int Valeur = ValeurInitiale; Valeur <= ValeurFinale; Valeur += Increment) yield return Valeur;
        }
        else
        {
            if (Increment >= 0) yield break;
            for (int Valeur = ValeurInitiale; Valeur >= ValeurInitiale; Valeur += Increment) yield return Valeur;
        }
    }
}
