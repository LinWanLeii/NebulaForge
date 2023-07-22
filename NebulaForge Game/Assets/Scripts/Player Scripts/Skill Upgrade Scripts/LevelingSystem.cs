using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelingSystem : MonoBehaviour
{
    public static LevelingSystem instance;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        }
        else if (instance != null) {
            Destroy(this);
        }
        //DontDestroyOnLoad(this);
    }


    public ExpBarUI expBarUI;
    public List<GameObject> upgradeGOs;
    public List<SkillUpgrade> upgradeSkills;
    public List<SkillUpgradeBehaviour> skillUpgradeBehaviours;

    public SkillUpgrade[] GetPossibleUpgrade() {
        expBarUI.SetMax();

        SkillUpgrade temp0 = upgradeSkills[Random.Range(0, upgradeSkills.Count)];
        while (temp0.sMaxed) {
            upgradeSkills.Remove(temp0);
            temp0 = upgradeSkills[Random.Range(0, upgradeSkills.Count)];
        }

        upgradeSkills.Remove(temp0);
        SkillUpgrade temp1 = upgradeSkills[Random.Range(0, upgradeSkills.Count)];
        while (temp1.sMaxed) {
            upgradeSkills.Remove(temp1);
            temp1 = upgradeSkills[Random.Range(0, upgradeSkills.Count)];
        }

        upgradeSkills.Remove(temp1);
        SkillUpgrade temp2 = upgradeSkills[Random.Range(0, upgradeSkills.Count)];
        while (temp2.sMaxed) {
            upgradeSkills.Remove(temp2);
            temp2 = upgradeSkills[Random.Range(0, upgradeSkills.Count)];
        }

        SkillUpgrade[] t = new SkillUpgrade[3];
        t[0] = temp0;
        t[1] = temp1;
        t[2] = temp2;
        upgradeSkills.Add(temp0);
        upgradeSkills.Add(temp1);

        return t;
    }

    // Given the current player's level,
    // return the amount of exp needed for the next level
    public float GetExpToNextLevel(int _currPlayerLevel) {
        return PlayerStats.instance.GetExpToNextLevel() + _currPlayerLevel;


        // switch (_currPlayerLevel)
        // {
        //     case 0:
        //         return 3;
        //     case 1:
        //         return 5;
        //     default:
        //         return 10;
        // }
    }

    public void TryUpgrade(SkillUpgrade _skillUpgrade) {
        foreach(GameObject go in upgradeGOs) {
            SkillUpgradeBehaviour i = go.GetComponent<SkillUpgradeBehaviour>();
            i.Upgrade(_skillUpgrade);
        }

        PlayerStats.instance.SetExpToNextLevel(GetExpToNextLevel(PlayerStats.instance.GetplayerLv()));
        PlayerStats.instance.GainExp(0);
        
        expBarUI.SetZero();
    }

    public void Start() {
        skillUpgradeBehaviours = new List<SkillUpgradeBehaviour>();

        foreach (GameObject go in upgradeGOs)
        {
            SkillUpgradeBehaviour i = go.GetComponent<SkillUpgradeBehaviour>();

            if (i != null) {
                skillUpgradeBehaviours.Add(i);
            }
        }
        
        foreach (SkillUpgradeBehaviour b in skillUpgradeBehaviours)
        {
            List<SkillUpgrade> temp = b.MakeDirtySkillUpgrade();
            upgradeSkills.AddRange(temp);
        }
    }
    
}
