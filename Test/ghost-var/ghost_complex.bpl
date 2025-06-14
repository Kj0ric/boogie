// Test case for ghost variables in complex expressions
procedure ComplexExpressions()
{
    var {:ghost} g:int;
    var i:int;
    var j:int;
    
    g := 10;
    i := 5;
    
    // This should error because g (ghost) is used in the expression
    j := i * (i + g);  // ERROR: Cannot use ghost variable in computation for non-ghost
}