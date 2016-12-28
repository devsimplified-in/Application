﻿using System.Collections.Generic;
using WinEchek.Model;
using WinEchek.Model.Pieces;

namespace WinEchek.Core
{
    public class Player
    {
        public delegate void MoveHandler(Player sender, Move move);

        private PlayerControler _playerControler;

        public Player(Color color, PlayerControler playerControler)
        {
            Color = color;
            _playerControler = playerControler;
        }

        public Color Color { get; internal set; }

        public Game Game { get; set; }

        /// <summary>
        ///     Notifie le joueur que c'est à son tour de jouer et que le Game peut recevoir un mouvement de sa part.
        ///     Tant que ce mouvement n'est pas valide, cette méthode est appelée.
        /// </summary>
        public void Play() => _playerControler.Play();

        public void Stop() => _playerControler.Stop();

        public List<Square> PossibleMoves(Piece piece) => Game.PossibleMoves(piece);

        public void Move(Move move)
        {
            MoveDone?.Invoke(this, move);
        }

        public event MoveHandler MoveDone;
    }
}