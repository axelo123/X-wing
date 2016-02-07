using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public static class OutilsFormulaire
{
    /// <summary>
    /// Permet de remplir une liste avec les données provenant d'une énumération
    /// </summary>
    /// <typeparam name="T">Type des éléments de l'énumération</typeparam>
    /// <param name="Liste">Contrôle ListBox/ComboBox à remplir</param>
    /// <param name="Elements">Enumération des éléments servant au remplissage de la liste</param>
    /// <param name="SelectionParDefaut">Element à sélectionner par défaut, sinon de préférence, l'élément précédemment sélectionné dans la liste sera si possible resélectionné</param>
    /// <returns>Vrai si le remplissage a pu se faire, sinon faux</returns>
    public static bool RemplirListe<T>(Control Liste, IEnumerable<T> Elements, T SelectionParDefaut)
    {
        T ValeurPrecedemmentSelectionnee = SelectionParDefaut;
        if (Liste is ComboBox)
        {
            if ((Liste as ComboBox).SelectedItem is T) ValeurPrecedemmentSelectionnee = (T)((Liste as ComboBox).SelectedItem);
            (Liste as ComboBox).Items.Clear();
        }
        else if (Liste is ListBox)
        {
            if ((Liste as ListBox).SelectedItem is T) ValeurPrecedemmentSelectionnee = (T)((Liste as ListBox).SelectedItem);
            (Liste as ListBox).Items.Clear();
        }
        else
            return false;
        foreach (T Element in Elements)
        {
            int Index = -1; // -1 pour la propriété .SelectedIndex correspond à une "non sélection"
            if (Liste is ComboBox)
                Index = (Liste as ComboBox).Items.Add(Element); // la méthode .Add retourne l'indice de l'élément au moment où il a été ajouté
            else //if (Liste is ListBox)
                Index = (Liste as ListBox).Items.Add(Element);
            if (Element.Equals(ValeurPrecedemmentSelectionnee))
            {
                if (Liste is ComboBox)
                    (Liste as ComboBox).SelectedIndex = Index;
                else //if (Liste is ListBox)
                    (Liste as ListBox).SelectedIndex = Index;
            }
        }
        return true;
    }

    /// <summary>
    /// Permet de sélectionner un élément dans une liste (en sélection simple)
    /// </summary>
    /// <typeparam name="T">Type des éléments de la liste</typeparam>
    /// <param name="Liste">Contrôle ListBox/ComboBox dans lequel effectuer la sélection</param>
    /// <param name="ElementASelectionner">Element à sélectionner</param>
    /// <returns>Vrai si la sélection a pu se faire, sinon faux</returns>
    public static bool SelectionnerDansListe<T>(Control Liste, T ElementASelectionner)
    {
        if (ElementASelectionner == null) return false;
        if (Liste is ComboBox)
        {
            (Liste as ComboBox).SelectedItem = ElementASelectionner;
            if (!ElementASelectionner.Equals((Liste as ComboBox).SelectedItem)) return false;
        }
        else if (Liste is ListBox)
        {
            (Liste as ListBox).SelectedItem = ElementASelectionner;
            if (!ElementASelectionner.Equals((Liste as ListBox).SelectedItem)) return false;
        }
        else
            return false;
        return true;
    }

    /// <summary>
    /// Permet de déselectionner l'élément actuellement sélectionné dans une liste (en sélection simple)
    /// </summary>
    /// <typeparam name="T">Type des éléments de la liste</typeparam>
    /// <param name="Liste">Contrôle ListBox/ComboBox dans lequel effectuer la désélection</param>
    /// <returns>Vrai si la désélection a pu se faire, sinon faux</returns>
    public static bool DeselectionnerDansListe<T>(Control Liste)
    {
        if (Liste is ComboBox)
            (Liste as ComboBox).SelectedItem = null;
        else if (Liste is ListBox)
            (Liste as ListBox).SelectedItem = null;
        else
            return false;
        return true;
    }
}
