# Fill a box with cubes
This repository contains an algorythmic solution to a specific problem. 
This is no library and unless you encounter a similar problem, this is probably of no use to you. 

What is this?
--------------
1. [The task](#the-task)
2. [Run instructions](#how-to-run)

# The task

You are given a box with integer dimensions length x width x height. You
also have a set of cubes whose sides are powers of 2, e.g. 1x1x1, 2x2x2,
4x4x4 etc.
You need to fill the box with cubes from the set.
Write a program that for a given box and given set of cubes can determine
the smallest number of cubes needed to fill the box.
The set of cubes can be represented for instance as a list or array of
numbers, where the position in the list designates the dimension of the
cube. E.g. 100 10 1 means you have 100 cubes of 1x1x1 and 10 cubes of 2x2x2
and 1 cube of 4x4x4.
A problem specification is a sequence of lines separated by newline. Each
line has the box dimensions as the first three elements and the remaining
elements enumerate the given cubes. Elements are separated by a single
space. Lines are terminated by your platform’s newline convention.  
E.g.  
2 3 4 5 6  
7 8 9 1 2 3 4   
specifies two problems:  
a box with dimensions 2 x 3 x 4, 5 cubes of 1x1x1 and 6 cubes of 2x2x2  
a box with dimensions 7 x 8 x 9, 1 cube of 1x1x1, 2 of 2x2x2, 3 of 4x4x4, and 4 of 8x8x8.  
Your program should read one or more problem specifications from stdin and
print the answer to each problem on stdout. Spend as little effort as
possible on parsing and validation of the input. An unsolvable problem
should yield -1. Please provide instructions on how to run / compile your
program.  
Examples:  
Assume the file ‘problems.txt’ contains:  
10 10 10 2000   
10 10 10 900  
4 4 8 10 10 1  
5 5 5 61 7 1  
5 5 6 61 4 1   
1000 1000 1000 0 0 0 46501 0 2791 631 127 19 1  
1 1 9 9 1  
Then executing
./myprogram < problems.txt
should print the following to stdout:  
1000  
-1  
9  
62  
59  
50070  
9  

# How to run

## Compiling with Visual Studio
1. Download this repository using git clone command (click green "Clone or download" button on the top for more specific instruction).
2. Unless you have it, download Visual Studio (preferably 2019). You can use community or express version if you wish.
3. Run Visual Studio, and open the "BoxFiller.sln" file inside the "src" folder
3. Click on the green arrow run button

## How it works 
Should you have the solution compiled (you can use any other method of running the application if you wish to do so)
1. Type input data using the format provided in the [task description](#the-task) (**box_x box_y box_z list_of_quantities_with_spaces_between** and press Enter
2. The solution will be displayed to you in the next line
3. The application sould still be working after, so you can proceed with typing next input values
4. To **stop** the application you can type any of those: **"exit", "quit", "finish", "end"**
