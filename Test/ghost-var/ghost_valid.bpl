// Test case for valid ghost variable usage
procedure ValidGhostUsage()
{
    var {:ghost} g: int;
    var {:ghost} h: int;
    var i: int;
    
    g := 10;     // OK: Assign to ghost
    h := g + 5;  // OK: Ghost to ghost assignment
    i := 20;     // OK: Regular assignment
    
    assert g == 10;
    assert h == 15;
    assert i == 20;
}