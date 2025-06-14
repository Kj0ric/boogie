// Should error: g is ghost and not initialized
procedure UninitGhost()
{
  var {:ghost} g: int;
  var x: int;
  x := 5;
  assume {:ghstbuster} g > x; // ERROR: g is uninitialized
}

procedure UninitGhost_OtherVarInitialized()
{
  var {:ghost} g1: int;
  var {:ghost} g2: int;
  g1 := 10;
  assume {:ghstbuster} g1 > 5 && g2 < 0; // ERROR: g2 is uninitialized
}