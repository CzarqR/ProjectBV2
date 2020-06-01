﻿using ProjectB.Model.Board;
using ProjectB.Model.Help;
using ProjectB.Model.Sklills;
using System.Collections.Generic;
using System.IO;

namespace ProjectB.Model.Figures
{
    using R = Properties.Resources;

    class Mag : Pawn
    {

        #region Properties

        /// Stats
        public const int SKILL_ATTACK_OUTSIDE = 10; //skill dmg outside center

        public override int BaseHp => 20;
        public override int BaseManna => 10;
        public override int Condition => 1;
        public override int Armor => 1;
        public override int PrimaryAttackRange => 1;
        public override int PrimaryAttackCost => 0;
        public override int PrimaryAttackDmg => 11;
        public override int SkillAttackRange => 4;
        public override int SkillAttackCost => 8;
        public override int SkillAttackDmg => 20; //skill dmg center
        public override int MannaRegeneration => 1;

        /// Strings
        public override string PrimaryAttackDesc => string.Format(R.mag_primary_desc, PrimaryAttackDmg, BaseManna);
        public override string PrimaryAttackName => string.Format(R.primary_attack_info, R.mag_skill_name, PrimaryAttackRange, PrimaryAttackCost);
        public override string SkillAttackDesc => string.Format(R.mag_skill_desc, SkillAttackDmg, SKILL_ATTACK_OUTSIDE);
        public override string SkillAttackName => string.Format(R.skilll_attack_info, R.mag_skill_name, SkillAttackRange, SkillAttackCost);
        public override string Class => R.mag;
        public override string Desc => R.mag_desc;

        #endregion


        #region Methods  

        public Mag(bool owner, Cord cord) : base(owner, cord)
        {

        }

        public override bool IsSomeoneToAttack(Cord cord, Arena A, bool attackType)
        {
            if (attackType) //primary attack
            {
                return base.IsSomeoneToAttack(cord, A, attackType);
            }
            else
            {
                if (Manna < SkillAttackCost)
                {
                    return false;
                }
                return true;
            }
        }

        protected override UnmanagedMemoryStream GetSound()
        {
            return R.mag_attack_0;
        }

        public override List<Cord> MarkFieldsToAttack(List<Cord> possibleAttackFields, Arena A, bool attackType)
        {


            if (attackType) //primary attack
            {
                return base.MarkFieldsToAttack(possibleAttackFields, A, attackType);
            }
            else
            {
                for (int i = 0; i < possibleAttackFields.Count; i++)
                {
                    if (A[possibleAttackFields[i]].SkillOwner != null)
                    {
                        A[possibleAttackFields[i]].FloorStatus = FloorStatus.Normal;
                        possibleAttackFields.RemoveAt(i--);
                    }
                }


                return possibleAttackFields;
            }

        }

        public override void SkillAttack(GameState gameState, Cord defender, int bonus)
        {
            TurnAttack(defender);
            Manna -= SkillAttackCost;
            gameState.At(defender).SkillOwner = Owner;
            MagSkill skill = new MagSkill(defender, Owner, SkillAttackDmg + gameState.At(Cord).AttackBonus, bonus, gameState);
            gameState.AddSkill(skill);
            skill.Place();

        }

        #endregion

    }
}
