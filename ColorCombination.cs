/*
 * William L. Gallon
 * 4/29/13
 * This program was created to solve the following prompt: */
#region Original Prompt
/*
The Challenge 

You recently had a state of the art security system installed in your home. The master control panel 
requires a series of bi‐colored chips to be placed end to end in a specific sequence in order to gain
access. The security provider split up the chips and gave a random number to each of your family
members. All of you must convene in order to assemble the chips and create the correct color
combination.
 
The access panel has a channel for the security chips. On each end of the channel is a colored marker. 
Chips are placed end to end such that the adjacent colors match and the starting and ending chips are 
color matched to the corresponding markers. 
Write a program to see if the family has all the chips necessary to unlock the master control panel. 

Input 

The input consists of a single line indicating the beginning and ending marker colors followed by a series 
of chip definitions. All lines consist of a pair of color indicators; you may use integers, strings, or 
characters to represent each color. For our example purposes, we will use strings.  

Output 

If the combination cannot be achieved by using all of the chips once and only once, then report “Cannot 
unlock master panel”. If the combination can be achieved, then print the chips in the order required to 
unlock the master control.

Examples:
 
One 

Input:
Blue, Green
Blue, Yellow 
Red, Orange 
Red, Green 
Yellow, Red 
Orange, Purple 

Output:
Cannot unlock master panel

Two

Input:
Blue, Green
Blue, Yellow 
Red, Orange 
Red, Green 
Yellow, Red
Orange, Red 

Output:
Blue, Yellow 
Yellow, Red 
Red, Orange 
Orange, Red 
Red, Green

Three 

Input:
Blue, Green 
Blue, Green 
Blue, Yellow 
Green, Yellow 
Orange, Red
Red, Green 
Red, Orange 
Yellow, Blue 
Yellow, Red 

Output:
Blue, Yellow 
Yellow, Red 
Red, Orange
Orange, Red 
Red, Green 
Green, Yellow 
Yellow, Blue
Blue, Green
 */
#endregion
/*
 * This project will take a given set of given color pair inputs, with the first pair being the starting
 * and ending values desired.  The following pairs are used to make a line from the first value to the
 * last value, with each pair being used once and all pairs being used.  This is done by using a list of
 * keyvaluepairs, so that multiple keys can be used.  Then the list is run through a recursive method that
 * finds if there is a successful path with the input given, and will output what path was used.  This program will
 * only give the first successful path found, even if there are multiple paths that could be used.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace ColorCombination
{
    class Program
    {
        //Uses a list of keyvaluepairs to retain much of the functionality of a dictionary, but with the ability to have duplicate keys
        public static List<KeyValuePair<string, string>> order = new List<KeyValuePair<string, string>>();
        static void Main(string[] args)
        {
            string start = "", end = "";
            List<KeyValuePair<string, string>> keys = new List<KeyValuePair<string, string>>();
            List<int> used = new List<int>(); //0 if unused, 1 if used
            bool success = false;

            //get input here
            #region read from txt file

            //Reads input from a text file named "input.txt" in the same directory as the executable, then does some basic
            //input sanitizing to ensure the input is in the format "color, color".  It will correct capitalization if this is true,
            //and then fill out the list of keys as well as the starting and ending color strings.

            bool firstRead = true; //variable used when reading from a text file
            using (StreamReader infile = new StreamReader("input.txt"))
            {
                string temp;
                string[] colors, delim = new string[] {","};
                string regex = @"^[a-zA-Z]*,\s?[a-zA-Z]*$";
                Match input;
                while (!infile.EndOfStream)
                {
                    temp = infile.ReadLine();
                    input = Regex.Match(temp, regex, RegexOptions.IgnoreCase);

                    if (!input.Success)
                    {
                        //Alerts the user that there is a line that is invalid for this test, and then shows them the line.
                        Console.WriteLine("Invalid input found:");
                        Console.WriteLine(temp);
                        return;
                    }

                    colors = temp.Split(delim, StringSplitOptions.None);

                    for(int i=0; i<colors.Length; i++)
                    {
                        //Ensures correct capitalization and no leading spaces
                        colors[i] = colors[i].Trim();
                        colors[i] = colors[i].Substring(0, 1).ToUpper() + colors[i].Substring(1).ToLower();
                    }

                    if (firstRead)
                    {
                        start = colors[0];
                        end = colors[1];
                        firstRead = false;
                    }
                    else
                    {
                        keys.Add(new KeyValuePair<string, string>(colors[0], colors[1]));
                    }
                }
            }

            #endregion

            if (keys.Count == 0)
            {
                Console.WriteLine("Cannot unlock master panel");
                return;
            }

            //set all keys to unused
            for (int i = 0; i < keys.Count; i++)
            {
                used.Add(0);
            }

            //make the call to the recursive function checkKey and see if panel can be unlocked
            success = checkKey(start, end, keys, used);

            if (success)
            {
                for (int i = order.Count - 1; i > -1; i--)
                {
                    Console.WriteLine(order[i].Key + ", " + order[i].Value);
                }
            }
            else
                Console.WriteLine("Cannot unlock master panel");

        }

        static bool checkKey(string color, string end, List<KeyValuePair<string, string>> keys, List<int> used)
        {
            int count = 0; //keeps a count of which key is being on, so that the List keys and the List used
            //will stay synced when checking for used/unused keys.

            bool success = false;

            if (!used.Contains(0) && color == end)
            {
                //if all keys are counted as used, then a successful combination exists.  This will end the stack
                //as a successful run.
                return true;
            }
            foreach (KeyValuePair<string, string> set in keys)
            {
                if (set.Key == color && used[count] == 0)
                {
                    //if the left-hand side of the key matches the right-hand side of the previous key, and it has not
                    //been used, it will use this key as the next key in the combination.

                    used[count] = 1;//ensures keys are only used once
                    success = checkKey(set.Value, end, keys, used);//recursively runs the checkKey function, with the new set
                    //of used keys, and the previous color that needs to be matched.

                    if (success)
                    {
                        //Keeps track of the stack as it is resolved, and saves the keys in reverse order of how they were used.
                        //This unfortunately needed to be a global list, so that it persisted through stack resolution

                        order.Add(new KeyValuePair<string, string>(set.Key, set.Value));
                        return true;
                    }
                    else
                    {
                        //If this key was not able to be used to create a successful combination, release it to be used again
                        //in the next combination attempt.
                        used[count] = 0;
                    }
                 }
                count++;
             }
             return false;
        }
    }
}