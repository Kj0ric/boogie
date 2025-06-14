using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Microsoft.Boogie;

public class VariableCollector : ReadOnlyVisitor
{
  private bool _ignoreOld;

  protected HashSet<Variable> _usedVars;

  public IEnumerable<Variable> usedVars
  {
    get { return _usedVars.AsEnumerable(); }
  }

  /* Harun Yılmaz 06.05.2025 */
  // Check if any ghost variables were found
  public bool ContainsGhostVariables => usedVars.Any(v => v.IsGhost());
  // Get the names of ghost variables (useful for error messages)
  public IEnumerable<string> GhostVariableNames => usedVars.Where(v => v.IsGhost()).Select(v => v.Name);
  /* Harun Yılmaz 06.05.2025 */

  protected HashSet<Variable> _oldVarsUsed;

  public IEnumerable<Variable> oldVarsUsed
  {
    get { return _oldVarsUsed.AsEnumerable(); }
  }

  [ContractInvariantMethod]
  void ObjectInvariant()
  {
    Contract.Invariant(Cce.NonNullElements(_usedVars));
    Contract.Invariant(Cce.NonNullElements(_oldVarsUsed));
  }

  int insideOldExpr;

  public VariableCollector(bool ignoreOld = false)
  {
    _ignoreOld = ignoreOld;
    _usedVars = new HashSet<Variable>();
    _oldVarsUsed = new HashSet<Variable>();
    insideOldExpr = 0;
  }

  public override Expr VisitOldExpr(OldExpr node)
  {
    if (!_ignoreOld)
    {
      insideOldExpr++;
      node.Expr = this.VisitExpr(node.Expr);
      insideOldExpr--;
    }
    return node;
  }

  public override Expr VisitIdentifierExpr(IdentifierExpr node)
  {
    if (node.Decl != null)
    {
      _usedVars.Add(node.Decl);
      if (insideOldExpr > 0)
      {
        _oldVarsUsed.Add(node.Decl);
      }
    }
    return node;
  }

  public static IEnumerable<Variable> Collect(Absy node, bool ignoreOld = false)
  {
    var collector = new VariableCollector(ignoreOld);
    collector.Visit(node);
    return collector.usedVars;
  }

  public static IEnumerable<Variable> Collect(IEnumerable<Absy> nodes, bool ignoreOld = false)
  {
    var collector = new VariableCollector(ignoreOld);
    foreach (var node in nodes)
    {
      collector.Visit(node);
    }
    return collector.usedVars;
  }

  /* Harun Yılmaz 06.05.2025 */
  // Helper method to collect ghost variables from an expression
  public static IEnumerable<Variable> CollectGhostVariables(Expr expr) 
  {
    return Collect(expr).Where(v => v.IsGhost());
  }
  /* Harun Yılmaz 06.05.2025 */
}