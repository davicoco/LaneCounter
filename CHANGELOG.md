# Changelog
 
All notable changes to LaneCounter are documented in this file.
 
The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).
 
This changelog starts from the current point in development; earlier history
lives in the commit log.
 
## [Unreleased]
 
### Added
- Aggregate player statistics (recent win rate and average KDA) now shown in the UI.
 
### Changed
 
### Fixed
- Faster and more reliable match loading — matches now fetch in parallel with automatic retry on rate limits.
 
## [1.0.0] - 2026-06-22
 
First public version of LaneCounter — a League of Legends companion app
built with ASP.NET Core (.NET 10) and React/TypeScript.
 
### Added
- Look up a player by Riot ID.
- View ranked league summary (tier, rank, LP, wins/losses).
- View recent match history with champion, KDA, and win/loss per game.
- Backend integration with the Riot Games API for account, league, and match data.
- React/TypeScript frontend deployed on Netlify, backend deployed on Render.