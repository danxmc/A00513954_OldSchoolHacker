using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour {
    #region Class Attributes
    //Attributes
    const string menuHint = "You can type menu at any time.";

    //These arrays will hold  the passwords of the different game's levels
    string[] passwordsLevel1 = { "book", "class", "teacher", "room", "hour" };
    string[] passwordsLevel2 = { "cashier", "department", "payment", "electronics" };
    string[] passwordsLevel3 = { "dossier", "international", "security" };

    //Enum type for the different game states
    //Variable to hold game state
    int level;
    enum GameState { MainMenu, Password, Win, MSF };
    GameState currentScreen = GameState.MainMenu;
    string password;

    //Variable receives the user's input
    string input;

    // Variable to check the phase of the msfconsole mode
    int state = 0;
    // Hints for msf
    string[] hints = {
            "(Hint: use a Handler e.g. use multi/handler)",
            "(Hint: now set a payload e.g. set PAYLOAD windows/meterpreter/reverse_tcp)",
            "(Hint: set attacker IP e.g. LHOST 192.168.1.70)",
            "(Hint: set local port e.g. LPORT 4444)",
            "(Hint: exploit :D e.g exploit)"
    };
    string[] banners = {
            @"%clr
 ______________________________________________________________________________
|                                                                              |
|                          %bld3Kom SuperHack II Logon%clr                             |
|______________________________________________________________________________|
|                                                                              |
|                                                                              |
|                                                                              |
|                 User Name:          [   %redsecurity%clr]                        |
|                                                                              |
|                 Password:           [               ]                        |
|                                                                              |
|                                                                              |
|                                                                              |
|                                   %bld[OK]%clr                                     |
|______________________________________________________________________________|
|                                                                              |
|                                                       https://metasploit.com |
|______________________________________________________________________________|%clr",
            @"     ,           ,
    /             \
   ((__---,,,---__))
      (_) O O (_)_________
         \ _ /            |\
          o_o \   M S F   | \
               \   _____  |  *
                |||   WW|||
                |||     |||",
            @"# cowsay++
 ____________
< metasploit >
 ------------
       \   ,__,
        \  (oo)____
           (__)    )\
              ||--|| *",
            @" _                                                    _
/ \    /\         __                         _   __  /_/ __
| |\  / | _____   \ \           ___   _____ | | /  \ _   \ \
| | \/| | | ___\ |- -|   /\    / __\ | -__/ | || | || | |- -|
|_|   | | | _|__  | |_  / -\ __\ \   | |    | | \__/| |  | |_
      |/  |____/  \___\/ /\ \\___/   \/     \__|    |_\  \___\",
            @"%whiIIIIII    %reddTb.dTb%clr        _.---._
%whi  II     %red4'  v  'B%clr   .'"".'/|\`.""'.
%whi  II     %red6.     .P%clr  :  .' / | \ `.  :
%whi  II     %red'T;. .;P'%clr  '.'  /  |  \  `.'
%whi  II      %red'T; ;P'%clr    `. /   |   \ .'
%whiIIIIII     %red'YvP'%clr       `-.__|__.-'

I love shells --egypt",
            @"%clr%whi
  Metasploit Park, System Security Interface
  Version 4.0.5, Alpha E
  Ready...
  > %bldaccess security%clr
  access: PERMISSION DENIED.
  > %bldaccess security grid%clr
  access: PERMISSION DENIED.
  > %bldaccess main security grid%clr
  access: PERMISSION DENIED....and...
  %redYOU DIDN'T SAY THE MAGIC WORD!
  YOU DIDN'T SAY THE MAGIC WORD!
  YOU DIDN'T SAY THE MAGIC WORD!
  YOU DIDN'T SAY THE MAGIC WORD!
  YOU DIDN'T SAY THE MAGIC WORD!
  YOU DIDN'T SAY THE MAGIC WORD!
  YOU DIDN'T SAY THE MAGIC WORD!%clr",
            @"%clr
%bluMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM%clr
%bluMMMMMMMMMMM                MMMMMMMMMM%clr
%bluMMMN$                           vMMMM%clr
%bluMMMNl%clr  %bldMMMMM             MMMMM%clr  %bluJMMMM%clr
%bluMMMNl%clr  %bldMMMMMMMN       NMMMMMMM%clr  %bluJMMMM%clr
%bluMMMNl%clr  %bldMMMMMMMMMNmmmNMMMMMMMMM%clr  %bluJMMMM%clr
%bluMMMNI%clr  %bldMMMMMMMMMMMMMMMMMMMMMMM%clr  %blujMMMM%clr
%bluMMMNI%clr  %bldMMMMMMMMMMMMMMMMMMMMMMM%clr  %blujMMMM%clr
%bluMMMNI%clr  %bldMMMMM   MMMMMMM   MMMMM%clr  %blujMMMM%clr
%bluMMMNI%clr  %bldMMMMM   MMMMMMM   MMMMM%clr  %blujMMMM%clr
%bluMMMNI%clr  %bldMMMNM   MMMMMMM   MMMMM%clr  %blujMMMM%clr
%bluMMMNI%clr  %bldWMMMM   MMMMMMM   MMMM#%clr  %bluJMMMM%clr
%bluMMMMR%clr  %bld?MMNM             MMMMM%clr %blu.dMMMM%clr
%bluMMMMNm%clr %bld`?MMM             MMMM`%clr %bludMMMMM%clr
%bluMMMMMMN%clr  %bld?MM             MM?%clr  %bluNMMMMMN%clr
%bluMMMMMMMMNe%clr                 %bluJMMMMMNMMM%clr
%bluMMMMMMMMMMNm,%clr            %blueMMMMMNMMNMM%clr
%bluMMMMNNMNMMMMMNx%clr        %bluMMMMMMNMMNMMNM%clr
%bluMMMMMMMMNMMNMMMMm+..+MMNMMNMNMMNMMNMM%clr
%clr%bld        https://metasploit.com",
            @"%clr                          ########                  #
                      #################            #
                   ######################         #
                  #########################      #
                ############################
               ##############################
               ###############################
              ###############################
              ##############################
                              #    ########   #
                 %red##%clr        %red###%clr        ####   ##
                                      ###   ###
                                    ####   ###
               ####          ##########   ####
               #######################   ####
                 ####################   ####
                  ##################  ####
                    ############      ##
                       ########        ###
                      #########        #####
                    ############      ######
                   ########      #########
                     #####       ########
                       ###       #########
                      ######    ############
                     #######################
                     #   #   ###  #   #   ##
                     ########################
                      ##     ##   ##     ##
                            https://metasploit.com%clr",
            @"
                   __
                  |  |
                  |  |
              ___/____\___
         _- ~              ~  _
      - ~                      ~ -_
    -                               _
  -         /\            /\          _
 -         / *\          / *\          _
_         /____\        /____\          _
_                  /\                   _
_                 /__\                  _
_      |\                      /|       _
 -     \ `\/\/\/\/\/\/\/\/\/\/' /      _
  -     \                      /      -
    ~    `\/^\/^\/^\/^\/^\/^\/'      ~
      ~                            -~
       `--_._._._._._._._._._.._--'",
            @"%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%     %%%         %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%  %%  %%%%%%%%   %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%  %  %%%%%%%%   %%%%%%%%%%% https://metasploit.com %%%%%%%%%%%%%%%%%%%%%%%%
%%  %%  %%%%%%   %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%  %%%%%%%%%   %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%%%%  %%%  %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%%%    %%   %%%%%%%%%%%  %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%  %%%  %%%%%
%%%%  %%  %%  %      %%      %%    %%%%%      %    %%%%  %%   %%%%%%       %%
%%%%  %%  %%  %  %%% %%%%  %%%%  %%  %%%%  %%%%  %% %%  %% %%% %%  %%%  %%%%%
%%%%  %%%%%%  %%   %%%%%%   %%%%  %%%  %%%%  %%    %%  %%% %%% %%   %%  %%%%%
%%%%%%%%%%%% %%%%     %%%%%    %%  %%   %    %%  %%%%  %%%%   %%%   %%%     %
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%  %%%%%%% %%%%%%%%%%%%%%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%          %%%%%%%%%%%%%%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%",
            @"%clr%red               .;lxO0KXXXK0Oxl:.
           ,o0WMMMMMMMMMMMMMMMMMMKd,
        'xNMMMMMMMMMMMMMMMMMMMMMMMMMWx,
      :KMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMK:
    .KMMMMMMMMMMMMMMMWNNNWMMMMMMMMMMMMMMMX,
   lWMMMMMMMMMMMXd:..     ..;dKMMMMMMMMMMMMo
  xMMMMMMMMMMWd.               .oNMMMMMMMMMMk
 oMMMMMMMMMMx.                    dMMMMMMMMMMx
.WMMMMMMMMM:                       :MMMMMMMMMM,
xMMMMMMMMMo                         lMMMMMMMMMO
NMMMMMMMMW                    ,cccccoMMMMMMMMMWlccccc;
MMMMMMMMMX                     ;KMMMMMMMMMMMMMMMMMMX:
NMMMMMMMMW.                      ;KMMMMMMMMMMMMMMX:
xMMMMMMMMMd                        ,0MMMMMMMMMMK;
.WMMMMMMMMMc                         'OMMMMMM0,
 lMMMMMMMMMMk.                         .kMMO'
  dMMMMMMMMMMWd'                         ..
   cWMMMMMMMMMMMNxc'.%clr%whi                ##########%clr
%red    .0MMMMMMMMMMMMMMMMWc%clr%whi            #+#    #+#%clr
%red      ;0MMMMMMMMMMMMMMMo.%clr%whi          +:+%clr
%red        .dNMMMMMMMMMMMMo%clr          +%whi#+%clr+:++#+
%red           'oOWMMMMMMMMo%clr                +:+
%red               .,cdkO0K;%clr        :+:    :+:                                
                                :::::::+:
           %whiMetasploit%clr %yelUnder Construction%clr"

    };
    #endregion

    // Use this for initialization
    void Start () {
        ShowMainMenu();
	}

    // Update is called once per frame
    void Update()
    {

    }

    private void ShowMainMenu()
    {
        //Clear screen
        Terminal.ClearScreen();

        currentScreen = GameState.MainMenu;
        Terminal.ClearScreen();
        Terminal.WriteLine("What do you want to hack?");
        Terminal.WriteLine("1. Town's college");
        Terminal.WriteLine("2. City's Super Center");
        Terminal.WriteLine("3. NSA server");
        Terminal.WriteLine("4. Fuck it, use msfconsole");
        Terminal.WriteLine("");
        Terminal.WriteLine("Option>");

        //Set current screen to main menu
        currentScreen = GameState.MainMenu;
    }

    //  The OnUserInput method is special.  It is called everytime the user
    //  hits enter on their keyboard.  This method will let us evaluate the
    //  input data and act accordingly.
    void OnUserInput(string input)
    {
        //If user inputs the 'menu' keyword, then we call the ShowMainMenu() method once more
        if (input == "menu")
        {
            ShowMainMenu();
        }
        //If the user types quit, close, exit then we try to close our game,
        //If the game is played on web browser, then we ask user to close the browser's tab.
        else if (input == "quit" || input == "close" || input == "exit") {
            Terminal.WriteLine("Please, close the browser's tab");
            Application.Quit();
        }

        //If the user inputs anything that is not menu, quit, close, or exit then we are going to handle
        //that input depending on the game state. If the game state is still MainMenu, then we call the RunMainMenu() method.
        else if(currentScreen == GameState.MainMenu)
        {
            RunMainMenu(input);
        }
        //But if the current game state is password, then we call the CheckPassword() method.
        else if (currentScreen == GameState.Password)
        {
            CheckPassword(input);
        } //If the user is on the msfconsole
        else if (currentScreen == GameState.MSF)
        {
            CheckCommands(input);
        }
    }

    private void CheckCommands(string input)
    {
        //Converts all input to lower case
        input = input.ToLower();

        // If user wants banner give banner つ ◕_◕ ༽つ
        if (input == "banner")
        {
            Terminal.ClearScreen();
            Terminal.WriteLine(banners[UnityEngine.Random.Range(0, banners.Length)]);
        }

        // Checks the different msfconsole states,
        // During each stage it checks if the commands are appropiate, else it reapeats the hint
        // until the user inputs a valid command and passing to the next stage
        // stage: use handler
        if (state == 0)
        {
            if (input.StartsWith("use "))
            {
                StartCoroutine(WaitMsfConsole(.5f, "msf exploit(handler) >"));
                //Terminal.WriteLine("msf exploit(handler) >");
                state = 1;
                StartCoroutine(WaitMsfConsole(.1f, hints[state]));
                //Terminal.WriteLine(hints[state]);
            } else
            {
                StartCoroutine(WaitMsfConsole(.1f, hints[state]));
                //Terminal.WriteLine(hints[state]);
            }
        } else if (state == 1) { // set payload
            if (input.StartsWith("set payload "))
            {
                //Terminal.WriteLine("payload => set");
                //Terminal.WriteLine("msf exploit(handler) >");
                StartCoroutine(WaitMsfConsole(.5f, "payload => set"));
                StartCoroutine(WaitMsfConsole(.6f, "msf exploit(handler) >"));
                state = 2;
                StartCoroutine(WaitMsfConsole(.1f, hints[state]));
            } else
            {
                StartCoroutine(WaitMsfConsole(.1f, hints[state]));
                StartCoroutine(WaitMsfConsole(.5f, "msf exploit(handler) >"));
                //Terminal.WriteLine(hints[state]);
            }
        } else if (state == 2) { //set attacker
            if (input.StartsWith("set lhost "))
            {
                //Terminal.WriteLine("LHOST => set");
                //Terminal.WriteLine("msf exploit(handler) >");
                StartCoroutine(WaitMsfConsole(.5f, "LHOST => set"));
                StartCoroutine(WaitMsfConsole(.6f, "msf exploit(handler) >"));
                state = 3;
                StartCoroutine(WaitMsfConsole(.1f, hints[state]));
            } else
            {
                StartCoroutine(WaitMsfConsole(.1f, hints[state]));
                StartCoroutine(WaitMsfConsole(.5f, "payload => set"));
                StartCoroutine(WaitMsfConsole(.6f, "msf exploit(handler) >"));
                //Terminal.WriteLine(hints[state]);
            }
        } else if (state == 3) //set port
        {
            if (input.StartsWith("set lport "))
            {
                //Terminal.WriteLine("LPORT => set");
                //Terminal.WriteLine("msf exploit(handler) >");
                StartCoroutine(WaitMsfConsole(.5f, "LPORT => set"));
                StartCoroutine(WaitMsfConsole(.6f, "msf exploit(handler) >"));
                state = 4;
                StartCoroutine(WaitMsfConsole(.1f, hints[state]));
            }
            else
            {
                StartCoroutine(WaitMsfConsole(.1f, hints[state]));
                StartCoroutine(WaitMsfConsole(.5f, "LHOST => set"));
                StartCoroutine(WaitMsfConsole(.6f, "msf exploit(handler) >"));
                //Terminal.WriteLine(hints[state]);
            }
        } else if (state == 4) { //exploit
            if (input.StartsWith("exploit"))
            {
                StartCoroutine(WaitMsfConsole(.5f, "[*] Started reverse TCP handler"));
                StartCoroutine(WaitMsfConsole(2f, "[*] Starting the payload handler..."));
                StartCoroutine(WaitMsfConsole(5f, "[*] Sending stage (957487 bytes)"));
                StartCoroutine(WaitMsfConsole(8f, "[*] Meterpreter session 1 opened"));
                StartCoroutine(WaitMsfConsole(8.1f, "C:\\"));
                /*Terminal.WriteLine("[*] Started reverse TCP handler");
                Terminal.WriteLine("[*] Starting the payload handler...");
                Terminal.WriteLine("[*] Sending stage (957487 bytes)");
                Terminal.WriteLine("[*] Meterpreter session 1 opened");*/
                state = 0;
                DisplayWinScreen();
            }
            else
            {
                StartCoroutine(WaitMsfConsole(.1f, hints[state]));
                StartCoroutine(WaitMsfConsole(.5f, "LPORT => set"));
                StartCoroutine(WaitMsfConsole(.6f, "msf exploit(handler) >"));
                //Terminal.WriteLine(hints[state]);
            }
        }


    }

    //Timeouts for the msfconsole
    private IEnumerator WaitMsfConsole(float sec,string s)
    {
        yield return new WaitForSeconds(sec);
        Terminal.WriteLine("");
        Terminal.WriteLine(s);
    }

    //  This method validates if the password the user entered is the correct
    //  one.  If it is, then we display the win screen, otherwise we kee asking
    //  for the correct password.
    private void CheckPassword(string input)
    {
        if (input == password)
        {
            DisplayWinScreen();
        }
        else
        {
            AskForPassword();
        }
    }

    //  This method updates the current game state and displays the win screen.
    private void DisplayWinScreen()
    {
        currentScreen = GameState.Win;

        Terminal.ClearScreen();

        ShowLevelReward();
        Terminal.WriteLine(menuHint);
    }

    private void ShowLevelReward()
    {
        //  Depending on the level, this method shows a different reward.
        //  If by any chance level is not among the valid level numbers, then
        //  we log an error.
        switch (level)
        {
            case 1:
                Terminal.WriteLine("Grab a lime!");
                Terminal.WriteLine(@"
     /
  __|__
 /     \
|       )
 \_____/
                ");
                break;
            case 2:
                Terminal.WriteLine("Have a book...");
                Terminal.WriteLine(@"
    _______
   /      //
  /      //
 /______//
(______(/
                ");
                break;
            case 3:
                Terminal.WriteLine("Greetings...");
                Terminal.WriteLine(@"
 _ __   ___  __ _
| '_ \ / __|/ _` |
| | | |\__ \ (_| |
|_| |_||___)\__,_|
                ");
                Terminal.WriteLine("Welcome to the NSA server");
                break;
            case 4:
                Terminal.WriteLine("Congratulations... you have complete access to a remote machine");
                break;
            default:
                Debug.LogError("Invalid level reached.");
                break;
        }
    }

    void RunMainMenu(string input)
    {
        //We first check that the input is a valid input
        bool isValidInput = (input == "1") || (input == "2") || (input == "3") || (input == "4");

        //If the user inputs a valid level, we convert that input to an int value and then we call the
        //AskForPassword() method.
        if (isValidInput)
        {
            level = int.Parse(input);

            // If the user selected one of the first three options...
            if (level != 4)
            {
                //We call the SetRandomPassword() method
                SetRandomPassword();

                AskForPassword();
            } else // Show msfconsole
            {
                StartCoroutine(ShowMsfConsole());
            }

            
        }

        //If the user did not enter a valid input, then we validate for our Easter Egg
        // If the user enters "007", we greet them as Mr. Bond
        else if(input == "007")
        {
            Terminal.WriteLine("Please enter a valid level, Mr. Bond");
        }
        // If not ask them to enter a valid level.
        else
        {
            Terminal.WriteLine("Enter a valid level");
        }
    }

    // The user selected to use metasploit instead of guessing a password
    private IEnumerator ShowMsfConsole()
    {
        currentScreen = GameState.MSF;
        Terminal.ClearScreen();
        Terminal.WriteLine("つ ◕_◕ ༽つ Write banner for real life Metasploit banners");
        Terminal.WriteLine("./msfconsole");
        yield return new WaitForSeconds(.5f);
        Terminal.WriteLine("[*] Starting the Metasploit Framework console...");
        yield return new WaitForSeconds(4);
        Terminal.WriteLine(banners[UnityEngine.Random.Range(0, banners.Length)]);
        Terminal.WriteLine("(Hint: use a Handler e.g. use multi/handler)");
    }

    private void AskForPassword()
    {
        //We set our current game state as Password
        currentScreen = GameState.Password;

        Terminal.ClearScreen();

        Terminal.WriteLine("Enter your password. Hint: " + password.Anagram());
        Terminal.WriteLine(menuHint);
    }

    private void SetRandomPassword()
    {
        //Depending on the selected level, we choose a random password to crack
        switch(level)
        {
            case 1:
                password = passwordsLevel1[UnityEngine.Random.Range(0, passwordsLevel1.Length)];
                break;
            case 2:
                password = passwordsLevel2[UnityEngine.Random.Range(0, passwordsLevel2.Length)];
                break;
            case 3:
                password = passwordsLevel3[UnityEngine.Random.Range(0, passwordsLevel3.Length)];
                break;
            default:
                Debug.LogError("Invalid level. How did you manage that?");
                break;
        }
    }

}
