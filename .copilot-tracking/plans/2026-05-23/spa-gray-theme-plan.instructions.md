---
applyTo: '.copilot-tracking/changes/2026-05-23/spa-gray-theme-changes.md'
---
<!-- markdownlint-disable-file -->
# Implementation Plan: SPA Gray Theme Implementation

## Overview

Shift the customer-management Single Page Application (SPA) background colors to a premium dark charcoal gray palette (Option B) for modern, clean visual presentation.

## Objectives

### User Requirements

* Change the SPA background theme to premium gray Option B — Source: conversation context

### Derived Objectives

* Update styles.css custom variables cleanly under :root to maintain consistency — Derived from: Codebase conventions
* Verify visual display contrast for pre-seeded data layout cards — Derived from: HVE design guidelines

## Context Summary

### Project Files

* wwwroot/css/styles.css - Main theme stylesheet holding color configuration properties

### References

* .copilot-tracking/research/2026-05-23/spa-gray-theme-research.md - SPA gray theme research report

### Standards References

* .github/copilot-instructions.md — Base coding conventions

## Implementation Checklist

### [x] Implementation Phase 1: CSS Theme Variables Refinement

<!-- parallelizable: true -->

* [x] Step 1.1: Replace background custom variables inside styles.css to match Option B values
  * Details: .copilot-tracking/details/2026-05-23/spa-gray-theme-details.md (Lines 10-25)
* [x] Step 1.2: Validate change via compilation and browser inspection
  * Details: .copilot-tracking/details/2026-05-23/spa-gray-theme-details.md (Lines 26-38)

### [x] Implementation Phase 2: HVE Walkthrough Documentation

<!-- parallelizable: false -->

* [x] Step 2.1: Write the HVE execution walkthrough document
  * Details: .copilot-tracking/details/2026-05-23/spa-gray-theme-details.md (Lines 39-50)

### [x] Implementation Phase 3: Validation

<!-- parallelizable: false -->

* [x] Step 3.1: Run full project validation
  * Execute C# project tests inside container via `make test`

## Planning Log

See `.copilot-tracking/plans/logs/2026-05-23/spa-gray-theme-log.md` for discrepancy tracking, implementation paths considered, and suggested follow-on work.

## Dependencies

* .NET 8.0 SDK inside Docker dev container
* make utility on host system

## Success Criteria

* SPA primary background matches #16161a — Traces to: User Requirements
* Card components remain fully readable with proper glassmorphic blur — Traces to: Derived Objectives
