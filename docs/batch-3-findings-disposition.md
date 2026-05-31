# Batch-3 Findings & TODO Disposition (Phase 2)

> Resolves the Phase 2 acceptance gate of the 1.0 roadmap: every in-`src/` TODO and every deferred
> batch-3 adversarial-review finding is fixed, closed with rationale, or recorded as deferred work.
> Audit source: the batch-3 adversarial audit at
> `…/Adversarial Review/fixportal-fixatdl/full-audit-20260528T211015Z/`.

## Fixed
- **O-G2** — `EditValueConverter.ConvertToComparableType` now rejects a null operand with
  `InvalidFieldValueException` instead of silently coercing numerics to `0` (and NRE-ing on
  enum/MonthYear/Tenor). Guarded by `EditValueConverterTests` (`Null_value_*`). Commits `4b2984f`, `dbe1731`.
- **G-D** — `ThrowHelper` now exposes `NewWithParamName<T>(source, paramName, message)` and threads the real
  parameter name through the ArgumentException-family path (the plain `New<T>` path still defaults the
  synthetic `"Value"`, so all existing callers are behaviour-identical). Four statically-known call sites
  (`ProcessExtensions`, `Parameter_t`, `EnumState` ×2) repointed with `nameof(...)`. A distinct method name
  was used deliberately to avoid colliding with the variadic `New<T>(object?, string, params object?[])`
  overload. Guarded by `ThrowHelperTests`. Commit `4b10eac`.

## Closed with rationale (no change)
- **F7 (TagNum leading zeros)** — kept the lenient parse (`"0044"`→`44u`); rejecting leading zeros is
  enforcement-tightening, not a correctness defect, and no conforming ATDL emits leading-zero tags
  (reviewer S rated it needs-evidence). Pinned by a characterization test in `TypeCoverageGapTests`. Commit `7c320e7`.
- **#7 (Tenor non-positive offset)** — kept the lenient parse (`D0`, `M-3`); `D0` = same-day in several FIX
  implementations and numeric-range enforcement is a business-layer concern, not a parser invariant (panel
  split 2:1 needs-evidence). Pinned by a characterization test in `TenorTests`. Commit `7c320e7`.
- **G-G (`IParentable<T>.Parent` nullability)** — kept the non-nullable contract. Making it `T?` would ripple
  to all five implementers (`Control_t`, `StateRule_t`, `ReadOnlyControlCollection`, `StrategyPanel_t`,
  `Strategy_t`) and every consumer that reads `.Parent`, pushing null-handling onto callers for a condition
  that **cannot occur in a fully-parsed model**: `ControlCollection.InsertItem` / `StateRuleCollection.InsertItem`
  assign the parent at attach time, before any read. For a Low finding, on the eve of the Phase 4 public-API
  freeze, the cost (nullable surface for every consumer) exceeds the benefit (removing an internal
  "not-yet-attached" sentinel). No code change.
- **`EditEvaluator` / `StateRule_t` "Implement IDisposable"** — no disposable members, no event
  subscriptions (repo-wide grep confirms no `+=` anywhere in the edit/control/state-rule graph); the disposal
  contract was a vestige of the removed Notification assembly (Task A8). TODOs replaced with rationale
  comments. Commit `7d5a4a7`.
- **`EditEvaluatingCollection` "Unbind needed?"** — `Resolve` forwards to each child's `Resolve` only
  (idempotent); no binding is established, and the model is rebuilt fresh per parse. The dead `IBindable<T>`
  interface this question referred to (defined, never implemented or called) was removed. Commit `7d5a4a7`.
- **`ModelUtils` "Move this somewhere better"** — `GetTypeFromName` belongs with the `_types` cache it reads;
  `ModelUtils` is the correct home. TODO removed. Commit `7d5a4a7`.

## Deferred (tracked here — repo issues are disabled)
- **`Clock_t.LocalMktTz` typing** — `LocalMktTz` is stored but not yet applied to initValue resolution:
  `LoadDefaultFromInitValue` compares against the injected `TimeProvider` in the host's local representation.
  Modelling it as a proper timezone type (NodaTime `DateTimeZone` at the boundary, per the house date/time
  convention) and applying it when resolving `initValueMode == 1` is a **feature, not a bug**, and out of
  proportion to a TODO-clearance pass. The GitHub repo has issues disabled, so this is tracked here rather
  than as an issue; the TODO at `Clock_t.cs` was replaced with a comment pointing back to this entry. Commit `8b02621`.

## Already resolved in batch 3 (recorded for completeness)
- **M4** — `ConvertToComparableType` wraps `FormatException`/`OverflowException` → `InvalidFieldValueException`
  (`EditValueConverter.cs`, the `catch` after the type switch).
- **L3** — `ConvertToBool` null guard + `ToUpperInvariant` (`EditValueConverter.cs`).
- **#8** — `Tenor.Parse` catches `OverflowException` alongside `FormatException` (`Tenor.cs`).
- **`NonNegativeIntegerTypeBase`** dead null-guard removed; `ConvertFromWireValueFormat` no longer carries a
  dead null branch and `ValidateValue` only checks `isRequired` (`NonNegativeIntegerTypeBase.cs`).

## Out of scope for Phase 2
- `FixFieldValueProvider` percentage/enum-pair branch coverage and `ReadOnlyControlCollection` integration
  paths are *coverage* gaps (Phase 1 / Phase 3 review remit), not behaviour findings.
