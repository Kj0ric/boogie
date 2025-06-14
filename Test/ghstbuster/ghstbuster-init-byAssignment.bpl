// Should verify since g is initialized
procedure InitGhost()
{
  var {:ghost} g: int;
  var x: int;

  g := 10;
  x := 5;
  assume {:ghstbuster} g > x; 
}