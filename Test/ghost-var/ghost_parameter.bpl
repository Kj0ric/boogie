// Test case for invalid ghost variable with parameter
procedure GhostWithParameter()
{
    var {:ghost 12} g1:int;     // ERROR: ghost variable takes no argument
    var {:ghost (1+2)} g2:int;  // ERROR: ghost variable takes no argument
    var a:int;
    var b:int;

    a := b * g1;
    b := g2 + 1;
}