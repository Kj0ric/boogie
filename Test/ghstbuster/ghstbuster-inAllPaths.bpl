procedure InitInLaterBlock()
{
  var {:ghost} g: int;
  var b: bool;
  havoc b; // non-deterministic choice

  if (b) {
    // g is not initialized here
  } else {
    g := 100;
  }
  
  // We already know if b is false g is initialized and the program verifies.
  assert b;  // Test when b is true

  assume {:ghstbuster} g > 0 || !b; // This should pass
}
