<!-- markdownlint-disable-file -->
# Planning Log: SPA Gray Theme Implementation

## Discrepancy Log

Gaps and differences identified between research findings and the implementation plan.

### Unaddressed Research Items

* DR-01: Auto-refresh of browser view during background change
  * Source: .copilot-tracking/research/2026-05-23/spa-gray-theme-research.md (Lines 20-30)
  * Reason: Rebuilding the container via Makefile handles browser updates; live-reloading tooling is not fully set up.
  * Impact: Low

### Plan Deviations from Research

* DD-01: Direct value mapping in styles.css
  * Research recommends: Dynamic client-side dark-mode selector toggle
  * Plan implements: Direct static root property replacement
  * Rationale: Simple color static replacement was requested by user for this iteration.

## Implementation Paths Considered

### Selected: Option B (Minimalist Dark Charcoal)

* Approach: Update `--bg-dark` to `#16161a`, `--bg-surface` to `rgba(36, 36, 45, 0.65)`, and `--bg-surface-hover` to `rgba(45, 45, 56, 0.8)`.
* Rationale: Provides the cleanest visual contrast for white and cyan typography while maintaining high aesthetic quality.
* Evidence: .copilot-tracking/research/2026-05-23/spa-gray-theme-research.md (Lines 25-45)

### IP-01: Option A (Cool Slate Steel)

* Approach: Update CSS variables to a blue-undertone steel slate gray (`#1e212b`).
* Trade-offs: Fits perfectly with the background blue glow, but has a colder aesthetic than neutral charcoal.
* Rejection rationale: Option B was explicitly preferred by the user.

## Suggested Follow-On Work

Items identified during planning that fall outside current scope.

* WI-01: WCAG AA Color Contrast Analysis — Run automated checks on the text contrast elements over the gray card boundaries. (Low Priority)
  * Source: .copilot-tracking/research/2026-05-23/spa-gray-theme-research.md
  * Dependency: None
