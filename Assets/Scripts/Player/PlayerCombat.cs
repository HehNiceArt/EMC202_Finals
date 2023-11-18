using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public bool isAttacking;
    [SerializeField] int comboCount;
    [SerializeField] Animator animCombo;
    public float lastAttackTime;
    public float comboTimeout;
    private void Update()
    {
        if (!isAttacking)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(AttackCombo());
                lastAttackTime = Time.time;
            }

            if (Time.time - lastAttackTime > comboTimeout)
            {
                ResetCombo();
            }
        }
    }
    public IEnumerator AttackCombo()
    {
        isAttacking = true;
        comboCount++;
        animCombo.SetTrigger("Attack" + comboCount);
        float animationTime = GetAnimationLength("Attack" + comboCount);
        yield return new WaitForSeconds(animationTime);
        if (comboCount >= 3)
        {
            ResetCombo();
        }
        isAttacking = false;
    }
    public void ResetCombo()
    {
        comboCount = 0;
        animCombo.SetTrigger("ResetCombo");
    }
    float GetAnimationLength(string triggerName)
    {
        AnimatorClipInfo[] clipInfo = animCombo.GetCurrentAnimatorClipInfo(0);
        foreach (var clip in clipInfo)
        {
            if (clip.clip.name == triggerName)
            {
                return clip.clip.length;
            }
        }
        return 0f;
    }
}
