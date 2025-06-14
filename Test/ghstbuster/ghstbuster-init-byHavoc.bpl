procedure InitByHavoc()
{
  var {:ghost} g_havoc: int;
  havoc g_havoc; // g_havoc is considered initialized by havoc
  assume {:ghstbuster} g_havoc > 0 || g_havoc <= 0; // Should pass
}