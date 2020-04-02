﻿using ProjectB.Model.Figures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB.Model.Board
{

    public class Field
    {
        public int MovementBonus
        {
            get; set;
        }
        public double AttackBonus
        {
            get; set;
        }
        public double DefBonus
        {
            get; set;
        }

        public Pawn PawnOnField
        {
            get;
            set;
        }

        public bool CanMove
        {
            get; set;
        }

        public FloorType Floor
        {
            get; set;
        }

        public string FloorPath()
        {
            return string.Format(App.pathToFloor, Floor);
        }


        public Field(Pawn pawnOnField = null, FloorType floor = FloorType.Base, int movementBonus = 0, double attackBonus = 1, double defBonus = 1) // default field without bonuses
        {
            MovementBonus = movementBonus;
            AttackBonus = attackBonus;
            DefBonus = defBonus;
            PawnOnField = pawnOnField;
            Floor = floor;


        }


    }

    public enum FloorType
    {
        Base = 1,
        Attack = 2,
        Def = 3,
        Cond = 4
    }

}
