using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class SkillUpgrade : ScriptableObject
{
    public Image sImage;
    public string sName;
    public string sDescription;
    public int sCurrLevel;
    public bool sMaxed;

    public void Copy(SkillUpgrade _skillUpgrade) {
        sImage = _skillUpgrade.sImage;
        sName = _skillUpgrade.sName;
        sDescription = _skillUpgrade.sDescription;
        sCurrLevel = _skillUpgrade.sCurrLevel;
        sMaxed = _skillUpgrade.sMaxed;
    }
}
