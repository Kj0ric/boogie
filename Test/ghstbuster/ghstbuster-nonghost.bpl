// Should verify since x is not ghost, so its initialization state is not checked by ghstbuster
procedure NonGhostUninit()
{
  var x: int; 
  assume {:ghstbuster} x > 0; 
}