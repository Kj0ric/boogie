procedure WrongCommand_Assert()
{
  var x: int;
  x := 1;
  assert {:ghstbuster} x > 0; // ERROR: ghstbuster only on assume
}

procedure WrongCommand_Havoc()
{
  var x: int;
  havoc {:ghstbuster} x; // ERROR: ghstbuster only on assume
}