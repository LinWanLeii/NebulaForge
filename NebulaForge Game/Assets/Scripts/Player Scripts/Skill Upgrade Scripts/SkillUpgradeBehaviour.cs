using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUpgradeBehaviour : MonoBehaviour
{
    public List<SkillUpgrade> upgradeSkills;

    public SkillUpgrade GetPossibleUpgrade() {
        SkillUpgrade temp = upgradeSkills[Random.Range(0, upgradeSkills.Count)];
        if (temp.sMaxed) {
            upgradeSkills.Remove(temp);
            temp = upgradeSkills[Random.Range(0, upgradeSkills.Count)];
        }

        return temp;
    }

    public List<SkillUpgrade> MakeDirtySkillUpgrade() {
        List<SkillUpgrade> temp = new List<SkillUpgrade>();
        foreach (SkillUpgrade item in upgradeSkills)
        {
            SkillUpgrade s = (SkillUpgrade)ScriptableObject.CreateInstance("SkillUpgrade");
            s.Copy(item);
            temp.Add(s);
        }
        
        return temp;
    }
    
    public virtual void Upgrade (SkillUpgrade _skillUpgrade) {}
}
