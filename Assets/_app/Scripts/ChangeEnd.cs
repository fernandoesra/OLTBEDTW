using UnityEngine;
using TMPro;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
public class ChangeEnd : MonoBehaviour
{
    [Header("END VARIABLES")]
    [SerializeField] TextMeshProUGUI EndText;
    [SerializeField] GameObject Fire;
    void Start()
    {
        if (EndController.Instance.GetEnd() == 1)
        {
            switch (Language())
            {
                case 0:
                    EndText.SetText("The walled valley where Stillness I landed lies before Aunia, " +
                        "infested throughout by Calliope's flowers. They tried to stop them, " +
                        "but Aunia knew it was a waste of time. Edereta would be here much longer " +
                        "than all of them, even Aunia. When time is not a concern, you eventually " +
                        "find a way. And so Edereta did.\n\n" +

                        "Aunia walks into the passages of the silent ship, the echo of her " +
                        "footsteps on the metal floor filling the interior. It has been eons " +
                        "since anyone entered here but nothing has changed. Everything is still " +
                        "in the same place where they left it so long ago.\n\n" +

                        "Even though it has been so long since she entered the ship she still " +
                        "remembers the architecture. The hallways, the rooms. She knows where " +
                        "everything is. She spent so much time in it that it would be impossible " +
                        "to forget it.\n\n" +

                        "After walking for what seems like hours she finally reaches the center " +
                        "of the ship. The core of the Sivid project, where Edereta rests. Her body, " +
                        "perfectly preserved by the Caliope, is the heart of the ship, the beginning " +
                        "of everything.\n\n" +

                        "Aunia drops her possessions and opens her backpack. She takes out " +
                        "the flamethrower box and sits down to reflect on her decision for a " +
                        "moment. After a long time, perhaps days… Aunia finds it difficult to follow " +
                        "the passage of time now… She stops looking at Edereta and opens the box.\n\n" +

                        "And so it all ends, for both of them.");
                    break;
                case 1:
                    EndText.SetText("El amurallado valle donde aterrizó la Stillness I descansa " +
                        "frente a Aunia, infestado en su totalidad por las flores de Caliope. " +
                        "Intentaron detenerlas, pero Aunia sabía que era una pérdida de tiempo. " +
                        "Edereta estaría aquí mucho más que todos ellos, incluso que Aunia. " +
                        "Cuando el tiempo no es una preocupación, terminas encontrando la manera. " +
                        "Y así lo hizo Edereta.\n\n" +

                        "Aunia se adentra en los pasajes de la silenciosa nave, el eco de sus " +
                        "pisadas sobre el suelo metálico inunda el interior. Hace eones que nadie " +
                        "entraba aquí pero nada ha cambiado. Todo sigue en el mismo lugar donde " +
                        "ellos lo dejaron hace tanto tiempo atrás.\n\n" +

                        "Pese a que hace tanto que no entra en la nave aún recuerda la " +
                        "arquitectura. Los pasillos, las salas. Sabe dónde está todo. Pasó tanto " +
                        "tiempo en ella que sería imposible olvidarlo.\n\n" +

                        "Después de andar durante lo que parecen horas al fin llega al centro " +
                        "de la nave. El núcleo del proyecto Sivid, donde Edereta descansa. Su cuerpo " +
                        "perfectamente conservado por la Caliope es el corazón de la nave, el inicio " +
                        "de todo.\n\n" +

                        "Aunia deja caer sus posesiones y abre la mochila. Saca la caja del " +
                        "lanzallamas y se sienta a reposar durante un momento su decisión. Pasado " +
                        "un largo tiempo, quizás días… A Aunia le cuesta seguir el paso del tiempo ya… " +
                        "Deja de mirar a Edereta y abre la caja.\n\n" +

                        "Y así acaba todo, para ambas.");
                    break;
            }
            Fire.SetActive(true);
        }
        else
        {
            switch (Language())
            {
                case 0:
                    EndText.SetText("The walled valley where Stillness I landed lies before " +
                        "Aunia, infested throughout by Calliope's flowers. They tried to " +
                        "stop them, but Aunia knew it was a waste of time. Edereta would be " +
                        "here much longer than all of them, even Aunia. When time is not a " +
                        "concern, you eventually find a way. And so Edereta did.\n\n" +

                        "Aunia walks into the passages of the silent ship, the echo of " +
                        "her footsteps on the metal floor filling the interior. It has been " +
                        "eons since anyone entered here but nothing has changed. Everything is " +
                        "still in the same place where they left it so long ago.\n\n" +

                        "Even though it has been so long since she entered the ship she still " +
                        "remembers the architecture. The hallways, the rooms. She knows where " +
                        "everything is. She spent so much time in it that it would be impossible " +
                        "to forget it.\n\n" +

                        "After walking for what seems like hours she finally reaches the " +
                        "center of the ship. The core of the Sivid project, where Edereta rests. " +
                        "Her body, perfectly preserved by the Caliope, is the heart of the ship, " +
                        "the beginning of everything.\n\n" +

                        "Aunia drops her possessions and enters, through the broken glass, " +
                        "into the core of the Sivid. Caliope flowers caress her hands as she " +
                        "hugs Edereta's body and for a second she thinks she can hear her " +
                        "breathing.\n\n" +

                        "“I'm sorry it took me so long,” says Aunia. Then she closes her " +
                        "eyes forever.");

                    break;
                case 1:
                    EndText.SetText("El amurallado valle donde aterrizó la Stillness I descansa " +
                        "frente a Aunia, infestado en su totalidad por las flores de Caliope. " +
                        "Intentaron detenerlas, pero Aunia sabía que era una pérdida de tiempo. " +
                        "Edereta estaría aquí mucho más que todos ellos, incluso que Aunia. " +
                        "Cuando el tiempo no es una preocupación, terminas encontrando la manera. " +
                        "Y así lo hizo Edereta.\n\n" +

                        "Aunia se adentra en los pasajes de la silenciosa nave, el eco de sus " +
                        "pisadas sobre el suelo metálico inunda el interior. Hace eones que nadie " +
                        "entraba aquí pero nada ha cambiado. Todo sigue en el mismo lugar donde " +
                        "ellos lo dejaron hace tanto tiempo atrás.\n\n" +

                        "Pese a que hace tanto que no entra en la nave aún recuerda la " +
                        "arquitectura. Los pasillos, las salas. Sabe dónde está todo. Pasó tanto " +
                        "tiempo en ella que sería imposible olvidarlo.\n\n" +

                        "Después de andar durante lo que parecen horas al fin llega al centro " +
                        "de la nave. El núcleo del proyecto Sivid, donde Edereta descansa. Su cuerpo " +
                        "perfectamente conservado por la Caliope es el corazón de la nave, el inicio " +
                        "de todo.\n\n" +

                        "Aunia deja caer sus posesiones y se interna, a través del cristal roto, " +
                        "en el núcleo del Sivid. Las flores de Caliope acarician sus manos mientras " +
                        "se abraza al cuerpo de Edereta y durante un segundo cree escuchar su " +
                        "respiración.\n\n" +

                        "“Lamento haber tardado tanto”, dice Aunia. Luego cierra los ojos " +
                        "para siempre.");

                    break;
            }
        }
    }

    public int Language()
    {
        return LocalizationSettings.AvailableLocales.Locales.IndexOf(LocalizationSettings.SelectedLocale);
    }

    public void MainMenu()
    {
        LoadingScene.LoadScene("MainMenu");
    }

}
