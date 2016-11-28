﻿using WinEchek.Engine.Rules;
using Type = WinEchek.Model.Piece.Type;

namespace WinEchek.Engine.RuleManager
{
    public class BishopRuleGroup : RuleGroup
    {
        public BishopRuleGroup()
        {
            Rules.Add(new CanOnlyTakeEnnemyRule());
            Rules.Add(new BishopMovementRule());
        }
        protected override Type Type => Type.Bishop;
    }
}