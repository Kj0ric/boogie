using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Boogie;

public interface ICarriesAttributes
{
  QKeyValue Attributes { get; set; }

  public void ResolveAttributes(ResolutionContext rc)
  {
    for (QKeyValue kv = this.Attributes; kv != null; kv = kv.Next)
    {
      kv.Resolve(rc);
    }
  }

  public void TypecheckAttributes(TypecheckingContext tc)
  {
    var oldGlobalAccessOnlyInOld = tc.GlobalAccessOnlyInOld;
    tc.GlobalAccessOnlyInOld = false;
    for (QKeyValue kv = this.Attributes; kv != null; kv = kv.Next)
    {
      /* Harun Yılmaz 06.05.2025 */
      // Check for ghost attribute on non-variables
      if (kv.Key == "ghost" && !(this is Variable))
      {
          tc.Error(kv, "attribute :ghost can only be used with variables");
      }
      /* Harun Yılmaz 06.05.2025 */

      /* Harun Yılmaz 15.05.2025 */
      if (kv.Key == "ghstbuster" && !(this is AssumeCmd))
      {
        tc.Error(kv, "attribute :ghstbuster can only be used with assume commands");
      }
      /* Harun Yılmaz 15.05.2025 */

      kv.Typecheck(tc);
    }
    tc.GlobalAccessOnlyInOld = oldGlobalAccessOnlyInOld;
  }

  public List<int> FindLayers()
  {
    List<int> layers = new List<int>();
    for (QKeyValue kv = this.Attributes; kv != null; kv = kv.Next)
    {
      if (kv.Key == CivlAttributes.LAYER)
      {
        layers.AddRange(kv.Params.Select(o => ((LiteralExpr)o).asBigNum.ToIntSafe));
      }
    }
    return layers.Distinct().OrderBy(l => l).ToList();
  }
}

public static class CarriesAttributesExtensions {
  
  // Look for {:name string} in list of attributes.
  public static string FindStringAttribute(this ICarriesAttributes destination, string name)
  {
    return QKeyValue.FindStringAttribute(destination.Attributes, name);
  }

  public static void AddStringAttribute(this ICarriesAttributes destination, IToken tok, string name, string parameter)
  {
    destination.Attributes = new QKeyValue(tok, name, new List<object>() {parameter}, destination.Attributes);
  }
  
  public static void CopyIdFrom(this ICarriesAttributes destination, IToken tok, ICarriesAttributes src)
  {
    var id = src.FindStringAttribute("id");
    if (id is not null) {
      destination.AddStringAttribute(tok, "id", id);
    }
  }

  public static void CopyIdWithModificationsFrom(this ICarriesAttributes destination, IToken tok, 
    ICarriesAttributes src, Func<string,TrackedNodeComponent> modifier)
  {
    var id = src.FindStringAttribute("id");
    if (id is not null) {
      destination.AddStringAttribute(tok, "id", modifier(id).SolverLabel);
    }
  }

  /* Harun Yılmaz 06.05.2025 */
  public static bool IsGhost(this ICarriesAttributes obj) {
    // We already ensured that only variables can have the ghost attribute in TypecheckAttributes
    return obj != null &&
          obj.Attributes != null &&
          obj.Attributes.FindBoolAttribute("ghost");
  }
  /* Harun Yılmaz 06.05.2025 */
}
