// Test case for ghost attribute on non-variable constructs

// Error: Ghost attribute on procedure
procedure {:ghost} GhostProcedure()     // Gives type check error TWICE. WHY?
{
    var x: int;
}

// Error: Ghost attribute on axiom
axiom {:ghost} 1 + 1 == 2;

// Error: Ghost attribute on function
function {:ghost} Add(x: int, y: int) : int { x + y }

// Error: Ghost attribute on assert command
procedure AssertWithGhost()
{
    var x: int;
    x := 10;
    
    // Error: Ghost attribute on command
    assert {:ghost} x == 10;
    //{:ghost} assert x == 10;  // Gives parse error since this is not correct syntax 
    // (attribute at the start of a declaration)
}

// Valid use of ghost attribute
procedure ValidGhostUsage()
{
    var {:ghost} g: int;
    g := 5;
}