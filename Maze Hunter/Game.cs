﻿using System;
using System.ComponentModel;

namespace Maze_Hunter
{
	// The central class for the game logic. 
	class Game
	{
		Random rand = new Random();
		bool IsRunning = true;		// When set to false, the game loop stops and program exits.
		GameUI UI;					// The UI object holds the visual elements, but no game logic.
		MazeRoom Maze;              // The Maze object holds the game logic, but no UI elements.
		public Character Player;

		// Creates a new instance of the game. (Should only be called once in the Main method)
		public Game()
		{
			Maze = new MazeRoom();
			Player = new Character();
			UI = new GameUI(Maze, Player);
		}

		// All games have a central game loop. The process goes as follows:
		// Read input => Apply action => Repeat
		public void Loop()
		{
			ConsoleKeyInfo keyInfo = Console.ReadKey();

			while (IsRunning)
			{
				// .Key as in key from the keyboard. Not to be confused with dictionary keys.
				if (keyInfo.Key == ConsoleKey.Escape)		// Whenever ESC is pressed, exit the app.
				{
					IsRunning = false;
				}
				else if (keyInfo.Key == ConsoleKey.Enter)	// Whenever Enter is pressed, apply action.
				{
					Update();
				}
				else
				{                                           // All other keys are passed to the
					UI.HandleKey(keyInfo.Key);              // currently active screen. Each screen 
					SetAttributesMenuParams();
				}											// has specific logic for different keys

				if (IsRunning)
				{
					UI.Draw();								// Redraw the UI each time a key is pressed.
					keyInfo = Console.ReadKey();
				}
			}
			// When the while-loop is over, the app exits.
		}

		// Handles the user selected options in the menus.
		// The outer if-else block separates different sections for different screens.
		// Each inner if-else block handles the selected options of the corresponding screen.
		void Update()
		{
			string currentOptionText = UI.GetMenu().GetCurrentOptionText();

			if (UI.currentScreen == "StartScreen")
			{   
				if (currentOptionText == "New Game")
				{
					UI.SetScreen("NewGameScreen");
					//SetNewGameMenuParams();
                }
				else if (currentOptionText == "History")
				{
					UI.SetScreen("HistoryScreen");
				}
				else if (currentOptionText == "Exit")
				{
					IsRunning = false;
				}

			}
			else if (UI.currentScreen == "NewGameScreen")
			{
				if (currentOptionText == "Start Game")
				{
					UI.SetScreen("MazeScreen");
				}

				// For now the other options in the New Game screen do nothing.
				else if (currentOptionText == "Back")
				{
					UI.SetScreen("StartScreen");
				}
				//indicate that each of the options leads to its own screen
				
				else if (currentOptionText == "Guild")
				{
					UI.SetScreen("GuildScreen");
					
				}
                else if (currentOptionText == "Gender")
                {
                    UI.SetScreen("GenderScreen");

                }
                else if (currentOptionText == "Name")
                {
                    UI.SetScreen("NameScreen");
					
                }
                else if (currentOptionText == "Back")
                {
                    UI.SetScreen("StartScreen");
                }
                else if (currentOptionText == "Attributes")
                {
					UI.SetScreen("AttributesScreen");
				}
                else if (currentOptionText == "Randomize")
                {
                    UI.SetScreen("RandomizeScreen");

                }
            }			//fill each of those screens with their own options
			else if (UI.currentScreen == "GuildScreen")
			{
				if (currentOptionText == "Guild Of Thieves")
                {
                    Player.GuildChecker = 1;
                    Player.Guilds();
					UI.SetScreen("NewGameScreen");
					SetGuildMenuParams();
				}
				else if (currentOptionText == "Guild Of Assassins")
                {
                    Player.GuildChecker = 2;
                    Player.Guilds();
					UI.SetScreen("NewGameScreen");
					SetGuildMenuParams();
				}
				else if (currentOptionText == "Back")
                {
                    UI.SetScreen("NewGameScreen");
                }
            }
            else if (UI.currentScreen == "GenderScreen")
            {
                if (currentOptionText == "Male")
                {
					Player.Male();
                    UI.SetScreen("NewGameScreen");
					SetGenderMenuParams();
                }
                if (currentOptionText == "Female")
                {
					Player.Female();
                    UI.SetScreen("NewGameScreen");
					SetGenderMenuParams();
                }
                if (currentOptionText == "Random")
                {
                    UI.SetScreen("NewGameScreen");
                }
                if (currentOptionText == "Back")
                {
                    UI.SetScreen("NewGameScreen");
                }
            }
            else if (UI.currentScreen == "NameScreen")
            {
                if (currentOptionText == "Back")
                {
                    UI.SetScreen("NewGameScreen");
                }
				else if (currentOptionText == "Enter Name")
				{
					UI.SetScreen("NameEnter");
					

                    if (UI.GetMenu().GetCurrentOptionText() == "Save Name")
                    {
                        NameEnter();
                        UI.SetScreen("NewGameScreen");
						SetNameMenuParams(); // Updates New Name on Select screen
                    }
                }
				else if(currentOptionText == "Random Name")
				{
					RandomName();
                    UI.SetScreen("NewGameScreen");
                    SetNameMenuParams();
                }
            }
            else if (UI.currentScreen == "AttributesScreen")
            {
				if (currentOptionText == "Health:")
				{
					Player.IncreaseAttribute = "Health";
					Player.DecreaseAttrtibute = "Health";
					SetAttributesMenuParams();
				} 
				if (currentOptionText == "Attack:")
                {
                    Player.IncreaseAttribute = "Attack";
                    Player.DecreaseAttrtibute = "Attack";
                    SetAttributesMenuParams();
                }
				if (currentOptionText == "Back")
                {
                    UI.SetScreen("NewGameScreen");
                }
            }
            else if (UI.currentScreen == "RandomizeScreen")
            {
                
                if (currentOptionText == "Back")
                {
                    UI.SetScreen("NewGameScreen");
                    SetGuildMenuParams();
                    SetGenderMenuParams();
                    SetNameMenuParams();
                }
            }
            else if (UI.currentScreen == "HistoryScreen")
			{
				// For now only the Back option is available.
				if (currentOptionText == "Back")
				{
					UI.SetScreen("StartScreen");
				}
			}
			else if (UI.currentScreen == "MazeScreen")
			{
				// TODO: Use a key or an option to get back from the maze to the game menus.
			}
		}

		void NameEnter()
		{
            Console.CursorVisible = true;
            Player.Name = Console.ReadLine();
            Console.CursorVisible = false;
		}

		void RandomName()
		{
            NameBase namebase = new NameBase();
            int randomName;
            if (Player.Gender == "Male")
			{
				randomName = rand.Next(namebase.maleNames.Length);
				Player.Name = namebase.maleNames[randomName];
            }
			else if (Player.Gender == "Female")
			{
				randomName = rand.Next(namebase.femaleNames.Length);
                Player.Name = namebase.femaleNames[randomName]; ;
            }
			else
			{
			}
			
		}

        void RandomGender()
        {
            int randomGender = rand.Next(1, 3);
            if (randomGender == 1)
            {
                Player.Male();
            }
            else if (randomGender == 2)
            {
                Player.Female();
            }
        }

        void RandomGuild()
        {
            Player.GuildChecker = rand.Next(1, 3);
            Player.Guilds();
        }

        void SetGuildMenuParams()
        {
			if (Player.Guild != null)
			{
				UI.GetMenu().OptionParams[0] = Player.Guild;
			}
		}

		void SetAttributesMenuParams()
		{
			if (UI.currentScreen == "AttributesScreen")
			{
				UI.GetMenu().OptionParams[0] = Player.MaxStats.ToString();
				UI.GetMenu().OptionParams[1] = Player.Health.ToString();
				UI.GetMenu().OptionParams[2] = Player.Attack.ToString();
			}
		}

        void SetNameMenuParams()
        {
            if (Player.Name != null)
            {
                UI.GetMenu().OptionParams[2] = Player.Name;
            }

        }

        void SetGenderMenuParams()
        {
            if (Player.Gender != null)
            {
                UI.GetMenu().OptionParams[1] = Player.Gender;
            }

        }
    }
}
