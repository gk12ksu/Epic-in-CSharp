Smart Farm C#
====================

These files are the ones that are written directly from the algorithms into C#.

We are using Epic version 0810, compiling C# with Mono.

- I think the file CPTBL.f90, BSIM.f90 has variable naming defs. It doesn't have all of them there, but there are a lot there in the comments.


Todo
=============

Translate all Fortran Files

- Use Parm, I believe uses global variables. Again, this needs to be addressed when rewriting for C#

- Fortran starts indices at 1 instead of 0***
    - Some for loops might need to be changed

- Lines to uncomment once others are coded
    - BSIM.cs
        - search for //Functions to see
    - CAGRO.cs
        - search for //Functions
    - CAHU.cs
        - search for //Functions
    - CGROW.cs
    - CPTBL.cs
    - CRGBD.cs
    - CSTRS.cs
        - Line 32 issue, error with mathematical operations on an array and a single variable expression
    - SCOUNT.cs
    - SPLA.cs

- CPNM shouldn't be a double array in modparam - see 1240, 1254, in BSIM.cs

About
=============

The people in charge of these are:

Brian Cain - bccain@ksu.edu
    A-D, H, S

Heath Yates - hlyates@ksu.edu
    E
Emily Jordan - delamern@ksu.edu
    W
Paul Cain - nietz111@ksu.edu
	I,O,P,R
Brian Dye - kascade@ksu.edu
    G, some H, N
Dustin Seabourn - dustin5@ksu.edu
    
