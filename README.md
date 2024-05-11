# HLTV.org Scraper API

This C# API allows you to scrape data from HLTV.org for professional CS2 player and team data. It provides filter controls to view data for specific timeframes.

## Overview

The HLTV.org Scraper API is designed to retrieve data from HLTV.org, the leading CS2 esports website, and provide it in a structured format for analysis and integration into other applications.

## Getting Started

To get started with using the HLTV.org Scraper API, follow these steps:

1. Clone the repository.
2. Install the necessary dependencies.
3. Run the API server.

## Usage

### Endpoints

#### `GET /players`

Retrieve a list of professional CS:GO players.

Parameters:

- `filter`: (Optional) Filter players based on specific criteria (e.g., name, team, country).
- `timeframe`: (Optional) Specify a timeframe for the data (e.g., last week, last month, last year).

Example request:

```http
GET /players?filter=dev1ce&timeframe=last_month HTTP/1.1
Host: your-api-host.com
