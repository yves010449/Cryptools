using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterHealthModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float val)
    {
        PlayerStats player = character.GetComponent<PlayerStats>();
        if (player != null)
        {
            player.AddHealth((int) val);
        }
    }
}
