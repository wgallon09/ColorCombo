ColorCombo
==========

This program takes a destination pair of colors and a list of color keys, and recursively finds a path between the destination pair

This program was created in VS2012, but should not have a problem running in earlier versions if you copy the source code into a new project.

Original Prompt:

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
