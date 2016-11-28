﻿using WinEchek.Engine.Rules;
using Type = WinEchek.Model.Piece.Type;

namespace WinEchek.Engine.RuleManager
{
    public class RookRuleGroup : RuleGroup
    {
        public RookRuleGroup()
        {
            Rules.Add(new RookMovementRule());
            Rules.Add(new CanOnlyTakeEnnemyRule());
        }
        protected override Type Type => Type.Rook;
    }
}