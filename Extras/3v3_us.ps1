﻿while(1) { sleep -sec 15; Invoke-RestMethod "http://localhost:5001/api/leaderboard?bracket=3v3&region=us&locale=en_us"; echo "done" }