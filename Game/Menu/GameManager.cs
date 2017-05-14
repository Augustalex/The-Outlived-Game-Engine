﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Menu;
using Game.Menu.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Spelkonstruktionsprojekt.ZEngine.Helpers;
using ZEngine.Systems;
using ZEngine.Wrappers;

namespace Game
{
    public class GameManager
    {

        private SpriteFont font;
        private Texture2D background;
        private float minScale = 1.0f, maxScale = 2.0f, scale = 1.0f;
        private bool moveHigher = true;
        private SpriteBatch sb = GameDependencies.Instance.SpriteBatch;

        // Here we just say that the first state is the Intro
        protected internal GameState CurrentGameState = GameState.Intro;
        protected internal GameState PreviousGameState;
        protected internal KeyboardState OldKeyboardState;
        protected internal GamePadState OldGamepadState;
        protected internal GameContent GameContent;
        protected internal FullZengineBundle Engine;
        // To keep track of the game configurations made
        protected internal GameConfig gameConfig;
    
        private IMenu mainMenu;
        private IMenu gameModesMenu;
        private IMenu characterMenu;
        private IMenu credits;
        private IMenu gameIntro;
        private IMenu survivalGame;
        private IMenu pausedMenu;
        private IMenu multiplayerMenu;

        // Game states
        public enum GameState
        {
            Intro,
            MainMenu,
            GameModesMenu,
            CharacterMenu,
            MultiplayerMenu,
            PlaySurvivalGame,
            Quit,
            Credits,
            Paused
        };

        public GameManager(FullZengineBundle gameBundle)
        {
            Engine = gameBundle;
            GameContent = new GameContent(gameBundle.Dependencies.Game);
            gameConfig = new GameConfig();

            // initializing the states, remember:
            // all the states need to exist in the 
            // manager.
            mainMenu = new MainMenu(this);
            gameModesMenu = new GameModeMenu(this);
            characterMenu = new CharacterMenu(this);
            credits = new Credits(this);
            gameIntro = new GameIntro(this);
            survivalGame = new SurvivalGame(this);
            pausedMenu = new PausedMenu(this);
            multiplayerMenu = new MultiplayerMenu(this);

        }

        // Draw method consists of a switch case with all
        // the different states that we have, depending on which
        // state we are we use that state's draw method.
        public void Draw(GameTime gameTime)
        {     
            sb.GraphicsDevice.Clear(Color.Black);    
            switch (CurrentGameState)
            {

                case GameState.Intro:
                    gameIntro.Draw(gameTime, sb);
                    break;

                case GameState.MainMenu:
                    DrawBackground();
                    mainMenu.Draw(gameTime, sb);
                    break;

                case GameState.PlaySurvivalGame:
                    survivalGame.Draw(gameTime, sb);
                    break;

                case GameState.Quit:
                    Engine.Dependencies.Game.Exit();
                    break;

                case GameState.GameModesMenu:
                    DrawBackground();
                    gameModesMenu.Draw(gameTime, sb);
                    break;

                case GameState.CharacterMenu:
                    DrawBackground();
                    characterMenu.Draw(gameTime, sb);
                    break;

                case GameState.Credits:
                    DrawBackground();
                    credits.Draw(gameTime, sb);
                    break;
                case GameState.Paused:
                    pausedMenu.Draw(gameTime, sb);
                    break;
                case GameState.MultiplayerMenu:
                    multiplayerMenu.Draw(gameTime, sb);
                    break;
            }
        }

        // Same as the draw method, the update method
        // we execute is the one of the current state.
        public void Update(GameTime gameTime)
        {
            switch (CurrentGameState)
            {
                case GameState.Intro:
                    gameIntro.Update(gameTime);
                    break;

                case GameState.MainMenu:
                    mainMenu.Update(gameTime);
                    break;

                case GameState.PlaySurvivalGame:
                    survivalGame.Update(gameTime);
                    break;

                case GameState.Quit:
                    Engine.Dependencies.Game.Exit();
                    break;

                case GameState.GameModesMenu:
                    gameModesMenu.Update(gameTime);
                    break;

                case GameState.CharacterMenu:
                    characterMenu.Update(gameTime);
                    break;

                case GameState.Credits:
                    credits.Update(gameTime);
                    break;

                case GameState.Paused:
                    pausedMenu.Update(gameTime);
                    break;
                case GameState.MultiplayerMenu:
                    multiplayerMenu.Update(gameTime);
                    break;
            }
        }

        // This method can be used to draw a shared, moving background
        // behind other drawn menus that have transparency in them.
        private void DrawBackground()
        {
            if (scale <= maxScale && scale >= minScale)
            {
                if (scale <= minScale + 0.1)
                {
                    moveHigher = true;
                }
                if (scale >= maxScale - 0.1)
                {
                    moveHigher = false;
                }

                if (moveHigher)
                {
                    scale = scale + 0.0001f;
                }
                else
                {
                    scale = scale - 0.0001f;
                }
            }


            sb.Begin();

            sb.Draw(
                texture: GameContent.Background,
                position: Vector2.Zero,
                color: Color.White,
                scale: new Vector2(scale)
            );
            sb.End();
        }
    }
}
