using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : SkillUpgradeBehaviour
{    public static PlayerStats instance;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        }
        else if (instance != null) {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

    override public void Upgrade(SkillUpgrade _skillUpgrade) {
        if (_skillUpgrade.sName == "Eject button") {
            const float INVUL_UPGRADE = 0.1f;

            invulTime += INVUL_UPGRADE;
        }
        else if (_skillUpgrade.sName == "Rocket boots") {
            const float MAX_SPEED = 13.0f;
            const float SPEED_UPGRADE = 1.0f;

            speed += SPEED_UPGRADE;
            if (speed >= MAX_SPEED) {
                speed = MAX_SPEED;
                _skillUpgrade.sMaxed = true;
            }
        }
        else if (_skillUpgrade.sName == "Metal plating") {
            const float HEALTH_UPGRADE = 5.0f;

            maxHealth += HEALTH_UPGRADE;
            currHealth += HEALTH_UPGRADE;
        }
    }

    [SerializeField]
    private float expToNextLevel;
    [SerializeField]
    private float currExp;
    [SerializeField]
    private int playerLv;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float currHealth;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private bool tookDamage;
    [SerializeField]
    private float invulTime;
    [SerializeField]
    private float invulTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        expToNextLevel = 1;
        currExp = 0;
        playerLv = 1;
        speed = 5;
        damage = 1;
        maxHealth = 200;
        currHealth = maxHealth;
        tookDamage = false;
        invulTime = 0.5f;
        invulTimer = 0.0f;

    }

    // Update is called once per frame
    void Update()
    {
        if (tookDamage) {
            HandleIFrames();
        }
    }

    // Handles the logic of Invisible frames
    // Allows player a small window of invulnerability after being damaged
    void HandleIFrames() {
        if (invulTimer >= invulTime) {
            invulTimer = 0;
            tookDamage = false;
        }
        else {
            invulTimer += Time.deltaTime;
        }
    }

    public int GetplayerLv() { return playerLv; }
    public float GetCurrExp() { return currExp; }
    public float GetExpToNextLevel() { return expToNextLevel; }
    public void SetExpToNextLevel(float _expToNextLevel) { expToNextLevel = _expToNextLevel; }
    public float GetCurrExpPercentClamped() { return currExp/expToNextLevel <= 0 ? 0 : currExp/expToNextLevel >= 1 ? 1 : currExp/expToNextLevel; }
    public float GetSpd() { return speed; }
    public float GetDamage() { return damage; }
    public float GetCurrHealth() { return currHealth; }
    public float GetCurrHealthPercentClamped() { return currHealth/maxHealth <= 0 ? 0 : currHealth/maxHealth >= 1 ? 1 : currHealth/maxHealth; }
    public float GetMaxHealth() { return maxHealth; }

    // Is called when player needs to lose a certain amount of health
    // If Health <= 0, then it will call the defeat function in LevelManager
    public void LoseHealth(float _dmg) {
        if (tookDamage) {
            return;
        }
        
        currHealth -= _dmg;
        if (currHealth <= 0) {
            //LevelManager.getInstance().Defeat();
        }
        
        tookDamage = true;
    }

    public void GainExp(int _exp) {
        currExp += _exp;
        if (currExp >= expToNextLevel) {
            currExp -= expToNextLevel;
            expToNextLevel = 1;
            playerLv++;
            LevelingUIManager.instance.ShowUI(true);
        }
    }
}
