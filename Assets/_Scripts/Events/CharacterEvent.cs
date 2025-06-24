using UnityEngine;
using UnityEngine.Events;

public class CharacterEvent
{
    public static UnityAction<GameObject, int> characterDamaged;
    public static UnityAction<GameObject, int> characterHealed;
}
