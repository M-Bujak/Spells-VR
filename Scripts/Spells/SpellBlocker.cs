using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpellBlocker : MonoBehaviour
{
    [field: SerializeField]
    private SpellCaster[] AllSpellCasters { get; set; }
    [field: SerializeField]
    private float BlockTimeOnHit { get; set; }
    [field: SerializeField]
    private GameObject[] BlockVFX { get; set; }

    private List<SpellCaster> BlockingSpellCasters { get; set; }
        = new List<SpellCaster>();

    private void Awake()
    {
        PlayerOnHitHandler.OnPlayerHit += BlockAllSpellCasters;
    }

    private void BlockAllSpellCasters()
    {
        for(int i = 0; i < AllSpellCasters.Length; i++)
        {
            AllSpellCasters[i].Block();
        }

        SetActiveVFX(true);

        StartCoroutine(WaitBeforeUnblock(BlockTimeOnHit));
    }

    IEnumerator WaitBeforeUnblock(float time)
    {
        float passedTime = 0;
        while(passedTime < time)
        {
            passedTime += Time.deltaTime;
            yield return null;
        }

        UnblockAllSpellCasters();
    }

    private void UnblockAllSpellCasters()
    {
        for (int i = 0; i < AllSpellCasters.Length; i++)
        {
            AllSpellCasters[i].Unblock();
        }

        SetActiveVFX(false);
    }

    private void SetActiveVFX(bool value)
    {
        for(int i = 0; i < BlockVFX.Length; i++)
        {
            BlockVFX[i].SetActive(value);
        }
    }

    public void BlockOthers(SpellCaster spellCaster)
    {
        for (int i = 0; i < spellCaster.BlocksThese.Length; i++)
        {
            spellCaster.BlocksThese[i].Block();
        }

        BlockingSpellCasters.Add(spellCaster);
    }

    public void UnblockOthers(SpellCaster spellCaster)
    {
        BlockingSpellCasters.Remove(spellCaster);

        for (int i = 0; i < spellCaster.BlocksThese.Length; i++)
        {
            bool CanUnblockThisOne = true;
            for (int j = 0; j < BlockingSpellCasters.Count; j++)
            {
                if (!BlockingSpellCasters[j].BlocksThese
                    .Contains(spellCaster.BlocksThese[j]))
                {
                    CanUnblockThisOne = false;
                }
            }

            if (CanUnblockThisOne == true)
            {
                spellCaster.BlocksThese[i].Unblock();
            }
        }
    }

    private void LogAllBlockStates()
    {
        for (int i = 0; i < AllSpellCasters.Length; i++)
        {
            Debug.Log(i + ". Blocked: " + AllSpellCasters[i].IsBeingBlocked);
        }
    }

    private void LogAllBlocking()
    {
        for (int i = 0; i < BlockingSpellCasters.Count; i++)
        {
            Debug.Log(BlockingSpellCasters[i]);
        }
    }

    private void OnDestroy()
    {
        PlayerOnHitHandler.OnPlayerHit -= BlockAllSpellCasters;
    }
}
