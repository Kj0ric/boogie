// Test case with multiple ghost variables in expression
procedure MultipleGhosts()
{
    var {:ghost} g1:int;
    var {:ghost} g2:int;
    var i:int;
    
    g1 := 5;
    g2 := 10;
    
    // Should error - expression contains multiple ghost variables
    i := g1 + g2;  // ERROR: Cannot assign ghost variables to non-ghost variable
}