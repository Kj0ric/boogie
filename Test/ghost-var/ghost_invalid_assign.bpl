// Test case for invalid ghost variable assignment
procedure InvalidGhostUsage()
{
    var {:ghost} g:int;
    var i:int;
    
    g := 10;  // OK: Assign to ghost
    i := g;   // ERROR: Cannot assign ghost variable to non-ghost variable
}